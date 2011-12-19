#region License

/* GazeInfo2: Uses QuickLink2DotNet to display info from the eye tracker.
 *
 * Copyright (c) 2011 Justin Weaver
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 * IN THE SOFTWARE.
 */

#endregion License

#region Header Comments

/* $Id: MainForm.cs 38 2011-05-09 01:07:39Z piranther $
 *
 * Description: This program shows info from the eye tracker about the user's
 * gaze.
 */

#endregion Header Comments

using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace GazeInfo2
{
    public partial class MainForm : Form
    {
        #region Fields

        /* The files used to store the password and the calibration
         * information.
         */
        private string filename_PasswordFilename = @"C:\qlsettings.txt";
        private string filename_CalibrationFilename = @"c:\qlcalibration.qlc";

        // The ID of the device we are using.  Fetched from QuickLink2.
        private int devID = -1;

        // True when the form is in the process of closing down.
        private volatile bool isClosing = false;

        // Thread that reads from the device.
        private Thread readerThread;

        #endregion Fields

        #region Init / Cleanup

        public MainForm()
        {
            InitializeComponent();

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);

            // Get the first device's ID.
            try
            {
                this.devID = GetFirstDeviceID();
                this.Display(string.Format("Using device {0}.\n", this.devID));
            }
            catch (Exception e)
            {
                this.Display(e.Message + "\n");
                // Can't continue without a device.
                return;
            }

            // Get the device info.
            try
            {
                QLDeviceInfo devInfo;
                QuickLink2API.QLDevice_GetInfo(this.devID, out devInfo);
                this.Display(string.Format("[Dev{0}] Model:{1}, Serial:{2}, Sensor:{3}x{4}.\n", this.devID, devInfo.modelName, devInfo.serialNumber, devInfo.sensorWidth, devInfo.sensorHeight));
            }
            catch (Exception e)
            {
                this.Display(e.Message + "\n");
                // Can't continue without device info.
                return;
            }

            // Create the capture thread, start it, and wait till it's alive.
            this.readerThread = new Thread(new ThreadStart(this.ReaderThreadTask));
            this.readerThread.Start();
            while (!this.readerThread.IsAlive)
                ;
        }

        // Called when "Exit" is clicked from the menu.
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Called when form is being closed.
        private void FormIsClosing(object sender, FormClosingEventArgs e)
        {
            this.isClosing = true;
            try
            {
                StopDevice(this.devID);
                this.Display("Stopped Device.\n");
            }
            catch (Exception ex)
            {
                this.Display(ex.Message + "\n");
            }
        }

        #endregion Init / Cleanup

        #region Update Main Form's Log and Frame Data Display

        /* We need to update the form, but we need to do it from the reader
         * thread.  Basically the idea here is that this method checks if
         * invoke is required (i.e. it is being called from the reader thread)
         * and then passes a pointer back to itself, so that it can be
         * triggered later from the proper context.
         */
        private delegate void DisplayCallback(string s);
        private void Display(string s)
        {
            if (this.logBox.InvokeRequired)
            {
                DisplayCallback d = new DisplayCallback(this.Display);
                try
                {
                    this.Invoke(d, new object[] { s });
                }
                catch
                {
                }
            }
            else if (!this.isClosing)
            {
                this.logBox.AppendText(s);
                this.logBox.SelectionStart = this.logBox.TextLength;

                /* This stuff is necessary to make sure the text window will
                 * scroll down as we would expect it to.
                 */
                this.logBox.ScrollToCaret();
            }
        }

        /* This gets called by the reader thread to update the info display on
         * the form.  The trick is that the form's controls need to be called
         * from the context of the thread that instantiated the form instance.
         * So, we do the same trick as we do for Display() above: We
         * check for invoke required before we do anything.
         */
        private delegate void UpdateReadoutCallback(QLFrameData frame);
        private void UpdateReadout(QLFrameData frame)
        {
            if (this.textBox_Timestamp.InvokeRequired)
            {
                UpdateReadoutCallback u = new UpdateReadoutCallback(this.UpdateReadout);
                try
                {
                    this.Invoke(u, new object[] { frame });
                }
                catch
                {
                }
            }
            else if (!this.isClosing)
            {
                // Update the general info.
                this.textBox_Timestamp.Text = frame.ImageData.Timestamp.ToString("F3");
                this.textBox_FrameNumber.Text = frame.ImageData.FrameNumber.ToString();
                this.textBox_Focus.Text = frame.Focus.ToString();
                this.textBox_Distance.Text = frame.Distance.ToString() + " cm";
                this.textBox_Dimensions.Text = frame.ImageData.Width.ToString() + "x" + frame.ImageData.Height.ToString();
                this.textBox_Gain.Text = frame.ImageData.Gain.ToString();

                // Update the left eye.
                this.textBox_LeftEyeFound.Text = frame.LeftEye.Found.ToString();
                if (frame.LeftEye.Found)
                {
                    this.textBox_LeftEyeCalibrated.Text = frame.LeftEye.Calibrated.ToString();
                    this.textBox_LeftEyePupil.Text = frame.LeftEye.Pupil.x.ToString("F0") + "," + frame.LeftEye.Pupil.y.ToString("F0");
                    this.textBox_LeftEyePupilDiameter.Text = frame.LeftEye.PupilDiameter.ToString("F1") + " mm";
                    this.textBox_LeftEyeGlint0.Text = frame.LeftEye.Glint0.x.ToString("F0") + "," + frame.LeftEye.Glint0.y.ToString("F0");
                    this.textBox_LeftEyeGlint1.Text = frame.LeftEye.Glint1.x.ToString("F0") + "," + frame.LeftEye.Glint1.y.ToString("F0");
                    if (frame.LeftEye.Calibrated)
                    {
                        this.textBox_LeftEyeGazePointX.Text = frame.LeftEye.GazePoint.x.ToString("F0") + "%";
                        this.textBox_LeftEyeGazePointY.Text = frame.LeftEye.GazePoint.y.ToString("F0") + "%";
                    }
                    else
                    {
                        this.textBox_LeftEyeGazePointX.Text = "--%";
                        this.textBox_LeftEyeGazePointY.Text = "--%";
                    }
                }
                else
                {
                    this.textBox_LeftEyeCalibrated.Text = "--";
                    this.textBox_LeftEyePupil.Text = "--";
                    this.textBox_LeftEyePupilDiameter.Text = "-- mm";
                    this.textBox_LeftEyeGlint0.Text = "--";
                    this.textBox_LeftEyeGlint1.Text = "--";
                    this.textBox_LeftEyeGazePointX.Text = "--%";
                    this.textBox_LeftEyeGazePointY.Text = "--%";
                }

                // Update the right eye.
                this.textBox_RightEyeFound.Text = frame.RightEye.Found.ToString();
                if (frame.RightEye.Found)
                {
                    this.textBox_RightEyeCalibrated.Text = frame.RightEye.Calibrated.ToString();
                    this.textBox_RightEyePupil.Text = frame.RightEye.Pupil.x.ToString("F0") + "," + frame.RightEye.Pupil.y.ToString("F0");
                    this.textBox_RightEyePupilDiameter.Text = frame.RightEye.PupilDiameter.ToString("F1") + " mm";
                    this.textBox_RightEyeGlint0.Text = frame.RightEye.Glint0.x.ToString("F0") + "," + frame.RightEye.Glint0.y.ToString("F0");
                    this.textBox_RightEyeGlint1.Text = frame.RightEye.Glint1.x.ToString("F0") + "," + frame.RightEye.Glint1.y.ToString("F0");
                    if (frame.RightEye.Calibrated)
                    {
                        this.textBox_RightEyeGazePointX.Text = frame.RightEye.GazePoint.x.ToString("F0") + "%";
                        this.textBox_RightEyeGazePointY.Text = frame.RightEye.GazePoint.y.ToString("F0") + "%";
                    }
                    else
                    {
                        this.textBox_RightEyeGazePointX.Text = "--%";
                        this.textBox_RightEyeGazePointY.Text = "--%";
                    }
                }
                else
                {
                    this.textBox_RightEyeCalibrated.Text = "--";
                    this.textBox_RightEyePupil.Text = "--";
                    this.textBox_RightEyePupilDiameter.Text = "-- mm";
                    this.textBox_RightEyeGlint0.Text = "--";
                    this.textBox_RightEyeGlint1.Text = "--";
                    this.textBox_RightEyeGazePointX.Text = "--%";
                    this.textBox_RightEyeGazePointY.Text = "--%";
                }

                // Update the weighted gaze point.
                if (((frame.LeftEye.Found && frame.LeftEye.Calibrated) || (frame.RightEye.Found && frame.RightEye.Calibrated)) && frame.WeightedGazePoint.Valid)
                {
                    this.textBox_GazePointX.Text = frame.WeightedGazePoint.x.ToString("F0") + "%";
                    this.textBox_GazePointY.Text = frame.WeightedGazePoint.y.ToString("F0") + "%";
                    this.textBox_LeftWeight.Text = frame.WeightedGazePoint.LeftWeight.ToString();
                    this.textBox_RightWeight.Text = frame.WeightedGazePoint.RightWeight.ToString();
                }
                else
                {
                    this.textBox_GazePointX.Text = "--%";
                    this.textBox_GazePointY.Text = "--%";
                    this.textBox_LeftWeight.Text = "--";
                    this.textBox_RightWeight.Text = "--";
                }
            }
        }

        #endregion Update Main Form's Log and Frame Data Display

        #region Device Control

        /* Find the first eye tracker on the system.  Returns the device
         * number.  Throws exception on error.
         */
        private static int GetFirstDeviceID()
        {
            // Allocate a buffer for the device ID array.
            int bufferSize = 100;
            int[] deviceIds = new int[bufferSize];
            int numDevices = bufferSize;

            // Enumerate the eye tracker devices.
            QLError qlerror = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIds);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QuickLink2API.QLDevice_Enumerate() returned {0}.", qlerror.ToString()));

            if (numDevices == 0)
                // No devices detected.
                throw new Exception("No eye trackers detected.");

            // Return the ID of the first device.
            return deviceIds[0];
        }

        /* Start the eye tracker.  Throws exception on error.
         */
        private static void StartDevice(int deviceID)
        {
            QLError qlerror;

            // Start the device.
            qlerror = QuickLink2API.QLDevice_Start(deviceID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_Start() returned {0}", qlerror.ToString()));
        }

        /* Stop the eye tracker.  Throws exception on error.
         */
        private static void StopDevice(int deviceID)
        {
            QLError qlerror;

            // Stop the device.
            qlerror = QuickLink2API.QLDevice_Stop(deviceID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_Stop() returned {0}", qlerror.ToString()));
        }

        /* Loads the device password from a file.  Returns the password string.
         * Throws exception on error.
         */
        private static string LoadDevicePassword(int deviceID, string loadFilename)
        {
            QLError qlerror;

            // Create a new settings container.
            int settingsID;
            qlerror = QuickLink2API.QLSettings_Create(0, out settingsID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QL_Settings_Create() returned {0}", qlerror.ToString()));

            // Read the settings out of a file.
            qlerror = QuickLink2API.QLSettings_Load(loadFilename, ref settingsID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLSettings_Load() returned {0}", qlerror.ToString()));

            // Get the device's serial number.
            QLDeviceInfo devInfo;
            qlerror = QuickLink2API.QLDevice_GetInfo(deviceID, out devInfo);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QL_Settings_Create() returned {0}", qlerror.ToString()));

            // Check for the device password already in settings.
            int buffSize = 25;
            System.Text.StringBuilder password = new System.Text.StringBuilder(buffSize + 1);
            qlerror = QuickLink2API.QLSettings_GetValueString(settingsID, "SN_" + devInfo.serialNumber, buffSize, password);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLSettings_GetValueString() returned {0}", qlerror.ToString()));

            // Set the password on the device.
            qlerror = QuickLink2API.QLDevice_SetPassword(deviceID, password.ToString());
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_SetPassword() returned {0}", qlerror.ToString()));

            // Return the password.
            return password.ToString();
        }

        /* Loads calibration from a file.  Throws exception on error.
         */
        private static void LoadDeviceCalibration(int deviceID, string loadFilename)
        {
            QLError qlerror;

            // Create a new calibration container.
            int calibrationID;
            qlerror = QuickLink2API.QLCalibration_Create(0, out calibrationID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLCalibration_Create() returned {0}", qlerror.ToString()));

            // Load the calibration out of a file.
            qlerror = QuickLink2API.QLCalibration_Load(loadFilename, ref calibrationID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLCalibration_Load() returned {0}", qlerror.ToString()));

            // Apply the calibration.
            qlerror = QuickLink2API.QLDevice_ApplyCalibration(deviceID, calibrationID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_ApplyCalibration() returned {0}", qlerror.ToString()));
        }

        #endregion Device Control

        #region Device Reader Thread

        /* This thread code periodically reads a new frame from the device and
         * triggers an update to the form's display.
         */
        private void ReaderThreadTask()
        {
            // Attempt to load the device password from a file.
            try
            {
                LoadDevicePassword(this.devID, this.filename_PasswordFilename);
                this.Display("Loaded password from settings file.\n");
            }
            catch (Exception)
            {
                this.Display(string.Format("Unable to load password from file '{0}.  Try running the Calibrate example first to generate the file.'\n", this.filename_PasswordFilename));
                // Can't continue without password.
                return;
            }

            // Start the device.
            try
            {
                StartDevice(this.devID);
                this.Display("Device has been started.\n");
            }
            catch (Exception ex)
            {
                this.Display(ex.Message + "\n");
                // Can't continue if device is not started.
                return;
            }

            // Attempt to load the device calibration from a file.
            try
            {
                LoadDeviceCalibration(this.devID, this.filename_CalibrationFilename);
                this.Display("Loaded calibration from calibration file.\n");
            }
            catch (Exception)
            {
                this.Display(string.Format("Unable to load calibration from file '{0}'.  Try running the Calibrate example first to generate the file.\n", this.filename_CalibrationFilename));
            }

            // Delay between updates (ms).
            int delay = 1000;

            this.Display(string.Format("Reading from device.  Updating Every: {0} ms.\n", delay));

            // Read frames from the device.
            while (!this.isClosing)
            {
                // Read a new data sample.
                QLFrameData frame = new QLFrameData();
                QLError qlerror = QuickLink2API.QLDevice_GetFrame(this.devID, 0, ref frame); // 0 = no waiting.
                if (qlerror == QLError.QL_ERROR_OK)
                {
                    // Update the form's display.
                    UpdateReadout(frame);

                    if (delay > 0)
                        Thread.Sleep(delay);
                }
                else if (qlerror == QLError.QL_ERROR_TIMEOUT_ELAPSED)
                {
                    // Timeout without a frame.  Just try again.
                }
                else
                {
                    // Attempting to get a frame resulted in an error!
                    this.Display(string.Format("QLDevice_GetFrame() returned {0}\n", qlerror.ToString()));
                }
            }
        }

        #endregion Device Reader Thread
    }
}
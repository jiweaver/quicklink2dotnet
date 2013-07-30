#region License

/* GazeInfo2: Uses QuickLink2DotNet to display info from the eye tracker.
 *
 * Copyright (c) 2011-2013 Justin Weaver
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
        #region Configuration

        /// <summary>
        /// Minimum delay between successful frame reads (ms).
        /// </summary>
        private const int MinDelayBetweenReads = 1000;

        /// <summary>
        /// Maximum time for the reader thread to block and wait for a new
        /// frame from the eye tracker before reporting error to the log and
        /// trying again.
        /// </summary>
        private const int MaxFrameWaitTime = 240;

        #endregion Configuration

        #region Fields

        private static string dirname = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QuickLink2DotNet");

        // The file used to store the password.
        private static string filename_Password = System.IO.Path.Combine(dirname, "qlsettings.txt");

        // The file used to store the calibration information.
        private static string filename_Calibration = System.IO.Path.Combine(dirname + "qlcalibration.qlc");

        // The ID of the device we are using.  Fetched from QuickLink2.
        private int devID = -1;

        // True when the form is in the process of closing down.
        private volatile bool isClosing = false;

        // Thread that reads from the device.
        private Thread readerThread;

        // Lock used for thread synchronization (wait/pulse).
        private object l = new object();

        // Frame struct is in use.
        private bool frameInUse;

        #endregion Fields

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);

            // Get the first device's ID.
            try
            {
                int[] deviceIDs = QLHelper.GetDeviceIDs();
                if (deviceIDs.Length == 0)
                {
                    this.Display("No eye trackers found.");
                    return;
                }
                this.devID = deviceIDs[0];
                this.Display(string.Format("Using device {0}.\n", this.devID));
            }
            catch (QLErrorException e)
            {
                this.Display(e.Message + "\n");
                // Can't continue without a device.
                return;
            }
            catch (DllNotFoundException e)
            {
                this.Display(e.Message + "\n");
                return;
            }

            // Get the device info.
            QLDeviceInfo devInfo;
            QuickLink2API.QLDevice_GetInfo(this.devID, out devInfo);
            this.Display(string.Format("[Dev{0}] Model:{1}, Serial:{2}, Sensor:{3}x{4}.\n", this.devID, devInfo.modelName, devInfo.serialNumber, devInfo.sensorWidth, devInfo.sensorHeight));

            // Create the capture thread, start it, and wait till it's alive.
            this.readerThread = new Thread(new ThreadStart(this.ReaderThreadTask));
            this.readerThread.Start();
            while (!this.readerThread.IsAlive)
                ;
        }

        #endregion Constructors

        #region User Exit Click

        /// <summary>
        /// The user selected "Exit" from the Form's menu.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion User Exit Click

        #region FormClosing Event

        /// <summary>
        /// The Form is closing.
        /// </summary>
        private void FormIsClosing(object sender, FormClosingEventArgs e)
        {
            this.isClosing = true;

            // Wake the reader thread.
            lock (this.l)
                Monitor.Pulse(this.l);
        }

        #endregion FormClosing Event

        #region Log Display

        private delegate void DisplayCallback(string s);

        /// <summary>
        /// We need to update the form, but sometimes we need to do it from
        /// another context.  If invoke is required, then this method wraps
        /// itself in a delegate and passes it to Invoke so it can be called
        /// properly.
        /// </summary>
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

                // Make sure the window scrolls down with new text as expected.
                this.logBox.ScrollToCaret();
            }
        }

        #endregion Log Display

        #region Form Readout Display

        private delegate void UpdateReadoutCallback(ref QLFrameData frame);

        /// <summary>
        /// We need to update the form, but sometimes we need to do it from
        /// another context.  If invoke is required, then this method wraps
        /// itself in a delegate and passes it to Invoke so it can be called
        /// properly.
        /// </summary>
        private void UpdateReadout(ref QLFrameData frame)
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

                // Request another frame.
                this.frameInUse = false;
                lock (this.l)
                    Monitor.Pulse(this.l);
            }
        }

        #endregion Form Readout Display

        #region Frame Reader Thread

        /// <summary>
        /// This thread code starts the device, reads and frame, and then
        /// sleeps.  When the PictureBox paint event wakes it up, then it reads
        /// another frame.  When the form is closing, it stops the device and
        /// exits.
        /// </summary>
        public void ReaderThreadTask()
        {
            string password;

            // Attempt to load the device password from a file.
            try
            {
                password = QLHelper.LoadDevicePasswordFromFile(this.devID, filename_Password);
                this.Display("Loaded password from settings file.\n");
            }
            catch (QLErrorException e)
            {
                this.Display(e.Message + "\n");
                // Can't continue without password.
                return;
            }
            catch (DllNotFoundException e)
            {
                this.Display(e.Message + "\n");
                // Can't continue without password.
                return;
            }

            // Write the password to the device.
            QLError error = QuickLink2API.QLDevice_SetPassword(this.devID, password);
            if (error != QLError.QL_ERROR_OK)
            {
                this.Display(string.Format("QLDevice_SetPassword() returned {0}.", error.ToString()));
                return;
            }

            // Start the device.
            error = QuickLink2API.QLDevice_Start(this.devID);
            if (error != QLError.QL_ERROR_OK)
            {
                this.Display(string.Format("QLDevice_Start() returned {0}", error.ToString()));
                return;
            }
            this.Display("Device has been started.\n");

            // Load the calibration out of a file into a new calibration container.
            int calibrationID = -1;
            error = QuickLink2API.QLCalibration_Load(filename_Calibration, ref calibrationID);
            if (error == QLError.QL_ERROR_INVALID_PATH)
            {
                this.Display(string.Format("The specified calibration file '{0}' does not exist", filename_Calibration));
                return;
            }
            else if (error != QLError.QL_ERROR_OK)
            {
                this.Display(string.Format("QLCalibration_Load() returned {0}.", error.ToString()));
                return;
            }

            // Apply the calibration.
            error = QuickLink2API.QLDevice_ApplyCalibration(this.devID, calibrationID);
            if (error == QLError.QL_ERROR_INVALID_DEVICE_ID)
            {
                this.Display(string.Format("Invalid device ID: {0}.", this.devID));
                return;
            }
            else if (error != QLError.QL_ERROR_OK)
            {
                this.Display(string.Format("QLDevice_ApplyCalibration returned {0}.", error.ToString()));
                return;
            }
            this.Display("Calibration has been loaded and applied.\n");

            this.Display(string.Format("Reading from device.  Updating Every: {0} ms.\n", MinDelayBetweenReads));

            // Create an empty frame structure to hold the data we read.
            QLFrameData frame = new QLFrameData();

            while (true)
            {
                // Sleep while the last frame is in use and we aren't shutting down.
                lock (this.l)
                    while (this.frameInUse && !this.isClosing)
                        Monitor.Wait(this.l);

                // Break if the program is closing.
                if (this.isClosing)
                    break;

                // Read a new data sample.
                QLError qlerror = QuickLink2API.QLDevice_GetFrame(this.devID, MaxFrameWaitTime, ref frame);
                if (qlerror == QLError.QL_ERROR_OK)
                {
                    // Tell the paint event handler to display the frame.
                    this.frameInUse = true;

                    // Update the form's display.
                    UpdateReadout(ref frame);

                    // Sleep the configured delay time.
                    if (MinDelayBetweenReads > 0)
                        Thread.Sleep(MinDelayBetweenReads);
                }
                else
                {
                    // Attempting to get a frame resulted in an error!
                    this.Display(string.Format("QLDevice_GetFrame() returned {0}\n", qlerror.ToString()));
                }
            }

            // Stop the device.
            error = QuickLink2API.QLDevice_Stop(this.devID);
            if (error != QLError.QL_ERROR_OK)
            {
                this.Display(string.Format("QLDevice_Stop() returned {0}", error.ToString()));
            }

            this.Display("Stopped Device.\n");
        }

        #endregion Frame Reader Thread
    }
}
#region License

/* Calibrator: This program provides 5, 9, or 16 point full screen calibration.
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
 * Description: This program provides 5, 9, or 16 point full screen calibration.
 */

#endregion Header Comments

using System;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace Calibrate
{
    public partial class MainForm : Form
    {
        #region Fields

        /* The files used to store the password and the calibration
         * information.
         */
        private string filename_Password = @"C:\qlsettings.txt";
        private string filename_Calibration = @"C:\qlcalibration.qlc";

        // The ID of the device we are using.  Fetched from QuickLink2.
        private int devID = -1;

        // True when the form is in the process of closing down.
        private volatile bool isClosing = false;

        #endregion Fields

        #region Init / Cleanup

        public MainForm()
        {
            InitializeComponent();

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);

            this.comboBox_CalibrationType.DataSource = Enum.GetValues(typeof(QLCalibrationType));

            this.numericUpDown_TargetDuration.Value = 1500;

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
                // Can't continue without device serial number.
                return;
            }

            // Load the device password from a file.
            try
            {
                this.textBox_Password.Text = LoadDevicePassword(this.devID, this.filename_Password);
                this.Display(string.Format("Loaded password {0} from settings file.\n", this.textBox_Password.Text));
            }
            catch (Exception)
            {
                this.Display("WARNING: No password file found.  Create one by enter the device's password above and performing a calibration.");
            }
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

        #region Update Main Form's Log

        // Basically the idea here is that this function passes a pointer back
        // to itself, so that it can be called again from the proper context,
        // in due time.  This delegate is used to encapsulate the callback
        // pointer.
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

                // This stuff is necessary to make sure the text window will
                // scroll down as we would expect it to.
                this.logBox.ScrollToCaret();
            }
        }

        #endregion Update Main Form's Log

        #region Form Controls

        private void button_BeginCalibration_Click(object sender, EventArgs e)
        {
            /* Attempt to set the device's password to the value currently in
             * the form's password textbox, and then save it to a file.
             */
            try
            {
                SetandSavePassword(this.devID, this.textBox_Password.Text, this.filename_Password);
                this.Display("Password set.\n");
            }
            catch (Exception ex)
            {
                this.Display(ex.Message + "\n");
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

            // Perform calibration.
            QLCalibrationType calType = (QLCalibrationType)this.comboBox_CalibrationType.SelectedValue;
            int duration = Convert.ToInt32(this.numericUpDown_TargetDuration.Value);
            try
            {
                PerformCalibration(this.devID, calType, duration, this.filename_Calibration);
                this.Display("Calibration complete.\n");
            }
            catch (Exception ex)
            {
                this.Display(ex.Message + "\n");
            }
        }

        #endregion Form Controls

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

        /* Sets the eye tracker's password and saves it to a file.  Throws
         * exception on error.
         */
        private static void SetandSavePassword(int deviceID, string password, string saveFilename)
        {
            QLError qlerror;

            // Set the password.
            qlerror = QuickLink2API.QLDevice_SetPassword(deviceID, password);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_SetPassword() returned {0}", qlerror.ToString()));

            // Get the device info so we have its serial number.
            QLDeviceInfo devInfo;
            qlerror = QuickLink2API.QLDevice_GetInfo(deviceID, out devInfo);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_GetInfo() returned {0}", qlerror.ToString()));

            // Create a new settings container.
            int settingsID;
            qlerror = QuickLink2API.QLSettings_Create(0, out settingsID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QL_Settings_Create() returned {0}", qlerror.ToString()));

            // Add the password field to the settings in our new container.
            qlerror = QuickLink2API.QLSettings_AddSetting(settingsID, "SN_" + devInfo.serialNumber);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QL_Settings_AddSetting() returned {0}", qlerror.ToString()));

            // Set the password field in our container.
            qlerror = QuickLink2API.QLSettings_SetValueString(settingsID, "SN_" + devInfo.serialNumber, password.ToString());
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QL_Settings_SetValueString() returned {0}", qlerror.ToString()));

            // Save the settings in our container to a settings file.
            qlerror = QuickLink2API.QLSettings_Save(saveFilename, settingsID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QL_Settings_Save() returned {0}", qlerror.ToString()));
        }

        /* Performs a new calibration, saves it to a file, and loads it into
         * the eye tracker.  Throws exception on error.
         */
        private static void PerformCalibration(int deviceID, QLCalibrationType calType, int duration, string saveFilename)
        {
            QLError qlerror;

            using (CalibrationForm calibrationForm = new CalibrationForm())
            {
                int calibrationID;

                try
                {
                    calibrationID = calibrationForm.PerformCalibration(deviceID, calType, duration);
                }
                catch (Exception ex)
                {
                    throw new Exception("CalibrationForm.PerformCalibration() failed with message: " + ex.Message);
                }

                // Calibration succeeded.  Save it to a file.
                qlerror = QuickLink2API.QLCalibration_Save(saveFilename, calibrationID);
                if (qlerror != QLError.QL_ERROR_OK)
                    throw new Exception(string.Format("QLCalibration_Save() returned {0}", qlerror.ToString()));
            }
        }

        #endregion Device Control
    }
}
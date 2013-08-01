#region License

/* Calibrator: This program provides 5, 9, or 16 point full screen calibration.
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
 * Description: This program provides 5, 9, or 16 point full screen calibration.
 */

#endregion Header Comments

using System;
using System.Threading;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace Calibrate2
{
    public partial class MainForm : Form
    {
        #region Fields

        private static string dirname = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QuickLink2DotNet");

        // The file used to store the password.
        private static string filename_Password = System.IO.Path.Combine(dirname, "qlsettings.txt");

        // The file used to store the calibration information.
        private static string filename_Calibration = System.IO.Path.Combine(dirname, "qlcalibration.qlc");

        // The ID of the device we are using.  Fetched from QuickLink2.
        private int devID = -1;

        // True when the form is in the process of closing down.
        private volatile bool isClosing = false;

        #endregion Fields

        #region Constructors

        public MainForm(int deviceID)
        {
            InitializeComponent();

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);

            this.comboBox_CalibrationType.DataSource = Enum.GetValues(typeof(QLCalibrationType));

            this.numericUpDown_TargetDuration.Value = 1500;

            this.devID = deviceID;

            this.Display(string.Format("Using device {0}.\n", this.devID));

            // Get the device info.
            QLDeviceInfo devInfo;
            QuickLink2API.QLDevice_GetInfo(this.devID, out devInfo);
            this.Display(string.Format("[Dev{0}] Model:{1}, Serial:{2}, Sensor:{3}x{4}.\n", this.devID, devInfo.modelName, devInfo.serialNumber, devInfo.sensorWidth, devInfo.sensorHeight));

            if (!System.IO.Directory.Exists(dirname))
            {
                // Create the program data directory
                try
                {
                    System.IO.Directory.CreateDirectory(dirname);
                    this.Display(string.Format("Created directory {0} to store settings.\n", dirname));
                }
                catch (Exception e)
                {
                    this.Display(string.Format("Could not create settings directory '{0}'.  MSG: {1}\n", dirname, e.Message));
                    return;
                }
            }

            string password;
            // Attempt to load the device password from a file.
            try
            {
                password = QLHelper.LoadDevicePasswordFromFile(this.devID, filename_Password);
                this.Display("Loaded password from settings file.\n");
            }
            catch (ArgumentException e)
            {
                this.Display(e.Message + "\n");
                return;
            }
            catch (QLErrorException e)
            {
                this.Display(e.Message + "\n");
                return;
            }

            // Write the password to the device.
            QLError error = QuickLink2API.QLDevice_SetPassword(this.devID, password);
            if (error != QLError.QL_ERROR_OK)
            {
                this.Display(string.Format("QLDevice_SetPassword() returned {0}.", error.ToString()));
                return;
            }
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

            // Stop the device.
            try
            {
                QLError error = QuickLink2API.QLDevice_Stop(this.devID);
                if (error != QLError.QL_ERROR_OK)
                {
                    throw new Exception(string.Format("QLDevice_Stop() returned {0}", error.ToString()));
                }
                this.Display("Stopped Device.\n");
            }
            catch (Exception ex)
            {
                this.Display(ex.Message + "\n");
            }
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

        #region Calibration

        private void button_BeginCalibration_Click(object sender, EventArgs e)
        {
            // Attempt to set the device's password to the value currently in
            // the form's password textbox.
            try
            {
                QLError error = QuickLink2API.QLDevice_SetPassword(this.devID, this.textBox_Password.Text);
                if (error != QLError.QL_ERROR_OK)
                {
                    throw new Exception(string.Format("QLDevice_SetPassword() returned {0}", error.ToString()));
                }
                this.Display("Password set.\n");
            }
            catch (Exception ex)
            {
                this.Display(ex.Message + "\n");
            }

            // Save the password to a file for later use.
            try
            {
                QLHelper.SaveDevicePasswordToFile(this.devID, this.textBox_Password.Text, filename_Password);
                this.Display("Password saved.\n");
            }
            catch (Exception ex)
            {
                this.Display("Saving Password: " + ex.Message + this.devID + "|" + this.textBox_Password.Text + "|" + filename_Password + "\n");
            }

            // Start the device.
            try
            {
                QLError error = QuickLink2API.QLDevice_Start(this.devID);
                if (error != QLError.QL_ERROR_OK)
                {
                    throw new Exception(string.Format("QLDevice_Start() returned {0}", error.ToString()));
                }
                this.Display("Device has been started.\n");
            }
            catch (Exception ex)
            {
                this.Display(ex.Message + "\n");
                // Can't continue if device is not started.
                return;
            }

            // Wait a moment to let the user prepare themselves.
            Thread.Sleep(1000);

            // Perform calibration.
            QLCalibrationType calType = (QLCalibrationType)this.comboBox_CalibrationType.SelectedValue;
            int duration = Convert.ToInt32(this.numericUpDown_TargetDuration.Value);
            try
            {
                PerformCalibration(this.devID, calType, duration, filename_Calibration);
                this.Display("Calibration complete.\n");
            }
            catch (Exception ex)
            {
                this.Display(ex.Message + "\n");
            }

            // Stop the device.
            try
            {
                QLError error = QuickLink2API.QLDevice_Stop(this.devID);
                if (error != QLError.QL_ERROR_OK)
                {
                    throw new Exception(string.Format("QLDevice_Stop() returned {0}", error.ToString()));
                }
                this.Display("Stopped Device.\n");
            }
            catch (Exception ex)
            {
                this.Display(ex.Message + "\n");
            }
        }

        /// <summary>
        /// Performs a new calibration, saves it to a file, and loads it into
        /// the eye tracker.  Throws exception on error.
        /// </summary>
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

        #endregion Calibration
    }
}
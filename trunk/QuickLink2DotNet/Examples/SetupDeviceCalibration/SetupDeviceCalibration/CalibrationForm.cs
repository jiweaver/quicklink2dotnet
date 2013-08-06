#region License

/* QuickLink2DotNet SetupDeviceCalibration Example: Displays a list of
 * available devices and prompts the user to select one.  Then performs device
 * calibration.  The calibration is saved in the file
 * "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlcalibration.qlc" for later
 * use.
 *
 * Copyright © 2011-2013 Justin Weaver
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

/* $Id: QLTypes.cs 38 2011-05-09 01:07:39Z piranther $
 *
 * Author: Justin Weaver
 * Homepage: http://quicklinkapi4net.googlecode.com
 */

#endregion Header Comments

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace SetupDeviceCalibration
{
    public partial class CalibrationForm : Form
    {
        #region Fields

        // Tells the PictureBox paint event handler to draw the next target.
        private volatile bool drawTarget;

        // A lock object used for sleep/wake notification between the
        // calibration method and the paint event handler.
        private object l = new object();

        // Tells the PictureBox paint event handler where to draw the next
        // target.
        private int x, y;

        public int CalibrationID;

        public QLCalibrationType CalibrationType;
        public int TargetDuration;
        public int NumberOfTargets;
        public QLCalibrationScore[] LeftScores;
        public QLCalibrationScore[] RightScores;
        public QLCalibrationTarget[] Targets;

        #endregion Fields

        #region Constructor

        public CalibrationForm(QLCalibrationType calibrationType, int targetDuration)
        {
            InitializeComponent();

            this.CalibrationType = QLCalibrationType.QL_CALIBRATION_TYPE_5;
            this.TargetDuration = targetDuration;
            this.CalibrationID = -1;

            switch (calibrationType)
            {
                case QLCalibrationType.QL_CALIBRATION_TYPE_5:
                    this.NumberOfTargets = 5;
                    break;

                case QLCalibrationType.QL_CALIBRATION_TYPE_9:
                    this.NumberOfTargets = 9;
                    break;

                case QLCalibrationType.QL_CALIBRATION_TYPE_16:
                default:
                    this.NumberOfTargets = 16;
                    break;
            }

            this.LeftScores = new QLCalibrationScore[this.NumberOfTargets];
            this.RightScores = new QLCalibrationScore[this.NumberOfTargets];
            this.Targets = new QLCalibrationTarget[this.NumberOfTargets];

            // Set the form size so that it covers the full screen.
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.calibrationPictureBox.ClientSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            this.TopMost = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Hide();
        }

        #endregion Constructor

        #region Update the Display

        // Draw a circle.
        private static void DrawCircle(Graphics g, int x, int y, int r, Color c)
        {
            int locX = x - r;
            int locY = y - r;
            using (Brush b = new SolidBrush(c))
                g.FillEllipse(b, locX, locY, r * 2, r * 2);
        }

        // Draw a calibration target.
        private static void DrawTarget(Graphics g, int x, int y)
        {
            DrawCircle(g, x, y, 80, Color.Black);
            DrawCircle(g, x, y, 78, Color.Gray);
            DrawCircle(g, x, y, 10, Color.Yellow);
        }

        // Repaint the picturebox.
        private void calibrationPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (this.drawTarget)
            {
                // We have been commanded to draw the current target.

                Graphics g = e.Graphics;

                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Paint the target.
                DrawTarget(g, this.x, this.y);

                this.drawTarget = false;

                // Tell the calibration thread we are done drawing the target.
                lock (this.l)
                    Monitor.Pulse(this.l);
            }
        }

        #endregion Update the Display

        #region PerformCalibration

        public bool CalibrateTarget(int targetIndex)
        {
            QLError error;

            this.x = (int)Math.Truncate((this.Targets[targetIndex].x / 100f) * (float)this.Size.Width);
            this.y = (int)Math.Truncate((this.Targets[targetIndex].y / 100f) * (float)this.Size.Height);

            while (true)
            {
                // Tell the picture box to draw the new target, and wait for it
                // to wake us.
                this.drawTarget = true;
                this.calibrationPictureBox.Refresh();
                lock (this.l)
                {
                    while (this.drawTarget)
                    {
                        Monitor.Wait(this.l);
                    }
                }

                // Calibrate the target.
                error = QuickLink2API.QLCalibration_Calibrate(this.CalibrationID, this.Targets[targetIndex].targetId, this.TargetDuration, true);
                if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("QLCalibration_Calibrate() retuned {0}.", error.ToString());
                    return false;
                }

                // Get the status of the last target.
                QLCalibrationStatus status;
                error = QuickLink2API.QLCalibration_GetStatus(this.CalibrationID, this.Targets[targetIndex].targetId, out status);
                if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("QLCalibration_GetStatus() retuned {0}.", error.ToString());
                    return false;
                }

                if (status == QLCalibrationStatus.QL_CALIBRATION_STATUS_NO_LEFT_DATA)
                {
                    DialogResult result = MessageBox.Show("Left eye not found.  Retry?", "Left Eye Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result != DialogResult.Yes)
                    {
                        Console.WriteLine("User cancelled.");
                        return false;
                    }
                    continue;
                }
                else if (status == QLCalibrationStatus.QL_CALIBRATION_STATUS_NO_RIGHT_DATA)
                {
                    DialogResult result = MessageBox.Show("Right eye not found.  Retry?", "Right Eye Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result != DialogResult.Yes)
                    {
                        Console.WriteLine("User cancelled.");
                        return false;
                    }
                    continue;
                }
                else if (status == QLCalibrationStatus.QL_CALIBRATION_STATUS_NO_DATA)
                {
                    DialogResult result = MessageBox.Show("Neither eye found.  Retry?", "Neither Eye Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result != DialogResult.Yes)
                    {
                        Console.WriteLine("User cancelled.");
                        return false;
                    }
                    continue;
                }
                else if (status == QLCalibrationStatus.QL_CALIBRATION_STATUS_OK)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Calibration failed!");
                    return false;
                }
            }

            return true;
        }

        public bool GetScores()
        {
            for (int i = 0; i < this.NumberOfTargets; i++)
            {
                QLError error = QuickLink2API.QLCalibration_GetScoring(this.CalibrationID, this.Targets[i].targetId, QLEyeType.QL_EYE_TYPE_LEFT, out this.LeftScores[i]);
                if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("QLCalibration_GetScoring(left) retuned {0}.", error.ToString());
                    return false;
                }

                error = QuickLink2API.QLCalibration_GetScoring(this.CalibrationID, this.Targets[i].targetId, QLEyeType.QL_EYE_TYPE_RIGHT, out this.RightScores[i]);
                if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("QLCalibration_GetScoring(right) retuned {0}.", error.ToString());
                    return false;
                }
            }

            return true;
        }

        public bool PerformCalibration(int deviceID)
        {
            // Create a new calibration container.
            QLError error = QuickLink2API.QLCalibration_Create(0, out this.CalibrationID);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLCalibration_Create() retuned {0}.", error.ToString());
                return false;
            }

            error = QuickLink2API.QLCalibration_Initialize(deviceID, this.CalibrationID, this.CalibrationType);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLCalibration_Initialize() retuned {0}.", error.ToString());
                return false;
            }

            int numTargets = this.NumberOfTargets;
            error = QuickLink2API.QLCalibration_GetTargets(this.CalibrationID, ref numTargets, this.Targets);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLCalibration_GetTargets() retuned {0}.", error.ToString());
                return false;
            }
            if (numTargets != this.NumberOfTargets)
            {
                Console.WriteLine("QLCalibration_GetTargets() returned an unexpected number of targets.  Expected {0}; got {1}.", this.NumberOfTargets, numTargets);
                return false;
            }

            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Show();

            // Make sure the screen is clear of targets.
            this.drawTarget = false;
            this.calibrationPictureBox.Refresh();

            bool result;

            // For each target, draw it and then perform calibration.
            for (int i = 0; i < numTargets; i++)
            {
                result = CalibrateTarget(i);

                if (!result)
                {
                    this.TopMost = false;
                    this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    this.Hide();
                    return false;
                }
            }

            this.TopMost = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Hide();

            result = GetScores();
            if (!result)
            {
                return false;
            }

            return true;
        }

        #endregion PerformCalibration

        public bool ImproveCalibration()
        {
            // Find lowest score.
            float highest = (this.LeftScores[0].score + this.RightScores[0].score) / 2f;
            int highestIndex = 0;
            for (int i = 0; i < this.NumberOfTargets; i++)
            {
                float score = (this.LeftScores[i].score + this.RightScores[i].score) / 2f;
                if (score > highest)
                {
                    highest = score;
                    highestIndex = i;
                }
            }

            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Show();

            // Make sure the screen is clear of targets.
            this.drawTarget = false;
            this.calibrationPictureBox.Refresh();

            bool result = CalibrateTarget(highestIndex);

            this.TopMost = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Hide();

            if (!result)
            {
                return false;
            }

            result = GetScores();
            if (!result)
            {
                return false;
            }

            return true;
        }
    }
}
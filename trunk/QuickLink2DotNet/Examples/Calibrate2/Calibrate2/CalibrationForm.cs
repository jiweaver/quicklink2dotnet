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

/* $Id: CalibrationForm.cs 38 2011-05-09 01:07:39Z piranther $
 *
 * Description: This form provides 5, 9, or 16 point full screen calibration.
 */

#endregion Header Comments

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace Calibrate2
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

        #endregion Fields

        #region Init / Cleanup

        public CalibrationForm()
        {
            InitializeComponent();

            // Make sure we are the topmost window.
            this.TopMost = true;

            // Maximize the window.
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            // Set the form size so that it covers the full screen.
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.calibrationPictureBox.ClientSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            this.Show();
        }

        #endregion Init / Cleanup

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

        #region Perform Calibration

        /// <summary>
        /// Perform the calibration.  Returns the calibrationID if calibration
        /// was successful.  Throws exception if it fails.
        /// </summary>
        public int PerformCalibration(int deviceID, QLCalibrationType calibrationType, int targetTime)
        {
            QLError qlerror;

            int calibrationID;

            // Create a new calibration container.
            qlerror = QuickLink2API.QLCalibration_Create(0, out calibrationID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception("QLCalibration_Create() Error:" + qlerror.ToString("g") + ".\n");

            qlerror = QuickLink2API.QLCalibration_Initialize(deviceID, calibrationID, calibrationType);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception("QLCalibration_Initialize() Error:" + qlerror.ToString("g") + ".\n");

            int numTargets = 20;
            QLCalibrationTarget[] targets = new QLCalibrationTarget[numTargets];
            qlerror = QuickLink2API.QLCalibration_GetTargets(calibrationID, ref numTargets, targets);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception("QLCalibration_GetTargets() Error:" + qlerror.ToString("g") + ".\n");

            // Don't display a target yet.
            this.drawTarget = false;

            // Paint the background.
            this.calibrationPictureBox.Refresh();

            float leftScore = 0;
            float rightScore = 0;
            // For each target, draw it and then perform calibration.
            for (int i = 0; i < numTargets; )
            {
                // Tell the picturebox to draw the new target, and wait for it
                // to wake us.
                this.x = Convert.ToInt32(Math.Truncate(targets[i].x / 100 * this.Size.Width));
                this.y = Convert.ToInt32(Math.Truncate(targets[i].y) / 100 * this.Size.Height);
                this.drawTarget = true;
                this.calibrationPictureBox.Refresh();
                lock (this.l)
                    while (this.drawTarget)
                        Monitor.Wait(this.l);

                if (i == 0)
                    // Display the target for a while before beginning
                    // calibration to let the user get ready.
                    Thread.Sleep(500);

                // Calibrate for the target.
                qlerror = QuickLink2API.QLCalibration_Calibrate(calibrationID, targets[i].targetId, targetTime, true);
                if (qlerror != QLError.QL_ERROR_OK)
                    throw new Exception("QLCalibration_Calibrate() Error:" + qlerror.ToString("g") + ".\n");

                // Get the status of the last target.
                QLCalibrationStatus status;
                qlerror = QuickLink2API.QLCalibration_GetStatus(calibrationID, targets[i].targetId, out status);
                if (qlerror != QLError.QL_ERROR_OK)
                    throw new Exception("QLCalibration_GetStatus() Error:" + qlerror.ToString("g") + ".\n");

                if (status == QLCalibrationStatus.QL_CALIBRATION_STATUS_NO_LEFT_DATA)
                {
                    // No data for left eye.
                    if (DialogResult.Yes == MessageBox.Show("Left eye not found.  Retry?", "Left Eye Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        continue;
                    else
                        throw new Exception("User cancelled.\n");
                }
                else if (status == QLCalibrationStatus.QL_CALIBRATION_STATUS_NO_RIGHT_DATA)
                {
                    // No data for right eye.
                    if (DialogResult.Yes == MessageBox.Show("Right eye not found.  Retry?", "Right Eye Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        continue;
                    else
                        throw new Exception("User cancelled.\n");
                }
                else if (status == QLCalibrationStatus.QL_CALIBRATION_STATUS_NO_DATA)
                {
                    // Neither eye was found in time.
                    if (DialogResult.Yes == MessageBox.Show("Neither eye found.  Retry?", "Neither Eye Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        continue;
                    else
                        throw new Exception("User cancelled.\n");
                }
                else if (status != QLCalibrationStatus.QL_CALIBRATION_STATUS_OK)
                {
                    // Failed!
                    throw new Exception("Calibration failed!\n");
                }

                // Get the left score.
                QLCalibrationScore left;
                qlerror = QuickLink2API.QLCalibration_GetScoring(calibrationID, targets[i].targetId, QLEyeType.QL_EYE_TYPE_LEFT, out left);
                if (qlerror != QLError.QL_ERROR_OK)
                    throw new Exception("QLCalibration_GetScoring() Left Error:" + qlerror.ToString("g") + ".\n");
                leftScore += left.score;

                // Get the right score.
                QLCalibrationScore right;
                qlerror = QuickLink2API.QLCalibration_GetScoring(calibrationID, targets[i].targetId, QLEyeType.QL_EYE_TYPE_RIGHT, out right);
                if (qlerror != QLError.QL_ERROR_OK)
                    throw new Exception("QLCalibration_GetScoring() Right Error:" + qlerror.ToString("g") + ".\n");
                rightScore += right.score;

                // Next target please.
                i++;
            }

            leftScore /= numTargets;
            rightScore /= numTargets;

            // Prompt to apply calibration.
            if (DialogResult.No == MessageBox.Show(string.Format("Your Average Score (Lower is Better): Left={0:f} Right={1:f}.  Apply calibration?", leftScore, rightScore), "Your Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                throw new Exception("User cancelled.\n");

            this.Cursor = Cursors.WaitCursor;
            qlerror = QuickLink2API.QLCalibration_Finalize(calibrationID);
            this.Cursor = Cursors.Default;
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception("QLCalibration_Finalize() Error:" + qlerror.ToString("g") + ".\n");

            return calibrationID;
        }

        #endregion Perform Calibration
    }
}
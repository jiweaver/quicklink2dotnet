#region License

/* QuickLink2DotNet QuickStart Example: A very simple console example to
 * demonstrate initialization, calibration, and data collection from the eye
 * tracker.  The password is saved in the file
 * "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlsettings.txt" for later
 * use.  The calibration is saved in the file
 * "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlcalibration.qlc" for later
 * use.
 *
 * Copyright © 2009-2012 EyeTech Digital Systems
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

/* $Id$
 *
 * Authors: Brianna Peters <bpeters@eyetechds.com>
 * Date: May 2, 2012; 10:50 AM
 * Last modified May 24, 2012; 3:54 PM
 * Copyright © 2009-2012 EyeTech Digital Systems
 * support@eyetechds.com
 * Description: Form for auto-calibration.
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace QuickStart
{
    public partial class CalibrationForm : Form
    {
        // Tells the PictureBox paint event handler to draw the next target.
        private volatile bool drawTarget;

        // Tells the PictureBox paint event handler where to draw the next
        // target.
        private int x, y;

        // The left and right calibration scores
        private QLCalibrationScore[] leftScores;
        private QLCalibrationScore[] rightScores;

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

            this.Show();
        }

        // Draw a circle.
        private static void DrawCircle(Graphics g, int x, int y, int r, Color c)
        {
            int locX = x - r;
            int locY = y - r;
            using (Brush b = new SolidBrush(c))
                g.FillEllipse(b, locX, locY, r * 2, r * 2);
        }

        //Draw a cross
        private static void DrawCross(Graphics g, int x, int y, int r, Color c)
        {
            using (Pen p = new Pen(c, 3))
            {
                g.DrawLine(p, x - r, y - r, x + r, y + r);
                g.DrawLine(p, x + r, y - r, x - r, y + r);
            }
        }

        // Draw a calibration target.
        private static void DrawTarget(Graphics g, int x, int y)
        {
            DrawCircle(g, x, y, 38, Color.Black);
            DrawCircle(g, x, y, 14, Color.White);
            DrawCross(g, x, y, 10, Color.LimeGreen);
        }

        private void DisplayCalibration(QLCalibrationTarget[] targets, QLCalibrationScore[] leftScores, QLCalibrationScore[] rightScores,
                                        int numTargets)
        {
            int tx = 0;
            int ty = 0;

            Graphics graphics = this.CreateGraphics();

            System.Console.Write("\n");

            // Loop through each target.
            for (int i = 0; i < numTargets; i++)
            {
                // Draw the Target
                tx = Convert.ToInt32(Math.Truncate(targets[i].x / 100 * this.Size.Width));
                ty = Convert.ToInt32(Math.Truncate(targets[i].y) / 100 * this.Size.Height);
                DrawTarget(graphics, tx, ty);

                //Draw the left
                tx = Convert.ToInt32(Math.Truncate(targets[i].x + leftScores[i].x) / 100 * this.Size.Width);
                ty = Convert.ToInt32(Math.Truncate(targets[i].y + leftScores[i].y) / 100 * this.Size.Height);
                DrawCross(graphics, tx, ty, 10, Color.Blue);

                //Draw the right
                tx = Convert.ToInt32(Math.Truncate(targets[i].x + rightScores[i].x) / 100 * this.Size.Width);
                ty = Convert.ToInt32(Math.Truncate(targets[i].y + rightScores[i].y) / 100 * this.Size.Height);
                DrawCross(graphics, tx, ty, 10, Color.Red);

                System.Console.Write("{0:F} {1:F} ", leftScores[i].score, rightScores[i].score);
            }

            System.Console.Write("\n\n");

            Thread.Sleep(2500);
        }

        private void CalibrateTarget(int calibrationId, QLCalibrationTarget target)
        {
            QLCalibrationStatus status = QLCalibrationStatus.QL_CALIBRATION_STATUS_OK;

            do
            {
                // Wait for a little bit so show the blanked screen.
                Thread.Sleep(100);

                // The target positions are in percentage of the area to be tracked so
                // we need to scale to the calibration window size.
                this.x = Convert.ToInt32(Math.Truncate(target.x / 100 * this.Size.Width));
                this.y = Convert.ToInt32(Math.Truncate(target.y) / 100 * this.Size.Height);
                this.drawTarget = true;

                //Draw a target
                this.Refresh();

                //Wait a little bit so the user can see the target before we calibrate.
                Thread.Sleep(250);

                // Calibrate the target for 1500 ms. This can be done two ways; blocking and
                // non-blocking. For blocking set the block variable to true. for
                // non-blocking set it to false.
                bool block = true;
                QuickLink2API.QLCalibration_Calibrate(calibrationId, target.targetId, 1500, block);

                // Get the status of the target.
                QuickLink2API.QLCalibration_GetStatus(calibrationId, target.targetId, out status);
            } while (status != QLCalibrationStatus.QL_CALIBRATION_STATUS_OK);
        }

        private void GetScoring(int calibrationId, int numTargets, QLCalibrationTarget[] targets)
        {
            leftScores = new QLCalibrationScore[numTargets];
            rightScores = new QLCalibrationScore[numTargets];
            for (int i = 0; i < numTargets; i++)
            {
                QuickLink2API.QLCalibration_GetScoring(calibrationId, targets[i].targetId, QLEyeType.QL_EYE_TYPE_LEFT, out leftScores[i]);
                QuickLink2API.QLCalibration_GetScoring(calibrationId, targets[i].targetId, QLEyeType.QL_EYE_TYPE_RIGHT, out rightScores[i]);
            }
        }

        public void PerformCalibration(int calibrationId, int numTargets, QLCalibrationTarget[] targets)
        {
            // Don't display a target yet.
            this.drawTarget = false;

            // Paint the background.
            this.Refresh();

            for (int i = 0; i < numTargets; i++)
            {
                CalibrateTarget(calibrationId, targets[i]);
            }

            // When all calibrations targets have been successfully calibrated then get the scoring.
            // Scores can only be calculated once all targets have been calibrated.
            GetScoring(calibrationId, numTargets, targets);

            // Display the calibration results graphically.
            DisplayCalibration(targets, leftScores, rightScores, numTargets);

            this.Hide();
        }

        public void ImproveCalibration(int calibrationId, int numTargets, QLCalibrationTarget[] targets)
        {
            this.Show();

            float highestScore = 0;
            int highestIndex = 0;
            for (int i = 0; i < numTargets; i++)
            {
                if (leftScores[i].score > highestScore)
                {
                    highestScore = leftScores[i].score;
                    highestIndex = i;
                }
                if (rightScores[i].score > highestScore)
                {
                    highestScore = rightScores[i].score;
                    highestIndex = i;
                }
            }

            // Calibrate the target with the highest score.
            CalibrateTarget(calibrationId, targets[highestIndex]);

            GetScoring(calibrationId, numTargets, targets);

            // Display the calibration results graphically.
            DisplayCalibration(targets, leftScores, rightScores, numTargets);

            this.Hide();
        }

        private void CalibrationForm_Paint(object sender, PaintEventArgs e)
        {
            if (this.drawTarget)
            {
                Graphics graphics = this.CreateGraphics();

                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Paint the target.
                DrawTarget(graphics, this.x, this.y);

                this.drawTarget = false;
            }
        }
    }
}
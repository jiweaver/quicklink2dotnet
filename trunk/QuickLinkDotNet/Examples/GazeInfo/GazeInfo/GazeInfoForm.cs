#region License

/* GazeInfo: Uses QuickLinkDotNet to display info from the eye tracker.
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
using QuickLinkDotNet;

namespace GazeInfo
{
    public partial class GazeInfoForm : Form
    {
        #region Fields

        // True when the form is in the process of closing down.
        private volatile bool isClosing = false;

        // Thread that reads from the device.
        private Thread readerThread;

        #endregion Fields

        #region Init / Cleanup

        public GazeInfoForm()
        {
            InitializeComponent();

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);

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
        private delegate void UpdateReadoutCallback(ImageData iDat);
        private void UpdateReadout(ImageData iDat)
        {
            if (this.textBox_Timestamp.InvokeRequired)
            {
                UpdateReadoutCallback u = new UpdateReadoutCallback(this.UpdateReadout);
                try
                {
                    this.Invoke(u, new object[] { iDat });
                }
                catch
                {
                }
            }
            else if (!this.isClosing)
            {
                // Update the general info.
                this.textBox_Timestamp.Text = iDat.Time.ToString("F3");

                // Update the left eye.
                this.textBox_LeftEyeFound.Text = iDat.LeftEye.Found.ToString();
                if (iDat.LeftEye.Found)
                {
                    this.textBox_LeftEyeCalibrated.Text = iDat.LeftEye.Calibrated.ToString();
                    this.textBox_LeftEyePupil.Text = iDat.LeftEye.Pupil.x.ToString("F0") + "," + iDat.LeftEye.Pupil.y.ToString("F0");
                    this.textBox_LeftEyePupilDiameter.Text = iDat.LeftEye.PupilDiameter.ToString("F1") + " mm";
                    this.textBox_LeftEyeGlint1.Text = iDat.LeftEye.Glint1.x.ToString("F0") + "," + iDat.LeftEye.Glint1.y.ToString("F0");
                    this.textBox_LeftEyeGlint2.Text = iDat.LeftEye.Glint2.x.ToString("F0") + "," + iDat.LeftEye.Glint2.y.ToString("F0");
                    if (iDat.LeftEye.Calibrated)
                    {
                        this.textBox_LeftEyeGazePoint.Text = iDat.LeftEye.GazePoint.x.ToString("F0") + "," + iDat.LeftEye.GazePoint.y.ToString("F0");
                    }
                    else
                    {
                        this.textBox_LeftEyeGazePoint.Text = "--";
                    }
                }
                else
                {
                    this.textBox_LeftEyeCalibrated.Text = "--";
                    this.textBox_LeftEyePupil.Text = "--";
                    this.textBox_LeftEyePupilDiameter.Text = "-- mm";
                    this.textBox_LeftEyeGlint1.Text = "--";
                    this.textBox_LeftEyeGlint2.Text = "--";
                    this.textBox_LeftEyeGazePoint.Text = "--";
                }

                // Update the right eye.
                this.textBox_RightEyeFound.Text = iDat.RightEye.Found.ToString();
                if (iDat.RightEye.Found)
                {
                    this.textBox_RightEyeCalibrated.Text = iDat.RightEye.Calibrated.ToString();
                    this.textBox_RightEyePupil.Text = iDat.RightEye.Pupil.x.ToString("F0") + "," + iDat.RightEye.Pupil.y.ToString("F0");
                    this.textBox_RightEyePupilDiameter.Text = iDat.RightEye.PupilDiameter.ToString("F1") + " mm";
                    this.textBox_RightEyeGlint1.Text = iDat.RightEye.Glint1.x.ToString("F0") + "," + iDat.RightEye.Glint1.y.ToString("F0");
                    this.textBox_RightEyeGlint2.Text = iDat.RightEye.Glint2.x.ToString("F0") + "," + iDat.RightEye.Glint2.y.ToString("F0");
                    if (iDat.RightEye.Calibrated)
                    {
                        this.textBox_RightEyeGazePoint.Text = iDat.RightEye.GazePoint.x.ToString("F0") + "," + iDat.RightEye.GazePoint.y.ToString("F0");
                    }
                    else
                    {
                        this.textBox_RightEyeGazePoint.Text = "--";
                    }
                }
                else
                {
                    this.textBox_RightEyeCalibrated.Text = "--";
                    this.textBox_RightEyePupil.Text = "--";
                    this.textBox_RightEyePupilDiameter.Text = "-- mm";
                    this.textBox_RightEyeGlint1.Text = "--";
                    this.textBox_RightEyeGlint2.Text = "--";
                    this.textBox_RightEyeGazePoint.Text = "--";
                }
            }
        }

        #endregion Update Main Form's Log and Frame Data Display

        #region Device Reader Thread

        /* This thread code periodically reads a new frame from the device and
         * triggers an update to the form's display.
         */
        private void ReaderThreadTask()
        {
            // Delay between updates (ms).
            int delay = 1000;

            this.Display(string.Format("Reading from device.  Updating Every: {0} ms.\n", delay));

            // Load QuickLink DLL files.
            QuickLink QL;
            try
            {
                QL = new QuickLink();
                this.Display("QuickLink loaded");
            }
            catch (Exception e)
            {
                this.Display(string.Format("Failed to load QuickLink.  MSG: {0}.\n", e.Message));
                // Can't continue without QuickLink.
                return;
            }

            // Read frames from the device.
            while (!this.isClosing)
            {
                // Read a new data sample.
                ImageData iDat = new ImageData();
                bool success = QL.GetImageData(0, ref iDat);
                if (success)
                {
                    // Update the form's display.
                    UpdateReadout(iDat);

                    if (delay > 0)
                        Thread.Sleep(delay);
                }

                else
                {
                    // Probably timeout.  Just try again.
                }
            }
        }

        #endregion Device Reader Thread
    }
}
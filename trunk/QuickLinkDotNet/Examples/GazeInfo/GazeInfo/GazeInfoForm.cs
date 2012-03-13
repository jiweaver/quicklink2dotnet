#region License

/* GazeInfo: Uses QuickLinkDotNet to display info from the eye tracker.
 *
 * Copyright (c) 2011-2012 Justin Weaver
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

        private delegate void UpdateReadoutCallback(ref ImageData iDat);

        /// <summary>
        /// We need to update the form, but sometimes we need to do it from
        /// another context.  If invoke is required, then this method wraps
        /// itself in a delegate and passes it to Invoke so it can be called
        /// properly.
        /// </summary>
        private void UpdateReadout(ref ImageData iDat)
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
        private void ReaderThreadTask()
        {
            this.Display(string.Format("Reading from device.  Updating Every: {0} ms.\n", MinDelayBetweenReads));

            // Load QuickLink DLL files.
            QuickLink QL;
            try
            {
                QL = new QuickLink();
                this.Display("QuickLink loaded.  Be sure to start QuickGlance, if you have not already.\n");
            }
            catch (Exception e)
            {
                this.Display(string.Format("Failed to load QuickLink.  MSG: {0}.\n", e.Message));
                // Can't continue without QuickLink.
                return;
            }

            // Create an empty frame structure to hold the data we read.
            ImageData iDat = new ImageData();

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
                bool success = QL.GetImageData(MaxFrameWaitTime, ref iDat);
                if (success)
                {
                    // Tell the paint event handler to display the frame.
                    this.frameInUse = true;

                    // Update the form's display.
                    UpdateReadout(ref iDat);

                    if (MinDelayBetweenReads > 0)
                        Thread.Sleep(MinDelayBetweenReads);
                }
                else
                {
                    // Failed.
                }
            }
        }

        #endregion Frame Reader Thread
    }
}
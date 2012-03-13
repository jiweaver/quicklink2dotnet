#region License

/* VideoViewer2: This program displays the video image from the first eye
 * tracker on the system.
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
 * Description: This program displays the video image from the first eye
 * tracker on the system.
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace VideoViewer2
{
    public partial class MainForm : Form
    {
        #region Configuration

        /// <summary>
        /// Maximum time for the reader thread to block and wait for a new
        /// frame from the eye tracker before reporting error to the log and
        /// trying again.
        /// </summary>
        private const int MaxFrameWaitTime = 240;

        #endregion Configuration

        #region Fields

        // The files used to store the password.
        private string filename_Password = @"C:\qlsettings.txt";

        // The ID of the device we are using.  Fetched from QuickLink2.
        private int devID = -1;

        // True when the form is in the process of closing down.
        private volatile bool isClosing = false;

        // Thread that reads from the device.
        private Thread readerThread;

        // Lock used for thread synchronization (wait/pulse).
        private object l = new object();

        // Last read frame.
        private QLFrameData currentFrame;
        private Bitmap currentFrameImage;

        // Frame struct is in use.
        private bool frameInUse;

        #endregion Fields

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            this.currentFrame = new QLFrameData();
            this.currentFrameImage = null;
            this.frameInUse = false;

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);

            // Get the first device's ID.
            try
            {
                this.devID = EyeTrackerControl.GetFirstDeviceID();
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

            // Create the reader thread, start it, and wait till it's alive.
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

        #region Paint Event

        /// <summary>
        /// Checks that the Form elements are the right size to accommodate the
        /// specified image, and adjusts the Form if necessary.
        /// </summary>
        private void AdjustFormSizeForImage(Bitmap image)
        {
            // Make sure the window is set to the right size.
            int height = image.Height + this.splitContainer1.Panel1.PreferredSize.Height + this.menuStrip1.Height + this.splitContainer1.SplitterWidth + this.splitContainer1.Panel1.ClientSize.Height;
            Size clientSize = new Size(image.Width, height);
            if (this.ClientSize != clientSize)
                this.Size = this.SizeFromClientSize(clientSize);
        }

        /// <summary>
        /// Update the main Form's PictureBox.
        /// </summary>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (this.isClosing)
                return;

            if (!this.frameInUse)
                return;

            AdjustFormSizeForImage(this.currentFrameImage);

            // Draw the new image.
            Graphics g = e.Graphics;
            g.DrawImage(this.currentFrameImage, 0, 0);

            // Free the Bitmap so we do not interfere with the pixel buffer memory form QuickLink.
            this.currentFrameImage.Dispose();
            this.currentFrameImage = null;

            // Request another frame.
            this.frameInUse = false;
            lock (this.l)
                Monitor.Pulse(this.l);
        }

        #endregion Paint Event

        #region Frame Reader Thread

        /// <summary>
        /// Given a QLImageData object, this function returns a Bitmap of the
        /// image data pointed to by the PixelData field.  Don't forget to call
        /// Dispose() on this Bitmap when you are done with it.
        /// </summary>
        private Bitmap GetBitmapFromImageData(ref QLImageData iDat)
        {
            // Create a new Bitmap from the PixelData (8bpp Indexed).
            Bitmap b = new Bitmap(iDat.Width, iDat.Height, iDat.Width, PixelFormat.Format8bppIndexed, iDat.PixelData);

            // Set the palette of the image to 256 incremental shades of grey.
            ColorPalette greyScalePalette = b.Palette;
            for (int i = 0; i < 256; i++)
                greyScalePalette.Entries[i] = Color.FromArgb(255, i, i, i);
            b.Palette = greyScalePalette;

            return b;
        }

        /// <summary>
        /// This thread code starts the device, reads and frame, and then
        /// sleeps.  When the PictureBox paint event wakes it up, then it reads
        /// another frame.  When the form is closing, it stops the device and
        /// exits.
        /// </summary>
        public void ReaderThreadTask()
        {
            // Attempt to load the device password from a file.
            try
            {
                EyeTrackerControl.LoadDevicePassword(this.devID, this.filename_Password);
                this.Display("Loaded password from settings file.\n");
            }
            catch (Exception)
            {
                this.Display(string.Format("Unable to load password from file '{0}'.  Try running the Calibrate example first to generate the file.\n", this.filename_Password));
                // Can't continue without password.
                return;
            }

            // Start the device.
            try
            {
                EyeTrackerControl.StartDevice(this.devID);
                this.Display("Device has been started.\n");
            }
            catch (Exception ex)
            {
                this.Display(ex.Message + "\n");
                // Can't continue if device is not started.
                return;
            }

            this.Display(string.Format("Reading from device {0}.", this.devID));

            while (true)
            {
                // Sleep while the frame is full and we aren't shutting down.
                lock (this.l)
                    while (this.frameInUse && !this.isClosing)
                        Monitor.Wait(this.l);

                // Break if the program is closing.
                if (this.isClosing)
                    break;

                // Read a new data sample.
                QLError qlerror = QuickLink2API.QLDevice_GetFrame(this.devID, MaxFrameWaitTime, ref this.currentFrame);
                if (qlerror == QLError.QL_ERROR_OK)
                {
                    // Make a Bitmap with the pixel data from the new frame.
                    this.currentFrameImage = GetBitmapFromImageData(ref this.currentFrame.ImageData);

                    // Tell the paint event handler to display the frame.
                    this.frameInUse = true;
                    this.pictureBox1.Invalidate();
                }
                else
                {
                    // Attempting to get a frame resulted in an error!
                    this.Display(string.Format("QLDevice_GetFrame() returned {0}\n", qlerror.ToString()));
                }
            }

            // Stop the device.
            try
            {
                EyeTrackerControl.StopDevice(this.devID);
                this.Display("Stopped Device.\n");
            }
            catch (Exception ex)
            {
                this.Display(ex.Message + "\n");
            }
        }

        #endregion Frame Reader Thread
    }
}
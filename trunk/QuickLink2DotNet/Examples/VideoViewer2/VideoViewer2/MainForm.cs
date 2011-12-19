#region License

/* VideoViewer2: This program displays the video image from the first eye
 * tracker on the system.
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
 * Description: This program displays the video image from the first eye
 * tracker on the system.
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace VideoViewer2
{
    public partial class MainForm : Form
    {
        #region Fields

        /* The files used to store the password.
         */
        private string filename_PasswordFilename = @"C:\qlsettings.txt";

        // The ID of the device we are using.  Fetched from QuickLink2.
        private int devID = -1;

        // True when the form is in the process of closing down.
        private volatile bool isClosing = false;

        // Thread that reads from the device.
        private Thread readerThread;
        private Queue<QLFrameData> frameQ;
        private object l;

        #endregion Fields

        #region Init / Cleanup

        public MainForm()
        {
            InitializeComponent();

            this.frameQ = new Queue<QLFrameData>();

            this.l = new object();

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

        #region Update Main Form's Video Data and Log Displays

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

        /* Update the pictureBox.
         * */
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (this.isClosing)
                return;

            // Get a frame off the queue.
            QLFrameData frame;
            lock (this.l)
                if (this.frameQ.Count > 0)
                    frame = this.frameQ.Dequeue();
                else
                    // No frames available.
                    return;

            Bitmap b = GetBitmapFromImageData(frame.ImageData);

            int newFormWidth = frame.ImageData.Width;
            int newFormHeight = this.splitContainer1.Panel1.PreferredSize.Height + this.menuStrip1.Height + frame.ImageData.Height + this.splitContainer1.SplitterWidth + this.splitContainer1.Panel1.ClientSize.Height;
            this.Size = this.SizeFromClientSize(new Size(newFormWidth, newFormHeight));

            Graphics g = e.Graphics;
            g.DrawImage(b, 0, 0);
        }

        /* Given a QLImageData object, this function returns a Bitmap
         * of the image data pointed to by the PixelData field.
         */
        private Bitmap GetBitmapFromImageData(QLImageData iDat)
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

        #endregion Update Main Form's Video Data and Log Displays

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
                this.Display(string.Format("Unable to load password from file '{0}'.  Try running the Calibrate example first to generate the file.\n", this.filename_PasswordFilename));
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

            this.Display(string.Format("Reading from device {0}.", this.devID));

            while (!this.isClosing)
            {
                // Read a new data sample.
                QLFrameData frame = new QLFrameData();
                QLError qlerror = QuickLink2API.QLDevice_GetFrame(this.devID, 0, ref frame); // 0 = no waiting.
                if (qlerror == QLError.QL_ERROR_OK)
                {
                    /* Put the frame on the queue for painting, then invalidate
                     * the pictureBox so it will get refreshed.
                     */
                    lock (this.l)
                    {
                        this.frameQ.Enqueue(frame);
                        this.pictureBox1.Invalidate();
                    }
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
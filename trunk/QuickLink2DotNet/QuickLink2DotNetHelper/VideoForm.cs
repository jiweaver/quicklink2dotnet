#region License

/* QLHelper: A class library containing some helper methods for use with
 * QuickLink2DotNet.
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace QuickLink2DotNetHelper
{
    public partial class QLHelper
    {
        /// <summary>
        /// A Windows Form that displays a live video stream from the eye
        /// tracker device in quarter-size, half-size, or full size.  Press
        /// the space bar with the form in focus to cycle through sizes.
        /// </summary>
        public partial class VideoForm : Form
        {
            private QLHelper _helper;

            private Thread _frameReader_Thread;

            private QLFrameData _frameData;

            private object _frameData_Lock = new object();

            private bool _nextSize;
            private float _videoScale;

            private Bitmap _latestImage;

            private bool _disposed = false;

            private PictureBox _videoPictureBox;

            /// <summary>
            /// Constructor for a Windows Form that displays a live video stream from the eye tracker.
            /// This constructor allows for the specification of an initial display size.
            /// </summary>
            /// <seealso cref="T:QuickLink2DotNetHelper.QLHelper.VideoForm"/>
            /// <seealso cref="VideoForm(QLHelper)"/>
            /// <param name="helper">
            /// The <see cref="T:QuickLink2DotNetHelper.QLHelper" /> object of the device from which live video will be displayed.
            /// This object can be obtained by calling <see cref="QLHelper.ChooseDevice"/> or
            /// <see cref="QLHelper.FromDeviceId"/>.
            /// </param>
            /// <param name="sizeDivisor">
            /// Specifies the initial size of the display.  A value of 1 causes the full size to be used,
            /// 2 is half size, and 4 is quarter size.  Passing other values cause the default of '1' to
            /// be used.
            /// </param>
            public VideoForm(QLHelper helper, int sizeDivisor)
            {
                this._helper = helper;

                switch (sizeDivisor)
                {
                    case 1:
                        this._videoScale = 1f;
                        break;

                    case 2:
                        this._videoScale = 0.5f;
                        break;

                    case 4:
                        this._videoScale = 0.25f;
                        break;
                }

                this._frameData = new QLFrameData();
                this._latestImage = null;
                this._nextSize = false;

                this.ClientSize = new Size((int)(helper._deviceInfo.sensorWidth * this._videoScale), (int)(helper._deviceInfo.sensorHeight * this._videoScale));
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                this.MaximizeBox = false;

                this._videoPictureBox = new System.Windows.Forms.PictureBox();
                this._videoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
                this._videoPictureBox.Paint += new PaintEventHandler(VideoForm_VideoPictureBox_Paint);
                this.Controls.Add(this._videoPictureBox);

                this.FormClosing += new FormClosingEventHandler(VideoForm_FormClosing);
                this.KeyUp += new KeyEventHandler(VideoForm_KeyUp);
                this.Shown += new EventHandler(VideoForm_Shown);

                this._frameReader_Thread = new Thread(FrameReader);
            }

            /// <summary>
            /// Constructor for a Windows Form that displays a live video stream from the eye tracker
            /// device in quarter-size, half-size, or full size.  Press the space bar with the form in
            /// focus to cycle through sizes.  This constructor always makes a video form with an
            /// initially full-sized display.
            /// </summary>
            /// <seealso cref="VideoForm(QLHelper, int)"/>
            /// <param name="helper">
            /// The <see cref="T:QuickLink2DotNetHelper.QLHelper" /> object of the device from which live video will be displayed.
            /// This object can be obtained by calling <see cref="QLHelper.ChooseDevice"/> or
            /// <see cref="QLHelper.FromDeviceId"/>.
            /// </param>
            public VideoForm(QLHelper helper)
                : this(helper, 1)
            {
            }

            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">
            /// True if managed resources should be disposed; otherwise, false.
            /// </param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (!this._disposed))
                {
                    this._videoPictureBox.Dispose();
                    this._disposed = true;
                }
                base.Dispose(disposing);
            }

            private void VideoForm_KeyUp(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Space)
                {
                    this._nextSize = true;
                }
            }

            private void VideoForm_Shown(object sender, EventArgs e)
            {
                this._frameReader_Thread.Start();
                while (!this._frameReader_Thread.IsAlive) ;
            }

            private void VideoForm_FormClosing(object sender, FormClosingEventArgs e)
            {
                this._frameReader_Thread.Abort();
                this._frameReader_Thread.Join();
            }

            private void VideoForm_VideoPictureBox_Paint(object sender, PaintEventArgs e)
            {
                if (this._nextSize)
                {
                    if (this._videoScale == 1f)
                    {
                        this._videoScale = 0.25f;
                    }
                    else if (this._videoScale == 0.25f)
                    {
                        this._videoScale = 0.5f;
                    }
                    else if (this._videoScale == 0.5f)
                    {
                        this._videoScale = 1f;
                    }

                    this.ClientSize = new Size((int)(this._helper.DeviceInfo.sensorWidth * this._videoScale), (int)(this._helper.DeviceInfo.sensorHeight * this._videoScale));
                    this._nextSize = false;
                    this._videoPictureBox.Invalidate();
                }
                else
                {
                    if (this._latestImage == null)
                    {
                        return;
                    }

                    Graphics g = e.Graphics;

                    g.DrawImage(this._latestImage, 0, 0, this._videoPictureBox.Width, this._videoPictureBox.Height);

                    g.Flush(FlushIntention.Sync);

                    this._latestImage.Dispose();
                    this._latestImage = null;

                    lock (this._frameData_Lock)
                    {
                        Monitor.Pulse(this._frameData_Lock);
                    }
                }
            }

            private void FrameReader()
            {
                while (true)
                {
                    QLError error = QuickLink2API.QLDevice_GetFrame(this._helper.DeviceId, 2000, ref this._frameData);
                    if (error != QLError.QL_ERROR_OK)
                    {
                        Console.WriteLine("QLDevice_GetFrame() returned {0}.", error.ToString());
                        continue;
                    }

                    this._latestImage = QLHelper.BitmapFromQLImageData(ref this._frameData.ImageData);

                    this._videoPictureBox.Invalidate();

                    lock (this._frameData_Lock)
                    {
                        Monitor.Wait(this._frameData_Lock);
                    }
                }
            }
        }
    }
}
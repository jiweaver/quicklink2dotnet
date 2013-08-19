#region License

/* Gaze: Track the user's gaze, and display a red circle where they are
 * currently looking.
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
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace Gaze
{
    public partial class GazeForm
    {
        public partial class TrackerForm : Form
        {
            private int _deviceId;

            private Thread _frameReader_Thread;

            private bool _disposed = false;

            private object _frameReader_Lock = new object();

            private const long GazePointTimeout = 500;  // milliseconds.

            private Stopwatch _stopwatch;

            private Point _gazePoint = new Point(0, 0);

            private QLFrameData _frameData = new QLFrameData();

            private PictureBox _pictureBox;

            public TrackerForm(int deviceId)
            {
                this._deviceId = deviceId;

                this._pictureBox = new PictureBox();
                this._pictureBox.Dock = DockStyle.Fill;
                this._pictureBox.Paint += new PaintEventHandler(this.GazeForm_Paint);
                this.Controls.Add(this._pictureBox);

                this.ShowInTaskbar = false;
                this.BackColor = Color.Lime;
                this.TransparencyKey = Color.Lime;
                this.FormBorderStyle = FormBorderStyle.None;

                this.TopMost = true;
                this.WindowState = FormWindowState.Maximized;

                this.FormClosing += new FormClosingEventHandler(GazeForm_FormClosing);
                this.Shown += new EventHandler(GazeForm_Shown);

                this._stopwatch = new Stopwatch();

                this._frameReader_Thread = new Thread(FrameReader);
            }

            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (!this._disposed))
                {
                    this._pictureBox.Dispose();
                    this._disposed = true;
                }
                base.Dispose(disposing);
            }

            private void FrameReader()
            {
                while (true)
                {
                    QLError error = QuickLink2API.QLDevice_GetFrame(this._deviceId, 2000, ref this._frameData);
                    if (error != QLError.QL_ERROR_OK)
                    {
                        continue;
                    }

                    if (!this._frameData.WeightedGazePoint.Valid)
                    {
                        if (!this._stopwatch.IsRunning)
                        {
                            this._stopwatch.Start();
                        }
                    }
                    else
                    {
                        if (this._stopwatch.IsRunning)
                        {
                            this._stopwatch.Reset();
                        }

                        this._gazePoint.X = (int)((this._frameData.WeightedGazePoint.x / 100.0) * this.Width);
                        this._gazePoint.Y = (int)((this._frameData.WeightedGazePoint.y / 100.0) * this.Height);
                    }

                    this._pictureBox.Invalidate();

                    lock (this._frameReader_Lock)
                    {
                        Monitor.Wait(this._frameReader_Lock);
                    }
                }
            }

            private void GazeForm_Paint(object sender, PaintEventArgs e)
            {
                if (!this.TopMost)
                {
                    this.TopMost = true;
                }

                if (!this.Visible)
                {
                    this.Show();
                }

                if (this._stopwatch.ElapsedMilliseconds < GazePointTimeout)
                {
                    Graphics g = e.Graphics;
                    g.FillEllipse(Brushes.Red, this._gazePoint.X - 10, this._gazePoint.Y - 10, 20, 20);
                    g.Flush(FlushIntention.Sync);
                }

                lock (this._frameReader_Lock)
                {
                    Monitor.Pulse(this._frameReader_Lock);
                }
            }

            private void GazeForm_FormClosing(object sender, FormClosingEventArgs e)
            {
                this._frameReader_Thread.Abort();
                this._frameReader_Thread.Join();
            }

            private void GazeForm_Shown(object sender, EventArgs e)
            {
                this.Activate();

                this._frameReader_Thread.Start();
                while (!this._frameReader_Thread.IsAlive) ;
            }
        }
    }
}
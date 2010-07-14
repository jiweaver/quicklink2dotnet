/* Author: Justin Weaver (Jul 2010)
 * Description: A simple example of calling the QuickLink API.  This program 
 * periodically reads samples from the eye tracker and displays information for
 * each eye.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuickLinkAPI4NET;

namespace QuickLinkExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            timer1.Start();
        }

        /* Periodically read raw data from the eye tracker and update the 
         * display.
         */
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!QuickLink.GetQGOnFlag())
            {
                this.richTextBox1.Text = "Quick Glance is not running.\n";
            }
            else
            {
                // Read a new data sample.
                ImageData d = new ImageData();
                if (QuickLink.GetImageData(0, ref d))  //0 means: no blocking
                {
                    this.richTextBox1.Text = "Left Eye\n";
                    this.richTextBox1.Text += "\tFound:" + d.LeftEye.Found + "\n";

                    // The value of Pupil Diameter is only valid when Found is true. 
                    this.richTextBox1.Text += "\tPupil Diameter:" +
                        ((d.LeftEye.Found) ? d.LeftEye.PupilDiameter.ToString() : "???") + "\n";

                    // The value of Calibrated is only valid when Found is true. 
                    this.richTextBox1.Text += "\tCalibrated:" + ((d.LeftEye.Found) ? d.LeftEye.Calibrated.ToString() : "???") + "\n";

                    /* The value of GazePoint is only valid when Found and Calibrated
                     * are both true. 
                     */
                    this.richTextBox1.Text += "\tGaze Point:" +
                        ((d.LeftEye.Found && d.LeftEye.Calibrated) ? d.LeftEye.GazePoint.x.ToString() : "???") + "," +
                        ((d.LeftEye.Found && d.LeftEye.Calibrated) ? d.LeftEye.GazePoint.y.ToString() : "???") + "\n";

                    this.richTextBox1.Text += "\n";

                    // Same as above, except now for the right eye.
                    this.richTextBox1.Text += "Right Eye\n";
                    this.richTextBox1.Text += "\tFound:" + d.RightEye.Found + "\n";
                    this.richTextBox1.Text += "\tPupil Diameter:" +
                        ((d.RightEye.Found) ? d.RightEye.PupilDiameter.ToString() : "???") + "\n";
                    this.richTextBox1.Text += "\tCalibrated:" + ((d.RightEye.Found) ? d.RightEye.Calibrated.ToString() : "???") + "\n";
                    this.richTextBox1.Text += "\tGaze Point:" +
                        ((d.RightEye.Found && d.RightEye.Calibrated) ? d.RightEye.GazePoint.x.ToString() : "???") + "," +
                        ((d.RightEye.Found && d.RightEye.Calibrated) ? d.RightEye.GazePoint.y.ToString() : "???") + "\n";
                }
            }
        }
    }
}

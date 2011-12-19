namespace Calibrate2
{
    partial class CalibrationForm 
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.calibrationPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // calibrationPictureBox
            // 
            this.calibrationPictureBox.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.calibrationPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calibrationPictureBox.Location = new System.Drawing.Point(0, 0);
            this.calibrationPictureBox.Name = "calibrationPictureBox";
            this.calibrationPictureBox.Size = new System.Drawing.Size(292, 266);
            this.calibrationPictureBox.TabIndex = 0;
            this.calibrationPictureBox.TabStop = false;
            this.calibrationPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.calibrationPictureBox_Paint);
            // 
            // CalibrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.calibrationPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CalibrationForm";
            this.Text = "calibrateForm";
            ((System.ComponentModel.ISupportInitialize)(this.calibrationPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox calibrationPictureBox;
    }
}
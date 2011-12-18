namespace Calibrate
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.logBox = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label_TargetDuration = new System.Windows.Forms.Label();
            this.label_CalibrationType = new System.Windows.Forms.Label();
            this.label_Password = new System.Windows.Forms.Label();
            this.numericUpDown_TargetDuration = new System.Windows.Forms.NumericUpDown();
            this.comboBox_CalibrationType = new System.Windows.Forms.ComboBox();
            this.button_BeginCalibration = new System.Windows.Forms.Button();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TargetDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(522, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBox.Location = new System.Drawing.Point(0, 0);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(518, 234);
            this.logBox.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label_TargetDuration);
            this.splitContainer1.Panel1.Controls.Add(this.label_CalibrationType);
            this.splitContainer1.Panel1.Controls.Add(this.label_Password);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDown_TargetDuration);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox_CalibrationType);
            this.splitContainer1.Panel1.Controls.Add(this.button_BeginCalibration);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_Password);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.logBox);
            this.splitContainer1.Size = new System.Drawing.Size(522, 325);
            this.splitContainer1.SplitterDistance = 83;
            this.splitContainer1.TabIndex = 2;
            // 
            // label_TargetDuration
            // 
            this.label_TargetDuration.AutoSize = true;
            this.label_TargetDuration.Location = new System.Drawing.Point(207, 6);
            this.label_TargetDuration.Name = "label_TargetDuration";
            this.label_TargetDuration.Size = new System.Drawing.Size(103, 13);
            this.label_TargetDuration.TabIndex = 6;
            this.label_TargetDuration.Text = "Target Duration (ms)";
            // 
            // label_CalibrationType
            // 
            this.label_CalibrationType.AutoSize = true;
            this.label_CalibrationType.Location = new System.Drawing.Point(58, 6);
            this.label_CalibrationType.Name = "label_CalibrationType";
            this.label_CalibrationType.Size = new System.Drawing.Size(83, 13);
            this.label_CalibrationType.TabIndex = 5;
            this.label_CalibrationType.Text = "Calibration Type";
            // 
            // label_Password
            // 
            this.label_Password.AutoSize = true;
            this.label_Password.Location = new System.Drawing.Point(36, 54);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(90, 13);
            this.label_Password.TabIndex = 4;
            this.label_Password.Text = "Device Password";
            // 
            // numericUpDown_TargetDuration
            // 
            this.numericUpDown_TargetDuration.Location = new System.Drawing.Point(210, 21);
            this.numericUpDown_TargetDuration.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDown_TargetDuration.Name = "numericUpDown_TargetDuration";
            this.numericUpDown_TargetDuration.Size = new System.Drawing.Size(100, 20);
            this.numericUpDown_TargetDuration.TabIndex = 3;
            // 
            // comboBox_CalibrationType
            // 
            this.comboBox_CalibrationType.FormattingEnabled = true;
            this.comboBox_CalibrationType.Location = new System.Drawing.Point(10, 21);
            this.comboBox_CalibrationType.Name = "comboBox_CalibrationType";
            this.comboBox_CalibrationType.Size = new System.Drawing.Size(182, 21);
            this.comboBox_CalibrationType.TabIndex = 2;
            // 
            // button_BeginCalibration
            // 
            this.button_BeginCalibration.Location = new System.Drawing.Point(327, 6);
            this.button_BeginCalibration.Name = "button_BeginCalibration";
            this.button_BeginCalibration.Size = new System.Drawing.Size(182, 66);
            this.button_BeginCalibration.TabIndex = 1;
            this.button_BeginCalibration.Text = "Begin Calibration";
            this.button_BeginCalibration.UseVisualStyleBackColor = true;
            this.button_BeginCalibration.Click += new System.EventHandler(this.button_BeginCalibration_Click);
            // 
            // textBox_Password
            // 
            this.textBox_Password.Location = new System.Drawing.Point(126, 51);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(106, 20);
            this.textBox_Password.TabIndex = 0;
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 349);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Calibrate";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TargetDuration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label_TargetDuration;
        private System.Windows.Forms.Label label_CalibrationType;
        private System.Windows.Forms.Label label_Password;
        private System.Windows.Forms.NumericUpDown numericUpDown_TargetDuration;
        private System.Windows.Forms.ComboBox comboBox_CalibrationType;
        private System.Windows.Forms.Button button_BeginCalibration;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}


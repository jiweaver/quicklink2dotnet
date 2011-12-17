namespace GazeInfo
{
    partial class GazeInfoForm
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
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox_RightEye = new System.Windows.Forms.GroupBox();
            this.label_RightEyeGazePoint = new System.Windows.Forms.Label();
            this.label_RightEyeGlint2 = new System.Windows.Forms.Label();
            this.label_RightEyeGlint1 = new System.Windows.Forms.Label();
            this.label_RightEyePupilDiameter = new System.Windows.Forms.Label();
            this.label_RightEyePupil = new System.Windows.Forms.Label();
            this.label_RightEyeCalibrated = new System.Windows.Forms.Label();
            this.label_RightEyeFound = new System.Windows.Forms.Label();
            this.textBox_RightEyeGazePoint = new System.Windows.Forms.TextBox();
            this.textBox_RightEyeGlint2 = new System.Windows.Forms.TextBox();
            this.textBox_RightEyeGlint1 = new System.Windows.Forms.TextBox();
            this.textBox_RightEyePupilDiameter = new System.Windows.Forms.TextBox();
            this.textBox_RightEyePupil = new System.Windows.Forms.TextBox();
            this.textBox_RightEyeFound = new System.Windows.Forms.TextBox();
            this.textBox_RightEyeCalibrated = new System.Windows.Forms.TextBox();
            this.groupBox_General = new System.Windows.Forms.GroupBox();
            this.label_Timestamp = new System.Windows.Forms.Label();
            this.textBox_Timestamp = new System.Windows.Forms.TextBox();
            this.groupBox_LeftEye = new System.Windows.Forms.GroupBox();
            this.label_LeftEyeGazePoint = new System.Windows.Forms.Label();
            this.label_LeftEyeGlint2 = new System.Windows.Forms.Label();
            this.label_LeftEyeGlint1 = new System.Windows.Forms.Label();
            this.label_LeftEyePupilDiameter = new System.Windows.Forms.Label();
            this.label_LeftEyePupil = new System.Windows.Forms.Label();
            this.label_LeftEyeCalibrated = new System.Windows.Forms.Label();
            this.label_LeftEyeFound = new System.Windows.Forms.Label();
            this.textBox_LeftEyeGazePoint = new System.Windows.Forms.TextBox();
            this.textBox_LeftEyeGlint2 = new System.Windows.Forms.TextBox();
            this.textBox_LeftEyeGlint1 = new System.Windows.Forms.TextBox();
            this.textBox_LeftEyePupilDiameter = new System.Windows.Forms.TextBox();
            this.textBox_LeftEyePupil = new System.Windows.Forms.TextBox();
            this.textBox_LeftEyeCalibrated = new System.Windows.Forms.TextBox();
            this.textBox_LeftEyeFound = new System.Windows.Forms.TextBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox_RightEye.SuspendLayout();
            this.groupBox_General.SuspendLayout();
            this.groupBox_LeftEye.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(630, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox_RightEye);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox_General);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox_LeftEye);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.logBox);
            this.splitContainer1.Size = new System.Drawing.Size(630, 459);
            this.splitContainer1.SplitterDistance = 204;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox_RightEye
            // 
            this.groupBox_RightEye.Controls.Add(this.label_RightEyeGazePoint);
            this.groupBox_RightEye.Controls.Add(this.label_RightEyeGlint2);
            this.groupBox_RightEye.Controls.Add(this.label_RightEyeGlint1);
            this.groupBox_RightEye.Controls.Add(this.label_RightEyePupilDiameter);
            this.groupBox_RightEye.Controls.Add(this.label_RightEyePupil);
            this.groupBox_RightEye.Controls.Add(this.label_RightEyeCalibrated);
            this.groupBox_RightEye.Controls.Add(this.label_RightEyeFound);
            this.groupBox_RightEye.Controls.Add(this.textBox_RightEyeGazePoint);
            this.groupBox_RightEye.Controls.Add(this.textBox_RightEyeGlint2);
            this.groupBox_RightEye.Controls.Add(this.textBox_RightEyeGlint1);
            this.groupBox_RightEye.Controls.Add(this.textBox_RightEyePupilDiameter);
            this.groupBox_RightEye.Controls.Add(this.textBox_RightEyePupil);
            this.groupBox_RightEye.Controls.Add(this.textBox_RightEyeFound);
            this.groupBox_RightEye.Controls.Add(this.textBox_RightEyeCalibrated);
            this.groupBox_RightEye.Location = new System.Drawing.Point(419, 3);
            this.groupBox_RightEye.Name = "groupBox_RightEye";
            this.groupBox_RightEye.Size = new System.Drawing.Size(207, 199);
            this.groupBox_RightEye.TabIndex = 2;
            this.groupBox_RightEye.TabStop = false;
            this.groupBox_RightEye.Text = "Right Eye";
            // 
            // label_RightEyeGazePoint
            // 
            this.label_RightEyeGazePoint.AutoSize = true;
            this.label_RightEyeGazePoint.Location = new System.Drawing.Point(36, 178);
            this.label_RightEyeGazePoint.Name = "label_RightEyeGazePoint";
            this.label_RightEyeGazePoint.Size = new System.Drawing.Size(56, 13);
            this.label_RightEyeGazePoint.TabIndex = 14;
            this.label_RightEyeGazePoint.Text = "GazePoint";
            // 
            // label_RightEyeGlint2
            // 
            this.label_RightEyeGlint2.AutoSize = true;
            this.label_RightEyeGlint2.Location = new System.Drawing.Point(58, 150);
            this.label_RightEyeGlint2.Name = "label_RightEyeGlint2";
            this.label_RightEyeGlint2.Size = new System.Drawing.Size(34, 13);
            this.label_RightEyeGlint2.TabIndex = 13;
            this.label_RightEyeGlint2.Text = "Glint2";
            // 
            // label_RightEyeGlint1
            // 
            this.label_RightEyeGlint1.AutoSize = true;
            this.label_RightEyeGlint1.Location = new System.Drawing.Point(58, 124);
            this.label_RightEyeGlint1.Name = "label_RightEyeGlint1";
            this.label_RightEyeGlint1.Size = new System.Drawing.Size(34, 13);
            this.label_RightEyeGlint1.TabIndex = 12;
            this.label_RightEyeGlint1.Text = "Glint1";
            // 
            // label_RightEyePupilDiameter
            // 
            this.label_RightEyePupilDiameter.AutoSize = true;
            this.label_RightEyePupilDiameter.Location = new System.Drawing.Point(17, 98);
            this.label_RightEyePupilDiameter.Name = "label_RightEyePupilDiameter";
            this.label_RightEyePupilDiameter.Size = new System.Drawing.Size(75, 13);
            this.label_RightEyePupilDiameter.TabIndex = 11;
            this.label_RightEyePupilDiameter.Text = "Pupil Diameter";
            // 
            // label_RightEyePupil
            // 
            this.label_RightEyePupil.AutoSize = true;
            this.label_RightEyePupil.Location = new System.Drawing.Point(62, 73);
            this.label_RightEyePupil.Name = "label_RightEyePupil";
            this.label_RightEyePupil.Size = new System.Drawing.Size(30, 13);
            this.label_RightEyePupil.TabIndex = 10;
            this.label_RightEyePupil.Text = "Pupil";
            // 
            // label_RightEyeCalibrated
            // 
            this.label_RightEyeCalibrated.AutoSize = true;
            this.label_RightEyeCalibrated.Location = new System.Drawing.Point(38, 47);
            this.label_RightEyeCalibrated.Name = "label_RightEyeCalibrated";
            this.label_RightEyeCalibrated.Size = new System.Drawing.Size(54, 13);
            this.label_RightEyeCalibrated.TabIndex = 9;
            this.label_RightEyeCalibrated.Text = "Calibrated";
            // 
            // label_RightEyeFound
            // 
            this.label_RightEyeFound.AutoSize = true;
            this.label_RightEyeFound.Location = new System.Drawing.Point(55, 21);
            this.label_RightEyeFound.Name = "label_RightEyeFound";
            this.label_RightEyeFound.Size = new System.Drawing.Size(37, 13);
            this.label_RightEyeFound.TabIndex = 8;
            this.label_RightEyeFound.Text = "Found";
            // 
            // textBox_RightEyeGazePoint
            // 
            this.textBox_RightEyeGazePoint.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_RightEyeGazePoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_RightEyeGazePoint.Location = new System.Drawing.Point(98, 175);
            this.textBox_RightEyeGazePoint.Name = "textBox_RightEyeGazePoint";
            this.textBox_RightEyeGazePoint.ReadOnly = true;
            this.textBox_RightEyeGazePoint.Size = new System.Drawing.Size(100, 20);
            this.textBox_RightEyeGazePoint.TabIndex = 6;
            // 
            // textBox_RightEyeGlint2
            // 
            this.textBox_RightEyeGlint2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_RightEyeGlint2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_RightEyeGlint2.Location = new System.Drawing.Point(98, 148);
            this.textBox_RightEyeGlint2.Name = "textBox_RightEyeGlint2";
            this.textBox_RightEyeGlint2.ReadOnly = true;
            this.textBox_RightEyeGlint2.Size = new System.Drawing.Size(100, 20);
            this.textBox_RightEyeGlint2.TabIndex = 5;
            // 
            // textBox_RightEyeGlint1
            // 
            this.textBox_RightEyeGlint1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_RightEyeGlint1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_RightEyeGlint1.Location = new System.Drawing.Point(98, 122);
            this.textBox_RightEyeGlint1.Name = "textBox_RightEyeGlint1";
            this.textBox_RightEyeGlint1.ReadOnly = true;
            this.textBox_RightEyeGlint1.Size = new System.Drawing.Size(100, 20);
            this.textBox_RightEyeGlint1.TabIndex = 4;
            // 
            // textBox_RightEyePupilDiameter
            // 
            this.textBox_RightEyePupilDiameter.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_RightEyePupilDiameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_RightEyePupilDiameter.Location = new System.Drawing.Point(98, 95);
            this.textBox_RightEyePupilDiameter.Name = "textBox_RightEyePupilDiameter";
            this.textBox_RightEyePupilDiameter.ReadOnly = true;
            this.textBox_RightEyePupilDiameter.Size = new System.Drawing.Size(100, 20);
            this.textBox_RightEyePupilDiameter.TabIndex = 3;
            // 
            // textBox_RightEyePupil
            // 
            this.textBox_RightEyePupil.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_RightEyePupil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_RightEyePupil.Location = new System.Drawing.Point(98, 70);
            this.textBox_RightEyePupil.Name = "textBox_RightEyePupil";
            this.textBox_RightEyePupil.ReadOnly = true;
            this.textBox_RightEyePupil.Size = new System.Drawing.Size(100, 20);
            this.textBox_RightEyePupil.TabIndex = 2;
            // 
            // textBox_RightEyeFound
            // 
            this.textBox_RightEyeFound.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_RightEyeFound.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_RightEyeFound.Location = new System.Drawing.Point(98, 18);
            this.textBox_RightEyeFound.Name = "textBox_RightEyeFound";
            this.textBox_RightEyeFound.ReadOnly = true;
            this.textBox_RightEyeFound.Size = new System.Drawing.Size(100, 20);
            this.textBox_RightEyeFound.TabIndex = 1;
            // 
            // textBox_RightEyeCalibrated
            // 
            this.textBox_RightEyeCalibrated.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_RightEyeCalibrated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_RightEyeCalibrated.Location = new System.Drawing.Point(98, 44);
            this.textBox_RightEyeCalibrated.Name = "textBox_RightEyeCalibrated";
            this.textBox_RightEyeCalibrated.ReadOnly = true;
            this.textBox_RightEyeCalibrated.Size = new System.Drawing.Size(100, 20);
            this.textBox_RightEyeCalibrated.TabIndex = 0;
            // 
            // groupBox_General
            // 
            this.groupBox_General.Controls.Add(this.label_Timestamp);
            this.groupBox_General.Controls.Add(this.textBox_Timestamp);
            this.groupBox_General.Location = new System.Drawing.Point(3, 3);
            this.groupBox_General.Name = "groupBox_General";
            this.groupBox_General.Size = new System.Drawing.Size(205, 199);
            this.groupBox_General.TabIndex = 0;
            this.groupBox_General.TabStop = false;
            this.groupBox_General.Text = "General";
            // 
            // label_Timestamp
            // 
            this.label_Timestamp.AutoSize = true;
            this.label_Timestamp.Location = new System.Drawing.Point(31, 21);
            this.label_Timestamp.Name = "label_Timestamp";
            this.label_Timestamp.Size = new System.Drawing.Size(58, 13);
            this.label_Timestamp.TabIndex = 4;
            this.label_Timestamp.Text = "Timestamp";
            // 
            // textBox_Timestamp
            // 
            this.textBox_Timestamp.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_Timestamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Timestamp.Location = new System.Drawing.Point(95, 18);
            this.textBox_Timestamp.Name = "textBox_Timestamp";
            this.textBox_Timestamp.ReadOnly = true;
            this.textBox_Timestamp.Size = new System.Drawing.Size(100, 20);
            this.textBox_Timestamp.TabIndex = 0;
            // 
            // groupBox_LeftEye
            // 
            this.groupBox_LeftEye.Controls.Add(this.label_LeftEyeGazePoint);
            this.groupBox_LeftEye.Controls.Add(this.label_LeftEyeGlint2);
            this.groupBox_LeftEye.Controls.Add(this.label_LeftEyeGlint1);
            this.groupBox_LeftEye.Controls.Add(this.label_LeftEyePupilDiameter);
            this.groupBox_LeftEye.Controls.Add(this.label_LeftEyePupil);
            this.groupBox_LeftEye.Controls.Add(this.label_LeftEyeCalibrated);
            this.groupBox_LeftEye.Controls.Add(this.label_LeftEyeFound);
            this.groupBox_LeftEye.Controls.Add(this.textBox_LeftEyeGazePoint);
            this.groupBox_LeftEye.Controls.Add(this.textBox_LeftEyeGlint2);
            this.groupBox_LeftEye.Controls.Add(this.textBox_LeftEyeGlint1);
            this.groupBox_LeftEye.Controls.Add(this.textBox_LeftEyePupilDiameter);
            this.groupBox_LeftEye.Controls.Add(this.textBox_LeftEyePupil);
            this.groupBox_LeftEye.Controls.Add(this.textBox_LeftEyeCalibrated);
            this.groupBox_LeftEye.Controls.Add(this.textBox_LeftEyeFound);
            this.groupBox_LeftEye.Location = new System.Drawing.Point(214, 3);
            this.groupBox_LeftEye.Name = "groupBox_LeftEye";
            this.groupBox_LeftEye.Size = new System.Drawing.Size(199, 199);
            this.groupBox_LeftEye.TabIndex = 1;
            this.groupBox_LeftEye.TabStop = false;
            this.groupBox_LeftEye.Text = "Left Eye";
            // 
            // label_LeftEyeGazePoint
            // 
            this.label_LeftEyeGazePoint.AutoSize = true;
            this.label_LeftEyeGazePoint.Location = new System.Drawing.Point(28, 178);
            this.label_LeftEyeGazePoint.Name = "label_LeftEyeGazePoint";
            this.label_LeftEyeGazePoint.Size = new System.Drawing.Size(56, 13);
            this.label_LeftEyeGazePoint.TabIndex = 14;
            this.label_LeftEyeGazePoint.Text = "GazePoint";
            // 
            // label_LeftEyeGlint2
            // 
            this.label_LeftEyeGlint2.AutoSize = true;
            this.label_LeftEyeGlint2.Location = new System.Drawing.Point(50, 151);
            this.label_LeftEyeGlint2.Name = "label_LeftEyeGlint2";
            this.label_LeftEyeGlint2.Size = new System.Drawing.Size(34, 13);
            this.label_LeftEyeGlint2.TabIndex = 13;
            this.label_LeftEyeGlint2.Text = "Glint2";
            // 
            // label_LeftEyeGlint1
            // 
            this.label_LeftEyeGlint1.AutoSize = true;
            this.label_LeftEyeGlint1.Location = new System.Drawing.Point(50, 125);
            this.label_LeftEyeGlint1.Name = "label_LeftEyeGlint1";
            this.label_LeftEyeGlint1.Size = new System.Drawing.Size(34, 13);
            this.label_LeftEyeGlint1.TabIndex = 12;
            this.label_LeftEyeGlint1.Text = "Glint1";
            // 
            // label_LeftEyePupilDiameter
            // 
            this.label_LeftEyePupilDiameter.AutoSize = true;
            this.label_LeftEyePupilDiameter.Location = new System.Drawing.Point(12, 99);
            this.label_LeftEyePupilDiameter.Name = "label_LeftEyePupilDiameter";
            this.label_LeftEyePupilDiameter.Size = new System.Drawing.Size(75, 13);
            this.label_LeftEyePupilDiameter.TabIndex = 11;
            this.label_LeftEyePupilDiameter.Text = "Pupil Diameter";
            // 
            // label_LeftEyePupil
            // 
            this.label_LeftEyePupil.AutoSize = true;
            this.label_LeftEyePupil.Location = new System.Drawing.Point(57, 73);
            this.label_LeftEyePupil.Name = "label_LeftEyePupil";
            this.label_LeftEyePupil.Size = new System.Drawing.Size(30, 13);
            this.label_LeftEyePupil.TabIndex = 10;
            this.label_LeftEyePupil.Text = "Pupil";
            // 
            // label_LeftEyeCalibrated
            // 
            this.label_LeftEyeCalibrated.AutoSize = true;
            this.label_LeftEyeCalibrated.Location = new System.Drawing.Point(33, 48);
            this.label_LeftEyeCalibrated.Name = "label_LeftEyeCalibrated";
            this.label_LeftEyeCalibrated.Size = new System.Drawing.Size(54, 13);
            this.label_LeftEyeCalibrated.TabIndex = 9;
            this.label_LeftEyeCalibrated.Text = "Calibrated";
            // 
            // label_LeftEyeFound
            // 
            this.label_LeftEyeFound.AutoSize = true;
            this.label_LeftEyeFound.Location = new System.Drawing.Point(47, 22);
            this.label_LeftEyeFound.Name = "label_LeftEyeFound";
            this.label_LeftEyeFound.Size = new System.Drawing.Size(37, 13);
            this.label_LeftEyeFound.TabIndex = 8;
            this.label_LeftEyeFound.Text = "Found";
            // 
            // textBox_LeftEyeGazePoint
            // 
            this.textBox_LeftEyeGazePoint.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_LeftEyeGazePoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_LeftEyeGazePoint.Location = new System.Drawing.Point(91, 176);
            this.textBox_LeftEyeGazePoint.Name = "textBox_LeftEyeGazePoint";
            this.textBox_LeftEyeGazePoint.ReadOnly = true;
            this.textBox_LeftEyeGazePoint.Size = new System.Drawing.Size(100, 20);
            this.textBox_LeftEyeGazePoint.TabIndex = 6;
            // 
            // textBox_LeftEyeGlint2
            // 
            this.textBox_LeftEyeGlint2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_LeftEyeGlint2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_LeftEyeGlint2.Location = new System.Drawing.Point(91, 149);
            this.textBox_LeftEyeGlint2.Name = "textBox_LeftEyeGlint2";
            this.textBox_LeftEyeGlint2.ReadOnly = true;
            this.textBox_LeftEyeGlint2.Size = new System.Drawing.Size(100, 20);
            this.textBox_LeftEyeGlint2.TabIndex = 5;
            // 
            // textBox_LeftEyeGlint1
            // 
            this.textBox_LeftEyeGlint1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_LeftEyeGlint1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_LeftEyeGlint1.Location = new System.Drawing.Point(91, 122);
            this.textBox_LeftEyeGlint1.Name = "textBox_LeftEyeGlint1";
            this.textBox_LeftEyeGlint1.ReadOnly = true;
            this.textBox_LeftEyeGlint1.Size = new System.Drawing.Size(100, 20);
            this.textBox_LeftEyeGlint1.TabIndex = 4;
            // 
            // textBox_LeftEyePupilDiameter
            // 
            this.textBox_LeftEyePupilDiameter.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_LeftEyePupilDiameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_LeftEyePupilDiameter.Location = new System.Drawing.Point(91, 96);
            this.textBox_LeftEyePupilDiameter.Name = "textBox_LeftEyePupilDiameter";
            this.textBox_LeftEyePupilDiameter.ReadOnly = true;
            this.textBox_LeftEyePupilDiameter.Size = new System.Drawing.Size(100, 20);
            this.textBox_LeftEyePupilDiameter.TabIndex = 3;
            // 
            // textBox_LeftEyePupil
            // 
            this.textBox_LeftEyePupil.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_LeftEyePupil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_LeftEyePupil.Location = new System.Drawing.Point(91, 71);
            this.textBox_LeftEyePupil.Name = "textBox_LeftEyePupil";
            this.textBox_LeftEyePupil.ReadOnly = true;
            this.textBox_LeftEyePupil.Size = new System.Drawing.Size(100, 20);
            this.textBox_LeftEyePupil.TabIndex = 2;
            // 
            // textBox_LeftEyeCalibrated
            // 
            this.textBox_LeftEyeCalibrated.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_LeftEyeCalibrated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_LeftEyeCalibrated.Location = new System.Drawing.Point(91, 46);
            this.textBox_LeftEyeCalibrated.Name = "textBox_LeftEyeCalibrated";
            this.textBox_LeftEyeCalibrated.ReadOnly = true;
            this.textBox_LeftEyeCalibrated.Size = new System.Drawing.Size(100, 20);
            this.textBox_LeftEyeCalibrated.TabIndex = 1;
            // 
            // textBox_LeftEyeFound
            // 
            this.textBox_LeftEyeFound.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox_LeftEyeFound.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_LeftEyeFound.Location = new System.Drawing.Point(91, 19);
            this.textBox_LeftEyeFound.Name = "textBox_LeftEyeFound";
            this.textBox_LeftEyeFound.ReadOnly = true;
            this.textBox_LeftEyeFound.Size = new System.Drawing.Size(100, 20);
            this.textBox_LeftEyeFound.TabIndex = 0;
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBox.Location = new System.Drawing.Point(0, 0);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logBox.Size = new System.Drawing.Size(630, 251);
            this.logBox.TabIndex = 4;
            this.logBox.WordWrap = false;
            // 
            // GazeInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 483);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "GazeInfoForm";
            this.Text = "GazeInfo";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox_RightEye.ResumeLayout(false);
            this.groupBox_RightEye.PerformLayout();
            this.groupBox_General.ResumeLayout(false);
            this.groupBox_General.PerformLayout();
            this.groupBox_LeftEye.ResumeLayout(false);
            this.groupBox_LeftEye.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox_RightEye;
        private System.Windows.Forms.TextBox textBox_RightEyeGazePoint;
        private System.Windows.Forms.TextBox textBox_RightEyeGlint1;
        private System.Windows.Forms.TextBox textBox_RightEyePupilDiameter;
        private System.Windows.Forms.TextBox textBox_RightEyePupil;
        private System.Windows.Forms.TextBox textBox_RightEyeFound;
        private System.Windows.Forms.TextBox textBox_RightEyeCalibrated;
        private System.Windows.Forms.GroupBox groupBox_LeftEye;
        private System.Windows.Forms.TextBox textBox_LeftEyeGazePoint;
        private System.Windows.Forms.TextBox textBox_LeftEyeGlint2;
        private System.Windows.Forms.TextBox textBox_LeftEyePupilDiameter;
        private System.Windows.Forms.TextBox textBox_LeftEyePupil;
        private System.Windows.Forms.TextBox textBox_LeftEyeCalibrated;
        private System.Windows.Forms.TextBox textBox_LeftEyeFound;
        private System.Windows.Forms.GroupBox groupBox_General;
        private System.Windows.Forms.TextBox textBox_Timestamp;
        private System.Windows.Forms.Label label_Timestamp;
        private System.Windows.Forms.Label label_LeftEyeGazePoint;
        private System.Windows.Forms.Label label_LeftEyeGlint2;
        private System.Windows.Forms.Label label_LeftEyeGlint1;
        private System.Windows.Forms.Label label_LeftEyePupilDiameter;
        private System.Windows.Forms.Label label_LeftEyePupil;
        private System.Windows.Forms.Label label_LeftEyeCalibrated;
        private System.Windows.Forms.Label label_LeftEyeFound;
        private System.Windows.Forms.Label label_RightEyeGazePoint;
        private System.Windows.Forms.Label label_RightEyeGlint2;
        private System.Windows.Forms.Label label_RightEyeGlint1;
        private System.Windows.Forms.Label label_RightEyePupilDiameter;
        private System.Windows.Forms.Label label_RightEyePupil;
        private System.Windows.Forms.Label label_RightEyeCalibrated;
        private System.Windows.Forms.Label label_RightEyeFound;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.TextBox textBox_RightEyeGlint2;
        private System.Windows.Forms.TextBox textBox_LeftEyeGlint1;
    }
}


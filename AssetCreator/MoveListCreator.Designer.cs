namespace AssetCreator
{
    partial class MoveListCreator
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
            this.moveListBox = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMoveListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateMoveListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateMoveListToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.generateAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addMoveBtn = new System.Windows.Forms.Button();
            this.deleteMoveBtn = new System.Windows.Forms.Button();
            this.addMoveAnimBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.targetComboBox = new System.Windows.Forms.ComboBox();
            this.methodComboBox = new System.Windows.Forms.ComboBox();
            this.inflictionComboBox = new System.Windows.Forms.ComboBox();
            this.effectComboBox = new System.Windows.Forms.ComboBox();
            this.movePowerNumeric = new System.Windows.Forms.NumericUpDown();
            this.moveCostNumeric = new System.Windows.Forms.NumericUpDown();
            this.inflictionChanceNumeric = new System.Windows.Forms.NumericUpDown();
            this.moveAnimPicBox = new System.Windows.Forms.PictureBox();
            this.moveAnimDelayNum = new System.Windows.Forms.NumericUpDown();
            this.moveAnimFrmWdthNum = new System.Windows.Forms.NumericUpDown();
            this.moveAnimFrmHgtNum = new System.Windows.Forms.NumericUpDown();
            this.moveAnimOTRChkBox = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.movePowerNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveCostNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inflictionChanceNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveAnimPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveAnimDelayNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveAnimFrmWdthNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveAnimFrmHgtNum)).BeginInit();
            this.SuspendLayout();
            // 
            // moveListBox
            // 
            this.moveListBox.FormattingEnabled = true;
            this.moveListBox.Location = new System.Drawing.Point(12, 27);
            this.moveListBox.Name = "moveListBox";
            this.moveListBox.Size = new System.Drawing.Size(140, 381);
            this.moveListBox.TabIndex = 0;
            this.moveListBox.SelectedIndexChanged += new System.EventHandler(this.moveListBox_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(586, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadMoveListToolStripMenuItem,
            this.generateMoveListToolStripMenuItem,
            this.generateMoveListToolStripMenuItem1,
            this.generateAllToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadMoveListToolStripMenuItem
            // 
            this.loadMoveListToolStripMenuItem.Name = "loadMoveListToolStripMenuItem";
            this.loadMoveListToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.loadMoveListToolStripMenuItem.Text = "Load Move List...";
            this.loadMoveListToolStripMenuItem.ToolTipText = "Loads a premade move list";
            this.loadMoveListToolStripMenuItem.Click += new System.EventHandler(this.loadMoveListToolStripMenuItem_Click);
            // 
            // generateMoveListToolStripMenuItem
            // 
            this.generateMoveListToolStripMenuItem.Name = "generateMoveListToolStripMenuItem";
            this.generateMoveListToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.generateMoveListToolStripMenuItem.Text = "Generate Move XML...";
            this.generateMoveListToolStripMenuItem.ToolTipText = "Generate each move\'s XML representation";
            this.generateMoveListToolStripMenuItem.Click += new System.EventHandler(this.generateMoveXmlToolStripMenuItem_Click);
            // 
            // generateMoveListToolStripMenuItem1
            // 
            this.generateMoveListToolStripMenuItem1.Name = "generateMoveListToolStripMenuItem1";
            this.generateMoveListToolStripMenuItem1.Size = new System.Drawing.Size(190, 22);
            this.generateMoveListToolStripMenuItem1.Text = "Generate Move List...";
            this.generateMoveListToolStripMenuItem1.ToolTipText = "Generate only the list of move names";
            this.generateMoveListToolStripMenuItem1.Click += new System.EventHandler(this.generateMoveListToolStripMenuItem_Click);
            // 
            // generateAllToolStripMenuItem
            // 
            this.generateAllToolStripMenuItem.Name = "generateAllToolStripMenuItem";
            this.generateAllToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.generateAllToolStripMenuItem.Text = "Generate All...";
            this.generateAllToolStripMenuItem.ToolTipText = "Generate move list and each move\'s XML representation";
            this.generateAllToolStripMenuItem.Click += new System.EventHandler(this.generateAllToolStripMenuItem_Click);
            // 
            // addMoveBtn
            // 
            this.addMoveBtn.Location = new System.Drawing.Point(11, 417);
            this.addMoveBtn.Name = "addMoveBtn";
            this.addMoveBtn.Size = new System.Drawing.Size(140, 23);
            this.addMoveBtn.TabIndex = 3;
            this.addMoveBtn.Text = "Add New Move";
            this.addMoveBtn.UseVisualStyleBackColor = true;
            this.addMoveBtn.Click += new System.EventHandler(this.addMoveBtn_Click);
            // 
            // deleteMoveBtn
            // 
            this.deleteMoveBtn.Location = new System.Drawing.Point(12, 446);
            this.deleteMoveBtn.Name = "deleteMoveBtn";
            this.deleteMoveBtn.Size = new System.Drawing.Size(139, 23);
            this.deleteMoveBtn.TabIndex = 4;
            this.deleteMoveBtn.Text = "Delete Move";
            this.deleteMoveBtn.UseVisualStyleBackColor = true;
            this.deleteMoveBtn.Click += new System.EventHandler(this.deleteMoveBtn_Click);
            // 
            // addMoveAnimBtn
            // 
            this.addMoveAnimBtn.Location = new System.Drawing.Point(3, 282);
            this.addMoveAnimBtn.Name = "addMoveAnimBtn";
            this.tableLayoutPanel1.SetRowSpan(this.addMoveAnimBtn, 2);
            this.addMoveAnimBtn.Size = new System.Drawing.Size(98, 58);
            this.addMoveAnimBtn.TabIndex = 6;
            this.addMoveAnimBtn.Text = "Add Spritesheet";
            this.addMoveAnimBtn.UseVisualStyleBackColor = true;
            this.addMoveAnimBtn.Click += new System.EventHandler(this.addMoveAnimBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 256);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Animation:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Effect";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Method";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Target";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.targetComboBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.methodComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.inflictionComboBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.effectComboBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.movePowerNumeric, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.moveCostNumeric, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.inflictionChanceNumeric, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.addMoveAnimBtn, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.moveAnimPicBox, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.moveAnimDelayNum, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.moveAnimFrmWdthNum, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.moveAnimFrmHgtNum, 3, 12);
            this.tableLayoutPanel1.Controls.Add(this.moveAnimOTRChkBox, 3, 13);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 12);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 13);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(158, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.161373F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.161373F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.161373F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.161373F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.161373F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.161373F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.161373F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.846402F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.217109F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.161373F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.466063F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.561086F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.161373F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.161373F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 442);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Name:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Power:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Status Infliction:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Cost:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 217);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Infliction Chance:";
            // 
            // nameTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.nameTextBox, 2);
            this.nameTextBox.Location = new System.Drawing.Point(107, 3);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(202, 20);
            this.nameTextBox.TabIndex = 12;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // targetComboBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.targetComboBox, 2);
            this.targetComboBox.FormattingEnabled = true;
            this.targetComboBox.Location = new System.Drawing.Point(107, 34);
            this.targetComboBox.Name = "targetComboBox";
            this.targetComboBox.Size = new System.Drawing.Size(202, 21);
            this.targetComboBox.TabIndex = 13;
            this.targetComboBox.SelectedIndexChanged += new System.EventHandler(this.targetComboBox_SelectedIndexChanged);
            // 
            // methodComboBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.methodComboBox, 2);
            this.methodComboBox.FormattingEnabled = true;
            this.methodComboBox.Location = new System.Drawing.Point(107, 65);
            this.methodComboBox.Name = "methodComboBox";
            this.methodComboBox.Size = new System.Drawing.Size(202, 21);
            this.methodComboBox.TabIndex = 14;
            this.methodComboBox.SelectedIndexChanged += new System.EventHandler(this.methodComboBox_SelectedIndexChanged);
            // 
            // inflictionComboBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.inflictionComboBox, 2);
            this.inflictionComboBox.FormattingEnabled = true;
            this.inflictionComboBox.Location = new System.Drawing.Point(107, 96);
            this.inflictionComboBox.Name = "inflictionComboBox";
            this.inflictionComboBox.Size = new System.Drawing.Size(202, 21);
            this.inflictionComboBox.TabIndex = 15;
            this.inflictionComboBox.SelectedIndexChanged += new System.EventHandler(this.inflictionComboBox_SelectedIndexChanged);
            // 
            // effectComboBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.effectComboBox, 2);
            this.effectComboBox.FormattingEnabled = true;
            this.effectComboBox.Location = new System.Drawing.Point(107, 127);
            this.effectComboBox.Name = "effectComboBox";
            this.effectComboBox.Size = new System.Drawing.Size(202, 21);
            this.effectComboBox.TabIndex = 16;
            this.effectComboBox.SelectedIndexChanged += new System.EventHandler(this.effectComboBox_SelectedIndexChanged);
            // 
            // movePowerNumeric
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.movePowerNumeric, 2);
            this.movePowerNumeric.Location = new System.Drawing.Point(107, 158);
            this.movePowerNumeric.Name = "movePowerNumeric";
            this.movePowerNumeric.Size = new System.Drawing.Size(202, 20);
            this.movePowerNumeric.TabIndex = 17;
            this.movePowerNumeric.ValueChanged += new System.EventHandler(this.movePowerNumeric_ValueChanged);
            // 
            // moveCostNumeric
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.moveCostNumeric, 2);
            this.moveCostNumeric.Location = new System.Drawing.Point(107, 189);
            this.moveCostNumeric.Name = "moveCostNumeric";
            this.moveCostNumeric.Size = new System.Drawing.Size(202, 20);
            this.moveCostNumeric.TabIndex = 18;
            this.moveCostNumeric.ValueChanged += new System.EventHandler(this.moveCostNumeric_ValueChanged);
            // 
            // inflictionChanceNumeric
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.inflictionChanceNumeric, 2);
            this.inflictionChanceNumeric.Location = new System.Drawing.Point(107, 220);
            this.inflictionChanceNumeric.Name = "inflictionChanceNumeric";
            this.inflictionChanceNumeric.Size = new System.Drawing.Size(202, 20);
            this.inflictionChanceNumeric.TabIndex = 19;
            this.inflictionChanceNumeric.ValueChanged += new System.EventHandler(this.inflictionChanceNumeric_ValueChanged);
            // 
            // moveAnimPicBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.moveAnimPicBox, 3);
            this.moveAnimPicBox.Location = new System.Drawing.Point(107, 282);
            this.moveAnimPicBox.Name = "moveAnimPicBox";
            this.tableLayoutPanel1.SetRowSpan(this.moveAnimPicBox, 3);
            this.moveAnimPicBox.Size = new System.Drawing.Size(306, 87);
            this.moveAnimPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.moveAnimPicBox.TabIndex = 20;
            this.moveAnimPicBox.TabStop = false;
            // 
            // moveAnimDelayNum
            // 
            this.moveAnimDelayNum.DecimalPlaces = 2;
            this.moveAnimDelayNum.Location = new System.Drawing.Point(107, 375);
            this.moveAnimDelayNum.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.moveAnimDelayNum.Name = "moveAnimDelayNum";
            this.moveAnimDelayNum.Size = new System.Drawing.Size(98, 20);
            this.moveAnimDelayNum.TabIndex = 21;
            this.moveAnimDelayNum.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.moveAnimDelayNum.ValueChanged += new System.EventHandler(this.moveAnimDelayNum_ValueChanged);
            // 
            // moveAnimFrmWdthNum
            // 
            this.moveAnimFrmWdthNum.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.moveAnimFrmWdthNum.Location = new System.Drawing.Point(107, 406);
            this.moveAnimFrmWdthNum.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.moveAnimFrmWdthNum.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.moveAnimFrmWdthNum.Name = "moveAnimFrmWdthNum";
            this.moveAnimFrmWdthNum.Size = new System.Drawing.Size(98, 20);
            this.moveAnimFrmWdthNum.TabIndex = 22;
            this.moveAnimFrmWdthNum.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.moveAnimFrmWdthNum.ValueChanged += new System.EventHandler(this.moveAnimFrmWdthNum_ValueChanged);
            // 
            // moveAnimFrmHgtNum
            // 
            this.moveAnimFrmHgtNum.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.moveAnimFrmHgtNum.Location = new System.Drawing.Point(315, 375);
            this.moveAnimFrmHgtNum.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.moveAnimFrmHgtNum.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.moveAnimFrmHgtNum.Name = "moveAnimFrmHgtNum";
            this.moveAnimFrmHgtNum.Size = new System.Drawing.Size(98, 20);
            this.moveAnimFrmHgtNum.TabIndex = 23;
            this.moveAnimFrmHgtNum.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.moveAnimFrmHgtNum.ValueChanged += new System.EventHandler(this.moveAnimFrmHgtNum_ValueChanged);
            // 
            // moveAnimOTRChkBox
            // 
            this.moveAnimOTRChkBox.AutoSize = true;
            this.moveAnimOTRChkBox.Location = new System.Drawing.Point(315, 406);
            this.moveAnimOTRChkBox.Name = "moveAnimOTRChkBox";
            this.moveAnimOTRChkBox.Size = new System.Drawing.Size(75, 30);
            this.moveAnimOTRChkBox.TabIndex = 24;
            this.moveAnimOTRChkBox.Text = "One Time \r\nRun?";
            this.moveAnimOTRChkBox.UseVisualStyleBackColor = true;
            this.moveAnimOTRChkBox.CheckedChanged += new System.EventHandler(this.moveAnimOTRChkBox_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(211, 372);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Frame Height:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 372);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Delay:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 403);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Frame Width:";
            // 
            // MoveListCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 481);
            this.Controls.Add(this.deleteMoveBtn);
            this.Controls.Add(this.addMoveBtn);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.moveListBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MoveListCreator";
            this.Text = "Move List Creator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.movePowerNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveCostNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inflictionChanceNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveAnimPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveAnimDelayNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveAnimFrmWdthNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveAnimFrmHgtNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox moveListBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Button addMoveBtn;
        private System.Windows.Forms.Button deleteMoveBtn;
        private System.Windows.Forms.Button addMoveAnimBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ComboBox targetComboBox;
        private System.Windows.Forms.ComboBox methodComboBox;
        private System.Windows.Forms.ComboBox effectComboBox;
        private System.Windows.Forms.NumericUpDown movePowerNumeric;
        private System.Windows.Forms.NumericUpDown moveCostNumeric;
        private System.Windows.Forms.NumericUpDown inflictionChanceNumeric;
        private System.Windows.Forms.PictureBox moveAnimPicBox;
        private System.Windows.Forms.NumericUpDown moveAnimDelayNum;
        private System.Windows.Forms.NumericUpDown moveAnimFrmWdthNum;
        private System.Windows.Forms.NumericUpDown moveAnimFrmHgtNum;
        private System.Windows.Forms.CheckBox moveAnimOTRChkBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripMenuItem loadMoveListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateMoveListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateMoveListToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem generateAllToolStripMenuItem;
        private System.Windows.Forms.ComboBox inflictionComboBox;
    }
}
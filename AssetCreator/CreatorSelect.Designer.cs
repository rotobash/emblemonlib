namespace AssetCreator
{
    partial class CreatorSelect
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
            this.charCreatorBtn = new System.Windows.Forms.Button();
            this.moveListBtn = new System.Windows.Forms.Button();
            this.questCreatorBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.itemCreatorBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // charCreatorBtn
            // 
            this.charCreatorBtn.Location = new System.Drawing.Point(3, 3);
            this.charCreatorBtn.Name = "charCreatorBtn";
            this.charCreatorBtn.Size = new System.Drawing.Size(222, 119);
            this.charCreatorBtn.TabIndex = 0;
            this.charCreatorBtn.Text = "Character Creator";
            this.charCreatorBtn.UseVisualStyleBackColor = true;
            this.charCreatorBtn.Click += new System.EventHandler(this.charCreatorBtn_Click);
            // 
            // moveListBtn
            // 
            this.moveListBtn.Location = new System.Drawing.Point(3, 141);
            this.moveListBtn.Name = "moveListBtn";
            this.moveListBtn.Size = new System.Drawing.Size(222, 119);
            this.moveListBtn.TabIndex = 1;
            this.moveListBtn.Text = "Move List Creator";
            this.moveListBtn.UseVisualStyleBackColor = true;
            this.moveListBtn.Click += new System.EventHandler(this.moveListBtn_Click);
            // 
            // questCreatorBtn
            // 
            this.questCreatorBtn.Location = new System.Drawing.Point(242, 3);
            this.questCreatorBtn.Name = "questCreatorBtn";
            this.questCreatorBtn.Size = new System.Drawing.Size(222, 119);
            this.questCreatorBtn.TabIndex = 2;
            this.questCreatorBtn.Text = "Quest Creator";
            this.questCreatorBtn.UseVisualStyleBackColor = true;
            this.questCreatorBtn.Click += new System.EventHandler(this.questCreatorBtn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.charCreatorBtn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.moveListBtn, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.questCreatorBtn, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.itemCreatorBtn, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(478, 277);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // itemCreatorBtn
            // 
            this.itemCreatorBtn.Location = new System.Drawing.Point(242, 141);
            this.itemCreatorBtn.Name = "itemCreatorBtn";
            this.itemCreatorBtn.Size = new System.Drawing.Size(222, 119);
            this.itemCreatorBtn.TabIndex = 3;
            this.itemCreatorBtn.Text = "Item Creator";
            this.itemCreatorBtn.UseVisualStyleBackColor = true;
            this.itemCreatorBtn.Click += new System.EventHandler(this.itemCreatorBtn_Click);
            // 
            // CreatorSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 301);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CreatorSelect";
            this.Text = "Creator Select";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button charCreatorBtn;
        private System.Windows.Forms.Button moveListBtn;
        private System.Windows.Forms.Button questCreatorBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button itemCreatorBtn;
    }
}
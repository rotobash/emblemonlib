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
            this.SuspendLayout();
            // 
            // charCreatorBtn
            // 
            this.charCreatorBtn.Location = new System.Drawing.Point(12, 32);
            this.charCreatorBtn.Name = "charCreatorBtn";
            this.charCreatorBtn.Size = new System.Drawing.Size(222, 119);
            this.charCreatorBtn.TabIndex = 0;
            this.charCreatorBtn.Text = "Character Creator";
            this.charCreatorBtn.UseVisualStyleBackColor = true;
            this.charCreatorBtn.Click += new System.EventHandler(this.charCreatorBtn_Click);
            // 
            // moveListBtn
            // 
            this.moveListBtn.Location = new System.Drawing.Point(12, 170);
            this.moveListBtn.Name = "moveListBtn";
            this.moveListBtn.Size = new System.Drawing.Size(222, 119);
            this.moveListBtn.TabIndex = 1;
            this.moveListBtn.Text = "Move List Creator";
            this.moveListBtn.UseVisualStyleBackColor = true;
            this.moveListBtn.Click += new System.EventHandler(this.moveListBtn_Click);
            // 
            // CreatorSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 327);
            this.Controls.Add(this.moveListBtn);
            this.Controls.Add(this.charCreatorBtn);
            this.Name = "CreatorSelect";
            this.Text = "Creator Select";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button charCreatorBtn;
        private System.Windows.Forms.Button moveListBtn;
    }
}
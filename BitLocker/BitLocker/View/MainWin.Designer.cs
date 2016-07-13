namespace BitLocker.View
{
    partial class MainWin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
            this.LogRichBox = new System.Windows.Forms.RichTextBox();
            this.StartBtn = new System.Windows.Forms.Button();
            this.ClosePrograssBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // LogRichBox
            // 
            this.LogRichBox.BackColor = System.Drawing.Color.Black;
            this.LogRichBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.LogRichBox.ForeColor = System.Drawing.Color.Lime;
            this.LogRichBox.Location = new System.Drawing.Point(12, 12);
            this.LogRichBox.Name = "LogRichBox";
            this.LogRichBox.ReadOnly = true;
            this.LogRichBox.Size = new System.Drawing.Size(460, 306);
            this.LogRichBox.TabIndex = 0;
            this.LogRichBox.Text = "";
            // 
            // StartBtn
            // 
            this.StartBtn.BackColor = System.Drawing.Color.Snow;
            this.StartBtn.Location = new System.Drawing.Point(397, 324);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 25);
            this.StartBtn.TabIndex = 1;
            this.StartBtn.Text = "Lock";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.Click += new System.EventHandler(this.LockClick);
            // 
            // ClosePrograssBar
            // 
            this.ClosePrograssBar.BackColor = System.Drawing.Color.Gray;
            this.ClosePrograssBar.Location = new System.Drawing.Point(13, 324);
            this.ClosePrograssBar.Name = "ClosePrograssBar";
            this.ClosePrograssBar.Size = new System.Drawing.Size(378, 25);
            this.ClosePrograssBar.TabIndex = 2;
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.ClosePrograssBar);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.LogRichBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BitLocker";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox LogRichBox;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.ProgressBar ClosePrograssBar;
    }
}
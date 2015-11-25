namespace Manti
{
    partial class FormAbout
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
            this.buttonAboutOK = new System.Windows.Forms.Button();
            this.labelAboutVersion = new System.Windows.Forms.Label();
            this.labelAboutManti = new System.Windows.Forms.Label();
            this.linkLabelAboutSource = new System.Windows.Forms.LinkLabel();
            this.labelAboutCopy = new System.Windows.Forms.Label();
            this.labelAboutCreator = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonAboutOK
            // 
            this.buttonAboutOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAboutOK.Location = new System.Drawing.Point(297, 124);
            this.buttonAboutOK.Name = "buttonAboutOK";
            this.buttonAboutOK.Size = new System.Drawing.Size(75, 25);
            this.buttonAboutOK.TabIndex = 0;
            this.buttonAboutOK.Text = "OK";
            this.buttonAboutOK.UseVisualStyleBackColor = true;
            this.buttonAboutOK.Click += new System.EventHandler(this.buttonAboutOK_Click);
            // 
            // labelAboutVersion
            // 
            this.labelAboutVersion.AutoSize = true;
            this.labelAboutVersion.BackColor = System.Drawing.SystemColors.Control;
            this.labelAboutVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelAboutVersion.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelAboutVersion.Location = new System.Drawing.Point(12, 25);
            this.labelAboutVersion.Name = "labelAboutVersion";
            this.labelAboutVersion.Size = new System.Drawing.Size(84, 16);
            this.labelAboutVersion.TabIndex = 1;
            this.labelAboutVersion.Text = "Version 1.0.0";
            // 
            // labelAboutManti
            // 
            this.labelAboutManti.AutoSize = true;
            this.labelAboutManti.BackColor = System.Drawing.SystemColors.Control;
            this.labelAboutManti.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAboutManti.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelAboutManti.Location = new System.Drawing.Point(12, 9);
            this.labelAboutManti.Name = "labelAboutManti";
            this.labelAboutManti.Size = new System.Drawing.Size(114, 16);
            this.labelAboutManti.TabIndex = 2;
            this.labelAboutManti.Text = "Manti : TrinityCore";
            // 
            // linkLabelAboutSource
            // 
            this.linkLabelAboutSource.AutoSize = true;
            this.linkLabelAboutSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.linkLabelAboutSource.Location = new System.Drawing.Point(15, 130);
            this.linkLabelAboutSource.Name = "linkLabelAboutSource";
            this.linkLabelAboutSource.Size = new System.Drawing.Size(151, 15);
            this.linkLabelAboutSource.TabIndex = 3;
            this.linkLabelAboutSource.TabStop = true;
            this.linkLabelAboutSource.Text = "PROJECT MANTI -> GITHUB";
            this.linkLabelAboutSource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAboutSource_LinkClicked);
            // 
            // labelAboutCopy
            // 
            this.labelAboutCopy.AutoSize = true;
            this.labelAboutCopy.BackColor = System.Drawing.SystemColors.Control;
            this.labelAboutCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelAboutCopy.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelAboutCopy.Location = new System.Drawing.Point(12, 41);
            this.labelAboutCopy.Name = "labelAboutCopy";
            this.labelAboutCopy.Size = new System.Drawing.Size(160, 16);
            this.labelAboutCopy.TabIndex = 1;
            this.labelAboutCopy.Text = "© 2015 All rights reserved.";
            // 
            // labelAboutCreator
            // 
            this.labelAboutCreator.AutoSize = true;
            this.labelAboutCreator.BackColor = System.Drawing.SystemColors.Control;
            this.labelAboutCreator.Font = new System.Drawing.Font("Adobe Garamond Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAboutCreator.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelAboutCreator.Location = new System.Drawing.Point(27, 110);
            this.labelAboutCreator.Name = "labelAboutCreator";
            this.labelAboutCreator.Size = new System.Drawing.Size(127, 20);
            this.labelAboutCreator.TabIndex = 4;
            this.labelAboutCreator.Text = "Developer: HeiTX.";
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.labelAboutCreator);
            this.Controls.Add(this.linkLabelAboutSource);
            this.Controls.Add(this.labelAboutManti);
            this.Controls.Add(this.labelAboutCopy);
            this.Controls.Add(this.labelAboutVersion);
            this.Controls.Add(this.buttonAboutOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAboutOK;
        private System.Windows.Forms.Label labelAboutVersion;
        private System.Windows.Forms.Label labelAboutManti;
        private System.Windows.Forms.LinkLabel linkLabelAboutSource;
        private System.Windows.Forms.Label labelAboutCopy;
        private System.Windows.Forms.Label labelAboutCreator;
    }
}
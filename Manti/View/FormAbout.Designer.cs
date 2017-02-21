namespace Manti.Views {
	partial class FormAbout {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.buttonAboutOK = new System.Windows.Forms.Button();
			this.labelAboutVersion = new System.Windows.Forms.Label();
			this.labelAboutManti = new System.Windows.Forms.Label();
			this.linkLabelAboutSource = new System.Windows.Forms.LinkLabel();
			this.labelAboutCopy = new System.Windows.Forms.Label();
			this.labelAboutCreator = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			(( System.ComponentModel.ISupportInitialize )(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonAboutOK
			// 
			this.buttonAboutOK.Anchor = (( System.Windows.Forms.AnchorStyles )((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAboutOK.Location = new System.Drawing.Point(295, 236);
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
			this.labelAboutVersion.Location = new System.Drawing.Point(9, 131);
			this.labelAboutVersion.Name = "labelAboutVersion";
			this.labelAboutVersion.Size = new System.Drawing.Size(92, 13);
			this.labelAboutVersion.TabIndex = 1;
			this.labelAboutVersion.Text = "Version: 0.4-alpha";
			// 
			// labelAboutManti
			// 
			this.labelAboutManti.AutoSize = true;
			this.labelAboutManti.BackColor = System.Drawing.SystemColors.Control;
			this.labelAboutManti.Location = new System.Drawing.Point(9, 115);
			this.labelAboutManti.Name = "labelAboutManti";
			this.labelAboutManti.Size = new System.Drawing.Size(318, 13);
			this.labelAboutManti.TabIndex = 2;
			this.labelAboutManti.Text = "Manti (Manager) is a database editor, specifically for Trinity 3.3.5a.\r\n";
			// 
			// linkLabelAboutSource
			// 
			this.linkLabelAboutSource.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.linkLabelAboutSource.AutoSize = true;
			this.linkLabelAboutSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.linkLabelAboutSource.Location = new System.Drawing.Point(15, 242);
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
			this.labelAboutCopy.Location = new System.Drawing.Point(9, 147);
			this.labelAboutCopy.Name = "labelAboutCopy";
			this.labelAboutCopy.Size = new System.Drawing.Size(371, 52);
			this.labelAboutCopy.TabIndex = 1;
			this.labelAboutCopy.Text = "© 2015 All rights reserved.\r\n\r\nThe contributors are not responsible for any error" +
	"s or corruption in your server.\r\nThis program is used at own risk and the creato" +
	"r takes no responsibility.";
			// 
			// labelAboutCreator
			// 
			this.labelAboutCreator.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.labelAboutCreator.AutoSize = true;
			this.labelAboutCreator.BackColor = System.Drawing.SystemColors.Control;
			this.labelAboutCreator.Location = new System.Drawing.Point(12, 225);
			this.labelAboutCreator.Margin = new System.Windows.Forms.Padding(4);
			this.labelAboutCreator.Name = "labelAboutCreator";
			this.labelAboutCreator.Size = new System.Drawing.Size(99, 13);
			this.labelAboutCreator.TabIndex = 4;
			this.labelAboutCreator.Text = "Contributor(s): Heitx";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::Manti.Properties.Resources.titleManti;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(355, 100);
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// FormAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(382, 273);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.labelAboutCreator);
			this.Controls.Add(this.linkLabelAboutSource);
			this.Controls.Add(this.labelAboutManti);
			this.Controls.Add(this.labelAboutCopy);
			this.Controls.Add(this.labelAboutVersion);
			this.Controls.Add(this.buttonAboutOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FormAbout";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			(( System.ComponentModel.ISupportInitialize )(this.pictureBox1)).EndInit();
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
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}
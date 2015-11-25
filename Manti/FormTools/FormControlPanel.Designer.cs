namespace Manti.FormTools
{
    partial class FormControlPanel
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
            this.buttonWorldServer = new System.Windows.Forms.Button();
            this.groupBoxWorldServer = new System.Windows.Forms.GroupBox();
            this.labelWorldStatus = new System.Windows.Forms.Label();
            this.buttonWorldPath = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelAuthStatus = new System.Windows.Forms.Label();
            this.buttonAuthPath = new System.Windows.Forms.Button();
            this.buttonAuthServer = new System.Windows.Forms.Button();
            this.textBoxPathServer = new System.Windows.Forms.TextBox();
            this.checkBoxHideWorld = new System.Windows.Forms.CheckBox();
            this.checkBoxHideAuth = new System.Windows.Forms.CheckBox();
            this.groupBoxWorldServer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonWorldServer
            // 
            this.buttonWorldServer.Location = new System.Drawing.Point(6, 19);
            this.buttonWorldServer.Name = "buttonWorldServer";
            this.buttonWorldServer.Size = new System.Drawing.Size(80, 25);
            this.buttonWorldServer.TabIndex = 0;
            this.buttonWorldServer.Text = "World";
            this.buttonWorldServer.UseVisualStyleBackColor = true;
            this.buttonWorldServer.Click += new System.EventHandler(this.buttonWorldServer_Click);
            // 
            // groupBoxWorldServer
            // 
            this.groupBoxWorldServer.Controls.Add(this.checkBoxHideWorld);
            this.groupBoxWorldServer.Controls.Add(this.labelWorldStatus);
            this.groupBoxWorldServer.Controls.Add(this.buttonWorldPath);
            this.groupBoxWorldServer.Controls.Add(this.buttonWorldServer);
            this.groupBoxWorldServer.Location = new System.Drawing.Point(12, 12);
            this.groupBoxWorldServer.Name = "groupBoxWorldServer";
            this.groupBoxWorldServer.Size = new System.Drawing.Size(270, 73);
            this.groupBoxWorldServer.TabIndex = 1;
            this.groupBoxWorldServer.TabStop = false;
            this.groupBoxWorldServer.Text = "World Server";
            // 
            // labelWorldStatus
            // 
            this.labelWorldStatus.AutoSize = true;
            this.labelWorldStatus.Location = new System.Drawing.Point(178, 25);
            this.labelWorldStatus.Name = "labelWorldStatus";
            this.labelWorldStatus.Size = new System.Drawing.Size(87, 13);
            this.labelWorldStatus.TabIndex = 1;
            this.labelWorldStatus.Text = "Status: OFFLINE";
            // 
            // buttonWorldPath
            // 
            this.buttonWorldPath.Location = new System.Drawing.Point(92, 19);
            this.buttonWorldPath.Name = "buttonWorldPath";
            this.buttonWorldPath.Size = new System.Drawing.Size(80, 25);
            this.buttonWorldPath.TabIndex = 0;
            this.buttonWorldPath.Text = "Path";
            this.buttonWorldPath.UseVisualStyleBackColor = true;
            this.buttonWorldPath.Click += new System.EventHandler(this.buttonWorldPath_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxHideAuth);
            this.groupBox1.Controls.Add(this.labelAuthStatus);
            this.groupBox1.Controls.Add(this.buttonAuthPath);
            this.groupBox1.Controls.Add(this.buttonAuthServer);
            this.groupBox1.Location = new System.Drawing.Point(288, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 73);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auth Server";
            // 
            // labelAuthStatus
            // 
            this.labelAuthStatus.AutoSize = true;
            this.labelAuthStatus.Location = new System.Drawing.Point(178, 25);
            this.labelAuthStatus.Name = "labelAuthStatus";
            this.labelAuthStatus.Size = new System.Drawing.Size(87, 13);
            this.labelAuthStatus.TabIndex = 1;
            this.labelAuthStatus.Text = "Status: OFFLINE";
            // 
            // buttonAuthPath
            // 
            this.buttonAuthPath.Location = new System.Drawing.Point(92, 19);
            this.buttonAuthPath.Name = "buttonAuthPath";
            this.buttonAuthPath.Size = new System.Drawing.Size(80, 25);
            this.buttonAuthPath.TabIndex = 0;
            this.buttonAuthPath.Text = "Path";
            this.buttonAuthPath.UseVisualStyleBackColor = true;
            this.buttonAuthPath.Click += new System.EventHandler(this.buttonAuthPath_Click);
            // 
            // buttonAuthServer
            // 
            this.buttonAuthServer.Location = new System.Drawing.Point(6, 19);
            this.buttonAuthServer.Name = "buttonAuthServer";
            this.buttonAuthServer.Size = new System.Drawing.Size(80, 25);
            this.buttonAuthServer.TabIndex = 0;
            this.buttonAuthServer.Text = "Auth";
            this.buttonAuthServer.UseVisualStyleBackColor = true;
            this.buttonAuthServer.Click += new System.EventHandler(this.buttonAuthServer_Click);
            // 
            // textBoxPathServer
            // 
            this.textBoxPathServer.Location = new System.Drawing.Point(12, 91);
            this.textBoxPathServer.Name = "textBoxPathServer";
            this.textBoxPathServer.Size = new System.Drawing.Size(546, 20);
            this.textBoxPathServer.TabIndex = 2;
            this.textBoxPathServer.Visible = false;
            // 
            // checkBoxHideWorld
            // 
            this.checkBoxHideWorld.AutoSize = true;
            this.checkBoxHideWorld.Location = new System.Drawing.Point(6, 50);
            this.checkBoxHideWorld.Name = "checkBoxHideWorld";
            this.checkBoxHideWorld.Size = new System.Drawing.Size(89, 17);
            this.checkBoxHideWorld.TabIndex = 2;
            this.checkBoxHideWorld.Text = "Hide Process";
            this.checkBoxHideWorld.UseVisualStyleBackColor = true;
            // 
            // checkBoxHideAuth
            // 
            this.checkBoxHideAuth.AutoSize = true;
            this.checkBoxHideAuth.Location = new System.Drawing.Point(6, 50);
            this.checkBoxHideAuth.Name = "checkBoxHideAuth";
            this.checkBoxHideAuth.Size = new System.Drawing.Size(89, 17);
            this.checkBoxHideAuth.TabIndex = 2;
            this.checkBoxHideAuth.Text = "Hide Process";
            this.checkBoxHideAuth.UseVisualStyleBackColor = true;
            // 
            // FormControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 117);
            this.Controls.Add(this.textBoxPathServer);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxWorldServer);
            this.Name = "FormControlPanel";
            this.Text = "Control Panel";
            this.groupBoxWorldServer.ResumeLayout(false);
            this.groupBoxWorldServer.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonWorldServer;
        private System.Windows.Forms.GroupBox groupBoxWorldServer;
        private System.Windows.Forms.Button buttonWorldPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAuthPath;
        private System.Windows.Forms.Button buttonAuthServer;
        private System.Windows.Forms.TextBox textBoxPathServer;
        private System.Windows.Forms.Label labelWorldStatus;
        private System.Windows.Forms.Label labelAuthStatus;
        private System.Windows.Forms.CheckBox checkBoxHideWorld;
        private System.Windows.Forms.CheckBox checkBoxHideAuth;
    }
}
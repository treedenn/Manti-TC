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
            this.components = new System.ComponentModel.Container();
            this.groupBoxWorldServer = new System.Windows.Forms.GroupBox();
            this.checkBoxRestartWorld = new System.Windows.Forms.CheckBox();
            this.checkBoxHideWorld = new System.Windows.Forms.CheckBox();
            this.labelWorldStatus = new System.Windows.Forms.Label();
            this.buttonWorldPath = new System.Windows.Forms.Button();
            this.buttonWorldServer = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxRestartAuth = new System.Windows.Forms.CheckBox();
            this.checkBoxHideAuth = new System.Windows.Forms.CheckBox();
            this.labelAuthStatus = new System.Windows.Forms.Label();
            this.buttonAuthPath = new System.Windows.Forms.Button();
            this.buttonAuthServer = new System.Windows.Forms.Button();
            this.textBoxPathServer = new System.Windows.Forms.TextBox();
            this.buttonPathDialog = new System.Windows.Forms.Button();
            this.timerCheckProcess = new System.Windows.Forms.Timer(this.components);
            this.groupBoxWorldServer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxWorldServer
            // 
            this.groupBoxWorldServer.Controls.Add(this.checkBoxRestartWorld);
            this.groupBoxWorldServer.Controls.Add(this.checkBoxHideWorld);
            this.groupBoxWorldServer.Controls.Add(this.labelWorldStatus);
            this.groupBoxWorldServer.Controls.Add(this.buttonWorldPath);
            this.groupBoxWorldServer.Controls.Add(this.buttonWorldServer);
            this.groupBoxWorldServer.Location = new System.Drawing.Point(12, 12);
            this.groupBoxWorldServer.Name = "groupBoxWorldServer";
            this.groupBoxWorldServer.Size = new System.Drawing.Size(160, 120);
            this.groupBoxWorldServer.TabIndex = 1;
            this.groupBoxWorldServer.TabStop = false;
            this.groupBoxWorldServer.Text = "World Server";
            // 
            // checkBoxRestartWorld
            // 
            this.checkBoxRestartWorld.AutoSize = true;
            this.checkBoxRestartWorld.Location = new System.Drawing.Point(12, 95);
            this.checkBoxRestartWorld.Name = "checkBoxRestartWorld";
            this.checkBoxRestartWorld.Size = new System.Drawing.Size(60, 17);
            this.checkBoxRestartWorld.TabIndex = 3;
            this.checkBoxRestartWorld.Text = "Restart";
            this.checkBoxRestartWorld.UseVisualStyleBackColor = true;
            // 
            // checkBoxHideWorld
            // 
            this.checkBoxHideWorld.AutoSize = true;
            this.checkBoxHideWorld.Location = new System.Drawing.Point(12, 72);
            this.checkBoxHideWorld.Name = "checkBoxHideWorld";
            this.checkBoxHideWorld.Size = new System.Drawing.Size(89, 17);
            this.checkBoxHideWorld.TabIndex = 2;
            this.checkBoxHideWorld.Text = "Hide Process";
            this.checkBoxHideWorld.UseVisualStyleBackColor = true;
            // 
            // labelWorldStatus
            // 
            this.labelWorldStatus.AutoSize = true;
            this.labelWorldStatus.Location = new System.Drawing.Point(69, 56);
            this.labelWorldStatus.Name = "labelWorldStatus";
            this.labelWorldStatus.Size = new System.Drawing.Size(87, 13);
            this.labelWorldStatus.TabIndex = 1;
            this.labelWorldStatus.Text = "Status: OFFLINE";
            // 
            // buttonWorldPath
            // 
            this.buttonWorldPath.Location = new System.Drawing.Point(69, 18);
            this.buttonWorldPath.Name = "buttonWorldPath";
            this.buttonWorldPath.Size = new System.Drawing.Size(80, 25);
            this.buttonWorldPath.TabIndex = 0;
            this.buttonWorldPath.Text = "Path";
            this.buttonWorldPath.UseVisualStyleBackColor = true;
            this.buttonWorldPath.Click += new System.EventHandler(this.buttonWorldPath_Click);
            // 
            // buttonWorldServer
            // 
            this.buttonWorldServer.BackgroundImage = global::Manti.Properties.Resources.iconPlayButton;
            this.buttonWorldServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonWorldServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWorldServer.Location = new System.Drawing.Point(12, 18);
            this.buttonWorldServer.Name = "buttonWorldServer";
            this.buttonWorldServer.Size = new System.Drawing.Size(51, 51);
            this.buttonWorldServer.TabIndex = 0;
            this.buttonWorldServer.UseVisualStyleBackColor = true;
            this.buttonWorldServer.Click += new System.EventHandler(this.buttonWorldServer_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxRestartAuth);
            this.groupBox1.Controls.Add(this.checkBoxHideAuth);
            this.groupBox1.Controls.Add(this.labelAuthStatus);
            this.groupBox1.Controls.Add(this.buttonAuthPath);
            this.groupBox1.Controls.Add(this.buttonAuthServer);
            this.groupBox1.Location = new System.Drawing.Point(178, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 120);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auth Server";
            // 
            // checkBoxRestartAuth
            // 
            this.checkBoxRestartAuth.AutoSize = true;
            this.checkBoxRestartAuth.Location = new System.Drawing.Point(12, 95);
            this.checkBoxRestartAuth.Name = "checkBoxRestartAuth";
            this.checkBoxRestartAuth.Size = new System.Drawing.Size(60, 17);
            this.checkBoxRestartAuth.TabIndex = 3;
            this.checkBoxRestartAuth.Text = "Restart";
            this.checkBoxRestartAuth.UseVisualStyleBackColor = true;
            // 
            // checkBoxHideAuth
            // 
            this.checkBoxHideAuth.AutoSize = true;
            this.checkBoxHideAuth.Location = new System.Drawing.Point(12, 72);
            this.checkBoxHideAuth.Name = "checkBoxHideAuth";
            this.checkBoxHideAuth.Size = new System.Drawing.Size(89, 17);
            this.checkBoxHideAuth.TabIndex = 2;
            this.checkBoxHideAuth.Text = "Hide Process";
            this.checkBoxHideAuth.UseVisualStyleBackColor = true;
            // 
            // labelAuthStatus
            // 
            this.labelAuthStatus.AutoSize = true;
            this.labelAuthStatus.Location = new System.Drawing.Point(69, 56);
            this.labelAuthStatus.Name = "labelAuthStatus";
            this.labelAuthStatus.Size = new System.Drawing.Size(87, 13);
            this.labelAuthStatus.TabIndex = 1;
            this.labelAuthStatus.Text = "Status: OFFLINE";
            // 
            // buttonAuthPath
            // 
            this.buttonAuthPath.Location = new System.Drawing.Point(69, 18);
            this.buttonAuthPath.Name = "buttonAuthPath";
            this.buttonAuthPath.Size = new System.Drawing.Size(80, 25);
            this.buttonAuthPath.TabIndex = 0;
            this.buttonAuthPath.Text = "Path";
            this.buttonAuthPath.UseVisualStyleBackColor = true;
            this.buttonAuthPath.Click += new System.EventHandler(this.buttonAuthPath_Click);
            // 
            // buttonAuthServer
            // 
            this.buttonAuthServer.BackgroundImage = global::Manti.Properties.Resources.iconPlayButton;
            this.buttonAuthServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonAuthServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAuthServer.Location = new System.Drawing.Point(12, 18);
            this.buttonAuthServer.Name = "buttonAuthServer";
            this.buttonAuthServer.Size = new System.Drawing.Size(51, 51);
            this.buttonAuthServer.TabIndex = 0;
            this.buttonAuthServer.UseVisualStyleBackColor = true;
            this.buttonAuthServer.Click += new System.EventHandler(this.buttonAuthServer_Click);
            // 
            // textBoxPathServer
            // 
            this.textBoxPathServer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPathServer.Location = new System.Drawing.Point(12, 142);
            this.textBoxPathServer.Name = "textBoxPathServer";
            this.textBoxPathServer.Size = new System.Drawing.Size(326, 20);
            this.textBoxPathServer.TabIndex = 2;
            this.textBoxPathServer.Visible = false;
            // 
            // buttonPathDialog
            // 
            this.buttonPathDialog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonPathDialog.Location = new System.Drawing.Point(319, 143);
            this.buttonPathDialog.Name = "buttonPathDialog";
            this.buttonPathDialog.Size = new System.Drawing.Size(18, 18);
            this.buttonPathDialog.TabIndex = 36;
            this.buttonPathDialog.UseVisualStyleBackColor = false;
            this.buttonPathDialog.Visible = false;
            this.buttonPathDialog.Click += new System.EventHandler(this.buttonPathDialog_Click);
            // 
            // timerCheckProcess
            // 
            this.timerCheckProcess.Interval = 1000;
            this.timerCheckProcess.Tick += new System.EventHandler(this.timerCheckProcess_Tick);
            // 
            // FormControlPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(351, 174);
            this.Controls.Add(this.buttonPathDialog);
            this.Controls.Add(this.textBoxPathServer);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxWorldServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormControlPanel";
            this.Text = "Control Panel (ALPHA)";
            this.Load += new System.EventHandler(this.FormControlPanel_Load);
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
        private System.Windows.Forms.Button buttonPathDialog;
        private System.Windows.Forms.CheckBox checkBoxRestartWorld;
        private System.Windows.Forms.CheckBox checkBoxRestartAuth;
        private System.Windows.Forms.Timer timerCheckProcess;
    }
}
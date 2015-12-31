namespace Manti
{
    partial class FormSettings
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
            this.treeViewSettings = new System.Windows.Forms.TreeView();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.groupBoxMysqlDatabase = new System.Windows.Forms.GroupBox();
            this.textBoxMysqlWorld = new System.Windows.Forms.TextBox();
            this.textBoxMysqlCharacters = new System.Windows.Forms.TextBox();
            this.textBoxMysqlAuth = new System.Windows.Forms.TextBox();
            this.labelMysqlWorld = new System.Windows.Forms.Label();
            this.labelMysqlCharacters = new System.Windows.Forms.Label();
            this.labelMysqlAuth = new System.Windows.Forms.Label();
            this.groupBoxMysqlDetails = new System.Windows.Forms.GroupBox();
            this.textBoxMysqlPort = new System.Windows.Forms.TextBox();
            this.textBoxMysqlPassword = new System.Windows.Forms.TextBox();
            this.textBoxMysqlUsername = new System.Windows.Forms.TextBox();
            this.textBoxMysqlAddress = new System.Windows.Forms.TextBox();
            this.labelMysqlPort = new System.Windows.Forms.Label();
            this.labelMysqlAddress = new System.Windows.Forms.Label();
            this.labelMysqlPassword = new System.Windows.Forms.Label();
            this.labelMysqlUsername = new System.Windows.Forms.Label();
            this.splitterMenu = new System.Windows.Forms.Splitter();
            this.panelSettings.SuspendLayout();
            this.groupBoxMysqlDatabase.SuspendLayout();
            this.groupBoxMysqlDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewSettings
            // 
            this.treeViewSettings.BackColor = System.Drawing.SystemColors.Control;
            this.treeViewSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeViewSettings.FullRowSelect = true;
            this.treeViewSettings.Location = new System.Drawing.Point(0, 0);
            this.treeViewSettings.Name = "treeViewSettings";
            this.treeViewSettings.Size = new System.Drawing.Size(121, 361);
            this.treeViewSettings.TabIndex = 0;
            this.treeViewSettings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSettings_AfterSelect);
            // 
            // panelSettings
            // 
            this.panelSettings.BackColor = System.Drawing.SystemColors.Control;
            this.panelSettings.Controls.Add(this.groupBoxMysqlDatabase);
            this.panelSettings.Controls.Add(this.groupBoxMysqlDetails);
            this.panelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSettings.Location = new System.Drawing.Point(121, 0);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(463, 361);
            this.panelSettings.TabIndex = 1;
            // 
            // groupBoxMysqlDatabase
            // 
            this.groupBoxMysqlDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMysqlDatabase.Controls.Add(this.textBoxMysqlWorld);
            this.groupBoxMysqlDatabase.Controls.Add(this.textBoxMysqlCharacters);
            this.groupBoxMysqlDatabase.Controls.Add(this.textBoxMysqlAuth);
            this.groupBoxMysqlDatabase.Controls.Add(this.labelMysqlWorld);
            this.groupBoxMysqlDatabase.Controls.Add(this.labelMysqlCharacters);
            this.groupBoxMysqlDatabase.Controls.Add(this.labelMysqlAuth);
            this.groupBoxMysqlDatabase.Location = new System.Drawing.Point(6, 198);
            this.groupBoxMysqlDatabase.Name = "groupBoxMysqlDatabase";
            this.groupBoxMysqlDatabase.Size = new System.Drawing.Size(445, 99);
            this.groupBoxMysqlDatabase.TabIndex = 9;
            this.groupBoxMysqlDatabase.TabStop = false;
            this.groupBoxMysqlDatabase.Text = "Databases";
            // 
            // textBoxMysqlWorld
            // 
            this.textBoxMysqlWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMysqlWorld.Location = new System.Drawing.Point(98, 73);
            this.textBoxMysqlWorld.Name = "textBoxMysqlWorld";
            this.textBoxMysqlWorld.Size = new System.Drawing.Size(341, 20);
            this.textBoxMysqlWorld.TabIndex = 10;
            // 
            // textBoxMysqlCharacters
            // 
            this.textBoxMysqlCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMysqlCharacters.Location = new System.Drawing.Point(98, 45);
            this.textBoxMysqlCharacters.Name = "textBoxMysqlCharacters";
            this.textBoxMysqlCharacters.Size = new System.Drawing.Size(341, 20);
            this.textBoxMysqlCharacters.TabIndex = 9;
            // 
            // textBoxMysqlAuth
            // 
            this.textBoxMysqlAuth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMysqlAuth.Location = new System.Drawing.Point(98, 17);
            this.textBoxMysqlAuth.Name = "textBoxMysqlAuth";
            this.textBoxMysqlAuth.Size = new System.Drawing.Size(341, 20);
            this.textBoxMysqlAuth.TabIndex = 8;
            // 
            // labelMysqlWorld
            // 
            this.labelMysqlWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMysqlWorld.AutoSize = true;
            this.labelMysqlWorld.Location = new System.Drawing.Point(6, 76);
            this.labelMysqlWorld.Name = "labelMysqlWorld";
            this.labelMysqlWorld.Size = new System.Drawing.Size(38, 13);
            this.labelMysqlWorld.TabIndex = 7;
            this.labelMysqlWorld.Text = "World:";
            // 
            // labelMysqlCharacters
            // 
            this.labelMysqlCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMysqlCharacters.AutoSize = true;
            this.labelMysqlCharacters.Location = new System.Drawing.Point(6, 48);
            this.labelMysqlCharacters.Name = "labelMysqlCharacters";
            this.labelMysqlCharacters.Size = new System.Drawing.Size(61, 13);
            this.labelMysqlCharacters.TabIndex = 6;
            this.labelMysqlCharacters.Text = "Characters:";
            // 
            // labelMysqlAuth
            // 
            this.labelMysqlAuth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMysqlAuth.AutoSize = true;
            this.labelMysqlAuth.Location = new System.Drawing.Point(6, 20);
            this.labelMysqlAuth.Name = "labelMysqlAuth";
            this.labelMysqlAuth.Size = new System.Drawing.Size(32, 13);
            this.labelMysqlAuth.TabIndex = 5;
            this.labelMysqlAuth.Text = "Auth:";
            // 
            // groupBoxMysqlDetails
            // 
            this.groupBoxMysqlDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMysqlDetails.Controls.Add(this.textBoxMysqlPort);
            this.groupBoxMysqlDetails.Controls.Add(this.textBoxMysqlPassword);
            this.groupBoxMysqlDetails.Controls.Add(this.textBoxMysqlUsername);
            this.groupBoxMysqlDetails.Controls.Add(this.textBoxMysqlAddress);
            this.groupBoxMysqlDetails.Controls.Add(this.labelMysqlPort);
            this.groupBoxMysqlDetails.Controls.Add(this.labelMysqlAddress);
            this.groupBoxMysqlDetails.Controls.Add(this.labelMysqlPassword);
            this.groupBoxMysqlDetails.Controls.Add(this.labelMysqlUsername);
            this.groupBoxMysqlDetails.Location = new System.Drawing.Point(6, 63);
            this.groupBoxMysqlDetails.Name = "groupBoxMysqlDetails";
            this.groupBoxMysqlDetails.Size = new System.Drawing.Size(445, 129);
            this.groupBoxMysqlDetails.TabIndex = 8;
            this.groupBoxMysqlDetails.TabStop = false;
            this.groupBoxMysqlDetails.Text = "Details";
            // 
            // textBoxMysqlPort
            // 
            this.textBoxMysqlPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMysqlPort.Location = new System.Drawing.Point(98, 101);
            this.textBoxMysqlPort.Name = "textBoxMysqlPort";
            this.textBoxMysqlPort.Size = new System.Drawing.Size(341, 20);
            this.textBoxMysqlPort.TabIndex = 7;
            // 
            // textBoxMysqlPassword
            // 
            this.textBoxMysqlPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMysqlPassword.Location = new System.Drawing.Point(98, 73);
            this.textBoxMysqlPassword.Name = "textBoxMysqlPassword";
            this.textBoxMysqlPassword.PasswordChar = '✱';
            this.textBoxMysqlPassword.Size = new System.Drawing.Size(341, 20);
            this.textBoxMysqlPassword.TabIndex = 6;
            // 
            // textBoxMysqlUsername
            // 
            this.textBoxMysqlUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMysqlUsername.Location = new System.Drawing.Point(98, 45);
            this.textBoxMysqlUsername.Name = "textBoxMysqlUsername";
            this.textBoxMysqlUsername.Size = new System.Drawing.Size(341, 20);
            this.textBoxMysqlUsername.TabIndex = 5;
            // 
            // textBoxMysqlAddress
            // 
            this.textBoxMysqlAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMysqlAddress.Location = new System.Drawing.Point(98, 17);
            this.textBoxMysqlAddress.Name = "textBoxMysqlAddress";
            this.textBoxMysqlAddress.Size = new System.Drawing.Size(341, 20);
            this.textBoxMysqlAddress.TabIndex = 1;
            // 
            // labelMysqlPort
            // 
            this.labelMysqlPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMysqlPort.AutoSize = true;
            this.labelMysqlPort.Location = new System.Drawing.Point(6, 104);
            this.labelMysqlPort.Name = "labelMysqlPort";
            this.labelMysqlPort.Size = new System.Drawing.Size(29, 13);
            this.labelMysqlPort.TabIndex = 4;
            this.labelMysqlPort.Text = "Port:";
            // 
            // labelMysqlAddress
            // 
            this.labelMysqlAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMysqlAddress.AutoSize = true;
            this.labelMysqlAddress.Location = new System.Drawing.Point(6, 20);
            this.labelMysqlAddress.Name = "labelMysqlAddress";
            this.labelMysqlAddress.Size = new System.Drawing.Size(48, 13);
            this.labelMysqlAddress.TabIndex = 0;
            this.labelMysqlAddress.Text = "Address:";
            // 
            // labelMysqlPassword
            // 
            this.labelMysqlPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMysqlPassword.AutoSize = true;
            this.labelMysqlPassword.Location = new System.Drawing.Point(6, 76);
            this.labelMysqlPassword.Name = "labelMysqlPassword";
            this.labelMysqlPassword.Size = new System.Drawing.Size(56, 13);
            this.labelMysqlPassword.TabIndex = 3;
            this.labelMysqlPassword.Text = "Password:";
            // 
            // labelMysqlUsername
            // 
            this.labelMysqlUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMysqlUsername.AutoSize = true;
            this.labelMysqlUsername.Location = new System.Drawing.Point(6, 48);
            this.labelMysqlUsername.Name = "labelMysqlUsername";
            this.labelMysqlUsername.Size = new System.Drawing.Size(58, 13);
            this.labelMysqlUsername.TabIndex = 2;
            this.labelMysqlUsername.Text = "Username:";
            // 
            // splitterMenu
            // 
            this.splitterMenu.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitterMenu.Location = new System.Drawing.Point(121, 0);
            this.splitterMenu.Name = "splitterMenu";
            this.splitterMenu.Size = new System.Drawing.Size(3, 361);
            this.splitterMenu.TabIndex = 2;
            this.splitterMenu.TabStop = false;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.splitterMenu);
            this.Controls.Add(this.panelSettings);
            this.Controls.Add(this.treeViewSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowIcon = false;
            this.Text = "FormSettings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.panelSettings.ResumeLayout(false);
            this.groupBoxMysqlDatabase.ResumeLayout(false);
            this.groupBoxMysqlDatabase.PerformLayout();
            this.groupBoxMysqlDetails.ResumeLayout(false);
            this.groupBoxMysqlDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewSettings;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.GroupBox groupBoxMysqlDatabase;
        private System.Windows.Forms.TextBox textBoxMysqlWorld;
        private System.Windows.Forms.TextBox textBoxMysqlCharacters;
        private System.Windows.Forms.TextBox textBoxMysqlAuth;
        private System.Windows.Forms.Label labelMysqlWorld;
        private System.Windows.Forms.Label labelMysqlCharacters;
        private System.Windows.Forms.Label labelMysqlAuth;
        private System.Windows.Forms.GroupBox groupBoxMysqlDetails;
        private System.Windows.Forms.TextBox textBoxMysqlPort;
        private System.Windows.Forms.TextBox textBoxMysqlPassword;
        private System.Windows.Forms.TextBox textBoxMysqlUsername;
        private System.Windows.Forms.TextBox textBoxMysqlAddress;
        private System.Windows.Forms.Label labelMysqlPort;
        private System.Windows.Forms.Label labelMysqlAddress;
        private System.Windows.Forms.Label labelMysqlPassword;
        private System.Windows.Forms.Label labelMysqlUsername;
        private System.Windows.Forms.Splitter splitterMenu;
    }
}
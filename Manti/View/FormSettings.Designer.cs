namespace Manti.Views {
	partial class FormSettings {
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
			this.treeViewSettings = new System.Windows.Forms.TreeView();
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
			this.tabControlSettings = new System.Windows.Forms.TabControl();
			this.tabPageMySql = new System.Windows.Forms.TabPage();
			this.tabPageGeneral = new System.Windows.Forms.TabPage();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonApply = new System.Windows.Forms.Button();
			this.groupBoxMysqlDatabase.SuspendLayout();
			this.groupBoxMysqlDetails.SuspendLayout();
			this.tabControlSettings.SuspendLayout();
			this.tabPageMySql.SuspendLayout();
			this.panel1.SuspendLayout();
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
			this.treeViewSettings.Size = new System.Drawing.Size(134, 383);
			this.treeViewSettings.TabIndex = 0;
			this.treeViewSettings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSettings_AfterSelect);
			// 
			// groupBoxMysqlDatabase
			// 
			this.groupBoxMysqlDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxMysqlDatabase.Controls.Add(this.textBoxMysqlWorld);
			this.groupBoxMysqlDatabase.Controls.Add(this.textBoxMysqlCharacters);
			this.groupBoxMysqlDatabase.Controls.Add(this.textBoxMysqlAuth);
			this.groupBoxMysqlDatabase.Controls.Add(this.labelMysqlWorld);
			this.groupBoxMysqlDatabase.Controls.Add(this.labelMysqlCharacters);
			this.groupBoxMysqlDatabase.Controls.Add(this.labelMysqlAuth);
			this.groupBoxMysqlDatabase.Location = new System.Drawing.Point(6, 141);
			this.groupBoxMysqlDatabase.Name = "groupBoxMysqlDatabase";
			this.groupBoxMysqlDatabase.Size = new System.Drawing.Size(499, 106);
			this.groupBoxMysqlDatabase.TabIndex = 9;
			this.groupBoxMysqlDatabase.TabStop = false;
			this.groupBoxMysqlDatabase.Text = "Databases";
			// 
			// textBoxMysqlWorld
			// 
			this.textBoxMysqlWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMysqlWorld.Location = new System.Drawing.Point(98, 76);
			this.textBoxMysqlWorld.Name = "textBoxMysqlWorld";
			this.textBoxMysqlWorld.Size = new System.Drawing.Size(395, 20);
			this.textBoxMysqlWorld.TabIndex = 10;
			// 
			// textBoxMysqlCharacters
			// 
			this.textBoxMysqlCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMysqlCharacters.Location = new System.Drawing.Point(98, 48);
			this.textBoxMysqlCharacters.Name = "textBoxMysqlCharacters";
			this.textBoxMysqlCharacters.Size = new System.Drawing.Size(395, 20);
			this.textBoxMysqlCharacters.TabIndex = 9;
			// 
			// textBoxMysqlAuth
			// 
			this.textBoxMysqlAuth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMysqlAuth.Location = new System.Drawing.Point(98, 20);
			this.textBoxMysqlAuth.Name = "textBoxMysqlAuth";
			this.textBoxMysqlAuth.Size = new System.Drawing.Size(395, 20);
			this.textBoxMysqlAuth.TabIndex = 8;
			// 
			// labelMysqlWorld
			// 
			this.labelMysqlWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.labelMysqlWorld.AutoSize = true;
			this.labelMysqlWorld.Location = new System.Drawing.Point(6, 79);
			this.labelMysqlWorld.Name = "labelMysqlWorld";
			this.labelMysqlWorld.Size = new System.Drawing.Size(38, 13);
			this.labelMysqlWorld.TabIndex = 7;
			this.labelMysqlWorld.Text = "World:";
			// 
			// labelMysqlCharacters
			// 
			this.labelMysqlCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.labelMysqlCharacters.AutoSize = true;
			this.labelMysqlCharacters.Location = new System.Drawing.Point(6, 51);
			this.labelMysqlCharacters.Name = "labelMysqlCharacters";
			this.labelMysqlCharacters.Size = new System.Drawing.Size(61, 13);
			this.labelMysqlCharacters.TabIndex = 6;
			this.labelMysqlCharacters.Text = "Characters:";
			// 
			// labelMysqlAuth
			// 
			this.labelMysqlAuth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.labelMysqlAuth.AutoSize = true;
			this.labelMysqlAuth.Location = new System.Drawing.Point(6, 23);
			this.labelMysqlAuth.Name = "labelMysqlAuth";
			this.labelMysqlAuth.Size = new System.Drawing.Size(32, 13);
			this.labelMysqlAuth.TabIndex = 5;
			this.labelMysqlAuth.Text = "Auth:";
			// 
			// groupBoxMysqlDetails
			// 
			this.groupBoxMysqlDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxMysqlDetails.Controls.Add(this.textBoxMysqlPort);
			this.groupBoxMysqlDetails.Controls.Add(this.textBoxMysqlPassword);
			this.groupBoxMysqlDetails.Controls.Add(this.textBoxMysqlUsername);
			this.groupBoxMysqlDetails.Controls.Add(this.textBoxMysqlAddress);
			this.groupBoxMysqlDetails.Controls.Add(this.labelMysqlPort);
			this.groupBoxMysqlDetails.Controls.Add(this.labelMysqlAddress);
			this.groupBoxMysqlDetails.Controls.Add(this.labelMysqlPassword);
			this.groupBoxMysqlDetails.Controls.Add(this.labelMysqlUsername);
			this.groupBoxMysqlDetails.Location = new System.Drawing.Point(6, 6);
			this.groupBoxMysqlDetails.Name = "groupBoxMysqlDetails";
			this.groupBoxMysqlDetails.Size = new System.Drawing.Size(499, 129);
			this.groupBoxMysqlDetails.TabIndex = 8;
			this.groupBoxMysqlDetails.TabStop = false;
			this.groupBoxMysqlDetails.Text = "Details";
			// 
			// textBoxMysqlPort
			// 
			this.textBoxMysqlPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMysqlPort.Location = new System.Drawing.Point(98, 101);
			this.textBoxMysqlPort.Name = "textBoxMysqlPort";
			this.textBoxMysqlPort.Size = new System.Drawing.Size(395, 20);
			this.textBoxMysqlPort.TabIndex = 7;
			// 
			// textBoxMysqlPassword
			// 
			this.textBoxMysqlPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMysqlPassword.Location = new System.Drawing.Point(98, 73);
			this.textBoxMysqlPassword.Name = "textBoxMysqlPassword";
			this.textBoxMysqlPassword.PasswordChar = '✱';
			this.textBoxMysqlPassword.Size = new System.Drawing.Size(395, 20);
			this.textBoxMysqlPassword.TabIndex = 6;
			// 
			// textBoxMysqlUsername
			// 
			this.textBoxMysqlUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMysqlUsername.Location = new System.Drawing.Point(98, 45);
			this.textBoxMysqlUsername.Name = "textBoxMysqlUsername";
			this.textBoxMysqlUsername.Size = new System.Drawing.Size(395, 20);
			this.textBoxMysqlUsername.TabIndex = 5;
			// 
			// textBoxMysqlAddress
			// 
			this.textBoxMysqlAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMysqlAddress.Location = new System.Drawing.Point(98, 17);
			this.textBoxMysqlAddress.Name = "textBoxMysqlAddress";
			this.textBoxMysqlAddress.Size = new System.Drawing.Size(395, 20);
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
			// tabControlSettings
			// 
			this.tabControlSettings.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.tabControlSettings.Controls.Add(this.tabPageGeneral);
			this.tabControlSettings.Controls.Add(this.tabPageMySql);
			this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlSettings.ItemSize = new System.Drawing.Size(50, 10);
			this.tabControlSettings.Location = new System.Drawing.Point(134, 0);
			this.tabControlSettings.Name = "tabControlSettings";
			this.tabControlSettings.SelectedIndex = 0;
			this.tabControlSettings.Size = new System.Drawing.Size(519, 383);
			this.tabControlSettings.TabIndex = 11;
			// 
			// tabPageMySql
			// 
			this.tabPageMySql.Controls.Add(this.groupBoxMysqlDetails);
			this.tabPageMySql.Controls.Add(this.groupBoxMysqlDatabase);
			this.tabPageMySql.Location = new System.Drawing.Point(4, 14);
			this.tabPageMySql.Name = "tabPageMySql";
			this.tabPageMySql.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMySql.Size = new System.Drawing.Size(511, 365);
			this.tabPageMySql.TabIndex = 0;
			this.tabPageMySql.Text = "MySql";
			this.tabPageMySql.UseVisualStyleBackColor = true;
			// 
			// tabPageGeneral
			// 
			this.tabPageGeneral.Location = new System.Drawing.Point(4, 14);
			this.tabPageGeneral.Name = "tabPageGeneral";
			this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageGeneral.Size = new System.Drawing.Size(735, 544);
			this.tabPageGeneral.TabIndex = 1;
			this.tabPageGeneral.Text = "General";
			this.tabPageGeneral.UseVisualStyleBackColor = true;
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonOk.Location = new System.Drawing.Point(441, 3);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 12;
			this.buttonOk.Text = "OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonActions_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.buttonCancel.Location = new System.Drawing.Point(10, 2);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 13;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonActions_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonApply);
			this.panel1.Controls.Add(this.buttonCancel);
			this.panel1.Controls.Add(this.buttonOk);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(134, 354);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(519, 29);
			this.panel1.TabIndex = 14;
			// 
			// buttonApply
			// 
			this.buttonApply.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.buttonApply.Location = new System.Drawing.Point(360, 3);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(75, 23);
			this.buttonApply.TabIndex = 14;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			this.buttonApply.Click += new System.EventHandler(this.buttonActions_Click);
			// 
			// FormSettings
			// 
			this.AcceptButton = this.buttonApply;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonOk;
			this.ClientSize = new System.Drawing.Size(653, 383);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tabControlSettings);
			this.Controls.Add(this.treeViewSettings);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSettings";
			this.ShowIcon = false;
			this.Text = "FormSettings";
			this.Load += new System.EventHandler(this.FormSettings_Load);
			this.groupBoxMysqlDatabase.ResumeLayout(false);
			this.groupBoxMysqlDatabase.PerformLayout();
			this.groupBoxMysqlDetails.ResumeLayout(false);
			this.groupBoxMysqlDetails.PerformLayout();
			this.tabControlSettings.ResumeLayout(false);
			this.tabPageMySql.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeViewSettings;
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
		private System.Windows.Forms.TabControl tabControlSettings;
		private System.Windows.Forms.TabPage tabPageMySql;
		private System.Windows.Forms.TabPage tabPageGeneral;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonApply;
	}
}
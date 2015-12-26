namespace Manti
{
    partial class FormMySQL
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
            this.labelAddress = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.groupBoxMySQL = new System.Windows.Forms.GroupBox();
            this.checkBoxSavePassword = new System.Windows.Forms.CheckBox();
            this.checkBoxSaveInformation = new System.Windows.Forms.CheckBox();
            this.groupBoxMySQLDatabase = new System.Windows.Forms.GroupBox();
            this.textBoxWorld = new System.Windows.Forms.TextBox();
            this.textBoxCharacters = new System.Windows.Forms.TextBox();
            this.textBoxAuth = new System.Windows.Forms.TextBox();
            this.labelWorld = new System.Windows.Forms.Label();
            this.labelCharacters = new System.Windows.Forms.Label();
            this.labelAuth = new System.Windows.Forms.Label();
            this.groupBoxMySQLLogin = new System.Windows.Forms.GroupBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonOffline = new System.Windows.Forms.Button();
            this.groupBoxMySQL.SuspendLayout();
            this.groupBoxMySQLDatabase.SuspendLayout();
            this.groupBoxMySQLLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(6, 20);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(48, 13);
            this.labelAddress.TabIndex = 0;
            this.labelAddress.Text = "Address:";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(98, 17);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(146, 20);
            this.textBoxAddress.TabIndex = 1;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(6, 48);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(58, 13);
            this.labelUsername.TabIndex = 2;
            this.labelUsername.Text = "Username:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(6, 76);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 3;
            this.labelPassword.Text = "Password:";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(6, 104);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 13);
            this.labelPort.TabIndex = 4;
            this.labelPort.Text = "Port:";
            // 
            // groupBoxMySQL
            // 
            this.groupBoxMySQL.Controls.Add(this.checkBoxSavePassword);
            this.groupBoxMySQL.Controls.Add(this.checkBoxSaveInformation);
            this.groupBoxMySQL.Controls.Add(this.groupBoxMySQLDatabase);
            this.groupBoxMySQL.Controls.Add(this.groupBoxMySQLLogin);
            this.groupBoxMySQL.Location = new System.Drawing.Point(12, 12);
            this.groupBoxMySQL.Name = "groupBoxMySQL";
            this.groupBoxMySQL.Size = new System.Drawing.Size(519, 154);
            this.groupBoxMySQL.TabIndex = 5;
            this.groupBoxMySQL.TabStop = false;
            this.groupBoxMySQL.Text = "MySQL Information";
            // 
            // checkBoxSavePassword
            // 
            this.checkBoxSavePassword.AutoSize = true;
            this.checkBoxSavePassword.Location = new System.Drawing.Point(412, 127);
            this.checkBoxSavePassword.Name = "checkBoxSavePassword";
            this.checkBoxSavePassword.Size = new System.Drawing.Size(100, 17);
            this.checkBoxSavePassword.TabIndex = 9;
            this.checkBoxSavePassword.Text = "Save Password";
            this.checkBoxSavePassword.UseVisualStyleBackColor = true;
            // 
            // checkBoxSaveInformation
            // 
            this.checkBoxSaveInformation.AutoSize = true;
            this.checkBoxSaveInformation.Location = new System.Drawing.Point(262, 127);
            this.checkBoxSaveInformation.Name = "checkBoxSaveInformation";
            this.checkBoxSaveInformation.Size = new System.Drawing.Size(106, 17);
            this.checkBoxSaveInformation.TabIndex = 8;
            this.checkBoxSaveInformation.Text = "Save Information";
            this.checkBoxSaveInformation.UseVisualStyleBackColor = true;
            // 
            // groupBoxMySQLDatabase
            // 
            this.groupBoxMySQLDatabase.Controls.Add(this.textBoxWorld);
            this.groupBoxMySQLDatabase.Controls.Add(this.textBoxCharacters);
            this.groupBoxMySQLDatabase.Controls.Add(this.textBoxAuth);
            this.groupBoxMySQLDatabase.Controls.Add(this.labelWorld);
            this.groupBoxMySQLDatabase.Controls.Add(this.labelCharacters);
            this.groupBoxMySQLDatabase.Controls.Add(this.labelAuth);
            this.groupBoxMySQLDatabase.Location = new System.Drawing.Point(262, 19);
            this.groupBoxMySQLDatabase.Name = "groupBoxMySQLDatabase";
            this.groupBoxMySQLDatabase.Size = new System.Drawing.Size(250, 99);
            this.groupBoxMySQLDatabase.TabIndex = 7;
            this.groupBoxMySQLDatabase.TabStop = false;
            this.groupBoxMySQLDatabase.Text = "Databases";
            // 
            // textBoxWorld
            // 
            this.textBoxWorld.Location = new System.Drawing.Point(98, 73);
            this.textBoxWorld.Name = "textBoxWorld";
            this.textBoxWorld.Size = new System.Drawing.Size(146, 20);
            this.textBoxWorld.TabIndex = 10;
            // 
            // textBoxCharacters
            // 
            this.textBoxCharacters.Location = new System.Drawing.Point(98, 45);
            this.textBoxCharacters.Name = "textBoxCharacters";
            this.textBoxCharacters.Size = new System.Drawing.Size(146, 20);
            this.textBoxCharacters.TabIndex = 9;
            // 
            // textBoxAuth
            // 
            this.textBoxAuth.Location = new System.Drawing.Point(98, 17);
            this.textBoxAuth.Name = "textBoxAuth";
            this.textBoxAuth.Size = new System.Drawing.Size(146, 20);
            this.textBoxAuth.TabIndex = 8;
            // 
            // labelWorld
            // 
            this.labelWorld.AutoSize = true;
            this.labelWorld.Location = new System.Drawing.Point(6, 76);
            this.labelWorld.Name = "labelWorld";
            this.labelWorld.Size = new System.Drawing.Size(38, 13);
            this.labelWorld.TabIndex = 7;
            this.labelWorld.Text = "World:";
            // 
            // labelCharacters
            // 
            this.labelCharacters.AutoSize = true;
            this.labelCharacters.Location = new System.Drawing.Point(6, 48);
            this.labelCharacters.Name = "labelCharacters";
            this.labelCharacters.Size = new System.Drawing.Size(61, 13);
            this.labelCharacters.TabIndex = 6;
            this.labelCharacters.Text = "Characters:";
            // 
            // labelAuth
            // 
            this.labelAuth.AutoSize = true;
            this.labelAuth.Location = new System.Drawing.Point(6, 20);
            this.labelAuth.Name = "labelAuth";
            this.labelAuth.Size = new System.Drawing.Size(32, 13);
            this.labelAuth.TabIndex = 5;
            this.labelAuth.Text = "Auth:";
            // 
            // groupBoxMySQLLogin
            // 
            this.groupBoxMySQLLogin.Controls.Add(this.textBoxPort);
            this.groupBoxMySQLLogin.Controls.Add(this.textBoxPassword);
            this.groupBoxMySQLLogin.Controls.Add(this.textBoxUsername);
            this.groupBoxMySQLLogin.Controls.Add(this.textBoxAddress);
            this.groupBoxMySQLLogin.Controls.Add(this.labelPort);
            this.groupBoxMySQLLogin.Controls.Add(this.labelAddress);
            this.groupBoxMySQLLogin.Controls.Add(this.labelPassword);
            this.groupBoxMySQLLogin.Controls.Add(this.labelUsername);
            this.groupBoxMySQLLogin.Location = new System.Drawing.Point(6, 19);
            this.groupBoxMySQLLogin.Name = "groupBoxMySQLLogin";
            this.groupBoxMySQLLogin.Size = new System.Drawing.Size(250, 129);
            this.groupBoxMySQLLogin.TabIndex = 6;
            this.groupBoxMySQLLogin.TabStop = false;
            this.groupBoxMySQLLogin.Text = "Details";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(98, 101);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(146, 20);
            this.textBoxPort.TabIndex = 7;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(98, 73);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '✱';
            this.textBoxPassword.Size = new System.Drawing.Size(146, 20);
            this.textBoxPassword.TabIndex = 6;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(98, 45);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(146, 20);
            this.textBoxUsername.TabIndex = 5;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonConnect.Location = new System.Drawing.Point(18, 172);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(158, 25);
            this.buttonConnect.TabIndex = 6;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonClear.Location = new System.Drawing.Point(366, 172);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(158, 25);
            this.buttonClear.TabIndex = 7;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonOffline
            // 
            this.buttonOffline.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOffline.Location = new System.Drawing.Point(192, 172);
            this.buttonOffline.Name = "buttonOffline";
            this.buttonOffline.Size = new System.Drawing.Size(158, 25);
            this.buttonOffline.TabIndex = 8;
            this.buttonOffline.Text = "Offline";
            this.buttonOffline.UseVisualStyleBackColor = true;
            this.buttonOffline.Click += new System.EventHandler(this.buttonOffline_Click);
            // 
            // FormMySQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 202);
            this.Controls.Add(this.buttonOffline);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.groupBoxMySQL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMySQL";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manti - MySQL Connection";
            this.Load += new System.EventHandler(this.FormMySQL_Load);
            this.groupBoxMySQL.ResumeLayout(false);
            this.groupBoxMySQL.PerformLayout();
            this.groupBoxMySQLDatabase.ResumeLayout(false);
            this.groupBoxMySQLDatabase.PerformLayout();
            this.groupBoxMySQLLogin.ResumeLayout(false);
            this.groupBoxMySQLLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.GroupBox groupBoxMySQL;
        private System.Windows.Forms.GroupBox groupBoxMySQLDatabase;
        private System.Windows.Forms.Label labelWorld;
        private System.Windows.Forms.Label labelCharacters;
        private System.Windows.Forms.Label labelAuth;
        private System.Windows.Forms.GroupBox groupBoxMySQLLogin;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TextBox textBoxWorld;
        private System.Windows.Forms.TextBox textBoxCharacters;
        private System.Windows.Forms.TextBox textBoxAuth;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Button buttonOffline;
        private System.Windows.Forms.CheckBox checkBoxSavePassword;
        private System.Windows.Forms.CheckBox checkBoxSaveInformation;
    }
}


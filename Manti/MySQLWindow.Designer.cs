namespace Tricore
{
    partial class MySQLWindow
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxWorld = new System.Windows.Forms.TextBox();
            this.textBoxCharacters = new System.Windows.Forms.TextBox();
            this.textBoxAuth = new System.Windows.Forms.TextBox();
            this.labelWorld = new System.Windows.Forms.Label();
            this.labelCharacters = new System.Windows.Forms.Label();
            this.labelAuth = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonTestConnect = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.textBoxAddress.Text = "127.0.0.1";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 154);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MySQL Information";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxWorld);
            this.groupBox3.Controls.Add(this.textBoxCharacters);
            this.groupBox3.Controls.Add(this.textBoxAuth);
            this.groupBox3.Controls.Add(this.labelWorld);
            this.groupBox3.Controls.Add(this.labelCharacters);
            this.groupBox3.Controls.Add(this.labelAuth);
            this.groupBox3.Location = new System.Drawing.Point(262, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 129);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Databases";
            // 
            // textBoxWorld
            // 
            this.textBoxWorld.Location = new System.Drawing.Point(98, 73);
            this.textBoxWorld.Name = "textBoxWorld";
            this.textBoxWorld.Size = new System.Drawing.Size(146, 20);
            this.textBoxWorld.TabIndex = 10;
            this.textBoxWorld.Text = "world";
            // 
            // textBoxCharacters
            // 
            this.textBoxCharacters.Location = new System.Drawing.Point(98, 45);
            this.textBoxCharacters.Name = "textBoxCharacters";
            this.textBoxCharacters.Size = new System.Drawing.Size(146, 20);
            this.textBoxCharacters.TabIndex = 9;
            this.textBoxCharacters.Text = "characters";
            // 
            // textBoxAuth
            // 
            this.textBoxAuth.Location = new System.Drawing.Point(98, 17);
            this.textBoxAuth.Name = "textBoxAuth";
            this.textBoxAuth.Size = new System.Drawing.Size(146, 20);
            this.textBoxAuth.TabIndex = 8;
            this.textBoxAuth.Text = "auth";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxPort);
            this.groupBox2.Controls.Add(this.textBoxPassword);
            this.groupBox2.Controls.Add(this.textBoxUsername);
            this.groupBox2.Controls.Add(this.textBoxAddress);
            this.groupBox2.Controls.Add(this.labelPort);
            this.groupBox2.Controls.Add(this.labelAddress);
            this.groupBox2.Controls.Add(this.labelPassword);
            this.groupBox2.Controls.Add(this.labelUsername);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 129);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Login Form";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(98, 101);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(146, 20);
            this.textBoxPort.TabIndex = 7;
            this.textBoxPort.Text = "3306";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(98, 73);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(146, 20);
            this.textBoxPassword.TabIndex = 6;
            this.textBoxPassword.Text = "password";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(98, 45);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(146, 20);
            this.textBoxUsername.TabIndex = 5;
            this.textBoxUsername.Text = "root";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(18, 172);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(250, 23);
            this.buttonConnect.TabIndex = 6;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonTestConnect
            // 
            this.buttonTestConnect.Location = new System.Drawing.Point(274, 172);
            this.buttonTestConnect.Name = "buttonTestConnect";
            this.buttonTestConnect.Size = new System.Drawing.Size(250, 23);
            this.buttonTestConnect.TabIndex = 7;
            this.buttonTestConnect.Text = "Clear";
            this.buttonTestConnect.UseVisualStyleBackColor = true;
            this.buttonTestConnect.Click += new System.EventHandler(this.buttonTestConnect_Click);
            // 
            // MySQLWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 202);
            this.Controls.Add(this.buttonTestConnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MySQLWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tricore - MySQL Connection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelWorld;
        private System.Windows.Forms.Label labelCharacters;
        private System.Windows.Forms.Label labelAuth;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonTestConnect;
        private System.Windows.Forms.TextBox textBoxWorld;
        private System.Windows.Forms.TextBox textBoxCharacters;
        private System.Windows.Forms.TextBox textBoxAuth;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
    }
}


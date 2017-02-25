using System;
using System.Windows.Forms;

using Manti.Classes.Settings;
using Manti.Classes.Database;

namespace Manti.Views {
	public partial class FormMySQL : Form {
		public FormMySQL() {
			InitializeComponent();
		}

		public static bool Offline = false;
		public static string dbPassword;

		// TODO: Remove the static variables when polishing is complete
		public static string Address;
		public static UInt16 Port;
		public static string Username;
		public static string Password;
		public static string DatabaseAuth;
		public static string DatabaseCharacters;
		public static string DatabaseWorld;

		#region Functions
		private void SaveSettings() {

			Settings.setSetting(Setting.Address, textBoxAddress.Text);
			Settings.setSetting(Setting.Username, textBoxUsername.Text);
			Settings.setSetting(Setting.Port, textBoxPort.Text);
			Settings.setSetting(Setting.DatabaseAuth, textBoxAuth.Text);
			Settings.setSetting(Setting.DatabaseCharacters, textBoxCharacters.Text);
			Settings.setSetting(Setting.DatabaseWorld, textBoxWorld.Text);
			

			if(checkBoxSavePassword.Checked == true) {
				Settings.setSetting(Setting.Password, textBoxPassword.Text);
				Settings.setSetting(Setting.SavePassword, checkBoxSavePassword.Checked.ToString());
			}

			Settings.saveSettings();
		}

		private void loadSettings() {
			textBoxAddress.Text          = Settings.getSetting(Setting.Address);
			textBoxUsername.Text         = Settings.getSetting(Setting.Username);
			textBoxPassword.Text         = Settings.getSetting(Setting.Password);
			textBoxPort.Text             = Settings.getSetting(Setting.Port);
			textBoxAuth.Text             = Settings.getSetting(Setting.DatabaseAuth);
			textBoxCharacters.Text       = Settings.getSetting(Setting.DatabaseCharacters);
			textBoxWorld.Text            = Settings.getSetting(Setting.DatabaseWorld);

			checkBoxSavePassword.Checked = Convert.ToBoolean(Settings.getSetting(Setting.SavePassword));
		}

		private void FormMySQL_Load(object sender, EventArgs e) {
			loadSettings();
		}

		#endregion

		private void buttonConnect_Click(object sender, EventArgs e) {
			if(MySqlAssist.TestConnection(new DatabaseAuth(textBoxAddress.Text, textBoxUsername.Text, textBoxPassword.Text, Convert.ToUInt16(textBoxPort.Text), textBoxAuth.Text))) {
				SaveSettings();

				dbPassword = textBoxPassword.Text;

				this.Hide();
				var mf = new Views.FormMain();
				mf.FormClosed += (s, args) => this.Close();
				mf.Show();
			}
		}

		private void buttonClear_Click(object sender, EventArgs e) {
			textBoxAddress.Clear();
			textBoxUsername.Clear();
			textBoxPassword.Clear();
			textBoxPort.Clear();

			textBoxAuth.Clear();
			textBoxCharacters.Clear();
			textBoxWorld.Clear();

			checkBoxSavePassword.Checked = false;
		}

		private void buttonOffline_Click(object sender, EventArgs e) {
			MySqlDatabase.runOffline = true;

			this.Hide();
			var mf = new Views.FormMain();
			mf.FormClosed += (s, args) => this.Close();
			mf.Show();
		}
	}
}

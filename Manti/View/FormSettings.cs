using System;
using System.Windows.Forms;

using Manti.Classes.Settings;
using Manti.Classes.Database;

namespace Manti.Views {
	public partial class FormSettings : Form {
		public FormSettings() {
			InitializeComponent();
		}

		private TreeNode[] options = new TreeNode[] {
			new TreeNode("General"),
			new TreeNode("MySQL")
			/*
			new TreeNode("Account"),
			new TreeNode("Character"),
			new TreeNode("Creature"),
			new TreeNode("Quest"),
			new TreeNode("Game Object"),
			new TreeNode("Item"),
			*/
		};

		private void FormSettings_Load(object sender, EventArgs e) {
			LoadMySqlSettings();

			foreach(TreeNode tn in options) {
				tn.Name = "NodeSettings" + tn.Text.Trim();

				treeViewSettings.Nodes.Add(tn);
			};
		}

		private void treeViewSettings_AfterSelect(object sender, TreeViewEventArgs e) {
			tabControlSettings.SelectedIndex = treeViewSettings.SelectedNode.Index;
		}

		private void buttonActions_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			if(btn == buttonOk) {
				if(testMySqlSettings()) {
					SaveMySqlSettings();
				} else {
					MessageBox.Show("Could not save the MySql Credentials.\nPlease doublecheck the given information.", "MySql - Could not  connect to databases.", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				this.Close();
			} else if(btn == buttonApply) {
				if(testMySqlSettings()) {
					SaveMySqlSettings();
				} else {
					MessageBox.Show("Could not save the MySql Credentials.\nPlease doublecheck the given information.", "MySql - Could not  connect to databases.", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			} else if(btn == buttonCancel) {
				this.Close();
			}
		}

		private void LoadMySqlSettings() {
			textBoxMysqlAddress.Text    = Settings.getSetting(Setting.Address);
			textBoxMysqlUsername.Text   = Settings.getSetting(Setting.Username);
			textBoxMysqlPassword.Text   = Settings.getSetting(Setting.Password);
			textBoxMysqlPort.Text       = Settings.getSetting(Setting.Port);
			textBoxMysqlAuth.Text       = Settings.getSetting(Setting.DatabaseAuth);
			textBoxMysqlCharacters.Text = Settings.getSetting(Setting.DatabaseCharacters);
			textBoxMysqlWorld.Text      = Settings.getSetting(Setting.DatabaseWorld);
		}

		private void SaveMySqlSettings() {
			Settings.setSetting(Setting.Address, textBoxMysqlAddress.Text);
			Settings.setSetting(Setting.Username, textBoxMysqlUsername.Text);
			Settings.setSetting(Setting.Password, textBoxMysqlPassword.Text);
			Settings.setSetting(Setting.Port, textBoxMysqlPort.Text);
			Settings.setSetting(Setting.DatabaseAuth, textBoxMysqlAuth.Text);
			Settings.setSetting(Setting.DatabaseCharacters, textBoxMysqlCharacters.Text);
			Settings.setSetting(Setting.DatabaseWorld, textBoxMysqlWorld.Text);
		}

		private bool testMySqlSettings() {
			var address = textBoxMysqlAddress.Text;
			var user    = textBoxMysqlUsername.Text;
			var pass    = textBoxMysqlPassword.Text;
			var port    = Convert.ToUInt16(textBoxMysqlPort.Text);

			var da = new DatabaseAuth(address, user, pass, port, textBoxMysqlAuth.Text);
			var dc = new DatabaseCharacters(address, user, pass, port, textBoxMysqlCharacters.Text);
			var dw = new DatabaseWorld(address, user, pass, port, textBoxMysqlWorld.Text);

			if(MySqlAssist.TestConnection(da) && MySqlAssist.TestConnection(dc) && MySqlAssist.TestConnection(dw)) {
				return true;
			}

			return false;
		}

	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manti.Views {
	public partial class FormSettings : Form {
		public FormSettings() {
			InitializeComponent();
		}

		private TreeNode[] options = new TreeNode[]
		{
			new TreeNode("MySQL"),
			new TreeNode("General"),
			new TreeNode("Account"),
			new TreeNode("Character"),
			new TreeNode("Creature"),
			new TreeNode("Quest"),
			new TreeNode("Game Object"),
			new TreeNode("Item"),
		};

		private void LoadSettings() {
			// MySQL
			textBoxMysqlAddress.Text = Properties.Settings.Default.Address;
			textBoxMysqlUsername.Text = Properties.Settings.Default.Username;
			textBoxMysqlPassword.Text = Properties.Settings.Default.Password;
			textBoxMysqlPort.Text = Properties.Settings.Default.Port;
			textBoxMysqlAuth.Text = Properties.Settings.Default.DatabaseAuth;
			textBoxMysqlCharacters.Text = Properties.Settings.Default.DatabaseCharacters;
			textBoxMysqlWorld.Text = Properties.Settings.Default.DatabaseWorld;
		}

		private void SaveSettings() {
			// MySQL
			Properties.Settings.Default.Address = textBoxMysqlAddress.Text;
			Properties.Settings.Default.Username = textBoxMysqlUsername.Text;
			Properties.Settings.Default.Password = textBoxMysqlPassword.Text;
			Properties.Settings.Default.Port = textBoxMysqlPort.Text;
			Properties.Settings.Default.DatabaseAuth = textBoxMysqlAuth.Text;
			Properties.Settings.Default.DatabaseCharacters = textBoxMysqlCharacters.Text;
			Properties.Settings.Default.DatabaseWorld = textBoxMysqlWorld.Text;
		}

		private void DisableVisibility(Control parent) {
			foreach(Control child in parent.Controls) {
				child.Visible = false;
			}
		}

		private void FormSettings_Load(object sender, EventArgs e) {
			LoadSettings();

			foreach(TreeNode tn in options) {
				tn.Name = "NodeSettings" + tn.Text;

				treeViewSettings.Nodes.Add(tn);
			};
		}

		private void treeViewSettings_AfterSelect(object sender, TreeViewEventArgs e) {
			DisableVisibility(panelSettings);

			switch(treeViewSettings.SelectedNode.Text) {
				case "MySQL":
					groupBoxMysqlDetails.Visible = true;
					groupBoxMysqlDatabase.Visible = true;
					break;
				case "General":

					break;
				case "Account":

					break;
				case "Character":

					break;
				case "Creature":

					break;
				case "Quest":

					break;
				case "Game Object":

					break;
				case "Item":

					break;
			}
		}
	}
}

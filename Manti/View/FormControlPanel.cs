using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

using Manti.Classes.Settings;

namespace Manti.Views {
	public partial class FormControlPanel : Form {
		public FormControlPanel() {
			InitializeComponent();
		}

		private void FormControlPanel_Load(object sender, EventArgs e) {
			buttonWorldServer.FlatAppearance.BorderSize  = 0;
			buttonAuthServer.FlatAppearance.BorderSize   = 0;
			buttonWorldServer.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
			buttonAuthServer.FlatAppearance.BorderColor  = Color.FromArgb(0, 255, 255, 255);

			timerCheckProcess.Start();
		}

		private bool togglePathComponents() {
			textBoxPathServer.Visible = !textBoxPathServer.Visible;
			buttonPathDialog.Visible  = !buttonPathDialog.Visible;

			return textBoxPathServer.Visible;
		}

		private void toggleServerState(bool isRunning, Label label, Button btn) {
			if(isRunning) {
				label.Text = "Status: ONLINE";
				btn.BackgroundImage = Manti.Properties.Resources.iconStopButton;
			} else {
				label.Text = "Status: OFFLINE";
				btn.BackgroundImage = Manti.Properties.Resources.iconPlayButton;
			} 
		}

		private bool isProcessRunning(string path) {
			string fileName = Path.GetFileName(path);
			fileName = fileName.Substring(0, fileName.Length - 4);

			Process[] pName = Process.GetProcessesByName(fileName);

			if(pName.Length > 0) { return true; }

			return false;
		}

		private bool startProcess(string path, bool hideProcess) {
			var process = new System.Diagnostics.Process();

			process.StartInfo.FileName         = path;
			process.StartInfo.UseShellExecute  = false;
			process.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);

			if(hideProcess) {
				process.StartInfo.WindowStyle    = System.Diagnostics.ProcessWindowStyle.Hidden;
				process.StartInfo.CreateNoWindow = true;
			}

			try {
				process.Start();
				return true;
			} catch { return false; }
		}

		private void closeProcess(string path) {
			string fileName = Path.GetFileName(path);
			fileName = fileName.Substring(0, fileName.Length - 4);

			foreach(var process in Process.GetProcessesByName(fileName)) {
				process.Kill();
			}
		}

		private void buttonServer_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			string path = "";
			bool hideProcess = false;
			bool isRunning = false;

			var model = Models.ServerModel.getInstance();

			if(btn == buttonAuthServer) {
				path = Settings.getSetting(Setting.PathAuthserver);
				hideProcess = checkBoxHideAuth.Checked;
				isRunning = model.isAuthOnline;
			} else if(btn == buttonWorldServer) {
				path = Settings.getSetting(Setting.PathWorldserver);
				hideProcess = checkBoxHideWorld.Checked;
				isRunning = model.isWorldOnline;
			}

			if(!isRunning) {
				if(File.Exists(path) && path.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase)) {
					bool isStarted = startProcess(path, hideProcess);

					if(btn == buttonAuthServer) {
						model.isAuthOnline = isStarted;
						toggleServerState(isStarted, labelAuthStatus, btn);
					} else if(btn == buttonWorldServer) {
						model.isWorldOnline = isStarted;
						toggleServerState(isStarted, labelWorldStatus, btn);
					}
				} else {
					MessageBox.Show("The path has not be found or the selected file is not a .exe file.\nDo you mind selecting a new one?", "Error: Unknown Path or File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			} else {
				closeProcess(path);

				if(btn == buttonAuthServer) {
					model.isAuthOnline = false;
					toggleServerState(false, labelAuthStatus, btn);
				} else if(btn == buttonWorldServer) {
					model.isWorldOnline = false;
					toggleServerState(false, labelWorldStatus, btn);
				}
			}

			if(!model.isAuthOnline && !model.isWorldOnline) {
				timerCheckProcess.Stop();
			} else {
				timerCheckProcess.Start();
			}
		}

		private void buttonPath_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			if(togglePathComponents()) {
				string path = "";

				if(btn == buttonAuthPath) {
					path = Settings.getSetting(Setting.PathAuthserver);
				} else if(btn == buttonWorldPath) {
					path = Settings.getSetting(Setting.PathWorldserver);
				}

				textBoxPathServer.Text = path;
			} else {
				if(!string.IsNullOrEmpty(textBoxPathServer.Text)) {
					if(btn == buttonAuthPath) {
						Settings.setSetting(Setting.PathAuthserver, textBoxPathServer.Text);
					} else if(btn == buttonWorldPath) {
						Settings.setSetting(Setting.PathWorldserver, textBoxPathServer.Text);
					}

					Settings.saveSettings();
				}
			}
		}

		private void buttonPathDialog_Click(object sender, EventArgs e) {
			var ofd = new OpenFileDialog();

			if(ofd.ShowDialog() == DialogResult.OK) {
				textBoxPathServer.Text = ofd.FileName;
			}
		}

		private void timerCheckProcess_Tick(object sender, EventArgs e) {
			var model = Models.ServerModel.getInstance();
			bool isRunning;

			string aPath = Settings.getSetting(Setting.PathAuthserver);
			string wPath = Settings.getSetting(Setting.PathWorldserver);

			if(!string.IsNullOrEmpty(aPath)) {
				isRunning = isProcessRunning(aPath);
				toggleServerState(isRunning, labelAuthStatus, buttonAuthServer);
				model.isAuthOnline = isRunning;

				if(checkBoxRestartAuth.Checked) {
					if(!model.isAuthOnline) {
						startProcess(aPath, checkBoxHideAuth.Checked);
					}
				}
			}

			if(!string.IsNullOrEmpty(wPath)) {
				isRunning = isProcessRunning(wPath);
				toggleServerState(isRunning, labelWorldStatus, buttonWorldServer);
				model.isWorldOnline = isRunning;

				if(checkBoxRestartWorld.Checked) {
					if(!model.isWorldOnline) {
						startProcess(wPath, checkBoxHideWorld.Checked);
					}
				}
			}
		}

		private void checkBoxRestartAuth_CheckedChanged(object sender, EventArgs e) {

		}
	}
}

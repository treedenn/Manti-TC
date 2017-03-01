using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Manti.Classes.Settings;

namespace Manti.Views {
	public partial class FormControlPanel : Form {
		public FormControlPanel() {
			InitializeComponent();
		}

		private Models.ControlPanelModel model;

		private void FormControlPanel_Load(object sender, EventArgs e) {
			model = Models.ControlPanelModel.getInstance();

			string path = Settings.getSetting(Setting.PathServer);

			if(!string.IsNullOrEmpty(path)) {
				model.findServerPaths(path);
				model.findConfigPaths(path);
				textBoxPathServer.Text = path;
			}

			buttonWorldServer.FlatAppearance.BorderSize  = 0;
			buttonAuthServer.FlatAppearance.BorderSize   = 0;
			buttonWorldServer.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
			buttonAuthServer.FlatAppearance.BorderColor  = Color.FromArgb(0, 255, 255, 255);
			buttonWorldConfig.FlatAppearance.BorderSize  = 0;
			buttonAuthConfig.FlatAppearance.BorderSize   = 0;
			buttonWorldConfig.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
			buttonAuthConfig.FlatAppearance.BorderColor  = Color.FromArgb(0, 255, 255, 255);
			
			timerCheckProcess.Start();
		}

		private void toggleServerState(bool isRunning, Button btn, PictureBox pb) {
			if(isRunning) {
				btn.BackgroundImage = Manti.Properties.Resources.iconStopButton;
				pb.BackgroundImage  = Manti.Properties.Resources.iconAdd;
			} else {
				btn.BackgroundImage = Manti.Properties.Resources.iconPlayButton;
				pb.BackgroundImage  = Manti.Properties.Resources.iconDelete;
			} 
		}

		private void buttonServer_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			string path      = "";
			bool hideProcess = false;
			bool isRunning   = false;

			var model = Models.ControlPanelModel.getInstance();

			if(btn == buttonAuthServer) {
				path = model.pathAuthServer;
				hideProcess = checkBoxHideAuth.Checked;
				isRunning   = model.isAuthOnline;
			} else if(btn == buttonWorldServer) {
				path = model.pathWorldServer;
				hideProcess = checkBoxHideWorld.Checked;
				isRunning   = model.isWorldOnline;
			}

			if(!isRunning) {
				bool isStarted;

				if(btn == buttonAuthServer) {
					if(File.Exists(path)) {
						isStarted = model.startProcess(path, hideProcess);

						model.isAuthOnline = isStarted;
						toggleServerState(isStarted, btn, pictureBoxAuth);
					} else {
						MessageBox.Show("The path has not be found or the selected file is not a .exe file.\nDo you mind selecting a new one?", "Error: Unknown Path or File", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				} else if(btn == buttonWorldServer) {
					if(File.Exists(path)) {
						isStarted = model.startProcess(path, hideProcess);

						model.isWorldOnline = isStarted;
						toggleServerState(isStarted, btn, pictureBoxWorld);
					} else {
						MessageBox.Show("The path has not be found or the selected file is not a .exe file.\nDo you mind selecting a new one?", "Error: Unknown Path or File", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			} else {
				var isClosed = model.closeProcess(path);

				if(btn == buttonAuthServer) {
					model.isAuthOnline = !isClosed;
					toggleServerState(false, btn, pictureBoxAuth);
				} else if(btn == buttonWorldServer) {
					model.isWorldOnline = !isClosed;
					toggleServerState(false, btn, pictureBoxWorld);
				}
			}

			/* Perhaps useful for later..
			if(!model.isAuthOnline && !model.isWorldOnline) {
				timerCheckProcess.Stop();
			} else {
				timerCheckProcess.Start();
			}
			*/
		}
		private void buttonConfig_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			if(btn == buttonAuthConfig) {
				if(File.Exists(model.pathAuthConfig)) {
					model.startProcess(model.pathAuthConfig, false);
				}
			} else if(btn == buttonWorldConfig) {
				if(File.Exists(model.pathWorldConfig)) {
					model.startProcess(model.pathWorldConfig, false);
				}
			}
		}

		private void buttonPathDialog_Click(object sender, EventArgs e) {
			var ofd = new FolderBrowserDialog();
			ofd.Description = "Select the TrinityCore Folder containing the .exe and .conf files!";
			ofd.SelectedPath = Settings.getSetting(Setting.PathServer);

			if(ofd.ShowDialog() == DialogResult.OK) {
				Settings.setSetting(Setting.PathServer, ofd.SelectedPath);
				Settings.saveSettings();

				model.findServerPaths(ofd.SelectedPath);
				model.findConfigPaths(ofd.SelectedPath);
			}
		}

		private void timerCheckProcess_Tick(object sender, EventArgs e) {
			bool isRunning;

			string aPath = model.pathAuthServer;
			string wPath = model.pathWorldServer;

			if(!string.IsNullOrEmpty(aPath)) {
				isRunning = model.isProcessRunning(aPath);
				toggleServerState(isRunning, buttonAuthServer, pictureBoxAuth);
				model.isAuthOnline = isRunning;

				if(checkBoxRestartAuth.Checked) {
					if(!model.isAuthOnline) {
						model.startProcess(aPath, checkBoxHideAuth.Checked);
					}
				}
			}

			if(!string.IsNullOrEmpty(wPath)) {
				isRunning = model.isProcessRunning(wPath);
				toggleServerState(isRunning, buttonWorldServer, pictureBoxWorld);
				model.isWorldOnline = isRunning;

				if(checkBoxRestartWorld.Checked) {
					if(!model.isWorldOnline) {
						model.startProcess(wPath, checkBoxHideWorld.Checked);
					}
				}
			}

		}
	}
}

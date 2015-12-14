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

namespace Manti.FormTools
{
    public partial class FormControlPanel : Form
    {
        public FormControlPanel()
        {
            InitializeComponent();
        }

        private bool WorldOnline = false;
        private bool AuthOnline = false;

        private void FormControlPanel_Load(object sender, EventArgs e)
        {
            buttonWorldServer.FlatAppearance.BorderSize = 0;
            buttonAuthServer.FlatAppearance.BorderSize = 0;
            buttonWorldServer.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            buttonAuthServer.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void ChangePath(bool world)
        {
            if (textBoxPathServer.Visible == true)
            {
                if (textBoxPathServer.Text.Trim() != "")
                {
                    if (world == true)
                    {
                        Properties.Settings.Default.PathWorldserver = textBoxPathServer.Text.Trim();
                    } else
                    {
                        Properties.Settings.Default.PathAuthserver = textBoxPathServer.Text.Trim();
                    }

                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Upgrade();
                }

                textBoxPathServer.Text = "";
                buttonPathDialog.Visible = false;
                textBoxPathServer.Visible = false;
            } else
            {
                textBoxPathServer.Text = (world == true) ? Properties.Settings.Default.PathWorldserver : Properties.Settings.Default.PathAuthserver;
                buttonPathDialog.Visible = true;
                textBoxPathServer.Visible = true;
            }
        }

        private bool StartProcess(string path, bool hide)
        {
            if (File.Exists(path) && path.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase))
            {
                var process = new System.Diagnostics.Process();
                string directPath = Path.GetDirectoryName(path);

                process.StartInfo.FileName = path;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = directPath;

                if (hide == true)
                {
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;
                }

                try
                {
                    process.Start();
                    return true;
                } catch
                {
                    return false;
                    throw;
                }
            } else
            {
                MessageBox.Show("The selected .exe can not been found.\n Do you mind selecting a new one?", "Error: Unknown Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CheckOnline(string path)
        {
            string fileName = Path.GetFileName(path);
            fileName = fileName.Substring(0, fileName.Length - 4);

            Process[] pName = Process.GetProcessesByName(fileName);

            if (pName.Length > 0)
            {
                return true;
            }

            return false;
        }

        private void CloseProcess(string path)
        {
            string fileName = Path.GetFileName(path);
            fileName = fileName.Substring(0, fileName.Length - 4);
            foreach (var process in Process.GetProcessesByName(fileName))
            {
                process.Kill();
            }
        }

        private void buttonPathDialog_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxPathServer.Text = ofd.FileName;
            }
        }
        #region StartButtons

        private void buttonWorldServer_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Properties.Settings.Default.PathWorldserver, "World Path");

            var pathWorld = Properties.Settings.Default.PathWorldserver;

            if (WorldOnline == false)
            {
                if (StartProcess(pathWorld, checkBoxHideWorld.Checked))
                {
                    WorldOnline = true;
                    timerCheckProcess.Enabled = true;
                    labelWorldStatus.Text = "Status: ONLINE";
                    buttonWorldServer.BackgroundImage = Manti.Properties.Resources.iconStopButton;
                }
            }
            else
            {
                WorldOnline = false;
                timerCheckProcess.Enabled = (AuthOnline) ? true : false;
                labelWorldStatus.Text = "Status: OFFLINE";
                CloseProcess(pathWorld);
                buttonWorldServer.BackgroundImage = Manti.Properties.Resources.iconPlayButton;
            }

        }

        private void buttonAuthServer_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Properties.Settings.Default.PathAuthserver, "Auth Path");

            var pathAuth = Properties.Settings.Default.PathAuthserver;

            if (AuthOnline == false)
            {
                if (StartProcess(pathAuth, checkBoxHideAuth.Checked))
                {
                    AuthOnline = true;
                    timerCheckProcess.Enabled = true;
                    labelAuthStatus.Text = "Status: ONLINE";
                    buttonAuthServer.BackgroundImage = Manti.Properties.Resources.iconStopButton;
                }
            } else
            {
                AuthOnline = false;
                timerCheckProcess.Enabled = (WorldOnline) ? true : false;
                labelAuthStatus.Text = "Status: OFFLINE";
                CloseProcess(pathAuth);
                buttonAuthServer.BackgroundImage = Manti.Properties.Resources.iconPlayButton;
            }
        }

        #endregion
        #region Paths

        private void buttonWorldPath_Click(object sender, EventArgs e)
        {
            ChangePath(true);
        }
        private void buttonAuthPath_Click(object sender, EventArgs e)
        {
            ChangePath(false);
        }

        #endregion

        private void timerCheckProcess_Tick(object sender, EventArgs e)
        {
            WorldOnline = CheckOnline(Properties.Settings.Default.PathWorldserver);
            AuthOnline = CheckOnline(Properties.Settings.Default.PathAuthserver);

            labelWorldStatus.Text = (WorldOnline) ? "Status: ONLINE" : "Status: OFFLINE";
            labelAuthStatus.Text = (AuthOnline) ? "Status: ONLINE" : "Status: OFFLINE";

            buttonWorldServer.BackgroundImage = (WorldOnline) ? Manti.Properties.Resources.iconStopButton : Manti.Properties.Resources.iconPlayButton;
            buttonAuthServer.BackgroundImage = (AuthOnline) ? Manti.Properties.Resources.iconStopButton : Manti.Properties.Resources.iconPlayButton;

            if (checkBoxRestartWorld.Checked)
            {
                if (!WorldOnline)
                {
                    StartProcess(Properties.Settings.Default.PathWorldserver, checkBoxHideWorld.Checked);
                }
            }

            if (checkBoxRestartAuth.Checked)
            {
                if (!AuthOnline)
                {
                    StartProcess(Properties.Settings.Default.PathAuthserver, checkBoxHideAuth.Checked);
                }
            }

            if (!checkBoxRestartWorld.Checked && !checkBoxRestartAuth.Checked)
            {
                if (!WorldOnline && !AuthOnline)
                {
                    timerCheckProcess.Enabled = false;
                }
            }

        }
    }
}

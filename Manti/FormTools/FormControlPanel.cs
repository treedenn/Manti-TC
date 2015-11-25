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

        private string WorldConsole = "";
        private string AuthConsole = "";

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
                textBoxPathServer.Visible = false;
            } else
            {
                textBoxPathServer.Text = (world == true) ? Properties.Settings.Default.PathWorldserver : Properties.Settings.Default.PathAuthserver;
                textBoxPathServer.Visible = true;
            }
        }

        private string StartProcess(string path, bool hide)
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

                process.Start();
            } else
            {
                MessageBox.Show("The selected .exe can not been found.\n Do you mind selecting a new one?", "Error: Unknown Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return "";
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

        #region StartButtons

        private void buttonWorldServer_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Properties.Settings.Default.PathWorldserver, "World Path");

            var pathWorld = Properties.Settings.Default.PathWorldserver;

            if (WorldOnline == false)
            {
                WorldOnline = true;
                labelWorldStatus.Text = "Status: ONLINE";
                StartProcess(pathWorld, checkBoxHideWorld.Checked);
            }
            else
            {
                WorldOnline = false;
                labelWorldStatus.Text = "Status: OFFLINE";
                CloseProcess(pathWorld);
            }

        }

        private void buttonAuthServer_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Properties.Settings.Default.PathAuthserver, "Auth Path");

            var pathAuth = Properties.Settings.Default.PathAuthserver;

            if (AuthOnline == false)
            {
                AuthOnline = true;
                labelAuthStatus.Text = "Status: ONLINE";
                StartProcess(pathAuth, checkBoxHideAuth.Checked);
            } else
            {
                AuthOnline = false;
                labelAuthStatus.Text = "Status: OFFLINE";
                CloseProcess(pathAuth);
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

    }
}

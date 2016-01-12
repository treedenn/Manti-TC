using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Manti
{
    public partial class FormMySQL : Form
    {
        public static bool Offline = false;
        public static string Address;
        public static UInt16 Port;
        public static string Username;
        public static string Password;
        public static string DatabaseAuth;
        public static string DatabaseCharacters;
        public static string DatabaseWorld;

        public FormMySQL()
        {
            InitializeComponent();
        }

        #region Functions
        private void SaveSettings()
        {
            if (checkBoxSaveInformation.Checked == true)
            {
                Properties.Settings.Default.Address             = textBoxAddress.Text;
                Properties.Settings.Default.Username            = textBoxUsername.Text;
                Properties.Settings.Default.Port                = textBoxPort.Text;
                Properties.Settings.Default.DatabaseAuth        = textBoxAuth.Text;
                Properties.Settings.Default.DatabaseCharacters  = textBoxCharacters.Text;
                Properties.Settings.Default.DatabaseWorld       = textBoxWorld.Text;
                Properties.Settings.Default.SaveInformation     = checkBoxSaveInformation.Checked;
            }

            if (checkBoxSavePassword.Checked == true)
            {
                Properties.Settings.Default.Password = textBoxPassword.Text;
                Properties.Settings.Default.SavePassword = checkBoxSavePassword.Checked;
            }

            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }
        #endregion

        private void FormMySQL_Load(object sender, EventArgs e)
        {
            textBoxAddress.Text     = Properties.Settings.Default.Address;
            textBoxUsername.Text    = Properties.Settings.Default.Username;
            textBoxPassword.Text    = Properties.Settings.Default.Password;
            textBoxPort.Text        = Properties.Settings.Default.Port;
            textBoxAuth.Text        = Properties.Settings.Default.DatabaseAuth;
            textBoxCharacters.Text  = Properties.Settings.Default.DatabaseCharacters;
            textBoxWorld.Text       = Properties.Settings.Default.DatabaseWorld;

            checkBoxSaveInformation.Checked = Properties.Settings.Default.SaveInformation;
            checkBoxSavePassword.Checked    = Properties.Settings.Default.SavePassword;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            bool succesConnection = true;
            var builder = new MySqlConnectionStringBuilder();

            builder.Server = textBoxAddress.Text.Trim();
            builder.UserID = textBoxUsername.Text.Trim();
            builder.Password = textBoxPassword.Text.Trim();
            builder.Port = Convert.ToUInt16(textBoxPort.Text);

            builder.Database = textBoxAuth.Text.Trim();
            if (!ConnectDB(builder.ToString()))
            {
                succesConnection = false;
            }

            builder.Database = textBoxCharacters.Text.Trim();
            if (!ConnectDB(builder.ToString()))
            {
                succesConnection = false;
            }

            builder.Database = textBoxWorld.Text.Trim();
            if (!ConnectDB(builder.ToString()))
            {
                succesConnection = false;
            }

            if (succesConnection)
            {
                SaveSettings();

                Address = textBoxAddress.Text;
                Port = Convert.ToUInt16(textBoxPort.Text);
                Username = textBoxUsername.Text;
                Password = textBoxPassword.Text;
                DatabaseAuth = textBoxAuth.Text;
                DatabaseCharacters = textBoxCharacters.Text;
                DatabaseWorld = textBoxWorld.Text;

                this.Hide();
                var mf = new FormMain();
                mf.FormClosed += (s, args) => this.Close();
                mf.Show();
            }
        }

        private bool ConnectDB(string dataString)
        {
            bool isConnected = false;

            var connect = new MySqlConnection(dataString);

            try
            {
                connect.Open();
                isConnected = true;
                connect.Close();
            }
            catch (MySqlException)
            {
                isConnected = false;
                throw;
            }

            return isConnected;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxAddress.Clear();
            textBoxUsername.Clear();
            textBoxPassword.Clear();
            textBoxPort.Clear();

            textBoxAuth.Clear();
            textBoxCharacters.Clear();
            textBoxWorld.Clear();
        }

        private void buttonOffline_Click(object sender, EventArgs e)
        {
            Offline = true;
            SaveSettings();

            this.Hide();
            var mf = new FormMain();
            mf.FormClosed += (s, args) => this.Close();
            mf.Show();
        }
    }
}

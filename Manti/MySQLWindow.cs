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

namespace Tricore
{
    public partial class MySQLWindow : Form
    {
        public MySQLWindow()
        {
            InitializeComponent();
        }

        public static string Andress;
        public static UInt16 Port;
        public static string Username;
        public static string Password;
        public static string DatabaseAuth;
        public static string DatabaseCharacters;
        public static string DatabaseWorld;

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            bool succesConnection = true;
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

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
                Andress = textBoxAddress.Text;
                Port = Convert.ToUInt16(textBoxPort.Text);
                Username = textBoxUsername.Text;
                Password = textBoxPassword.Text;
                DatabaseAuth = textBoxAuth.Text;
                DatabaseCharacters = textBoxCharacters.Text;

                //MessageBox.Show("MySQL Connection Established.", "MySQL", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.Hide();
                MainWindow mf = new MainWindow();
                mf.FormClosed += (s, args) => this.Close();
                mf.Show();
            }
        }

        private bool ConnectDB(string dataString)
        {
            bool isConnected = false;

            MySqlConnection connect = new MySqlConnection(dataString);

            try
            {
                connect.Open();
                isConnected = true;
                connect.Close();
            }
            catch (MySqlException ex)
            {
                isConnected = false;
                MessageBox.Show(ex.Message, "MySQL Error: " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isConnected;
        }

        private void buttonTestConnect_Click(object sender, EventArgs e)
        {
            textBoxAddress.Clear();
            textBoxUsername.Clear();
            textBoxPassword.Clear() ;
            textBoxPort.Clear();

            textBoxAuth.Clear();
            textBoxCharacters.Clear();
            textBoxWorld.Clear();
        }
    }
}

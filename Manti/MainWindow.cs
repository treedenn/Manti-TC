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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            tabControlCategory.SelectedTab = tabPageCharacter;
        }

        /*
            CUSTOM FUNCTIONS
        */

        private static string ConnectionString(string database)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

            builder.Server = MySQLWindow.Andress;
            builder.UserID = MySQLWindow.Username;
            builder.Password = MySQLWindow.Password;
            builder.Port = MySQLWindow.Port;
            builder.Database = database;

            return builder.ToString();
        }

        private bool ConnectionOpen(MySqlConnection connect)
        {
            try
            {
                connect.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL Error: " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private bool ConnectionClose(MySqlConnection connect)
        {
            try
            {
                connect.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL Error: " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private DataSet DatabaseSearch(MySqlConnection connect, string sqlQuery)
        {
            DataSet ds = new DataSet();
            
            if (connect.State == ConnectionState.Open)
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, connect);

                da.Fill(ds);
            }

            return ds;
        }

        private int DatabaseUpdate(MySqlConnection connect, string sqlQuery)
        {
            if (connect.State == ConnectionState.Open)
            {
                MySqlCommand query = new MySqlCommand(sqlQuery, connect);

                return query.ExecuteNonQuery();
            }

            return 0;
        }

        private void DatabaseAccountSearch(string accountID)
        {
            MySqlConnection connect = new MySqlConnection(ConnectionString(MySQLWindow.DatabaseAuth));

            if (ConnectionOpen(connect))
            {
                string accountQuery =
                "SELECT id, username, email, reg_mail, joindate, last_ip, locked, online, expansion " +
                "FROM account WHERE id='" + accountID.ToString() + "';";

                string banQuery =
                    "SELECT bandate, unbandate, banreason, bannedby, active " +
                    "FROM account_banned WHERE id='" + accountID.ToString() + "';";

                string muteQuery =
                    "SELECT mutedate, mutetime, mutereason, mutedby " +
                    "FROM account_muted WHERE guid='" + accountID.ToString() + "';";

                string accessQuery =
                    "SELECT gmlevel, RealmID " +
                    "FROM account_access WHERE id='" + accountID.ToString() + "';";

                string finalQuery = accountQuery + banQuery + muteQuery + accessQuery;

                DataSet AccountTable = DatabaseSearch(connect, finalQuery);

                if (AccountTable.Tables[0].Rows.Count != 0)
                {
                    textBoxAccountAccountID.Text = AccountTable.Tables[0].Rows[0][0].ToString();
                    textBoxAccountAccountUsername.Text = AccountTable.Tables[0].Rows[0][1].ToString();
                    textBoxAccountAccountEmail.Text = AccountTable.Tables[0].Rows[0][2].ToString();
                    textBoxAccountAccountRegmail.Text = AccountTable.Tables[0].Rows[0][3].ToString();
                    textBoxAccountAccountJoindate.Text = AccountTable.Tables[0].Rows[0][4].ToString();
                    textBoxAccountAccountLastIP.Text = AccountTable.Tables[0].Rows[0][5].ToString();
                    checkBoxAccountAccountLocked.Checked = Convert.ToBoolean(Convert.ToInt16(AccountTable.Tables[0].Rows[0][6]));
                    checkBoxAccountAccountOnline.Checked = Convert.ToBoolean(Convert.ToInt16(AccountTable.Tables[0].Rows[0][7]));
                    textBoxAccountAccountExpansion.Text = AccountTable.Tables[0].Rows[0][8].ToString();
                }

                if (AccountTable.Tables[1].Rows.Count != 0)
                {
                    textBoxAccountAccountBandate.Text = UnixStampToDateTime(Convert.ToDouble(AccountTable.Tables[1].Rows[0][0])).ToString();
                    textBoxAccountAccountUnbandate.Text = UnixStampToDateTime(Convert.ToDouble(AccountTable.Tables[1].Rows[0][1])).ToString();
                    textBoxAccountAccountBanreason.Text = AccountTable.Tables[1].Rows[0][2].ToString();
                    textBoxAccountAccountBannedby.Text = AccountTable.Tables[1].Rows[0][3].ToString();
                    checkBoxAccountAccountBanActive.Checked = Convert.ToBoolean(Convert.ToInt16(AccountTable.Tables[1].Rows[0][4]));
                }

                if (AccountTable.Tables[2].Rows.Count != 0)
                {
                    textBoxAccountAccountMutedate.Text = UnixStampToDateTime(Convert.ToDouble(AccountTable.Tables[2].Rows[0][0])).ToString();
                    textBoxAccountAccountMutetime.Text = AccountTable.Tables[2].Rows[0][1].ToString();
                    textBoxAccountAccountMutereason.Text = AccountTable.Tables[2].Rows[0][2].ToString();
                    textBoxAccountAccountMutedby.Text = AccountTable.Tables[2].Rows[0][3].ToString();
                }

                if (AccountTable.Tables[3].Rows.Count != 0)
                {
                    dataGridViewAccountAccountAccess.DataSource = AccountTable.Tables[3];
                }


                tabControlCategoryAccount.SelectedTab = tabPageAccountAccount;
                ConnectionClose(connect);
            }
        }

        private static DateTime UnixStampToDateTime(double unixStamp)
        {
            DateTime DateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime = DateTime.AddSeconds(unixStamp).ToLocalTime();

            return DateTime;
        }

        private static double DateTimeToUnixStamp(DateTime dateTime)
        {
            return ( TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        // CUSTOM FUNCTIONS ENDS

        /*
            ---------------------------------------------------------------
                                ACCOUNT SECTION
            ---------------------------------------------------------------
        */

        private void buttonAccountSearchButton_Click(object sender, EventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(ConnectionString(MySQLWindow.DatabaseAuth));

            if (ConnectionOpen(connect))
            {
                string searchQuery = "SELECT id, username, email, expansion " +
                    "FROM account WHERE id = '" + textBoxAccountSearchID.Text.Trim() + "' OR username = '" + textBoxAccountSearchUsername.Text.Trim() + "'";

                DataSet AccountSearch = DatabaseSearch(connect, searchQuery.Trim());
                dataGridViewAccountSearchSearch.DataSource = AccountSearch.Tables[0];
                toolStripStatusLabelAccountSearchResult.Text = "Account(s) found: " + AccountSearch.Tables[0].Rows.Count.ToString();
            }

            ConnectionClose(connect);
        }

        private void dataGridViewAccountSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DatabaseAccountSearch(dataGridViewAccountSearchSearch.SelectedCells[0].Value.ToString());
        }

        private void buttonAccountAccountGenerateScript_Click(object sender, EventArgs e)
        {
            textBoxAccountScript.Clear();

            textBoxAccountScript.Text += "UPDATE account " +
                "SET id = " + textBoxAccountAccountID.Text.Trim() + ", username = '" + textBoxAccountAccountUsername.Text.Trim() + "', " +
                "email = '" + textBoxAccountAccountEmail.Text.Trim() + "', reg_mail = '" + textBoxAccountAccountRegmail.Text.Trim() + "', " +
                "last_ip = '" + textBoxAccountAccountLastIP.Text.Trim() + "', " +
                "locked = " + checkBoxAccountAccountLocked.Checked + ", online = " + checkBoxAccountAccountOnline.Checked + ", " +
                "expansion = " + textBoxAccountAccountExpansion.Text.Trim() + ", mutetime = '" + textBoxAccountAccountMutetime.Text.Trim() + "', " +
                "mutereason = '" + textBoxAccountAccountMutereason.Text.Trim() + "', muteby = '" + textBoxAccountAccountMutedby.Text.Trim() + "' " +
                "WHERE id = " + textBoxAccountAccountID.Text.Trim() + ";";

            tabControlCategoryAccount.SelectedTab = tabPageAccountScript;
        }

        private void buttonAccountScriptUpdate_Click(object sender, EventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(ConnectionString(MySQLWindow.DatabaseAuth));
            
            if (ConnectionOpen(connect))
            {
                int rows = DatabaseUpdate(connect, textBoxAccountScript.Text.Trim());
                toolStripStatusLabelAccountScriptResult.Text = "Row(s) affected: " + rows.ToString();
                ConnectionClose(connect);
            }
            
        }

        /*
            ---------------------------------------------------------------
                               CHARACTER SECTION
            ---------------------------------------------------------------
        */

        private void buttonCharacterSearchSearch_Click(object sender, EventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(ConnectionString(MySQLWindow.DatabaseCharacters));

            if (ConnectionOpen(connect))
            {
                string searchQuery = "SELECT guid, account, name, race, class, level " +
                    "FROM characters WHERE guid = '" + textBoxCharacterSearchID.Text.Trim() +
                    "' OR account = '" + textBoxCharacterSearchAccount.Text.Trim() +
                    "' OR name = '" + textBoxCharacterSearchUsername.Text.Trim() + "';";

                DataSet CharacterSearch = DatabaseSearch(connect, searchQuery.Trim());
                dataGridViewCharacterSearchSearch.DataSource = CharacterSearch.Tables[0];
                toolStripStatusLabelCharacterSearchResult.Text = "Character(s) found: " + CharacterSearch.Tables[0].Rows.Count.ToString();

                ConnectionClose(connect);
            }
        }
    }
}

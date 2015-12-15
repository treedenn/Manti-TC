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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();   
        }

        // MUST READ
        //  Hello viewer. This project contains a lot of code and functions.
        //      For a better overview (Visual Studio) do the command: CTRL + M + O.
        //          I know, it's a great feature!
        // Official SourceCode: https://github.com/Heitx/Manti-TC

        #region GlobalEvents

        private void FormMain_Load(object sender, EventArgs e)
        {
            tabControlCategory.Focus();


            dataGridViewCharacterInventory.AutoGenerateColumns = false;

            dataGridViewItemLoot.AutoGenerateColumns = false;
            dataGridViewItemProspect.AutoGenerateColumns = false;
            dataGridViewItemMill.AutoGenerateColumns = false;
            dataGridViewItemDE.AutoGenerateColumns = false;

            if (FormMySQL.Offline == true)
            {
                tabControlCategory.Enabled = false;
            }

            var textboxToolTip = new ToolTip();
            
            textboxToolTip.InitialDelay = 100;
            textboxToolTip.ShowAlways = true;

            textboxToolTip.SetToolTip(textBoxAccountSearchID, "Account Identifier.");

        }
        private void tabControlCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (tabControlCategory.SelectedTab == tabPageAccount) // Account Tab
                {
                    if (tabControlCategoryAccount.SelectedTab == tabPageAccountSearch)
                    {
                        buttonAccountSearchSearch_Click(this, new EventArgs());
                    }
                } else if (tabControlCategory.SelectedTab == tabPageCharacter) // Character Tab
                {
                    if (tabControlCategoryCharacter.SelectedTab == tabPageCharacterSearch)
                    {
                        buttonCharacterSearchSearch_Click(this, new EventArgs());
                    }
                } else if (tabControlCategory.SelectedTab == tabPageCreature) // Creature Tab
                {
                    if (tabControlCategoryCreature.SelectedTab == tabPageCreatureSearch)
                    {
                        buttonCreatureSearchSearch_Click(this, new EventArgs());
                    }
                } else if (tabControlCategory.SelectedTab == tabPageQuest) // Quest Tab
                {
                    if (tabControlCategoryQuest.SelectedTab == tabPageQuestSearch)
                    {
                        buttonQuestSearchSearch_Click(this, new EventArgs());
                    }
                } else if (tabControlCategory.SelectedTab == tabPageGameObject) // Game Object Tab
                {
                    if (tabControlCategoryGameObject.SelectedTab == tabPageGameObjectSearch)
                    {
                        buttonGameObjectSearchSearch_Click(this, new EventArgs());
                    }
                } else if (tabControlCategory.SelectedTab == tabPageItem) // Item Tab
                {
                    if (tabControlCategoryItem.SelectedTab == tabPageItemSearch)
                    {
                        buttonItemSearchSearch_Click(this, new EventArgs());
                    }
                }
            }
        }

        #region ToolStrip
        private void newConnectionToolStripMenuItemFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Application.Exit();
        }
        private void aboutToolStripMenuHelp_Click(object sender, EventArgs e)
        {
            var fa = new FormAbout();
            fa.ShowDialog();
        }
        private void controlPanelToolStripMenuTools_Click(object sender, EventArgs e)
        {
            Form CP = new FormTools.FormControlPanel();
            
            CP.StartPosition = FormStartPosition.CenterScreen;
            CP.Show();
        }
        #endregion

        #endregion
        #region CustomFunctions

        #region GlobalFunctions
            // Reads a CSV file (ID and Name are columns in the CSV file, separated with a comma).
        private DataTable ReadExcelCSV(string csvName, int ID, int value)
        {
            var reader = new System.IO.StreamReader(@".\CSV\" + csvName + ".dbc.csv");
            var forgetFirst = true;

            var dataColumn = new DataTable();

            if (reader != null)
            {
                dataColumn.Columns.Add("id", typeof(string));
                dataColumn.Columns.Add("value", typeof(string));

                string line; string[] words;

                while ((line = reader.ReadLine()) != null)
                {
                    words = line.Split(';');

                    if (forgetFirst == false)
                    {
                        if (words.Length > value && words[value] != null)
                        {
                            dataColumn.Rows.Add(words[ID].Trim('"'), words[value].Trim('"'));
                        }
                    }

                    forgetFirst = false;
                }

                reader.Close();
            } else
            {
                MessageBox.Show(csvName + " Could not been found in the CSV folder.\n It has to be same location as the program.", "File Directory : CSV ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataColumn;
        }
            // It checks for the char '%' in frot or/and end of a string.
        private string DatabaseQueryFilter(string value, string query)
        {
            if (value != "")
            {
                if (value.Trim().StartsWith("%", StringComparison.InvariantCultureIgnoreCase) || value.Trim().EndsWith("%", StringComparison.InvariantCultureIgnoreCase))
                {
                    value = " AND " + query + " LIKE '" + value + "'";
                }
                else
                {
                    value = " AND " + query + " = '" + value + "'";
                }
            }

            return value;
        }
            // Check if all textboxes are empty.
        private bool CheckEmptyControls(Control control)
        {
            foreach (Control ct in control.Controls)
            {
                if (ct is TextBox || ct is ComboBox)
                {
                    if (ct.Text != "")
                    {
                        return false;
                    }
                }
            }

            return true;
        }
            // Convert datatable columns to string columns
        private DataTable ConvertColumnsToString(DataTable datatable)
        {
            var newTable = datatable.Clone();

            for (var i = 0; i < newTable.Columns.Count; i++)
            {
                if (newTable.Columns[i].DataType != typeof(string))
                {
                    newTable.Columns[i].DataType = typeof(string);
                }
            }

            foreach (DataRow row in datatable.Rows)
            {
                newTable.ImportRow(row);
            }

            return newTable;
        }
            // Create Popup : Selection
        private void CreatePopupSelection(string formTitle, DataTable data, Control textbox)
        {
            var popupDialog = new FormPopup.FormPopupSelection();
            popupDialog.setFormTitle = formTitle;
            popupDialog.changeSelection = textbox.Text.Trim();
            popupDialog.setDataTable = data;
            popupDialog.Owner = this;
            popupDialog.ShowDialog();

            this.Activate();
            textbox.Text = (popupDialog.changeSelection == "") ? textbox.Text : popupDialog.changeSelection;
        }
            // Create Popup : Checklist
        private void CreatePopupChecklist(string formTitle, DataTable data, Control textbox, bool bitMask = false)
        {
            string currentValue = textbox.Text.Trim();
            var popupDialog = new FormPopup.FormPopupCheckboxList();

            popupDialog.setFormTitle = formTitle;
            popupDialog.setDataTable = data;
            popupDialog.setValue = Convert.ToInt32((currentValue == "") ? "0" : currentValue);
            popupDialog.setBitMask = bitMask;
            popupDialog.Owner = this;
            popupDialog.ShowDialog();

            this.Activate();
            textbox.Text = currentValue = (popupDialog.getValue == "") ? currentValue : popupDialog.getValue;
        }
            // Generate SQL Loot
        private void GenerateDataColumn(string lootTable, DataGridView dataGrid, TextBox output)
        {
            string query = "DELETE FROM `" + lootTable + "` WHERE entry = '" + dataGrid.Rows[0].Cells[0].Value.ToString() + "';";

            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                if (row.Cells[0].Value.ToString() != "")
                {
                    query += Environment.NewLine + "INSERT INTO `" + lootTable + "` VALUES (";

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.OwningColumn.DataPropertyName != "name")
                        {
                            query += cell.Value.ToString() + ", ";
                        }
                    }

                    query += "0);";


                }
            }

            output.AppendText(query);
        }
            // Set All textboxes to ZERO.
        private void DefaultValuesGenerate(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child is TextBox)
                {
                    child.Text = "0";
                } else
                {
                    DefaultValuesGenerate(child);
                }
            }
        }
            // Override textboxes by a list with textbox object and the replacement value.
        private void DefaultValuesOverride(List<Tuple<TextBox, string>> exclude)
        {
            foreach (var data in exclude)
            {
                data.Item1.Text = data.Item2.ToString();
            }
        }
            // Generate SQL to delete a specific 'type', specified by entry/guid and outputs it into a textbox.
        private void GenerateDeleteSelectedRow(DataGridView gv, string sqlTable, string uniqueIndex, TextBox output)
        {
            if (gv.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow gvR in gv.SelectedRows)
                {
                    output.AppendText("DELETE FROM `" + sqlTable + "` WHERE `" + uniqueIndex + "` = '" + gvR.Cells[0].Value.ToString() + "';");
                }
            }
        }
            // Convert to DateTime.
        private DateTime UnixStampToDateTime(double unixStamp)
        {
            var DateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime = DateTime.AddSeconds(unixStamp).ToLocalTime();

            return DateTime;
        }
            // Reverse the DataTime process.
        private double DateTimeToUnixStamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(1970, 1, 1)).TotalSeconds;
        }
        #endregion
        #region DatabaseFunctions

            // Generates the string required to create a connection
        private static string DatabaseString(string database)
        {
            var builder = new MySqlConnectionStringBuilder();

            builder.Server = FormMySQL.Andress;
            builder.UserID = FormMySQL.Username;
            builder.Password = FormMySQL.Password;
            builder.Port = FormMySQL.Port;
            builder.Database = database;

            return builder.ToString();
        }
            // Tries to open the connection between the program and database.
        private bool ConnectionOpen(MySqlConnection connect)
        {
            try
            {
                connect.Open();
                return true;
            }
            catch (MySqlException)
            {
                return false;
                throw;
            }
        }
            // Tries to close the connection between the program and database.
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
            // Searching the database with a specific query, then saves in a DataSet.
        private DataSet DatabaseSearch(MySqlConnection connect, string sqlQuery)
        {
            var ds = new DataSet();
            
            if (connect.State == ConnectionState.Open)
            {
                var da = new MySqlDataAdapter(sqlQuery, connect);

                da.Fill(ds);
            }

            return ds;
        }
            // Updates the database with a specific query and returns the row affected.
        private int DatabaseUpdate(MySqlConnection connect, string sqlQuery)
        {
            if (connect.State == ConnectionState.Open)
            {
                var query = new MySqlCommand(sqlQuery, connect);

                return query.ExecuteNonQuery();
            }

            return 0;
        }
            // Gets all rows from a search, searches for items names. if itemid is false, it tries for item identifier.
        private DataTable DatabaseItemNameColumn(string table, string where, string id, int itemColumn, bool isItemID)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            var searchTable = new DataTable();

            if (ConnectionOpen(connect))
            {
                // Create the query depending on the paramenters
                string query = "SELECT * FROM " +table+ " WHERE " +where+ " = '" + id + "';";

                // Searches in mySQL.
                var datatable = DatabaseSearch(connect, query);

                // Sets all the columns to string.
                searchTable = ConvertColumnsToString(datatable.Tables[0]);

                if (searchTable.Rows.Count != 0)
                {
                    // Adds a new column to the existing one, called 'name'.
                    searchTable.Columns.Add("name", typeof(string));

                    // Loops through all rows
                    for (int i = 0; i < searchTable.Rows.Count; i++)
                    {

                        if (isItemID)
                        {
                            searchTable.Rows[i]["name"] = DatabaseItemGetName(Convert.ToUInt32(searchTable.Rows[i][itemColumn]));
                        } else
                        {
                            searchTable.Rows[i]["name"] = DatabaseItemGetName(DatabaseItemGetEntry(Convert.ToUInt32(searchTable.Rows[i][itemColumn])));
                        }

                    }
                }

                ConnectionClose(connect);
            }

            return searchTable;
        }
            // Gets the Item Entry with global item identifier.
        private uint DatabaseItemGetEntry(uint itemIdentifier)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseCharacters));

            if (ConnectionOpen(connect))
            {
                // Get the ItemID
                string instanceQuery = "SELECT itemEntry FROM item_instance WHERE guid = '" + itemIdentifier + "';";

                // Item_instance Table
                DataSet iiTable = DatabaseSearch(connect, instanceQuery);

                if (iiTable.Tables[0].Rows.Count != 0) { return Convert.ToUInt32(iiTable.Tables[0].Rows[0][0]); }
            }

            ConnectionClose(connect);
            return 0;
        }
            // Gets the item name from itemIdentifier.
        private string DatabaseItemGetName(uint itemEntry)
        {

            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            if (ConnectionOpen(connect))
            {

                // Get the ItemID
                string nameQuery = "SELECT name FROM item_template WHERE entry = '" + itemEntry + "';";

                // item_template
                DataSet itTable = DatabaseSearch(connect, nameQuery);

                return (itTable.Tables[0].Rows.Count > 0) ? itTable.Tables[0].Rows[0][0].ToString() : "";
            }

            ConnectionClose(connect);

            return "";
        }

        #endregion
        #region DataTables
        private DataTable DataItemClass()
        {
            var iClass = new DataTable();
            iClass.Columns.Add("id", typeof(string));
            iClass.Columns.Add("name", typeof(string));

            iClass.Rows.Add(0, "Consumables");
            iClass.Rows.Add(1, "Container");
            iClass.Rows.Add(2, "Weapon");
            iClass.Rows.Add(3, "Gem");
            iClass.Rows.Add(4, "Armor");
            iClass.Rows.Add(5, "Reagent");
            iClass.Rows.Add(6, "Projectile");
            iClass.Rows.Add(7, "Trade Goods");
            iClass.Rows.Add(8, "Generic (OBSOLETE)");
            iClass.Rows.Add(9, "Recipe");
            iClass.Rows.Add(10, "Money (OBSOLETE)");
            iClass.Rows.Add(11, "Quiver");
            iClass.Rows.Add(12, "Quest");
            iClass.Rows.Add(13, "Key");
            iClass.Rows.Add(14, "Permanent (OBSOLETE)");
            iClass.Rows.Add(15, "Miscellaneous");
            iClass.Rows.Add(16, "Glyph");

            return iClass;
        }
        private DataTable DataItemSubclass(string classID)
        {
            var iSubclass = new DataTable();

            iSubclass.Columns.Add("id", typeof(string));
            iSubclass.Columns.Add("name", typeof(string));

            switch (classID)
            {
                case "0": // Consumable
                    iSubclass.Rows.Add(0, "Consumbable");
                    iSubclass.Rows.Add(1, "Potion");
                    iSubclass.Rows.Add(2, "Elixir");
                    iSubclass.Rows.Add(3, "Flask");
                    iSubclass.Rows.Add(4, "Scroll");
                    iSubclass.Rows.Add(5, "Food & Drink");
                    iSubclass.Rows.Add(6, "Item Enhancement");
                    iSubclass.Rows.Add(7, "Bandage");
                    iSubclass.Rows.Add(8, "Other");
                    break;
                case "1": // Container
                    iSubclass.Rows.Add(0, "Bag");
                    iSubclass.Rows.Add(1, "Soul Bag");
                    iSubclass.Rows.Add(2, "Herb Bag");
                    iSubclass.Rows.Add(3, "Enchanting Bag");
                    iSubclass.Rows.Add(4, "Engineering Bag");
                    iSubclass.Rows.Add(5, "Gem Bag");
                    iSubclass.Rows.Add(7, "Mining Bag");
                    iSubclass.Rows.Add(8, "Inscription Bag");
                    break;
                case "2": // Weapon
                    iSubclass.Rows.Add(0, "Axe (one-hand)");
                    iSubclass.Rows.Add(1, "Axe (two-hand)");
                    iSubclass.Rows.Add(2, "Bow");
                    iSubclass.Rows.Add(3, "Gun");
                    iSubclass.Rows.Add(4, "Mace (one-hand)");
                    iSubclass.Rows.Add(5, "Mace (two-hand)");
                    iSubclass.Rows.Add(6, "Polearm");
                    iSubclass.Rows.Add(7, "Sword (one-hand)");
                    iSubclass.Rows.Add(8, "Sword (two-hand)");
                    iSubclass.Rows.Add(9, "Obsolete");
                    iSubclass.Rows.Add(10, "Staff");
                    iSubclass.Rows.Add(11, "Exotic");
                    iSubclass.Rows.Add(12, "Exotic");
                    iSubclass.Rows.Add(13, "Fist Weapon");
                    iSubclass.Rows.Add(14, "Miscellaneous");
                    iSubclass.Rows.Add(15, "Dagger");
                    iSubclass.Rows.Add(16, "Thrown");
                    iSubclass.Rows.Add(17, "Spear");
                    iSubclass.Rows.Add(18, "Crossbow");
                    iSubclass.Rows.Add(19, "Wand");
                    iSubclass.Rows.Add(20, "Fishing Pole");
                    break;
                case "3": // Gem
                    iSubclass.Rows.Add(0, "Red");
                    iSubclass.Rows.Add(1, "Blue");
                    iSubclass.Rows.Add(2, "Yellow");
                    iSubclass.Rows.Add(3, "Purple");
                    iSubclass.Rows.Add(4, "Green");
                    iSubclass.Rows.Add(5, "Orange");
                    iSubclass.Rows.Add(7, "Meta");
                    iSubclass.Rows.Add(8, "Simple");
                    iSubclass.Rows.Add(9, "Prismatic");
                    break;
                case "4": // Armor
                    iSubclass.Rows.Add(0, "Miscellaneous");
                    iSubclass.Rows.Add(1, "Cloth");
                    iSubclass.Rows.Add(2, "Leather");
                    iSubclass.Rows.Add(3, "Mail");
                    iSubclass.Rows.Add(4, "Plate");
                    iSubclass.Rows.Add(5, "Buckler (OBSOLETE)");
                    iSubclass.Rows.Add(6, "Shield");
                    iSubclass.Rows.Add(7, "Libram");
                    iSubclass.Rows.Add(8, "Idol");
                    iSubclass.Rows.Add(9, "Totel");
                    iSubclass.Rows.Add(10, "Sigil");
                    break;
                case "5": // Reagent
                    iSubclass.Rows.Add(0, "Reagent");
                    break;
                case "6": // Projectile
                    iSubclass.Rows.Add(0, "Wand (OBSOLETE)");
                    iSubclass.Rows.Add(1, "Bolt (OBSOLETE)");
                    iSubclass.Rows.Add(2, "Arrow");
                    iSubclass.Rows.Add(3, "Bullet");
                    iSubclass.Rows.Add(4, "Thrown (OBSOLETE)");
                    break;
                case "7": // Trade Goods
                    iSubclass.Rows.Add(0, "Trade Goods");
                    iSubclass.Rows.Add(1, "Parts");
                    iSubclass.Rows.Add(2, "Explosives");
                    iSubclass.Rows.Add(3, "Devices");
                    iSubclass.Rows.Add(4, "Jewelcrafting");
                    iSubclass.Rows.Add(5, "Cloth");
                    iSubclass.Rows.Add(6, "Leather");
                    iSubclass.Rows.Add(7, "Metal & Stone");
                    iSubclass.Rows.Add(8, "Meat");
                    iSubclass.Rows.Add(9, "Herb");
                    iSubclass.Rows.Add(10, "Elemental");
                    iSubclass.Rows.Add(11, "Other");
                    iSubclass.Rows.Add(12, "Enchanting");
                    iSubclass.Rows.Add(13, "Materials");
                    iSubclass.Rows.Add(14, "Armor Enchantment");
                    iSubclass.Rows.Add(15, "Weapon Enchantment");
                    break;
                case "8": // Generic (OBSOLETE)
                    iSubclass.Rows.Add(0, "Generic (OBSOLETE)");
                    break;
                case "9": // Recipe
                    iSubclass.Rows.Add(0, "Book");
                    iSubclass.Rows.Add(1, "Leatherworking");
                    iSubclass.Rows.Add(2, "Tailoring");
                    iSubclass.Rows.Add(3, "Engineering");
                    iSubclass.Rows.Add(4, "Blacksmithing");
                    iSubclass.Rows.Add(5, "Cooking");
                    iSubclass.Rows.Add(6, "Alchemy");
                    iSubclass.Rows.Add(7, "First Aid");
                    iSubclass.Rows.Add(8, "Enchanting");
                    iSubclass.Rows.Add(9, "Fishing");
                    iSubclass.Rows.Add(10, "Jewelcrafting");
                    break;
                case "10": // Money (OBSOLETE)
                    iSubclass.Rows.Add(0, "Money");
                    break;
                case "11": // Quiver
                    iSubclass.Rows.Add(0, "Quiver (OBSOLETE)");
                    iSubclass.Rows.Add(1, "Quiver (OBSOLETE)");
                    iSubclass.Rows.Add(2, "Quiver (can hold arrows)");
                    iSubclass.Rows.Add(3, "Ammo Pouch (can hold bullets)");
                    break;
                case "12": // Quest
                    iSubclass.Rows.Add(0, "Quest");
                    break;
                case "13": // Key
                    iSubclass.Rows.Add(0, "Key");
                    iSubclass.Rows.Add(1, "Lockpick");
                    break;
                case "14": // Permanent (OBSOLETE)
                    iSubclass.Rows.Add(0, "Permanent");
                    break;
                case "15": // Miscellaneous
                    iSubclass.Rows.Add(0, "Junk");
                    iSubclass.Rows.Add(1, "Reagent");
                    iSubclass.Rows.Add(2, "Pet");
                    iSubclass.Rows.Add(3, "Holiday");
                    iSubclass.Rows.Add(4, "Other");
                    iSubclass.Rows.Add(5, "Mount");
                    break;
                case "16": // Glyph
                    iSubclass.Rows.Add(1, "Warrior");
                    iSubclass.Rows.Add(2, "Paladin");
                    iSubclass.Rows.Add(3, "Hunter");
                    iSubclass.Rows.Add(4, "Rogue");
                    iSubclass.Rows.Add(5, "Priest");
                    iSubclass.Rows.Add(6, "Death Knight");
                    iSubclass.Rows.Add(7, "Shaman");
                    iSubclass.Rows.Add(8, "Mage");
                    iSubclass.Rows.Add(9, "Warlock");
                    iSubclass.Rows.Add(11, "Druid");
                    break;
            }

                    return iSubclass;
        }
        #endregion
        #region Account

        // Searches the data for a specific account.
        private void DatabaseAccountSearch(string accountID)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseAuth));

            if (ConnectionOpen(connect))
            {
                string accountQuery =
                "SELECT id, username, email, reg_mail, joindate, last_ip, locked, online, expansion " +
                "FROM account WHERE id='" + accountID + "';";

                string banQuery =
                    "SELECT bandate, unbandate, banreason, bannedby, active " +
                    "FROM account_banned WHERE id='" + accountID + "';";

                string muteQuery =
                    "SELECT mutedate, mutetime, mutereason, mutedby " +
                    "FROM account_muted WHERE guid='" + accountID + "';";

                string accessQuery =
                    "SELECT gmlevel, RealmID " +
                    "FROM account_access WHERE id='" + accountID + "';";

                string finalQuery = accountQuery + banQuery + muteQuery + accessQuery;

                DataSet AccountTable = DatabaseSearch(connect, finalQuery);

                    // account data
                if (AccountTable.Tables[0].Rows.Count != 0)
                {
                    textBoxAccountAccountID.Text = AccountTable.Tables[0].Rows[0][0].ToString();
                    textBoxAccountAccountUsername.Text = AccountTable.Tables[0].Rows[0][1].ToString();
                    textBoxAccountAccountEmail.Text = AccountTable.Tables[0].Rows[0][2].ToString();
                    textBoxAccountAccountRegmail.Text = AccountTable.Tables[0].Rows[0][3].ToString();
                    textBoxAccountAccountJoindate.Text = AccountTable.Tables[0].Rows[0][4].ToString();
                    textBoxAccountAccountLastIP.Text = AccountTable.Tables[0].Rows[0][5].ToString();
                    checkBoxAccountAccountLocked.Checked = Convert.ToBoolean(AccountTable.Tables[0].Rows[0][6]);
                    checkBoxAccountAccountOnline.Checked = Convert.ToBoolean(AccountTable.Tables[0].Rows[0][7]);
                    textBoxAccountAccountExpansion.Text = AccountTable.Tables[0].Rows[0][8].ToString();
                }

                    // ban data
                if (AccountTable.Tables[1].Rows.Count != 0)
                {
                    textBoxAccountAccountBandate.Text = UnixStampToDateTime(Convert.ToDouble(AccountTable.Tables[1].Rows[0][0])).ToString();
                    textBoxAccountAccountUnbandate.Text = UnixStampToDateTime(Convert.ToDouble(AccountTable.Tables[1].Rows[0][1])).ToString();
                    textBoxAccountAccountBanreason.Text = AccountTable.Tables[1].Rows[0][2].ToString();
                    textBoxAccountAccountBannedby.Text = AccountTable.Tables[1].Rows[0][3].ToString();
                    checkBoxAccountAccountBanActive.Checked = Convert.ToBoolean(AccountTable.Tables[1].Rows[0][4]);
                }

                    // mute data
                if (AccountTable.Tables[2].Rows.Count != 0)
                {
                    textBoxAccountAccountMutedate.Text = UnixStampToDateTime(Convert.ToDouble(AccountTable.Tables[2].Rows[0][0])).ToString();
                    textBoxAccountAccountMutetime.Text = AccountTable.Tables[2].Rows[0][1].ToString();
                    textBoxAccountAccountMutereason.Text = AccountTable.Tables[2].Rows[0][2].ToString();
                    textBoxAccountAccountMutedby.Text = AccountTable.Tables[2].Rows[0][3].ToString();
                }

                    // acces data
                if (AccountTable.Tables[3].Rows.Count != 0)
                {
                    dataGridViewAccountAccess.DataSource = AccountTable.Tables[3];
                }
                
                ConnectionClose(connect);
            }
        }

        #endregion
        #region Character

            // Searches for data for a specific character.
        private void DatabaseCharacterSearch(string characterGUID)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseCharacters));

            if (ConnectionOpen(connect))
            {
                // Line 1 -> General Information : Line 2 -> Location : Line 3 ->  : Line 4 -> Stats : Line 5 -> Unknown.
                string characterQuery = "SELECT * FROM characters WHERE guid = '" + characterGUID + "';";

                DataSet CharacterTable = DatabaseSearch(connect, characterQuery);

                if (CharacterTable.Tables[0].Rows.Count != 0)
                {
                    // General Information
                    textBoxCharacterCharacterGUID.Text              = CharacterTable.Tables[0].Rows[0]["guid"].ToString();
                    textBoxCharacterCharacterAccount.Text           = CharacterTable.Tables[0].Rows[0]["account"].ToString();
                    textBoxCharacterCharacterName.Text              = CharacterTable.Tables[0].Rows[0]["NAME"].ToString();
                    textBoxCharacterCharacterRace.Text              = CharacterTable.Tables[0].Rows[0]["race"].ToString();
                    textBoxCharacterCharacterClass.Text             = CharacterTable.Tables[0].Rows[0]["class"].ToString();
                    textBoxCharacterCharacterGender.Text            = CharacterTable.Tables[0].Rows[0]["gender"].ToString();
                    textBoxCharacterCharacterLevel.Text             = CharacterTable.Tables[0].Rows[0]["level"].ToString();
                    textBoxCharacterCharacterMoney.Text             = CharacterTable.Tables[0].Rows[0]["money"].ToString();
                    textBoxCharacterCharacterXP.Text                = CharacterTable.Tables[0].Rows[0]["xp"].ToString();
                    textBoxCharacterCharacterTitle.Text             = CharacterTable.Tables[0].Rows[0]["chosentitle"].ToString();
                    checkBoxCharacterCharacterOnline.Checked        = Convert.ToBoolean(CharacterTable.Tables[0].Rows[0]["online"]);
                    checkBoxCharacterCharacterCinematic.Checked     = Convert.ToBoolean(CharacterTable.Tables[0].Rows[0]["cinematic"]);
                    checkBoxCharacterCharacterRest.Checked          = Convert.ToBoolean(CharacterTable.Tables[0].Rows[0]["is_logout_resting"]);
                    // Location
                    textBoxCharacterCharacterMapID.Text             = CharacterTable.Tables[0].Rows[0]["map"].ToString();
                    textBoxCharacterCharacterInstanceID.Text        = CharacterTable.Tables[0].Rows[0]["instance_id"].ToString();
                    textBoxCharacterCharacterZoneID.Text            = CharacterTable.Tables[0].Rows[0]["zone"].ToString();
                    textBoxCharacterCharacterCoordO.Text            = CharacterTable.Tables[0].Rows[0]["orientation"].ToString();
                    textBoxCharacterCharacterCoordX.Text            = CharacterTable.Tables[0].Rows[0]["position_x"].ToString();
                    textBoxCharacterCharacterCoordY.Text            = CharacterTable.Tables[0].Rows[0]["position_y"].ToString();
                    textBoxCharacterCharacterCoordZ.Text            = CharacterTable.Tables[0].Rows[0]["position_z"].ToString();
                    // Player vs Player
                    textBoxCharacterCharacterHonorPoints.Text       = CharacterTable.Tables[0].Rows[0]["totalHonorPoints"].ToString();
                    textBoxCharacterCharacterArenaPoints.Text       = CharacterTable.Tables[0].Rows[0]["arenaPoints"].ToString();
                    textBoxCharacterCharacterTotalKills.Text        = CharacterTable.Tables[0].Rows[0]["totalKills"].ToString();
                    // Stats
                    textBoxCharacterCharacterHealth.Text            = CharacterTable.Tables[0].Rows[0]["health"].ToString();
                    textBoxCharacterCharacterPower1.Text            = CharacterTable.Tables[0].Rows[0]["power1"].ToString();
                    textBoxCharacterCharacterPower2.Text            = CharacterTable.Tables[0].Rows[0]["power2"].ToString();
                    textBoxCharacterCharacterPower3.Text            = CharacterTable.Tables[0].Rows[0]["power3"].ToString();
                    textBoxCharacterCharacterPower4.Text            = CharacterTable.Tables[0].Rows[0]["power4"].ToString();
                    textBoxCharacterCharacterPower5.Text            = CharacterTable.Tables[0].Rows[0]["power5"].ToString();
                    textBoxCharacterCharacterPower6.Text            = CharacterTable.Tables[0].Rows[0]["power6"].ToString();
                    textBoxCharacterCharacterPower7.Text            = CharacterTable.Tables[0].Rows[0]["power7"].ToString();
                    // Unknown
                    textBoxCharacterCharacterEquipmentCache.Text    = CharacterTable.Tables[0].Rows[0]["equipmentCache"].ToString();
                    textBoxCharacterCharacterKnownTitles.Text       = CharacterTable.Tables[0].Rows[0]["knownTitles"].ToString();
                    textBoxCharacterCharacterExploredZones.Text     = CharacterTable.Tables[0].Rows[0]["exploredZones"].ToString();
                    textBoxCharacterCharacterTaxiMask.Text          = CharacterTable.Tables[0].Rows[0]["taximask"].ToString();
                }

                ConnectionClose(connect);
            }
        }
            // Outputs the inventory for a specific player.
        private void DatabaseCharacterInventory(string characterGUID)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseCharacters));

            if (ConnectionOpen(connect))
            {
                string inventoryQuery = "SELECT * FROM character_inventory WHERE guid = '" + characterGUID + "';";

                var datatable = DatabaseSearch(connect, inventoryQuery);

                var newTable = ConvertColumnsToString(datatable.Tables[0]);

                if (newTable.Rows.Count != 0)
                {
                    // Adds a new column 'name'
                    newTable.Columns.Add("name", typeof(string));
                    
                    // loops every inventory item for name
                    for (int i = 0; i < newTable.Rows.Count; i++)
                    {
                        // sets the column 'name' to the itemname
                        newTable.Rows[i]["name"] = DatabaseItemGetName( DatabaseItemGetEntry( Convert.ToUInt32(newTable.Rows[i][3]) ) );
                    }
                }

                dataGridViewCharacterInventory.DataSource = newTable;
            }

            ConnectionClose(connect);
        }
        private string DatabaseCharacterInventoryGenerate()
        {
            string query = "";

            if (dataGridViewCharacterInventory.Rows.Count > 0)
            {
                query = "DELETE FROM `character_inventory` WHERE guid = '" + dataGridViewCharacterInventory.Rows[0].Cells[0].Value.ToString() + "';";

                foreach (DataGridViewRow row in dataGridViewCharacterInventory.Rows)
                {
                    if (row.Cells[0].Value.ToString() != "")
                    {
                        query += Environment.NewLine;

                        query += "INSERT INTO `character_inventory` VALUES (" +
                            row.Cells[0].Value.ToString() + ", " + row.Cells[1].Value.ToString() + ", " +
                            row.Cells[2].Value.ToString() + ", " + row.Cells[3].Value.ToString() + ");";
                    }
                }

            }

            return query;
        }
        private string DatabaseCharacterCharacterGenerate()
        {
            var query = "REPLACE INTO `characters` (" +
            "`guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `money`, `xp`, `chosentitle`, `online`, `cinematic`, `is_logout_resting`, " +
            "`map`, `instance_id`, `zone`, `orientation`, `position_x`, `position_y`, `position_z`, " +
            "`totalHonorPoints`, `arenaPoints`, `totalKills`, " +
            "`health`, `power1`, `power2`, `power3`, `power4`, `power5`, `power6`, `power7`, " +
            "`equipmentCache`, `knownTitles`, `exploredZones`, `taximask`" +
            ") VALUES (" +

            textBoxCharacterCharacterGUID.Text + ", " +
            textBoxCharacterCharacterAccount.Text + ", '" +
            textBoxCharacterCharacterName.Text + "', " +
            textBoxCharacterCharacterRace.Text + ", " +
            textBoxCharacterCharacterClass.Text + ", " +
            textBoxCharacterCharacterGender.Text + ", " +
            textBoxCharacterCharacterLevel.Text + ", " +
            textBoxCharacterCharacterMoney.Text + ", " +
            textBoxCharacterCharacterXP.Text + ", " +
            textBoxCharacterCharacterTitle.Text + ", " +
            checkBoxCharacterCharacterOnline.Checked.ToString() + ", " +
            checkBoxCharacterCharacterCinematic.Checked.ToString() + ", " +
            checkBoxCharacterCharacterRest.Checked.ToString() + ", " +

            textBoxCharacterCharacterMapID.Text + ", " +
            textBoxCharacterCharacterInstanceID.Text + ", " +
            textBoxCharacterCharacterZoneID.Text + ", " +
            textBoxCharacterCharacterCoordO.Text + ", " +
            textBoxCharacterCharacterCoordX.Text + ", " +
            textBoxCharacterCharacterCoordY.Text + ", " +
            textBoxCharacterCharacterCoordZ.Text + ", " +

            textBoxCharacterCharacterHonorPoints.Text + ", " +
            textBoxCharacterCharacterArenaPoints.Text + ", " +
            textBoxCharacterCharacterTotalKills.Text + ", " +

            textBoxCharacterCharacterHealth.Text + ", " +
            textBoxCharacterCharacterPower1.Text + ", " +
            textBoxCharacterCharacterPower2.Text + ", " +
            textBoxCharacterCharacterPower3.Text + ", " +
            textBoxCharacterCharacterPower4.Text + ", " +
            textBoxCharacterCharacterPower5.Text + ", " +
            textBoxCharacterCharacterPower6.Text + ", " +
            textBoxCharacterCharacterPower7.Text + ", '" +

            textBoxCharacterCharacterEquipmentCache.Text + "', '" +
            textBoxCharacterCharacterKnownTitles.Text + "', '" +
            textBoxCharacterCharacterExploredZones.Text + "', '" +
            textBoxCharacterCharacterTaxiMask.Text + "'" +

            ");";
                
            return query;
        }

        #endregion       
        #region Creature
            // Searches the database for the creature's information.
        private void DatabaseCreatureSearch(string creatureEntryID)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            if (ConnectionOpen(connect))
            {

                string query = "SELECT * FROM creature_template WHERE entry = '" + creatureEntryID + "'; ";

                var ctTable = DatabaseSearch(connect, query);

                textBoxCreatureTemplateEntry.Text           = ctTable.Tables[0].Rows[0]["entry"].ToString();
                textBoxCreatureTemplateDifEntry1.Text       = ctTable.Tables[0].Rows[0]["difficulty_entry_1"].ToString();
                textBoxCreatureTemplateDifEntry2.Text       = ctTable.Tables[0].Rows[0]["difficulty_entry_2"].ToString();
                textBoxCreatureTemplateDifEntry3.Text       = ctTable.Tables[0].Rows[0]["difficulty_entry_3"].ToString();
                textBoxCreatureTemplateName.Text            = ctTable.Tables[0].Rows[0]["NAME"].ToString();
                textBoxCreatureTemplateSubname.Text         = ctTable.Tables[0].Rows[0]["subname"].ToString();

                textBoxCreatureTemplateModelID1.Text        = ctTable.Tables[0].Rows[0]["modelid1"].ToString();
                textBoxCreatureTemplateModelID2.Text        = ctTable.Tables[0].Rows[0]["modelid2"].ToString();
                textBoxCreatureTemplateModelID3.Text        = ctTable.Tables[0].Rows[0]["modelid3"].ToString();
                textBoxCreatureTemplateModelID4.Text        = ctTable.Tables[0].Rows[0]["modelid4"].ToString();
                textBoxCreatureTemplateLevelMin.Text        = ctTable.Tables[0].Rows[0]["minlevel"].ToString();
                textBoxCreatureTemplateLevelMax.Text        = ctTable.Tables[0].Rows[0]["maxlevel"].ToString();
                textBoxCreatureTemplateGoldMin.Text         = ctTable.Tables[0].Rows[0]["mingold"].ToString();
                textBoxCreatureTemplateGoldMax.Text         = ctTable.Tables[0].Rows[0]["maxgold"].ToString();
                textBoxCreatureTemplateKillCredit1.Text     = ctTable.Tables[0].Rows[0]["KillCredit1"].ToString();
                textBoxCreatureTemplateKillCredit2.Text     = ctTable.Tables[0].Rows[0]["KillCredit2"].ToString();
                textBoxCreatureTemplateRank.Text            = ctTable.Tables[0].Rows[0]["rank"].ToString();
                textBoxCreatureTemplateScale.Text           = ctTable.Tables[0].Rows[0]["scale"].ToString();
                textBoxCreatureTemplateFaction.Text         = ctTable.Tables[0].Rows[0]["faction"].ToString();
                textBoxCreatureTemplateNPCFlags.Text        = ctTable.Tables[0].Rows[0]["npcflag"].ToString();

                textBoxCreatureTemplateModHealth.Text       = ctTable.Tables[0].Rows[0]["HealthModifier"].ToString();
                textBoxCreatureTemplateModMana.Text         = ctTable.Tables[0].Rows[0]["ManaModifier"].ToString();
                textBoxCreatureTemplateModArmor.Text        = ctTable.Tables[0].Rows[0]["ArmorModifier"].ToString();
                textBoxCreatureTemplateModDamage.Text       = ctTable.Tables[0].Rows[0]["DamageModifier"].ToString();
                textBoxCreatureTemplateModExperience.Text   = ctTable.Tables[0].Rows[0]["ExperienceModifier"].ToString();

                textBoxCreatureTemplateBaseAttack.Text      = ctTable.Tables[0].Rows[0]["BaseAttackTime"].ToString();
                textBoxCreatureTemplateRangedAttack.Text    = ctTable.Tables[0].Rows[0]["RangeAttackTime"].ToString();
                textBoxCreatureTemplateBV.Text              = ctTable.Tables[0].Rows[0]["BaseVariance"].ToString();
                textBoxCreatureTemplateRV.Text              = ctTable.Tables[0].Rows[0]["RangeVariance"].ToString();
                textBoxCreatureTemplateDS.Text              = ctTable.Tables[0].Rows[0]["dmgschool"].ToString();

                textBoxCreatureTemplateAIName.Text          = ctTable.Tables[0].Rows[0]["AIName"].ToString();
                textBoxCreatureTemplateMType.Text           = ctTable.Tables[0].Rows[0]["MovementType"].ToString();
                textBoxCreatureTemplateInhabitType.Text     = ctTable.Tables[0].Rows[0]["InhabitType"].ToString();
                textBoxCreatureTemplateHH.Text              = ctTable.Tables[0].Rows[0]["HoverHeight"].ToString();
                textBoxCreatureTemplateGMID.Text            = ctTable.Tables[0].Rows[0]["gossip_menu_id"].ToString();
                textBoxCreatureTemplateMID.Text             = ctTable.Tables[0].Rows[0]["movementId"].ToString();
                textBoxCreatureTemplateScriptName.Text      = ctTable.Tables[0].Rows[0]["ScriptName"].ToString();
                textBoxCreatureTemplateVID.Text             = ctTable.Tables[0].Rows[0]["VehicleId"].ToString();

                textBoxCreatureTemplateTType.Text           = ctTable.Tables[0].Rows[0]["trainer_type"].ToString();
                textBoxCreatureTemplateTSpell.Text          = ctTable.Tables[0].Rows[0]["trainer_spell"].ToString();
                textBoxCreatureTemplateTRace.Text           = ctTable.Tables[0].Rows[0]["trainer_class"].ToString();
                textBoxCreatureTemplateTClass.Text          = ctTable.Tables[0].Rows[0]["trainer_race"].ToString();

                textBoxCreatureTemplateLootID.Text          = ctTable.Tables[0].Rows[0]["lootid"].ToString();
                textBoxCreatureTemplatePickID.Text          = ctTable.Tables[0].Rows[0]["pickpocketloot"].ToString();
                textBoxCreatureTemplateSkinID.Text          = ctTable.Tables[0].Rows[0]["skinloot"].ToString();

                textBoxCreatureTemplateResis1.Text          = ctTable.Tables[0].Rows[0]["resistance1"].ToString();
                textBoxCreatureTemplateResis2.Text          = ctTable.Tables[0].Rows[0]["resistance2"].ToString();
                textBoxCreatureTemplateResis3.Text          = ctTable.Tables[0].Rows[0]["resistance3"].ToString();
                textBoxCreatureTemplateResis4.Text          = ctTable.Tables[0].Rows[0]["resistance4"].ToString();
                textBoxCreatureTemplateResis5.Text          = ctTable.Tables[0].Rows[0]["resistance5"].ToString();
                textBoxCreatureTemplateResis6.Text          = ctTable.Tables[0].Rows[0]["resistance6"].ToString();

                checkBoxCreatureTemplateHR.Checked          = Convert.ToBoolean(ctTable.Tables[0].Rows[0]["RegenHealth"]);
                textBoxCreatureTemplateMechanic.Text        = ctTable.Tables[0].Rows[0]["mechanic_immune_mask"].ToString();
                textBoxCreatureTemplateFamily.Text          = ctTable.Tables[0].Rows[0]["family"].ToString();
                textBoxCreatureTemplateType.Text            = ctTable.Tables[0].Rows[0]["TYPE"].ToString();
                textBoxCreatureTemplateTypeFlags.Text       = ctTable.Tables[0].Rows[0]["type_flags"].ToString();
                textBoxCreatureTemplateFlagsExtra.Text      = ctTable.Tables[0].Rows[0]["flags_extra"].ToString();
                textBoxCreatureTemplateUnitClass.Text       = ctTable.Tables[0].Rows[0]["unit_class"].ToString();
                textBoxCreatureTemplateUnitflags.Text       = ctTable.Tables[0].Rows[0]["unit_flags"].ToString();
                textBoxCreatureTemplateUnitflags2.Text      = ctTable.Tables[0].Rows[0]["unit_flags2"].ToString();
                textBoxCreatureTemplateDynamic.Text         = ctTable.Tables[0].Rows[0]["dynamicflags"].ToString();

                textBoxCreatureTemplateSpeedWalk.Text       = ctTable.Tables[0].Rows[0]["speed_walk"].ToString();
                textBoxCreatureTemplateSpeedRun.Text        = ctTable.Tables[0].Rows[0]["speed_run"].ToString();

                textBoxCreatureTemplateSpell1.Text          = ctTable.Tables[0].Rows[0]["spell1"].ToString();
                textBoxCreatureTemplateSpell2.Text          = ctTable.Tables[0].Rows[0]["spell2"].ToString();
                textBoxCreatureTemplateSpell3.Text          = ctTable.Tables[0].Rows[0]["spell3"].ToString();
                textBoxCreatureTemplateSpell4.Text          = ctTable.Tables[0].Rows[0]["spell4"].ToString();
                textBoxCreatureTemplateSpell5.Text          = ctTable.Tables[0].Rows[0]["spell5"].ToString();
                textBoxCreatureTemplateSpell6.Text          = ctTable.Tables[0].Rows[0]["spell6"].ToString();
                textBoxCreatureTemplateSpell7.Text          = ctTable.Tables[0].Rows[0]["spell7"].ToString();
                textBoxCreatureTemplateSpell8.Text          = ctTable.Tables[0].Rows[0]["spell8"].ToString();

                ConnectionClose(connect);
            }


        }
            // Searches the database for the creature's spawnlocations
        private void DatabaseCreatureLocation(string creatureEntryID)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            if (ConnectionOpen(connect))
            {
                string query = "SELECT id, guid, map, zoneId, areaId, position_x, position_y, position_z, orientation, spawntimesecs, spawndist " +
                    "FROM creature WHERE id = '"+creatureEntryID+"';";

                // CreatureTable
                DataSet ctTable = DatabaseSearch(connect, query);

                dataGridViewCreatureLocation.DataSource = ctTable.Tables[0];

                ConnectionClose(connect);
            }
        }
            // Template Generation
        private string DatabaseCreatureTempGenerate()
        {
            var query = "REPLACE INTO `creature_template` (" +
            "`entry`, `difficulty_entry_1`, `difficulty_entry_2`, `difficulty_entry_3`, `NAME`, `subname`, " +
            "`modelid1`, `modelid2`, `modelid3`, `modelid4`, `minlevel`, `maxlevel`, `mingold`, `maxgold`, `KillCredit1`, `KillCredit2`, `rank`, `scale`, `faction`, `npcflag`, " +
            "`HealthModifier`, `ManaModifier`, `ArmorModifier`, `DamageModifier`, `ExperienceModifier`, " +
            "`BaseAttackTime`, `RangeAttackTime`, `BaseVariance`, `RangeVariance`, `dmgschool`, " +
            "`AIName`, `MovementType`, `InhabitType`, `HoverHeight`, `gossip_menu_id`, `movementId`, `ScriptName`, `VehicleId`, " +
            "`trainer_type`, `trainer_spell`, `trainer_class`, `trainer_race`, `lootid`, `pickpocketloot`, `skinloot`, " +
            "`resistance1`, `resistance2`, `resistance3`, `resistance4`, `resistance5`, `resistance6`, " +
            "`RegenHealth`, `mechanic_immune_mask`, `family`, `TYPE`, `type_flags`, `flags_extra`, `unit_class`, `unit_flags`, `unit_flags2`, `dynamicflags`, `speed_walk`, `speed_run`, " +
            "`spell1`, `spell2`, `spell3`, `spell4`, `spell5`, `spell6`, `spell7`, `spell8`" +
            ") VALUES (" +

            textBoxCreatureTemplateEntry.Text.Trim() + ", " +
            textBoxCreatureTemplateDifEntry1.Text.Trim() + ", " +
            textBoxCreatureTemplateDifEntry2.Text.Trim() + ", " +
            textBoxCreatureTemplateDifEntry3.Text.Trim() + ", '" +
            textBoxCreatureTemplateName.Text.Trim() + "', '" +
            textBoxCreatureTemplateSubname.Text.Trim() + "', " +

            textBoxCreatureTemplateModelID1.Text.Trim() + ", " +
            textBoxCreatureTemplateModelID2.Text.Trim() + ", " +
            textBoxCreatureTemplateModelID3.Text.Trim() + ", " +
            textBoxCreatureTemplateModelID4.Text.Trim() + ", " +
            textBoxCreatureTemplateLevelMin.Text.Trim() + ", " +
            textBoxCreatureTemplateLevelMax.Text.Trim() + ", " +
            textBoxCreatureTemplateGoldMin.Text.Trim() + ", " +
            textBoxCreatureTemplateGoldMax.Text.Trim() + ", " +
            textBoxCreatureTemplateKillCredit1.Text.Trim() + ", " +
            textBoxCreatureTemplateKillCredit2.Text.Trim() + ", " +
            textBoxCreatureTemplateRank.Text.Trim() + ", " +
            textBoxCreatureTemplateScale.Text.Trim() + ", " +
            textBoxCreatureTemplateFaction.Text.Trim() + ", " +
            textBoxCreatureTemplateNPCFlags.Text.Trim() + ", " +

            textBoxCreatureTemplateModHealth.Text.Trim() + ", " +
            textBoxCreatureTemplateModMana.Text.Trim() + ", " +
            textBoxCreatureTemplateModArmor.Text.Trim() + ", " +
            textBoxCreatureTemplateModDamage.Text.Trim() + ", " +
            textBoxCreatureTemplateModExperience.Text.Trim() + ", " +

            textBoxCreatureTemplateBaseAttack.Text.Trim() + ", " +
            textBoxCreatureTemplateRangedAttack.Text.Trim() + ", " +
            textBoxCreatureTemplateBV.Text.Trim() + ", " +
            textBoxCreatureTemplateRV.Text.Trim() + ", " +
            textBoxCreatureTemplateDS.Text.Trim() + ", '" +

            textBoxCreatureTemplateAIName.Text.Trim() + "', " +
            textBoxCreatureTemplateMType.Text.Trim() + ", " +
            textBoxCreatureTemplateInhabitType.Text.Trim() + ", " +
            textBoxCreatureTemplateHH.Text.Trim() + ", " +
            textBoxCreatureTemplateGMID.Text.Trim() + ", " +
            textBoxCreatureTemplateMID.Text.Trim() + ", '" +
            textBoxCreatureTemplateScriptName.Text.Trim() + "', " +
            textBoxCreatureTemplateVID.Text.Trim() + ", " +

            textBoxCreatureTemplateTType.Text.Trim() + ", " +
            textBoxCreatureTemplateTSpell.Text.Trim() + ", " +
            textBoxCreatureTemplateTRace.Text.Trim() + ", " +
            textBoxCreatureTemplateTClass.Text.Trim() + ", " +

            textBoxCreatureTemplateLootID.Text.Trim() + ", " +
            textBoxCreatureTemplatePickID.Text.Trim() + ", " +
            textBoxCreatureTemplateSkinID.Text.Trim() + ", " +

            textBoxCreatureTemplateResis1.Text.Trim() + ", " +
            textBoxCreatureTemplateResis2.Text.Trim() + ", " +
            textBoxCreatureTemplateResis3.Text.Trim() + ", " +
            textBoxCreatureTemplateResis4.Text.Trim() + ", " +
            textBoxCreatureTemplateResis5.Text.Trim() + ", " +
            textBoxCreatureTemplateResis6.Text.Trim() + ", " +

            checkBoxCreatureTemplateHR.Checked.ToString() + ", " +
            textBoxCreatureTemplateMechanic.Text.Trim() + ", " +
            textBoxCreatureTemplateFamily.Text.Trim() + ", " +
            textBoxCreatureTemplateType.Text.Trim() + ", " +
            textBoxCreatureTemplateTypeFlags.Text.Trim() + ", " +
            textBoxCreatureTemplateFlagsExtra.Text.Trim() + ", " +
            textBoxCreatureTemplateUnitClass.Text.Trim() + ", " +
            textBoxCreatureTemplateUnitflags.Text.Trim() + ", " +
            textBoxCreatureTemplateUnitflags2.Text.Trim() + ", " +
            textBoxCreatureTemplateDynamic.Text.Trim() + ", " +

            textBoxCreatureTemplateSpeedWalk.Text.Trim() + ", " +
            textBoxCreatureTemplateSpeedRun.Text.Trim() + ", " +

            textBoxCreatureTemplateSpell1.Text.Trim() + ", " +
            textBoxCreatureTemplateSpell2.Text.Trim() + ", " +
            textBoxCreatureTemplateSpell3.Text.Trim() + ", " +
            textBoxCreatureTemplateSpell4.Text.Trim() + ", " +
            textBoxCreatureTemplateSpell5.Text.Trim() + ", " +
            textBoxCreatureTemplateSpell6.Text.Trim() + ", " +
            textBoxCreatureTemplateSpell7.Text.Trim() + ", " +
            textBoxCreatureTemplateSpell8.Text.Trim() + 

            ");";

            return query;
        }

        private void DatabaseCreatureVendor()
        {

        }
        #endregion
        #region Quest
        private void DatabaseQuestSearch(string questEntryID)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            if (ConnectionOpen(connect))
            {
                var query = "SELECT * FROM quest_template WHERE ID = '" + questEntryID + "';" +
                            "SELECT * FROM quest_template_addon WHERE ID = '" + questEntryID + "';";

                var qtTable = DatabaseSearch(connect, query);
                // ExclusiveGroup PrevQuestID NextQuestID MaxLevel RewardMailTemplateID
                #region Section1
                textBoxQuestSectionID.Text              = qtTable.Tables[0].Rows[0]["ID"].ToString();
                textBoxQuestSectionExclusive.Text       = qtTable.Tables[1].Rows[0]["ExclusiveGroup"].ToString();
                textBoxQuestSectionPrevQuest.Text       = qtTable.Tables[1].Rows[0]["PrevQuestID"].ToString();
                textBoxQuestSectionNextQuest.Text       = qtTable.Tables[1].Rows[0]["NextQuestID"].ToString();

                textBoxQuestSectionReqRace.Text         = qtTable.Tables[0].Rows[0]["AllowableRaces"].ToString();
                textBoxQuestSectionReqClass.Text        = qtTable.Tables[1].Rows[0]["AllowableClasses"].ToString();
                textBoxQuestSectionLevelMin.Text        = qtTable.Tables[0].Rows[0]["Minlevel"].ToString();
                textBoxQuestSectionLevelMax.Text        = qtTable.Tables[1].Rows[0]["MaxLevel"].ToString();
                textBoxQuestSectionSkillID.Text         = qtTable.Tables[1].Rows[0]["RequiredSkillID"].ToString();
                textBoxQuestSectionSkillPoints.Text     = qtTable.Tables[1].Rows[0]["RequiredSkillPoints"].ToString();
                textBoxQuestSectionFaction1.Text        = qtTable.Tables[1].Rows[0]["RequiredMinRepFaction"].ToString();
                textBoxQuestSectionFaction2.Text        = qtTable.Tables[1].Rows[0]["RequiredMaxRepFaction"].ToString();
                textBoxQuestSectionValue1.Text          = qtTable.Tables[1].Rows[0]["RequiredMinRepValue"].ToString();
                textBoxQuestSectionValue2.Text          = qtTable.Tables[1].Rows[0]["RequiredMaxRepValue"].ToString();

                textBoxQuestSectionType.Text            = qtTable.Tables[0].Rows[0]["Flags"].ToString();
                textBoxQuestSectionType.Text            = qtTable.Tables[0].Rows[0]["Flags"].ToString();
                textBoxQuestSectionTimeAllowed.Text     = qtTable.Tables[0].Rows[0]["TimeAllowed"].ToString();

                textBoxQuestSectionTitle.Text           = qtTable.Tables[0].Rows[0]["LogTitle"].ToString();
                textBoxQuestSectionLDescription.Text    = qtTable.Tables[0].Rows[0]["QuestDescription"].ToString();
                textBoxQuestSectionQDescription.Text    = qtTable.Tables[0].Rows[0]["LogDescription"].ToString();
                textBoxQuestSectionAreaDescription.Text = qtTable.Tables[0].Rows[0]["LogTitle"].ToString();

                textBoxQuestSectionCompleted.Text       = qtTable.Tables[0].Rows[0]["QuestCompletionLog"].ToString();
                textBoxQuestSectionObjectives1.Text     = qtTable.Tables[0].Rows[0]["ObjectiveText1"].ToString();
                textBoxQuestSectionObjectives2.Text     = qtTable.Tables[0].Rows[0]["ObjectiveText2"].ToString();
                textBoxQuestSectionObjectives3.Text     = qtTable.Tables[0].Rows[0]["ObjectiveText3"].ToString();
                textBoxQuestSectionObjectives4.Text     = qtTable.Tables[0].Rows[0]["ObjectiveText4"].ToString();

                // Other
                textBoxQuestSectionQuestLevel.Text      = qtTable.Tables[0].Rows[0]["QuestLevel"].ToString();
                textBoxQuestSectionOtherSP.Text         = qtTable.Tables[0].Rows[0]["SuggestedGroupNum"].ToString();
                textBoxQuestSectionOtherSF.Text         = qtTable.Tables[1].Rows[0]["SpecialFlags"].ToString();
                textBoxQuestSectionOtherPK.Text         = qtTable.Tables[0].Rows[0]["RequiredPlayerKills"].ToString();
                textBoxQuestSectionQType.Text           = qtTable.Tables[0].Rows[0]["QuestType"].ToString();
                textBoxQuestSectionQuestStartItem.Text  = qtTable.Tables[0].Rows[0]["StartItem"].ToString();

                #endregion
                #region Section2
                // Requirements
                //RewardMailTemplateID RequiredSkillID RequiredSkillPoints
                textBoxQuestSectionReqNPCID1.Text           = qtTable.Tables[0].Rows[0]["RequiredNpcOrGo1"].ToString();
                textBoxQuestSectionReqNPCC1.Text            = qtTable.Tables[0].Rows[0]["RequiredNpcOrGoCount1"].ToString();
                textBoxQuestSectionReqNPCID2.Text           = qtTable.Tables[0].Rows[0]["RequiredNpcOrGo2"].ToString();
                textBoxQuestSectionReqNPCC2.Text            = qtTable.Tables[0].Rows[0]["RequiredNpcOrGoCount2"].ToString();
                textBoxQuestSectionReqNPCID3.Text           = qtTable.Tables[0].Rows[0]["RequiredNpcOrGo3"].ToString();
                textBoxQuestSectionReqNPCC3.Text            = qtTable.Tables[0].Rows[0]["RequiredNpcOrGoCount3"].ToString();
                textBoxQuestSectionReqNPCID4.Text           = qtTable.Tables[0].Rows[0]["RequiredNpcOrGo4"].ToString();
                textBoxQuestSectionReqNPCC4.Text            = qtTable.Tables[0].Rows[0]["RequiredNpcOrGoCount4"].ToString();

                textBoxQuestSectionReqItemID1.Text          = qtTable.Tables[0].Rows[0]["RequiredItemId1"].ToString();
                textBoxQuestSectionReqItemC1.Text           = qtTable.Tables[0].Rows[0]["RequiredItemCount1"].ToString();
                textBoxQuestSectionReqItemID2.Text          = qtTable.Tables[0].Rows[0]["RequiredItemId2"].ToString();
                textBoxQuestSectionReqItemC2.Text           = qtTable.Tables[0].Rows[0]["RequiredItemCount2"].ToString();
                textBoxQuestSectionReqItemID3.Text          = qtTable.Tables[0].Rows[0]["RequiredItemId3"].ToString();
                textBoxQuestSectionReqItemC3.Text           = qtTable.Tables[0].Rows[0]["RequiredItemCount3"].ToString();
                textBoxQuestSectionReqItemID4.Text          = qtTable.Tables[0].Rows[0]["RequiredItemId4"].ToString();
                textBoxQuestSectionReqItemC4.Text           = qtTable.Tables[0].Rows[0]["RequiredItemCount4"].ToString();
                textBoxQuestSectionReqItemID5.Text          = qtTable.Tables[0].Rows[0]["RequiredItemId5"].ToString();
                textBoxQuestSectionReqItemC5.Text           = qtTable.Tables[0].Rows[0]["RequiredItemCount5"].ToString();
                textBoxQuestSectionReqItemID6.Text          = qtTable.Tables[0].Rows[0]["RequiredItemId6"].ToString();
                textBoxQuestSectionReqItemC6.Text           = qtTable.Tables[0].Rows[0]["RequiredItemCount6"].ToString();

                // Rewards
                textBoxQuestSectionRewItemID1.Text          = qtTable.Tables[0].Rows[0]["RewardItem1"].ToString();
                textBoxQuestSectionRewItemC1.Text           = qtTable.Tables[0].Rows[0]["RewardAmount1"].ToString();
                textBoxQuestSectionRewItemID2.Text          = qtTable.Tables[0].Rows[0]["RewardItem2"].ToString();
                textBoxQuestSectionRewItemC2.Text           = qtTable.Tables[0].Rows[0]["RewardAmount2"].ToString();
                textBoxQuestSectionRewItemID3.Text          = qtTable.Tables[0].Rows[0]["RewardItem3"].ToString();
                textBoxQuestSectionRewItemC3.Text           = qtTable.Tables[0].Rows[0]["RewardAmount3"].ToString();
                textBoxQuestSectionRewItemID4.Text          = qtTable.Tables[0].Rows[0]["RewardItem4"].ToString();
                textBoxQuestSectionRewItemC4.Text           = qtTable.Tables[0].Rows[0]["RewardAmount4"].ToString();

                textBoxQuestSectionRewChoiceID1.Text        = qtTable.Tables[0].Rows[0]["RewardChoiceItemID1"].ToString();
                textBoxQuestSectionRewChoiceC1.Text         = qtTable.Tables[0].Rows[0]["RewardChoiceItemQuantity1"].ToString();
                textBoxQuestSectionRewChoiceID2.Text        = qtTable.Tables[0].Rows[0]["RewardChoiceItemID2"].ToString();
                textBoxQuestSectionRewChoiceC2.Text         = qtTable.Tables[0].Rows[0]["RewardChoiceItemQuantity2"].ToString();
                textBoxQuestSectionRewChoiceID3.Text        = qtTable.Tables[0].Rows[0]["RewardChoiceItemID3"].ToString();
                textBoxQuestSectionRewChoiceC3.Text         = qtTable.Tables[0].Rows[0]["RewardChoiceItemQuantity3"].ToString();
                textBoxQuestSectionRewChoiceID4.Text        = qtTable.Tables[0].Rows[0]["RewardChoiceItemID4"].ToString();
                textBoxQuestSectionRewChoiceC4.Text         = qtTable.Tables[0].Rows[0]["RewardChoiceItemQuantity4"].ToString();
                textBoxQuestSectionRewChoiceID5.Text        = qtTable.Tables[0].Rows[0]["RewardChoiceItemID5"].ToString();
                textBoxQuestSectionRewChoiceC5.Text         = qtTable.Tables[0].Rows[0]["RewardChoiceItemQuantity5"].ToString();
                textBoxQuestSectionRewChoiceID6.Text        = qtTable.Tables[0].Rows[0]["RewardChoiceItemID6"].ToString();
                textBoxQuestSectionRewChoiceC6.Text         = qtTable.Tables[0].Rows[0]["RewardChoiceItemQuantity6"].ToString();

                textBoxQuestSectionRewOtherSpell.Text       = qtTable.Tables[0].Rows[0]["RewardDisplaySpell"].ToString();
                textBoxQuestSectionRewOtherSpellCast.Text   = qtTable.Tables[0].Rows[0]["RewardSpell"].ToString();
                textBoxQuestSectionRewOtherMoney.Text       = qtTable.Tables[0].Rows[0]["RewardMoney"].ToString();
                textBoxQuestSectionRewOtherMoneyML.Text     = qtTable.Tables[0].Rows[0]["RewardBonusMoney"].ToString();
                textBoxQuestSectionRewOtherMailID.Text      = qtTable.Tables[1].Rows[0]["RewardMailTemplateID"].ToString();
                textBoxQuestSectionRewOtherTitleID.Text     = qtTable.Tables[1].Rows[0]["RewardMailTemplateID"].ToString();
                textBoxQuestSectionRewOtherTP.Text          = qtTable.Tables[1].Rows[0]["RewardMailTemplateID"].ToString();
                textBoxQuestSectionRewOtherHP.Text          = qtTable.Tables[0].Rows[0]["RewardHonor"].ToString();
                textBoxQuestSectionRewOtherAP.Text          = qtTable.Tables[0].Rows[0]["RewardArenaPoints"].ToString();
                textBoxQuestSectionRewOtherTP.Text          = qtTable.Tables[0].Rows[0]["RewardTalents"].ToString();

                #endregion

                ConnectionClose(connect);
            }
        }
        private string DatabaseQuestSectionGenerate()
        {
            string query = "REPLACE INTO `quest_template` (" +
            "`ID`, `ExclusiveGroup`, `PrevQuestID`, `NextQuestID`, `AllowableRaces`, `AllowableClasses`, `Minlevel`, `MaxLevel`, `RequiredSkillID`, `RequiredSkillPoints`, `RequiredMinRepFaction`, `RequiredMaxRepFaction`, `RequiredMinRepValue`, `RequiredMaxRepValue`, " +
            "`Flags`, `Flags`, `TimeAllowed`, `LogTitle`, `QuestDescription`, `LogDescription`, `LogTitle`, `QuestCompletionLog`, `ObjectiveText1`, `ObjectiveText2`, `ObjectiveText3`, `ObjectiveText4`, `QuestLevel`, `SuggestedGroupNum`, `SpecialFlags`, `RequiredPlayerKills`, `QuestType`, `StartItem, `" +
            "`RequiredNpcOrGo1`, `RequiredNpcOrGoCount1`, `RequiredNpcOrGo2`, `RequiredNpcOrGoCount2`, `RequiredNpcOrGo3`, `RequiredNpcOrGoCount3`, `RequiredNpcOrGo4`, `RequiredNpcOrGoCount4`, " +
            "`RequiredItemId1`, `RequiredItemCount1`, `RequiredItemId2`, `RequiredItemCount2`, `RequiredItemId3`, `RequiredItemCount3`, `RequiredItemId4`, `RequiredItemCount4`, `RequiredItemId5`, `RequiredItemCount5`, `RequiredItemId6`, `RequiredItemCount6`, " +
            "`RewardItem1`, `RewardAmount1`, `RewardItem2`, `RewardAmount2`, `RewardItem3`, `RewardAmount3`, `RewardItem4`, `RewardAmount4`, " +
            "`RewardChoiceItemID1`, `RewardChoiceItemQuantity1`, `RewardChoiceItemID2`, `RewardChoiceItemQuantity2`, `RewardChoiceItemID3`, `RewardChoiceItemQuantity3`, `RewardChoiceItemID4`, `RewardChoiceItemQuantity4`, `RewardChoiceItemID5`, `RewardChoiceItemQuantity5`, `RewardChoiceItemID6`, `RewardChoiceItemQuantity6`, " +
            "`RewardDisplaySpell`, `RewardSpell`, `RewardMoney`, `RewardBonusMoney`, `RewardMailTemplateID`, `RewardMailTemplateID`, `RewardMailTemplateID`, `RewardHonor`, `RewardArenaPoints`, `RewardTalents`" +
            ") VALUES (" +

            textBoxQuestSectionID.Text.Trim() + ", " +
            textBoxQuestSectionExclusive.Text.Trim() + ", " +
            textBoxQuestSectionPrevQuest.Text.Trim() + ", " +
            textBoxQuestSectionNextQuest.Text.Trim() + ", " +

            textBoxQuestSectionReqRace.Text.Trim() + ", " +
            textBoxQuestSectionReqClass.Text.Trim() + ", " +
            textBoxQuestSectionLevelMin.Text.Trim() + ", " +
            textBoxQuestSectionLevelMax.Text.Trim() + ", " +
            textBoxQuestSectionSkillID.Text.Trim() + ", " +
            textBoxQuestSectionSkillPoints.Text.Trim() + ", " +
            textBoxQuestSectionFaction1.Text.Trim() + ", " +
            textBoxQuestSectionFaction2.Text.Trim() + ", " +
            textBoxQuestSectionValue1.Text.Trim() + ", " +
            textBoxQuestSectionValue2.Text.Trim() + ", " +

            textBoxQuestSectionType.Text.Trim() + ", " +
            textBoxQuestSectionType.Text.Trim() + ", " +
            textBoxQuestSectionTimeAllowed.Text.Trim() + ", '" +

            textBoxQuestSectionTitle.Text.Trim() + "', '" +
            textBoxQuestSectionLDescription.Text.Trim() + "', '" +
            textBoxQuestSectionQDescription.Text.Trim() + "', '" +
            textBoxQuestSectionAreaDescription.Text.Trim() + "', '" +

            textBoxQuestSectionCompleted.Text.Trim() + "', '" +
            textBoxQuestSectionObjectives1.Text.Trim() + "', '" +
            textBoxQuestSectionObjectives2.Text.Trim() + "', '" +
            textBoxQuestSectionObjectives3.Text.Trim() + "', '" +
            textBoxQuestSectionObjectives4.Text.Trim() + "', " +

            // Other
            textBoxQuestSectionQuestLevel.Text.Trim() + ", " +
            textBoxQuestSectionOtherSP.Text.Trim() + ", " +
            textBoxQuestSectionOtherSF.Text.Trim() + ", " +
            textBoxQuestSectionOtherPK.Text.Trim() + ", " +
            textBoxQuestSectionQType.Text.Trim() + ", " +
            textBoxQuestSectionQuestStartItem.Text.Trim() + ", " +

            textBoxQuestSectionReqNPCID1.Text.Trim() + ", " +
            textBoxQuestSectionReqNPCC1.Text.Trim() + ", " +
            textBoxQuestSectionReqNPCID2.Text.Trim() + ", " +
            textBoxQuestSectionReqNPCC2.Text.Trim() + ", " +
            textBoxQuestSectionReqNPCID3.Text.Trim() + ", " +
            textBoxQuestSectionReqNPCC3.Text.Trim() + ", " +
            textBoxQuestSectionReqNPCID4.Text.Trim() + ", " +
            textBoxQuestSectionReqNPCC4.Text.Trim() + ", " +

            textBoxQuestSectionReqItemID1.Text.Trim() + ", " +
            textBoxQuestSectionReqItemC1.Text.Trim() + ", " +
            textBoxQuestSectionReqItemID2.Text.Trim() + ", " +
            textBoxQuestSectionReqItemC2.Text.Trim() + ", " +
            textBoxQuestSectionReqItemID3.Text.Trim() + ", " +
            textBoxQuestSectionReqItemC3.Text.Trim() + ", " +
            textBoxQuestSectionReqItemID4.Text.Trim() + ", " +
            textBoxQuestSectionReqItemC4.Text.Trim() + ", " +
            textBoxQuestSectionReqItemID5.Text.Trim() + ", " +
            textBoxQuestSectionReqItemC5.Text.Trim() + ", " +
            textBoxQuestSectionReqItemID6.Text.Trim() + ", " +
            textBoxQuestSectionReqItemC6.Text.Trim() + ", " +

            textBoxQuestSectionRewItemID1.Text.Trim() + ", " +
            textBoxQuestSectionRewItemC1.Text.Trim() + ", " +
            textBoxQuestSectionRewItemID2.Text.Trim() + ", " +
            textBoxQuestSectionRewItemC2.Text.Trim() + ", " +
            textBoxQuestSectionRewItemID3.Text.Trim() + ", " +
            textBoxQuestSectionRewItemC3.Text.Trim() + ", " +
            textBoxQuestSectionRewItemID4.Text.Trim() + ", " +
            textBoxQuestSectionRewItemC4.Text.Trim() + ", " +

            textBoxQuestSectionRewChoiceID1.Text.Trim() + ", " +
            textBoxQuestSectionRewChoiceC1.Text.Trim() + ", " +
            textBoxQuestSectionRewChoiceID2.Text.Trim() + ", " +
            textBoxQuestSectionRewChoiceC2.Text.Trim() + ", " +
            textBoxQuestSectionRewChoiceID3.Text.Trim() + ", " +
            textBoxQuestSectionRewChoiceC3.Text.Trim() + ", " +
            textBoxQuestSectionRewChoiceID4.Text.Trim() + ", " +
            textBoxQuestSectionRewChoiceC4.Text.Trim() + ", " +
            textBoxQuestSectionRewChoiceID5.Text.Trim() + ", " +
            textBoxQuestSectionRewChoiceC5.Text.Trim() + ", " +
            textBoxQuestSectionRewChoiceID6.Text.Trim() + ", " +
            textBoxQuestSectionRewChoiceC6.Text.Trim() + ", " +

            textBoxQuestSectionRewOtherSpell.Text.Trim() + ", " +
            textBoxQuestSectionRewOtherSpellCast.Text.Trim() + ", " +
            textBoxQuestSectionRewOtherMoney.Text.Trim() + ", " +
            textBoxQuestSectionRewOtherMoneyML.Text.Trim() + ", " +
            textBoxQuestSectionRewOtherMailID.Text.Trim() + ", " +
            textBoxQuestSectionRewOtherTitleID.Text.Trim() + ", " +
            textBoxQuestSectionRewOtherTP.Text.Trim() + ", " +
            textBoxQuestSectionRewOtherHP.Text.Trim() + ", " +
            textBoxQuestSectionRewOtherAP.Text.Trim() + ", " +
            textBoxQuestSectionRewOtherTP.Text.Trim() +

            ");";

            return query;
        }
        #endregion
        #region GameObject
        private void DatabaseGameObjectSearch(string GameobjectEntryID)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            if (ConnectionOpen(connect))
            {
                var query = "SELECT * FROM gameobject_template WHERE entry = '" + GameobjectEntryID  + "';";

                var gotTable = DatabaseSearch(connect, query);

                #region General
                textBoxGameObjectTempEntry.Text = gotTable.Tables[0].Rows[0]["entry"].ToString();
                textBoxGameObjectTempType.Text = gotTable.Tables[0].Rows[0]["type"].ToString();
                textBoxGameObjectTempDID.Text = gotTable.Tables[0].Rows[0]["displayId"].ToString();
                textBoxGameObjectTempName.Text = gotTable.Tables[0].Rows[0]["name"].ToString();
                textBoxGameObjectTempFaction.Text = gotTable.Tables[0].Rows[0]["faction"].ToString();
                textBoxGameObjectTempFlags.Text = gotTable.Tables[0].Rows[0]["flags"].ToString();
                textBoxGameObjectTempSize.Text = gotTable.Tables[0].Rows[0]["size"].ToString();
                #endregion
                #region Datas
                textBoxGameObjectTempD0.Text = gotTable.Tables[0].Rows[0]["Data0"].ToString();
                textBoxGameObjectTempD1.Text = gotTable.Tables[0].Rows[0]["Data1"].ToString();
                textBoxGameObjectTempD2.Text = gotTable.Tables[0].Rows[0]["Data2"].ToString();
                textBoxGameObjectTempD3.Text = gotTable.Tables[0].Rows[0]["Data3"].ToString();
                textBoxGameObjectTempD4.Text = gotTable.Tables[0].Rows[0]["Data4"].ToString();
                textBoxGameObjectTempD5.Text = gotTable.Tables[0].Rows[0]["Data5"].ToString();
                textBoxGameObjectTempD6.Text = gotTable.Tables[0].Rows[0]["Data6"].ToString();
                textBoxGameObjectTempD7.Text = gotTable.Tables[0].Rows[0]["Data7"].ToString();
                textBoxGameObjectTempD8.Text = gotTable.Tables[0].Rows[0]["Data8"].ToString();
                textBoxGameObjectTempD9.Text = gotTable.Tables[0].Rows[0]["Data9"].ToString();
                textBoxGameObjectTempD10.Text = gotTable.Tables[0].Rows[0]["Data10"].ToString();
                textBoxGameObjectTempD11.Text = gotTable.Tables[0].Rows[0]["Data11"].ToString();
                textBoxGameObjectTempD12.Text = gotTable.Tables[0].Rows[0]["Data12"].ToString();
                textBoxGameObjectTempD13.Text = gotTable.Tables[0].Rows[0]["Data13"].ToString();
                textBoxGameObjectTempD14.Text = gotTable.Tables[0].Rows[0]["Data14"].ToString();
                textBoxGameObjectTempD15.Text = gotTable.Tables[0].Rows[0]["Data15"].ToString();
                textBoxGameObjectTempD16.Text = gotTable.Tables[0].Rows[0]["Data16"].ToString();
                textBoxGameObjectTempD17.Text = gotTable.Tables[0].Rows[0]["Data17"].ToString();
                textBoxGameObjectTempD18.Text = gotTable.Tables[0].Rows[0]["Data18"].ToString();
                textBoxGameObjectTempD19.Text = gotTable.Tables[0].Rows[0]["Data19"].ToString();
                textBoxGameObjectTempD20.Text = gotTable.Tables[0].Rows[0]["Data20"].ToString();
                textBoxGameObjectTempD21.Text = gotTable.Tables[0].Rows[0]["Data21"].ToString();
                textBoxGameObjectTempD22.Text = gotTable.Tables[0].Rows[0]["Data22"].ToString();
                textBoxGameObjectTempD23.Text = gotTable.Tables[0].Rows[0]["Data23"].ToString();
                #endregion

                ConnectionClose(connect);
            }
        }
        #endregion
        #region Item
        private void DatabaseItemSearch(string itemEntryID)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            if (ConnectionOpen(connect))
            {
                var query = "SELECT * FROM item_template WHERE entry = '" + itemEntryID + "';";

                var itTable = DatabaseSearch(connect, query);

                textBoxItemTempEntry.Text = itTable.Tables[0].Rows[0]["entry"].ToString();
                textBoxItemTempTypeClass.Text = itTable.Tables[0].Rows[0]["class"].ToString();
                textBoxItemTempSubclass.Text = itTable.Tables[0].Rows[0]["subclass"].ToString();
                textBoxItemTempName.Text = itTable.Tables[0].Rows[0]["name"].ToString();
                textBoxItemTempDisplayID.Text = itTable.Tables[0].Rows[0]["displayid"].ToString();
                textBoxItemTempQuality.Text = itTable.Tables[0].Rows[0]["Quality"].ToString();
                textBoxItemTempFlags.Text = itTable.Tables[0].Rows[0]["Flags"].ToString();
                textBoxItemTempEFlags.Text = itTable.Tables[0].Rows[0]["FlagsExtra"].ToString();
                textBoxItemTempBuyC.Text = itTable.Tables[0].Rows[0]["BuyCount"].ToString();
                textBoxItemTempBuyP.Text = itTable.Tables[0].Rows[0]["BuyPrice"].ToString();
                textBoxItemTempSellP.Text = itTable.Tables[0].Rows[0]["SellPrice"].ToString();
                textBoxItemTempInventory.Text = itTable.Tables[0].Rows[0]["InventoryType"].ToString();
                textBoxItemTempMaxC.Text = itTable.Tables[0].Rows[0]["maxcount"].ToString();
                textBoxItemTempContainer.Text = itTable.Tables[0].Rows[0]["ContainerSlots"].ToString();

                textBoxItemTempReqClass.Text = itTable.Tables[0].Rows[0]["AllowableClass"].ToString();
                textBoxItemTempReqRace.Text = itTable.Tables[0].Rows[0]["AllowableRace"].ToString();
                textBoxItemTempReqItemLevel.Text = itTable.Tables[0].Rows[0]["ItemLevel"].ToString();
                textBoxItemTempReqLevel.Text = itTable.Tables[0].Rows[0]["RequiredLevel"].ToString();
                textBoxItemTempReqSkill.Text = itTable.Tables[0].Rows[0]["RequiredSkill"].ToString();
                textBoxItemTempReqSkillRank.Text = itTable.Tables[0].Rows[0]["RequiredSkillRank"].ToString();
                textBoxItemTempReqSpell.Text = itTable.Tables[0].Rows[0]["requiredspell"].ToString();
                textBoxItemTempReqHonorRank.Text = itTable.Tables[0].Rows[0]["requiredhonorrank"].ToString();
                textBoxItemTempReqCityRank.Text = itTable.Tables[0].Rows[0]["RequiredCityRank"].ToString();
                textBoxItemTempReqRepFaction.Text = itTable.Tables[0].Rows[0]["RequiredReputationFaction"].ToString();
                textBoxItemTempReqRepRank.Text = itTable.Tables[0].Rows[0]["RequiredReputationRank"].ToString();
                textBoxItemTempReqDisenchant.Text = itTable.Tables[0].Rows[0]["RequiredDisenchantSkill"].ToString();

                textBoxItemTempStatsC.Text = itTable.Tables[0].Rows[0]["StatsCount"].ToString();
                textBoxItemTempStatsType1.Text = itTable.Tables[0].Rows[0]["stat_type1"].ToString();
                textBoxItemTempStatsValue1.Text = itTable.Tables[0].Rows[0]["stat_value1"].ToString();
                textBoxItemTempStatsType2.Text = itTable.Tables[0].Rows[0]["stat_type2"].ToString();
                textBoxItemTempStatsValue2.Text = itTable.Tables[0].Rows[0]["stat_value2"].ToString();
                textBoxItemTempStatsType3.Text = itTable.Tables[0].Rows[0]["stat_type3"].ToString();
                textBoxItemTempStatsValue3.Text = itTable.Tables[0].Rows[0]["stat_value3"].ToString();
                textBoxItemTempStatsType4.Text = itTable.Tables[0].Rows[0]["stat_type4"].ToString();
                textBoxItemTempStatsValue4.Text = itTable.Tables[0].Rows[0]["stat_value4"].ToString();
                textBoxItemTempStatsType5.Text = itTable.Tables[0].Rows[0]["stat_type5"].ToString();
                textBoxItemTempStatsValue5.Text = itTable.Tables[0].Rows[0]["stat_value5"].ToString();
                textBoxItemTempStatsType6.Text = itTable.Tables[0].Rows[0]["stat_type6"].ToString();
                textBoxItemTempStatsValue6.Text = itTable.Tables[0].Rows[0]["stat_value6"].ToString();
                textBoxItemTempStatsType7.Text = itTable.Tables[0].Rows[0]["stat_type7"].ToString();
                textBoxItemTempStatsValue7.Text = itTable.Tables[0].Rows[0]["stat_value7"].ToString();
                textBoxItemTempStatsType8.Text = itTable.Tables[0].Rows[0]["stat_type8"].ToString();
                textBoxItemTempStatsValue8.Text = itTable.Tables[0].Rows[0]["stat_value8"].ToString();
                textBoxItemTempStatsType9.Text = itTable.Tables[0].Rows[0]["stat_type9"].ToString();
                textBoxItemTempStatsValue9.Text = itTable.Tables[0].Rows[0]["stat_value9"].ToString();
                textBoxItemTempStatsType10.Text = itTable.Tables[0].Rows[0]["stat_type10"].ToString();
                textBoxItemTempStatsValue10.Text = itTable.Tables[0].Rows[0]["stat_value10"].ToString();
                textBoxItemTempStatsScaleDist.Text = itTable.Tables[0].Rows[0]["ScalingStatDistribution"].ToString();
                textBoxItemTempStatsScaleValue.Text = itTable.Tables[0].Rows[0]["ScalingStatValue"].ToString();

                textBoxItemTempDmgType1.Text = itTable.Tables[0].Rows[0]["dmg_type1"].ToString();
                textBoxItemTempDmgMin1.Text = itTable.Tables[0].Rows[0]["dmg_min1"].ToString();
                textBoxItemTempDmgMax1.Text = itTable.Tables[0].Rows[0]["dmg_max1"].ToString();
                textBoxItemTempDmgType2.Text = itTable.Tables[0].Rows[0]["dmg_type2"].ToString();
                textBoxItemTempDmgMin2.Text = itTable.Tables[0].Rows[0]["dmg_min2"].ToString();
                textBoxItemTempDmgMax2.Text = itTable.Tables[0].Rows[0]["dmg_max2"].ToString();

                textBoxItemTempResisHoly.Text = itTable.Tables[0].Rows[0]["holy_res"].ToString();
                textBoxItemTempResisFire.Text = itTable.Tables[0].Rows[0]["fire_res"].ToString();
                textBoxItemTempResisNature.Text = itTable.Tables[0].Rows[0]["nature_res"].ToString();
                textBoxItemTempResisFrost.Text = itTable.Tables[0].Rows[0]["frost_res"].ToString();
                textBoxItemTempResisShadow.Text = itTable.Tables[0].Rows[0]["shadow_res"].ToString();
                textBoxItemTempResisArcane.Text = itTable.Tables[0].Rows[0]["arcane_res"].ToString();

                textBoxItemTempSpellID1.Text = itTable.Tables[0].Rows[0]["spellid_1"].ToString();
                textBoxItemTempTrigger1.Text = itTable.Tables[0].Rows[0]["spelltrigger_1"].ToString();
                textBoxItemTempCharges1.Text = itTable.Tables[0].Rows[0]["spellcharges_1"].ToString();
                textBoxItemTempRate1.Text = itTable.Tables[0].Rows[0]["spellppmRate_1"].ToString();
                textBoxItemTempCD1.Text = itTable.Tables[0].Rows[0]["spellcooldown_1"].ToString();
                textBoxItemTempCategory1.Text = itTable.Tables[0].Rows[0]["spellcategory_1"].ToString();
                textBoxItemTempCategoryCD1.Text = itTable.Tables[0].Rows[0]["spellcategorycooldown_1"].ToString();
                textBoxItemTempSpellID2.Text = itTable.Tables[0].Rows[0]["spellid_2"].ToString();
                textBoxItemTempTrigger2.Text = itTable.Tables[0].Rows[0]["spelltrigger_2"].ToString();
                textBoxItemTempCharges2.Text = itTable.Tables[0].Rows[0]["spellcharges_2"].ToString();
                textBoxItemTempRate2.Text = itTable.Tables[0].Rows[0]["spellppmRate_2"].ToString();
                textBoxItemTempCD2.Text = itTable.Tables[0].Rows[0]["spellcooldown_2"].ToString();
                textBoxItemTempCategory2.Text = itTable.Tables[0].Rows[0]["spellcategory_2"].ToString();
                textBoxItemTempCategoryCD2.Text = itTable.Tables[0].Rows[0]["spellcategorycooldown_2"].ToString();
                textBoxItemTempSpellID3.Text = itTable.Tables[0].Rows[0]["spellid_3"].ToString();
                textBoxItemTempTrigger3.Text = itTable.Tables[0].Rows[0]["spelltrigger_3"].ToString();
                textBoxItemTempCharges3.Text = itTable.Tables[0].Rows[0]["spellcharges_3"].ToString();
                textBoxItemTempRate3.Text = itTable.Tables[0].Rows[0]["spellppmRate_3"].ToString();
                textBoxItemTempCD3.Text = itTable.Tables[0].Rows[0]["spellcooldown_3"].ToString();
                textBoxItemTempCategory3.Text = itTable.Tables[0].Rows[0]["spellcategory_3"].ToString();
                textBoxItemTempCategoryCD3.Text = itTable.Tables[0].Rows[0]["spellcategorycooldown_3"].ToString();
                textBoxItemTempSpellID4.Text = itTable.Tables[0].Rows[0]["spellid_4"].ToString();
                textBoxItemTempTrigger4.Text = itTable.Tables[0].Rows[0]["spelltrigger_4"].ToString();
                textBoxItemTempCharges4.Text = itTable.Tables[0].Rows[0]["spellcharges_4"].ToString();
                textBoxItemTempRate4.Text = itTable.Tables[0].Rows[0]["spellppmRate_4"].ToString();
                textBoxItemTempCD4.Text = itTable.Tables[0].Rows[0]["spellcooldown_4"].ToString();
                textBoxItemTempCategory4.Text = itTable.Tables[0].Rows[0]["spellcategory_4"].ToString();
                textBoxItemTempCategoryCD4.Text = itTable.Tables[0].Rows[0]["spellcategorycooldown_4"].ToString();
                textBoxItemTempSpellID5.Text = itTable.Tables[0].Rows[0]["spellid_5"].ToString();
                textBoxItemTempTrigger5.Text = itTable.Tables[0].Rows[0]["spelltrigger_5"].ToString();
                textBoxItemTempCharges5.Text = itTable.Tables[0].Rows[0]["spellcharges_5"].ToString();
                textBoxItemTempRate5.Text = itTable.Tables[0].Rows[0]["spellppmRate_5"].ToString();
                textBoxItemTempCD5.Text = itTable.Tables[0].Rows[0]["spellcooldown_5"].ToString();
                textBoxItemTempCategory5.Text = itTable.Tables[0].Rows[0]["spellcategory_5"].ToString();
                textBoxItemTempCategoryCD5.Text = itTable.Tables[0].Rows[0]["spellcategorycooldown_5"].ToString();

                textBoxItemTempColor1.Text = itTable.Tables[0].Rows[0]["socketColor_1"].ToString();
                textBoxItemTempContent1.Text = itTable.Tables[0].Rows[0]["socketContent_1"].ToString();
                textBoxItemTempColor2.Text = itTable.Tables[0].Rows[0]["socketColor_2"].ToString();
                textBoxItemTempContent2.Text = itTable.Tables[0].Rows[0]["socketContent_2"].ToString();
                textBoxItemTempColor3.Text = itTable.Tables[0].Rows[0]["socketColor_3"].ToString();
                textBoxItemTempContent3.Text = itTable.Tables[0].Rows[0]["socketContent_3"].ToString();
                textBoxItemTempSocketBonus.Text = itTable.Tables[0].Rows[0]["socketBonus"].ToString();
                textBoxItemTempGemProper.Text = itTable.Tables[0].Rows[0]["GemProperties"].ToString();

                textBoxItemTempDelay.Text = itTable.Tables[0].Rows[0]["delay"].ToString();
                textBoxItemTempAmmoType.Text = itTable.Tables[0].Rows[0]["ammo_type"].ToString();
                textBoxItemTempRangedMod.Text = itTable.Tables[0].Rows[0]["RangedModRange"].ToString();
                textBoxItemTempBonding.Text = itTable.Tables[0].Rows[0]["bonding"].ToString();
                textBoxItemTempDescription.Text = itTable.Tables[0].Rows[0]["description"].ToString();
                textBoxItemTempPageText.Text = itTable.Tables[0].Rows[0]["PageText"].ToString();
                textBoxItemTempLanguage.Text = itTable.Tables[0].Rows[0]["LanguageID"].ToString();
                textBoxItemTempPageMaterial.Text = itTable.Tables[0].Rows[0]["PageMaterial"].ToString();
                textBoxItemTempStartQuest.Text = itTable.Tables[0].Rows[0]["startquest"].ToString();
                textBoxItemTempLockID.Text = itTable.Tables[0].Rows[0]["lockid"].ToString();
                textBoxItemTempMaterial.Text = itTable.Tables[0].Rows[0]["Material"].ToString();
                textBoxItemTempSheath.Text = itTable.Tables[0].Rows[0]["sheath"].ToString();
                textBoxItemTempProperty.Text = itTable.Tables[0].Rows[0]["RandomProperty"].ToString();
                textBoxItemTempSuffix.Text = itTable.Tables[0].Rows[0]["RandomSuffix"].ToString();
                textBoxItemTempBlock.Text = itTable.Tables[0].Rows[0]["block"].ToString();
                textBoxItemTempItemSet.Text = itTable.Tables[0].Rows[0]["itemset"].ToString();
                textBoxItemTempDurability.Text = itTable.Tables[0].Rows[0]["MaxDurability"].ToString();
                textBoxItemTempArea.Text = itTable.Tables[0].Rows[0]["area"].ToString();
                textBoxItemTempMap.Text = itTable.Tables[0].Rows[0]["Map"].ToString();
                textBoxItemTempDisenchantID.Text = itTable.Tables[0].Rows[0]["DisenchantID"].ToString();
                textBoxItemTempModifier.Text = itTable.Tables[0].Rows[0]["ArmorDamageModifier"].ToString();
                textBoxItemTempHolidayID.Text = itTable.Tables[0].Rows[0]["HolidayId"].ToString();
                textBoxItemTempFoodType.Text = itTable.Tables[0].Rows[0]["FoodType"].ToString();
                textBoxItemTempFlagsC.Text = itTable.Tables[0].Rows[0]["flagsCustom"].ToString();
                textBoxItemTempDuration.Text = itTable.Tables[0].Rows[0]["duration"].ToString();
                textBoxItemTempLimitCate.Text = itTable.Tables[0].Rows[0]["ItemLimitCategory"].ToString();
                textBoxItemTempMoneyMin.Text = itTable.Tables[0].Rows[0]["minMoneyLoot"].ToString();
                textBoxItemTempMoneyMax.Text = itTable.Tables[0].Rows[0]["maxMoneyLoot"].ToString();

                ConnectionClose(connect);
            }
        }
        private string DatabaseItemTempGenerate()
        {
            string query = "REPLACE INTO `item_template` (" +
            "`entry`, `class`, `subclass`, `name`, `displayid`, `Quality`, `Flags`, `FlagsExtra`, `BuyCount`, `BuyPrice`, `SellPrice`, `InventoryType`, `maxcount`, `ContainerSlots`, " +
            "`AllowableClass`, `AllowableRace`, `ItemLevel`, `RequiredLevel`, `RequiredSkill`, `RequiredSkillRank`, `requiredspell`, `requiredhonorrank`, `RequiredCityRank`, `RequiredReputationFaction`, `RequiredReputationRank`, `RequiredDisenchantSkill`, " +
            "`StatsCount`, `stat_type1`, `stat_value1`, `stat_type2`, `stat_value2`, `stat_type3`, `stat_value3`, `stat_type4`, `stat_value4`, `stat_type5`, `stat_value5`, `stat_type6`, `stat_value6`, `stat_type7`, `stat_value7`, `stat_type8`, `stat_value8`, `stat_type9`, `stat_value9`, `stat_type10`, `stat_value10`, `ScalingStatDistribution`, `ScalingStatValue`, " +
            "`spellid_1`, `spelltrigger_1`, `spellcharges_1`, `spellppmRate_1`, `spellcooldown_1`, `spellcategory_1`, `spellcategorycooldown_1`, `spellid_2`, `spelltrigger_2`, `spellcharges_2`, `spellppmRate_2`, `spellcooldown_2`, `spellcategory_2`, `spellcategorycooldown_2`, " +
            "`spellid_3`, `spelltrigger_3`, `spellcharges_3`, `spellppmRate_3`, `spellcooldown_3`, `spellcategory_3`, `spellcategorycooldown_3`, `spellid_4`, `spelltrigger_4`, `spellcharges_4`, `spellppmRate_4`, `spellcooldown_4`, `spellcategory_4`, `spellcategorycooldown_4`, `spellid_5`, `spelltrigger_5`, `spellcharges_5`, `spellppmRate_5`, `spellcooldown_5`, `spellcategory_5`, `spellcategorycooldown_5`, " +
            "`dmg_type1`, `dmg_min1`, `dmg_max1`, `dmg_type2`, `dmg_min2`, `dmg_max2`, " +
            "`holy_res`, `fire_res`, `nature_res`, `frost_res`, `shadow_res`, `arcane_res`, " +
            "`socketColor_1`, `socketContent_1`, `socketColor_2`, `socketContent_2`, `socketColor_3`, `socketContent_3`, `socketBonus`, `GemProperties`, " +
            "`delay`, `ammo_type`, `RangedModRange`, `bonding`, `description`, `PageText`, `LanguageID`, `PageMaterial`, `startquest`, `lockid`, `Material`, `sheath`, " +
            "`RandomProperty`, `RandomSuffix`, `block`, `itemset`, `MaxDurability`, `area`, `Map`, `DisenchantID`, `ArmorDamageModifier`, `HolidayId`, `FoodType`, `flagsCustom`, `duration`, `ItemLimitCategory`, `minMoneyLoot`, `maxMoneyLoot`" +
            ") VALUES (" +

            textBoxItemTempEntry.Text.Trim() + ", " +
            textBoxItemTempTypeClass.Text.Trim() + ", " +
            textBoxItemTempSubclass.Text.Trim() + ", '" +
            textBoxItemTempName.Text.Trim() + "', " +
            textBoxItemTempDisplayID.Text.Trim() + ", " +
            textBoxItemTempQuality.Text.Trim() + ", " +
            textBoxItemTempFlags.Text.Trim() + ", " +
            textBoxItemTempEFlags.Text.Trim() + ", " +
            textBoxItemTempBuyC.Text.Trim() + ", " +
            textBoxItemTempBuyP.Text.Trim() + ", " +
            textBoxItemTempSellP.Text.Trim() + ", " +
            textBoxItemTempInventory.Text.Trim() + ", " +
            textBoxItemTempMaxC.Text.Trim() + ", " +
            textBoxItemTempContainer.Text.Trim() + ", " +

            textBoxItemTempReqClass.Text.Trim() + ", " +
            textBoxItemTempReqRace.Text.Trim() + ", " +
            textBoxItemTempReqItemLevel.Text.Trim() + ", " +
            textBoxItemTempReqLevel.Text.Trim() + ", " +
            textBoxItemTempReqSkill.Text.Trim() + ", " +
            textBoxItemTempReqSkillRank.Text.Trim() + ", " +
            textBoxItemTempReqSpell.Text.Trim() + ", " +
            textBoxItemTempReqHonorRank.Text.Trim() + ", " +
            textBoxItemTempReqCityRank.Text.Trim() + ", " +
            textBoxItemTempReqRepFaction.Text.Trim() + ", " +
            textBoxItemTempReqRepRank.Text.Trim() + ", " +
            textBoxItemTempReqDisenchant.Text.Trim() + ", " +
            
            textBoxItemTempStatsC.Text.Trim() + ", " +
            textBoxItemTempStatsType1.Text.Trim() + ", " +
            textBoxItemTempStatsValue1.Text.Trim() + ", " +
            textBoxItemTempStatsType2.Text.Trim() + ", " +
            textBoxItemTempStatsValue2.Text.Trim() + ", " +
            textBoxItemTempStatsType3.Text.Trim() + ", " +
            textBoxItemTempStatsValue3.Text.Trim() + ", " +
            textBoxItemTempStatsType4.Text.Trim() + ", " +
            textBoxItemTempStatsValue4.Text.Trim() + ", " +
            textBoxItemTempStatsType5.Text.Trim() + ", " +
            textBoxItemTempStatsValue5.Text.Trim() + ", " +
            textBoxItemTempStatsType6.Text.Trim() + ", " +
            textBoxItemTempStatsValue6.Text.Trim() + ", " +
            textBoxItemTempStatsType7.Text.Trim() + ", " +
            textBoxItemTempStatsValue7.Text.Trim() + ", " +
            textBoxItemTempStatsType8.Text.Trim() + ", " +
            textBoxItemTempStatsValue8.Text.Trim() + ", " +
            textBoxItemTempStatsType9.Text.Trim() + ", " +
            textBoxItemTempStatsValue9.Text.Trim() + ", " +
            textBoxItemTempStatsType10.Text.Trim() + ", " +
            textBoxItemTempStatsValue10.Text.Trim() + ", " +
            textBoxItemTempStatsScaleDist.Text.Trim() + ", " +
            textBoxItemTempStatsScaleValue.Text.Trim() + ", " +

            textBoxItemTempSpellID1.Text.Trim() + ", " +
            textBoxItemTempTrigger1.Text.Trim() + ", " +
            textBoxItemTempCharges1.Text.Trim() + ", " +
            textBoxItemTempRate1.Text.Trim() + ", " +
            textBoxItemTempCD1.Text.Trim() + ", " +
            textBoxItemTempCategory1.Text.Trim() + ", " +
            textBoxItemTempCategoryCD1.Text.Trim() + ", " +
            textBoxItemTempSpellID2.Text.Trim() + ", " +
            textBoxItemTempTrigger2.Text.Trim() + ", " +
            textBoxItemTempCharges2.Text.Trim() + ", " +
            textBoxItemTempRate2.Text.Trim() + ", " +
            textBoxItemTempCD2.Text.Trim() + ", " +
            textBoxItemTempCategory2.Text.Trim() + ", " +
            textBoxItemTempCategoryCD2.Text.Trim() + ", " +
            textBoxItemTempSpellID3.Text.Trim() + ", " +
            textBoxItemTempTrigger3.Text.Trim() + ", " +
            textBoxItemTempCharges3.Text.Trim() + ", " +
            textBoxItemTempRate3.Text.Trim() + ", " +
            textBoxItemTempCD3.Text.Trim() + ", " +
            textBoxItemTempCategory3.Text.Trim() + ", " +
            textBoxItemTempCategoryCD3.Text.Trim() + ", " +
            textBoxItemTempSpellID4.Text.Trim() + ", " +
            textBoxItemTempTrigger4.Text.Trim() + ", " +
            textBoxItemTempCharges4.Text.Trim() + ", " +
            textBoxItemTempRate4.Text.Trim() + ", " +
            textBoxItemTempCD4.Text.Trim() + ", " +
            textBoxItemTempCategory4.Text.Trim() + ", " +
            textBoxItemTempCategoryCD4.Text.Trim() + ", " +
            textBoxItemTempSpellID5.Text.Trim() + ", " +
            textBoxItemTempTrigger5.Text.Trim() + ", " +
            textBoxItemTempCharges5.Text.Trim() + ", " +
            textBoxItemTempRate5.Text.Trim() + ", " +
            textBoxItemTempCD5.Text.Trim() + ", " +
            textBoxItemTempCategory5.Text.Trim() + ", " +
            textBoxItemTempCategoryCD5.Text.Trim() + ", " +

            textBoxItemTempDmgType1.Text.Trim() + ", " +
            textBoxItemTempDmgMin1.Text.Trim() + ", " +
            textBoxItemTempDmgMax1.Text.Trim() + ", " +
            textBoxItemTempDmgType2.Text.Trim() + ", " +
            textBoxItemTempDmgMin2.Text.Trim() + ", " +
            textBoxItemTempDmgMax2.Text.Trim() + ", " +

            textBoxItemTempResisHoly.Text.Trim() + ", " +
            textBoxItemTempResisFire.Text.Trim() + ", " +
            textBoxItemTempResisNature.Text.Trim() + ", " +
            textBoxItemTempResisFrost.Text.Trim() + ", " +
            textBoxItemTempResisShadow.Text.Trim() + ", " +
            textBoxItemTempResisArcane.Text.Trim() + ", " +

            textBoxItemTempColor1.Text.Trim() + ", " +
            textBoxItemTempContent1.Text.Trim() + ", " +
            textBoxItemTempColor2.Text.Trim() + ", " +
            textBoxItemTempContent2.Text.Trim() + ", " +
            textBoxItemTempColor3.Text.Trim() + ", " +
            textBoxItemTempContent3.Text.Trim() + ", " +
            textBoxItemTempSocketBonus.Text.Trim() + ", " +
            textBoxItemTempGemProper.Text.Trim() + ", " +

            textBoxItemTempDelay.Text.Trim() + ", " +
            textBoxItemTempAmmoType.Text.Trim() + ", " +
            textBoxItemTempRangedMod.Text.Trim() + ", " +
            textBoxItemTempBonding.Text.Trim() + ", '" +
            textBoxItemTempDescription.Text.Trim() + "', " +
            textBoxItemTempPageText.Text.Trim() + ", " +
            textBoxItemTempLanguage.Text.Trim() + ", " +
            textBoxItemTempPageMaterial.Text.Trim() + ", " +
            textBoxItemTempStartQuest.Text.Trim() + ", " +
            textBoxItemTempLockID.Text.Trim() + ", " +
            textBoxItemTempMaterial.Text.Trim() + ", " +
            textBoxItemTempSheath.Text.Trim() + ", " +
            textBoxItemTempProperty.Text.Trim() + ", " +
            textBoxItemTempSuffix.Text.Trim() + ", " +
            textBoxItemTempBlock.Text.Trim() + ", " +
            textBoxItemTempItemSet.Text.Trim() + ", " +
            textBoxItemTempDurability.Text.Trim() + ", " +
            textBoxItemTempArea.Text.Trim() + ", " +
            textBoxItemTempMap.Text.Trim() + ", " +
            textBoxItemTempDisenchantID.Text.Trim() + ", " +
            textBoxItemTempModifier.Text.Trim() + ", " +
            textBoxItemTempHolidayID.Text.Trim() + ", " +
            textBoxItemTempFoodType.Text.Trim() + ", " +
            textBoxItemTempFlagsC.Text.Trim() + ", " +
            textBoxItemTempDuration.Text.Trim() + ", " +
            textBoxItemTempLimitCate.Text.Trim() + ", " +
            textBoxItemTempMoneyMin.Text.Trim() + ", " +
            textBoxItemTempMoneyMax.Text.Trim() +

            ");";

            return query;
        }

        #endregion

        #endregion
        #region Tab Events

        #region Account
        private void buttonAccountSearchSearch_Click(object sender, EventArgs e)
        {
            bool totalSearch = CheckEmptyControls(tabPageAccountSearch); DialogResult dr;

            string query = "SELECT id, username, email, expansion FROM account WHERE '1' = '1'";

            if (totalSearch)
            {
                dr = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                query += DatabaseQueryFilter(textBoxAccountSearchID.Text, "id");
                query += DatabaseQueryFilter(textBoxAccountSearchUsername.Text, "username");

                dr = DialogResult.OK;
            }

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseAuth));

            if (ConnectionOpen(connect))
            {
                query += " ORDER BY id;";
                // Combined DataSet with all the tables.
                DataSet combinedTable = DatabaseSearch(connect, query);

                dataGridViewAccountSearch.DataSource = combinedTable.Tables[0];
                toolStripStatusLabelAccountSearchRows.Text = "Account(s) found: " + combinedTable.Tables[0].Rows.Count.ToString();

                ConnectionClose(connect);
            }
        }
        private void dataGridViewAccountSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewAccountSearch.RowCount != 0)
            {
                DatabaseAccountSearch(dataGridViewAccountSearch.SelectedCells[0].Value.ToString());

                tabControlCategoryAccount.SelectedTab = tabPageAccountAccount;
            }
        }
        private void buttonAccountAccountGenerateScript_Click(object sender, EventArgs e)
        {
            textBoxAccountScriptOutput.Clear();

            textBoxAccountScriptOutput.Text += "UPDATE account " +
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
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseAuth));
            
            if (ConnectionOpen(connect))
            {
                int rows = DatabaseUpdate(connect, textBoxAccountScriptOutput.Text.Trim());
                toolStripStatusLabelAccountScriptResult.Text = "Row(s) affected: " + rows.ToString();
                ConnectionClose(connect);
            } 
        }

        #endregion
        #region Character

        private void buttonCharacterSearchSearch_Click(object sender, EventArgs e)
        {
            bool totalSearch = CheckEmptyControls(tabPageCharacterSearch); DialogResult dr;

            string query = "SELECT guid, account, name, race, class, level FROM characters WHERE '1' = '1'";

            if (totalSearch)
            {
                dr = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                query += DatabaseQueryFilter(textBoxCharacterSearchID.Text, "guid");
                query += DatabaseQueryFilter(textBoxCharacterSearchAccount.Text, "account");
                query += DatabaseQueryFilter(textBoxCharacterSearchUsername.Text, "name");

                dr = DialogResult.OK;
            }

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseCharacters));

            if (ConnectionOpen(connect))
            {
                query += " ORDER BY guid;";
                // Combined DataSet with all the tables.
                DataSet combinedTable = DatabaseSearch(connect, query);

                dataGridViewCharacterSearch.DataSource = combinedTable.Tables[0];
                toolStripStatusLabelCharacterSearchRows.Text = "Character(s) found: " + combinedTable.Tables[0].Rows.Count.ToString();

                ConnectionClose(connect);
            }
        }
        private void dataGridViewCharacterSearchSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCharacterSearch.RowCount != 0)
            {
                DatabaseCharacterSearch(dataGridViewCharacterSearch.SelectedCells[0].Value.ToString());
                DatabaseCharacterInventory(dataGridViewCharacterSearch.SelectedCells[0].Value.ToString());

                tabControlCategoryCharacter.SelectedTab = tabPageCharacterCharacter;
            }
        }
        private void buttonCharacterScriptUpdate_Click(object sender, EventArgs e)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseCharacters));

            if (ConnectionOpen(connect))
            {
                toolStripStatusLabelCharacterScriptRows.Text = "Row(s) Affected: " + DatabaseUpdate(connect, textBoxCharacterScriptOutput.Text).ToString();

                ConnectionClose(connect);
            }
        }
        private void buttonCharacterCharacterGenerate_Click(object sender, EventArgs e)
        {
            textBoxCharacterScriptOutput.Text = DatabaseCharacterCharacterGenerate();
        }

        private void buttonCharacterInventoryAdd_Click(object sender, EventArgs e)
        {
            var values = new string[] {
                textBoxCharacterInventoryGUID.Text,
                textBoxCharacterInventoryBag.Text,
                textBoxCharacterInventorySlot.Text,
                textBoxCharacterInventoryItemID.Text
            };

            if (textBoxCharacterInventoryGUID.Text.Trim() != "")
            {
                var existingData = (DataTable)dataGridViewCharacterInventory.DataSource;
                existingData.Rows.Add(values);
                dataGridViewCharacterInventory.DataSource = existingData;
            }
        }
        private void buttonCharacterInventoryRefresh_Click(object sender, EventArgs e)
        {
            DatabaseCharacterInventory((textBoxCharacterInventoryGUID.Text.Trim() != "") ? textBoxCharacterInventoryGUID.Text : textBoxCharacterCharacterGUID.Text);
        }
        private void buttonCharacterInventoryDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCharacterInventory.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewCharacterInventory.SelectedRows)
                {
                    dataGridViewCharacterInventory.Rows.RemoveAt(row.Index);
                }
            }
        }
        private void buttonCharacterInventoryGenerate_Click(object sender, EventArgs e)
        {
            textBoxCharacterScriptOutput.Text = DatabaseCharacterInventoryGenerate();
        }

        #endregion
        #region Creature

        private void buttonCreatureSearchSearch_Click(object sender, EventArgs e)
        {
            bool totalSearch = CheckEmptyControls(tabPageCreatureSearch); DialogResult dr;

            string query = "SELECT entry, NAME, subname, minlevel, maxlevel, rank, lootid FROM creature_template WHERE '1' = '1'";

            if (totalSearch)
            {
                dr = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            } else
            {
                query += DatabaseQueryFilter(textBoxCreatureSearchEntry.Text, "entry");
                query += DatabaseQueryFilter(textBoxCreatureSearchName.Text, "name");
                query += DatabaseQueryFilter(textBoxCreatureSearchSubname.Text, "subname");
                query += DatabaseQueryFilter(textBoxCreatureSearchLevelMin.Text, "minlevel");
                query += DatabaseQueryFilter(textBoxCreatureSearchLevelMax.Text, "maxlevel");
                query += DatabaseQueryFilter(textBoxCreatureSearchRank.Text, "rank");

                dr = DialogResult.OK;
            }

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));
            
            if (ConnectionOpen(connect))
            {
                query += " ORDER BY entry;";
                // Creature Template
                DataSet ctTable = DatabaseSearch(connect, query);

                dataGridViewCreatureSearch.DataSource = ctTable.Tables[0];
                toolStripStatusLabelCreatureSearchRows.Text = "Creature(s) found: " + ctTable.Tables[0].Rows.Count.ToString();

                ConnectionClose(connect);
            }
        }
        private void dataGridViewCreatureSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCreatureSearch.Rows.Count != 0)
            {
                DatabaseCreatureSearch(dataGridViewCreatureSearch.SelectedCells[0].Value.ToString());
                DatabaseCreatureLocation(dataGridViewCreatureSearch.SelectedCells[0].Value.ToString());

                dataGridViewCreatureVendor.DataSource = DatabaseItemNameColumn("npc_vendor", "entry", textBoxCreatureTemplateEntry.Text.Trim(), 2, true);
                dataGridViewCreatureLoot.DataSource = DatabaseItemNameColumn("creature_loot_template", "Entry", textBoxCreatureTemplateLootID.Text.Trim(), 1, true);
                dataGridViewCreaturePickpocketLoot.DataSource = DatabaseItemNameColumn("pickpocketing_loot_template", "Entry", textBoxCreatureTemplatePickID.Text.Trim(), 1, true);
                dataGridViewCreatureSkinLoot.DataSource = DatabaseItemNameColumn("skinning_loot_template", "Entry", textBoxCreatureTemplateSkinID.Text.Trim(), 1, true);
            }

            tabControlCategoryCreature.SelectedTab = tabPageCreatureTemplate;
        } 
        private void buttonCreatureTempGenerate_Click(object sender, EventArgs e)
        {
            textBoxCreatureScriptOutput.Text = DatabaseCreatureTempGenerate();
        }
        private void buttonCreatureScriptUpdate_Click(object sender, EventArgs e)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseAuth));

            if (ConnectionOpen(connect))
            {
                toolStripStatusLabelCreatureScriptRows.Text = "Row(s) Affected: " + DatabaseUpdate(connect, textBoxCreatureScriptOutput.Text).ToString();

                ConnectionClose(connect);
            }
        }

        private void newCreatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = new List<Tuple<TextBox, string>>();

            list.Add(new Tuple<TextBox, string>(textBoxCreatureTemplateName, ""));
            list.Add(new Tuple<TextBox, string>(textBoxCreatureTemplateSubname, ""));
            list.Add(new Tuple<TextBox, string>(textBoxCreatureTemplateSpeedWalk, "1"));
            list.Add(new Tuple<TextBox, string>(textBoxCreatureTemplateSpeedRun, "1.4286"));
            list.Add(new Tuple<TextBox, string>(textBoxCreatureTemplateName, ""));
            list.Add(new Tuple<TextBox, string>(textBoxCreatureTemplateAIName, ""));
            list.Add(new Tuple<TextBox, string>(textBoxCreatureTemplateScriptName, ""));

            DefaultValuesGenerate(tabPageCreatureTemplate);
            DefaultValuesOverride(list);

            tabControlCategoryCreature.SelectedTab = tabPageCreatureTemplate;
        }
        private void deleteCreatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateDeleteSelectedRow(dataGridViewCreatureSearch, "creature_template", "entry", textBoxCreatureScriptOutput);
        }

        private void buttonCreatureVendorEC_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Extended Cost Selection", ReadExcelCSV("ItemExtendedCost", 0, 1), textBoxCreatureVendorEC);
        }

        #region Loot
            private void buttonCreatureLootAdd_Click(object sender, EventArgs e)
            {
                var values = new object[] {
                    textBoxCreatureLootEntry.Text,
                    textBoxCreatureLootItemID.Text,
                    textBoxCreatureLootReference.Text,
                    textBoxCreatureLootChance.Text,
                    textBoxCreatureLootQR.Text,
                    textBoxCreatureLootLM.Text,
                    textBoxCreatureLootGID.Text,
                    textBoxCreatureLootMIC.Text,
                    textBoxCreatureLootMAC.Text
                };

                if (textBoxCreatureLootEntry.Text.Trim() != "")
                {
                    var existingData = (DataTable)dataGridViewCreatureLoot.DataSource;
                    existingData.Rows.Add(values);
                    dataGridViewCreatureLoot.DataSource = existingData;
                    dataGridViewCreatureLoot.FirstDisplayedScrollingRowIndex = dataGridViewCreatureLoot.Rows.Count - 1;
                }
            }
            private void buttonCreatureLootRefresh_Click(object sender, EventArgs e)
            {
                dataGridViewItemLoot.DataSource = DatabaseItemNameColumn("item_loot_template", "entry", (textBoxCreatureLootEntry.Text.Trim() != "") ? textBoxCreatureLootEntry.Text : textBoxCreatureTemplateEntry.Text, 1, true);
            }
            private void buttonCreatureLootDelete_Click(object sender, EventArgs e)
            {
                if (dataGridViewCreatureLoot.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridViewCreatureLoot.SelectedRows)
                    {
                        dataGridViewCreatureLoot.Rows.RemoveAt(row.Index);
                    }
                }
            }
            private void buttonCreatureLootGenerate_Click(object sender, EventArgs e)
        {
            GenerateDataColumn("creature_loot_template", dataGridViewCreatureLoot, textBoxCreatureScriptOutput);
        }
        #endregion
        #region Pickpocket
        private void buttonCreaturePickpocketAdd_Click(object sender, EventArgs e)
        {
            var values = new object[] {
                    textBoxCreaturePickpocketEntry.Text,
                    textBoxCreaturePickpocketItemID.Text,
                    textBoxCreaturePickpocketReference.Text,
                    textBoxCreaturePickpocketChance.Text,
                    textBoxCreaturePickpocketQR.Text,
                    textBoxCreaturePickpocketLM.Text,
                    textBoxCreaturePickpocketGID.Text,
                    textBoxCreaturePickpocketMIC.Text,
                    textBoxCreaturePickpocketMAC.Text
                };

            if (textBoxCreaturePickpocketEntry.Text.Trim() != "")
            {
                var existingData = (DataTable)dataGridViewCreaturePickpocketLoot.DataSource;
                existingData.Rows.Add(values);
                dataGridViewCreaturePickpocketLoot.DataSource = existingData;
                dataGridViewCreaturePickpocketLoot.FirstDisplayedScrollingRowIndex = dataGridViewCreaturePickpocketLoot.Rows.Count - 1;
            }
        }
        private void buttonCreaturePickpocketRefresh_Click(object sender, EventArgs e)
        {
            dataGridViewCreaturePickpocketLoot.DataSource = DatabaseItemNameColumn("pickpocketing_loot_template", "Entry", (textBoxCreaturePickpocketEntry.Text.Trim() != "") ? textBoxCreaturePickpocketEntry.Text : textBoxCreatureTemplateEntry.Text, 1, true);
        }
        private void buttonCreaturePickpocketDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCreaturePickpocketLoot.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewCreaturePickpocketLoot.SelectedRows)
                {
                    dataGridViewCreaturePickpocketLoot.Rows.RemoveAt(row.Index);
                }
            }
        }
        private void buttonCreaturePickpocketGenerate_Click(object sender, EventArgs e)
        {
            GenerateDataColumn("pickpocketing_loot_template", dataGridViewCreaturePickpocketLoot, textBoxCreatureScriptOutput);
        }
        #endregion
        #region Skin
        private void buttonCreatureSkinAdd_Click(object sender, EventArgs e)
        {
            var values = new object[] {
                    textBoxCreatureSkinEntry.Text,
                    textBoxCreatureSkinItemID.Text,
                    textBoxCreatureSkinReference.Text,
                    textBoxCreatureSkinChance.Text,
                    textBoxCreatureSkinQR.Text,
                    textBoxCreatureSkinLM.Text,
                    textBoxCreatureSkinGID.Text,
                    textBoxCreatureSkinMIC.Text,
                    textBoxCreatureSkinMAC.Text
                };

            if (textBoxCreatureSkinEntry.Text.Trim() != "")
            {
                var existingData = (DataTable)dataGridViewCreatureSkinLoot.DataSource;
                existingData.Rows.Add(values);
                dataGridViewCreatureSkinLoot.DataSource = existingData;
                dataGridViewCreatureSkinLoot.FirstDisplayedScrollingRowIndex = dataGridViewCreatureSkinLoot.Rows.Count - 1;
            }
        }
        private void buttonCreatureSkinRefresh_Click(object sender, EventArgs e)
        {
            dataGridViewCreatureSkinLoot.DataSource = DatabaseItemNameColumn("skinning_loot_template", "Entry", (textBoxCreatureSkinEntry.Text.Trim() != "") ? textBoxCreatureSkinEntry.Text : textBoxCreatureTemplateEntry.Text, 1, true);
        }
        private void buttonCreatureSkinDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCreatureSkinLoot.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewCreatureSkinLoot.SelectedRows)
                {
                    dataGridViewCreatureSkinLoot.Rows.RemoveAt(row.Index);
                }
            }
        }
        private void buttonCreatureSkinGenerate_Click(object sender, EventArgs e)
        {
            GenerateDataColumn("skinning_loot_template", dataGridViewCreatureSkinLoot, textBoxCreatureScriptOutput);
        }
        #endregion
        #region Vendor
        private void buttonCreatureVendorAdd_Click(object sender, EventArgs e)
        {
            var values = new object[] {
                    textBoxCreatureVendorEntry.Text,
                    textBoxCreatureVendorSlot.Text,
                    textBoxCreatureVendorItemID.Text,
                    textBoxCreatureVendorMAC.Text,
                    textBoxCreatureVendorIncrtime.Text,
                    textBoxCreatureVendorEC.Text
                };

            if (textBoxCreatureVendorEntry.Text.Trim() != "")
            {
                var existingData = (DataTable)dataGridViewCreatureVendor.DataSource;
                existingData.Rows.Add(values);
                dataGridViewCreatureVendor.DataSource = existingData;
                dataGridViewCreatureVendor.FirstDisplayedScrollingRowIndex = dataGridViewCreatureVendor.Rows.Count - 1;
            }
        }
        private void buttonCreatureVendorRefresh_Click(object sender, EventArgs e)
        {
            
            dataGridViewCreatureVendor.DataSource = DatabaseItemNameColumn("npc_vendor", "entry", (textBoxCreatureVendorEntry.Text.Trim() != "") ? textBoxCreatureVendorEntry.Text.Trim() : textBoxCreatureTemplateEntry.Text.Trim(), 2, true);
        }
        private void buttonCreatureVendorDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCreatureVendor.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewCreatureVendor.SelectedRows)
                {
                    dataGridViewCreatureVendor.Rows.RemoveAt(row.Index);
                }
            }
        }
        private void buttonCreatureVendorGenerate_Click(object sender, EventArgs e)
        {
            GenerateDataColumn("npc_vendor", dataGridViewCreatureVendor, textBoxCreatureScriptOutput);
        }
        #endregion

        #endregion
        #region Quest

        private void buttonQuestSearchSearch_Click(object sender, EventArgs e)
        {
            bool totalSearch = CheckEmptyControls(tabPageQuestSearch); DialogResult dr;

            string query = "SELECT ID, LogTitle, LogDescription FROM quest_template WHERE '1' = '1'";
            string qsQuery = " AND ID IN (SELECT quest FROM creature_queststarter WHERE id = '"+ textBoxQuestSearchGiver.Text +"')"; // queststart query
            string qeQuery = " AND ID IN (SELECT quest FROM creature_questender WHERE id = '"+ textBoxQuestSearchTaker.Text + "')"; // questender query

            if (totalSearch)
            {
                dr = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            } else
            {
                if (textBoxQuestSearchID.Text != "" || textBoxQuestSearchTitle.Text != "" || textBoxQuestSearchType.Text != "")
                {
                    query += DatabaseQueryFilter(textBoxQuestSearchID.Text, "ID");
                    query += DatabaseQueryFilter(textBoxQuestSearchTitle.Text, "logTitle");
                    query += DatabaseQueryFilter(textBoxQuestSearchType.Text, "QuestType");
                }

                if (textBoxQuestSearchGiver.Text != "")
                {
                    query += qsQuery;
                }

                if (textBoxQuestSearchTaker.Text != "")
                {
                    query += qeQuery;
                }

                dr = DialogResult.OK;
            }

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));
            
            if (ConnectionOpen(connect))
            {
                query += " ORDER BY ID;";
                DataSet combinedTable = DatabaseSearch(connect, query);

                dataGridViewQuestSearch.DataSource = combinedTable.Tables[0];
                toolStripStatusLabelQuestSearchRows.Text = "Quest(s) found: " + combinedTable.Tables[0].Rows.Count.ToString();

                ConnectionClose(connect);
            }
        }
        private void dataGridViewQuestSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewQuestSearch.Rows.Count != 0)
            {
                DatabaseQuestSearch(dataGridViewQuestSearch.SelectedCells[0].Value.ToString());

                tabControlCategoryQuest.SelectedTab = tabPageQuestSection1;
            }
        }
        private void buttonQuestSectionGenerate_Click(object sender, EventArgs e)
        {
            textBoxQuestScriptOutput.Text = DatabaseQuestSectionGenerate();
        }
        private void buttonQuestScriptUpdate_Click(object sender, EventArgs e)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            if (ConnectionOpen(connect))
            {
                toolStripStatusLabelQuestScriptRows.Text = "Row(s) Affected: " + DatabaseUpdate(connect, textBoxQuestScriptOutput.Text).ToString();

                ConnectionClose(connect);
            }
        }

        private void newQuestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = new List<Tuple<TextBox, string>>();

            list.Add(new Tuple<TextBox, string>(textBoxQuestSectionTitle, ""));
            list.Add(new Tuple<TextBox, string>(textBoxQuestSectionLDescription, ""));
            list.Add(new Tuple<TextBox, string>(textBoxQuestSectionQDescription, ""));
            list.Add(new Tuple<TextBox, string>(textBoxQuestSectionAreaDescription, ""));
            list.Add(new Tuple<TextBox, string>(textBoxQuestSectionCompleted, ""));
            list.Add(new Tuple<TextBox, string>(textBoxQuestSectionObjectives1, ""));
            list.Add(new Tuple<TextBox, string>(textBoxQuestSectionObjectives2, ""));
            list.Add(new Tuple<TextBox, string>(textBoxQuestSectionObjectives3, ""));
            list.Add(new Tuple<TextBox, string>(textBoxQuestSectionObjectives4, ""));

            DefaultValuesGenerate(tabPageQuestSection1);
            DefaultValuesGenerate(tabPageQuestSection2);
            DefaultValuesOverride(list);

            tabControlCategoryQuest.SelectedTab = tabPageQuestSection1;
        }
        private void deleteQuestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateDeleteSelectedRow(dataGridViewQuestSearch, "quest_template", "ID", textBoxQuestScriptOutput);
        }

        private void buttonQuestSectionReqRace_Click(object sender, EventArgs e)
        {

        }
        private void buttonQuestSectionReqClass_Click(object sender, EventArgs e)
        {

        }
        private void buttonQuestSectionQSort_Click(object sender, EventArgs e)
        {
            if (radioButtonQuestSectionZID.Checked)
            {
                CreatePopupSelection("Zone ID Selection", ReadExcelCSV("AreaTable", 0, 11), textBoxQuestSectionQSort);
            } else
            {
                CreatePopupSelection("Quest Sort Selection", ReadExcelCSV("QuestSort", 0, 1), textBoxQuestSectionQSort);
            }
        }
        private void buttonQuestSectionRewOtherTitleID_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Title Selection", ReadExcelCSV("CharTitles", 0, 2), textBoxQuestSectionRewOtherTitleID);
        }

        #endregion
        #region GameObject

        private void buttonGameObjectSearchSearch_Click(object sender, EventArgs e)
        {
            bool totalSearch = CheckEmptyControls(tabPageGameObjectSearch); DialogResult dr;

            string query = "SELECT entry, TYPE, NAME FROM gameobject_template WHERE '1' = '1'";

            if (totalSearch)
            {
                dr = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                query += DatabaseQueryFilter(textBoxGameObjectSearchEntry.Text, "entry");
                query += DatabaseQueryFilter(textBoxGameObjectSearchType.Text, "type");
                query += DatabaseQueryFilter(textBoxGameObjectSearchName.Text, "name");

                dr = DialogResult.OK;
            }

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            if (ConnectionOpen(connect))
            {
                query += " ORDER BY entry;";

                DataSet goTable = DatabaseSearch(connect, query);

                dataGridViewGameObjectSearch.DataSource = goTable.Tables[0];

                toolStripStatusLabelGameObjectSearchRows.Text = "Game Object(s): " + dataGridViewGameObjectSearch.Rows.Count.ToString();
                ConnectionClose(connect);
            }
        }
        private void dataGridViewGameObjectSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewGameObjectSearch.Rows.Count > 0)
            {
                DatabaseGameObjectSearch(dataGridViewGameObjectSearch.SelectedCells[0].Value.ToString());

                tabControlCategoryGameObject.SelectedTab = tabPageGameObjectTemplate;
            }
        }
        private void buttonGameObjectScriptUpdate_Click(object sender, EventArgs e)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            if (ConnectionOpen(connect))
            {
                toolStripStatusLabelGameObjectScriptRows.Text = "Row(s) Affected: " + DatabaseUpdate(connect, textBoxGameObjectScriptOutput.Text).ToString();

                ConnectionClose(connect);
            }
        }
        
        private void newGOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = new List<Tuple<TextBox, string>>();

            list.Add(new Tuple<TextBox, string>(textBoxGameObjectTempName, ""));
            list.Add(new Tuple<TextBox, string>(textBoxGameObjectTempSize, "1"));
            list.Add(new Tuple<TextBox, string>(textBoxGameObjectTempAIName, ""));
            list.Add(new Tuple<TextBox, string>(textBoxGameObjectTempScriptName, ""));

            DefaultValuesGenerate(tabPageGameObjectTemplate);
            DefaultValuesOverride(list);

            tabControlCategoryGameObject.SelectedTab = tabPageGameObjectTemplate;
        }
        private void deleteGOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateDeleteSelectedRow(dataGridViewGameObjectSearch, "gameobject_template", "entry", textBoxGameObjectScriptOutput);
        }

        #region POPUP
        private void buttonGameObjectTempType_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Game Object Type Selection", ReadExcelCSV("GameObjectTypes", 0, 1), textBoxGameObjectTempType);
        }
        private void buttonGameObjectTempFlags_Click(object sender, EventArgs e)
        {
            CreatePopupChecklist("Game Object Flags Selection", ReadExcelCSV("GameObjectFlags", 0, 1), textBoxGameObjectTempFlags, true);
        }
        #endregion

        #endregion
        #region Item

        private void buttonItemSearchSearch_Click(object sender, EventArgs e)
        {
            bool totalSearch = CheckEmptyControls(tabPageItemSearch); DialogResult dr;
            string query = "SELECT entry, NAME, class, subclass, quality, description, requiredlevel, itemlevel FROM item_template WHERE '1' = '1'";

            if (totalSearch)
            {
                dr = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                var qualityValue = (comboBoxItemSearchQuality.SelectedIndex.ToString() == "-1") ? "" : comboBoxItemSearchQuality.SelectedIndex.ToString();

                query += DatabaseQueryFilter(textBoxItemSearchEntry.Text, "entry");
                query += DatabaseQueryFilter(textBoxItemSearchName.Text, "name");
                query += DatabaseQueryFilter(textBoxItemSearchClass.Text, "class");
                query += DatabaseQueryFilter(textBoxItemSearchSubclass.Text, "subclass");
                query += DatabaseQueryFilter(qualityValue, "quality");
                query += DatabaseQueryFilter(textBoxItemSearchDescription.Text, "description");
                query += DatabaseQueryFilter(textBoxItemSearchReqLevel.Text, "requiredlevel");
                query += DatabaseQueryFilter(textBoxItemSearchILevel.Text, "itemlevel");

                dr = DialogResult.OK;
            }

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            if (ConnectionOpen(connect))
            {
                query += " ORDER BY entry;";

                DataSet itTable = DatabaseSearch(connect, query);

                dataGridViewItemSearch.DataSource = itTable.Tables[0];

                toolStripStatusLabelItemSearchRows.Text = "Item(s) found: " + dataGridViewItemSearch.Rows.Count.ToString();
                ConnectionClose(connect);
            }
        }
        private void dataGridViewItemSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewItemSearch.Rows.Count > 0)
            {
                tabControlCategoryItem.SelectedTab = tabPageItemTemplate;

                DatabaseItemSearch(dataGridViewItemSearch.SelectedCells[0].Value.ToString());

                dataGridViewItemLoot.DataSource = DatabaseItemNameColumn("item_loot_template", "entry", dataGridViewItemSearch.SelectedCells[0].Value.ToString(), 1, true);
                dataGridViewItemDE.DataSource = DatabaseItemNameColumn("disenchant_loot_template", "entry", textBoxItemTempDisenchantID.Text.Trim(), 1, true);
                dataGridViewItemMill.DataSource = DatabaseItemNameColumn("milling_loot_template", "entry", dataGridViewItemSearch.SelectedCells[0].Value.ToString(), 1, true);
                dataGridViewItemProspect.DataSource = DatabaseItemNameColumn("prospecting_loot_template", "entry", dataGridViewItemSearch.SelectedCells[0].Value.ToString(), 1, true);
            }
        }
        private void buttonItemScriptUpdate_Click(object sender, EventArgs e)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            if (ConnectionOpen(connect))
            {
                toolStripStatusLabelItemScriptRows.Text = "Row(s) Affected: " + DatabaseUpdate(connect, textBoxItemScriptOutput.Text).ToString();

                ConnectionClose(connect);
            }
        }
        private void buttonItemTempGenerate_Click(object sender, EventArgs e)
        {
            textBoxItemScriptOutput.Text += DatabaseItemTempGenerate();
        }

        private void newItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = new List<Tuple<TextBox, string>>();

            list.Add(new Tuple<TextBox, string>(textBoxItemTempName, ""));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempDescription, ""));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempReqRace, "1791"));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempReqClass, "-1"));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempCD1, "-1"));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempCategoryCD1, "-1"));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempCD2, "-1"));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempCategoryCD2, "-1"));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempCD3, "-1"));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempCategoryCD3, "-1"));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempCD4, "-1"));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempCategoryCD4, "-1"));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempCD5, "-1"));
            list.Add(new Tuple<TextBox, string>(textBoxItemTempCategoryCD5, "-1"));

            DefaultValuesGenerate(tabPageItemTemplate);
            DefaultValuesOverride(list);

            tabControlCategoryItem.SelectedTab = tabPageItemTemplate;
        }
        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateDeleteSelectedRow(dataGridViewItemSearch, "item_template", "entry", textBoxItemScriptOutput);
        }

        #region Loot
        private void buttonItemLootAdd_Click(object sender, EventArgs e)
        {
            var values = new object[] {
                textBoxItemLootEntry.Text,
                textBoxItemLootItemID.Text,
                textBoxItemLootReference.Text,
                textBoxItemLootChance.Text,
                textBoxItemLootQR.Text,
                textBoxItemLootLM.Text,
                textBoxItemLootGID.Text,
                textBoxItemLootMIC.Text,
                textBoxItemLootMAC.Text
            };

            if (textBoxItemLootEntry.Text.Trim() != "")
            {
                var existingData = (DataTable)dataGridViewItemLoot.DataSource;
                existingData.Rows.Add(values);
                dataGridViewItemLoot.DataSource = existingData;
            }
        }
        private void buttonItemLootRefresh_Click(object sender, EventArgs e)
        {
            dataGridViewItemLoot.DataSource = DatabaseItemNameColumn("item_loot_template", "entry", (textBoxItemLootEntry.Text.Trim() != "") ? textBoxItemLootEntry.Text : textBoxItemTempEntry.Text, 1, true);
        }
        private void buttonItemLootDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewItemLoot.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewItemLoot.SelectedRows)
                {
                    dataGridViewItemLoot.Rows.RemoveAt(row.Index);
                }
            }
        }
        private void buttonItemLootGenerate_Click(object sender, EventArgs e)
        {
            GenerateDataColumn("item_loot_template", dataGridViewItemLoot, textBoxItemScriptOutput);
        }
        #endregion
        #region Disenchant
        private void buttonItemDEAdd_Click(object sender, EventArgs e)
        {
            var values = new string[] {
                textBoxItemDEID.Text,
                textBoxItemDEItemID.Text,
                textBoxItemDEReference.Text,
                textBoxItemDEChance.Text,
                textBoxItemDEQR.Text,
                textBoxItemDELM.Text,
                textBoxItemDEGID.Text,
                textBoxItemDEMIC.Text,
                textBoxItemDEMAC.Text
            };

            if (textBoxItemDEID.Text.Trim() != "")
            {
                var existingData = (DataTable)dataGridViewItemDE.DataSource;
                existingData.Rows.Add(values);
                dataGridViewItemDE.DataSource = existingData;
            }
        }
        private void buttonItemDERefresh_Click(object sender, EventArgs e)
        {
            
            dataGridViewItemDE.DataSource = DatabaseItemNameColumn("disenchant_loot_template", "entry", (textBoxItemDEID.Text.Trim() != "") ? textBoxItemDEID.Text : textBoxItemTempDisenchantID.Text, 1, true);
        }
        private void buttonItemDEDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewItemDE.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewItemDE.SelectedRows)
                {
                    dataGridViewItemDE.Rows.RemoveAt(row.Index);
                }
            }
        }
        private void buttonItemDEGenerate_Click(object sender, EventArgs e)
        {
            GenerateDataColumn("disenchant_loot_template", dataGridViewItemProspect, textBoxItemScriptOutput);
        }
        #endregion
        #region Milling
        private void buttonItemMillAdd_Click(object sender, EventArgs e)
        {
            var values = new string[] {
                textBoxItemMillEntry.Text,
                textBoxItemMillItemID.Text,
                textBoxItemMillReference.Text,
                textBoxItemMillChance.Text,
                textBoxItemMillQR.Text,
                textBoxItemMillLM.Text,
                textBoxItemMillGID.Text,
                textBoxItemMillMIC.Text,
                textBoxItemMillMAC.Text
            };

            if (textBoxItemMillEntry.Text.Trim() != "")
            {
                var existingData = (DataTable)dataGridViewItemMill.DataSource;
                existingData.Rows.Add(values);
                dataGridViewItemMill.DataSource = existingData;
            }
        }
        private void buttonItemMillRefresh_Click(object sender, EventArgs e)
        {
            dataGridViewItemMill.DataSource = DatabaseItemNameColumn("milling_loot_template", "entry", (textBoxItemMillEntry.Text.Trim() != "") ? textBoxItemMillEntry.Text.Trim() : textBoxItemTempEntry.Text, 1, true);
        }
        private void buttonItemMillDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewItemMill.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewItemMill.SelectedRows)
                {
                    dataGridViewItemMill.Rows.RemoveAt(row.Index);
                }
            }
        }
        private void buttonItemMillGenerate_Click(object sender, EventArgs e)
        {
            GenerateDataColumn("milling_loot_template", dataGridViewItemMill, textBoxItemScriptOutput);
        }
        #endregion
        #region Prospecting
        private void buttonItemProspectAdd_Click(object sender, EventArgs e)
        {
            var values = new string[] {
                textBoxItemProspectEntry.Text,
                textBoxItemProspectItemID.Text,
                textBoxItemProspectReference.Text,
                textBoxItemProspectChance.Text,
                textBoxItemProspectQR.Text,
                textBoxItemProspectLM.Text,
                textBoxItemProspectGID.Text,
                textBoxItemProspectMIC.Text,
                textBoxItemProspectMAC.Text
            };

            if (textBoxItemProspectEntry.Text.Trim() != "")
            {
                var existingData = (DataTable)dataGridViewItemProspect.DataSource;
                existingData.Rows.Add(values);
                dataGridViewItemProspect.DataSource = existingData;
            }
        }
        private void buttonItemProspectRefresh_Click(object sender, EventArgs e)
        {
            dataGridViewItemProspect.DataSource = DatabaseItemNameColumn("prospecting_loot_template", "entry", (textBoxItemProspectEntry.Text.Trim() != "") ? textBoxItemProspectEntry.Text.Trim() : textBoxItemTempEntry.Text, 1, true);
        }
        private void buttonItemProspectDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewItemProspect.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewItemProspect.SelectedRows)
                {
                    dataGridViewItemProspect.Rows.RemoveAt(row.Index);
                }
            }
        }
        private void buttonItemProspectGenerate_Click(object sender, EventArgs e)
        {
            GenerateDataColumn("prospecting_loot_template", dataGridViewItemProspect, textBoxItemScriptOutput);
        }
        #endregion

        #region POPUPS
        private void buttonItemSearchClass_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Class Selection", DataItemClass(), textBoxItemSearchClass);
        }
        private void buttonItemSearchSubclass_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Subclass Selection", DataItemSubclass(textBoxItemSearchClass.Text.Trim()), textBoxItemSearchSubclass);
        }
        private void buttonItemTempRace_Click(object sender, EventArgs e)
        {
            CreatePopupChecklist("Race Requirement", ReadExcelCSV("ChrRaces", 0, 14), textBoxItemTempReqRace, true);
        }
        private void buttonItemTempClass_Click(object sender, EventArgs e)
        {
            CreatePopupChecklist("Class Requirement", ReadExcelCSV("ChrClasses", 0, 4), textBoxItemTempReqClass, true);
        }
        private void buttonItemTempDmgType1_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Damage Type I Selection", ReadExcelCSV("ItemDamageTypes", 0, 1), textBoxItemTempDmgType1);
        }
        private void buttonItemTempDmgType2_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Damage Type II Selection", ReadExcelCSV("ItemDamageTypes", 0, 1), textBoxItemTempDmgType2);
        }
        private void buttonItemTempStatsType1_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Stat Selection I", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType1);
        }
        private void buttonItemTempStatsType2_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Stat Selection II", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType2);
        }
        private void buttonItemTempStatsType3_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Stat Selection III", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType3);
        }
        private void buttonItemTempStatsType4_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Stat Selection IV", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType4);
        }
        private void buttonItemTempStatsType5_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Stat Selection V", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType5);
        }
        private void buttonItemTempStatsType6_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Stat Selection VI", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType6);
        }
        private void buttonItemTempStatsType7_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Stat Selection VII", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType7);
        }
        private void buttonItemTempStatsType8_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Stat Selection VIII", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType8);
        }
        private void buttonItemTempStatsType9_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Stat Selection IX", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType9);
        }
        private void buttonItemTempStatsType10_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Stat Selection X", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType10);
        }
        private void buttonItemTempTypeClass_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Class Selection", DataItemClass(), textBoxItemTempTypeClass);
        }
        private void buttonItemTempSubclass_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Subclass Selection", DataItemSubclass(textBoxItemTempTypeClass.Text.Trim()), textBoxItemTempSubclass);
        }
        private void buttonItemTempQuality_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Quality Selection", ReadExcelCSV("ItemQuality", 0, 1), textBoxItemTempQuality);
        }
        private void buttonItemTempItemSet_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("ItemSet Selection", ReadExcelCSV("ItemSet", 0, 1), textBoxItemTempItemSet);
        }
        private void buttonItemTempBonding_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Bonding Selection", ReadExcelCSV("ItemBondings", 0, 1), textBoxItemTempBonding);
        }
        private void buttonItemTempSheath_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Sheath Selection", ReadExcelCSV("ItemSheaths", 0, 1), textBoxItemTempSheath);
        }
        private void buttonItemTempColor1_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Color Selection I", ReadExcelCSV("ItemSocketColors", 0, 1), textBoxItemTempColor1);
        }
        private void buttonItemTempColor2_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Color Selection II", ReadExcelCSV("ItemSocketColors", 0, 1), textBoxItemTempColor2);
        }
        private void buttonItemTempColor3_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Color Selection III", ReadExcelCSV("ItemSocketColors", 0, 1), textBoxItemTempColor3);
        }
        private void buttonItemTempSocketBonus_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Socket Bonus Selection III", ReadExcelCSV("ItemSocketBonus", 0, 1), textBoxItemTempSocketBonus);
        }
        private void buttonItemTempGemProper_Click(object sender, EventArgs e)
        {
            CreatePopupSelection("Gem Property Selection", ReadExcelCSV("GemProperties", 0, 1), textBoxItemTempGemProper);
        }







        #endregion

        #endregion

        #endregion

    }


}
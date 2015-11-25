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

        #region Everything

        #region GlobalEvents

        private void FormMain_Load(object sender, EventArgs e)
        {
            tabControlCategory.SelectedTab = tabPageItem;

            if (FormMySQL.Offline == true)
            {
                tabControlCategory.Enabled = false;
            }

            var textboxToolTip = new ToolTip();
            
            textboxToolTip.InitialDelay = 100;
            textboxToolTip.ShowAlways = true;

            textboxToolTip.SetToolTip(textBoxAccountSearchID, "Account Identifier.");

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fa = new FormAbout();
            fa.ShowDialog();
        }

        #endregion
        #region CustomFunctions

        #region GlobalFunctions

            // Allow childs form to change the enable state of formmain.
        public bool EnableTabs
        {
            set { this.Enabled = value; }
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

                return itTable.Tables[0].Rows[0][0].ToString();
            }

            ConnectionClose(connect);

            return "";
        }
            // Its a method to check a textbox if it has text, if it does, add the query and 'OR' to the query.
            // The ntextbox is next textbox, if thats not empty, it will
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
        #region DataTables
        private DataTable DataItemClass()
        {
            var iClass = new DataTable();
            iClass.Columns.Add("id", typeof(string));
            iClass.Columns.Add("value", typeof(string));

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
            iSubclass.Columns.Add("value", typeof(string));

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
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "MySQL Error: " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
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
                
                tabControlCategoryAccount.SelectedTab = tabPageAccountAccount;
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
                string characterQuery = "SELECT guid, account, NAME, race, class, gender, LEVEL, money, xp, chosentitle, online, cinematic, is_logout_resting," +
                    "map, instance_id, zone, orientation, position_x, position_y, position_z, " +
                    "totalHonorPoints, arenaPoints, totalKills, " +
                    "health, power1, power2, power3, power4, power5, power6, power7, " +
                    "equipmentCache, knownTitles, exploredZones, taxi_path " +
                    "FROM characters WHERE guid = '" + characterGUID + "';";

                DataSet CharacterTable = DatabaseSearch(connect, characterQuery);

                if (CharacterTable.Tables[0].Rows.Count != 0)
                {
                    // General Information
                    textBoxCharacterCharacterGUID.Text = CharacterTable.Tables[0].Rows[0][0].ToString();
                    textBoxCharacterCharacterAccount.Text = CharacterTable.Tables[0].Rows[0][1].ToString();
                    textBoxCharacterCharacterName.Text = CharacterTable.Tables[0].Rows[0][2].ToString();
                    textBoxCharacterCharacterRace.Text = CharacterTable.Tables[0].Rows[0][3].ToString();
                    textBoxCharacterCharacterClass.Text = CharacterTable.Tables[0].Rows[0][4].ToString();
                    textBoxCharacterCharacterGender.Text = CharacterTable.Tables[0].Rows[0][5].ToString();
                    textBoxCharacterCharacterLevel.Text = CharacterTable.Tables[0].Rows[0][6].ToString();
                    textBoxCharacterCharacterMoney.Text = CharacterTable.Tables[0].Rows[0][7].ToString();
                    textBoxCharacterCharacterXP.Text = CharacterTable.Tables[0].Rows[0][8].ToString();
                    textBoxCharacterCharacterTitle.Text = CharacterTable.Tables[0].Rows[0][9].ToString();
                    checkBoxCharacterCharacterOnline.Checked = Convert.ToBoolean(CharacterTable.Tables[0].Rows[0][10]);
                    checkBoxCharacterCharacterCinematic.Checked = Convert.ToBoolean(CharacterTable.Tables[0].Rows[0][11]);
                    checkBoxCharacterCharacterRest.Checked = Convert.ToBoolean(CharacterTable.Tables[0].Rows[0][12]);
                    // Location
                    textBoxCharacterCharacterMapID.Text = CharacterTable.Tables[0].Rows[0][13].ToString();
                    textBoxCharacterCharacterInstanceID.Text = CharacterTable.Tables[0].Rows[0][14].ToString();
                    textBoxCharacterCharacterZoneID.Text = CharacterTable.Tables[0].Rows[0][15].ToString();
                    textBoxCharacterCharacterCoordO.Text = CharacterTable.Tables[0].Rows[0][16].ToString();
                    textBoxCharacterCharacterCoordX.Text = CharacterTable.Tables[0].Rows[0][17].ToString();
                    textBoxCharacterCharacterCoordY.Text = CharacterTable.Tables[0].Rows[0][18].ToString();
                    textBoxCharacterCharacterCoordZ.Text = CharacterTable.Tables[0].Rows[0][19].ToString();
                    // Player vs Player
                    textBoxCharacterCharacterHonorPoints.Text = CharacterTable.Tables[0].Rows[0][20].ToString();
                    textBoxCharacterCharacterArenaPoints.Text = CharacterTable.Tables[0].Rows[0][21].ToString();
                    textBoxCharacterCharacterTotalKills.Text = CharacterTable.Tables[0].Rows[0][22].ToString();
                    // Stats
                    textBoxCharacterCharacterHealth.Text = CharacterTable.Tables[0].Rows[0][23].ToString();
                    textBoxCharacterCharacterPower1.Text = CharacterTable.Tables[0].Rows[0][24].ToString();
                    textBoxCharacterCharacterPower2.Text = CharacterTable.Tables[0].Rows[0][25].ToString();
                    textBoxCharacterCharacterPower3.Text = CharacterTable.Tables[0].Rows[0][26].ToString();
                    textBoxCharacterCharacterPower4.Text = CharacterTable.Tables[0].Rows[0][27].ToString();
                    textBoxCharacterCharacterPower5.Text = CharacterTable.Tables[0].Rows[0][28].ToString();
                    textBoxCharacterCharacterPower6.Text = CharacterTable.Tables[0].Rows[0][29].ToString();
                    textBoxCharacterCharacterPower7.Text = CharacterTable.Tables[0].Rows[0][30].ToString();
                    // Unknown
                    textBoxCharacterCharacterEquipmentCache.Text = CharacterTable.Tables[0].Rows[0][31].ToString();
                    textBoxCharacterCharacterKnownTitles.Text = CharacterTable.Tables[0].Rows[0][32].ToString();
                    textBoxCharacterCharacterExploredZones.Text = CharacterTable.Tables[0].Rows[0][33].ToString();
                    textBoxCharacterCharacterTaxiMask.Text = CharacterTable.Tables[0].Rows[0][34].ToString();
                }

                tabControlCategoryCharacter.SelectedTab = tabPageCharacterCharacter;
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

                DataSet iTable = DatabaseSearch(connect, inventoryQuery);

                if (iTable.Tables[0].Rows.Count != 0)
                {
                    // Adds a new column 'name'
                    iTable.Tables[0].Columns.Add("name", typeof(string));
                    
                    // loops every inventory item for name
                    for (int i = 0; i < iTable.Tables[0].Rows.Count; i++)
                    {   
                        // sets the column 'name' to the itemname
                        iTable.Tables[0].Rows[i]["name"] = DatabaseItemGetName( DatabaseItemGetEntry( Convert.ToUInt32(iTable.Tables[0].Rows[i][3]) ) );
                    }

                    dataGridViewCharacterInventory.DataSource = iTable.Tables[0];
                }
            }

            ConnectionClose(connect);
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

                DataSet ctTable = DatabaseSearch(connect, query);

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

                tabControlCategoryCreature.SelectedTab = tabPageCreatureTemplate;
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
            // Searches the database for the creature's loot
        private void DatabaseCreatureLoot(string lootID)
        {
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

            if (ConnectionOpen(connect))
            {
                string query = "SELECT * FROM creature_loot_template WHERE Entry = '" + lootID + "';";

                // Creature Loot Template Table
                DataSet cltTable = DatabaseSearch(connect, query);

                dataGridViewCreatureLootTemplate.DataSource = cltTable.Tables[0];

                ConnectionClose(connect);
            }
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
                textBoxQuestSectionReqNPCID1.Text = qtTable.Tables[0].Rows[0]["RequiredNpcOrGo1"].ToString();
                textBoxQuestSectionReqNPCC1.Text = qtTable.Tables[0].Rows[0]["RequiredNpcOrGoCount1"].ToString();
                textBoxQuestSectionReqNPCID2.Text = qtTable.Tables[0].Rows[0]["RequiredNpcOrGo2"].ToString();
                textBoxQuestSectionReqNPCC2.Text = qtTable.Tables[0].Rows[0]["RequiredNpcOrGoCount2"].ToString();
                textBoxQuestSectionReqNPCID3.Text = qtTable.Tables[0].Rows[0]["RequiredNpcOrGo3"].ToString();
                textBoxQuestSectionReqNPCC3.Text = qtTable.Tables[0].Rows[0]["RequiredNpcOrGoCount3"].ToString();
                textBoxQuestSectionReqNPCID4.Text = qtTable.Tables[0].Rows[0]["RequiredNpcOrGo4"].ToString();
                textBoxQuestSectionReqNPCC4.Text = qtTable.Tables[0].Rows[0]["RequiredNpcOrGoCount4"].ToString();

                textBoxQuestSectionReqItemID1.Text = qtTable.Tables[0].Rows[0]["RequiredItemId1"].ToString();
                textBoxQuestSectionReqItemC1.Text = qtTable.Tables[0].Rows[0]["RequiredItemCount1"].ToString();
                textBoxQuestSectionReqItemID2.Text = qtTable.Tables[0].Rows[0]["RequiredItemId2"].ToString();
                textBoxQuestSectionReqItemC2.Text = qtTable.Tables[0].Rows[0]["RequiredItemCount2"].ToString();
                textBoxQuestSectionReqItemID3.Text = qtTable.Tables[0].Rows[0]["RequiredItemId3"].ToString();
                textBoxQuestSectionReqItemC3.Text = qtTable.Tables[0].Rows[0]["RequiredItemCount3"].ToString();
                textBoxQuestSectionReqItemID4.Text = qtTable.Tables[0].Rows[0]["RequiredItemId4"].ToString();
                textBoxQuestSectionReqItemC4.Text = qtTable.Tables[0].Rows[0]["RequiredItemCount4"].ToString();
                textBoxQuestSectionReqItemID5.Text = qtTable.Tables[0].Rows[0]["RequiredItemId5"].ToString();
                textBoxQuestSectionReqItemC5.Text = qtTable.Tables[0].Rows[0]["RequiredItemCount5"].ToString();
                textBoxQuestSectionReqItemID6.Text = qtTable.Tables[0].Rows[0]["RequiredItemId6"].ToString();
                textBoxQuestSectionReqItemC6.Text = qtTable.Tables[0].Rows[0]["RequiredItemCount6"].ToString();

                // Rewards
                textBoxQuestSectionRewItemID1.Text = qtTable.Tables[0].Rows[0]["RewardItem1"].ToString();
                textBoxQuestSectionRewItemC1.Text = qtTable.Tables[0].Rows[0]["RewardAmount1"].ToString();
                textBoxQuestSectionRewItemID2.Text = qtTable.Tables[0].Rows[0]["RewardItem2"].ToString();
                textBoxQuestSectionRewItemC2.Text = qtTable.Tables[0].Rows[0]["RewardAmount2"].ToString();
                textBoxQuestSectionRewItemID3.Text = qtTable.Tables[0].Rows[0]["RewardItem3"].ToString();
                textBoxQuestSectionRewItemC3.Text = qtTable.Tables[0].Rows[0]["RewardAmount3"].ToString();
                textBoxQuestSectionRewItemID4.Text = qtTable.Tables[0].Rows[0]["RewardItem4"].ToString();
                textBoxQuestSectionRewItemC4.Text = qtTable.Tables[0].Rows[0]["RewardAmount4"].ToString();

                textBoxQuestSectionRewChoiceID1.Text = qtTable.Tables[0].Rows[0]["RewardChoiceItemID1"].ToString();
                textBoxQuestSectionRewChoiceC1.Text = qtTable.Tables[0].Rows[0]["RewardChoiceItemQuantity1"].ToString();
                textBoxQuestSectionRewChoiceID2.Text = qtTable.Tables[0].Rows[0]["RewardChoiceItemID2"].ToString();
                textBoxQuestSectionRewChoiceC2.Text = qtTable.Tables[0].Rows[0]["RewardChoiceItemQuantity2"].ToString();
                textBoxQuestSectionRewChoiceID3.Text = qtTable.Tables[0].Rows[0]["RewardChoiceItemID3"].ToString();
                textBoxQuestSectionRewChoiceC3.Text = qtTable.Tables[0].Rows[0]["RewardChoiceItemQuantity3"].ToString();
                textBoxQuestSectionRewChoiceID4.Text = qtTable.Tables[0].Rows[0]["RewardChoiceItemID4"].ToString();
                textBoxQuestSectionRewChoiceC4.Text = qtTable.Tables[0].Rows[0]["RewardChoiceItemQuantity4"].ToString();
                textBoxQuestSectionRewChoiceID5.Text = qtTable.Tables[0].Rows[0]["RewardChoiceItemID5"].ToString();
                textBoxQuestSectionRewChoiceC5.Text = qtTable.Tables[0].Rows[0]["RewardChoiceItemQuantity5"].ToString();
                textBoxQuestSectionRewChoiceID6.Text = qtTable.Tables[0].Rows[0]["RewardChoiceItemID6"].ToString();
                textBoxQuestSectionRewChoiceC6.Text = qtTable.Tables[0].Rows[0]["RewardChoiceItemQuantity6"].ToString();

                textBoxQuestSectionRewOtherSpell.Text = qtTable.Tables[0].Rows[0]["RewardDisplaySpell"].ToString();
                textBoxQuestSectionRewOtherSpellCast.Text = qtTable.Tables[0].Rows[0]["RewardSpell"].ToString();
                textBoxQuestSectionRewOtherMoney.Text = qtTable.Tables[0].Rows[0]["RewardMoney"].ToString();
                textBoxQuestSectionRewOtherMoneyML.Text = qtTable.Tables[0].Rows[0]["RewardBonusMoney"].ToString();
                textBoxQuestSectionRewOtherMailID.Text = qtTable.Tables[1].Rows[0]["RewardMailTemplateID"].ToString();
                textBoxQuestSectionRewOtherTitleID.Text = qtTable.Tables[1].Rows[0]["RewardMailTemplateID"].ToString();
                textBoxQuestSectionRewOtherTP.Text = qtTable.Tables[1].Rows[0]["RewardMailTemplateID"].ToString();
                textBoxQuestSectionRewOtherHP.Text = qtTable.Tables[0].Rows[0]["RewardHonor"].ToString();
                textBoxQuestSectionRewOtherAP.Text = qtTable.Tables[0].Rows[0]["RewardArenaPoints"].ToString();
                textBoxQuestSectionRewOtherTP.Text = qtTable.Tables[0].Rows[0]["RewardTalents"].ToString();

                #endregion

                tabControlCategoryQuest.SelectedTab = tabPageQuestSection1;
            }
        }
        #endregion
        #region GameObject
        private void DatabaseGameObjectSearch(string GameobjectEntryID)
        {

        }
        #endregion
        #region Item
        private void DatabaseItemSearch(string ItemEntryID)
        {

        }
        #endregion

        #endregion
        #region Tabs

        #region Account
        private void buttonAccountSearchButton_Click(object sender, EventArgs e)
        {
            bool totalSearch = CheckEmptyControls(tabPageAccountSearch); DialogResult dr;

            string query = "SELECT id, username, email, expansion FROM account WHERE '1' = '1'";

            if (totalSearch)
            {
                dr = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            } else
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
                toolStripStatusLabelAccountSearchRow.Text = "Account(s) found: " + combinedTable.Tables[0].Rows.Count.ToString();

                ConnectionClose(connect);
            }
        }

        private void dataGridViewAccountSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewAccountSearch.RowCount != 0)
            {
                DatabaseAccountSearch(dataGridViewAccountSearch.SelectedCells[0].Value.ToString());
            }
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
            var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseAuth));
            
            if (ConnectionOpen(connect))
            {
                int rows = DatabaseUpdate(connect, textBoxAccountScript.Text.Trim());
                toolStripStatusLabelAccountScriptResult.Text = "Row(s) affected: " + rows.ToString();
                ConnectionClose(connect);
            } 
        }

        #endregion AccountSection
        #region Character
            // Character search
        private void buttonCharacterSearchSearch_Click(object sender, EventArgs e)
        {
            bool totalSearch = CheckEmptyControls(tabPageAccountSearch); DialogResult dr;

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
                toolStripStatusLabelCharacterSearchRow.Text = "Character(s) found: " + combinedTable.Tables[0].Rows.Count.ToString();

                ConnectionClose(connect);
            }
        }
            // Cell Doubleclick.
        private void dataGridViewCharacterSearchSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCharacterSearch.RowCount != 0)
            {
                DatabaseCharacterSearch(dataGridViewCharacterSearch.SelectedCells[0].Value.ToString());
                DatabaseCharacterInventory(dataGridViewCharacterSearch.SelectedCells[0].Value.ToString());
            }
        }

        #endregion
        #region Creature
            // Search button
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
                toolStripStatusLabelCreatureSearchRow.Text = "Creature(s) found: " + ctTable.Tables[0].Rows.Count.ToString();

                ConnectionClose(connect);
            }
        }

        private void dataGridViewCreatureSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCreatureSearch.Rows.Count != 0)
            {
                DatabaseCreatureSearch(dataGridViewCreatureSearch.SelectedCells[0].Value.ToString());
                DatabaseCreatureLocation(dataGridViewCreatureSearch.SelectedCells[0].Value.ToString());
                DatabaseCreatureLoot(dataGridViewCreatureSearch.SelectedCells[6].Value.ToString());
            }
        } 

        #endregion
        #region Quest

            // Quest Search
        private void buttonQuestSearch_Click(object sender, EventArgs e)
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
                toolStripStatusLabelQuestSearchRow.Text = "Quest(s) found: " + combinedTable.Tables[0].Rows.Count.ToString();

                ConnectionClose(connect);
            }
        }
            // Cell Doubleclick
        private void dataGridViewQuestSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewQuestSearch.Rows.Count != 0)
            {
                DatabaseQuestSearch(dataGridViewQuestSearch.SelectedCells[0].Value.ToString());
            }
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

                toolStripStatusLabelGameObjectSearchRow.Text = "Game Object(s): " + dataGridViewGameObjectSearch.Rows.Count.ToString();
                ConnectionClose(connect);
            }
        }

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

                toolStripStatusLabelItemSearchRow.Text = "Item(s): " + dataGridViewItemSearch.Rows.Count.ToString();
                ConnectionClose(connect);
            }
        }        
        private void buttonItemSearchToolClass_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            var toolClass = new FormPopup.FormToolSelection();
            toolClass.setDataTable = DataItemClass();
            toolClass.setFormText = "Class Selection";
            toolClass.Owner = this;
            toolClass.ShowDialog();

            this.Activate();
            textBoxItemSearchClass.Text = (toolClass.getSelection == "") ? textBoxItemSearchClass.Text : toolClass.getSelection;
        }
        private void buttonItemSearchToolSubclass_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            var toolSubclass = new FormPopup.FormToolSelection();
            toolSubclass.setDataTable = DataItemSubclass(textBoxItemSearchClass.Text.Trim());
            toolSubclass.setFormText = "Subclass Selection";
            toolSubclass.Owner = this;
            toolSubclass.ShowDialog();

            this.Activate();
            textBoxItemSearchSubclass.Text = (toolSubclass.getSelection == "") ? textBoxItemSearchSubclass.Text : toolSubclass.getSelection;
        }


        #endregion

        #endregion

        #endregion

        private void controlPanelToolStripMenuItemTools_Click(object sender, EventArgs e)
        {
            var CP = new FormTools.FormControlPanel();

            CP.StartPosition = FormStartPosition.CenterScreen;
            CP.Show(this);
        }
    }
}
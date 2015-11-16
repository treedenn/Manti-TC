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

            tabControlCategory.SelectedTab = tabPageCreature;
        }

        #region CustomFunctions

        #region DatabaseFunctions

            // Generates the string required to create a connection
        private static string DatabaseString(string database)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

            builder.Server = MySQLWindow.Andress;
            builder.UserID = MySQLWindow.Username;
            builder.Password = MySQLWindow.Password;
            builder.Port = MySQLWindow.Port;
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
            DataSet ds = new DataSet();
            
            if (connect.State == ConnectionState.Open)
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, connect);

                da.Fill(ds);
            }

            return ds;
        }
            // Updates the database with a specific query and returns the row affected.
        private int DatabaseUpdate(MySqlConnection connect, string sqlQuery)
        {
            if (connect.State == ConnectionState.Open)
            {
                MySqlCommand query = new MySqlCommand(sqlQuery, connect);

                return query.ExecuteNonQuery();
            }

            return 0;
        }

        #endregion
        #region Account

        // Searches the data for a specific account.
        private void DatabaseAccountSearch(string accountID)
        {
            MySqlConnection connect = new MySqlConnection(DatabaseString(MySQLWindow.DatabaseAuth));

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
            MySqlConnection connect = new MySqlConnection(DatabaseString(MySQLWindow.DatabaseCharacters));

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
            MySqlConnection connect = new MySqlConnection(DatabaseString(MySQLWindow.DatabaseCharacters));

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
            MySqlConnection connect = new MySqlConnection(DatabaseString(MySQLWindow.DatabaseWorld));

            if (ConnectionOpen(connect))
            {

                string query = "SELECT entry, difficulty_entry_1, difficulty_entry_2, difficulty_entry_3, NAME, subname, " +
                    "modelid1, modelid2, modelid3, modelid4, minlevel, maxlevel, mingold, maxgold, KillCredit1, KillCredit2, rank, scale, faction, npcflag, " + // 19

                    "HealthModifier, ManaModifier, ArmorModifier, DamageModifier, ExperienceModifier, " + // 24

                    "BaseAttackTime, RangeAttackTime, BaseVariance, RangeVariance, dmgschool, " + // 29
                    "AIName, MovementType, InhabitType, HoverHeight, gossip_menu_id, movementId, ScriptName, VehicleId, " + // 37

                    "trainer_type, trainer_spell, trainer_class, trainer_race, lootid, pickpocketloot, skinloot, " + // 44
                    "resistance1, resistance2, resistance3, resistance4, resistance5, resistance6, " + // 50

                    "RegenHealth, mechanic_immune_mask, family, TYPE, type_flags, flags_extra, unit_class, unit_flags, unit_flags2, dynamicflags, " + // 60

                    "speed_walk, speed_run, " + // 62

                    "spell1, spell2, spell3, spell4, spell5, spell6, spell7, spell8 " + // 69

                    "FROM creature_template WHERE entry = '" + creatureEntryID + "'; ";

                DataSet ctTable = DatabaseSearch(connect, query);

                textBoxCreatureTemplateEntry.Text           = ctTable.Tables[0].Rows[0][0].ToString();
                textBoxCreatureTemplateDifEntry1.Text       = ctTable.Tables[0].Rows[0][1].ToString();
                textBoxCreatureTemplateDifEntry2.Text       = ctTable.Tables[0].Rows[0][2].ToString();
                textBoxCreatureTemplateDifEntry3.Text       = ctTable.Tables[0].Rows[0][3].ToString();
                textBoxCreatureTemplateName.Text            = ctTable.Tables[0].Rows[0][4].ToString();
                textBoxCreatureTemplateSubname.Text         = ctTable.Tables[0].Rows[0][5].ToString();

                textBoxCreatureTemplateModelID1.Text        = ctTable.Tables[0].Rows[0][6].ToString();
                textBoxCreatureTemplateModelID2.Text        = ctTable.Tables[0].Rows[0][7].ToString();
                textBoxCreatureTemplateModelID3.Text        = ctTable.Tables[0].Rows[0][8].ToString();
                textBoxCreatureTemplateModelID4.Text        = ctTable.Tables[0].Rows[0][9].ToString();
                textBoxCreatureTemplateLevelMin.Text        = ctTable.Tables[0].Rows[0][10].ToString();
                textBoxCreatureTemplateLevelMax.Text        = ctTable.Tables[0].Rows[0][11].ToString();
                textBoxCreatureTemplateGoldMin.Text         = ctTable.Tables[0].Rows[0][12].ToString();
                textBoxCreatureTemplateGoldMax.Text         = ctTable.Tables[0].Rows[0][13].ToString();
                textBoxCreatureTemplateKillCredit1.Text     = ctTable.Tables[0].Rows[0][14].ToString();
                textBoxCreatureTemplateKillCredit2.Text     = ctTable.Tables[0].Rows[0][15].ToString();
                textBoxCreatureTemplateRank.Text            = ctTable.Tables[0].Rows[0][16].ToString();
                textBoxCreatureTemplateScale.Text           = ctTable.Tables[0].Rows[0][17].ToString();
                textBoxCreatureTemplateFaction.Text         = ctTable.Tables[0].Rows[0][18].ToString();
                textBoxCreatureTemplateNPCFlags.Text        = ctTable.Tables[0].Rows[0][19].ToString();

                textBoxCreatureTemplateModHealth.Text       = ctTable.Tables[0].Rows[0][20].ToString();
                textBoxCreatureTemplateModMana.Text         = ctTable.Tables[0].Rows[0][21].ToString();
                textBoxCreatureTemplateModArmor.Text        = ctTable.Tables[0].Rows[0][22].ToString();
                textBoxCreatureTemplateModDamage.Text       = ctTable.Tables[0].Rows[0][23].ToString();
                textBoxCreatureTemplateModExperience.Text   = ctTable.Tables[0].Rows[0][24].ToString();

                textBoxCreatureTemplateBaseAttack.Text      = ctTable.Tables[0].Rows[0][25].ToString();
                textBoxCreatureTemplateRangedAttack.Text    = ctTable.Tables[0].Rows[0][26].ToString();
                textBoxCreatureTemplateBV.Text              = ctTable.Tables[0].Rows[0][27].ToString();
                textBoxCreatureTemplateRV.Text              = ctTable.Tables[0].Rows[0][28].ToString();
                textBoxCreatureTemplateDS.Text              = ctTable.Tables[0].Rows[0][29].ToString();

                textBoxCreatureTemplateAIName.Text          = ctTable.Tables[0].Rows[0][30].ToString();
                textBoxCreatureTemplateMType.Text           = ctTable.Tables[0].Rows[0][31].ToString();
                textBoxCreatureTemplateInhabitType.Text     = ctTable.Tables[0].Rows[0][32].ToString();
                textBoxCreatureTemplateHH.Text              = ctTable.Tables[0].Rows[0][33].ToString();
                textBoxCreatureTemplateGMID.Text            = ctTable.Tables[0].Rows[0][34].ToString();
                textBoxCreatureTemplateMID.Text             = ctTable.Tables[0].Rows[0][35].ToString();
                textBoxCreatureTemplateScriptName.Text      = ctTable.Tables[0].Rows[0][36].ToString();
                textBoxCreatureTemplateVID.Text             = ctTable.Tables[0].Rows[0][37].ToString();

                textBoxCreatureTemplateTType.Text           = ctTable.Tables[0].Rows[0][38].ToString();
                textBoxCreatureTemplateTSpell.Text          = ctTable.Tables[0].Rows[0][39].ToString();
                textBoxCreatureTemplateTRace.Text           = ctTable.Tables[0].Rows[0][40].ToString();
                textBoxCreatureTemplateTClass.Text          = ctTable.Tables[0].Rows[0][41].ToString();

                textBoxCreatureTemplateLootID.Text          = ctTable.Tables[0].Rows[0][42].ToString();
                textBoxCreatureTemplatePickID.Text          = ctTable.Tables[0].Rows[0][43].ToString();
                textBoxCreatureTemplateSkinID.Text          = ctTable.Tables[0].Rows[0][44].ToString();

                textBoxCreatureTemplateResis1.Text          = ctTable.Tables[0].Rows[0][45].ToString();
                textBoxCreatureTemplateResis2.Text          = ctTable.Tables[0].Rows[0][46].ToString();
                textBoxCreatureTemplateResis3.Text          = ctTable.Tables[0].Rows[0][47].ToString();
                textBoxCreatureTemplateResis4.Text          = ctTable.Tables[0].Rows[0][48].ToString();
                textBoxCreatureTemplateResis5.Text          = ctTable.Tables[0].Rows[0][49].ToString();
                textBoxCreatureTemplateResis6.Text          = ctTable.Tables[0].Rows[0][50].ToString();

                checkBoxCreatureTemplateHR.Checked          = Convert.ToBoolean(ctTable.Tables[0].Rows[0][51]);
                textBoxCreatureTemplateMechanic.Text        = ctTable.Tables[0].Rows[0][52].ToString();
                textBoxCreatureTemplateFamily.Text          = ctTable.Tables[0].Rows[0][53].ToString();
                textBoxCreatureTemplateType.Text            = ctTable.Tables[0].Rows[0][54].ToString();
                textBoxCreatureTemplateTypeFlags.Text       = ctTable.Tables[0].Rows[0][55].ToString();
                textBoxCreatureTemplateFlagsExtra.Text      = ctTable.Tables[0].Rows[0][56].ToString();
                textBoxCreatureTemplateUnitClass.Text       = ctTable.Tables[0].Rows[0][57].ToString();
                textBoxCreatureTemplateUnitflags.Text       = ctTable.Tables[0].Rows[0][58].ToString();
                textBoxCreatureTemplateUnitflags2.Text      = ctTable.Tables[0].Rows[0][59].ToString();
                textBoxCreatureTemplateDynamic.Text         = ctTable.Tables[0].Rows[0][60].ToString();

                textBoxCreatureTemplateSpeedWalk.Text       = ctTable.Tables[0].Rows[0][61].ToString();
                textBoxCreatureTemplateSpeedRun.Text        = ctTable.Tables[0].Rows[0][62].ToString();

                textBoxCreatureTemplateSpell1.Text          = ctTable.Tables[0].Rows[0][63].ToString();
                textBoxCreatureTemplateSpell2.Text          = ctTable.Tables[0].Rows[0][64].ToString();
                textBoxCreatureTemplateSpell3.Text          = ctTable.Tables[0].Rows[0][65].ToString();
                textBoxCreatureTemplateSpell4.Text          = ctTable.Tables[0].Rows[0][66].ToString();
                textBoxCreatureTemplateSpell5.Text          = ctTable.Tables[0].Rows[0][67].ToString();
                textBoxCreatureTemplateSpell6.Text          = ctTable.Tables[0].Rows[0][68].ToString();
                textBoxCreatureTemplateSpell7.Text          = ctTable.Tables[0].Rows[0][69].ToString();
                textBoxCreatureTemplateSpell8.Text          = ctTable.Tables[0].Rows[0][70].ToString();

                tabControlCategoryCreature.SelectedTab = tabPageCreatureTemplate;
                ConnectionClose(connect);
            }


        }
            // Searches the database for the creature's spawnlocations
        private void DatabaseCreatureLocation(string creatureEntryID)
        {
            MySqlConnection connect = new MySqlConnection(DatabaseString(MySQLWindow.DatabaseWorld));

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
            MySqlConnection connect = new MySqlConnection(DatabaseString(MySQLWindow.DatabaseWorld));

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
        #region GlobalFunctions

        // Gets the Item Entry with global item identifier.
        private uint DatabaseItemGetEntry(uint itemIdentifier)
        {
            MySqlConnection connect = new MySqlConnection(DatabaseString(MySQLWindow.DatabaseCharacters));

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

            MySqlConnection connect = new MySqlConnection(DatabaseString(MySQLWindow.DatabaseWorld));

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
                if (!value.Trim().StartsWith("%") && !value.Trim().EndsWith("%"))
                {
                    value = " AND " + query + " = '" + value + "'";
                } else
                {
                    value = " AND " + query + " LIKE '%" + value + "%'";
                }
            }

            return value;
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

        #endregion

        #endregion
    
        #region AccountSection
        /*
            ---------------------------------------------------------------
                                ACCOUNT SECTION
            ---------------------------------------------------------------
        */

        private void buttonAccountSearchButton_Click(object sender, EventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(DatabaseString(MySQLWindow.DatabaseAuth));

            if (ConnectionOpen(connect))
            {
                string searchQuery = "SELECT id, username, email, expansion " +
                    "FROM account WHERE id = '" + textBoxAccountSearchID.Text.Trim() + "' OR username = '" + textBoxAccountSearchUsername.Text.Trim() + "'";

                DataSet AccountSearch = DatabaseSearch(connect, searchQuery.Trim());
                dataGridViewAccountSearch.DataSource = AccountSearch.Tables[0];

                toolStripStatusLabelAccountSearchRow.Text = "Account(s) found: " + AccountSearch.Tables[0].Rows.Count.ToString();

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
            MySqlConnection connect = new MySqlConnection(DatabaseString(MySQLWindow.DatabaseAuth));
            
            if (ConnectionOpen(connect))
            {
                int rows = DatabaseUpdate(connect, textBoxAccountScript.Text.Trim());
                toolStripStatusLabelAccountScriptResult.Text = "Row(s) affected: " + rows.ToString();
                ConnectionClose(connect);
            } 
        }

        #endregion AccountSection
        #region CharacterSection

        /*
            ---------------------------------------------------------------
                               CHARACTER SECTION
            ---------------------------------------------------------------
        */

        // Search button on Character -> Search tab.
        private void buttonCharacterSearchSearch_Click(object sender, EventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(DatabaseString(MySQLWindow.DatabaseCharacters));

            if (ConnectionOpen(connect))
            {
                string searchQuery = "SELECT guid, account, name, race, class, level " +
                    "FROM characters WHERE guid = '" + textBoxCharacterSearchID.Text.Trim() +
                    "' OR account = '" + textBoxCharacterSearchAccount.Text.Trim() +
                    "' OR name = '" + textBoxCharacterSearchUsername.Text.Trim() + "';";

                DataSet CharacterSearch = DatabaseSearch(connect, searchQuery.Trim());
                dataGridViewCharacterSearch.DataSource = CharacterSearch.Tables[0];

                toolStripStatusLabelCharacterSearchRow.Text = "Character(s) found: " + CharacterSearch.Tables[0].Rows.Count.ToString();

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
        #region CreatureSection

        /*
            ---------------------------------------------------------------
                               CREATURE SECTION
            ---------------------------------------------------------------
        */

        // Search button
        private void buttonCreatureSearchSearch_Click(object sender, EventArgs e)
        {
            bool totalSearch = true; DialogResult dr; string finalquery;

            string query = "SELECT entry, NAME, subname, minlevel, maxlevel, rank, lootid FROM creature_template WHERE '1' = '1'";
            string wq = "";

            foreach (Control ct in tabPageCreatureSearch.Controls)
            {  
                if (ct is TextBox)
                {
                    if (ct.Text != "") {
                        totalSearch = false;
                    }
                }
            }

            if (totalSearch)
            {
                dr = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                finalquery = query;
            } else
            {
                wq += DatabaseQueryFilter(textBoxCreatureSearchEntry.Text, "entry");
                wq += DatabaseQueryFilter(textBoxCreatureSearchName.Text, "name");
                wq += DatabaseQueryFilter(textBoxCreatureSearchSubname.Text, "subname");
                wq += DatabaseQueryFilter(textBoxCreatureSearchLevelMin.Text, "minlevel");
                wq += DatabaseQueryFilter(textBoxCreatureSearchLevelMax.Text, "maxlevel");
                wq += DatabaseQueryFilter(textBoxCreatureSearchRank.Text, "rank");

                finalquery = query + wq;

                dr = DialogResult.OK;
            }

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            MySqlConnection connect = new MySqlConnection(DatabaseString(MySQLWindow.DatabaseWorld));
            
            if (ConnectionOpen(connect))
            {
                // Creature Template
                DataSet ctTable = DatabaseSearch(connect, finalquery);

                dataGridViewCreatureSearch.DataSource = ctTable.Tables[0];
                toolStripStatusLabelCreatureSearchRow.Text = ctTable.Tables[0].Rows.Count.ToString();

                ConnectionClose(connect);
            }
        }

        private void dataGridViewCreatureSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCreatureSearch.Rows.Count != 0)
            {
                DatabaseCreatureSearch( dataGridViewCreatureSearch.SelectedCells[0].Value.ToString() );
                DatabaseCreatureLocation( dataGridViewCreatureSearch.SelectedCells[0].Value.ToString() );
                DatabaseCreatureLoot(dataGridViewCreatureSearch.SelectedCells[6].Value.ToString());
            }
        }

        #endregion

    }
}

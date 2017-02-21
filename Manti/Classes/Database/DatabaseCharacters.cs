using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

using Manti.Classes.CharacterTab;

namespace Manti.Classes.Database {
	public class DatabaseCharacters : MySqlDatabase {
		public DatabaseCharacters(string address, string username, string password, uint port, string dbName)
			: base(address, username, password, port, dbName) {
		}

		public int uploadSql(string sql) {
			return executeNonQuery(sql);
		}

		public DataTable searchForCharacters(string name, bool isExact) {
			var query = "SELECT guid, account, name, race, class, level FROM characters WHERE LOWER(name) ";
			query += (isExact ? "= '?value'" : "LIKE '%?value%'") + " ORDER BY guid;";

			var dt = executeQuery(query, new MySqlParameter("?value", name.ToLower()));

			return dt;
		}
		public DataTable searchForCharacterByID(uint id) {
			DataTable dt = executeQuery("SELECT guid, account, name, race, class, level FROM characters WHERE guid = '?value' ORDER BY guid;", new MySqlParameter("?value", id));

			return dt;
		}
		public DataTable searchForCharacterByAccount(uint id) {
			DataTable dt = executeQuery("SELECT guid, account, name, race, class, level FROM characters WHERE account = '?value' ORDER BY guid;", new MySqlParameter("?value", id));

			return dt;
		}
		public DataTable searchForAllCharacters() {
			DataTable dt = executeQuery("SELECT guid, account, name, race, class, level FROM characters ORDER BY guid;");

			return dt;
		}
		public Character getCharacter(uint id) {
			var query = "SELECT guid, account, name, race, class, gender, level, money, xp, chosentitle, online, cinematic, is_logout_resting, map, instance_id, zone, orientation, position_x, position_y, position_z, totalHonorPoints, arenaPoints, totalKills, health, power1, power2, power3, power4, power5, power6, power7 " +
				"FROM characters WHERE guid = '?value' ORDER BY guid;";

			DataTable dt = executeQuery(query, new MySqlParameter("?value", id));

			return buildCharacter(dt);
		}
		public CharacterInventory[] getCharacterInventory(uint id) {
			DataTable _charInv = executeQuery("SELECT * FROM character_inventory WHERE guid = '?value' ORDER BY guid;", new MySqlParameter("?value", id));
			DataTable _charNames = getCharacterInventoryName(id); 

			return buildCharacterInventory(_charInv, _charNames);
		}
		public DataTable getCharacterInventoryName(uint id) {
			string charInventoryQuery = "SELECT character_inventory.item FROM character_inventory WHERE guid = '?value' ORDER BY guid";
			string itemEntryQuery     = "SELECT itemEntry FROM item_instance WHERE guid IN ("+charInventoryQuery+")";
			string itemNameQuery      = "SELECT world.item_template.name FROM world.item_template WHERE world.item_template.entry IN ("+itemEntryQuery+");";

			DataTable dt = executeQuery(itemNameQuery, new MySqlParameter("?value", id));

			return dt;
		}
		public Character buildCharacter(DataTable dt) {
			var _char = new Character();
			// General
			_char.guid         = Convert.ToUInt32(dt.Rows[0]["guid"]);
			_char.accountID    = Convert.ToUInt32(dt.Rows[0]["account"]);
			_char.name         = dt.Rows[0]["name"].ToString();
			_char.charRace     = Convert.ToUInt32(dt.Rows[0]["race"]);
			_char.charClass    = Convert.ToUInt32(dt.Rows[0]["class"]);
			_char.sex          = Convert.ToUInt32(dt.Rows[0]["gender"]);
			_char.level        = Convert.ToUInt32(dt.Rows[0]["level"]);
			_char.money        = Convert.ToUInt32(dt.Rows[0]["money"]);
			_char.xp           = Convert.ToUInt32(dt.Rows[0]["xp"]);
			_char.chosenTitle  = Convert.ToUInt32(dt.Rows[0]["chosentitle"]);
			_char.isOnline     = Convert.ToBoolean(dt.Rows[0]["online"]);
			_char.isCinematic  = Convert.ToBoolean(dt.Rows[0]["cinematic"]);
			_char.isResting    = Convert.ToBoolean(dt.Rows[0]["is_logout_resting"]);
			// Location
			_char.mapID       = Convert.ToInt32(dt.Rows[0]["map"]);
			_char.instanceID  = Convert.ToInt32(dt.Rows[0]["instance_id"]);
			_char.zoneID      = Convert.ToInt32(dt.Rows[0]["zone"]);
			_char.orientation = Convert.ToDouble(dt.Rows[0]["orientation"]);
			_char.xPosition   = Convert.ToDouble(dt.Rows[0]["position_x"]);
			_char.yPosition   = Convert.ToDouble(dt.Rows[0]["position_y"]);
			_char.zPosition   = Convert.ToDouble(dt.Rows[0]["position_z"]);
			// Player vs Player
			_char.honorPoints = Convert.ToInt32(dt.Rows[0]["totalHonorPoints"]);
			_char.arenaPoints = Convert.ToInt32(dt.Rows[0]["arenaPoints"]);
			_char.totalKills  = Convert.ToInt32(dt.Rows[0]["totalKills"]);
			// Stats
			_char.health = Convert.ToUInt64(dt.Rows[0]["health"]);
			_char.power1 = Convert.ToUInt64(dt.Rows[0]["power1"]);
			_char.power2 = Convert.ToUInt64(dt.Rows[0]["power2"]);
			_char.power3 = Convert.ToUInt64(dt.Rows[0]["power3"]);
			_char.power4 = Convert.ToUInt64(dt.Rows[0]["power4"]);
			_char.power5 = Convert.ToUInt64(dt.Rows[0]["power5"]);
			_char.power6 = Convert.ToUInt64(dt.Rows[0]["power6"]);
			_char.power7 = Convert.ToUInt64(dt.Rows[0]["power7"]);

			return _char;
		}
		public CharacterInventory[] buildCharacterInventory(DataTable dtInventory, DataTable dtNames) {
			var ci = new CharacterInventory[dtInventory.Rows.Count];

			for(int i = 0; i < dtInventory.Rows.Count; i++) {
				ci[i] = new CharacterInventory();

				ci[i].guid = Convert.ToUInt32(dtInventory.Rows[i]["guid"].ToString());
				ci[i].bag  = Convert.ToUInt32(dtInventory.Rows[i]["bag"].ToString());
				ci[i].slot = Convert.ToUInt32(dtInventory.Rows[i]["slot"].ToString());
				ci[i].item = Convert.ToUInt32(dtInventory.Rows[i]["item"].ToString());
				ci[i].name = dtNames.Rows[i]["name"].ToString();
			}

			return ci;
		}

	}
}

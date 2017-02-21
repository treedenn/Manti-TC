using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manti.Classes.Generate {
	public class GenerateCharacters : SqlGenerate {
		public string CharacterToSql(CharacterTab.Character c) {
			if(c != null) {
				string[] columns = {"guid", "account", "name", "race", "class", "gender", "level", "money", "xp", "chosentitle", "online", "cinematic",
					"is_logout_resting", "map", "instance_id", "zone", "orientation", "position_x", "position_y", "position_z", "totalHonorPoints", "arenaPoints",
					"totalKills", "health", "power1", "power2", "power3", "power4", "power5", "power6", "power7"};

				object[] values = {c.guid, c.accountID, c.name, c.charRace, c.charClass, c.sex, c.level, c.money, c.xp, c.chosenTitle, c.isOnline, c.isCinematic,
					c.isResting, c.mapID, c.instanceID, c.zoneID, c.orientation, c.xPosition, c.yPosition, c.zPosition, c.honorPoints, c.arenaPoints,
					c.totalKills, c.health, c.power1, c.power2, c.power3, c.power4, c.power5, c.power6, c.power7};

				return updateToDatabase("characters", columns, values, "guid", c.guid.ToString());
			}

			return null;
		}

		public string CharacterInventoryToSql(uint id, CharacterTab.CharacterInventory[] inv) {
			if(inv != null) {
				string[] columns = {"guid", "bag", "slot", "item"};
				object[] values;

				string query = "";

				foreach(var i in inv) {
					values = new object[] { i.guid, i.bag, i.slot, i.item};
					query += insertToDatabase("character_inventory", columns, values);
				}

				return deleteFromDatabase("character_inventory", "guid", id) + query;
			}

			return null;
		}
	}
}

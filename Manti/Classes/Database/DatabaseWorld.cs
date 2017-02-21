﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using Manti.Classes.CreatureTab;
using Manti.Classes.QuestTab;
using Manti.Classes.GameObjectTab;
using Manti.Classes.ItemTab;

namespace Manti.Classes.Database {
	public class DatabaseWorld : MySqlDatabase {
		public DatabaseWorld(string address, string username, string password, uint port, string dbName)
			: base(address, username, password, port, dbName) { }

		#region Creature

		public DataTable searchForAllCreatures() {
			DataTable dt = executeQuery("SELECT entry, name, subname, minlevel, maxlevel, rank, lootid FROM creature_template ORDER BY entry;");

			return dt;
		}

		public DataTable searchForCreatures(string name, string subname, string minlevel, string maxlevel, string rank) {
			string query = "SELECT entry, name, subname, minlevel, maxlevel, rank, lootid FROM creature_template WHERE ";

			string[] values = {name, subname, minlevel, maxlevel, rank};

			byte notEmpty = 0;

			foreach(string s in values) {
				if(!string.IsNullOrEmpty(s)) { notEmpty += 1; }
			}

			if(!string.IsNullOrEmpty(name)) {
				if(name.StartsWith("#")) {
					query += String.Format("name = '{0}'", Classes.UtilityHelper.removeExactSign(name));
				} else {
					query += String.Format("name LIKE '%{0}%'", name);
				}

				if(notEmpty > 1) { query += " AND "; notEmpty -= 1; }
			}

			if(!string.IsNullOrEmpty(subname)) {
				if(subname.StartsWith("#")) {
					query += String.Format("subname = '{0}'", Classes.UtilityHelper.removeExactSign(subname));
				} else {
					query += String.Format("subname LIKE '%{0}%'", subname);
				}

				if(notEmpty > 1) { query += " AND "; notEmpty -= 1; }
			}

			if(!string.IsNullOrEmpty(minlevel)) {
				query += String.Format("minlevel >= '{0}'", minlevel);

				if(notEmpty > 1) { query += " AND "; notEmpty -= 1; }
			}

			if(!string.IsNullOrEmpty(maxlevel)) {
				query += String.Format("maxlevel <= '{0}'", maxlevel);

				if(notEmpty > 1) { query += " AND "; notEmpty -= 1; }
			}

			if(!string.IsNullOrEmpty(rank)) {
				query += String.Format("rank = '{0}'", rank);

				if(notEmpty > 1) { query += " AND "; notEmpty -= 1; }
			}

			query += " ORDER BY entry;";

			var dt = executeQuery(query);

			return dt;
		}

		public DataTable searchForCreatureByID(uint id) {
			DataTable dt = executeQuery("SELECT entry, name, subname, minlevel, maxlevel, rank, lootid FROM creature_template WHERE entry = '?value' ORDER BY entry;", new MySqlParameter("?value", id));

			return dt;
		}

		public Creature getCreature(uint entry) {
			string query = "SELECT entry, difficulty_entry_1, difficulty_entry_2, difficulty_entry_3, name, subname, modelid1, modelid2, modelid3, modelid4, minlevel, maxlevel, mingold, maxgold,";
			query += "KillCredit1, KillCredit2, rank, scale, faction, npcflag, HealthModifier, ManaModifier, ArmorModifier, DamageModifier, ExperienceModifier, BaseAttackTime, ";
			query += "RangeAttackTime, BaseVariance, RangeVariance, dmgschool, AIName, MovementType, InhabitType, HoverHeight, gossip_menu_id, movementId, ScriptName, VehicleId, ";
			query += "trainer_type, trainer_spell, trainer_class, trainer_race, lootid, pickpocketloot, skinloot, resistance1, resistance2, resistance3, resistance4, resistance5, resistance6, ";
			query += "RegenHealth, mechanic_immune_mask, family, type, type_flags, flags_extra, unit_class, unit_flags, unit_flags2, dynamicflags, speed_walk, speed_run, ";
			query += "spell1, spell2, spell3, spell4, spell5, spell6, spell7, spell8 ";
			query += "FROM creature_template WHERE entry = '?entry' ORDER BY entry;";

			DataTable dt = executeQuery(query, new MySqlParameter("?entry", entry));

			return buildCreature(dt);
		}

		public CreatureLocation[] getCreatureLocation(uint entry) {
			DataTable dt = executeQuery("SELECT id, guid, map, zoneId, areaId, position_x, position_y, position_z, orientation, spawntimesecs, spawndist FROM creature WHERE id = '?entry';", new MySqlParameter("?entry", entry));

			return buildCreatureLocation(dt);
		}

		public CreatureVendor[] getCreatureVendor(uint entry) {
			DataTable dt = executeQuery("SELECT entry, slot, item, maxcount, incrtime, extendedcost FROM npc_vendor WHERE entry = '?entry' ORDER BY entry", new MySqlParameter("?entry", entry));

			return buildCreatureVendor(dt, getItemNames("npc_vendor", entry));
		}

		public CreatureLPS[] getCreatureLoot(uint entry) {
			DataTable dt = executeQuery("SELECT entry, item, reference, chance, questrequired, lootmode, groupid, mincount, maxcount FROM creature_loot_template WHERE entry = '?entry'", 
				new MySqlParameter("?entry", entry));

			return buildCreatureLPS(dt, getItemNames("creature_loot_template", entry));
		}

		public CreatureLPS[] getCreaturePickpocket(uint entry) {
			DataTable dt = executeQuery("SELECT entry, item, reference, chance, questrequired, lootmode, groupid, mincount, maxcount FROM pickpocketing_loot_template WHERE entry = '?entry'", new MySqlParameter("?entry", entry));

			return buildCreatureLPS(dt, getItemNames("pickpocketing_loot_template", entry));
		}

		public CreatureLPS[] getCreatureSkin(uint entry) {
			DataTable dt = executeQuery("SELECT entry, item, reference, chance, questrequired, lootmode, groupid, mincount, maxcount FROM skinning_loot_template WHERE entry = '?entry'", new MySqlParameter("?entry", entry));

			return buildCreatureLPS(dt, getItemNames("skinning_loot_template", entry));
		}

		public DataTable getItemNames(string table, uint entry) {
			return executeQuery("SELECT name FROM item_template WHERE entry IN (SELECT item FROM ?table WHERE entry = '?entry' ORDER BY entry);", 
				new MySqlParameter("?entry", entry), new MySqlParameter("?table", table));
		}

		public Creature buildCreature(DataTable dt) {
			Creature c = new Creature();

			c.entry              = Convert.ToUInt32(dt.Rows[0]["entry"]);
			c.diffEntry1         = Convert.ToUInt32(dt.Rows[0]["difficulty_entry_1"]);
			c.diffEntry2         = Convert.ToUInt32(dt.Rows[0]["difficulty_entry_2"]);
			c.diffEntry3         = Convert.ToUInt32(dt.Rows[0]["difficulty_entry_3"]);
			c.name               = dt.Rows[0]["name"].ToString();
			c.subname            = dt.Rows[0]["subname"].ToString();
			c.modelId1           = Convert.ToUInt32(dt.Rows[0]["modelid1"]);
			c.modelId2           = Convert.ToUInt32(dt.Rows[0]["modelid2"]);
			c.modelId3           = Convert.ToUInt32(dt.Rows[0]["modelid3"]);
			c.modelId4           = Convert.ToUInt32(dt.Rows[0]["modelid4"]);
			c.minlevel           = Convert.ToUInt32(dt.Rows[0]["minlevel"]);
			c.maxlevel           = Convert.ToUInt32(dt.Rows[0]["maxlevel"]);
			c.mingold            = Convert.ToUInt32(dt.Rows[0]["mingold"]);
			c.maxgold            = Convert.ToUInt32(dt.Rows[0]["maxgold"]);
			c.killCredit1        = Convert.ToInt32(dt.Rows[0]["KillCredit1"]);
			c.killCredit2        = Convert.ToInt32(dt.Rows[0]["KillCredit2"]);
			c.rank               = Convert.ToUInt32(dt.Rows[0]["rank"]);
			c.scale              = Convert.ToSingle(dt.Rows[0]["scale"]);
			c.faction            = Convert.ToInt32(dt.Rows[0]["faction"]);
			c.npcFlags           = Convert.ToInt32(dt.Rows[0]["npcflag"]);
			c.spell1             = Convert.ToUInt32(dt.Rows[0]["spell1"]);
			c.spell2             = Convert.ToUInt32(dt.Rows[0]["spell2"]);
			c.spell3             = Convert.ToUInt32(dt.Rows[0]["spell3"]);
			c.spell4             = Convert.ToUInt32(dt.Rows[0]["spell4"]);
			c.spell5             = Convert.ToUInt32(dt.Rows[0]["spell5"]);
			c.spell6             = Convert.ToUInt32(dt.Rows[0]["spell6"]);
			c.spell7             = Convert.ToUInt32(dt.Rows[0]["spell7"]);
			c.spell8             = Convert.ToUInt32(dt.Rows[0]["spell8"]);
			c.modHealth          = Convert.ToSingle(dt.Rows[0]["HealthModifier"]);
			c.modMana            = Convert.ToSingle(dt.Rows[0]["ManaModifier"]);
			c.modArmor           = Convert.ToSingle(dt.Rows[0]["ArmorModifier"]);
			c.modDamage          = Convert.ToSingle(dt.Rows[0]["DamageModifier"]);
			c.modExp             = Convert.ToSingle(dt.Rows[0]["ExperienceModifier"]);
			c.speedWalk          = Convert.ToSingle(dt.Rows[0]["speed_walk"]);
			c.speedRun           = Convert.ToSingle(dt.Rows[0]["speed_run"]);
			c.baseAttackTime     = Convert.ToSingle(dt.Rows[0]["BaseAttackTime"]);
			c.rangedAttackTime   = Convert.ToSingle(dt.Rows[0]["RangeAttackTime"]);
			c.bVariance          = Convert.ToSingle(dt.Rows[0]["BaseVariance"]);
			c.rVariance          = Convert.ToSingle(dt.Rows[0]["RangeVariance"]);
			c.dSchool            = Convert.ToByte(dt.Rows[0]["dmgschool"]);
			c.aiName             = dt.Rows[0]["AIName"].ToString();
			c.movementType       = Convert.ToInt32(dt.Rows[0]["MovementType"]);
			c.inhabitType        = Convert.ToInt32(dt.Rows[0]["InhabitType"]);
			c.hoverHeight        = Convert.ToUInt32(dt.Rows[0]["HoverHeight"]);
			c.gossipMenuId       = Convert.ToUInt32(dt.Rows[0]["gossip_menu_id"]);
			c.movementId         = Convert.ToUInt32(dt.Rows[0]["movementId"]);
			c.scriptName         = dt.Rows[0]["ScriptName"].ToString();
			c.vehicleId          = Convert.ToUInt32(dt.Rows[0]["VehicleId"]);
			c.mechanicImmuneMask = Convert.ToInt32(dt.Rows[0]["mechanic_immune_mask"]);
			c.family             = Convert.ToUInt32(dt.Rows[0]["family"]);
			c.familyType         = Convert.ToUInt32(dt.Rows[0]["type"]);
			c.typeFlags          = Convert.ToInt32(dt.Rows[0]["type_flags"]);
			c.extraFlags         = Convert.ToInt32(dt.Rows[0]["flags_extra"]);
			c.unitClass          = Convert.ToUInt32(dt.Rows[0]["unit_class"]);
			c.unitFlags1         = Convert.ToInt32(dt.Rows[0]["unit_flags"]);
			c.unitFlags2         = Convert.ToInt32(dt.Rows[0]["unit_flags2"]);
			c.dynamicFlags       = Convert.ToUInt32(dt.Rows[0]["dynamicflags"]);
			c.isRegenHealth      = Convert.ToBoolean(dt.Rows[0]["RegenHealth"]);
			c.resistHoly         = Convert.ToInt32(dt.Rows[0]["resistance1"]);
			c.resistFire         = Convert.ToInt32(dt.Rows[0]["resistance2"]);
			c.resistNature       = Convert.ToInt32(dt.Rows[0]["resistance3"]);
			c.resistFrost        = Convert.ToInt32(dt.Rows[0]["resistance4"]);
			c.resistShadow       = Convert.ToInt32(dt.Rows[0]["resistance5"]);
			c.resistArcane       = Convert.ToInt32(dt.Rows[0]["resistance6"]);
			c.trainerType        = Convert.ToInt32(dt.Rows[0]["trainer_type"]);
			c.trainerSpell       = Convert.ToInt32(dt.Rows[0]["trainer_spell"]);
			c.trainerClass       = Convert.ToInt32(dt.Rows[0]["trainer_class"]);
			c.trainerRace        = Convert.ToInt32(dt.Rows[0]["trainer_race"]);
			c.lootId             = Convert.ToUInt32(dt.Rows[0]["lootid"]);
			c.pickpocketId       = Convert.ToUInt32(dt.Rows[0]["pickpocketloot"]);
			c.skinId             = Convert.ToUInt32(dt.Rows[0]["skinloot"]);

			return c;
		}

		public CreatureLocation[] buildCreatureLocation(DataTable dt) {
			if(dt != null) {
				CreatureLocation[] cl = new CreatureLocation[dt.Rows.Count];

				for(var i = 0; i < dt.Rows.Count; i++) {
					cl[i] = new CreatureLocation();

					cl[i].id            = Convert.ToUInt32(dt.Rows[i]["id"]);
					cl[i].guid          = Convert.ToUInt32(dt.Rows[i]["guid"]);
					cl[i].map           = Convert.ToUInt32(dt.Rows[i]["map"]);
					cl[i].zoneId        = Convert.ToUInt32(dt.Rows[i]["zoneId"]);
					cl[i].areaId        = Convert.ToUInt32(dt.Rows[i]["areaId"]);
					cl[i].xPosition     = Convert.ToSingle(dt.Rows[i]["position_x"]);
					cl[i].yPosition     = Convert.ToSingle(dt.Rows[i]["position_y"]);
					cl[i].zPosition     = Convert.ToSingle(dt.Rows[i]["position_z"]);
					cl[i].orientation   = Convert.ToSingle(dt.Rows[i]["orientation"]);
					cl[i].spawntimeSecs = Convert.ToUInt32(dt.Rows[i]["spawntimesecs"]);
					cl[i].spawnDist     = Convert.ToSingle(dt.Rows[i]["spawndist"]);
				}

				return cl;
			}

			return null;
		}

		public CreatureVendor[] buildCreatureVendor(DataTable dt, DataTable dtNames) {
			if(dt != null) {
				CreatureVendor[] cv = new CreatureVendor[dt.Rows.Count];

				for(var i = 0; i < dt.Rows.Count; i++) {
					cv[i] = new CreatureVendor();

					cv[i].entry        = Convert.ToUInt32(dt.Rows[i]["entry"]);
					cv[i].slot         = Convert.ToInt32(dt.Rows[i]["slot"]);
					cv[i].item         = Convert.ToUInt32(dt.Rows[i]["item"]);
					cv[i].maxCount     = Convert.ToUInt32(dt.Rows[i]["maxcount"]);
					cv[i].increaseTime = Convert.ToUInt32(dt.Rows[i]["incrtime"]);
					cv[i].extendedCost = Convert.ToUInt32(dt.Rows[i]["extendedcost"]);
					cv[i].name         = dtNames.Rows[i]["name"].ToString();
				}

				return cv;
			}

			return null;
		}

		public CreatureLPS[] buildCreatureLPS(DataTable dt, DataTable dtNames) {
			if(dt != null) {
				CreatureLPS[] clps = new CreatureLPS[dt.Rows.Count];

				for(var i = 0; i < dt.Rows.Count; i++) {
					clps[i] = new CreatureLPS();

					clps[i].entry         = Convert.ToUInt32(dt.Rows[i]["entry"]);
					clps[i].item          = Convert.ToUInt32(dt.Rows[i]["item"]);
					clps[i].reference     = Convert.ToInt32(dt.Rows[i]["reference"]);
					clps[i].chance        = Convert.ToSingle(dt.Rows[i]["chance"]);
					clps[i].questRequired = Convert.ToByte(dt.Rows[i]["questrequired"]);
					clps[i].lootMode      = Convert.ToUInt16(dt.Rows[i]["lootmode"]);
					clps[i].groupId       = Convert.ToUInt16(dt.Rows[i]["groupid"]);
					clps[i].minCount      = Convert.ToUInt16(dt.Rows[i]["mincount"]);
					clps[i].maxCount      = Convert.ToUInt16(dt.Rows[i]["maxcount"]);
					if(i < dtNames.Rows.Count) { clps[i].name = dtNames.Rows[i]["name"].ToString(); }
				}

				return clps;
			}

			return null;
		}

		#endregion

		#region Quest
		
		public DataTable searchForAllQuests() {
			return executeQuery("SELECT id, logtitle, logdescription FROM quest_template ORDER BY id;");
		}

		public DataTable searchForQuestsById(uint id) {
			return executeQuery("SELECT id, logtitle, logdescription FROM quest_template WHERE id = '?value' ORDER BY id;", new MySqlParameter("?value", id));
		}

		public DataTable searchForQuestsByTitle(string title, bool isExact) {
			return executeQuery("SELECT id, logtitle, logdescription FROM quest_template WHERE lower(logtitle) " + 
				(isExact ? "= '?value'" : "LIKE '%?value%'") + " ORDER BY id;", new MySqlParameter("?value", title));
		}

		public DataTable searchForQuestsByGiver(uint id) {
			return executeQuery("SELECT id, logtitle, logdescription FROM quest_template WHERE id IN (SELECT quest FROM creature_queststarter WHERE id = '?value') ORDER BY id;", new MySqlParameter("?value", id));
		}

		public DataTable searchForQuestsByTaker(uint id) {
			return executeQuery("SELECT id, logtitle, logdescription FROM quest_template WHERE id IN (SELECT quest FROM creature_questender WHERE id = '?value') ORDER BY id;", new MySqlParameter("?value", id));
		}

		public DataTable searchForQuestsByPrevId(uint id) {
			return executeQuery("SELECT id, logtitle, logdescription FROM quest_template WHERE id IN (SELECT ID FROM quest_template_addon WHERE PrevQuestID = '?value') ORDER BY id;", new MySqlParameter("?value", id));
		}

		public DataTable searchForQuestsByNextId(uint id) {
			return executeQuery("SELECT id, logtitle, logdescription FROM quest_template WHERE id IN (SELECT ID FROM quest_template_addon WHERE NextQuestID = '?value') ORDER BY id;", new MySqlParameter("?value", id));
		}

		public DataTable searchForQuestsByInfoId(uint id) {
			return executeQuery("SELECT id, logtitle, logdescription FROM quest_template WHERE questinfoid = '?value' ORDER BY id;", new MySqlParameter("?value", id));
		}

		public Quest getQuest(uint id) {
			Quest quest = new Quest();

			DataTable dtQuest = executeQuery("SELECT * FROM quest_template WHERE id = '?value' ORDER BY id;", new MySqlParameter("?value", id));
			DataTable dtQuestAddon = executeQuery("SELECT exclusivegroup, rewardmailtemplateid, rewardmaildelay, prevquestid, nextquestid, allowableclasses, maxlevel, requiredminrepfaction, requiredmaxrepfaction, requiredminrepvalue, requiredmaxrepvalue, " +
				"requiredskillid, requiredskillpoints, specialflags, provideditemcount, sourcespellid FROM quest_template_addon WHERE id = '?value' ORDER BY id;", new MySqlParameter("?value", id));

			return buildQuest(dtQuest, dtQuestAddon);
		}

		public QuestGT[] getQuestGT(uint id, bool isGiver) {
			DataTable dt;

			if(isGiver) {
				dt = executeQuery("SELECT entry, name, subname FROM creature_template WHERE entry IN (SELECT id FROM creature_queststarter WHERE quest = '?value');", new MySqlParameter("?value", id));
			} else {
				dt = executeQuery("SELECT entry, name, subname FROM creature_template WHERE entry IN (SELECT id FROM creature_questender WHERE quest = '?value');", new MySqlParameter("?value", id));
			}

			return buildQuestGT(dt);
		}

		public Quest buildQuest(DataTable dtQuest, DataTable dtQuestAddon) {
			Quest q = new Quest();

			q.id                              = Convert.ToUInt32(dtQuest.Rows[0]["id"]);
			q.title                           = dtQuest.Rows[0]["logtitle"].ToString();
			q.logDescription                  = dtQuest.Rows[0]["logdescription"].ToString();
			q.questDescription                = dtQuest.Rows[0]["questdescription"].ToString();
			q.area                            = dtQuest.Rows[0]["areadescription"].ToString();
			q.completed                       = dtQuest.Rows[0]["questcompletionlog"].ToString();
			q.objective1                      = dtQuest.Rows[0]["objectivetext1"].ToString();
			q.objective2                      = dtQuest.Rows[0]["objectivetext2"].ToString();
			q.objective3                      = dtQuest.Rows[0]["objectivetext3"].ToString();
			q.objective4                      = dtQuest.Rows[0]["objectivetext4"].ToString();
			q.requirePlayerKills              = Convert.ToInt32(dtQuest.Rows[0]["requiredplayerkills"]);
			q.timeAllowed                     = Convert.ToInt32(dtQuest.Rows[0]["timeallowed"]);
			q.questInfo                       = Convert.ToInt32(dtQuest.Rows[0]["questinfoid"]);
			q.questLevel                      = Convert.ToInt32(dtQuest.Rows[0]["questlevel"]);
			q.suggestedPlayers                = Convert.ToInt32(dtQuest.Rows[0]["suggestedgroupnum"]);
			q.exclusiveGroup                  = Convert.ToInt32(dtQuestAddon.Rows[0]["exclusivegroup"]);
			q.prevQuest                       = Convert.ToInt32(dtQuestAddon.Rows[0]["prevquestid"]);
			q.nextQuest                       = Convert.ToInt32(dtQuestAddon.Rows[0]["nextquestid"]);
			q.races                           = Convert.ToInt32(dtQuest.Rows[0]["allowableraces"]);
			q.classes                         = Convert.ToInt32(dtQuestAddon.Rows[0]["allowableclasses"]);
			q.minLevel                        = Convert.ToInt32(dtQuest.Rows[0]["minlevel"]);
			q.maxLevel                        = Convert.ToInt32(dtQuestAddon.Rows[0]["maxlevel"]);
			q.reqFaction1                     = Convert.ToInt32(dtQuest.Rows[0]["requiredfactionid1"]);
			q.reqFaction2                     = Convert.ToInt32(dtQuest.Rows[0]["requiredfactionid2"]);
			q.reqValue1                       = Convert.ToInt32(dtQuest.Rows[0]["requiredfactionvalue1"]);
			q.reqValue2                       = Convert.ToInt32(dtQuest.Rows[0]["requiredfactionvalue2"]);
			q.minRepFaction                   = Convert.ToInt32(dtQuestAddon.Rows[0]["requiredminrepfaction"]);
			q.maxRepFaction                   = Convert.ToInt32(dtQuestAddon.Rows[0]["requiredmaxrepfaction"]);
			q.minRepValue                     = Convert.ToInt32(dtQuestAddon.Rows[0]["requiredminrepvalue"]);
			q.maxRepValue                     = Convert.ToInt32(dtQuestAddon.Rows[0]["requiredmaxrepvalue"]);
			q.zoneIdOrQuestSort               = Convert.ToInt32(dtQuest.Rows[0]["questsortid"]);
			q.skillId                         = Convert.ToInt32(dtQuestAddon.Rows[0]["requiredskillid"]);
			q.skillPoints                     = Convert.ToInt32(dtQuestAddon.Rows[0]["requiredskillpoints"]);
			q.type                            = Convert.ToInt32(dtQuest.Rows[0]["questtype"]);
			q.flags                           = Convert.ToInt32(dtQuest.Rows[0]["flags"]);
			q.specialFlags                    = Convert.ToInt32(dtQuestAddon.Rows[0]["specialflags"]);
			q.sourceItemId                    = Convert.ToInt32(dtQuest.Rows[0]["startitem"]);
			q.sourceItemCount                 = Convert.ToInt32(dtQuestAddon.Rows[0]["provideditemcount"]);
			q.sourceSpellId                   = Convert.ToInt32(dtQuestAddon.Rows[0]["sourcespellid"]);
			q.requiredNpcOrGoId1              = Convert.ToUInt32(dtQuest.Rows[0]["requirednpcorgo1"]);
			q.requiredNpcOrGoId2              = Convert.ToUInt32(dtQuest.Rows[0]["requirednpcorgo2"]);
			q.requiredNpcOrGoId3              = Convert.ToUInt32(dtQuest.Rows[0]["requirednpcorgo3"]);
			q.requiredNpcOrGoId4              = Convert.ToUInt32(dtQuest.Rows[0]["requirednpcorgo4"]);
			q.requiredNpcCount1               = Convert.ToInt32(dtQuest.Rows[0]["requirednpcorgocount1"]);
			q.requiredNpcCount2               = Convert.ToInt32(dtQuest.Rows[0]["requirednpcorgocount2"]);
			q.requiredNpcCount3               = Convert.ToInt32(dtQuest.Rows[0]["requirednpcorgocount3"]);
			q.requiredNpcCount4               = Convert.ToInt32(dtQuest.Rows[0]["requirednpcorgocount4"]);
			q.requiredItemId1                 = Convert.ToUInt32(dtQuest.Rows[0]["requireditemid1"]);
			q.requiredItemId2                 = Convert.ToUInt32(dtQuest.Rows[0]["requireditemid2"]);
			q.requiredItemId3                 = Convert.ToUInt32(dtQuest.Rows[0]["requireditemid3"]);
			q.requiredItemId4                 = Convert.ToUInt32(dtQuest.Rows[0]["requireditemid4"]);
			q.requiredItemId5                 = Convert.ToUInt32(dtQuest.Rows[0]["requireditemid5"]);
			q.requiredItemId6                 = Convert.ToUInt32(dtQuest.Rows[0]["requireditemid6"]);
			q.requiredItemCount1              = Convert.ToInt32(dtQuest.Rows[0]["requireditemcount1"]);
			q.requiredItemCount2              = Convert.ToInt32(dtQuest.Rows[0]["requireditemcount2"]);
			q.requiredItemCount3              = Convert.ToInt32(dtQuest.Rows[0]["requireditemcount3"]);
			q.requiredItemCount4              = Convert.ToInt32(dtQuest.Rows[0]["requireditemcount4"]);
			q.requiredItemCount5              = Convert.ToInt32(dtQuest.Rows[0]["requireditemcount5"]);
			q.requiredItemCount6              = Convert.ToInt32(dtQuest.Rows[0]["requireditemcount6"]);
			q.rewardChoiceItemId1             = Convert.ToUInt32(dtQuest.Rows[0]["rewardchoiceitemid1"]);
			q.rewardChoiceItemId2             = Convert.ToUInt32(dtQuest.Rows[0]["rewardchoiceitemid2"]);
			q.rewardChoiceItemId3             = Convert.ToUInt32(dtQuest.Rows[0]["rewardchoiceitemid3"]);
			q.rewardChoiceItemId4             = Convert.ToUInt32(dtQuest.Rows[0]["rewardchoiceitemid4"]);
			q.rewardChoiceItemId5             = Convert.ToUInt32(dtQuest.Rows[0]["rewardchoiceitemid5"]);
			q.rewardChoiceItemId6             = Convert.ToUInt32(dtQuest.Rows[0]["rewardchoiceitemid6"]);
			q.rewardChoiceItemCount1          = Convert.ToInt32(dtQuest.Rows[0]["rewardchoiceitemquantity1"]);
			q.rewardChoiceItemCount2          = Convert.ToInt32(dtQuest.Rows[0]["rewardchoiceitemquantity2"]);
			q.rewardChoiceItemCount3          = Convert.ToInt32(dtQuest.Rows[0]["rewardchoiceitemquantity3"]);
			q.rewardChoiceItemCount4          = Convert.ToInt32(dtQuest.Rows[0]["rewardchoiceitemquantity4"]);
			q.rewardChoiceItemCount5          = Convert.ToInt32(dtQuest.Rows[0]["rewardchoiceitemquantity5"]);
			q.rewardChoiceItemCount6          = Convert.ToInt32(dtQuest.Rows[0]["rewardchoiceitemquantity6"]);
			q.rewardItemId1                   = Convert.ToUInt32(dtQuest.Rows[0]["rewarditem1"]);
			q.rewardItemId2                   = Convert.ToUInt32(dtQuest.Rows[0]["rewarditem2"]);
			q.rewardItemId3                   = Convert.ToUInt32(dtQuest.Rows[0]["rewarditem3"]);
			q.rewardItemId4                   = Convert.ToUInt32(dtQuest.Rows[0]["rewarditem4"]);
			q.rewardItemCount1                = Convert.ToInt32(dtQuest.Rows[0]["rewardamount1"]);
			q.rewardItemCount2                = Convert.ToInt32(dtQuest.Rows[0]["rewardamount2"]);
			q.rewardItemCount3                = Convert.ToInt32(dtQuest.Rows[0]["rewardamount3"]);
			q.rewardItemCount4                = Convert.ToInt32(dtQuest.Rows[0]["rewardamount4"]);
			q.rewardFactionId1                = Convert.ToUInt32(dtQuest.Rows[0]["rewardfactionid1"]);
			q.rewardFactionId2                = Convert.ToUInt32(dtQuest.Rows[0]["rewardfactionid2"]);
			q.rewardFactionId3                = Convert.ToUInt32(dtQuest.Rows[0]["rewardfactionid3"]);
			q.rewardFactionId4                = Convert.ToUInt32(dtQuest.Rows[0]["rewardfactionid4"]);
			q.rewardFactionId5                = Convert.ToUInt32(dtQuest.Rows[0]["rewardfactionid5"]);
			q.rewardFactionValue1             = Convert.ToInt32(dtQuest.Rows[0]["rewardfactionvalue1"]);
			q.rewardFactionValue2             = Convert.ToInt32(dtQuest.Rows[0]["rewardfactionvalue2"]);
			q.rewardFactionValue3             = Convert.ToInt32(dtQuest.Rows[0]["rewardfactionvalue3"]);
			q.rewardFactionValue4             = Convert.ToInt32(dtQuest.Rows[0]["rewardfactionvalue4"]);
			q.rewardFactionValue5             = Convert.ToInt32(dtQuest.Rows[0]["rewardfactionvalue5"]);
			q.rewardOrRequiredMoney           = Convert.ToInt32(dtQuest.Rows[0]["rewardmoney"]);
			q.rewardOrRequiredMoneyML         = Convert.ToInt32(dtQuest.Rows[0]["rewardbonusmoney"]);
			q.rewardOrRequiredArenaPoints     = Convert.ToInt32(dtQuest.Rows[0]["rewardarenapoints"]);
			q.rewardOrRequiredHonorPoints     = Convert.ToInt32(dtQuest.Rows[0]["rewardHonor"]);
			q.rewardOrRequiredHonorMultiplier = Convert.ToInt32(dtQuest.Rows[0]["rewardkillhonor"]);
			q.rewardTitleId                   = Convert.ToUInt32(dtQuest.Rows[0]["rewardtitle"]);
			q.rewardTalent                    = Convert.ToUInt32(dtQuest.Rows[0]["rewardtalents"]);
			q.mailTemplateId                  = Convert.ToUInt32(dtQuestAddon.Rows[0]["rewardmailtemplateid"]);
			q.mailDelay                       = Convert.ToUInt32(dtQuestAddon.Rows[0]["rewardmaildelay"]);
			q.rewardSpell                     = Convert.ToUInt32(dtQuest.Rows[0]["rewardspell"]);

			return q;
		}

		public QuestGT[] buildQuestGT(DataTable dt) {
			var qgt = new QuestGT[dt.Rows.Count];

			for(var i = 0; i < qgt.Length; i++) {
				qgt[i] = new QuestGT();

				qgt[i].entry   = Convert.ToUInt32(dt.Rows[i]["entry"]);
				qgt[i].name    = dt.Rows[i]["name"].ToString();
				qgt[i].subname = dt.Rows[i]["subname"].ToString();
			}

			return qgt;
		}

		#endregion

		#region Game Object

		public DataTable searchForAllGameObjects() {
			return executeQuery("SELECT entry, name FROM gameobject_template ORDER BY entry;");
		}

		public DataTable searchForAllGameObjectsByEntry(uint entry) {
			return executeQuery("SELECT entry, name FROM gameobject_template WHERE entry = '?value' ORDER BY entry;", new MySqlParameter("?value", entry));
		}

		public DataTable searchForAllGameObjectsByName(string name, bool isExact) {
			if(isExact) {
				return executeQuery("SELECT entry, name FROM gameobject_template WHERE name = '?value' ORDER BY entry;", new MySqlParameter("?value", name));
			}

			return executeQuery("SELECT entry, name FROM gameobject_template WHERE name LIKE '%?value%' ORDER BY entry;", new MySqlParameter("?value", name));
		}

		public GameObject getGameObject(uint entry) {
			DataTable dtTemp = executeQuery("SELECT entry, type, displayid, name, size, ainame, scriptname, " +
				"data0, data1, data2, data3, data4, data5, data6, data7, data8, data9, data10, data11, data12, data13, data14, data15, data16, data17, data18, data19, data20, data21, data22, data23 " +
				"FROM gameobject_template WHERE entry = '?value' ORDER BY entry;", new MySqlParameter("?value", entry));

			DataTable dtAddon = executeQuery("SELECT faction, flags FROM gameobject_template_addon WHERE entry = '?value' ORDER BY entry;", new MySqlParameter("?value", entry));

			return buildGameObject(dtTemp, dtAddon);
		}

		public GameObject buildGameObject(DataTable dt, DataTable dtAddon) {
			GameObject go = new GameObject();

			go.entry      = Convert.ToUInt32(dt.Rows[0]["entry"]);
			go.type       = Convert.ToUInt32(dt.Rows[0]["type"]);
			go.displayId  = Convert.ToUInt32(dt.Rows[0]["displayid"]);
			go.name       = dt.Rows[0]["name"].ToString();
			go.size       = Convert.ToSingle(dt.Rows[0]["size"]);
			go.aiName     = dt.Rows[0]["ainame"].ToString();
			go.scriptName = dt.Rows[0]["scriptname"].ToString();
			go.data0      = Convert.ToInt32(dt.Rows[0]["data0"]);
			go.data1      = Convert.ToInt32(dt.Rows[0]["data1"]);
			go.data2      = Convert.ToInt32(dt.Rows[0]["data2"]);
			go.data3      = Convert.ToInt32(dt.Rows[0]["data3"]);
			go.data4      = Convert.ToInt32(dt.Rows[0]["data4"]);
			go.data5      = Convert.ToInt32(dt.Rows[0]["data5"]);
			go.data6      = Convert.ToInt32(dt.Rows[0]["data6"]);
			go.data7      = Convert.ToInt32(dt.Rows[0]["data7"]);
			go.data8      = Convert.ToInt32(dt.Rows[0]["data8"]);
			go.data9      = Convert.ToInt32(dt.Rows[0]["data9"]);
			go.data10     = Convert.ToInt32(dt.Rows[0]["data10"]);
			go.data11     = Convert.ToInt32(dt.Rows[0]["data11"]);
			go.data12     = Convert.ToInt32(dt.Rows[0]["data12"]);
			go.data13     = Convert.ToInt32(dt.Rows[0]["data13"]);
			go.data14     = Convert.ToInt32(dt.Rows[0]["data14"]);
			go.data15     = Convert.ToInt32(dt.Rows[0]["data15"]);
			go.data16     = Convert.ToInt32(dt.Rows[0]["data16"]);
			go.data17     = Convert.ToInt32(dt.Rows[0]["data17"]);
			go.data18     = Convert.ToInt32(dt.Rows[0]["data18"]);
			go.data19     = Convert.ToInt32(dt.Rows[0]["data19"]);
			go.data20     = Convert.ToInt32(dt.Rows[0]["data20"]);
			go.data21     = Convert.ToInt32(dt.Rows[0]["data21"]);
			go.data22     = Convert.ToInt32(dt.Rows[0]["data22"]);
			go.data23     = Convert.ToInt32(dt.Rows[0]["data23"]);

			return go;
		}

		#endregion

		#region Item

		public DataTable searchForAllItems() {
			return executeQuery("SELECT entry, name, description, class, subclass, quality, requiredlevel FROM item_template ORDER BY entry;");
		}

		public DataTable searchForItemByEntry(uint entry) {
			return executeQuery("SELECT entry, name, description, class, subclass, quality, requiredlevel FROM item_template WHERE entry = '?value' ORDER BY entry;", new MySqlParameter("?value", entry));
		}

		public DataTable searchForItems(string name, string desc, int iClass, int iSub, int quality, int reqlvl) {
			string query = "SELECT entry, name, description, class, subclass, quality, requiredlevel FROM item_template WHERE 1 = 1 ";

			bool isNameExact = name.StartsWith("#");
			bool isDescExact = desc.StartsWith("#"); 

			name = (isNameExact ? Classes.UtilityHelper.removeExactSign(name) : name);
			desc = (isNameExact ? Classes.UtilityHelper.removeExactSign(desc) : desc);

			if(!string.IsNullOrEmpty(name))      { query += "AND " + (isNameExact ? "name = '?name' " : "name LIKE '%?name%' "); }
			if(!string.IsNullOrEmpty(desc)) { query += "AND " + (isDescExact ? "description = '?desc' " : "description LIKE '%?desc%' "); }
			if(iClass >= 0)                 { query += "AND class = '?class' "; }
			if(iSub >= 0)                   { query += "AND subclass LIKE '?sub' "; }
			if(quality >= 0)                { query += "AND quality LIKE '?quality' "; }
			if(reqlvl >= 0)                 { query += "AND requiredlevel LIKE '?reqlvl' "; }

			query += "ORDER BY entry;";

			return executeQuery(query, new MySqlParameter("?name", name), new MySqlParameter("?desc", desc), new MySqlParameter("?class", iClass), 
				new MySqlParameter("?sub", iSub), new MySqlParameter("?quality", quality), new MySqlParameter("?reqlvl", reqlvl));
		}

		public DataTable searchForItemsByQuality(int quality) {
			return executeQuery("SELECT entry, name, description, class, subclass, quality, requiredlevel FROM item_template WHERE quality = '?value' ORDER BY entry;", new MySqlParameter("?value", quality));
		}

		public Item getItem(uint entry) {
			DataTable dt = executeQuery("SELECT * FROM item_template WHERE entry = '?value' ORDER BY entry;", new MySqlParameter("?value", entry));

			return buildItem(dt);
		}

		public ItemLPMD[] getItemLPMD(uint entry, LPMD type) {
			DataTable dt = null;
			DataTable dtNames = null;
			// 
			switch(type) {
				case LPMD.LOOT:
					dt = executeQuery("SELECT entry, item, reference, chance, questrequired, lootmode, groupid, mincount, maxcount FROM item_loot_template WHERE entry = '?value' ORDER BY entry;", new MySqlParameter("?value", entry));
					dtNames = executeQuery("SELECT name FROM item_template WHERE entry IN (SELECT item FROM item_loot_template WHERE entry = '?value') ORDER BY entry;", new MySqlParameter("?value", entry));
					break;
				case LPMD.PROSPECTING:
					dt = executeQuery("SELECT entry, item, reference, chance, questrequired, lootmode, groupid, mincount, maxcount FROM prospecting_loot_template WHERE entry = '?value' ORDER BY entry;", new MySqlParameter("?value", entry));
					dtNames = executeQuery("SELECT name FROM item_template WHERE entry IN (SELECT item FROM prospecting_loot_template WHERE entry = '?value') ORDER BY entry;", new MySqlParameter("?value", entry));
					break;
				case LPMD.MILLING:
					dt = executeQuery("SELECT entry, item, reference, chance, questrequired, lootmode, groupid, mincount, maxcount FROM milling_loot_template WHERE entry = '?value' ORDER BY entry;", new MySqlParameter("?value", entry));
					dtNames = executeQuery("SELECT name FROM item_template WHERE entry IN (SELECT item FROM milling_loot_template WHERE entry = '?value') ORDER BY entry;", new MySqlParameter("?value", entry));
					break;
				case LPMD.DISENCHANT:
					dt = executeQuery("SELECT entry, item, reference, chance, questrequired, lootmode, groupid, mincount, maxcount FROM disenchant_loot_template WHERE entry = '?value' ORDER BY entry;", new MySqlParameter("?value", entry));
					dtNames = executeQuery("SELECT name FROM item_template WHERE entry IN (SELECT item FROM disenchant_loot_template WHERE entry = '?value') ORDER BY entry;", new MySqlParameter("?value", entry));
					break;
			}

			return buildItemLPMD(dt, dtNames);
		}

		public Item buildItem(DataTable dt) {
			if(dt != null) {
				Item item = new Item();

				item.entry                  = Convert.ToUInt32(dt.Rows[0]["entry"]);
				item.iClass                 = Convert.ToUInt32(dt.Rows[0]["class"]);
				item.iSub                   = Convert.ToUInt32(dt.Rows[0]["subclass"]);
				item.name                   = dt.Rows[0]["name"].ToString();
				item.description            = dt.Rows[0]["description"].ToString();
				item.displayId              = Convert.ToUInt32(dt.Rows[0]["displayid"]);
				item.quality                = Convert.ToUInt32(dt.Rows[0]["Quality"]);
				item.buycount               = Convert.ToInt32(dt.Rows[0]["BuyCount"]);
				item.inventory              = Convert.ToUInt32(dt.Rows[0]["InventoryType"]);
				item.flags                  = Convert.ToInt32(dt.Rows[0]["Flags"]);
				item.extraFlags             = Convert.ToInt32(dt.Rows[0]["FlagsExtra"]);
				item.maxCount               = Convert.ToInt32(dt.Rows[0]["maxcount"]);
				item.containerSlot          = Convert.ToUInt32(dt.Rows[0]["ContainerSlots"]);
				item.buyPrice               = Convert.ToUInt32(dt.Rows[0]["BuyPrice"]);
				item.sellPrice              = Convert.ToUInt32(dt.Rows[0]["SellPrice"]);
				item.damageType1            = Convert.ToUInt32(dt.Rows[0]["dmg_type1"]);
				item.damageType2            = Convert.ToUInt32(dt.Rows[0]["dmg_type2"]);
				item.damageMin1             = Convert.ToUInt32(dt.Rows[0]["dmg_min1"]);
				item.damageMin2             = Convert.ToUInt32(dt.Rows[0]["dmg_min2"]);
				item.damageMax1             = Convert.ToUInt32(dt.Rows[0]["dmg_max1"]);
				item.damageMax2             = Convert.ToUInt32(dt.Rows[0]["dmg_max2"]);
				item.delay                  = Convert.ToUInt32(dt.Rows[0]["delay"]);
				item.ammoType               = Convert.ToUInt32(dt.Rows[0]["ammo_type"]);
				item.rangedMod              = Convert.ToUInt32(dt.Rows[0]["RangedModRange"]);
				item.itemSet                = Convert.ToUInt32(dt.Rows[0]["itemset"]);
				item.bonding                = Convert.ToUInt32(dt.Rows[0]["bonding"]);
				item.block                  = Convert.ToInt32(dt.Rows[0]["block"]);
				item.durability             = Convert.ToInt32(dt.Rows[0]["MaxDurability"]);
				item.sheath                 = Convert.ToUInt32(dt.Rows[0]["sheath"]);
				item.reistanceHoly          = Convert.ToUInt32(dt.Rows[0]["holy_res"]);
				item.reistanceFrost         = Convert.ToUInt32(dt.Rows[0]["frost_res"]);
				item.reistanceFire          = Convert.ToUInt32(dt.Rows[0]["fire_res"]);
				item.reistanceShadow        = Convert.ToUInt32(dt.Rows[0]["shadow_res"]);
				item.reistanceNature        = Convert.ToUInt32(dt.Rows[0]["nature_res"]);
				item.reistanceArcane        = Convert.ToUInt32(dt.Rows[0]["arcane_res"]);
				item.socketColor1           = Convert.ToUInt32(dt.Rows[0]["socketColor_1"].ToString());
				item.socketColor2           = Convert.ToUInt32(dt.Rows[0]["socketColor_2"].ToString());
				item.socketColor3           = Convert.ToUInt32(dt.Rows[0]["socketColor_3"].ToString());
				item.socketContent1         = Convert.ToUInt32(dt.Rows[0]["socketContent_1"].ToString());
				item.socketContent2         = Convert.ToUInt32(dt.Rows[0]["socketContent_2"].ToString());
				item.socketContent3         = Convert.ToUInt32(dt.Rows[0]["socketContent_3"].ToString());
				item.socketBonus            = Convert.ToUInt32(dt.Rows[0]["socketBonus"].ToString());
				item.socketGemProperty      = Convert.ToUInt32(dt.Rows[0]["GemProperties"].ToString());
				item.spellEntry1            = Convert.ToUInt32(dt.Rows[0]["spellid_1"]);
				item.spellEntry2            = Convert.ToUInt32(dt.Rows[0]["spellid_2"]);
				item.spellEntry3            = Convert.ToUInt32(dt.Rows[0]["spellid_3"]);
				item.spellEntry4            = Convert.ToUInt32(dt.Rows[0]["spellid_4"]);
				item.spellEntry5            = Convert.ToUInt32(dt.Rows[0]["spellid_5"]);
				item.spellTrigger1          = Convert.ToInt32(dt.Rows[0]["spelltrigger_1"]);
				item.spellTrigger2          = Convert.ToInt32(dt.Rows[0]["spelltrigger_2"]);
				item.spellTrigger3          = Convert.ToInt32(dt.Rows[0]["spelltrigger_3"]);
				item.spellTrigger4          = Convert.ToInt32(dt.Rows[0]["spelltrigger_4"]);
				item.spellTrigger5          = Convert.ToInt32(dt.Rows[0]["spelltrigger_5"]);
				item.spellCharges1          = Convert.ToInt32(dt.Rows[0]["spellcharges_1"]);
				item.spellCharges2          = Convert.ToInt32(dt.Rows[0]["spellcharges_2"]);
				item.spellCharges3          = Convert.ToInt32(dt.Rows[0]["spellcharges_3"]);
				item.spellCharges4          = Convert.ToInt32(dt.Rows[0]["spellcharges_4"]);
				item.spellCharges5          = Convert.ToInt32(dt.Rows[0]["spellcharges_5"]);
				item.spellPPMRate1          = Convert.ToInt32(dt.Rows[0]["spellppmRate_1"]);
				item.spellPPMRate2          = Convert.ToInt32(dt.Rows[0]["spellppmRate_2"]);
				item.spellPPMRate3          = Convert.ToInt32(dt.Rows[0]["spellppmRate_3"]);
				item.spellPPMRate4          = Convert.ToInt32(dt.Rows[0]["spellppmRate_4"]);
				item.spellPPMRate5          = Convert.ToInt32(dt.Rows[0]["spellppmRate_5"]);
				item.spellCooldown1         = Convert.ToSingle(dt.Rows[0]["spellcooldown_1"]);
				item.spellCooldown2         = Convert.ToSingle(dt.Rows[0]["spellcooldown_2"]);
				item.spellCooldown3         = Convert.ToSingle(dt.Rows[0]["spellcooldown_3"]);
				item.spellCooldown4         = Convert.ToSingle(dt.Rows[0]["spellcooldown_4"]);
				item.spellCooldown5         = Convert.ToSingle(dt.Rows[0]["spellcooldown_5"]);
				item.spellCategory1         = Convert.ToInt32(dt.Rows[0]["spellcategory_1"]);
				item.spellCategory2         = Convert.ToInt32(dt.Rows[0]["spellcategory_2"]);
				item.spellCategory3         = Convert.ToInt32(dt.Rows[0]["spellcategory_3"]);
				item.spellCategory4         = Convert.ToInt32(dt.Rows[0]["spellcategory_4"]);
				item.spellCategory5         = Convert.ToInt32(dt.Rows[0]["spellcategory_5"]);
				item.spellCategoryCooldown1 = Convert.ToSingle(dt.Rows[0]["spellcategorycooldown_1"]);
				item.spellCategoryCooldown2 = Convert.ToSingle(dt.Rows[0]["spellcategorycooldown_2"]);
				item.spellCategoryCooldown3 = Convert.ToSingle(dt.Rows[0]["spellcategorycooldown_3"]);
				item.spellCategoryCooldown4 = Convert.ToSingle(dt.Rows[0]["spellcategorycooldown_4"]);
				item.spellCategoryCooldown5 = Convert.ToSingle(dt.Rows[0]["spellcategorycooldown_5"]);
				item.startQuest             = Convert.ToUInt32(dt.Rows[0]["startquest"]);
				item.material               = Convert.ToInt32(dt.Rows[0]["material"]);
				item.property               = Convert.ToUInt32(dt.Rows[0]["randomproperty"]);
				item.suffix                 = Convert.ToUInt32(dt.Rows[0]["randomsuffix"]);
				item.area                   = Convert.ToUInt32(dt.Rows[0]["area"]);
				item.map                    = Convert.ToUInt32(dt.Rows[0]["map"]);
				item.disenchantId           = Convert.ToUInt32(dt.Rows[0]["disenchantid"]);
				item.pageText               = Convert.ToUInt32(dt.Rows[0]["pagetext"]);
				item.languageId             = Convert.ToUInt32(dt.Rows[0]["languageid"]);
				item.pageMaterial           = Convert.ToUInt32(dt.Rows[0]["pagematerial"]);
				item.foodType               = Convert.ToUInt32(dt.Rows[0]["foodtype"]);
				item.lockId                 = Convert.ToUInt32(dt.Rows[0]["lockid"]);
				item.holidayId              = Convert.ToUInt32(dt.Rows[0]["holidayid"]);
				item.bagFamily              = Convert.ToUInt32(dt.Rows[0]["BagFamily"]);
				item.modifier               = Convert.ToUInt32(dt.Rows[0]["ArmorDamageModifier"]);
				item.duration               = Convert.ToUInt32(dt.Rows[0]["duration"]);
				item.limitCategory          = Convert.ToUInt32(dt.Rows[0]["ItemLimitCategory"]);
				item.minMoney               = Convert.ToUInt32(dt.Rows[0]["minMoneyLoot"]);
				item.maxMoney               = Convert.ToUInt32(dt.Rows[0]["maxMoneyLoot"]);
				item.flagsCustom            = Convert.ToUInt32(dt.Rows[0]["flagscustom"]);
				item.totemCategory          = Convert.ToUInt32(dt.Rows[0]["TotemCategory"]);
				item.reqRace                = Convert.ToInt32(dt.Rows[0]["AllowableRace"]);
				item.reqClass               = Convert.ToInt32(dt.Rows[0]["AllowableClass"]);
				item.reqLevel               = Convert.ToUInt32(dt.Rows[0]["ItemLevel"]);
				item.reqSkill               = Convert.ToUInt32(dt.Rows[0]["RequiredLevel"]);
				item.reqSkillRank           = Convert.ToUInt32(dt.Rows[0]["RequiredSkill"]);
				item.reqHonorRank           = Convert.ToUInt32(dt.Rows[0]["RequiredSkillRank"]);
				item.reqRepFaction          = Convert.ToUInt32(dt.Rows[0]["requiredspell"]);
				item.reqRepRank             = Convert.ToUInt32(dt.Rows[0]["requiredhonorrank"]);
				item.reqDisenchant          = Convert.ToUInt32(dt.Rows[0]["RequiredCityRank"]);
				item.reqSpell               = Convert.ToUInt32(dt.Rows[0]["RequiredReputationFaction"]);
				item.reqCityRank            = Convert.ToUInt32(dt.Rows[0]["RequiredReputationRank"]);
				item.reqItemLevel           = Convert.ToInt32(dt.Rows[0]["RequiredDisenchantSkill"]);
				item.statsCount             = Convert.ToUInt32(dt.Rows[0]["StatsCount"]);
				item.statsType1             = Convert.ToUInt32(dt.Rows[0]["stat_type1"]);
				item.statsType2             = Convert.ToUInt32(dt.Rows[0]["stat_type2"]);
				item.statsType3             = Convert.ToUInt32(dt.Rows[0]["stat_type3"]);
				item.statsType4             = Convert.ToUInt32(dt.Rows[0]["stat_type4"]);
				item.statsType5             = Convert.ToUInt32(dt.Rows[0]["stat_type5"]);
				item.statsType6             = Convert.ToUInt32(dt.Rows[0]["stat_type6"]);
				item.statsType7             = Convert.ToUInt32(dt.Rows[0]["stat_type7"]);
				item.statsType8             = Convert.ToUInt32(dt.Rows[0]["stat_type8"]);
				item.statsType9             = Convert.ToUInt32(dt.Rows[0]["stat_type9"]);
				item.statsType10            = Convert.ToUInt32(dt.Rows[0]["stat_type10"]);
				item.statsValue1            = Convert.ToInt32(dt.Rows[0]["stat_value1"]);
				item.statsValue2            = Convert.ToInt32(dt.Rows[0]["stat_value2"]);
				item.statsValue3            = Convert.ToInt32(dt.Rows[0]["stat_value3"]);
				item.statsValue4            = Convert.ToInt32(dt.Rows[0]["stat_value4"]);
				item.statsValue5            = Convert.ToInt32(dt.Rows[0]["stat_value5"]);
				item.statsValue6            = Convert.ToInt32(dt.Rows[0]["stat_value6"]);
				item.statsValue7            = Convert.ToInt32(dt.Rows[0]["stat_value7"]);
				item.statsValue8            = Convert.ToInt32(dt.Rows[0]["stat_value8"]);
				item.statsValue9            = Convert.ToInt32(dt.Rows[0]["stat_value9"]);
				item.statsValue10           = Convert.ToInt32(dt.Rows[0]["stat_value10"]);
				item.scalingStatDist        = Convert.ToInt32(dt.Rows[0]["ScalingStatDistribution"]);
				item.scalingStatValue       = Convert.ToInt32(dt.Rows[0]["ScalingStatValue"]);

				return item;
			}

			return null;
		}

		public ItemLPMD[] buildItemLPMD(DataTable dt, DataTable names) {
			if(dt != null) {
				ItemLPMD[] items = new ItemLPMD[dt.Rows.Count];

				for(var i = 0; i < items.Length; i++) {
					items[i] = new ItemLPMD();

					items[i].entry         = Convert.ToUInt32(dt.Rows[i]["entry"]);
					items[i].item          = Convert.ToUInt32(dt.Rows[i]["item"]);
					items[i].reference     = Convert.ToInt32(dt.Rows[i]["reference"]);
					items[i].chance        = Convert.ToUInt32(dt.Rows[i]["chance"]);
					items[i].questRequired = Convert.ToByte(dt.Rows[i]["questrequired"]);
					items[i].lootMode      = Convert.ToUInt16(dt.Rows[i]["lootmode"]);
					items[i].groupId       = Convert.ToUInt16(dt.Rows[i]["groupid"]);
					items[i].minCount      = Convert.ToUInt16(dt.Rows[i]["mincount"]);
					items[i].maxCount      = Convert.ToUInt16(dt.Rows[i]["maxcount"]);
					items[i].name          = (i < names.Rows.Count ? names.Rows[i]["name"].ToString() : "");
				}

				return items;
			}

			return null;
		}

		public enum LPMD {
			LOOT,
			PROSPECTING,
			MILLING,
			DISENCHANT
		}

		#endregion

	}
}
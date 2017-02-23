using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Manti.Classes.CreatureTab;
using Manti.Classes.QuestTab;
using Manti.Classes.GameObjectTab;
using Manti.Classes.ItemTab;

namespace Manti.Classes.Generate {
	public class GenerateWorld : SqlGenerate {
		public string creatureToSql(Creature c) {
			if(c != null) {
				string[] columns = { "entry", "difficulty_entry_1", "difficulty_entry_2", "difficulty_entry_3", "name", "subname", "modelid1", "modelid2", "modelid3", "modelid4", "minlevel", "maxlevel", "mingold", "maxgold", "KillCredit1", "KillCredit2",
					"rank", "scale", "faction", "npcflag","spell1", "spell2", "spell3", "spell4", "spell5", "spell6", "spell7", "spell8","HealthModifier", "ManaModifier", "ArmorModifier", "DamageModifier", "ExperienceModifier",
					"speed_walk", "speed_run", "BaseAttackTime", "RangeAttackTime", "BaseVariance", "RangeVariance", "dmgschool", "AIName", "MovementType", "InhabitType", "HoverHeight", "gossip_menu_id", "movementId", "ScriptName",
					"VehicleId", "mechanic_immune_mask", "family", "type", "type_flags", "flags_extra", "unit_class", "unit_flags", "unit_flags2", "dynamicflags", "RegenHealth", "resistance1", "resistance2", "resistance3", "resistance4", "resistance5", "resistance6",
					"trainer_type", "trainer_spell", "trainer_class", "trainer_race", "lootid", "pickpocketloot", "skinloot"};

				object[] values = { c.entry, c.diffEntry1, c.diffEntry2, c.diffEntry3, c.name, c.subname, c.modelId1, c.modelId2, c.modelId3, c.modelId4, c.minlevel, c.maxlevel, c.mingold, c.maxgold, c.killCredit1, c.killCredit2,
					c.rank, c.scale, c.faction, c.npcFlags, c.spell1, c.spell2, c.spell3, c.spell4, c.spell5, c.spell6, c.spell7, c.spell8, c.modHealth, c.modMana, c.modArmor, c.modDamage, c.modExp,
					c.speedWalk, c.speedRun, c.baseAttackTime, c.rangedAttackTime, c.bVariance, c.rVariance, c.dSchool, c.aiName, c.movementType, c.inhabitType, c.hoverHeight, c.gossipMenuId, c.movementId, c.scriptName,
					c.vehicleId, c.mechanicImmuneMask, c.family, c.familyType, c.typeFlags, c.extraFlags, c.unitClass, c.unitFlags1, c.unitFlags2, c.dynamicFlags, Convert.ToByte(c.isRegenHealth), c.resistHoly, c.resistFire, c.resistNature, c.resistFrost, c.resistShadow, c.resistArcane,
					c.trainerType, c.trainerSpell, c.trainerClass, c.trainerRace, c.lootId, c.pickpocketId, c.skinId };

				return insertOrUpdateToDatabase("creature_template", columns, values);
			}

			return null;
		}
		
		public string creatureVendorToSql(CreatureVendor[] cv) {
			if(cv != null) {
				if(cv.Length > 0) {
					string sql = "";

					string[] columns = { "entry", "slot", "item", "maxcount", "incrtime", "extendedcost" };
					object[] values;

					foreach(var c in cv) {
						values = new object[] { c.entry, c.slot, c.item, c.maxCount, c.increaseTime, c.extendedCost };
						sql += insertToDatabase("npc_vendor", columns, values);
					}

					return deleteFromDatabase("npc_vendor", "entry", cv[0].entry) + sql;
				}
			}

			return null;
		}

		public string creatureLPSToSql(string table, CreatureLPS[] clps) {
			if(clps != null) {
				if(clps.Length > 0) {
					string sql = "";

					string[] columns = { "entry", "item", "reference", "chance", "questRequired", "lootMode", "groupId", "minCount", "maxCount" };
					object[] values;

					foreach(var c in clps) {
						values = new object[] { c.entry, c.item, c.reference, c.chance, c.questRequired, c.lootMode, c.groupId, c.minCount, c.maxCount };
						sql += insertToDatabase(table, columns, values);
					}

					return deleteFromDatabase(table, "entry", clps[0].entry) + sql;
				}
			}

			return null;
		}

		public string questToSql(Quest q) {
			if(q != null) {
				string[] columns = { "id", "logtitle", "logdescription", "questdescription", "areadescription", "questcompletionlog", "objectivetext1", "objectivetext2", "objectivetext3", "objectivetext4", "requiredplayerkills", "timeallowed", "questinfoid", "questlevel", "suggestedgroupnum", "exclusivegroup", "prevquestid", "nextquestid",
					"allowableraces", "allowableclasses", "minlevel", "maxlevel", "requiredfactionid1", "requiredfactionid2", "requiredfactionvalue1", "requiredfactionvalue2", "requiredminrepfaction", "requiredmaxrepfaction", "requiredminrepvalue", "requiredmaxrepvalue", "questsortid", "requiredskillid", "requiredskillpoints", "questtype", "flags", "specialflags", "startitem", "provideditemcount", "sourcespellid",
					"requirednpcorgo1", "requirednpcorgo2", "requirednpcorgo3", "requirednpcorgo4", "requirednpcorgocount1", "requirednpcorgocount2", "requirednpcorgocount3", "requirednpcorgocount4", "requireditemid1", "requireditemid2", "requireditemid3", "requireditemid4", "requireditemid5", "requireditemid6", "requireditemcount1", "requireditemcount2", "requireditemcount3", "requireditemcount4", "requireditemcount5", "requireditemcount6",
					"rewardchoiceitemid1", "rewardchoiceitemid2", "rewardchoiceitemid3", "rewardchoiceitemid4", "rewardchoiceitemid5", "rewardchoiceitemid6", "rewardchoiceitemquantity1", "rewardchoiceitemquantity2", "rewardchoiceitemquantity3", "rewardchoiceitemquantity4", "rewardchoiceitemquantity5", "rewardchoiceitemquantity6", "rewarditem1", "rewarditem2", "rewarditem3", "rewarditem4", "rewardamount1", "rewardamount2", "rewardamount3", "rewardamount4",
					"rewardfactionid1", "rewardfactionid2", "rewardfactionid3", "rewardfactionid4", "rewardfactionid5", "rewardfactionvalue1", "rewardfactionvalue2", "rewardfactionvalue3", "rewardfactionvalue4", "rewardfactionvalue5",
					"rewardmoney", "rewardbonusmoney", "rewardarenapoints", "rewardHonor", "rewardkillhonor", "rewardtitle", "rewardtalents", "rewardmailtemplateid", "rewardmaildelay", "rewarddisplayspell", "rewardspell" };

				object[] values = { q.id, q.title, q.logDescription, q.questDescription, q.area, q.completed, q.objective1, q.objective2, q.objective3, q.objective4, q.requirePlayerKills, q.timeAllowed, q.questInfo, q.questLevel, q.suggestedPlayers, q.exclusiveGroup, q.prevQuest, q.nextQuest,
					q.races, q.classes, q.minLevel, q.maxLevel, q.reqFaction1, q.reqFaction2, q.reqValue1, q.reqValue2, q.minRepFaction, q.maxRepFaction, q.minRepValue, q.maxRepValue, q.zoneIdOrQuestSort, q.skillId, q.skillPoints, q.type, q.flags, q.specialFlags, q.sourceItemId, q.sourceItemCount, q.sourceSpellId,
					q.requiredNpcOrGoId1, q.requiredNpcOrGoId2, q.requiredNpcOrGoId3, q.requiredNpcOrGoId4, q.requiredNpcCount1, q.requiredNpcCount2, q.requiredNpcCount3, q.requiredNpcCount4, q.requiredItemId1, q.requiredItemId2, q.requiredItemId3, q.requiredItemId4, q.requiredItemId5, q.requiredItemId6,
					q.requiredItemCount1, q.requiredItemCount2, q.requiredItemCount3, q.requiredItemCount4, q.requiredItemCount5, q.requiredItemCount6, q.rewardChoiceItemId1, q.rewardChoiceItemId2, q.rewardChoiceItemId3, q.rewardChoiceItemId4, q.rewardChoiceItemId5, q.rewardChoiceItemId6,
					q.rewardChoiceItemCount1, q.rewardChoiceItemCount2, q.rewardChoiceItemCount3, q.rewardChoiceItemCount4, q.rewardChoiceItemCount5, q.rewardChoiceItemCount6, q.rewardItemId1, q.rewardItemId2, q.rewardItemId3, q.rewardItemId4, q.rewardItemCount1, q.rewardItemCount2, q.rewardItemCount3, q.rewardItemCount4,
					q.rewardFactionId1, q.rewardFactionId2, q.rewardFactionId3, q.rewardFactionId4, q.rewardFactionId5, q.rewardFactionValue1, q.rewardFactionValue2, q.rewardFactionValue3, q.rewardFactionValue4, q.rewardFactionValue5,
					q.rewardOrRequiredMoney, q.rewardOrRequiredMoneyML, q.rewardOrRequiredArenaPoints, q.rewardOrRequiredHonorPoints, q.rewardOrRequiredHonorMultiplier, q.rewardTitleId, q.rewardTalent, q.mailTemplateId, q.mailDelay, q.rewardDisplaySpell, q.rewardSpell };

				return insertOrUpdateToDatabase("quest_template", columns, values);
			}

			return null;
		}

		public string gameObjectToSql(GameObject go) {
			if(go != null) {
				string[] tempColumns = { "entry", "type", "displayid", "name", "size", "ainame", "scriptname", "data0", "data1", "data2", "data3", "data4", "data5", "data6", "data7", "data8", "data9", "data10",
					"data11", "data12", "data13", "data14", "data15", "data16", "data17", "data18", "data19", "data20", "data21", "data22", "data23" };

				object[] tempValues = { go.entry, go.type, go.displayId, go.name, go.size, go.aiName, go.scriptName, go.data0, go.data1, go.data2, go.data3, go.data4, go.data5, go.data6, go.data7, go.data8, go.data9, go.data10,
					go.data11, go.data12, go.data13, go.data14, go.data15, go.data16, go.data17, go.data18, go.data19, go.data20, go.data21, go.data22, go.data23};

				string[] addonColumns = { "faction", "flags" };

				object[] addonValues = { go.faction, go.flags };

				return insertOrUpdateToDatabase("gameobject_template", tempColumns, tempValues) + insertOrUpdateToDatabase("gameobject_template_addon", addonColumns, addonValues);
			}

			return null;
		}

		public string itemToSql(Item item) {
			if(item != null) {
				string[] columns = { "entry", "class", "subclass", "name", "description", "displayid", "Quality", "BuyCount", "InventoryType", "Flags", "FlagsExtra", "maxcount", "ContainerSlots", "BuyPrice", "SellPrice",
					"dmg_type1", "dmg_type2", "dmg_min1", "dmg_min2", "dmg_max1", "dmg_max2", "delay", "ammo_type", "RangedModRange", "itemset", "bonding", "block", "MaxDurability", "sheath", "holy_res", "frost_res", "fire_res", "shadow_res", "nature_res", "arcane_res",
					"socketColor_1", "socketColor_2", "socketColor_3", "socketContent_1", "socketContent_2", "socketContent_3", "socketBonus", "GemProperties", "spellid_1", "spellid_2", "spellid_3", "spellid_4", "spellid_5", "spelltrigger_1", "spelltrigger_2", "spelltrigger_3", "spelltrigger_4", "spelltrigger_5",
					"spellcharges_1", "spellcharges_2", "spellcharges_3", "spellcharges_4", "spellcharges_5", "spellppmRate_1", "spellppmRate_2", "spellppmRate_3", "spellppmRate_4", "spellppmRate_5", "spellcooldown_1", "spellcooldown_2", "spellcooldown_3", "spellcooldown_4", "spellcooldown_5",
					"spellcategory_1", "spellcategory_2", "spellcategory_3", "spellcategory_4", "spellcategory_5", "spellcategorycooldown_1", "spellcategorycooldown_2", "spellcategorycooldown_3", "spellcategorycooldown_4", "spellcategorycooldown_5", "startquest", "material", "randomproperty", "randomsuffix", "area", "map",
					"disenchantid", "pagetext", "languageid", "pagematerial", "foodtype", "lockid", "holidayid", "BagFamily", "ArmorDamageModifier", "duration", "ItemLimitCategory", "minMoneyLoot", "maxMoneyLoot", "flagscustom", "TotemCategory", "AllowableRace", "AllowableClass", "ItemLevel",
					"RequiredLevel", "RequiredSkill", "RequiredSkillRank", "requiredspell", "requiredhonorrank", "RequiredCityRank", "RequiredReputationFaction", "RequiredReputationRank", "RequiredDisenchantSkill", "StatsCount", "stat_type1", "stat_type2", "stat_type3", "stat_type4", "stat_type5",
					"stat_type6", "stat_type7", "stat_type8", "stat_type9", "stat_type10", "stat_value1", "stat_value2", "stat_value3", "stat_value4", "stat_value5", "stat_value6", "stat_value7", "stat_value8", "stat_value9", "stat_value10", "ScalingStatDistribution", "ScalingStatValue" };

				object[] values = { item.entry, item.iClass, item.iSub, item.name, item.description, item.displayId, item.quality, item.buycount, item.inventory, item.flags, item.extraFlags, item.maxCount, item.containerSlot, item.buyPrice, item.sellPrice,
					item.damageType1, item.damageType2, item.damageMin1, item.damageMin2, item.damageMax1, item.damageMax2, item.delay, item.ammoType, item.rangedMod, item.itemSet, item.bonding, item.block, item.durability, item.sheath, item.reistanceHoly, item.reistanceFrost, item.reistanceFire, item.reistanceShadow, item.reistanceNature, item.reistanceArcane,
					item.socketColor1, item.socketColor2, item.socketColor3, item.socketContent1, item.socketContent2, item.socketContent3, item.socketBonus, item.socketGemProperty, item.spellEntry1, item.spellEntry2, item.spellEntry3, item.spellEntry4, item.spellEntry5, item.spellTrigger1, item.spellTrigger2, item.spellTrigger3, item.spellTrigger4, item.spellTrigger5,
					item.spellCharges1, item.spellCharges2, item.spellCharges3, item.spellCharges4, item.spellCharges5, item.spellPPMRate1, item.spellPPMRate2, item.spellPPMRate3, item.spellPPMRate4, item.spellPPMRate5, item.spellCooldown1, item.spellCooldown2, item.spellCooldown3, item.spellCooldown4, item.spellCooldown5,
					item.spellCategory1, item.spellCategory2, item.spellCategory3, item.spellCategory4, item.spellCategory5, item.spellCategoryCooldown1, item.spellCategoryCooldown2, item.spellCategoryCooldown3, item.spellCategoryCooldown4, item.spellCategoryCooldown5, item.startQuest, item.material, item.property, item.suffix, item.area, item.map,
					item.disenchantId, item.pageText, item.languageId, item.pageMaterial, item.foodType, item.lockId, item.holidayId, item.bagFamily, item.modifier, item.duration, item.limitCategory, item.minMoney, item.maxMoney, item.flagsCustom, item.totemCategory, item.reqRace, item.reqClass, item.reqLevel, item.reqSkill, item.reqSkillRank, item.reqHonorRank, item.reqRepFaction, item.reqRepRank, item.reqDisenchant, item.reqSpell, item.reqCityRank,
					item.reqItemLevel, item.statsCount, item.statsType1, item.statsType2, item.statsType3, item.statsType4, item.statsType5, item.statsType6, item.statsType7, item.statsType8, item.statsType9, item.statsType10, item.statsValue1, item.statsValue2, item.statsValue3, item.statsValue4, item.statsValue5,
					item.statsValue6, item.statsValue7, item.statsValue8, item.statsValue9, item.statsValue10, item.scalingStatDist, item.scalingStatValue };

				return insertOrUpdateToDatabase("item_template", columns, values);
			}

			return null;
		}

		public string itemLPMDToSql(string table, ItemLPMD[] lpmd) {
			/*
			 * if(clps != null) {
				if(clps.Length > 0) {
					string sql = "";

					string[] columns = { "entry", "item", "reference", "chance", "questRequired", "lootMode", "groupId", "minCount", "maxCount" };
					object[] values;

					foreach(var c in clps) {
						values = new object[] { c.entry, c.item, c.reference, c.chance, c.questRequired, c.lootMode, c.groupId, c.minCount, c.maxCount };
						sql += insertToDatabase(table, columns, values);
					}

					return deleteFromDatabase(table, "entry", clps[0].entry) + sql;
				}
			}

			return null;
			 */

			if(lpmd != null) {
				if(lpmd.Length > 0) {
					string sql = "";

					string[] columns = { "entry", "item", "reference", "chance", "questRequired", "lootMode", "groupId", "minCount", "maxCount" };
					object[] values;

					foreach(var v in lpmd) {
						values = new object[] { v.entry, v.item, v.reference, v.chance, v.questRequired, v.lootMode, v.groupId, v.minCount, v.maxCount };
						sql += insertToDatabase(table, columns, values);
					}

					return deleteFromDatabase(table, "entry", lpmd[0].entry) + sql;
				}
			}

			return null;
		}

		// delete

		public string deleteCreature(uint entry) {
			return $"DELETE FROM creature_template WHERE entry = '{entry}';";
		}

		public string deleteQuest(uint id) {
			return $"DELETE FROM quest_template WHERE id = '{id}';";
		}

		public string deleteGameObject(uint entry) {
			return $"DELETE FROM gameobject_template WHERE entry = '{entry}';";
		}

		public string deleteItem(uint entry) {
			return $"DELETE FROM item_template WHERE entry = '{entry}';";
		}
	}
}

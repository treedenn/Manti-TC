using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

using Manti.Classes.AccountTab;
using Manti.Classes.CharacterTab;
using Manti.Classes.CreatureTab;
using Manti.Classes.QuestTab;
using Manti.Classes.GameObjectTab;
using Manti.Classes.ItemTab;
using Manti.Classes.Database;
using Manti.Classes.Settings;
using Manti.Classes.Generate;

namespace Manti.Views {
	public partial class FormMain : Form {
		public FormMain() {
			InitializeComponent();
		}

		// - - - - - - - - - - - MUST READ - - - - - - - - - - -
		//       Welcome to the Source Code of Manti-TC!
		// - - - - - - - - - - - - - - - - - - - - - - - - - - -
		// This project has been revamped and hopefully it is
		// more understandable than it was before.
		// - - - - - - - - - - - - - - - - - - - - - - - - - - -
		// For a better overview in Visual Studio 14,
		// Do the command: CTRL + M + O, simultaneously.
		// - - - - - - - - - - - - - - - - - - - - - - - - - - -
		// The forum thread can be found at Emudevs:
		// http://emudevs.com/showthread.php/5261-Trinity-Manti-Manager
		// The newest Source Code can be found at Github:
		// https://github.com/Heitx/Manti-TC
		// - - - - - - - - - - - - - - - - - - - - - - - - - - -

		private DateTime getDateTimeFromString(string date) {
			DateTime expetedDate;
			if(DateTime.TryParseExact(date, new string[] { "dd/MM/yyyy H:m:s", "MM/dd/yyyy H:m:s" }, null, DateTimeStyles.None, out expetedDate)) {
				return expetedDate;
			}

			return new DateTime();
		}

		private string setDefaultValue(Control control, string defaultValue) {
			if(string.IsNullOrEmpty(control.Text)) {
				return defaultValue;
			}

			return control.Text;
		}

		#region Functions

		#region Fill/add

		// account

		private void fillAccountTemplate(Account account) {
			if(account != null) {
				textBoxAccountAccountID.Text = account.id.ToString();
				textBoxAccountAccountUsername.Text = account.username;
				textBoxAccountAccountEmail.Text = account.email;
				textBoxAccountAccountRegmail.Text = account.reqemail;
				textBoxAccountAccountJoindate.Text = account.reqemail;
				textBoxAccountAccountLastIP.Text = account.lastIP;
				checkBoxAccountAccountLocked.Checked = account.locked;
				checkBoxAccountAccountOnline.Checked = account.online;
				textBoxAccountAccountExpansion.Text = account.expansion.ToString();

				if(account.banned != null) {
					textBoxAccountAccountBandate.Text = account.banned.banDate.ToString();
					textBoxAccountAccountUnbandate.Text = account.banned.unbanDate.ToString();
					textBoxAccountAccountBanreason.Text = account.banned.reason;
					textBoxAccountAccountBannedby.Text = account.banned.by;
					checkBoxAccountAccountBanActive.Checked = account.banned.isActive;

					monthCalendarAccountAccountBanDate.AddMonthlyBoldedDate(account.banned.banDate);
					monthCalendarAccountAccountBanDate.SetDate(account.banned.banDate);
					monthCalendarAccountAccountUnbanDate.AddMonthlyBoldedDate(account.banned.unbanDate);
					monthCalendarAccountAccountUnbanDate.SetDate(account.banned.unbanDate);
				}

				if(account.muted != null) {
					textBoxAccountAccountMutedate.Text = account.muted.muteDate.ToString();
					textBoxAccountAccountMutetime.Text = account.muted.duration.ToString();
					textBoxAccountAccountMutereason.Text = account.muted.reason;
					textBoxAccountAccountMutedby.Text = account.muted.by;

					monthCalendarAccountAccountMuteDate.AddMonthlyBoldedDate(account.muted.muteDate);
					monthCalendarAccountAccountMuteDate.SetDate(account.muted.muteDate);
					monthCalendarAccountAccountUnmuteDate.AddMonthlyBoldedDate(account.muted.getUnmuteDate());
					monthCalendarAccountAccountUnmuteDate.SetDate(account.muted.getUnmuteDate());
				}

				if(account.access != null) {
					dataGridViewAccountAccess.Rows.Clear();

					foreach(var a in account.access) {
						dataGridViewAccountAccess.Rows.Add(a.id, a.gmLevel, a.realmID);
					}
				}

				tabControlCategoryAccount.SelectedTab = tabPageAccountAccount;
			}
		}

		// character

		private void fillCharacterTemplate(Character character) {
			if(character != null) {
				textBoxCharacterCharacterGUID.Text = character.guid.ToString();
				textBoxCharacterCharacterAccount.Text = character.accountID.ToString();
				textBoxCharacterCharacterName.Text = character.name.ToString();
				textBoxCharacterCharacterRace.Text = character.charRace.ToString();
				textBoxCharacterCharacterClass.Text = character.charClass.ToString();
				textBoxCharacterCharacterGender.Text = character.sex.ToString();
				textBoxCharacterCharacterLevel.Text = character.level.ToString();
				textBoxCharacterCharacterMoney.Text = character.money.ToString();
				textBoxCharacterCharacterXP.Text = character.xp.ToString();
				textBoxCharacterCharacterTitle.Text = character.chosenTitle.ToString();
				checkBoxCharacterCharacterOnline.Checked = character.isOnline;
				checkBoxCharacterCharacterCinematic.Checked = character.isCinematic;
				checkBoxCharacterCharacterRest.Checked = character.isResting;
				// Location
				textBoxCharacterCharacterMapID.Text = character.mapID.ToString();
				textBoxCharacterCharacterInstanceID.Text = character.instanceID.ToString();
				textBoxCharacterCharacterZoneID.Text = character.zoneID.ToString();
				textBoxCharacterCharacterCoordO.Text = character.orientation.ToString();
				textBoxCharacterCharacterCoordX.Text = character.xPosition.ToString();
				textBoxCharacterCharacterCoordY.Text = character.yPosition.ToString();
				textBoxCharacterCharacterCoordZ.Text = character.zPosition.ToString();
				// Player vs Player
				textBoxCharacterCharacterHonorPoints.Text = character.honorPoints.ToString();
				textBoxCharacterCharacterArenaPoints.Text = character.arenaPoints.ToString();
				textBoxCharacterCharacterTotalKills.Text = character.totalKills.ToString();
				// Stats
				textBoxCharacterCharacterHealth.Text = character.health.ToString();
				textBoxCharacterCharacterPower1.Text = character.power1.ToString();
				textBoxCharacterCharacterPower2.Text = character.power2.ToString();
				textBoxCharacterCharacterPower3.Text = character.power3.ToString();
				textBoxCharacterCharacterPower4.Text = character.power4.ToString();
				textBoxCharacterCharacterPower5.Text = character.power5.ToString();
				textBoxCharacterCharacterPower6.Text = character.power6.ToString();
				textBoxCharacterCharacterPower7.Text = character.power7.ToString();

				tabControlCategoryCharacter.SelectedTab = tabPageCharacterCharacter;
			}
		}
		private void addCharacterInventory(CharacterInventory[] inventory) {
			if(inventory != null) {

				dataGridViewCharacterInventory.Rows.Clear();

				foreach(var i in inventory) {
					dataGridViewCharacterInventory.Rows.Add(i.guid, i.bag, i.slot, i.item, i.name);
				}
			}
		}

		// creature

		private void fillCreatureTemplate(Creature creature) {
			if(creature != null) {
				textBoxCreatureTemplateEntry.Text = creature.entry.ToString();
				textBoxCreatureTemplateDifEntry1.Text = creature.diffEntry1.ToString();
				textBoxCreatureTemplateDifEntry2.Text = creature.diffEntry2.ToString();
				textBoxCreatureTemplateDifEntry3.Text = creature.diffEntry3.ToString();
				textBoxCreatureTemplateName.Text = creature.name;
				textBoxCreatureTemplateSubname.Text = creature.subname;
				textBoxCreatureTemplateModelID1.Text = creature.modelId1.ToString();
				textBoxCreatureTemplateModelID2.Text = creature.modelId2.ToString();
				textBoxCreatureTemplateModelID3.Text = creature.modelId3.ToString();
				textBoxCreatureTemplateModelID4.Text = creature.modelId4.ToString();
				textBoxCreatureTemplateLevelMin.Text = creature.minlevel.ToString();
				textBoxCreatureTemplateLevelMax.Text = creature.maxlevel.ToString();
				textBoxCreatureTemplateGoldMin.Text = creature.mingold.ToString();
				textBoxCreatureTemplateGoldMax.Text = creature.maxgold.ToString();
				textBoxCreatureTemplateKillCredit1.Text = creature.killCredit1.ToString();
				textBoxCreatureTemplateKillCredit2.Text = creature.killCredit2.ToString();
				textBoxCreatureTemplateRank.Text = creature.rank.ToString();
				textBoxCreatureTemplateScale.Text = creature.scale.ToString();
				textBoxCreatureTemplateFaction.Text = creature.faction.ToString();
				textBoxCreatureTemplateNPCFlags.Text = creature.npcFlags.ToString();
				textBoxCreatureTemplateSpell1.Text = creature.spell1.ToString();
				textBoxCreatureTemplateSpell2.Text = creature.spell2.ToString();
				textBoxCreatureTemplateSpell3.Text = creature.spell3.ToString();
				textBoxCreatureTemplateSpell4.Text = creature.spell4.ToString();
				textBoxCreatureTemplateSpell5.Text = creature.spell5.ToString();
				textBoxCreatureTemplateSpell6.Text = creature.spell6.ToString();
				textBoxCreatureTemplateSpell7.Text = creature.spell7.ToString();
				textBoxCreatureTemplateSpell8.Text = creature.spell8.ToString();
				textBoxCreatureTemplateModHealth.Text = creature.modHealth.ToString();
				textBoxCreatureTemplateModMana.Text = creature.modMana.ToString();
				textBoxCreatureTemplateModArmor.Text = creature.modArmor.ToString();
				textBoxCreatureTemplateModDamage.Text = creature.modDamage.ToString();
				textBoxCreatureTemplateModExperience.Text = creature.modExp.ToString();
				textBoxCreatureTemplateSpeedWalk.Text = creature.speedWalk.ToString();
				textBoxCreatureTemplateSpeedRun.Text = creature.speedRun.ToString();
				textBoxCreatureTemplateBaseAttack.Text = creature.baseAttackTime.ToString();
				textBoxCreatureTemplateRangedAttack.Text = creature.rangedAttackTime.ToString();
				textBoxCreatureTemplateBV.Text = creature.bVariance.ToString();
				textBoxCreatureTemplateRV.Text = creature.rVariance.ToString();
				textBoxCreatureTemplateDS.Text = creature.dSchool.ToString();
				textBoxCreatureTemplateAIName.Text = creature.aiName.ToString();
				textBoxCreatureTemplateMType.Text = creature.movementType.ToString();
				textBoxCreatureTemplateInhabitType.Text = creature.inhabitType.ToString();
				textBoxCreatureTemplateHH.Text = creature.hoverHeight.ToString();
				textBoxCreatureTemplateGMID.Text = creature.gossipMenuId.ToString();
				textBoxCreatureTemplateMID.Text = creature.movementId.ToString();
				textBoxCreatureTemplateScriptName.Text = creature.scriptName.ToString();
				textBoxCreatureTemplateVID.Text = creature.vehicleId.ToString();
				textBoxCreatureTemplateMechanic.Text = creature.mechanicImmuneMask.ToString();
				textBoxCreatureTemplateFamily.Text = creature.family.ToString();
				textBoxCreatureTemplateType.Text = creature.familyType.ToString();
				textBoxCreatureTemplateTypeFlags.Text = creature.typeFlags.ToString();
				textBoxCreatureTemplateFlagsExtra.Text = creature.extraFlags.ToString();
				textBoxCreatureTemplateUnitClass.Text = creature.unitClass.ToString();
				textBoxCreatureTemplateUnitflags.Text = creature.unitFlags1.ToString();
				textBoxCreatureTemplateUnitflags2.Text = creature.unitFlags2.ToString();
				textBoxCreatureTemplateDynamic.Text = creature.dynamicFlags.ToString();
				checkBoxCreatureTemplateHR.Checked = creature.isRegenHealth;
				textBoxCreatureTemplateResis1.Text = creature.resistHoly.ToString();
				textBoxCreatureTemplateResis2.Text = creature.resistFire.ToString();
				textBoxCreatureTemplateResis3.Text = creature.resistNature.ToString();
				textBoxCreatureTemplateResis4.Text = creature.resistFrost.ToString();
				textBoxCreatureTemplateResis5.Text = creature.resistShadow.ToString();
				textBoxCreatureTemplateResis6.Text = creature.resistArcane.ToString();
				textBoxCreatureTemplateTType.Text = creature.trainerType.ToString();
				textBoxCreatureTemplateTSpell.Text = creature.trainerSpell.ToString();
				textBoxCreatureTemplateTRace.Text = creature.trainerClass.ToString();
				textBoxCreatureTemplateTClass.Text = creature.trainerRace.ToString();
				textBoxCreatureTemplateLootID.Text = creature.lootId.ToString();
				textBoxCreatureTemplatePickID.Text = creature.pickpocketId.ToString();
				textBoxCreatureTemplateSkinID.Text = creature.skinId.ToString();

				tabControlCategoryCreature.SelectedTab = tabPageCreatureTemplate;
			}
		}
		private void addCreatureLocationRows(CreatureLocation[] location) {
			if(location != null) {
				dataGridViewCreatureLocation.Rows.Clear();

				foreach(var v in location) {
					dataGridViewCreatureLocation.Rows.Add(v.id, v.guid, v.map, v.zoneId, v.areaId, v.xPosition, v.yPosition, v.zPosition, v.orientation, v.spawntimeSecs, v.spawnDist);
				}
			}
		}
		private void addCreatureVendorRows(CreatureVendor[] vendor) {
			if(vendor != null) {
				dataGridViewCreatureVendor.Rows.Clear();

				foreach(var v in vendor) {
					dataGridViewCreatureVendor.Rows.Add(v.entry, v.slot, v.item, v.maxCount, v.increaseTime, v.extendedCost, v.name);
				}
			}
		}
		private void addCreatureLootRows(CreatureLPS[] loot) {
			if(loot != null) {
				dataGridViewCreatureLoot.Rows.Clear();

				foreach(var v in loot) {
					dataGridViewCreatureLoot.Rows.Add(v.entry, v.item, v.reference, v.chance, v.questRequired, v.lootMode, v.groupId, v.minCount, v.maxCount, v.name);
				}
			}
		}
		private void addCreaturePickpocketRows(CreatureLPS[] pick) {
			if(pick != null) {
				dataGridViewCreaturePickpocketLoot.Rows.Clear();

				foreach(var v in pick) {
					dataGridViewCreaturePickpocketLoot.Rows.Add(v.entry, v.item, v.reference, v.chance, v.questRequired, v.lootMode, v.groupId, v.minCount, v.maxCount, v.name);
				}
			}
		}
		private void addCreatureSkinRows(CreatureLPS[] skin) {
			if(skin != null) {
				dataGridViewCreatureSkinLoot.Rows.Clear();

				foreach(var v in skin) {
					dataGridViewCreatureSkinLoot.Rows.Add(v.entry, v.item, v.reference, v.chance, v.questRequired, v.lootMode, v.groupId, v.minCount, v.maxCount, v.name);
				}
			}
		}

		// quest

		private void fillQuestSections(Quest quest) {
			if(quest != null) {
				textBoxQuestSectionID.Text                = quest.id.ToString();
				textBoxQuestSectionTitle.Text             = quest.title.ToString();
				textBoxQuestSectionLDescription.Text      = quest.logDescription.ToString();
				textBoxQuestSectionQDescription.Text      = quest.questDescription.ToString();
				textBoxQuestSectionAreaDescription.Text   = quest.area.ToString();
				textBoxQuestSectionCompleted.Text         = quest.completed.ToString();
				textBoxQuestSectionObjectives1.Text       = quest.objective1.ToString();
				textBoxQuestSectionObjectives2.Text       = quest.objective2.ToString();
				textBoxQuestSectionObjectives3.Text       = quest.objective3.ToString();
				textBoxQuestSectionObjectives4.Text       = quest.objective4.ToString();
				textBoxQuestSectionReqPK.Text             = quest.requirePlayerKills.ToString();
				textBoxQuestSectionTimeAllowed.Text       = quest.timeAllowed.ToString();
				textBoxQuestSectionQuestInfo.Text         = quest.questInfo.ToString();
				textBoxQuestSectionQuestLevel.Text        = quest.questLevel.ToString();
				textBoxQuestSectionOtherSP.Text           = quest.suggestedPlayers.ToString();
				textBoxQuestSectionExclusive.Text         = quest.exclusiveGroup.ToString();
				textBoxQuestSectionPrevQuest.Text         = quest.prevQuest.ToString();
				textBoxQuestSectionNextQuest.Text         = quest.nextQuest.ToString();
				textBoxQuestSectionReqRace.Text           = quest.races.ToString();
				textBoxQuestSectionReqClass.Text          = quest.classes.ToString();
				textBoxQuestSectionReqLevelMin.Text       = quest.minLevel.ToString();
				textBoxQuestSectionReqLevelMax.Text       = quest.maxLevel.ToString();
				textBoxQuestSectionReqFaction1.Text       = quest.reqFaction1.ToString();
				textBoxQuestSectionReqFaction2.Text       = quest.reqFaction2.ToString();
				textBoxQuestSectionReqValue1.Text         = quest.reqValue1.ToString();
				textBoxQuestSectionReqValue2.Text         = quest.reqValue2.ToString();
				textBoxQuestSectionReqMinRepF.Text        = quest.minRepFaction.ToString();
				textBoxQuestSectionReqMaxRepF.Text        = quest.maxRepFaction.ToString();
				textBoxQuestSectionReqMinRepV.Text        = quest.minRepValue.ToString();
				textBoxQuestSectionReqMaxRepV.Text        = quest.maxRepValue.ToString();
				textBoxQuestSectionReqQSort.Text          = quest.zoneIdOrQuestSort.ToString();
				textBoxQuestSectionReqSkillID.Text        = quest.skillId.ToString();
				textBoxQuestSectionReqSkillPoints.Text    = quest.skillPoints.ToString();
				textBoxQuestSectionQuestType.Text         = quest.type.ToString();
				textBoxQuestSectionQuestFlags.Text        = quest.flags.ToString();
				textBoxQuestSectionOtherSF.Text           = quest.specialFlags.ToString();
				textBoxQuestSectionSourceItemID.Text      = quest.sourceItemId.ToString();
				textBoxQuestSectionSourceItemCount.Text   = quest.sourceItemCount.ToString();
				textBoxQuestSectionSourceSpellID.Text     = quest.sourceSpellId.ToString();
				textBoxQuestSectionReqNPCID1.Text         = quest.requiredNpcOrGoId1.ToString();
				textBoxQuestSectionReqNPCID2.Text         = quest.requiredNpcOrGoId2.ToString();
				textBoxQuestSectionReqNPCID3.Text         = quest.requiredNpcOrGoId3.ToString();
				textBoxQuestSectionReqNPCID4.Text         = quest.requiredNpcOrGoId4.ToString();
				textBoxQuestSectionReqNPCC1.Text          = quest.requiredNpcCount1.ToString();
				textBoxQuestSectionReqNPCC2.Text          = quest.requiredNpcCount2.ToString();
				textBoxQuestSectionReqNPCC3.Text          = quest.requiredNpcCount3.ToString();
				textBoxQuestSectionReqNPCC4.Text          = quest.requiredNpcCount4.ToString();
				textBoxQuestSectionReqItemID1.Text        = quest.requiredItemId1.ToString();
				textBoxQuestSectionReqItemID2.Text        = quest.requiredItemId2.ToString();
				textBoxQuestSectionReqItemID3.Text        = quest.requiredItemId3.ToString();
				textBoxQuestSectionReqItemID4.Text        = quest.requiredItemId4.ToString();
				textBoxQuestSectionReqItemID5.Text        = quest.requiredItemId5.ToString();
				textBoxQuestSectionReqItemID6.Text        = quest.requiredItemId6.ToString();
				textBoxQuestSectionReqItemC1.Text         = quest.requiredItemCount1.ToString();
				textBoxQuestSectionReqItemC2.Text         = quest.requiredItemCount2.ToString();
				textBoxQuestSectionReqItemC3.Text         = quest.requiredItemCount3.ToString();
				textBoxQuestSectionReqItemC4.Text         = quest.requiredItemCount4.ToString();
				textBoxQuestSectionReqItemC5.Text         = quest.requiredItemCount5.ToString();
				textBoxQuestSectionReqItemC6.Text         = quest.requiredItemCount6.ToString();
				textBoxQuestSectionRewChoiceID1.Text      = quest.rewardChoiceItemId1.ToString();
				textBoxQuestSectionRewChoiceID2.Text      = quest.rewardChoiceItemId2.ToString();
				textBoxQuestSectionRewChoiceID3.Text      = quest.rewardChoiceItemId3.ToString();
				textBoxQuestSectionRewChoiceID4.Text      = quest.rewardChoiceItemId4.ToString();
				textBoxQuestSectionRewChoiceID5.Text      = quest.rewardChoiceItemId5.ToString();
				textBoxQuestSectionRewChoiceID6.Text      = quest.rewardChoiceItemId6.ToString();
				textBoxQuestSectionRewChoiceC1.Text       = quest.rewardChoiceItemCount1.ToString();
				textBoxQuestSectionRewChoiceC2.Text       = quest.rewardChoiceItemCount2.ToString();
				textBoxQuestSectionRewChoiceC3.Text       = quest.rewardChoiceItemCount3.ToString();
				textBoxQuestSectionRewChoiceC4.Text       = quest.rewardChoiceItemCount4.ToString();
				textBoxQuestSectionRewChoiceC5.Text       = quest.rewardChoiceItemCount5.ToString();
				textBoxQuestSectionRewChoiceC6.Text       = quest.rewardChoiceItemCount6.ToString();
				textBoxQuestSectionRewItemID1.Text        = quest.rewardItemId1.ToString();
				textBoxQuestSectionRewItemID2.Text        = quest.rewardItemId2.ToString();
				textBoxQuestSectionRewItemID3.Text        = quest.rewardItemId3.ToString();
				textBoxQuestSectionRewItemID4.Text        = quest.rewardItemId4.ToString();
				textBoxQuestSectionRewItemC1.Text         = quest.rewardItemCount1.ToString();
				textBoxQuestSectionRewItemC2.Text         = quest.rewardItemCount2.ToString();
				textBoxQuestSectionRewItemC3.Text         = quest.rewardItemCount3.ToString();
				textBoxQuestSectionRewItemC4.Text         = quest.rewardItemCount4.ToString();
				textBoxQuestSectionRewFactionID1.Text     = quest.rewardFactionId1.ToString();
				textBoxQuestSectionRewFactionID2.Text     = quest.rewardFactionId2.ToString();
				textBoxQuestSectionRewFactionID3.Text     = quest.rewardFactionId3.ToString();
				textBoxQuestSectionRewFactionID4.Text     = quest.rewardFactionId4.ToString();
				textBoxQuestSectionRewFactionID5.Text     = quest.rewardFactionId5.ToString();
				textBoxQuestSectionRewFactionV1.Text      = quest.rewardFactionValue1.ToString();
				textBoxQuestSectionRewFactionV2.Text      = quest.rewardFactionValue2.ToString();
				textBoxQuestSectionRewFactionV3.Text      = quest.rewardFactionValue3.ToString();
				textBoxQuestSectionRewFactionV4.Text      = quest.rewardFactionValue4.ToString();
				textBoxQuestSectionRewFactionV5.Text      = quest.rewardFactionValue5.ToString();
				textBoxQuestSectionRewOtherMoney.Text     = quest.rewardOrRequiredMoney.ToString();
				textBoxQuestSectionRewOtherMoneyML.Text   = quest.rewardOrRequiredMoneyML.ToString();
				textBoxQuestSectionRewOtherAP.Text        = quest.rewardOrRequiredArenaPoints.ToString();
				textBoxQuestSectionRewOtherHP.Text        = quest.rewardOrRequiredHonorPoints.ToString();
				textBoxQuestSectionRewOtherHM.Text        = quest.rewardOrRequiredHonorMultiplier.ToString();
				textBoxQuestSectionRewOtherTitleID.Text   = quest.rewardTitleId.ToString();
				textBoxQuestSectionRewOtherTP.Text        = quest.rewardTalent.ToString();
				textBoxQuestSectionRewOtherMailID.Text    = quest.mailTemplateId.ToString();
				textBoxQuestSectionRewOtherMailDelay.Text = quest.mailDelay.ToString();
				textBoxQuestSectionRewSpellDisplay.Text   = quest.rewardDisplaySpell.ToString();
				textBoxQuestSectionRewSpell.Text          = quest.rewardSpell.ToString();

				tabControlCategoryQuest.SelectedTab = tabPageQuestSection1;
			}
		}
		private void addQuestGiverRows(QuestGT[] gt) {
			if(gt != null) {
				dataGridViewQuestGivers.Rows.Clear();

				foreach(var v in gt) {
					dataGridViewQuestGivers.Rows.Add(v.entry, v.name, v.subname);
				}
			}
		}
		private void addQuestTakerRows(QuestGT[] gt) {
			if(gt != null) {
				dataGridViewQuestTakers.Rows.Clear();

				foreach(var v in gt) {
					dataGridViewQuestTakers.Rows.Add(v.entry, v.name, v.subname);
				}
			}
		}

		// game object

		private void fillGameObjectTemplate(GameObject go) {
			if(go != null) {
				textBoxGameObjectTempEntry.Text = go.entry.ToString();
				textBoxGameObjectTempType.Text = go.type.ToString();
				textBoxGameObjectTempDID.Text = go.displayId.ToString();
				textBoxGameObjectTempName.Text = go.name;
				textBoxGameObjectTempFaction.Text = go.faction.ToString();
				textBoxGameObjectTempFlags.Text = go.flags.ToString();
				textBoxGameObjectTempSize.Text = go.size.ToString();
				textBoxGameObjectTempD0.Text = go.data0.ToString();
				textBoxGameObjectTempD1.Text = go.data1.ToString();
				textBoxGameObjectTempD2.Text = go.data2.ToString();
				textBoxGameObjectTempD3.Text = go.data3.ToString();
				textBoxGameObjectTempD4.Text = go.data4.ToString();
				textBoxGameObjectTempD5.Text = go.data5.ToString();
				textBoxGameObjectTempD6.Text = go.data6.ToString();
				textBoxGameObjectTempD7.Text = go.data7.ToString();
				textBoxGameObjectTempD8.Text = go.data8.ToString();
				textBoxGameObjectTempD9.Text = go.data9.ToString();
				textBoxGameObjectTempD10.Text = go.data10.ToString();
				textBoxGameObjectTempD11.Text = go.data11.ToString();
				textBoxGameObjectTempD12.Text = go.data12.ToString();
				textBoxGameObjectTempD13.Text = go.data13.ToString();
				textBoxGameObjectTempD14.Text = go.data14.ToString();
				textBoxGameObjectTempD15.Text = go.data15.ToString();
				textBoxGameObjectTempD16.Text = go.data16.ToString();
				textBoxGameObjectTempD17.Text = go.data17.ToString();
				textBoxGameObjectTempD18.Text = go.data18.ToString();
				textBoxGameObjectTempD19.Text = go.data19.ToString();
				textBoxGameObjectTempD20.Text = go.data20.ToString();
				textBoxGameObjectTempD21.Text = go.data21.ToString();
				textBoxGameObjectTempD22.Text = go.data22.ToString();
				textBoxGameObjectTempD23.Text = go.data23.ToString();

				tabControlCategoryGameObject.SelectedTab = tabPageGameObjectTemplate;
			}
		}

		//item

		private void fillItemTemplate(Item item) {
			if(item != null) {
				textBoxItemTempEntry.Text           = item.entry.ToString();
				textBoxItemTempTypeClass.Text       = item.iClass.ToString();
				textBoxItemTempSubclass.Text        = item.iSub.ToString();
				textBoxItemTempName.Text            = item.name.ToString();
				textBoxItemTempDescription.Text     = item.description.ToString();
				textBoxItemTempDisplayID.Text       = item.displayId.ToString();
				textBoxItemTempQuality.Text         = item.quality.ToString();
				textBoxItemTempBuyC.Text            = item.buycount.ToString();
				textBoxItemTempInventory.Text       = item.inventory.ToString();
				textBoxItemTempFlags.Text           = item.flags.ToString();
				textBoxItemTempEFlags.Text          = item.extraFlags.ToString();
				textBoxItemTempMaxC.Text            = item.maxCount.ToString();
				textBoxItemTempContainer.Text       = item.containerSlot.ToString();
				textBoxItemTempBuyP.Text            = item.buyPrice.ToString();
				textBoxItemTempSellP.Text           = item.sellPrice.ToString();
				textBoxItemTempDmgType1.Text        = item.damageType1.ToString();
				textBoxItemTempDmgType2.Text        = item.damageType2.ToString();
				textBoxItemTempDmgMin1.Text         = item.damageMin1.ToString();
				textBoxItemTempDmgMin2.Text         = item.damageMin2.ToString();
				textBoxItemTempDmgMax1.Text         = item.damageMax1.ToString();
				textBoxItemTempDmgMax2.Text         = item.damageMax2.ToString();
				textBoxItemTempDelay.Text           = item.delay.ToString();
				textBoxItemTempAmmoType.Text        = item.ammoType.ToString();
				textBoxItemTempRangedMod.Text       = item.rangedMod.ToString();
				textBoxItemTempItemSet.Text         = item.itemSet.ToString();
				textBoxItemTempBonding.Text         = item.bonding.ToString();
				textBoxItemTempBlock.Text           = item.block.ToString();
				textBoxItemTempDurability.Text      = item.durability.ToString();
				textBoxItemTempSheath.Text          = item.sheath.ToString();
				textBoxItemTempResisHoly.Text       = item.reistanceHoly.ToString();
				textBoxItemTempResisFrost.Text      = item.reistanceFrost.ToString();
				textBoxItemTempResisFire.Text       = item.reistanceFire.ToString();
				textBoxItemTempResisShadow.Text     = item.reistanceShadow.ToString();
				textBoxItemTempResisNature.Text     = item.reistanceNature.ToString();
				textBoxItemTempResisArcane.Text     = item.reistanceArcane.ToString();
				textBoxItemTempColor1.Text          = item.socketColor1.ToString();
				textBoxItemTempColor2.Text          = item.socketColor2.ToString();
				textBoxItemTempColor3.Text          = item.socketColor3.ToString();
				textBoxItemTempContent1.Text        = item.socketContent1.ToString();
				textBoxItemTempContent2.Text        = item.socketContent2.ToString();
				textBoxItemTempContent3.Text        = item.socketContent3.ToString();
				textBoxItemTempSocketBonus.Text     = item.socketBonus.ToString();
				textBoxItemTempGemProper.Text       = item.socketGemProperty.ToString();
				textBoxItemTempSpellID1.Text        = item.spellEntry1.ToString();
				textBoxItemTempSpellID2.Text        = item.spellEntry2.ToString();
				textBoxItemTempSpellID3.Text        = item.spellEntry3.ToString();
				textBoxItemTempSpellID4.Text        = item.spellEntry4.ToString();
				textBoxItemTempSpellID5.Text        = item.spellEntry5.ToString();
				textBoxItemTempTrigger1.Text        = item.spellTrigger1.ToString();
				textBoxItemTempTrigger2.Text        = item.spellTrigger2.ToString();
				textBoxItemTempTrigger3.Text        = item.spellTrigger3.ToString();
				textBoxItemTempTrigger4.Text        = item.spellTrigger4.ToString();
				textBoxItemTempTrigger5.Text        = item.spellTrigger5.ToString();
				textBoxItemTempCharges1.Text        = item.spellCharges1.ToString();
				textBoxItemTempCharges2.Text        = item.spellCharges2.ToString();
				textBoxItemTempCharges3.Text        = item.spellCharges3.ToString();
				textBoxItemTempCharges4.Text        = item.spellCharges4.ToString();
				textBoxItemTempCharges5.Text        = item.spellCharges5.ToString();
				textBoxItemTempRate1.Text           = item.spellPPMRate1.ToString();
				textBoxItemTempRate2.Text           = item.spellPPMRate2.ToString();
				textBoxItemTempRate3.Text           = item.spellPPMRate3.ToString();
				textBoxItemTempRate4.Text           = item.spellPPMRate4.ToString();
				textBoxItemTempRate5.Text           = item.spellPPMRate5.ToString();
				textBoxItemTempCD1.Text             = item.spellCooldown1.ToString();
				textBoxItemTempCD2.Text             = item.spellCooldown2.ToString();
				textBoxItemTempCD3.Text             = item.spellCooldown3.ToString();
				textBoxItemTempCD4.Text             = item.spellCooldown4.ToString();
				textBoxItemTempCD5.Text             = item.spellCooldown5.ToString();
				textBoxItemTempCategory1.Text       = item.spellCategory1.ToString();
				textBoxItemTempCategory2.Text       = item.spellCategory2.ToString();
				textBoxItemTempCategory3.Text       = item.spellCategory3.ToString();
				textBoxItemTempCategory4.Text       = item.spellCategory4.ToString();
				textBoxItemTempCategory5.Text       = item.spellCategory5.ToString();
				textBoxItemTempCategoryCD1.Text     = item.spellCategoryCooldown1.ToString();
				textBoxItemTempCategoryCD2.Text     = item.spellCategoryCooldown2.ToString();
				textBoxItemTempCategoryCD3.Text     = item.spellCategoryCooldown3.ToString();
				textBoxItemTempCategoryCD4.Text     = item.spellCategoryCooldown4.ToString();
				textBoxItemTempCategoryCD5.Text     = item.spellCategoryCooldown5.ToString();
				textBoxItemTempStartQuest.Text      = item.startQuest.ToString();
				textBoxItemTempMaterial.Text        = item.material.ToString();
				textBoxItemTempProperty.Text        = item.property.ToString();
				textBoxItemTempSuffix.Text          = item.suffix.ToString();
				textBoxItemTempArea.Text            = item.area.ToString();
				textBoxItemTempMap.Text             = item.map.ToString();
				textBoxItemTempDisenchantID.Text    = item.disenchantId.ToString();
				textBoxItemTempPageText.Text        = item.pageText.ToString();
				textBoxItemTempLanguage.Text        = item.languageId.ToString();
				textBoxItemTempPageMaterial.Text    = item.pageMaterial.ToString();
				textBoxItemTempFoodType.Text        = item.foodType.ToString();
				textBoxItemTempLockID.Text          = item.lockId.ToString();
				textBoxItemTempHolidayID.Text       = item.holidayId.ToString();
				textBoxItemTempBagFamily.Text       = item.bagFamily.ToString();
				textBoxItemTempModifier.Text        = item.modifier.ToString();
				textBoxItemTempDuration.Text        = item.duration.ToString();
				textBoxItemTempLimitCate.Text       = item.limitCategory.ToString();
				textBoxItemTempMoneyMin.Text        = item.minMoney.ToString();
				textBoxItemTempMoneyMax.Text        = item.maxMoney.ToString();
				textBoxItemTempFlagsC.Text          = item.flagsCustom.ToString();
				textBoxItemTempTotemCategory.Text   = item.totemCategory.ToString();
				textBoxItemTempReqRace.Text         = item.reqRace.ToString();
				textBoxItemTempReqClass.Text        = item.reqClass.ToString();
				textBoxItemTempReqLevel.Text        = item.reqLevel.ToString();
				textBoxItemTempReqSkill.Text        = item.reqSkill.ToString();
				textBoxItemTempReqSkillRank.Text    = item.reqSkillRank.ToString();
				textBoxItemTempReqHonorRank.Text    = item.reqHonorRank.ToString();
				textBoxItemTempReqRepFaction.Text   = item.reqRepFaction.ToString();
				textBoxItemTempReqRepRank.Text      = item.reqRepRank.ToString();
				textBoxItemTempReqDisenchant.Text   = item.reqDisenchant.ToString();
				textBoxItemTempReqSpell.Text        = item.reqSpell.ToString();
				textBoxItemTempReqCityRank.Text     = item.reqCityRank.ToString();
				textBoxItemTempReqItemLevel.Text    = item.reqItemLevel.ToString();
				textBoxItemTempStatsC.Text          = item.statsCount.ToString();
				textBoxItemTempStatsType1.Text      = item.statsType1.ToString();
				textBoxItemTempStatsType2.Text      = item.statsType2.ToString();
				textBoxItemTempStatsType3.Text      = item.statsType3.ToString();
				textBoxItemTempStatsType4.Text      = item.statsType4.ToString();
				textBoxItemTempStatsType5.Text      = item.statsType5.ToString();
				textBoxItemTempStatsType6.Text      = item.statsType6.ToString();
				textBoxItemTempStatsType7.Text      = item.statsType7.ToString();
				textBoxItemTempStatsType8.Text      = item.statsType8.ToString();
				textBoxItemTempStatsType9.Text      = item.statsType9.ToString();
				textBoxItemTempStatsType10.Text     = item.statsType10.ToString();
				textBoxItemTempStatsValue1.Text     = item.statsValue1.ToString();
				textBoxItemTempStatsValue2.Text     = item.statsValue2.ToString();
				textBoxItemTempStatsValue3.Text     = item.statsValue3.ToString();
				textBoxItemTempStatsValue4.Text     = item.statsValue4.ToString();
				textBoxItemTempStatsValue5.Text     = item.statsValue5.ToString();
				textBoxItemTempStatsValue6.Text     = item.statsValue6.ToString();
				textBoxItemTempStatsValue7.Text     = item.statsValue7.ToString();
				textBoxItemTempStatsValue8.Text     = item.statsValue8.ToString();
				textBoxItemTempStatsValue9.Text     = item.statsValue9.ToString();
				textBoxItemTempStatsValue10.Text    = item.statsValue10.ToString();
				textBoxItemTempStatsScaleDist.Text  = item.scalingStatDist.ToString();
				textBoxItemTempStatsScaleValue.Text = item.scalingStatValue.ToString();

				tabControlCategoryItem.SelectedTab = tabPageItemTemplate;
			}
		}
		private void addItemLootRows(ItemLPMD[] loot) {
			if(loot != null) {
				dataGridViewItemLoot.Rows.Clear();

				foreach(var v in loot) {
					dataGridViewItemLoot.Rows.Add(v.entry, v.item, v.reference, v.chance, v.questRequired, v.lootMode, v.groupId, v.minCount, v.maxCount, v.name);
				}
			}
		}
		private void addItemProspectRows(ItemLPMD[] prospect) {
			if(prospect != null) {
				dataGridViewItemProspect.Rows.Clear();

				foreach(var v in prospect) {
					dataGridViewItemProspect.Rows.Add(v.entry, v.item, v.reference, v.chance, v.questRequired, v.lootMode, v.groupId, v.minCount, v.maxCount, v.name);
				}
			}
		}
		private void addItemMillingRows(ItemLPMD[] milling) {
			if(milling != null) {
				dataGridViewItemMill.Rows.Clear();

				foreach(var v in milling) {
					dataGridViewItemMill.Rows.Add(v.entry, v.item, v.reference, v.chance, v.questRequired, v.lootMode, v.groupId, v.minCount, v.maxCount, v.name);
				}
			}
		}
		private void addItemDisenchantRows(ItemLPMD[] disenchant) {
			if(disenchant != null) {
				dataGridViewItemDE.Rows.Clear();

				foreach(var v in disenchant) {
					dataGridViewItemDE.Rows.Add(v.entry, v.item, v.reference, v.chance, v.questRequired, v.lootMode, v.groupId, v.minCount, v.maxCount, v.name);
				}
			}
		}

		#endregion

		#region Update

		private Account updateAccount() {
			Account account = new Account();

			if(!string.IsNullOrEmpty(textBoxAccountAccountID.Text)) {
				account.id = Convert.ToUInt32(textBoxAccountAccountID.Text);
				account.username = textBoxAccountAccountUsername.Text;
				account.email = textBoxAccountAccountEmail.Text;
				account.reqemail = textBoxAccountAccountRegmail.Text;
				account.reqemail = textBoxAccountAccountJoindate.Text;
				account.lastIP = textBoxAccountAccountLastIP.Text;
				account.locked = checkBoxAccountAccountLocked.Checked;
				account.online = checkBoxAccountAccountOnline.Checked;
				account.expansion = Convert.ToByte(textBoxAccountAccountExpansion.Text);
			}

			if(!string.IsNullOrEmpty(textBoxAccountAccountBandate.Text)) {
				account.banned = new Classes.AccountTab.AccountBanned();

				account.banned.banDate = getDateTimeFromString(textBoxAccountAccountBandate.Text);
				account.banned.unbanDate = getDateTimeFromString(textBoxAccountAccountUnbandate.Text);
				account.banned.reason = textBoxAccountAccountBanreason.Text;
				account.banned.by = textBoxAccountAccountBannedby.Text;
				account.banned.isActive = checkBoxAccountAccountBanActive.Checked;
			}

			if(!string.IsNullOrEmpty(textBoxAccountAccountMutedate.Text)) {
				account.muted = new Classes.AccountTab.AccountMuted();

				account.muted.muteDate = getDateTimeFromString(textBoxAccountAccountMutedate.Text);
				account.muted.duration = Convert.ToDouble(textBoxAccountAccountMutetime.Text);
				account.muted.reason = textBoxAccountAccountMutereason.Text;
				account.muted.by = textBoxAccountAccountMutedby.Text;
			}

			if(dataGridViewAccountAccess.Rows.Count > 0) {
				AccountAccess[] access = new AccountAccess[dataGridViewAccountAccess.Rows.Count];

				for(var i = 0; i < dataGridViewAccountAccess.Rows.Count; i++) {
					access[i] = new AccountAccess();

					access[i].id = Convert.ToUInt32(dataGridViewAccountAccess.Rows[i].Cells[0].Value); // id
					access[i].gmLevel = Convert.ToInt32(dataGridViewAccountAccess.Rows[i].Cells[1].Value); // gmlevel
					access[i].realmID = Convert.ToInt32(dataGridViewAccountAccess.Rows[i].Cells[2].Value); // realmid
				}

				account.access = access;
			}

			return account;
		}

		private Character updateCharacter() { // Model.CharacterTab.Character
			Character character = new Character();

			character.guid = Convert.ToUInt32(textBoxCharacterCharacterGUID.Text);
			character.accountID = Convert.ToUInt32(textBoxCharacterCharacterAccount.Text);
			character.name = textBoxCharacterCharacterName.Text;
			character.charRace = Convert.ToUInt32(textBoxCharacterCharacterRace.Text);
			character.charClass = Convert.ToUInt32(textBoxCharacterCharacterClass.Text);
			character.sex = Convert.ToUInt32(textBoxCharacterCharacterGender.Text);
			character.level = Convert.ToUInt32(textBoxCharacterCharacterLevel.Text);
			character.money = Convert.ToUInt32(textBoxCharacterCharacterMoney.Text);
			character.xp = Convert.ToUInt32(textBoxCharacterCharacterXP.Text);
			character.chosenTitle = Convert.ToUInt32(textBoxCharacterCharacterTitle.Text);
			character.isOnline = checkBoxCharacterCharacterOnline.Checked;
			character.isCinematic = checkBoxCharacterCharacterCinematic.Checked;
			character.isResting = checkBoxCharacterCharacterRest.Checked;
			// Location
			character.mapID = Convert.ToInt32(textBoxCharacterCharacterMapID.Text);
			character.instanceID = Convert.ToInt32(textBoxCharacterCharacterInstanceID.Text);
			character.zoneID = Convert.ToInt32(textBoxCharacterCharacterZoneID.Text);
			character.orientation = Convert.ToDouble(textBoxCharacterCharacterCoordO.Text);
			character.xPosition = Convert.ToDouble(textBoxCharacterCharacterCoordX.Text);
			character.yPosition = Convert.ToDouble(textBoxCharacterCharacterCoordY.Text);
			character.zPosition = Convert.ToDouble(textBoxCharacterCharacterCoordZ.Text);
			// Player vs Player
			character.honorPoints = Convert.ToInt32(textBoxCharacterCharacterHonorPoints.Text);
			character.arenaPoints = Convert.ToInt32(textBoxCharacterCharacterArenaPoints.Text);
			character.totalKills = Convert.ToInt32(textBoxCharacterCharacterTotalKills.Text);
			// Stats
			character.health = Convert.ToUInt64(textBoxCharacterCharacterHealth.Text);
			character.power1 = Convert.ToUInt64(textBoxCharacterCharacterPower1.Text);
			character.power2 = Convert.ToUInt64(textBoxCharacterCharacterPower2.Text);
			character.power3 = Convert.ToUInt64(textBoxCharacterCharacterPower3.Text);
			character.power4 = Convert.ToUInt64(textBoxCharacterCharacterPower4.Text);
			character.power5 = Convert.ToUInt64(textBoxCharacterCharacterPower5.Text);
			character.power6 = Convert.ToUInt64(textBoxCharacterCharacterPower6.Text);
			character.power7 = Convert.ToUInt64(textBoxCharacterCharacterPower7.Text);

			return character;
		}
		private CharacterInventory[] updateCharacterInventory() {
			var ci = new CharacterInventory[dataGridViewCharacterInventory.Rows.Count];

			for(var i = 0; i < dataGridViewCharacterInventory.Rows.Count; i++) {
				ci[i] = new CharacterInventory();

				ci[i].guid = Convert.ToUInt32(dataGridViewCharacterInventory.Rows[0].Cells[0].Value);
				ci[i].bag = Convert.ToUInt32(dataGridViewCharacterInventory.Rows[0].Cells[1].Value);
				ci[i].slot = Convert.ToUInt32(dataGridViewCharacterInventory.Rows[0].Cells[2].Value);
				ci[i].item = Convert.ToUInt32(dataGridViewCharacterInventory.Rows[0].Cells[3].Value);
			}

			return ci;
		}

		private void updateCreature() {
			Creature c = new Creature();

			c.entry              = Convert.ToUInt32(textBoxCreatureTemplateEntry.Text);
			c.diffEntry1         = Convert.ToUInt32(textBoxCreatureTemplateDifEntry1.Text);
			c.diffEntry2         = Convert.ToUInt32(textBoxCreatureTemplateDifEntry2.Text);
			c.diffEntry3         = Convert.ToUInt32(textBoxCreatureTemplateDifEntry3.Text);
			c.name               = textBoxCreatureTemplateName.Text;
			c.subname            = textBoxCreatureTemplateSubname.Text;
			c.modelId1           = Convert.ToUInt32(textBoxCreatureTemplateModelID1.Text);
			c.modelId2           = Convert.ToUInt32(textBoxCreatureTemplateModelID2.Text);
			c.modelId3           = Convert.ToUInt32(textBoxCreatureTemplateModelID3.Text);
			c.modelId4           = Convert.ToUInt32(textBoxCreatureTemplateModelID4.Text);
			c.minlevel           = Convert.ToUInt32(textBoxCreatureTemplateLevelMin.Text);
			c.maxlevel           = Convert.ToUInt32(textBoxCreatureTemplateLevelMax.Text);
			c.mingold            = Convert.ToUInt32(textBoxCreatureTemplateGoldMin.Text);
			c.maxgold            = Convert.ToUInt32(textBoxCreatureTemplateGoldMax.Text);
			c.killCredit1        = Convert.ToInt32(textBoxCreatureTemplateKillCredit1.Text);
			c.killCredit2        = Convert.ToInt32(textBoxCreatureTemplateKillCredit2.Text);
			c.rank               = Convert.ToUInt32(textBoxCreatureTemplateRank.Text);
			c.scale              = Convert.ToSingle(textBoxCreatureTemplateScale.Text);
			c.faction            = Convert.ToInt32(textBoxCreatureTemplateFaction.Text);
			c.npcFlags           = Convert.ToInt32(textBoxCreatureTemplateNPCFlags.Text);
			c.spell1             = Convert.ToUInt32(textBoxCreatureTemplateSpell1.Text);
			c.spell2             = Convert.ToUInt32(textBoxCreatureTemplateSpell2.Text);
			c.spell3             = Convert.ToUInt32(textBoxCreatureTemplateSpell3.Text);
			c.spell4             = Convert.ToUInt32(textBoxCreatureTemplateSpell4.Text);
			c.spell5             = Convert.ToUInt32(textBoxCreatureTemplateSpell5.Text);
			c.spell6             = Convert.ToUInt32(textBoxCreatureTemplateSpell6.Text);
			c.spell7             = Convert.ToUInt32(textBoxCreatureTemplateSpell7.Text);
			c.spell8             = Convert.ToUInt32(textBoxCreatureTemplateSpell8.Text);
			c.modHealth          = Convert.ToSingle(textBoxCreatureTemplateModHealth.Text);
			c.modMana            = Convert.ToSingle(textBoxCreatureTemplateModMana.Text);
			c.modArmor           = Convert.ToSingle(textBoxCreatureTemplateModArmor.Text);
			c.modDamage          = Convert.ToSingle(textBoxCreatureTemplateModDamage.Text);
			c.modExp             = Convert.ToSingle(textBoxCreatureTemplateModExperience.Text);
			c.speedWalk          = Convert.ToSingle(textBoxCreatureTemplateSpeedWalk.Text);
			c.speedRun           = Convert.ToSingle(textBoxCreatureTemplateSpeedRun.Text);
			c.baseAttackTime     = Convert.ToSingle(textBoxCreatureTemplateBaseAttack.Text);
			c.rangedAttackTime   = Convert.ToSingle(textBoxCreatureTemplateRangedAttack.Text);
			c.bVariance          = Convert.ToSingle(textBoxCreatureTemplateBV.Text);
			c.rVariance          = Convert.ToSingle(textBoxCreatureTemplateRV.Text);
			c.dSchool            = Convert.ToByte(textBoxCreatureTemplateDS.Text);
			c.aiName             = textBoxCreatureTemplateAIName.Text;
			c.movementType       = Convert.ToInt32(textBoxCreatureTemplateMType.Text);
			c.inhabitType        = Convert.ToInt32(textBoxCreatureTemplateInhabitType.Text);
			c.hoverHeight        = Convert.ToUInt32(textBoxCreatureTemplateHH.Text);
			c.gossipMenuId       = Convert.ToUInt32(textBoxCreatureTemplateGMID.Text);
			c.movementId         = Convert.ToUInt32(textBoxCreatureTemplateMID.Text);
			c.scriptName         = textBoxCreatureTemplateScriptName.Text;
			c.vehicleId          = Convert.ToUInt32(textBoxCreatureTemplateVID.Text);
			c.mechanicImmuneMask = Convert.ToInt32(textBoxCreatureTemplateMechanic.Text);
			c.family             = Convert.ToUInt32(textBoxCreatureTemplateFamily.Text);
			c.familyType         = Convert.ToUInt32(textBoxCreatureTemplateType.Text);
			c.typeFlags          = Convert.ToInt32(textBoxCreatureTemplateTypeFlags.Text);
			c.extraFlags         = Convert.ToInt32(textBoxCreatureTemplateFlagsExtra.Text);
			c.unitClass          = Convert.ToUInt32(textBoxCreatureTemplateUnitClass.Text);
			c.unitFlags1         = Convert.ToInt32(textBoxCreatureTemplateUnitflags.Text);
			c.unitFlags2         = Convert.ToInt32(textBoxCreatureTemplateUnitflags2.Text);
			c.dynamicFlags       = Convert.ToUInt32(textBoxCreatureTemplateDynamic.Text);
			c.isRegenHealth      = Convert.ToBoolean(checkBoxCreatureTemplateHR.Checked);
			c.resistHoly         = Convert.ToInt32(textBoxCreatureTemplateResis1.Text);
			c.resistFire         = Convert.ToInt32(textBoxCreatureTemplateResis2.Text);
			c.resistNature       = Convert.ToInt32(textBoxCreatureTemplateResis3.Text);
			c.resistFrost        = Convert.ToInt32(textBoxCreatureTemplateResis4.Text);
			c.resistShadow       = Convert.ToInt32(textBoxCreatureTemplateResis5.Text);
			c.resistArcane       = Convert.ToInt32(textBoxCreatureTemplateResis6.Text);
			c.trainerType        = Convert.ToInt32(textBoxCreatureTemplateTType.Text);
			c.trainerSpell       = Convert.ToInt32(textBoxCreatureTemplateTSpell.Text);
			c.trainerClass       = Convert.ToInt32(textBoxCreatureTemplateTRace.Text);
			c.trainerRace        = Convert.ToInt32(textBoxCreatureTemplateTClass.Text);
			c.lootId             = Convert.ToUInt32(textBoxCreatureTemplateLootID.Text);
			c.pickpocketId       = Convert.ToUInt32(textBoxCreatureTemplatePickID.Text);
			c.skinId             = Convert.ToUInt32(textBoxCreatureTemplateSkinID.Text);

			Models.CreatureModel.getInstance().creature = c;
		}
		private void updateCreatureVendor() {
			var dg = dataGridViewCreatureVendor;

			var vendor = new CreatureVendor[dataGridViewCreatureVendor.Rows.Count];

			for(var i = 0; i < vendor.Length; i++) {
				vendor[i] = new CreatureVendor();

				vendor[i].entry        = Convert.ToUInt32(dg.Rows[i].Cells[0].Value);
				vendor[i].slot         = Convert.ToInt32(dg.Rows[i].Cells[1].Value);
				vendor[i].item         = Convert.ToUInt32(dg.Rows[i].Cells[2].Value);
				vendor[i].maxCount     = Convert.ToUInt32(dg.Rows[i].Cells[3].Value);
				vendor[i].increaseTime = Convert.ToByte(dg.Rows[i].Cells[4].Value);
				vendor[i].extendedCost = Convert.ToUInt16(dg.Rows[i].Cells[5].Value);
			}

			Models.CreatureModel.getInstance().vendor = vendor;
		}
		private void updateCreatureLoot() {
			var dg = dataGridViewCreatureLoot;

			var lps = new CreatureLPS[dataGridViewCreatureLoot.Rows.Count];

			for(var i = 0; i < lps.Length; i++) {
				lps[i] = new CreatureLPS();

				lps[i].entry         = Convert.ToUInt32(dg.Rows[i].Cells[0].Value);
				lps[i].item          = Convert.ToUInt32(dg.Rows[i].Cells[1].Value);
				lps[i].reference     = Convert.ToInt32(dg.Rows[i].Cells[2].Value);
				lps[i].chance        = Convert.ToUInt32(dg.Rows[i].Cells[3].Value);
				lps[i].questRequired = Convert.ToByte(dg.Rows[i].Cells[4].Value);
				lps[i].lootMode      = Convert.ToUInt16(dg.Rows[i].Cells[5].Value);
				lps[i].groupId       = Convert.ToUInt16(dg.Rows[i].Cells[6].Value);
				lps[i].minCount      = Convert.ToUInt16(dg.Rows[i].Cells[7].Value);
				lps[i].maxCount      = Convert.ToUInt16(dg.Rows[i].Cells[8].Value);
			}

			Models.CreatureModel.getInstance().loot = lps;
		}
		private void updateCreaturePickpocket() {
			var dg = dataGridViewCreaturePickpocketLoot;

			var lps = new CreatureLPS[dataGridViewCreaturePickpocketLoot.Rows.Count];

			for(var i = 0; i < lps.Length; i++) {
				lps[i] = new CreatureLPS();

				lps[i].entry         = Convert.ToUInt32(dg.Rows[i].Cells[0].Value);
				lps[i].item          = Convert.ToUInt32(dg.Rows[i].Cells[1].Value);
				lps[i].reference     = Convert.ToInt32(dg.Rows[i].Cells[2].Value);
				lps[i].chance        = Convert.ToUInt32(dg.Rows[i].Cells[3].Value);
				lps[i].questRequired = Convert.ToByte(dg.Rows[i].Cells[4].Value);
				lps[i].lootMode      = Convert.ToUInt16(dg.Rows[i].Cells[5].Value);
				lps[i].groupId       = Convert.ToUInt16(dg.Rows[i].Cells[6].Value);
				lps[i].minCount      = Convert.ToUInt16(dg.Rows[i].Cells[7].Value);
				lps[i].maxCount      = Convert.ToUInt16(dg.Rows[i].Cells[8].Value);
			}

			Models.CreatureModel.getInstance().pickpocket = lps;
		}
		private void updateCreatureSkin() {
			var dg = dataGridViewCreatureSkinLoot;

			var lps = new CreatureLPS[dataGridViewCreatureSkinLoot.Rows.Count];

			for(var i = 0; i < lps.Length; i++) {
				lps[i] = new CreatureLPS();

				lps[i].entry         = Convert.ToUInt32(dg.Rows[i].Cells[0].Value);
				lps[i].item          = Convert.ToUInt32(dg.Rows[i].Cells[1].Value);
				lps[i].reference     = Convert.ToInt32(dg.Rows[i].Cells[2].Value);
				lps[i].chance        = Convert.ToUInt32(dg.Rows[i].Cells[3].Value);
				lps[i].questRequired = Convert.ToByte(dg.Rows[i].Cells[4].Value);
				lps[i].lootMode      = Convert.ToUInt16(dg.Rows[i].Cells[5].Value);
				lps[i].groupId       = Convert.ToUInt16(dg.Rows[i].Cells[6].Value);
				lps[i].minCount      = Convert.ToUInt16(dg.Rows[i].Cells[7].Value);
				lps[i].maxCount      = Convert.ToUInt16(dg.Rows[i].Cells[8].Value);
			}

			Models.CreatureModel.getInstance().skin = lps;
		}

		private Quest updateQuest() {
			Quest quest = new Quest();

			quest.id                              = Convert.ToUInt32(textBoxQuestSectionID.Text);
			quest.title                           = textBoxQuestSectionTitle.Text;
			quest.logDescription                  = textBoxQuestSectionLDescription.Text;
			quest.questDescription                = textBoxQuestSectionQDescription.Text;
			quest.area                            = textBoxQuestSectionAreaDescription.Text;
			quest.completed                       = textBoxQuestSectionCompleted.Text;
			quest.objective1                      = textBoxQuestSectionObjectives1.Text;
			quest.objective2                      = textBoxQuestSectionObjectives2.Text;
			quest.objective3                      = textBoxQuestSectionObjectives3.Text;
			quest.objective4                      = textBoxQuestSectionObjectives4.Text;
			quest.requirePlayerKills              = Convert.ToInt32(textBoxQuestSectionReqPK.Text);
			quest.timeAllowed                     = Convert.ToInt32(textBoxQuestSectionTimeAllowed.Text);
			quest.questInfo                       = Convert.ToInt32(textBoxQuestSectionQuestInfo.Text);
			quest.questLevel                      = Convert.ToInt32(textBoxQuestSectionQuestLevel.Text);
			quest.suggestedPlayers                = Convert.ToInt32(textBoxQuestSectionOtherSP.Text);
			quest.exclusiveGroup                  = Convert.ToInt32(textBoxQuestSectionExclusive.Text);
			quest.prevQuest                       = Convert.ToInt32(textBoxQuestSectionPrevQuest.Text);
			quest.nextQuest                       = Convert.ToInt32(textBoxQuestSectionNextQuest.Text);
			quest.races                           = Convert.ToInt32(textBoxQuestSectionReqRace.Text);
			quest.classes                         = Convert.ToInt32(textBoxQuestSectionReqClass.Text);
			quest.minLevel                        = Convert.ToInt32(textBoxQuestSectionReqLevelMin.Text);
			quest.maxLevel                        = Convert.ToInt32(textBoxQuestSectionReqLevelMax.Text);
			quest.reqFaction1                     = Convert.ToInt32(textBoxQuestSectionReqFaction1.Text);
			quest.reqFaction2                     = Convert.ToInt32(textBoxQuestSectionReqFaction2.Text);
			quest.reqValue1                       = Convert.ToInt32(textBoxQuestSectionReqValue1.Text);
			quest.reqValue2                       = Convert.ToInt32(textBoxQuestSectionReqValue2.Text);
			quest.minRepFaction                   = Convert.ToInt32(textBoxQuestSectionReqMinRepF.Text);
			quest.maxRepFaction                   = Convert.ToInt32(textBoxQuestSectionReqMaxRepF.Text);
			quest.minRepValue                     = Convert.ToInt32(textBoxQuestSectionReqMinRepV.Text);
			quest.maxRepValue                     = Convert.ToInt32(textBoxQuestSectionReqMaxRepV.Text);
			quest.zoneIdOrQuestSort               = Convert.ToInt32(textBoxQuestSectionReqQSort.Text);
			quest.skillId                         = Convert.ToInt32(textBoxQuestSectionReqSkillID.Text);
			quest.skillPoints                     = Convert.ToInt32(textBoxQuestSectionReqSkillPoints.Text);
			quest.type                            = Convert.ToInt32(textBoxQuestSectionQuestType.Text);
			quest.flags                           = Convert.ToInt32(textBoxQuestSectionQuestFlags.Text);
			quest.specialFlags                    = Convert.ToInt32(textBoxQuestSectionOtherSF.Text);
			quest.sourceItemId                    = Convert.ToInt32(textBoxQuestSectionSourceItemID.Text);
			quest.sourceItemCount                 = Convert.ToInt32(textBoxQuestSectionSourceItemCount.Text);
			quest.sourceSpellId                   = Convert.ToInt32(textBoxQuestSectionSourceSpellID.Text);
			quest.requiredNpcOrGoId1              = Convert.ToUInt32(textBoxQuestSectionReqNPCID1.Text);
			quest.requiredNpcOrGoId2              = Convert.ToUInt32(textBoxQuestSectionReqNPCID2.Text);
			quest.requiredNpcOrGoId3              = Convert.ToUInt32(textBoxQuestSectionReqNPCID3.Text);
			quest.requiredNpcOrGoId4              = Convert.ToUInt32(textBoxQuestSectionReqNPCID4.Text);
			quest.requiredNpcCount1               = Convert.ToInt32(textBoxQuestSectionReqNPCC1.Text);
			quest.requiredNpcCount2               = Convert.ToInt32(textBoxQuestSectionReqNPCC2.Text);
			quest.requiredNpcCount3               = Convert.ToInt32(textBoxQuestSectionReqNPCC3.Text);
			quest.requiredNpcCount4               = Convert.ToInt32(textBoxQuestSectionReqNPCC4.Text);
			quest.requiredItemId1                 = Convert.ToUInt32(textBoxQuestSectionReqItemID1.Text);
			quest.requiredItemId2                 = Convert.ToUInt32(textBoxQuestSectionReqItemID2.Text);
			quest.requiredItemId3                 = Convert.ToUInt32(textBoxQuestSectionReqItemID3.Text);
			quest.requiredItemId4                 = Convert.ToUInt32(textBoxQuestSectionReqItemID4.Text);
			quest.requiredItemId5                 = Convert.ToUInt32(textBoxQuestSectionReqItemID5.Text);
			quest.requiredItemId6                 = Convert.ToUInt32(textBoxQuestSectionReqItemID6.Text);
			quest.requiredItemCount1              = Convert.ToInt32(textBoxQuestSectionReqItemC1.Text);
			quest.requiredItemCount2              = Convert.ToInt32(textBoxQuestSectionReqItemC2.Text);
			quest.requiredItemCount3              = Convert.ToInt32(textBoxQuestSectionReqItemC3.Text);
			quest.requiredItemCount4              = Convert.ToInt32(textBoxQuestSectionReqItemC4.Text);
			quest.requiredItemCount5              = Convert.ToInt32(textBoxQuestSectionReqItemC5.Text);
			quest.requiredItemCount6              = Convert.ToInt32(textBoxQuestSectionReqItemC6.Text);
			quest.rewardChoiceItemId1             = Convert.ToUInt32(textBoxQuestSectionRewChoiceID1.Text);
			quest.rewardChoiceItemId2             = Convert.ToUInt32(textBoxQuestSectionRewChoiceID2.Text);
			quest.rewardChoiceItemId3             = Convert.ToUInt32(textBoxQuestSectionRewChoiceID3.Text);
			quest.rewardChoiceItemId4             = Convert.ToUInt32(textBoxQuestSectionRewChoiceID4.Text);
			quest.rewardChoiceItemId5             = Convert.ToUInt32(textBoxQuestSectionRewChoiceID5.Text);
			quest.rewardChoiceItemId6             = Convert.ToUInt32(textBoxQuestSectionRewChoiceID6.Text);
			quest.rewardChoiceItemCount1          = Convert.ToInt32(textBoxQuestSectionRewChoiceC1.Text);
			quest.rewardChoiceItemCount2          = Convert.ToInt32(textBoxQuestSectionRewChoiceC2.Text);
			quest.rewardChoiceItemCount3          = Convert.ToInt32(textBoxQuestSectionRewChoiceC3.Text);
			quest.rewardChoiceItemCount4          = Convert.ToInt32(textBoxQuestSectionRewChoiceC4.Text);
			quest.rewardChoiceItemCount5          = Convert.ToInt32(textBoxQuestSectionRewChoiceC5.Text);
			quest.rewardChoiceItemCount6          = Convert.ToInt32(textBoxQuestSectionRewChoiceC6.Text);
			quest.rewardItemId1                   = Convert.ToUInt32(textBoxQuestSectionRewItemID1.Text);
			quest.rewardItemId2                   = Convert.ToUInt32(textBoxQuestSectionRewItemID2.Text);
			quest.rewardItemId3                   = Convert.ToUInt32(textBoxQuestSectionRewItemID3.Text);
			quest.rewardItemId4                   = Convert.ToUInt32(textBoxQuestSectionRewItemID4.Text);
			quest.rewardItemCount1                = Convert.ToInt32(textBoxQuestSectionRewItemC1.Text);
			quest.rewardItemCount2                = Convert.ToInt32(textBoxQuestSectionRewItemC2.Text);
			quest.rewardItemCount3                = Convert.ToInt32(textBoxQuestSectionRewItemC3.Text);
			quest.rewardItemCount4                = Convert.ToInt32(textBoxQuestSectionRewItemC4.Text);
			quest.rewardFactionId1                = Convert.ToUInt32(textBoxQuestSectionRewFactionID1.Text);
			quest.rewardFactionId2                = Convert.ToUInt32(textBoxQuestSectionRewFactionID2.Text);
			quest.rewardFactionId3                = Convert.ToUInt32(textBoxQuestSectionRewFactionID3.Text);
			quest.rewardFactionId4                = Convert.ToUInt32(textBoxQuestSectionRewFactionID4.Text);
			quest.rewardFactionId5                = Convert.ToUInt32(textBoxQuestSectionRewFactionID5.Text);
			quest.rewardFactionValue1             = Convert.ToInt32(textBoxQuestSectionRewFactionV1.Text);
			quest.rewardFactionValue2             = Convert.ToInt32(textBoxQuestSectionRewFactionV2.Text);
			quest.rewardFactionValue3             = Convert.ToInt32(textBoxQuestSectionRewFactionV3.Text);
			quest.rewardFactionValue4             = Convert.ToInt32(textBoxQuestSectionRewFactionV4.Text);
			quest.rewardFactionValue5             = Convert.ToInt32(textBoxQuestSectionRewFactionV5.Text);
			quest.rewardOrRequiredMoney           = Convert.ToInt32(textBoxQuestSectionRewOtherMoney.Text);
			quest.rewardOrRequiredMoneyML         = Convert.ToInt32(textBoxQuestSectionRewOtherMoneyML.Text);
			quest.rewardOrRequiredArenaPoints     = Convert.ToInt32(textBoxQuestSectionRewOtherAP.Text);
			quest.rewardOrRequiredHonorPoints     = Convert.ToInt32(textBoxQuestSectionRewOtherHP.Text);
			quest.rewardOrRequiredHonorMultiplier = Convert.ToInt32(textBoxQuestSectionRewOtherHM.Text);
			quest.rewardTitleId                   = Convert.ToUInt32(textBoxQuestSectionRewOtherTitleID.Text);
			quest.rewardTalent                    = Convert.ToUInt32(textBoxQuestSectionRewOtherTP.Text);
			quest.mailTemplateId                  = Convert.ToUInt32(textBoxQuestSectionRewOtherMailID.Text);
			quest.mailDelay                       = Convert.ToUInt32(textBoxQuestSectionRewOtherMailDelay.Text);
			quest.rewardDisplaySpell              = Convert.ToUInt32(textBoxQuestSectionRewSpellDisplay.Text);
			quest.rewardSpell                     = Convert.ToUInt32(textBoxQuestSectionRewSpell.Text);

			return quest;
		}

		private GameObject updateGameObject() {
			GameObject go = new GameObject();

			go.entry      = Convert.ToUInt32(textBoxGameObjectTempEntry.Text);
			go.type       = Convert.ToUInt32(textBoxGameObjectTempType.Text);
			go.displayId  = Convert.ToUInt32(textBoxGameObjectTempDID.Text);
			go.name       = textBoxGameObjectTempName.Text;
			go.size       = Convert.ToSingle(textBoxGameObjectTempFaction.Text);
			go.aiName     = textBoxGameObjectTempFlags.Text;
			go.scriptName = textBoxGameObjectTempSize.Text;
			go.data0      = Convert.ToInt32(textBoxGameObjectTempD0.Text);
			go.data1      = Convert.ToInt32(textBoxGameObjectTempD1.Text);
			go.data2      = Convert.ToInt32(textBoxGameObjectTempD2.Text);
			go.data3      = Convert.ToInt32(textBoxGameObjectTempD3.Text);
			go.data4      = Convert.ToInt32(textBoxGameObjectTempD4.Text);
			go.data5      = Convert.ToInt32(textBoxGameObjectTempD5.Text);
			go.data6      = Convert.ToInt32(textBoxGameObjectTempD6.Text);
			go.data7      = Convert.ToInt32(textBoxGameObjectTempD7.Text);
			go.data8      = Convert.ToInt32(textBoxGameObjectTempD8.Text);
			go.data9      = Convert.ToInt32(textBoxGameObjectTempD9.Text);
			go.data10     = Convert.ToInt32(textBoxGameObjectTempD10.Text);
			go.data11     = Convert.ToInt32(textBoxGameObjectTempD11.Text);
			go.data12     = Convert.ToInt32(textBoxGameObjectTempD12.Text);
			go.data13     = Convert.ToInt32(textBoxGameObjectTempD13.Text);
			go.data14     = Convert.ToInt32(textBoxGameObjectTempD14.Text);
			go.data15     = Convert.ToInt32(textBoxGameObjectTempD15.Text);
			go.data16     = Convert.ToInt32(textBoxGameObjectTempD16.Text);
			go.data17     = Convert.ToInt32(textBoxGameObjectTempD17.Text);
			go.data18     = Convert.ToInt32(textBoxGameObjectTempD18.Text);
			go.data19     = Convert.ToInt32(textBoxGameObjectTempD19.Text);
			go.data20     = Convert.ToInt32(textBoxGameObjectTempD20.Text);
			go.data21     = Convert.ToInt32(textBoxGameObjectTempD21.Text);
			go.data22     = Convert.ToInt32(textBoxGameObjectTempD22.Text);
			go.data23     = Convert.ToInt32(textBoxGameObjectTempD23.Text);

			return go;
		}

		private Item updateItem() {
			Item item = new Item();

			item.entry = Convert.ToUInt32(textBoxItemTempEntry.Text);
			item.iClass = Convert.ToUInt32(textBoxItemTempTypeClass.Text);
			item.iSub = Convert.ToUInt32(textBoxItemTempSubclass.Text);
			item.name = textBoxItemTempName.Text;
			item.description = textBoxItemTempDescription.Text;
			item.displayId = Convert.ToUInt32(textBoxItemTempDisplayID.Text);
			item.quality = Convert.ToUInt32(textBoxItemTempQuality.Text);
			item.buycount = Convert.ToInt32(textBoxItemTempBuyC.Text);
			item.inventory = Convert.ToUInt32(textBoxItemTempInventory.Text);
			item.flags = Convert.ToInt32(textBoxItemTempFlags.Text);
			item.extraFlags = Convert.ToInt32(textBoxItemTempEFlags.Text);
			item.maxCount = Convert.ToInt32(textBoxItemTempMaxC.Text);
			item.containerSlot = Convert.ToUInt32(textBoxItemTempContainer.Text);
			item.buyPrice = Convert.ToUInt32(textBoxItemTempBuyP.Text);
			item.sellPrice = Convert.ToUInt32(textBoxItemTempSellP.Text);
			item.damageType1 = Convert.ToUInt32(textBoxItemTempDmgType1.Text);
			item.damageType2 = Convert.ToUInt32(textBoxItemTempDmgType2.Text);
			item.damageMin1 = Convert.ToUInt32(textBoxItemTempDmgMin1.Text);
			item.damageMin2 = Convert.ToUInt32(textBoxItemTempDmgMin2.Text);
			item.damageMax1 = Convert.ToUInt32(textBoxItemTempDmgMax1.Text);
			item.damageMax2 = Convert.ToUInt32(textBoxItemTempDmgMax2.Text);
			item.delay = Convert.ToUInt32(textBoxItemTempDelay.Text);
			item.ammoType = Convert.ToUInt32(textBoxItemTempAmmoType.Text);
			item.rangedMod = Convert.ToUInt32(textBoxItemTempRangedMod.Text);
			item.itemSet = Convert.ToUInt32(textBoxItemTempItemSet.Text);
			item.bonding = Convert.ToUInt32(textBoxItemTempBonding.Text);
			item.block = Convert.ToInt32(textBoxItemTempBlock.Text);
			item.durability = Convert.ToInt32(textBoxItemTempDurability.Text);
			item.sheath = Convert.ToUInt32(textBoxItemTempSheath.Text);
			item.reistanceHoly = Convert.ToUInt32(textBoxItemTempResisHoly.Text);
			item.reistanceFrost = Convert.ToUInt32(textBoxItemTempResisFrost.Text);
			item.reistanceFire = Convert.ToUInt32(textBoxItemTempResisFire.Text);
			item.reistanceShadow = Convert.ToUInt32(textBoxItemTempResisShadow.Text);
			item.reistanceNature = Convert.ToUInt32(textBoxItemTempResisNature.Text);
			item.reistanceArcane = Convert.ToUInt32(textBoxItemTempResisArcane.Text);
			item.socketColor1 = Convert.ToUInt32(textBoxItemTempColor1.Text);
			item.socketColor2 = Convert.ToUInt32(textBoxItemTempColor2.Text);
			item.socketColor3 = Convert.ToUInt32(textBoxItemTempColor3.Text);
			item.socketContent1 = Convert.ToUInt32(textBoxItemTempContent1.Text);
			item.socketContent2 = Convert.ToUInt32(textBoxItemTempContent2.Text);
			item.socketContent3 = Convert.ToUInt32(textBoxItemTempContent3.Text);
			item.socketBonus = Convert.ToUInt32(textBoxItemTempSocketBonus.Text);
			item.socketGemProperty = Convert.ToUInt32(textBoxItemTempGemProper.Text);
			item.spellEntry1 = Convert.ToUInt32(textBoxItemTempSpellID1.Text);
			item.spellEntry2 = Convert.ToUInt32(textBoxItemTempSpellID2.Text);
			item.spellEntry3 = Convert.ToUInt32(textBoxItemTempSpellID3.Text);
			item.spellEntry4 = Convert.ToUInt32(textBoxItemTempSpellID4.Text);
			item.spellEntry5 = Convert.ToUInt32(textBoxItemTempSpellID5.Text);
			item.spellTrigger1 = Convert.ToInt32(textBoxItemTempTrigger1.Text);
			item.spellTrigger2 = Convert.ToInt32(textBoxItemTempTrigger2.Text);
			item.spellTrigger3 = Convert.ToInt32(textBoxItemTempTrigger3.Text);
			item.spellTrigger4 = Convert.ToInt32(textBoxItemTempTrigger4.Text);
			item.spellTrigger5 = Convert.ToInt32(textBoxItemTempTrigger5.Text);
			item.spellCharges1 = Convert.ToInt32(textBoxItemTempCharges1.Text);
			item.spellCharges2 = Convert.ToInt32(textBoxItemTempCharges2.Text);
			item.spellCharges3 = Convert.ToInt32(textBoxItemTempCharges3.Text);
			item.spellCharges4 = Convert.ToInt32(textBoxItemTempCharges4.Text);
			item.spellCharges5 = Convert.ToInt32(textBoxItemTempCharges5.Text);
			item.spellPPMRate1 = Convert.ToInt32(textBoxItemTempRate1.Text);
			item.spellPPMRate2 = Convert.ToInt32(textBoxItemTempRate2.Text);
			item.spellPPMRate3 = Convert.ToInt32(textBoxItemTempRate3.Text);
			item.spellPPMRate4 = Convert.ToInt32(textBoxItemTempRate4.Text);
			item.spellPPMRate5 = Convert.ToInt32(textBoxItemTempRate5.Text);
			item.spellCooldown1 = Convert.ToSingle(textBoxItemTempCD1.Text);
			item.spellCooldown2 = Convert.ToSingle(textBoxItemTempCD2.Text);
			item.spellCooldown3 = Convert.ToSingle(textBoxItemTempCD3.Text);
			item.spellCooldown4 = Convert.ToSingle(textBoxItemTempCD4.Text);
			item.spellCooldown5 = Convert.ToSingle(textBoxItemTempCD5.Text);
			item.spellCategory1 = Convert.ToInt32(textBoxItemTempCategory1.Text);
			item.spellCategory2 = Convert.ToInt32(textBoxItemTempCategory2.Text);
			item.spellCategory3 = Convert.ToInt32(textBoxItemTempCategory3.Text);
			item.spellCategory4 = Convert.ToInt32(textBoxItemTempCategory4.Text);
			item.spellCategory5 = Convert.ToInt32(textBoxItemTempCategory5.Text);
			item.spellCategoryCooldown1 = Convert.ToSingle(textBoxItemTempCategoryCD1.Text);
			item.spellCategoryCooldown2 = Convert.ToSingle(textBoxItemTempCategoryCD2.Text);
			item.spellCategoryCooldown3 = Convert.ToSingle(textBoxItemTempCategoryCD3.Text);
			item.spellCategoryCooldown4 = Convert.ToSingle(textBoxItemTempCategoryCD4.Text);
			item.spellCategoryCooldown5 = Convert.ToSingle(textBoxItemTempCategoryCD5.Text);
			item.startQuest = Convert.ToUInt32(textBoxItemTempStartQuest.Text);
			item.material = Convert.ToInt32(textBoxItemTempMaterial.Text);
			item.property = Convert.ToUInt32(textBoxItemTempProperty.Text);
			item.suffix = Convert.ToUInt32(textBoxItemTempSuffix.Text);
			item.area = Convert.ToUInt32(textBoxItemTempArea.Text);
			item.map = Convert.ToUInt32(textBoxItemTempMap.Text);
			item.disenchantId = Convert.ToUInt32(textBoxItemTempDisenchantID.Text);
			item.pageText = Convert.ToUInt32(textBoxItemTempPageText.Text);
			item.languageId = Convert.ToUInt32(textBoxItemTempLanguage.Text);
			item.pageMaterial = Convert.ToUInt32(textBoxItemTempPageMaterial.Text);
			item.foodType = Convert.ToUInt32(textBoxItemTempFoodType.Text);
			item.lockId = Convert.ToUInt32(textBoxItemTempLockID.Text);
			item.holidayId = Convert.ToUInt32(textBoxItemTempHolidayID.Text);
			item.bagFamily = Convert.ToUInt32(textBoxItemTempBagFamily.Text);
			item.modifier = Convert.ToUInt32(textBoxItemTempModifier.Text);
			item.duration = Convert.ToUInt32(textBoxItemTempDuration.Text);
			item.limitCategory = Convert.ToUInt32(textBoxItemTempLimitCate.Text);
			item.minMoney = Convert.ToUInt32(textBoxItemTempMoneyMin.Text);
			item.maxMoney = Convert.ToUInt32(textBoxItemTempMoneyMax.Text);
			item.flagsCustom = Convert.ToUInt32(textBoxItemTempFlagsC.Text);
			item.totemCategory = Convert.ToUInt32(textBoxItemTempTotemCategory.Text);
			item.reqRace = Convert.ToInt32(textBoxItemTempReqRace.Text);
			item.reqClass = Convert.ToInt32(textBoxItemTempReqClass.Text);
			item.reqLevel = Convert.ToUInt32(textBoxItemTempReqLevel.Text);
			item.reqSkill = Convert.ToUInt32(textBoxItemTempReqSkill.Text);
			item.reqSkillRank = Convert.ToUInt32(textBoxItemTempReqSkillRank.Text);
			item.reqHonorRank = Convert.ToUInt32(textBoxItemTempReqHonorRank.Text);
			item.reqRepFaction = Convert.ToUInt32(textBoxItemTempReqRepFaction.Text);
			item.reqRepRank = Convert.ToUInt32(textBoxItemTempReqRepRank.Text);
			item.reqDisenchant = Convert.ToUInt32(textBoxItemTempReqDisenchant.Text);
			item.reqSpell = Convert.ToUInt32(textBoxItemTempReqSpell.Text);
			item.reqCityRank = Convert.ToUInt32(textBoxItemTempReqCityRank.Text);
			item.reqItemLevel = Convert.ToInt32(textBoxItemTempReqItemLevel.Text);
			item.statsCount = Convert.ToUInt32(textBoxItemTempStatsC.Text);
			item.statsType1 = Convert.ToUInt32(textBoxItemTempStatsType1.Text);
			item.statsType2 = Convert.ToUInt32(textBoxItemTempStatsType2.Text);
			item.statsType3 = Convert.ToUInt32(textBoxItemTempStatsType3.Text);
			item.statsType4 = Convert.ToUInt32(textBoxItemTempStatsType4.Text);
			item.statsType5 = Convert.ToUInt32(textBoxItemTempStatsType5.Text);
			item.statsType6 = Convert.ToUInt32(textBoxItemTempStatsType6.Text);
			item.statsType7 = Convert.ToUInt32(textBoxItemTempStatsType7.Text);
			item.statsType8 = Convert.ToUInt32(textBoxItemTempStatsType8.Text);
			item.statsType9 = Convert.ToUInt32(textBoxItemTempStatsType9.Text);
			item.statsType10 = Convert.ToUInt32(textBoxItemTempStatsType10.Text);
			item.statsValue1 = Convert.ToInt32(textBoxItemTempStatsValue1.Text);
			item.statsValue2 = Convert.ToInt32(textBoxItemTempStatsValue2.Text);
			item.statsValue3 = Convert.ToInt32(textBoxItemTempStatsValue3.Text);
			item.statsValue4 = Convert.ToInt32(textBoxItemTempStatsValue4.Text);
			item.statsValue5 = Convert.ToInt32(textBoxItemTempStatsValue5.Text);
			item.statsValue6 = Convert.ToInt32(textBoxItemTempStatsValue6.Text);
			item.statsValue7 = Convert.ToInt32(textBoxItemTempStatsValue7.Text);
			item.statsValue8 = Convert.ToInt32(textBoxItemTempStatsValue8.Text);
			item.statsValue9 = Convert.ToInt32(textBoxItemTempStatsValue9.Text);
			item.statsValue10 = Convert.ToInt32(textBoxItemTempStatsValue10.Text);
			item.scalingStatDist = Convert.ToInt32(textBoxItemTempStatsScaleDist.Text);
			item.scalingStatValue = Convert.ToInt32(textBoxItemTempStatsScaleValue.Text);

			return item;
		}
		private ItemLPMD[] updateItemLoot() {
			var dg = dataGridViewItemLoot.Rows[0];

			var lpmd = new ItemLPMD[dataGridViewItemLoot.Rows.Count];

			for(var i = 0; i < lpmd.Length; i++) {
				lpmd[i] = new ItemLPMD();

				lpmd[i].entry         = Convert.ToUInt32(dg.Cells[0].Value);
				lpmd[i].item          = Convert.ToUInt32(dg.Cells[1].Value);
				lpmd[i].reference     = Convert.ToInt32(dg.Cells[2].Value);
				lpmd[i].chance        = Convert.ToUInt32(dg.Cells[3].Value);
				lpmd[i].questRequired = Convert.ToByte(dg.Cells[4].Value);
				lpmd[i].lootMode      = Convert.ToUInt16(dg.Cells[5].Value);
				lpmd[i].groupId       = Convert.ToUInt16(dg.Cells[6].Value);
				lpmd[i].minCount      = Convert.ToUInt16(dg.Cells[7].Value);
				lpmd[i].maxCount      = Convert.ToUInt16(dg.Cells[8].Value);
			}

			return lpmd;
		}
		private ItemLPMD[] updateItemProspect() {
			var dg = dataGridViewItemProspect.Rows[0];

			var lpmd = new ItemLPMD[dataGridViewItemProspect.Rows.Count];

			for(var i = 0; i < lpmd.Length; i++) {
				lpmd[i] = new ItemLPMD();

				lpmd[i].entry         = Convert.ToUInt32(dg.Cells[0].Value);
				lpmd[i].item          = Convert.ToUInt32(dg.Cells[1].Value);
				lpmd[i].reference     = Convert.ToInt32(dg.Cells[2].Value);
				lpmd[i].chance        = Convert.ToUInt32(dg.Cells[3].Value);
				lpmd[i].questRequired = Convert.ToByte(dg.Cells[4].Value);
				lpmd[i].lootMode      = Convert.ToUInt16(dg.Cells[5].Value);
				lpmd[i].groupId       = Convert.ToUInt16(dg.Cells[6].Value);
				lpmd[i].minCount      = Convert.ToUInt16(dg.Cells[7].Value);
				lpmd[i].maxCount      = Convert.ToUInt16(dg.Cells[8].Value);
			}

			return lpmd;
		}
		private ItemLPMD[] updateItemMilling() {
			var dg = dataGridViewItemMill.Rows[0];

			var lpmd = new ItemLPMD[dataGridViewItemMill.Rows.Count];

			for(var i = 0; i < lpmd.Length; i++) {
				lpmd[i] = new ItemLPMD();

				lpmd[i].entry         = Convert.ToUInt32(dg.Cells[0].Value);
				lpmd[i].item          = Convert.ToUInt32(dg.Cells[1].Value);
				lpmd[i].reference     = Convert.ToInt32(dg.Cells[2].Value);
				lpmd[i].chance        = Convert.ToUInt32(dg.Cells[3].Value);
				lpmd[i].questRequired = Convert.ToByte(dg.Cells[4].Value);
				lpmd[i].lootMode      = Convert.ToUInt16(dg.Cells[5].Value);
				lpmd[i].groupId       = Convert.ToUInt16(dg.Cells[6].Value);
				lpmd[i].minCount      = Convert.ToUInt16(dg.Cells[7].Value);
				lpmd[i].maxCount      = Convert.ToUInt16(dg.Cells[8].Value);
			}

			return lpmd;
		}
		private ItemLPMD[] updateItemDisenchant() {
			var dg = dataGridViewItemDE.Rows[0];

			var lpmd = new ItemLPMD[dataGridViewItemDE.Rows.Count];

			for(var i = 0; i < lpmd.Length; i++) {
				lpmd[i] = new ItemLPMD();

				lpmd[i].entry         = Convert.ToUInt32(dg.Cells[0].Value);
				lpmd[i].item          = Convert.ToUInt32(dg.Cells[1].Value);
				lpmd[i].reference     = Convert.ToInt32(dg.Cells[2].Value);
				lpmd[i].chance        = Convert.ToUInt32(dg.Cells[3].Value);
				lpmd[i].questRequired = Convert.ToByte(dg.Cells[4].Value);
				lpmd[i].lootMode      = Convert.ToUInt16(dg.Cells[5].Value);
				lpmd[i].groupId       = Convert.ToUInt16(dg.Cells[6].Value);
				lpmd[i].minCount      = Convert.ToUInt16(dg.Cells[7].Value);
				lpmd[i].maxCount      = Convert.ToUInt16(dg.Cells[8].Value);
			}

			return lpmd;
		}

		#endregion

		#endregion

		#region Primary Events

		#region Misc

		private void FormMain_Load(object sender, EventArgs e) {
			this.Icon = Properties.Resources.iconManti;
			//bsAA = new BindingSource();
			//bsCI = new BindingSource();

			tabControlCategory.Focus();
			tabControlCategory.SelectedTab = tabPageItem;

			dataGridViewAccountSearch.AutoGenerateColumns = false;
			dataGridViewCreatureSearch.AutoGenerateColumns = false;
			dataGridViewQuestSearch.AutoGenerateColumns = false;
			dataGridViewGameObjectSearch.AutoGenerateColumns = false;
			dataGridViewItemSearch.AutoGenerateColumns = false;
			dataGridViewCharacterInventory.AutoGenerateColumns = false;
			dataGridViewItemLoot.AutoGenerateColumns = false;
			dataGridViewItemProspect.AutoGenerateColumns = false;
			dataGridViewItemMill.AutoGenerateColumns = false;
			dataGridViewItemDE.AutoGenerateColumns = false;
			dataGridViewQuestGivers.AutoGenerateColumns = false;

			setOfflineMode(FormMySQL.Offline);
		}
		private void tabControlCategory_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Enter) {
				e.SuppressKeyPress = true;

				if(tabControlCategory.SelectedTab == tabPageAccount) { // Account Tab
					if(tabControlCategoryAccount.SelectedTab == tabPageAccountSearch) {
						buttonAccountSearchSearch_Click(this, new EventArgs());
					}
				} else if(tabControlCategory.SelectedTab == tabPageCharacter) { // Character Tab
					if(tabControlCategoryCharacter.SelectedTab == tabPageCharacterSearch) {
						buttonCharacterSearchSearch_Click(this, new EventArgs());
					}
				} else if(tabControlCategory.SelectedTab == tabPageCreature) { // Creature Tab
					if(tabControlCategoryCreature.SelectedTab == tabPageCreatureSearch) {
						buttonCreatureSearchSearch_Click(this, new EventArgs());
					}
				} else if(tabControlCategory.SelectedTab == tabPageQuest) { // Quest Tab
					if(tabControlCategoryQuest.SelectedTab == tabPageQuestSearch) {
						buttonQuestSearchSearch_Click(this, new EventArgs());
					}
				} else if(tabControlCategory.SelectedTab == tabPageGameObject) { // Game Object Tab
					if(tabControlCategoryGameObject.SelectedTab == tabPageGameObjectSearch) {
						buttonGameObjectSearchSearch_Click(this, new EventArgs());
					}
				} else if(tabControlCategory.SelectedTab == tabPageItem) { // Item Tab
					if(tabControlCategoryItem.SelectedTab == tabPageItemSearch) {
						buttonItemSearchSearch_Click(this, new EventArgs());
					}
				}
			}
		}

		#endregion

		#region Search

		private void buttonAccountSearchSearch_Click(object sender, EventArgs e) {
			bool emptyControls = CheckEmptyControls(tabPageAccountSearch);

			var dbAuth = Settings.getAuthDB();
			var user   = textBoxAccountSearchUsername.Text;
			var id     = textBoxAccountSearchID.Text;

			DataTable dt;

			if(emptyControls) {
				var dialog = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

				if(dialog == DialogResult.Cancel) { return; } else {
					dt = dbAuth.searchForAllAccounts();
					dataGridViewAccountSearch.DataSource = dt;
				}
			} else {
				dataGridViewCharacterSearch.DataSource = null;

				if(!string.IsNullOrEmpty(user)) {
					bool isExact = user.StartsWith("#");

					user = (isExact ? Classes.UtilityHelper.removeExactSign(user) : user);

					dt = dbAuth.searchForAccounts(user, isExact);
					dataGridViewAccountSearch.DataSource = dt;

				} else {
					dt = dbAuth.searchForAccountByID(Convert.ToUInt32(id));
					dataGridViewAccountSearch.DataSource = dt;
				}
			}

			toolStripStatusLabelAccountSearchRows.Text = "Account(s) found: " + (dt != null ? dt.Rows.Count.ToString() : "0");
		}
		private void buttonCharacterSearchSearch_Click(object sender, EventArgs e) {
			bool emptyControls = CheckEmptyControls(tabPageCharacterSearch);

			var dbChar  = Settings.getCharsDB();
			var user    = textBoxCharacterSearchUsername.Text;
			var account = textBoxCharacterSearchAccount.Text;
			var guid    = textBoxCharacterSearchID.Text;

			DataTable dt;

			if(emptyControls) {
				var dialog = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

				if(dialog == DialogResult.Cancel) { return; }

				dt = dbChar.searchForAllCharacters();
				dataGridViewCharacterSearch.DataSource = dt;

			} else {
				dataGridViewCharacterSearch.DataSource = null;

				if(!string.IsNullOrEmpty(user)) {
					bool isExact = user.StartsWith("#");

					user = (isExact ? Classes.UtilityHelper.removeExactSign(user) : user);

					dt = dbChar.searchForCharacters(user, isExact);
					dataGridViewCharacterSearch.DataSource = dt;

				} else if(!string.IsNullOrEmpty(account)) {
					dt = dbChar.searchForCharacterByAccount(Convert.ToUInt32(account));
					dataGridViewCharacterSearch.DataSource = dt;
				} else {
					dt = dbChar.searchForCharacterByID(Convert.ToUInt32(guid));
					dataGridViewCharacterSearch.DataSource = dt;
				}
			}

			toolStripStatusLabelCharacterSearchRows.Text = "Character(s) found: " + (dt != null ? dt.Rows.Count.ToString() : "0");
		}
		private void buttonCreatureSearchSearch_Click(object sender, EventArgs e) {
			bool emptyControls = CheckEmptyControls(tabPageCreatureSearch);

			var dbWorld  = Settings.getWorldDB();
			var entry    = textBoxCreatureSearchEntry.Text;
			var name     = textBoxCreatureSearchName.Text;
			var subname  = textBoxCreatureSearchSubname.Text;
			var minlevel = textBoxCreatureSearchLevelMin.Text;
			var maxlevel = textBoxCreatureSearchLevelMax.Text;
			var rank     = textBoxCreatureSearchRank.Text;

			DataTable dt;

			if(emptyControls) {
				var dialog = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

				if(dialog == DialogResult.Cancel) { return; }

				dt = dbWorld.searchForAllCreatures();
				dataGridViewCreatureSearch.DataSource = dt;
			} else {
				dataGridViewCreatureSearch.DataSource = null;

				if(!string.IsNullOrEmpty(entry)) {
					dt = dbWorld.searchForCreatureByID(Convert.ToUInt32(entry));
					dataGridViewCreatureSearch.DataSource = dt;
				} else {
					dt = dbWorld.searchForCreatures(name, subname, minlevel, maxlevel, rank);
					dataGridViewCreatureSearch.DataSource = dt;
				}

			}

			toolStripStatusLabelCreatureSearchRows.Text = "Creature(s) found: " + (dt != null ? dt.Rows.Count.ToString() : "0");
		}
		private void buttonQuestSearchSearch_Click(object sender, EventArgs e) {
			bool emptyControls = CheckEmptyControls(tabPageQuestSearch);

			var dbWorld = Settings.getWorldDB();
			var id = textBoxQuestSearchID.Text;
			var title = textBoxQuestSearchTitle.Text;
			var giver = textBoxQuestSearchGiver.Text;
			var taker = textBoxQuestSearchTaker.Text;
			var prevId = textBoxQuestSearchPQID.Text;
			var nextId = textBoxQuestSearchNQID.Text;
			var info = textBoxQuestSearchInfo.Text;

			DataTable dt;

			if(emptyControls) {
				var dialog = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

				if(dialog == DialogResult.Cancel) { return; }

				dt = dbWorld.searchForAllQuests();
				dataGridViewQuestSearch.DataSource = dt;
			} else {
				dataGridViewQuestSearch.DataSource = null;

				if(!string.IsNullOrEmpty(id)) {
					dt = dbWorld.searchForQuestsById(Convert.ToUInt32(id));
				} else if(!string.IsNullOrEmpty(title)) {
					bool isExact = title.StartsWith("#");
					dt = dbWorld.searchForQuestsByTitle(isExact ? Classes.UtilityHelper.removeExactSign(title) : title, isExact);
				} else if(!string.IsNullOrEmpty(giver)) {
					dt = dbWorld.searchForQuestsByGiver(Convert.ToUInt32(giver));
				} else if(!string.IsNullOrEmpty(taker)) {
					dt = dbWorld.searchForQuestsByTaker(Convert.ToUInt32(taker));
				} else if(!string.IsNullOrEmpty(prevId)) {
					dt = dbWorld.searchForQuestsByPrevId(Convert.ToUInt32(prevId));
				} else if(!string.IsNullOrEmpty(nextId)) {
					dt = dbWorld.searchForQuestsByNextId(Convert.ToUInt32(nextId));
				} else {
					dt = dbWorld.searchForQuestsByInfoId(Convert.ToUInt32(info));
				}

				dataGridViewQuestSearch.DataSource = dt;
			}

			toolStripStatusLabelQuestSearchRows.Text = "Quest(s) found: " + (dt != null ? dt.Rows.Count.ToString() : "0");
		}
		private void buttonGameObjectSearchSearch_Click(object sender, EventArgs e) {
			bool emptyControls = CheckEmptyControls(tabPageGameObjectSearch);

			var dbWorld = Settings.getWorldDB();
			var entry   = textBoxGameObjectSearchEntry.Text;
			var name    = textBoxGameObjectSearchName.Text;

			DataTable dt;

			if(emptyControls) {
				var dialog = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

				if(dialog == DialogResult.Cancel) { return; }

				dt = dbWorld.searchForAllGameObjects();
				dataGridViewGameObjectSearch.DataSource = dt;
			} else {
				dataGridViewGameObjectSearch.DataSource = null;

				if(!string.IsNullOrEmpty(entry)) {
					dt = dbWorld.searchForAllGameObjectsByEntry(Convert.ToUInt32(entry));
					dataGridViewGameObjectSearch.DataSource = dt;
				} else {
					bool isExact = name.StartsWith("#");
					dt = dbWorld.searchForAllGameObjectsByName((isExact ? Classes.UtilityHelper.removeExactSign(name) : name), isExact);
					dataGridViewGameObjectSearch.DataSource = dt;
				}
			}

			toolStripStatusLabelGameObjectSearchRows.Text = "Game Object(s) found: " + (dt != null ? dt.Rows.Count.ToString() : "0");
		}
		private void buttonItemSearchSearch_Click(object sender, EventArgs e) {
			bool emptyControls = CheckEmptyControls(tabPageItemSearch);

			var dbWorld  = Settings.getWorldDB();
			var entry    = textBoxItemSearchEntry.Text;
			var name     = textBoxItemSearchName.Text;
			var desc     = textBoxItemSearchDescription.Text;
			var iClass   = textBoxItemSearchClass.Text;
			var iSub     = textBoxItemSearchSubclass.Text;
			var quality  = comboBoxItemSearchQuality.SelectedIndex;
			var reqLevel = textBoxItemSearchReqLevel.Text;

			DataTable dt;

			if(emptyControls) {
				var dialog = MessageBox.Show("You sure, you want to load them all?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

				if(dialog == DialogResult.Cancel) { return; }

				dt = dbWorld.searchForAllItems();
				dataGridViewItemSearch.DataSource = dt;
			} else {
				dataGridViewItemSearch.DataSource = null;

				if(!string.IsNullOrEmpty(entry)) {
					dt = dbWorld.searchForItemByEntry(Convert.ToUInt32(entry));
					dataGridViewItemSearch.DataSource = dt;
				} else {

					iClass   = string.IsNullOrEmpty(iClass) ? "-1" : iClass;
					iSub     = string.IsNullOrEmpty(iSub) ? "-1" : iSub;
					reqLevel = string.IsNullOrEmpty(reqLevel) ? "-1" : reqLevel;

					dt = dbWorld.searchForItems(name, desc, Convert.ToInt32(iClass), Convert.ToInt32(iSub), Convert.ToInt32(quality), Convert.ToInt32(reqLevel));
					dataGridViewItemSearch.DataSource = dt;
				}
			}

			toolStripStatusLabelItemSearchRows.Text = "Item(s) found: " + (dt != null ? dt.Rows.Count.ToString() : "0");
		}
		
		#endregion

		#region CellDoubleClick

		private void dataGridViewAccountSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
			var id = Convert.ToUInt32(dataGridViewAccountSearch.SelectedCells[0].Value.ToString());

			var accModel = Models.AccountModel.getInstance();
			accModel.getAccountFromDatabase(id);

			var account = accModel.account;

			fillAccountTemplate(accModel.account);
		}
		private void dataGridViewCharacterSearchSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
			var guid = Convert.ToUInt32(dataGridViewCharacterSearch.SelectedCells[0].Value.ToString());

			var model = Models.CharacterModel.getInstance();

			model.updateFullCharacterFromDatabase(guid);

			fillCharacterTemplate(model.character);
			addCharacterInventory(model.inventory);
		}
		private void dataGridViewCreatureSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
			var entry = Convert.ToUInt32(dataGridViewCreatureSearch.SelectedCells[0].Value.ToString());

			var model = Models.CreatureModel.getInstance();

			model.updateFullCreatureFromDatabase(entry);

			fillCreatureTemplate(model.creature);
			addCreatureLocationRows(model.location);
			addCreatureVendorRows(model.vendor);
			addCreatureLootRows(model.loot);
			addCreaturePickpocketRows(model.pickpocket);
			addCreatureSkinRows(model.skin);
		}
		private void dataGridViewQuestSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
			var id = Convert.ToUInt32(dataGridViewQuestSearch.SelectedCells[0].Value);

			var model = Models.QuestModel.getInstance();

			model.updateFullQuestFromDatabase(id);

			fillQuestSections(model.quest);
			addQuestGiverRows(model.giver);
			addQuestTakerRows(model.taker);
		}
		private void dataGridViewGameObjectSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
			var entry = Convert.ToUInt32(dataGridViewGameObjectSearch.SelectedCells[0].Value);

			var model = Models.GameObjectModel.getInstance();

			model.updateGameObjectFromDatabase(entry);

			fillGameObjectTemplate(model.gameObject);
		}
		private void dataGridViewItemSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
			var entry = Convert.ToUInt32(dataGridViewItemSearch.SelectedCells[0].Value);

			var model = Models.ItemModel.getInstance();

			model.updateFullItemFromDatabase(entry);

			fillItemTemplate(model.item);
			addItemLootRows(model.loot);
			addItemProspectRows(model.prospect);
			addItemMillingRows(model.mill);
			addItemDisenchantRows(model.disenchant);
		}

		#endregion

		#region GenerateSql

		private void buttonAccountAccountGenerateScript_Click(object sender, EventArgs e) {
			if(!string.IsNullOrEmpty(textBoxAccountAccountID.Text)) {
				var account = updateAccount();
				var ga      = new GenerateAuth();

				textBoxAccountScriptOutput.AppendText(ga.accountFullToSQL(account));
				tabControlCategoryAccount.SelectedTab = tabPageAccountScript;
			}
		}
		private void buttonCharacterCharacterGenerate_Click(object sender, EventArgs e) {
			var c = updateCharacter();

			if(c.accountID != 0) {
				var gc = new GenerateCharacters();

				textBoxCharacterScriptOutput.AppendText(gc.CharacterToSql(c));

				tabControlCategoryCharacter.SelectedTab = tabPageCharacterScript;
			}
		}
		private void buttonCreatureTempGenerate_Click(object sender, EventArgs e) {
			if(!string.IsNullOrEmpty(textBoxCreatureTemplateEntry.Text)) {
				updateCreature();

				var model = Models.CreatureModel.getInstance();
				var gw = new GenerateWorld();

				textBoxCreatureScriptOutput.AppendText(gw.creatureToSql(model.creature));
				tabControlCategoryCreature.SelectedTab = tabPageCreatureScript;
			}
		}
		private void buttonQuestSectionGenerate_Click(object sender, EventArgs e) {
			if(!string.IsNullOrEmpty(textBoxQuestSectionID.Text)) {
				var quest = updateQuest();
				var gw = new GenerateWorld();

				textBoxQuestScriptOutput.AppendText(gw.questToSql(quest));
				tabControlCategoryQuest.SelectedTab = tabPageQuestScript;
			}
		}
		private void buttonGameObjectTempGenerate_Click(object sender, EventArgs e) {
			if(!string.IsNullOrEmpty(textBoxGameObjectTempEntry.Text)) {
				var go = updateGameObject();
				var gw = new GenerateWorld();

				textBoxGameObjectScriptOutput.AppendText(gw.gameObjectToSql(go));
				tabControlCategoryGameObject.SelectedTab = tabPageGameObjectScript;
			}
		}
		private void buttonItemTempGenerate_Click(object sender, EventArgs e) {
			if(!string.IsNullOrEmpty(textBoxItemTempEntry.Text)) {
				var item = updateItem();
				var gw = new GenerateWorld();

				textBoxItemScriptOutput.AppendText(gw.itemToSql(item));
				tabControlCategoryItem.SelectedTab = tabPageItemScript;
			}
		}

		#endregion

		#region GenerateSqlFile

		private void toolStripSplitButtonCreatureScriptSQLGenerate_ButtonClick(object sender, EventArgs e) {
			GenerateSQLFile("Creature_", textBoxCreatureTemplateEntry.Text + "-" + textBoxCreatureTemplateName.Text, textBoxCreatureScriptOutput);
		}
		private void toolStripSplitButtonQuestScriptSQLGenerate_ButtonClick(object sender, EventArgs e) {
			GenerateSQLFile("QUEST_", textBoxQuestSectionID.Text + "-" + textBoxQuestSearchTitle.Text, textBoxQuestScriptOutput);
		}
		private void toolStripSplitButtonGOScriptSQLGenerate_ButtonClick(object sender, EventArgs e) {
			GenerateSQLFile("GO_", textBoxCreatureTemplateEntry.Text + "-" + textBoxCreatureTemplateName.Text, textBoxCreatureScriptOutput);
		}
		private void toolStripSplitButtonItemScriptSQLGenerate_ButtonClick(object sender, EventArgs e) {
			GenerateSQLFile("ITEM_", textBoxItemTempEntry.Text.Trim() + "-" + textBoxItemTempName.Text.Trim(), textBoxItemScriptOutput);
		}

		#endregion

		#region Update

		private void toolStripSplitButtonUploadScriptTab(object sender, EventArgs e) {
			ToolStripSplitButton btn = (ToolStripSplitButton) sender;

			int rows = 0;
			ToolStripStatusLabel label = null;

			if(btn == toolStripSplitButtonAccountScriptUpdate) {
				DatabaseAuth da = Settings.getAuthDB();

				rows = da.uploadSql(textBoxAccountScriptOutput.Text);
				label = toolStripStatusLabelAccountScriptRows;
			} else if(btn == toolStripSplitButtonCharacterScriptUpdate) {
				DatabaseCharacters dw = Settings.getCharsDB();

				rows = dw.uploadSql(textBoxAccountScriptOutput.Text);
				label = toolStripStatusLabelAccountScriptRows;
			} else {
				DatabaseWorld dw = Settings.getWorldDB();

				if(btn == toolStripSplitButtonCreatureScriptUpdate) {
					rows = dw.uploadSql(textBoxCreatureScriptOutput.Text);
					label = toolStripStatusLabelCreatureScriptRows;

				} else if(btn == toolStripSplitButtonQuestScriptUpdate) {
					rows = dw.uploadSql(textBoxQuestScriptOutput.Text);
					label = toolStripStatusLabelQuestScriptRows;

				} else if(btn == toolStripSplitButtonGOScriptUpdate) {
					rows = dw.uploadSql(textBoxGameObjectScriptOutput.Text);
					label = toolStripStatusLabelGameObjectScriptRows;

				} else if(btn == toolStripSplitButtonItemScriptUpdate) {
					rows = dw.uploadSql(textBoxItemScriptOutput.Text);
					label = toolStripStatusLabelItemScriptRows;
				}
			}

			if(label != null) {
				label.Text = "Row(s) affected: " + rows.ToString();
			}
		}

		#endregion

		#endregion

		#region Secondary

		#region ToolStrip
		private void newConnectionToolStripMenuItemFile_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(Application.ExecutablePath);
			Application.Exit();
		}
		private void controlPanelToolStripMenuTools_Click(object sender, EventArgs e) {
			Form CP = new Views.FormControlPanel();

			CP.StartPosition = FormStartPosition.CenterScreen;
			CP.Show();
		}
		private void aboutToolStripMenuHelp_Click(object sender, EventArgs e) {
			var fa = new FormAbout();
			fa.ShowDialog();
		}
		#endregion

		#region Account

		// account tab
		private void monthCalendarAccountAccountBanDate_DateChanged(object sender, DateRangeEventArgs e) {
			textBoxAccountAccountBandate.Text = monthCalendarAccountAccountBanDate.SelectionStart.ToString();
		}
		private void monthCalendarAccountAccountUnbanDate_DateChanged(object sender, DateRangeEventArgs e) {
			textBoxAccountAccountUnbandate.Text = monthCalendarAccountAccountUnbanDate.SelectionStart.ToString();
		}
		private void monthCalendarAccountAccountMuteDate_DateChanged(object sender, DateRangeEventArgs e) {
			textBoxAccountAccountMutedate.Text = monthCalendarAccountAccountMuteDate.SelectionStart.ToString();

			if(!string.IsNullOrEmpty(textBoxAccountAccountMutedate.Text)) {
				DateTime unmuteDay = monthCalendarAccountAccountUnmuteDate.SelectionStart;
				DateTime muteDay = Convert.ToDateTime(textBoxAccountAccountMutedate.Text);

				double muteTime = (unmuteDay - muteDay).TotalMinutes >= 0 ? (unmuteDay - muteDay).TotalMinutes : 0;

				textBoxAccountAccountMutetime.Text = Convert.ToUInt64(muteTime).ToString();
			}
		}
		private void monthCalendarAccountAccountUnmuteDate_DateChanged(object sender, DateRangeEventArgs e) {
			if(!string.IsNullOrEmpty(textBoxAccountAccountMutedate.Text)) {
				DateTime unmuteDay = monthCalendarAccountAccountUnmuteDate.SelectionStart;
				DateTime muteDay = Convert.ToDateTime(textBoxAccountAccountMutedate.Text);

				double muteTime = (unmuteDay - muteDay).TotalMinutes >= 0 ? (unmuteDay - muteDay).TotalMinutes : 0;

				textBoxAccountAccountMutetime.Text = Convert.ToInt64(muteTime).ToString();
			}
		}

		private void buttonAccountAccountAccessAdd_Click(object sender, EventArgs e) {
			var id = textBoxAccountAccountID.Text;
			var gm = textBoxAccountAccountAccessGM.Text;
			var rid = textBoxAccountAccountAccessRID.Text;

			int noNeed;

			if(int.TryParse(id, out noNeed) && int.TryParse(gm, out noNeed) && int.TryParse(rid, out noNeed)) {
				dataGridViewAccountAccess.Rows.Add(id, gm, rid);
			}
		}
		private void buttonAccountAccountAccessDelete_Click(object sender, EventArgs e) {
			if(dataGridViewAccountAccess.SelectedRows.Count > 0) {
				foreach(DataGridViewRow row in dataGridViewAccountAccess.SelectedRows) {
					dataGridViewAccountAccess.Rows.RemoveAt(row.Index);
				}
			}
		}

		#endregion

		#region Character

		// inventory tab
		private void buttonCharacterInventoryAdd_Click(object sender, EventArgs e) {
			var guid   = textBoxCharacterInventoryGUID.Text;
			var bag    = textBoxCharacterInventoryBag.Text;
			var slot   = textBoxCharacterInventorySlot.Text;
			var itemId = textBoxCharacterInventoryItemID.Text;

			int iNoNeed;
			uint uiNoNeed;

			if(uint.TryParse(guid, out uiNoNeed) && int.TryParse(bag, out iNoNeed) && int.TryParse(slot, out iNoNeed) && uint.TryParse(itemId, out uiNoNeed)) {
				dataGridViewCharacterInventory.Rows.Add(guid, bag, slot, itemId);
			}
		}
		private void buttonCharacterInventoryRefresh_Click(object sender, EventArgs e) {
			var tGuid = textBoxCharacterCharacterGUID.Text; // template
			var iGuid = textBoxCharacterInventoryGUID.Text; // inventory

			uint entry;

			// checks the inventory textbox first, then templates, if both empty, return/stop
			if(!string.IsNullOrEmpty(iGuid)) { uint.TryParse(iGuid, out entry); }
			else if (!string.IsNullOrEmpty(tGuid)) { uint.TryParse(tGuid, out entry); }
			else { return; }

			if(entry > 0) {
				var model = Models.CharacterModel.getInstance();
				model.updateCharacterInventoryFromDatabase(entry);

				addCharacterInventory(model.inventory);
			}
		}
		private void buttonCharacterInventoryDelete_Click(object sender, EventArgs e) {
			if(dataGridViewCharacterInventory.SelectedRows.Count > 0) {
				foreach(DataGridViewRow row in dataGridViewCharacterInventory.SelectedRows) {
					dataGridViewCharacterInventory.Rows.RemoveAt(row.Index);
				}
			}
		}
		private void buttonCharacterInventoryGenerate_Click(object sender, EventArgs e) {
			var ci = updateCharacterInventory();

			if(ci.Length > 0) {
				textBoxCharacterScriptOutput.Text = new GenerateCharacters().CharacterInventoryToSql(ci[0].guid, ci);
			}
		}

		#endregion

		#region Creature

		// search tab
		private void toolStripSplitButtonCreatureNew_ButtonClick(object sender, EventArgs e) {
			var list = new List<Tuple<TextBox, string>>
			{
				Tuple.Create(textBoxCreatureTemplateName, ""),

				Tuple.Create(textBoxCreatureTemplateName, ""),
				Tuple.Create(textBoxCreatureTemplateSubname, ""),
				Tuple.Create(textBoxCreatureTemplateBaseAttack, "2000"),
				Tuple.Create(textBoxCreatureTemplateRangedAttack, "2000"),
				Tuple.Create(textBoxCreatureTemplateBV, "1"),
				Tuple.Create(textBoxCreatureTemplateRV, "1"),
				Tuple.Create(textBoxCreatureTemplateSpeedWalk, "1"),
				Tuple.Create(textBoxCreatureTemplateSpeedRun, "1.4286"),
				Tuple.Create(textBoxCreatureTemplateAIName, ""),
				Tuple.Create(textBoxCreatureTemplateScriptName, "")
			};

			DefaultValuesGenerate(tabPageCreatureTemplate);
			DefaultValuesOverride(list);

			checkBoxCreatureTemplateHR.Checked = true;

			tabControlCategoryCreature.SelectedTab = tabPageCreatureTemplate;
		}
		private void toolStripSplitButtonCreatureDelete_ButtonClick(object sender, EventArgs e) {
			GenerateDeleteSelectedRow(dataGridViewCreatureSearch, "creature_template", "entry", textBoxCreatureScriptOutput);
		}

		// vendor, loot, pickpocket and skin tab
		private void buttonCreatureDataGridAdd_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			DataGridView dgv = null;
			object[] values = { };

			if(btn == buttonCreatureVendorAdd) {
				values = new object[] {
					textBoxCreatureVendorEntry.Text,
					textBoxCreatureVendorSlot.Text,
					textBoxCreatureVendorItemID.Text,
					textBoxCreatureVendorMAC.Text,
					textBoxCreatureVendorIncrtime.Text,
					textBoxCreatureVendorEC.Text
				};

				dgv = dataGridViewCreatureVendor;
			} else if(btn == buttonCreatureLootAdd) {
				values = new object[] {
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

				dgv = dataGridViewCreatureLoot;

				dataGridViewCreatureLoot.Rows.Add(values);
			} else if(btn == buttonCreaturePickpocketAdd) {
				values = new object[] {
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

				dgv = dataGridViewCreaturePickpocketLoot;

				dataGridViewCreaturePickpocketLoot.Rows.Add(values);
			} else if(btn == buttonCreatureSkinAdd) {
				values = new object[] {
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

				dgv = dataGridViewCreatureSkinLoot;
			}
			
			if(dgv != null && values.Length > 0) {
				dgv.Rows.Add(values.ToString());
			}
		}
		private void buttonCreatureDataGridRefresh_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			uint entry = 0;

			var model = Models.CreatureModel.getInstance();

			if(btn == buttonCreatureVendorRefresh) {
				entry = uint.Parse(textBoxCreatureVendorEntry.Text);
				model.updateCreatureVendorFromDatabase(entry);

				addCreatureVendorRows(model.vendor);
			} else if(btn == buttonCreatureLootRefresh) {
				entry = uint.Parse(textBoxCreatureLootEntry.Text);
				model.updateCreatureLootFromDatabase(entry);

				addCreatureLootRows(model.loot);
			} else if(btn == buttonCreaturePickpocketRefresh) {
				entry = uint.Parse(textBoxCreaturePickpocketEntry.Text);
				model.updateCreaturePickpocketFromDatabase(entry);

				addCreaturePickpocketRows(model.pickpocket);
			} else if(btn == buttonCreatureSkinRefresh) {
				entry = uint.Parse(textBoxCreatureSkinEntry.Text);
				model.updateCreatureSkinFromDatabase(entry);

				addCreatureSkinRows(model.skin);
			}
		}
		private void buttonCreatureDataGridDelete_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			DataGridView dgv = null;

			if(btn == buttonCreatureVendorDelete) {
				dgv = dataGridViewCreatureVendor;
			} else if(btn == buttonCreatureLootDelete) {
				dgv = dataGridViewCreatureLoot;
			} else if(btn == buttonCreaturePickpocketDelete) {
				dgv = dataGridViewCreaturePickpocketLoot;
			} else if(btn == buttonCreatureSkinDelete) {
				dgv = dataGridViewCreatureSkinLoot;
			}

			if(dgv != null) {
				if(dgv.SelectedRows.Count > 0) {
					foreach(DataGridViewRow row in dgv.SelectedRows) {
						dgv.Rows.RemoveAt(row.Index);
					}
				}
			}
		}
		private void buttonCreatureDataGridGenerate_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			var model = Models.CreatureModel.getInstance();

			if(btn == buttonCreatureVendorGenerate) {
				updateCreatureVendor();
				var sql = new GenerateWorld().creatureVendorToSql(model.vendor);

				textBoxCreatureScriptOutput.AppendText(sql);
			} else if(btn == buttonCreatureLootGenerate) {
				updateCreatureLoot();
				var sql = new GenerateWorld().creatureLPSToSql("creature_loot_template", model.loot);

				textBoxCreatureScriptOutput.AppendText(sql);
			} else if(btn == buttonCreaturePickpocketGenerate) {
				updateCreaturePickpocket();
				var sql = new GenerateWorld().creatureLPSToSql("pickpocketing_loot_template", model.pickpocket);

				textBoxCreatureScriptOutput.AppendText(sql);
			} else if(btn == buttonCreatureSkinGenerate) {
				updateCreatureSkin();
				var sql = new GenerateWorld().creatureLPSToSql("skinning_loot_template", model.skin);

				textBoxCreatureScriptOutput.AppendText(sql);
			}

			tabControlCategoryCreature.SelectedTab = tabPageCreatureScript;
		}

		#endregion

		#region Quest

		// search tab
		private void toolStripSplitButtonQuestNew_ButtonClick(object sender, EventArgs e) {
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
		private void toolStripSplitButtonQuestDelete_ButtonClick(object sender, EventArgs e) {
			GenerateDeleteSelectedRow(dataGridViewQuestSearch, "quest_template", "ID", textBoxQuestScriptOutput);
		}

		#endregion

		#region Game Object

		// search tab
		private void toolStripSplitButtonGONew_ButtonClick(object sender, EventArgs e) {
			var list = new List<Tuple<TextBox, string>>();

			list.Add(new Tuple<TextBox, string>(textBoxGameObjectTempName, ""));
			list.Add(new Tuple<TextBox, string>(textBoxGameObjectTempSize, "1"));
			list.Add(new Tuple<TextBox, string>(textBoxGameObjectTempAIName, ""));
			list.Add(new Tuple<TextBox, string>(textBoxGameObjectTempScriptName, ""));

			DefaultValuesGenerate(tabPageGameObjectTemplate);
			DefaultValuesOverride(list);

			tabControlCategoryGameObject.SelectedTab = tabPageGameObjectTemplate;
		}
		private void toolStripSplitButtonGODelete_ButtonClick(object sender, EventArgs e) {
			GenerateDeleteSelectedRow(dataGridViewGameObjectSearch, "gameobject_template", "entry", textBoxGameObjectScriptOutput);
		}

		#endregion

		#region Item

		// search tab
		private void toolStripSplitButtonItemNew_ButtonClick(object sender, EventArgs e) {
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
		private void toolStripSplitButtonItemDelete_ButtonClick(object sender, EventArgs e) {
			GenerateDeleteSelectedRow(dataGridViewItemSearch, "item_template", "entry", textBoxItemScriptOutput);
		}

		private void buttonItemDataGridAdd_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			DataGridView dgv = null;
			object[] values = { };

			if(btn == buttonItemLootAdd) {
				values = new object[] {
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

				dgv = dataGridViewItemLoot;
			} else if (btn == buttonItemProspectAdd) {
				values = new object[] {
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

				dgv = dataGridViewItemProspect;
			} else if(btn == buttonItemMillAdd) {
				values = new object[] {
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

				dgv = dataGridViewItemMill;
			} else if(btn == buttonItemDEAdd) {
				values = new object[] {
					textBoxItemDEEntry.Text,
					textBoxItemDEItemID.Text,
					textBoxItemDEReference.Text,
					textBoxItemDEChance.Text,
					textBoxItemDEQR.Text,
					textBoxItemDELM.Text,
					textBoxItemDEGID.Text,
					textBoxItemDEMIC.Text,
					textBoxItemDEMAC.Text
				};

				dgv = dataGridViewItemDE;
			}

			if(dgv != null && values.Length > 0) {
				dgv.Rows.Add(values.ToString());
			}
		}
		private void buttonItemDataGridRefresh_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			uint entry;

			var model = Models.ItemModel.getInstance();

			if(btn == buttonItemLootRefresh) {
				entry = uint.Parse(textBoxItemLootEntry.Text);
				model.updateItemLootFromDatabase(entry);

				addItemLootRows(model.loot);
			} else if(btn == buttonItemProspectRefresh) {
				entry = uint.Parse(textBoxItemProspectEntry.Text);
				model.updateItemProspectFromDatabase(entry);

				addItemProspectRows(model.prospect);
			} else if(btn == buttonItemMillRefresh) {
				entry = uint.Parse(textBoxItemMillEntry.Text);
				model.updateItemMillFromDatabase(entry);

				addItemMillingRows(model.mill);
			} else if(btn == buttonItemDERefresh) {
				entry = uint.Parse(textBoxItemDEEntry.Text);
				model.updateItemDisenchantFromDatabase(entry);

				addItemDisenchantRows(model.disenchant);
			}
		}
		private void buttonItemDataGridDelete_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			DataGridView dgv = null;

			if(btn == buttonItemLootDelete) {
				dgv = dataGridViewItemLoot;
			} else if(btn == buttonItemProspectDelete) {
				dgv = dataGridViewItemProspect;
			} else if(btn == buttonItemMillDelete) {
				dgv = dataGridViewItemMill;
			} else if(btn == buttonItemDEDelete) {
				dgv = dataGridViewItemDE;
			}

			if(dgv != null) {
				if(dgv.SelectedRows.Count > 0) {
					foreach(DataGridViewRow row in dgv.SelectedRows) {
						dgv.Rows.RemoveAt(row.Index);
					}
				}
			}
		}
		private void buttonItemDataGridGenerate_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			var model = Models.ItemModel.getInstance();

			if(btn == buttonItemLootGenerate) {
				updateItemLoot();
				var sql = new GenerateWorld().itemLPMDToSql("item_loot_template", model.loot);

				textBoxItemScriptOutput.AppendText(sql);
			} else if(btn == buttonItemProspectGenerate) {
				updateItemProspect();
				var sql = new GenerateWorld().itemLPMDToSql("prospecting_loot_template", model.prospect);

				textBoxItemScriptOutput.AppendText(sql);
			} else if(btn == buttonItemMillGenerate) {
				updateItemMilling();
				var sql = new GenerateWorld().itemLPMDToSql("milling_loot_template", model.mill);

				textBoxItemScriptOutput.AppendText(sql);
			} else if(btn == buttonItemDEGenerate) {
				updateItemDisenchant();
				var sql = new GenerateWorld().itemLPMDToSql("disenchant_loot_template", model.disenchant);

				textBoxItemScriptOutput.AppendText(sql);
			}
		}

		#endregion

		#endregion

		// The things below is still a WIP.

		#region GLOBAL Functions
		private void setOfflineMode(bool enable) {
			FormMySQL.Offline = enable;

			// Search Buttons
			Button[] dButtons = new Button[]
			{
					buttonAccountSearchSearch,
					buttonCharacterSearchSearch,
					buttonCreatureSearchSearch,
					buttonQuestSearchSearch,
					buttonGameObjectSearchSearch,
					buttonItemSearchSearch
			};

			// Execute Buttons
			ToolStripSplitButton[] dStripButton = new ToolStripSplitButton[]
			{
					toolStripSplitButtonAccountScriptUpdate,
					toolStripSplitButtonCharacterScriptUpdate,
					toolStripSplitButtonCreatureScriptUpdate,
					toolStripSplitButtonQuestScriptUpdate,
					toolStripSplitButtonGOScriptUpdate,
					toolStripSplitButtonItemScriptUpdate
			};

			foreach(Button btn in dButtons) {
				btn.Enabled = !enable;
			}

			foreach(ToolStripSplitButton btn in dStripButton) {
				btn.Enabled = !enable;
			}
		}
		#region GlobalFunctions
		/// <summary>
		/// Function looks for a specific .CSV extension file and turns it into a DataTable (used for FormTools).
		/// </summary>
		/// <param name="csvName">The specific .CSV File</param>
		/// <param name="ID">What column the ID is</param>
		/// <param name="value">Where the value/name is in the CSV extension</param>
		/// <returns>DataTable with all the rows from the ID and Value</returns>
		private DataTable ReadExcelCSV(string csvName, int ID, int value, int value2 = 0) {
			var reader = new System.IO.StreamReader(@".\CSV\" + csvName + ".dbc.csv");
			var forgetFirst = true;

			var newTable = new DataTable();

			if(reader != null) {
				newTable.Columns.Add("id", typeof(string));
				newTable.Columns.Add("value", typeof(string));
				if(value2 != 0) { newTable.Columns.Add("value2", typeof(string)); }

				string line; string[] words;

				while((line = reader.ReadLine()) != null) {
					words = line.Split(';');

					if(forgetFirst == false) {
						if(words.Length > value && words[value] != null) {
							DataRow newRow = newTable.NewRow();

							// adds the id and value to the row
							newRow["id"] = words[ID].Trim('"'); newRow["value"] = words[value].Trim('"');
							// if value2 is above 0, add another column value
							if(value2 != 0) { newRow["value2"] = words[value2].Trim('"'); }

							newTable.Rows.Add(newRow);
						}
					}

					forgetFirst = false;
				}

				reader.Close();
			} else {
				MessageBox.Show(csvName + " Could not been found in the CSV folder.\n It has to be same location as the program.", "File Directory : CSV ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return newTable;
		}
		/// <summary>
		/// It filters, if the character '%' is in the beginning or end of the string (value).
		/// If it is, it turns the SQL query to LIKE (a sort of containing method for MySQL).
		/// </summary>
		/// <param name="value">What the user is searching for</param>
		/// <param name="columnName">Column name for the query</param>
		/// <returns>Returns either LIKE or equal to the search</returns>
		private string DatabaseQueryFilter(string value, string columnName) {
			if(value != string.Empty) {
				if(value.Trim().StartsWith("%", StringComparison.InvariantCultureIgnoreCase) || value.Trim().EndsWith("%", StringComparison.InvariantCultureIgnoreCase)) {
					value = " AND " + columnName + " LIKE '" + value + "'";
				} else {
					value = " AND " + columnName + " = '" + value + "'";
				}
			}

			return value;
		}
		/// <summary>
		/// Only used in search tabs. It checks if all the textboxes are empty.
		/// </summary>
		/// <param name="control">The selected control to check</param>
		/// <returns>Returns a boolean. Textboxes empty = true</returns>
		private bool CheckEmptyControls(Control control) {
			foreach(Control ct in control.Controls) {
				if(ct is TextBox || ct is ComboBox) {
					if(ct.Text != "") {
						return false;
					}
				}
			}

			return true;
		}
		/// <summary>
		/// Converts all the ColumnHeader to string header (holds strings).
		/// Required to add new rows.
		/// </summary>
		/// <param name="datatable">The specific table to convert</param>
		/// <returns>Returns the transformed DataTable with string headers</returns>
		private DataTable ConvertColumnsToString(DataTable datatable) {
			var newTable = datatable.Clone();

			for(var i = 0; i < newTable.Columns.Count; i++) {
				if(newTable.Columns[i].DataType != typeof(string)) {
					newTable.Columns[i].DataType = typeof(string);
				}
			}

			foreach(DataRow row in datatable.Rows) {
				newTable.ImportRow(row);
			}

			return newTable;
		}
		/// <summary>
		/// Creates a popup, where the user can select only one row.
		/// </summary>
		/// <param name="formTitle">Changes the popup title</param>
		/// <param name="data">This sends the data to the listview/datagrid used in the popup</param>
		/// <param name="currentValue">Highlights the current value</param>
		/// <returns>It returns the selected /= current value (the ID)</returns>
		private string CreatePopupSelection(string formTitle, DataTable data, string currentValue) {
			var popupDialog = new FormPopup.FormPopupSelection();

			popupDialog.setFormTitle = formTitle;
			popupDialog.changeSelection = currentValue;
			popupDialog.setDataTable = data;
			popupDialog.Owner = this;
			popupDialog.ShowDialog();

			currentValue = (popupDialog.changeSelection == "") ? currentValue : popupDialog.changeSelection;
			popupDialog.Dispose();

			GC.Collect();
			this.Activate();
			return currentValue;
		}
		/// <summary>
		/// Same as the Selection popup, except it has checkboxes (multiple selections).
		/// </summary>
		/// <param name="formTitle">Changes the popup title</param>
		/// <param name="data">This sends the data to the listview/datagrid used in the popup</param>
		/// <param name="currentValue">Highlights the current value</param>
		/// <param name="bitMask">If the data is 2^n based (1, 2, 4, 8, 16 so on)</param>
		/// <returns>It returns the selected /= current value (the ID)</returns>
		private string CreatePopupChecklist(string formTitle, DataTable data, string currentValue, bool bitMask = false) {
			var popupDialog = new FormPopup.FormPopupCheckboxList();

			popupDialog.setFormTitle = formTitle;
			popupDialog.setDataTable = data;
			popupDialog.usedValue = (currentValue == string.Empty) ? "0" : currentValue;
			popupDialog.setBitmask = bitMask;
			popupDialog.Owner = this;
			popupDialog.ShowDialog();

			currentValue = (popupDialog.usedValue.ToString() == "") ? currentValue : popupDialog.usedValue.ToString();
			popupDialog.Dispose();

			GC.Collect();

			this.Activate();
			return currentValue;
		}
		/// <summary>
		/// Similar to selection and checklist popup, however, is it used for entities (items, creatures & gameobjects)
		/// </summary>
		/// <param name="currentValue">Highlights the current value</param>
		/// <param name="disableEntity">Used to disable or enable radiobuttons {items, creatures, gameobjects} in that order</param>
		/// <returns>It returns the selected /= current value (the ID)</returns>
		private string CreatePopupEntity(string currentValue, bool[] disableEntity, bool outputID = true) {
			var popupDialog = new FormPopup.FormPopupEntities();
			DataSet entities = new DataSet();

			popupDialog.changeSelection = (currentValue == "") ? "0" : currentValue;
			popupDialog.changeOutput = outputID;
			popupDialog.disableEntity = disableEntity;
			popupDialog.Owner = this;

			if(FormMySQL.Offline) {
				// Popup Entity follows order: 0: ID, 1: displayID, 2: Name
				entities.Tables.Add(ReadExcelCSV("ItemTemplate", 0, 2, 1));
				entities.Tables.Add(ReadExcelCSV("CreatureTemplate", 0, 1, 2));
				entities.Tables.Add(ReadExcelCSV("GameObjectTemplate", 0, 1, 2));
			} else {
				var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

				if(ConnectionOpen(connect)) {
					string query = "";
					query += "SELECT entry, displayid, name FROM item_template ORDER BY entry ASC;";
					query += "SELECT entry, modelid1, name FROM creature_template ORDER BY entry ASC;";
					query += "SELECT entry, displayId, name FROM gameobject_template ORDER BY entry ASC;";

					entities = DatabaseSearch(connect, query);

					ConnectionClose(connect);
				}
			}

			popupDialog.setEntityTable = entities;
			popupDialog.ShowDialog();

			currentValue = (popupDialog.changeSelection == "") ? currentValue : popupDialog.changeSelection;

			entities.Dispose();
			popupDialog.Dispose();

			GC.Collect();

			this.Activate();
			return currentValue;
		}
		/// <summary>
		/// Generates SQL text used to execute. Is used in loot tables (see creature -> loot as a reference).
		/// This might change to string return instead.
		/// </summary>
		/// <param name="table">What database it generates for</param>
		/// <param name="dataGrid">The grid it has to loop through</param>
		/// <param name="output">The textbox it has to 'add' to</param>
		private void GenerateLootSQL(string table, DataGridView dataGrid, TextBox output) {
			string query = "DELETE FROM `" + table + "` WHERE entry = '" + dataGrid.Rows[0].Cells[0].Value.ToString() + "';";

			foreach(DataGridViewRow row in dataGrid.Rows) {
				if(row.Cells[0].Value.ToString() != "") {
					query += Environment.NewLine + "INSERT INTO `" + table + "` VALUES (";

					foreach(DataGridViewCell cell in row.Cells) {
						if(cell.OwningColumn.DataPropertyName != "name") {
							query += cell.Value.ToString() + ", ";
						}
					}

					query += "0);";


				}
			}

			output.AppendText(query);
		}
		/// <summary>
		/// Generates a SQL File and saves it in the SQL folder.
		/// </summary>
		/// <param name="fileStart">The beginning of the fileName</param>
		/// <param name="fileName">FileName after fileStart (usually entry & name)</param>
		/// <param name="tb">The textbox to create from (text)</param>
		private void GenerateSQLFile(string fileStart, string fileName, TextBox tb) {
			// Save location / path
			string path = @".\SQL\" + fileStart + fileName + ".SQL";

			// Checks if the path file exists
			if(File.Exists(path)) {
				// Creates a messagebox with a warning
				DialogResult dr = MessageBox.Show("File already exists.\n Replace it?", "Warning ...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

				// If the feedback is no, stop the program from running
				if(dr == DialogResult.No) {
					return;
				}
			} else {
				DialogResult dr = MessageBox.Show("SQL folder does not exist. \nAutomatically create one for you?", "The folder 'SQL' could not been found.", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

				if(dr == DialogResult.Yes) {
					Directory.CreateDirectory(@".\SQL\");
				} else {
					return;
				}
			}

			// Checks if textbox is empty OR fileName is empty
			if(tb.TextLength == 0 || fileName == string.Empty) {
				return;
			}

			// StreamWriter is used to write the SQL.
			StreamWriter sw = new StreamWriter(path);

			// Puts every line of the selected textbox in an array.
			int lineCount = tb.GetLineFromCharIndex(tb.Text.Length) + 1;

			for(var i = 0; i < lineCount; i++) {
				int startIndex = tb.GetFirstCharIndexFromLine(i);

				int endIndex = (i < lineCount - 1) ?
					tb.GetFirstCharIndexFromLine(i + 1) : tb.Text.Length;

				sw.WriteLine(tb.Text.Substring(startIndex, endIndex - startIndex));
			}

			// Closes the StreamWriter.
			sw.Close();

		}
		/// <summary>
		/// Sets all textboxes in a control to '0'.
		/// Mostly/only used on 'new button' in most tabs.
		/// </summary>
		/// <param name="parent">The beginning (usually a tabpage)</param>
		private void DefaultValuesGenerate(Control parent) {
			foreach(Control child in parent.Controls) {
				if(child is TextBox) {
					child.Text = "0";
				} else {
					DefaultValuesGenerate(child);
				}
			}
		}
		/// <summary>
		/// Overrides the selected database with a value, if textbox don't need a '0'.
		/// An example could be item name, it needs to be empty.
		/// </summary>
		/// <param name="exclude">A tuple with two variables, the textbox to target and replacement string.</param>
		private void DefaultValuesOverride(List<Tuple<TextBox, string>> exclude) {
			foreach(var data in exclude) {
				data.Item1.Text = data.Item2.ToString();
			}
		}
		/// <summary>
		/// The function name says it all. Takes the selected/selections of a datagrid and creates a deleting SQL(s) to execute.
		/// </summary>
		/// <param name="gv">The tageting DataGridView</param>
		/// <param name="sqlTable">What table is it targeting in the database</param>
		/// <param name="uniqueIndex">The specific row to delete</param>
		/// <param name="output">The textbox to output the SQL</param>
		private void GenerateDeleteSelectedRow(DataGridView gv, string sqlTable, string uniqueIndex, TextBox output) {
			if(gv.SelectedRows.Count > 0) {
				foreach(DataGridViewRow gvR in gv.SelectedRows) {
					output.AppendText("DELETE FROM `" + sqlTable + "` WHERE `" + uniqueIndex + "` = '" + gvR.Cells[0].Value.ToString() + "';");
				}
			}
		}
		/// <summary>
		/// Converts an unix stamp to a datatime used for calenders and readable.
		/// </summary>
		/// <param name="unixStamp">The unixstamp to convert</param>
		/// <returns>Datatime based on unix stamp</returns>
		private DateTime UnixStampToDateTime(double unixStamp) {
			var DateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			DateTime = DateTime.AddSeconds(unixStamp).ToLocalTime();

			return DateTime;
		}
		/// <summary>
		/// A reverse of the UnixStampToDateTime function.
		/// </summary>
		/// <param name="dateTime">A datatime to convert</param>
		/// <returns>Outputs an unixstamp based on the datatime</returns>
		private double DateTimeToUnixStamp(DateTime dateTime) {
			return (TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(1970, 1, 1)).TotalSeconds;
		}
		#endregion
		#region DatabaseFunctions

		// Generates the string required to create a connection
		private static string DatabaseString(string database) {
			var builder = new MySqlConnectionStringBuilder();

			builder.Server = FormMySQL.Address;
			builder.UserID = FormMySQL.Username;
			builder.Password = FormMySQL.Password;
			builder.Port = FormMySQL.Port;
			builder.Database = database;

			return builder.ToString();
		}
		// Tries to open the connection between the program and database.
		private bool ConnectionOpen(MySqlConnection connect) {
			try {
				connect.Open();
				return true;
			} catch(MySqlException) {
				return false;
				throw;
			}
		}
		// Tries to close the connection between the program and database.
		private bool ConnectionClose(MySqlConnection connect) {
			try {
				connect.Close();
				return true;
			} catch(MySqlException ex) {
				MessageBox.Show(ex.Message, "MySQL Error: " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return false;
		}
		// Searching the database with a specific query, then saves in a DataSet.
		private DataSet DatabaseSearch(MySqlConnection connect, string sqlQuery) {
			var ds = new DataSet();

			if(connect.State == ConnectionState.Open) {
				var da = new MySqlDataAdapter(sqlQuery, connect);

				da.Fill(ds);
			}

			return ds;
		}
		// Updates the database with a specific query and returns the row affected.
		private int DatabaseUpdate(MySqlConnection connect, string sqlQuery) {
			if(connect.State == ConnectionState.Open && sqlQuery != "") {
				var query = new MySqlCommand(sqlQuery, connect);
				return query.ExecuteNonQuery();
			}

			return 0;
		}
		// Get all rows from a search, searches for items names. if itemid is false, it tries for item identifier.
		private DataTable DatabaseItemNameColumn(string table, string where, string id, int itemColumn, bool isItemID) {
			var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

			var searchTable = new DataTable();

			if(ConnectionOpen(connect)) {
				// Create the query depending on the paramenters
				string query = "SELECT * FROM " + table + " WHERE " + where + " = '" + id + "';";

				// Searches in mySQL.
				var datatable = DatabaseSearch(connect, query);

				// Sets all the columns to string.
				searchTable = ConvertColumnsToString(datatable.Tables[0]);

				if(searchTable.Rows.Count != 0) {
					// Adds a new column to the existing one, called 'name'.
					searchTable.Columns.Add("name", typeof(string));

					// Loops through all rows
					for(int i = 0; i < searchTable.Rows.Count; i++) {

						if(isItemID) {
							searchTable.Rows[i]["name"] = DatabaseItemGetName(Convert.ToUInt32(searchTable.Rows[i][itemColumn]));
						} else {
							searchTable.Rows[i]["name"] = DatabaseItemGetName(DatabaseItemGetEntry(Convert.ToUInt32(searchTable.Rows[i][itemColumn])));
						}

					}
				}

				ConnectionClose(connect);
			}

			return searchTable;
		}
		// Gets the item entry with global item identifier.
		private uint DatabaseItemGetEntry(uint itemIdentifier) {
			var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseCharacters));

			if(ConnectionOpen(connect)) {
				// Get the ItemID
				string instanceQuery = "SELECT itemEntry FROM item_instance WHERE guid = '" + itemIdentifier + "';";

				// Item_instance Table
				DataSet iiTable = DatabaseSearch(connect, instanceQuery);

				if(iiTable.Tables[0].Rows.Count != 0) { return Convert.ToUInt32(iiTable.Tables[0].Rows[0][0]); }
			}

			ConnectionClose(connect);
			return 0;
		}
		// Gets the item name from item entry.
		private string DatabaseItemGetName(uint itemEntry) {

			var connect = new MySqlConnection(DatabaseString(FormMySQL.DatabaseWorld));

			if(ConnectionOpen(connect)) {

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
		private DataTable DataItemClass() {
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
		private DataTable DataItemSubclass(string classID) {
			var iSubclass = new DataTable();

			iSubclass.Columns.Add("id", typeof(string));
			iSubclass.Columns.Add("name", typeof(string));

			switch(classID) {
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
		#endregion

		#region POPUPS
		private void buttonCharacterCharacterRace_Click(object sender, EventArgs e) {
			textBoxCharacterCharacterRace.Text = CreatePopupSelection("Character Race", ReadExcelCSV("ChrRaces", 0, 14), textBoxCharacterCharacterRace.Text);
		}
		private void buttonCharacterCharacterClass_Click(object sender, EventArgs e) {
			textBoxCharacterCharacterClass.Text = CreatePopupSelection("Character Class", ReadExcelCSV("ChrClasses", 0, 4), textBoxCharacterCharacterClass.Text);
		}
		#endregion

		#region POPUPS
		private void buttonCreatureTemplateModelID1_Click(object sender, EventArgs e) {
			bool[] rButtons = { false, true, false };

			textBoxCreatureTemplateModelID1.Text = CreatePopupEntity(textBoxCreatureTemplateModelID1.Text, rButtons, false);
		}
		private void buttonCreatureTemplateRank_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateRank.Text = CreatePopupSelection("Creature Rank", ReadExcelCSV("CreatureRanks", 0, 1), textBoxCreatureTemplateRank.Text);
		}
		private void buttonCreatureTemplateNPCFlags_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateNPCFlags.Text = CreatePopupChecklist("Creature NPC Flags", ReadExcelCSV("CreatureNPCFlags", 0, 1), textBoxCreatureTemplateNPCFlags.Text, true);
		}
		private void buttonCreatureTemplateSpell1_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateSpell1.Text = CreatePopupSelection("Spells I", ReadExcelCSV("Spells", 0, 1), textBoxCreatureTemplateSpell1.Text);
		}
		private void buttonCreatureTemplateDS_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateDS.Text = CreatePopupSelection("Damage School (Type)", ReadExcelCSV("CreatureDmgSchool", 0, 1), textBoxCreatureTemplateDS.Text);
		}
		private void buttonCreatureTemplateMType_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateMType.Text = CreatePopupSelection("Movement Type", ReadExcelCSV("CreatureMovementType", 0, 1), textBoxCreatureTemplateMType.Text);
		}
		private void buttonCreatureTemplateInhabitType_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateInhabitType.Text = CreatePopupChecklist("Inhabit Types", ReadExcelCSV("CreatureInhabitTypes", 0, 1), textBoxCreatureTemplateInhabitType.Text, true); ;
		}
		private void buttonCreatureTemplateMechanic_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateMechanic.Text = CreatePopupChecklist("Creature's Immunity", ReadExcelCSV("CreatureMechanic", 0, 1), textBoxCreatureTemplateMechanic.Text, true);
		}
		private void buttonCreatureTemplateFamily_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateFamily.Text = CreatePopupSelection("Creature's Family", ReadExcelCSV("CreatureFamily", 0, 1), textBoxCreatureTemplateFamily.Text);
		}
		private void buttonCreatureTemplateType_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateType.Text = CreatePopupSelection("Creature's Type", ReadExcelCSV("CreatureFamilyType", 0, 1), textBoxCreatureTemplateType.Text);
		}
		private void buttonCreatureTemplateTypeFlags_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateTypeFlags.Text = CreatePopupChecklist("Unit Flags I", ReadExcelCSV("CreatureTypeFlags", 0, 1), textBoxCreatureTemplateTypeFlags.Text, true);
		}
		private void buttonCreatureTemplateFlagsExtra_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateFlagsExtra.Text = CreatePopupChecklist("Extra Flags", ReadExcelCSV("CreatureFlagsExtra", 0, 1), textBoxCreatureTemplateFlagsExtra.Text, true);
		}
		private void buttonCreatureTemplateUnitClass_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateUnitClass.Text = CreatePopupSelection("Creature's Class", ReadExcelCSV("CreatureUnitClass", 0, 1), textBoxCreatureTemplateUnitClass.Text);
		}
		private void buttonCreatureTemplateUnitflags_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateUnitflags.Text = CreatePopupChecklist("Unit Flags I", ReadExcelCSV("CreatureUnitFlags", 0, 1), textBoxCreatureTemplateUnitflags.Text, true);
		}
		private void buttonCreatureTemplateUnitflags2_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateUnitflags2.Text = CreatePopupChecklist("Unit Flags II", ReadExcelCSV("CreatureUnitFlags2", 0, 1), textBoxCreatureTemplateUnitflags2.Text, true);
		}
		private void buttonCreatureTemplateDynamic_Click(object sender, EventArgs e) {
			textBoxCreatureTemplateDynamic.Text = CreatePopupChecklist("Dynamic Flags", ReadExcelCSV("CreatureDynamicFlags", 0, 1), textBoxCreatureTemplateDynamic.Text, true);
		}
		#endregion

		#region POPUPS
		private void buttonQuestSearchInfo_Click(object sender, EventArgs e) {
			textBoxQuestSearchInfo.Text = CreatePopupSelection("Quest Info", ReadExcelCSV("QuestInfo", 0, 1), textBoxQuestSearchInfo.Text);
		}
		// Section 1
		private void buttonQuestSectionSourceItemID_Click(object sender, EventArgs e) {
			bool[] rButton = { true, false, false };

			textBoxQuestSectionSourceItemID.Text = CreatePopupEntity(textBoxQuestSectionSourceItemID.Text, rButton);
		}
		private void buttonQuestSectionReqRace_Click(object sender, EventArgs e) {
			textBoxQuestSectionReqRace.Text = CreatePopupChecklist("Requirement: Races", ReadExcelCSV("ChrRaces", 0, 14), textBoxQuestSectionReqRace.Text, true);
		}
		private void buttonQuestSectionReqClass_Click(object sender, EventArgs e) {
			textBoxQuestSectionReqClass.Text = CreatePopupChecklist("Requirement: Classes", ReadExcelCSV("ChrClasses", 0, 4), textBoxQuestSectionReqClass.Text, true);
		}
		private void buttonQuestSectionQSort_Click(object sender, EventArgs e) {
			if(radioButtonQuestSectionZID.Checked) {
				textBoxQuestSectionReqQSort.Text = CreatePopupSelection("Zone ID Selection", ReadExcelCSV("AreaTable", 0, 11), textBoxQuestSectionReqQSort.Text);
			} else {
				string newValue = CreatePopupSelection("Quest Sort Selection", ReadExcelCSV("QuestSort", 0, 1), textBoxQuestSectionReqQSort.Text.Trim('-'));

				textBoxQuestSectionReqQSort.Text = (textBoxQuestSectionReqQSort.Text == newValue || newValue == "0") ? textBoxQuestSectionReqQSort.Text : "-" + newValue;
			}
		}
		private void buttonQuestSectionReqFaction1_Click(object sender, EventArgs e) {
			textBoxQuestSectionReqFaction1.Text = CreatePopupSelection("Objective Faction ID I", ReadExcelCSV("Faction", 0, 23), textBoxQuestSectionReqFaction1.Text);
		}
		private void buttonQuestSectionReqFaction2_Click(object sender, EventArgs e) {
			textBoxQuestSectionReqFaction2.Text = CreatePopupSelection("Objective Faction ID II", ReadExcelCSV("Faction", 0, 23), textBoxQuestSectionReqFaction2.Text);
		}
		private void buttonQuestSectionReqMinRepF_Click(object sender, EventArgs e) {
			textBoxQuestSectionReqMinRepF.Text = CreatePopupSelection("Minimum Reputation Faction", ReadExcelCSV("Faction", 0, 23), textBoxQuestSectionReqMinRepF.Text);
		}
		private void buttonQuestSectionReqMaxRepF_Click(object sender, EventArgs e) {
			textBoxQuestSectionReqMaxRepF.Text = CreatePopupSelection("Maximum Reputation Faction", ReadExcelCSV("Faction", 0, 23), textBoxQuestSectionReqMaxRepF.Text);
		}
		private void buttonQuestSectionReqSkillID_Click(object sender, EventArgs e) {
			textBoxQuestSectionReqSkillID.Text = CreatePopupSelection("Required Skill ID", ReadExcelCSV("SkillLine", 0, 3), textBoxQuestSectionReqSkillID.Text);
		}
		private void buttonQuestSectionQuestType_Click(object sender, EventArgs e) {
			textBoxQuestSectionQuestType.Text = CreatePopupSelection("Quest Type", ReadExcelCSV("QuestType", 0, 1), textBoxQuestSectionQuestType.Text);
		}
		private void buttonQuestSectionQuestFlags_Click(object sender, EventArgs e) {
			textBoxQuestSectionQuestFlags.Text = CreatePopupChecklist("Quest : Flags", ReadExcelCSV("QuestFlags", 0, 1), textBoxQuestSectionQuestFlags.Text, true);
		}
		private void buttonQuestSectionOtherSF_Click(object sender, EventArgs e) {
			textBoxQuestSectionOtherSF.Text = CreatePopupChecklist("Quest : Special Flags", ReadExcelCSV("QuestSpecialFlags", 0, 1), textBoxQuestSectionOtherSF.Text, true);
		}
		private void buttonQuestSectionQuestInfo_Click(object sender, EventArgs e) {
			textBoxQuestSectionQuestInfo.Text = CreatePopupSelection("Quest Info", ReadExcelCSV("QuestInfo", 0, 1), textBoxQuestSectionQuestInfo.Text);
		}
		private void buttonQuestSectionSourceSpellID_Click(object sender, EventArgs e) {
			textBoxQuestSectionSourceSpellID.Text = CreatePopupSelection("Spells", ReadExcelCSV("Spells", 0, 1), textBoxQuestSectionSourceSpellID.Text);
		}
		// Section 2
		private void buttonQuestSectionReqNPCID1_Click(object sender, EventArgs e) {
			bool[] rButton = { false, true, false };

			textBoxQuestSectionReqNPCID1.Text = CreatePopupEntity(textBoxQuestSectionReqNPCID1.Text, rButton);
		}
		private void buttonQuestSectionReqItemID1_Click(object sender, EventArgs e) {
			bool[] rButton = { true, false, false };

			textBoxQuestSectionReqItemID1.Text = CreatePopupEntity(textBoxQuestSectionReqItemID1.Text, rButton);
		}
		private void buttonQuestSectionRewChoiceID1_Click(object sender, EventArgs e) {
			bool[] rButton = { true, false, false };

			textBoxQuestSectionRewChoiceID1.Text = CreatePopupEntity(textBoxQuestSectionRewChoiceID1.Text, rButton);
		}
		private void buttonQuestSectionRewItemID1_Click(object sender, EventArgs e) {
			bool[] rButton = { true, false, false };

			textBoxQuestSectionRewItemID1.Text = CreatePopupEntity(textBoxQuestSectionRewItemID1.Text, rButton);
		}
		private void buttonQuestSectionRewFactionID1_Click(object sender, EventArgs e) {
			textBoxQuestSectionRewFactionID1.Text = CreatePopupSelection("Faction Selection", ReadExcelCSV("Faction", 0, 23), textBoxQuestSectionRewFactionID1.Text);
		}
		private void buttonQuestSectionRewOtherTitleID_Click(object sender, EventArgs e) {
			textBoxQuestSectionRewOtherTitleID.Text = CreatePopupSelection("Title Selection", ReadExcelCSV("CharTitles", 0, 2), textBoxQuestSectionRewOtherTitleID.Text);
		}
		private void buttonQuestSectionRewSpell_Click(object sender, EventArgs e) {
			textBoxQuestSectionRewSpell.Text = CreatePopupSelection("Spell Selection", ReadExcelCSV("Spells", 0, 1), textBoxQuestSectionRewSpell.Text);
		}
		#endregion

		#region POPUPS
		private void buttonGameObjectTempType_Click(object sender, EventArgs e) {
			textBoxGameObjectTempType.Text = CreatePopupSelection("Game Object Type Selection", ReadExcelCSV("GameObjectTypes", 0, 1), textBoxGameObjectTempType.Text);
		}
		private void buttonGameObjectTempFlags_Click(object sender, EventArgs e) {
			textBoxGameObjectTempFlags.Text = CreatePopupChecklist("Game Object Flags Selection", ReadExcelCSV("GameObjectFlags", 0, 1), textBoxGameObjectTempFlags.Text, true);
		}
		#endregion

		#region POPUPS
		// Search
		private void buttonItemSearchClass_Click(object sender, EventArgs e) {
			textBoxItemSearchClass.Text = CreatePopupSelection("Class Selection", DataItemClass(), textBoxItemSearchClass.Text);
		}
		private void buttonItemSearchSubclass_Click(object sender, EventArgs e) {
			textBoxItemSearchSubclass.Text = CreatePopupSelection("Subclass Selection", DataItemSubclass(textBoxItemSearchClass.Text.Trim()), textBoxItemSearchSubclass.Text);
		}
		// Template
		private void buttonItemTempTypeClass_Click(object sender, EventArgs e) {
			textBoxItemTempTypeClass.Text = CreatePopupSelection("Class Selection", DataItemClass(), textBoxItemTempTypeClass.Text);
		}
		private void buttonItemTempSubclass_Click(object sender, EventArgs e) {
			textBoxItemTempSubclass.Text = CreatePopupSelection("Subclass Selection", DataItemSubclass(textBoxItemTempTypeClass.Text.Trim()), textBoxItemTempSubclass.Text);
		}
		private void buttonItemTempDisplayID_Click(object sender, EventArgs e) {
			bool[] rButtons = { true, false, false };

			textBoxItemTempDisplayID.Text = CreatePopupEntity(textBoxItemTempDisplayID.Text, rButtons, false);
		}
		private void buttonItemTempQuality_Click(object sender, EventArgs e) {
			textBoxItemTempQuality.Text = CreatePopupSelection("Quality Selection", ReadExcelCSV("ItemQuality", 0, 1), textBoxItemTempQuality.Text);
		}
		private void buttonItemTempFlags_Click(object sender, EventArgs e) {
			textBoxItemTempFlags.Text = CreatePopupChecklist("Flags", ReadExcelCSV("ItemFlags", 0, 1), textBoxItemTempFlags.Text, true);
		}
		private void buttonItemTempEFlags_Click(object sender, EventArgs e) {
			textBoxItemTempEFlags.Text = CreatePopupChecklist("Extra Flags", ReadExcelCSV("ItemFlagsExtra", 0, 1), textBoxItemTempEFlags.Text, true);
		}
		private void buttonItemTempDmgType1_Click(object sender, EventArgs e) {
			textBoxItemTempDmgType1.Text = CreatePopupSelection("Damage Type I Selection", ReadExcelCSV("ItemDamageTypes", 0, 1), textBoxItemTempDmgType1.Text);
		}
		private void buttonItemTempDmgType2_Click(object sender, EventArgs e) {
			textBoxItemTempDmgType2.Text = CreatePopupSelection("Damage Type II Selection", ReadExcelCSV("ItemDamageTypes", 0, 1), textBoxItemTempDmgType2.Text);
		}
		private void buttonItemTempAmmoType_Click(object sender, EventArgs e) {
			textBoxItemTempAmmoType.Text = CreatePopupSelection("Ammo Types", ReadExcelCSV("ItemAmmoType", 0, 1), textBoxItemTempAmmoType.Text);
		}
		private void buttonItemTempItemSet_Click(object sender, EventArgs e) {
			textBoxItemTempItemSet.Text = CreatePopupSelection("ItemSet Selection", ReadExcelCSV("ItemSet", 0, 1), textBoxItemTempItemSet.Text);
		}
		private void buttonItemTempBonding_Click(object sender, EventArgs e) {
			textBoxItemTempBonding.Text = CreatePopupSelection("Bonding Selection", ReadExcelCSV("ItemBondings", 0, 1), textBoxItemTempBonding.Text);
		}
		private void buttonItemTempSheath_Click(object sender, EventArgs e) {
			textBoxItemTempSheath.Text = CreatePopupSelection("Sheath Selection", ReadExcelCSV("ItemSheaths", 0, 1), textBoxItemTempSheath.Text);
		}
		private void buttonItemTempColor1_Click(object sender, EventArgs e) {
			textBoxItemTempColor1.Text = CreatePopupSelection("Color Selection I", ReadExcelCSV("ItemSocketColors", 0, 1), textBoxItemTempColor1.Text);
		}
		private void buttonItemTempColor2_Click(object sender, EventArgs e) {
			textBoxItemTempColor2.Text = CreatePopupSelection("Color Selection II", ReadExcelCSV("ItemSocketColors", 0, 1), textBoxItemTempColor2.Text);
		}
		private void buttonItemTempColor3_Click(object sender, EventArgs e) {
			textBoxItemTempColor3.Text = CreatePopupSelection("Color Selection III", ReadExcelCSV("ItemSocketColors", 0, 1), textBoxItemTempColor3.Text);
		}
		private void buttonItemTempSocketBonus_Click(object sender, EventArgs e) {
			textBoxItemTempSocketBonus.Text = CreatePopupSelection("Socket Bonus Selection III", ReadExcelCSV("ItemSocketBonus", 0, 1), textBoxItemTempSocketBonus.Text);
		}
		private void buttonItemTempStatsType1_Click(object sender, EventArgs e) {
			textBoxItemTempStatsType1.Text = CreatePopupSelection("Stat Selection I", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType1.Text);
		}
		private void buttonItemTempStatsType2_Click(object sender, EventArgs e) {
			textBoxItemTempStatsType2.Text = CreatePopupSelection("Stat Selection II", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType2.Text);
		}
		private void buttonItemTempStatsType3_Click(object sender, EventArgs e) {
			CreatePopupSelection("Stat Selection III", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType3.Text);
		}
		private void buttonItemTempStatsType4_Click(object sender, EventArgs e) {
			CreatePopupSelection("Stat Selection IV", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType4.Text);
		}
		private void buttonItemTempStatsType5_Click(object sender, EventArgs e) {
			textBoxItemTempStatsType5.Text = CreatePopupSelection("Stat Selection V", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType5.Text);
		}
		private void buttonItemTempStatsType6_Click(object sender, EventArgs e) {
			textBoxItemTempStatsType6.Text = CreatePopupSelection("Stat Selection VI", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType6.Text);
		}
		private void buttonItemTempStatsType7_Click(object sender, EventArgs e) {
			textBoxItemTempStatsType7.Text = CreatePopupSelection("Stat Selection VII", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType7.Text);
		}
		private void buttonItemTempStatsType8_Click(object sender, EventArgs e) {
			textBoxItemTempStatsType8.Text = CreatePopupSelection("Stat Selection VIII", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType8.Text);
		}
		private void buttonItemTempStatsType9_Click(object sender, EventArgs e) {
			textBoxItemTempStatsType9.Text = CreatePopupSelection("Stat Selection IX", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType9.Text);
		}
		private void buttonItemTempStatsType10_Click(object sender, EventArgs e) {
			textBoxItemTempStatsType10.Text = CreatePopupSelection("Stat Selection X", ReadExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType10.Text);
		}
		private void buttonItemTempSpellID1_Click(object sender, EventArgs e) {
			textBoxItemTempSpellID1.Text = CreatePopupSelection("Required Spell", ReadExcelCSV("Spells", 0, 1), textBoxItemTempSpellID1.Text);
		}
		private void buttonItemTempTrigger1_Click(object sender, EventArgs e) {
			textBoxItemTempTrigger1.Text = CreatePopupSelection("Spell Trigger", ReadExcelCSV("ItemSpellTrigger", 0, 1), textBoxItemTempTrigger1.Text);
		}
		private void buttonItemTempReqRace_Click(object sender, EventArgs e) {
			textBoxItemTempReqRace.Text = CreatePopupChecklist("Race Requirement", ReadExcelCSV("ChrRaces", 0, 14), textBoxItemTempReqRace.Text, true);
		}
		private void buttonItemTempReqClass_Click(object sender, EventArgs e) {
			textBoxItemTempReqClass.Text = CreatePopupChecklist("Class Requirement", ReadExcelCSV("ChrClasses", 0, 4), textBoxItemTempReqClass.Text, true);
		}
		private void buttonItemTempReqSkill_Click(object sender, EventArgs e) {
			textBoxItemTempReqSkill.Text = CreatePopupSelection("Required Skill", ReadExcelCSV("SkillLine", 0, 3), textBoxItemTempReqSkill.Text);
		}
		private void buttonItemTempReqRepFaction_Click(object sender, EventArgs e) {
			textBoxItemTempReqRepFaction.Text = CreatePopupSelection("Required Reputation Faction", ReadExcelCSV("Faction", 0, 23), textBoxItemTempReqRepFaction.Text);
		}
		private void buttonItemTempReqRepRank_Click(object sender, EventArgs e) {
			textBoxItemTempReqRepRank.Text = CreatePopupSelection("Required Reputation Rank", ReadExcelCSV("ItemReqReputationRank", 0, 1), textBoxItemTempReqRepRank.Text);
		}
		private void buttonItemTempReqSpell_Click(object sender, EventArgs e) {
			textBoxItemTempReqSpell.Text = CreatePopupSelection("Required Spell", ReadExcelCSV("Spells", 0, 1), textBoxItemTempReqSpell.Text);
		}
		private void buttonItemTempMaterial_Click(object sender, EventArgs e) {
			textBoxItemTempMaterial.Text = CreatePopupSelection("Materials", ReadExcelCSV("ItemMaterial", 0, 1), textBoxItemTempMaterial.Text);
		}
		private void buttonItemTempFoodType_Click(object sender, EventArgs e) {
			textBoxItemTempFoodType.Text = CreatePopupSelection("Food Type", ReadExcelCSV("ItemFoodType", 0, 1), textBoxItemTempFoodType.Text);
		}
		private void buttonItemTempBagFamily_Click(object sender, EventArgs e) {
			textBoxItemTempBagFamily.Text = CreatePopupSelection("Bag Family", ReadExcelCSV("ItemBagFamily", 0, 1), textBoxItemTempBagFamily.Text);
		}
		private void buttonItemTempFlagsC_Click(object sender, EventArgs e) {
			textBoxItemTempFlagsC.Text = CreatePopupSelection("Custom Flags", ReadExcelCSV("ItemFlagsCustom", 0, 1), textBoxItemTempFlagsC.Text);
		}
		private void buttonItemTempTotemCategory_Click(object sender, EventArgs e) {
			textBoxItemTempTotemCategory.Text = CreatePopupSelection("Totem Category", ReadExcelCSV("ItemTotemCategory", 0, 1), textBoxItemTempTotemCategory.Text);
		}





		#endregion
	}
}
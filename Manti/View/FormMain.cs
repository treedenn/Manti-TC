using System;
using System.Data;
using System.Windows.Forms;

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

		private void setOfflineMode(bool enable) {
			MySqlDatabase.isRunningOffline = enable;

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
					toolStripSplitButtonAMScriptUpdate,
					toolStripSplitButtonCMScriptUpdate,
					toolStripSplitButtonWMScriptUpdate
			};

			foreach(Button btn in dButtons) {
				btn.Enabled = !enable;
			}

			foreach(ToolStripSplitButton btn in dStripButton) {
				btn.Enabled = !enable;
			}
		}

		#region Functions

		#region Fill/add

		// account

		private void fillAccountTemplate(Account account) {
			if(account != null) {
				textBoxAccountAccountID.Text         = account.id.ToString();
				textBoxAccountAccountUsername.Text   = account.username;
				textBoxAccountAccountEmail.Text      = account.email;
				textBoxAccountAccountRegmail.Text    = account.reqemail;
				textBoxAccountAccountJoindate.Text   = account.reqemail;
				textBoxAccountAccountLastIP.Text     = account.lastIP;
				checkBoxAccountAccountLocked.Checked = account.locked;
				checkBoxAccountAccountOnline.Checked = account.online;
				textBoxAccountAccountExpansion.Text  = account.expansion.ToString();

				if(account.banned != null) {
					textBoxAccountAccountBandate.Text       = account.banned.banDate.ToString();
					textBoxAccountAccountUnbandate.Text     = account.banned.unbanDate.ToString();
					textBoxAccountAccountBanreason.Text     = account.banned.reason;
					textBoxAccountAccountBannedby.Text      = account.banned.by;
					checkBoxAccountAccountBanActive.Checked = account.banned.isActive;

					monthCalendarAccountAccountBanDate.AddMonthlyBoldedDate(account.banned.banDate);
					monthCalendarAccountAccountBanDate.SetDate(account.banned.banDate);
					monthCalendarAccountAccountUnbanDate.AddMonthlyBoldedDate(account.banned.unbanDate);
					monthCalendarAccountAccountUnbanDate.SetDate(account.banned.unbanDate);
				}

				if(account.muted != null) {
					textBoxAccountAccountMutedate.Text   = account.muted.muteDate.ToString();
					textBoxAccountAccountMutetime.Text   = account.muted.duration.ToString();
					textBoxAccountAccountMutereason.Text = account.muted.reason;
					textBoxAccountAccountMutedby.Text    = account.muted.by;

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
				textBoxCharacterCharacterGUID.Text          = character.guid.ToString();
				textBoxCharacterCharacterAccount.Text       = character.accountID.ToString();
				textBoxCharacterCharacterName.Text          = character.name.ToString();
				textBoxCharacterCharacterRace.Text          = character.charRace.ToString();
				textBoxCharacterCharacterClass.Text         = character.charClass.ToString();
				textBoxCharacterCharacterGender.Text        = character.sex.ToString();
				textBoxCharacterCharacterLevel.Text         = character.level.ToString();
				textBoxCharacterCharacterMoney.Text         = character.money.ToString();
				textBoxCharacterCharacterXP.Text            = character.xp.ToString();
				textBoxCharacterCharacterTitle.Text         = character.chosenTitle.ToString();
				checkBoxCharacterCharacterOnline.Checked    = character.isOnline;
				checkBoxCharacterCharacterCinematic.Checked = character.isCinematic;
				checkBoxCharacterCharacterRest.Checked      = character.isResting;
				// Location
				textBoxCharacterCharacterMapID.Text      = character.mapID.ToString();
				textBoxCharacterCharacterInstanceID.Text = character.instanceID.ToString();
				textBoxCharacterCharacterZoneID.Text     = character.zoneID.ToString();
				textBoxCharacterCharacterCoordO.Text     = character.orientation.ToString();
				textBoxCharacterCharacterCoordX.Text     = character.xPosition.ToString();
				textBoxCharacterCharacterCoordY.Text     = character.yPosition.ToString();
				textBoxCharacterCharacterCoordZ.Text     = character.zPosition.ToString();
				// Player vs Player
				textBoxCharacterCharacterHonorPoints.Text = character.honorPoints.ToString();
				textBoxCharacterCharacterArenaPoints.Text = character.arenaPoints.ToString();
				textBoxCharacterCharacterTotalKills.Text  = character.totalKills.ToString();
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
				textBoxCreatureTemplateEntry.Text         = creature.entry.ToString();
				textBoxCreatureTemplateDifEntry1.Text     = creature.diffEntry1.ToString();
				textBoxCreatureTemplateDifEntry2.Text     = creature.diffEntry2.ToString();
				textBoxCreatureTemplateDifEntry3.Text     = creature.diffEntry3.ToString();
				textBoxCreatureTemplateName.Text          = creature.name;
				textBoxCreatureTemplateSubname.Text       = creature.subname;
				textBoxCreatureTemplateModelID1.Text      = creature.modelId1.ToString();
				textBoxCreatureTemplateModelID2.Text      = creature.modelId2.ToString();
				textBoxCreatureTemplateModelID3.Text      = creature.modelId3.ToString();
				textBoxCreatureTemplateModelID4.Text      = creature.modelId4.ToString();
				textBoxCreatureTemplateLevelMin.Text      = creature.minlevel.ToString();
				textBoxCreatureTemplateLevelMax.Text      = creature.maxlevel.ToString();
				textBoxCreatureTemplateGoldMin.Text       = creature.mingold.ToString();
				textBoxCreatureTemplateGoldMax.Text       = creature.maxgold.ToString();
				textBoxCreatureTemplateKillCredit1.Text   = creature.killCredit1.ToString();
				textBoxCreatureTemplateKillCredit2.Text   = creature.killCredit2.ToString();
				textBoxCreatureTemplateRank.Text          = creature.rank.ToString();
				textBoxCreatureTemplateScale.Text         = creature.scale.ToString();
				textBoxCreatureTemplateFaction.Text       = creature.faction.ToString();
				textBoxCreatureTemplateNPCFlags.Text      = creature.npcFlags.ToString();
				textBoxCreatureTemplateSpell1.Text        = creature.spell1.ToString();
				textBoxCreatureTemplateSpell2.Text        = creature.spell2.ToString();
				textBoxCreatureTemplateSpell3.Text        = creature.spell3.ToString();
				textBoxCreatureTemplateSpell4.Text        = creature.spell4.ToString();
				textBoxCreatureTemplateSpell5.Text        = creature.spell5.ToString();
				textBoxCreatureTemplateSpell6.Text        = creature.spell6.ToString();
				textBoxCreatureTemplateSpell7.Text        = creature.spell7.ToString();
				textBoxCreatureTemplateSpell8.Text        = creature.spell8.ToString();
				textBoxCreatureTemplateModHealth.Text     = creature.modHealth.ToString();
				textBoxCreatureTemplateModMana.Text       = creature.modMana.ToString();
				textBoxCreatureTemplateModArmor.Text      = creature.modArmor.ToString();
				textBoxCreatureTemplateModDamage.Text     = creature.modDamage.ToString();
				textBoxCreatureTemplateModExperience.Text = creature.modExp.ToString();
				textBoxCreatureTemplateSpeedWalk.Text     = creature.speedWalk.ToString();
				textBoxCreatureTemplateSpeedRun.Text      = creature.speedRun.ToString();
				textBoxCreatureTemplateBaseAttack.Text    = creature.baseAttackTime.ToString();
				textBoxCreatureTemplateRangedAttack.Text  = creature.rangedAttackTime.ToString();
				textBoxCreatureTemplateBV.Text            = creature.bVariance.ToString();
				textBoxCreatureTemplateRV.Text            = creature.rVariance.ToString();
				textBoxCreatureTemplateDS.Text            = creature.dSchool.ToString();
				textBoxCreatureTemplateAIName.Text        = creature.aiName.ToString();
				textBoxCreatureTemplateMType.Text         = creature.movementType.ToString();
				textBoxCreatureTemplateInhabitType.Text   = creature.inhabitType.ToString();
				textBoxCreatureTemplateHH.Text            = creature.hoverHeight.ToString();
				textBoxCreatureTemplateGMID.Text          = creature.gossipMenuId.ToString();
				textBoxCreatureTemplateMID.Text           = creature.movementId.ToString();
				textBoxCreatureTemplateScriptName.Text    = creature.scriptName.ToString();
				textBoxCreatureTemplateVID.Text           = creature.vehicleId.ToString();
				textBoxCreatureTemplateMechanic.Text      = creature.mechanicImmuneMask.ToString();
				textBoxCreatureTemplateFamily.Text        = creature.family.ToString();
				textBoxCreatureTemplateType.Text          = creature.familyType.ToString();
				textBoxCreatureTemplateTypeFlags.Text     = creature.typeFlags.ToString();
				textBoxCreatureTemplateFlagsExtra.Text    = creature.extraFlags.ToString();
				textBoxCreatureTemplateUnitClass.Text     = creature.unitClass.ToString();
				textBoxCreatureTemplateUnitflags.Text     = creature.unitFlags1.ToString();
				textBoxCreatureTemplateUnitflags2.Text    = creature.unitFlags2.ToString();
				textBoxCreatureTemplateDynamic.Text       = creature.dynamicFlags.ToString();
				checkBoxCreatureTemplateHR.Checked        = creature.isRegenHealth;
				textBoxCreatureTemplateResis1.Text        = creature.resistHoly.ToString();
				textBoxCreatureTemplateResis2.Text        = creature.resistFire.ToString();
				textBoxCreatureTemplateResis3.Text        = creature.resistNature.ToString();
				textBoxCreatureTemplateResis4.Text        = creature.resistFrost.ToString();
				textBoxCreatureTemplateResis5.Text        = creature.resistShadow.ToString();
				textBoxCreatureTemplateResis6.Text        = creature.resistArcane.ToString();
				textBoxCreatureTemplateTType.Text         = creature.trainerType.ToString();
				textBoxCreatureTemplateTSpell.Text        = creature.trainerSpell.ToString();
				textBoxCreatureTemplateTRace.Text         = creature.trainerClass.ToString();
				textBoxCreatureTemplateTClass.Text        = creature.trainerRace.ToString();
				textBoxCreatureTemplateLootID.Text        = creature.lootId.ToString();
				textBoxCreatureTemplatePickID.Text        = creature.pickpocketId.ToString();
				textBoxCreatureTemplateSkinID.Text        = creature.skinId.ToString();

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
				textBoxGameObjectTempEntry.Text   = go.entry.ToString();
				textBoxGameObjectTempType.Text    = go.type.ToString();
				textBoxGameObjectTempDID.Text     = go.displayId.ToString();
				textBoxGameObjectTempName.Text    = go.name;
				textBoxGameObjectTempFaction.Text = go.faction.ToString();
				textBoxGameObjectTempFlags.Text   = go.flags.ToString();
				textBoxGameObjectTempSize.Text    = go.size.ToString();
				textBoxGameObjectTempD0.Text      = go.data0.ToString();
				textBoxGameObjectTempD1.Text      = go.data1.ToString();
				textBoxGameObjectTempD2.Text      = go.data2.ToString();
				textBoxGameObjectTempD3.Text      = go.data3.ToString();
				textBoxGameObjectTempD4.Text      = go.data4.ToString();
				textBoxGameObjectTempD5.Text      = go.data5.ToString();
				textBoxGameObjectTempD6.Text      = go.data6.ToString();
				textBoxGameObjectTempD7.Text      = go.data7.ToString();
				textBoxGameObjectTempD8.Text      = go.data8.ToString();
				textBoxGameObjectTempD9.Text      = go.data9.ToString();
				textBoxGameObjectTempD10.Text     = go.data10.ToString();
				textBoxGameObjectTempD11.Text     = go.data11.ToString();
				textBoxGameObjectTempD12.Text     = go.data12.ToString();
				textBoxGameObjectTempD13.Text     = go.data13.ToString();
				textBoxGameObjectTempD14.Text     = go.data14.ToString();
				textBoxGameObjectTempD15.Text     = go.data15.ToString();
				textBoxGameObjectTempD16.Text     = go.data16.ToString();
				textBoxGameObjectTempD17.Text     = go.data17.ToString();
				textBoxGameObjectTempD18.Text     = go.data18.ToString();
				textBoxGameObjectTempD19.Text     = go.data19.ToString();
				textBoxGameObjectTempD20.Text     = go.data20.ToString();
				textBoxGameObjectTempD21.Text     = go.data21.ToString();
				textBoxGameObjectTempD22.Text     = go.data22.ToString();
				textBoxGameObjectTempD23.Text     = go.data23.ToString();

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

		private void updateAccount() {
			Account account = new Account();

			if(!string.IsNullOrEmpty(textBoxAccountAccountID.Text)) {
				account.id        = Convert.ToUInt32(textBoxAccountAccountID.Text);
				account.username  = textBoxAccountAccountUsername.Text;
				account.email     = textBoxAccountAccountEmail.Text;
				account.reqemail  = textBoxAccountAccountRegmail.Text;
				account.reqemail  = textBoxAccountAccountJoindate.Text;
				account.lastIP    = textBoxAccountAccountLastIP.Text;
				account.locked    = checkBoxAccountAccountLocked.Checked;
				account.online    = checkBoxAccountAccountOnline.Checked;
				account.expansion = Convert.ToByte(textBoxAccountAccountExpansion.Text);
			}

			if(!string.IsNullOrEmpty(textBoxAccountAccountBandate.Text)) {
				account.banned = new Classes.AccountTab.AccountBanned();

				account.banned.banDate   = Classes.UtilityHelper.getDateTimeFromString(textBoxAccountAccountBandate.Text);
				account.banned.unbanDate = Classes.UtilityHelper.getDateTimeFromString(textBoxAccountAccountUnbandate.Text);
				account.banned.reason    = textBoxAccountAccountBanreason.Text;
				account.banned.by        = textBoxAccountAccountBannedby.Text;
				account.banned.isActive  = checkBoxAccountAccountBanActive.Checked;
			}

			if(!string.IsNullOrEmpty(textBoxAccountAccountMutedate.Text)) {
				account.muted = new Classes.AccountTab.AccountMuted();

				account.muted.muteDate = Classes.UtilityHelper.getDateTimeFromString(textBoxAccountAccountMutedate.Text);
				account.muted.duration = Convert.ToDouble(textBoxAccountAccountMutetime.Text);
				account.muted.reason   = textBoxAccountAccountMutereason.Text;
				account.muted.by       = textBoxAccountAccountMutedby.Text;
			}

			if(dataGridViewAccountAccess.Rows.Count > 0) {
				AccountAccess[] access = new AccountAccess[dataGridViewAccountAccess.Rows.Count];

				for(var i = 0; i < dataGridViewAccountAccess.Rows.Count; i++) {
					access[i] = new AccountAccess();

					access[i].id      = Convert.ToUInt32(dataGridViewAccountAccess.Rows[i].Cells[0].Value); // id
					access[i].gmLevel = Convert.ToInt32(dataGridViewAccountAccess.Rows[i].Cells[1].Value); // gmlevel
					access[i].realmID = Convert.ToInt32(dataGridViewAccountAccess.Rows[i].Cells[2].Value); // realmid
				}

				account.access = access;
			}

			Models.AccountModel.getInstance().account = account;
		}

		private void updateCharacter() { // Model.CharacterTab.Character
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

			Models.CharacterModel.getInstance().character = character;
		}
		private void updateCharacterInventory() {
			var ci = new CharacterInventory[dataGridViewCharacterInventory.Rows.Count];

			for(var i = 0; i < dataGridViewCharacterInventory.Rows.Count; i++) {
				ci[i] = new CharacterInventory();

				ci[i].guid = Convert.ToUInt32(dataGridViewCharacterInventory.Rows[0].Cells[0].Value);
				ci[i].bag  = Convert.ToUInt32(dataGridViewCharacterInventory.Rows[0].Cells[1].Value);
				ci[i].slot = Convert.ToUInt32(dataGridViewCharacterInventory.Rows[0].Cells[2].Value);
				ci[i].item = Convert.ToUInt32(dataGridViewCharacterInventory.Rows[0].Cells[3].Value);
			}

			Models.CharacterModel.getInstance().inventory = ci;
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

		private void updateQuest() {
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

			Models.QuestModel.getInstance().quest = quest;
		}

		private void updateGameObject() {
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

			Models.GameObjectModel.getInstance().gameObject = go;
		}

		private void updateItem() {
			Item item = new Item();

			item.entry                  = Convert.ToUInt32(textBoxItemTempEntry.Text);
			item.iClass                 = Convert.ToUInt32(textBoxItemTempTypeClass.Text);
			item.iSub                   = Convert.ToUInt32(textBoxItemTempSubclass.Text);
			item.name                   = textBoxItemTempName.Text;
			item.description            = textBoxItemTempDescription.Text;
			item.displayId              = Convert.ToUInt32(textBoxItemTempDisplayID.Text);
			item.quality                = Convert.ToUInt32(textBoxItemTempQuality.Text);
			item.buycount               = Convert.ToInt32(textBoxItemTempBuyC.Text);
			item.inventory              = Convert.ToUInt32(textBoxItemTempInventory.Text);
			item.flags                  = Convert.ToInt32(textBoxItemTempFlags.Text);
			item.extraFlags             = Convert.ToInt32(textBoxItemTempEFlags.Text);
			item.maxCount               = Convert.ToInt32(textBoxItemTempMaxC.Text);
			item.containerSlot          = Convert.ToUInt32(textBoxItemTempContainer.Text);
			item.buyPrice               = Convert.ToUInt32(textBoxItemTempBuyP.Text);
			item.sellPrice              = Convert.ToUInt32(textBoxItemTempSellP.Text);
			item.damageType1            = Convert.ToUInt32(textBoxItemTempDmgType1.Text);
			item.damageType2            = Convert.ToUInt32(textBoxItemTempDmgType2.Text);
			item.damageMin1             = Convert.ToUInt32(textBoxItemTempDmgMin1.Text);
			item.damageMin2             = Convert.ToUInt32(textBoxItemTempDmgMin2.Text);
			item.damageMax1             = Convert.ToUInt32(textBoxItemTempDmgMax1.Text);
			item.damageMax2             = Convert.ToUInt32(textBoxItemTempDmgMax2.Text);
			item.delay                  = Convert.ToUInt32(textBoxItemTempDelay.Text);
			item.ammoType               = Convert.ToUInt32(textBoxItemTempAmmoType.Text);
			item.rangedMod              = Convert.ToUInt32(textBoxItemTempRangedMod.Text);
			item.itemSet                = Convert.ToUInt32(textBoxItemTempItemSet.Text);
			item.bonding                = Convert.ToUInt32(textBoxItemTempBonding.Text);
			item.block                  = Convert.ToInt32(textBoxItemTempBlock.Text);
			item.durability             = Convert.ToInt32(textBoxItemTempDurability.Text);
			item.sheath                 = Convert.ToUInt32(textBoxItemTempSheath.Text);
			item.reistanceHoly          = Convert.ToUInt32(textBoxItemTempResisHoly.Text);
			item.reistanceFrost         = Convert.ToUInt32(textBoxItemTempResisFrost.Text);
			item.reistanceFire          = Convert.ToUInt32(textBoxItemTempResisFire.Text);
			item.reistanceShadow        = Convert.ToUInt32(textBoxItemTempResisShadow.Text);
			item.reistanceNature        = Convert.ToUInt32(textBoxItemTempResisNature.Text);
			item.reistanceArcane        = Convert.ToUInt32(textBoxItemTempResisArcane.Text);
			item.socketColor1           = Convert.ToUInt32(textBoxItemTempColor1.Text);
			item.socketColor2           = Convert.ToUInt32(textBoxItemTempColor2.Text);
			item.socketColor3           = Convert.ToUInt32(textBoxItemTempColor3.Text);
			item.socketContent1         = Convert.ToUInt32(textBoxItemTempContent1.Text);
			item.socketContent2         = Convert.ToUInt32(textBoxItemTempContent2.Text);
			item.socketContent3         = Convert.ToUInt32(textBoxItemTempContent3.Text);
			item.socketBonus            = Convert.ToUInt32(textBoxItemTempSocketBonus.Text);
			item.socketGemProperty      = Convert.ToUInt32(textBoxItemTempGemProper.Text);
			item.spellEntry1            = Convert.ToUInt32(textBoxItemTempSpellID1.Text);
			item.spellEntry2            = Convert.ToUInt32(textBoxItemTempSpellID2.Text);
			item.spellEntry3            = Convert.ToUInt32(textBoxItemTempSpellID3.Text);
			item.spellEntry4            = Convert.ToUInt32(textBoxItemTempSpellID4.Text);
			item.spellEntry5            = Convert.ToUInt32(textBoxItemTempSpellID5.Text);
			item.spellTrigger1          = Convert.ToInt32(textBoxItemTempTrigger1.Text);
			item.spellTrigger2          = Convert.ToInt32(textBoxItemTempTrigger2.Text);
			item.spellTrigger3          = Convert.ToInt32(textBoxItemTempTrigger3.Text);
			item.spellTrigger4          = Convert.ToInt32(textBoxItemTempTrigger4.Text);
			item.spellTrigger5          = Convert.ToInt32(textBoxItemTempTrigger5.Text);
			item.spellCharges1          = Convert.ToInt32(textBoxItemTempCharges1.Text);
			item.spellCharges2          = Convert.ToInt32(textBoxItemTempCharges2.Text);
			item.spellCharges3          = Convert.ToInt32(textBoxItemTempCharges3.Text);
			item.spellCharges4          = Convert.ToInt32(textBoxItemTempCharges4.Text);
			item.spellCharges5          = Convert.ToInt32(textBoxItemTempCharges5.Text);
			item.spellPPMRate1          = Convert.ToInt32(textBoxItemTempRate1.Text);
			item.spellPPMRate2          = Convert.ToInt32(textBoxItemTempRate2.Text);
			item.spellPPMRate3          = Convert.ToInt32(textBoxItemTempRate3.Text);
			item.spellPPMRate4          = Convert.ToInt32(textBoxItemTempRate4.Text);
			item.spellPPMRate5          = Convert.ToInt32(textBoxItemTempRate5.Text);
			item.spellCooldown1         = Convert.ToSingle(textBoxItemTempCD1.Text);
			item.spellCooldown2         = Convert.ToSingle(textBoxItemTempCD2.Text);
			item.spellCooldown3         = Convert.ToSingle(textBoxItemTempCD3.Text);
			item.spellCooldown4         = Convert.ToSingle(textBoxItemTempCD4.Text);
			item.spellCooldown5         = Convert.ToSingle(textBoxItemTempCD5.Text);
			item.spellCategory1         = Convert.ToInt32(textBoxItemTempCategory1.Text);
			item.spellCategory2         = Convert.ToInt32(textBoxItemTempCategory2.Text);
			item.spellCategory3         = Convert.ToInt32(textBoxItemTempCategory3.Text);
			item.spellCategory4         = Convert.ToInt32(textBoxItemTempCategory4.Text);
			item.spellCategory5         = Convert.ToInt32(textBoxItemTempCategory5.Text);
			item.spellCategoryCooldown1 = Convert.ToSingle(textBoxItemTempCategoryCD1.Text);
			item.spellCategoryCooldown2 = Convert.ToSingle(textBoxItemTempCategoryCD2.Text);
			item.spellCategoryCooldown3 = Convert.ToSingle(textBoxItemTempCategoryCD3.Text);
			item.spellCategoryCooldown4 = Convert.ToSingle(textBoxItemTempCategoryCD4.Text);
			item.spellCategoryCooldown5 = Convert.ToSingle(textBoxItemTempCategoryCD5.Text);
			item.startQuest             = Convert.ToUInt32(textBoxItemTempStartQuest.Text);
			item.material               = Convert.ToInt32(textBoxItemTempMaterial.Text);
			item.property               = Convert.ToUInt32(textBoxItemTempProperty.Text);
			item.suffix                 = Convert.ToUInt32(textBoxItemTempSuffix.Text);
			item.area                   = Convert.ToUInt32(textBoxItemTempArea.Text);
			item.map                    = Convert.ToUInt32(textBoxItemTempMap.Text);
			item.disenchantId           = Convert.ToUInt32(textBoxItemTempDisenchantID.Text);
			item.pageText               = Convert.ToUInt32(textBoxItemTempPageText.Text);
			item.languageId             = Convert.ToUInt32(textBoxItemTempLanguage.Text);
			item.pageMaterial           = Convert.ToUInt32(textBoxItemTempPageMaterial.Text);
			item.foodType               = Convert.ToUInt32(textBoxItemTempFoodType.Text);
			item.lockId                 = Convert.ToUInt32(textBoxItemTempLockID.Text);
			item.holidayId              = Convert.ToUInt32(textBoxItemTempHolidayID.Text);
			item.bagFamily              = Convert.ToUInt32(textBoxItemTempBagFamily.Text);
			item.modifier               = Convert.ToUInt32(textBoxItemTempModifier.Text);
			item.duration               = Convert.ToUInt32(textBoxItemTempDuration.Text);
			item.limitCategory          = Convert.ToUInt32(textBoxItemTempLimitCate.Text);
			item.minMoney               = Convert.ToUInt32(textBoxItemTempMoneyMin.Text);
			item.maxMoney               = Convert.ToUInt32(textBoxItemTempMoneyMax.Text);
			item.flagsCustom            = Convert.ToUInt32(textBoxItemTempFlagsC.Text);
			item.totemCategory          = Convert.ToUInt32(textBoxItemTempTotemCategory.Text);
			item.reqRace                = Convert.ToInt32(textBoxItemTempReqRace.Text);
			item.reqClass               = Convert.ToInt32(textBoxItemTempReqClass.Text);
			item.reqLevel               = Convert.ToUInt32(textBoxItemTempReqLevel.Text);
			item.reqSkill               = Convert.ToUInt32(textBoxItemTempReqSkill.Text);
			item.reqSkillRank           = Convert.ToUInt32(textBoxItemTempReqSkillRank.Text);
			item.reqHonorRank           = Convert.ToUInt32(textBoxItemTempReqHonorRank.Text);
			item.reqRepFaction          = Convert.ToUInt32(textBoxItemTempReqRepFaction.Text);
			item.reqRepRank             = Convert.ToUInt32(textBoxItemTempReqRepRank.Text);
			item.reqDisenchant          = Convert.ToUInt32(textBoxItemTempReqDisenchant.Text);
			item.reqSpell               = Convert.ToUInt32(textBoxItemTempReqSpell.Text);
			item.reqCityRank            = Convert.ToUInt32(textBoxItemTempReqCityRank.Text);
			item.reqItemLevel           = Convert.ToInt32(textBoxItemTempReqItemLevel.Text);
			item.statsCount             = Convert.ToUInt32(textBoxItemTempStatsC.Text);
			item.statsType1             = Convert.ToUInt32(textBoxItemTempStatsType1.Text);
			item.statsType2             = Convert.ToUInt32(textBoxItemTempStatsType2.Text);
			item.statsType3             = Convert.ToUInt32(textBoxItemTempStatsType3.Text);
			item.statsType4             = Convert.ToUInt32(textBoxItemTempStatsType4.Text);
			item.statsType5             = Convert.ToUInt32(textBoxItemTempStatsType5.Text);
			item.statsType6             = Convert.ToUInt32(textBoxItemTempStatsType6.Text);
			item.statsType7             = Convert.ToUInt32(textBoxItemTempStatsType7.Text);
			item.statsType8             = Convert.ToUInt32(textBoxItemTempStatsType8.Text);
			item.statsType9             = Convert.ToUInt32(textBoxItemTempStatsType9.Text);
			item.statsType10            = Convert.ToUInt32(textBoxItemTempStatsType10.Text);
			item.statsValue1            = Convert.ToInt32(textBoxItemTempStatsValue1.Text);
			item.statsValue2            = Convert.ToInt32(textBoxItemTempStatsValue2.Text);
			item.statsValue3            = Convert.ToInt32(textBoxItemTempStatsValue3.Text);
			item.statsValue4            = Convert.ToInt32(textBoxItemTempStatsValue4.Text);
			item.statsValue5            = Convert.ToInt32(textBoxItemTempStatsValue5.Text);
			item.statsValue6            = Convert.ToInt32(textBoxItemTempStatsValue6.Text);
			item.statsValue7            = Convert.ToInt32(textBoxItemTempStatsValue7.Text);
			item.statsValue8            = Convert.ToInt32(textBoxItemTempStatsValue8.Text);
			item.statsValue9            = Convert.ToInt32(textBoxItemTempStatsValue9.Text);
			item.statsValue10           = Convert.ToInt32(textBoxItemTempStatsValue10.Text);
			item.scalingStatDist        = Convert.ToInt32(textBoxItemTempStatsScaleDist.Text);
			item.scalingStatValue       = Convert.ToInt32(textBoxItemTempStatsScaleValue.Text);

			Models.ItemModel.getInstance().item = item;
		}
		private void updateItemLoot() {
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

			Models.ItemModel.getInstance().loot = lpmd;
		}
		private void updateItemProspect() {
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

			Models.ItemModel.getInstance().prospect = lpmd;
		}
		private void updateItemMilling() {
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

			Models.ItemModel.getInstance().mill = lpmd;
		}
		private void updateItemDisenchant() {
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

			Models.ItemModel.getInstance().disenchant = lpmd;
		}

		#endregion

		#endregion

		#region Primary Events

		#region Misc

		private void FormMain_Load(object sender, EventArgs e) {
			this.Icon = Properties.Resources.iconManti;

			tabControlAccountManager.Focus();
			//tabControlCategory.SelectedTab = tabPageItem;

			dataGridViewAccountSearch.AutoGenerateColumns      = false;
			dataGridViewCreatureSearch.AutoGenerateColumns     = false;
			dataGridViewQuestSearch.AutoGenerateColumns        = false;
			dataGridViewGameObjectSearch.AutoGenerateColumns   = false;
			dataGridViewItemSearch.AutoGenerateColumns         = false;
			dataGridViewCharacterInventory.AutoGenerateColumns = false;
			dataGridViewItemLoot.AutoGenerateColumns           = false;
			dataGridViewItemProspect.AutoGenerateColumns       = false;
			dataGridViewItemMill.AutoGenerateColumns           = false;
			dataGridViewItemDE.AutoGenerateColumns             = false;
			dataGridViewQuestGivers.AutoGenerateColumns        = false;

			setOfflineMode(MySqlDatabase.isRunningOffline);
		}
		private void tabControlCategory_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Enter) {
				e.SuppressKeyPress = true;

				if(!MySqlDatabase.isRunningOffline) {
					if(tabControlAccountManager.SelectedTab == tabPageAccount) { // Account Tab
						if(tabControlCategoryAccount.SelectedTab == tabPageAccountSearch) {
							buttonAccountSearchSearch_Click(this, new EventArgs());
						}
					} else if(tabControlAccountManager.SelectedTab == tabPageCharacter) { // Character Tab
						if(tabControlCategoryCharacter.SelectedTab == tabPageCharacterSearch) {
							buttonCharacterSearchSearch_Click(this, new EventArgs());
						}
					} else if(tabControlAccountManager.SelectedTab == tabPageCreature) { // Creature Tab
						if(tabControlCategoryCreature.SelectedTab == tabPageCreatureSearch) {
							buttonCreatureSearchSearch_Click(this, new EventArgs());
						}
					} else if(tabControlAccountManager.SelectedTab == tabPageQuest) { // Quest Tab
						if(tabControlCategoryQuest.SelectedTab == tabPageQuestSearch) {
							buttonQuestSearchSearch_Click(this, new EventArgs());
						}
					} else if(tabControlAccountManager.SelectedTab == tabPageGameObject) { // Game Object Tab
						if(tabControlCategoryGameObject.SelectedTab == tabPageGameObjectSearch) {
							buttonGameObjectSearchSearch_Click(this, new EventArgs());
						}
					} else if(tabControlAccountManager.SelectedTab == tabPageItem) { // Item Tab
						if(tabControlCategoryItem.SelectedTab == tabPageItemSearch) {
							buttonItemSearchSearch_Click(this, new EventArgs());
						}
					}
				}
			}
		}

		#endregion

		#region Search

		private void buttonAccountSearchSearch_Click(object sender, EventArgs e) {
			bool emptyControls = Classes.UtilityHelper.checkEmptyControls(tabPageAccountSearch);

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

			GC.Collect();

			toolStripStatusLabelAccountSearchRows.Text = "Account(s) found: " + (dt != null ? dt.Rows.Count.ToString() : "0");
		}
		private void buttonCharacterSearchSearch_Click(object sender, EventArgs e) {
			bool emptyControls = Classes.UtilityHelper.checkEmptyControls(tabPageCharacterSearch);

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

			GC.Collect();

			toolStripStatusLabelCharacterSearchRows.Text = "Character(s) found: " + (dt != null ? dt.Rows.Count.ToString() : "0");
		}
		private void buttonCreatureSearchSearch_Click(object sender, EventArgs e) {
			bool emptyControls = Classes.UtilityHelper.checkEmptyControls(tabPageCreatureSearch);

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

			GC.Collect();

			toolStripStatusLabelCreatureSearchRows.Text = "Creature(s) found: " + (dt != null ? dt.Rows.Count.ToString() : "0");
		}
		private void buttonQuestSearchSearch_Click(object sender, EventArgs e) {
			bool emptyControls = Classes.UtilityHelper.checkEmptyControls(tabPageQuestSearch);

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

			GC.Collect();

			toolStripStatusLabelQuestSearchRows.Text = "Quest(s) found: " + (dt != null ? dt.Rows.Count.ToString() : "0");
		}
		private void buttonGameObjectSearchSearch_Click(object sender, EventArgs e) {
			bool emptyControls = Classes.UtilityHelper.checkEmptyControls(tabPageGameObjectSearch);

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

			GC.Collect();

			toolStripStatusLabelGameObjectSearchRows.Text = "Game Object(s) found: " + (dt != null ? dt.Rows.Count.ToString() : "0");
		}
		private void buttonItemSearchSearch_Click(object sender, EventArgs e) {
			bool emptyControls = Classes.UtilityHelper.checkEmptyControls(tabPageItemSearch);

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

			GC.Collect();

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
			try {
				updateAccount();

				Account account = Models.AccountModel.getInstance().account;
				var ga = new GenerateAuth();

				textBoxAMScriptOutput.AppendText(ga.accountFullToSQL(account));
				tabControlAccountManager.SelectedTab = tabPageAMScript;
			} catch(Exception ex) when(ex is FormatException || ex is NullReferenceException) {
				MessageBox.Show("In most scenarios all textboxes have not be filled.\nPlease check that textboxes that require a number, has a number and not letters.\nIf it require a string/text, empty is mostly accepted.", "Format or Null Exception occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void buttonCharacterCharacterGenerate_Click(object sender, EventArgs e) {
			try {
				updateCharacter();

				Character c = Models.CharacterModel.getInstance().character;

				var gc = new GenerateCharacters();

				textBoxCMScriptOutput.AppendText(gc.characterToSql(c));

				tabControlCharacterManager.SelectedTab = tabPageCMScript;
			} catch(Exception ex) when (ex is FormatException || ex is NullReferenceException) {
				MessageBox.Show("In most scenarios all textboxes have not be filled.\nPlease check that textboxes that require a number, has a number and not letters.\nIf it require a string/text, empty is mostly accepted.", "Format or Null Exception occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void buttonWorldManagerGenerate_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			var worldModel = Models.WorldManagerModel.getInstance();
			var gw = new GenerateWorld();

			string sql = "";

			try {
				if(btn == buttonCreatureTempGenerate) {
					updateCreature();
					worldModel.lastGeneratedWorldObject = Models.WorldManagerModel.worldObject.CREATURE;
					sql = gw.creatureToSql(Models.CreatureModel.getInstance().creature);
				} else if(btn == buttonItemTempGenerate) {
					updateItem();
					worldModel.lastGeneratedWorldObject = Models.WorldManagerModel.worldObject.ITEM;
					sql = gw.itemToSql(Models.ItemModel.getInstance().item);
				} else if(btn == buttonQuestSectionGenerate) {
					updateQuest();
					worldModel.lastGeneratedWorldObject = Models.WorldManagerModel.worldObject.QUEST;
					sql = gw.questToSql(Models.QuestModel.getInstance().quest);
				} else if(btn == buttonGameObjectTempGenerate) {
					updateGameObject();
					worldModel.lastGeneratedWorldObject = Models.WorldManagerModel.worldObject.GAMEOBJECT;
					sql = gw.gameObjectToSql(Models.GameObjectModel.getInstance().gameObject);
				}

				textBoxWMScriptOutput.AppendText(sql);
				tabControlWorldManager.SelectedTab = tabPageWMScript;
			} catch(Exception ex) when(ex is FormatException || ex is NullReferenceException) {
				MessageBox.Show("In most scenarios all textboxes have not be filled.\nPlease check that textboxes that require a number, has a number and not letters.\nIf it require a string/text, empty is mostly accepted.", "Format or Null Exception occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

		#endregion - - - - - - 

		#region Secondary Events

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

		#region Account Manager

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

		#region Character Manager

		// character - inventory tab
		private void buttonCharacterInventoryAdd_Click(object sender, EventArgs e) {
			var guid = textBoxCharacterInventoryGUID.Text;
			var bag = textBoxCharacterInventoryBag.Text;
			var slot = textBoxCharacterInventorySlot.Text;
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
			if(!string.IsNullOrEmpty(iGuid)) { uint.TryParse(iGuid, out entry); } else if(!string.IsNullOrEmpty(tGuid)) { uint.TryParse(tGuid, out entry); } else { return; }

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
			updateCharacterInventory();

			var ci = Models.CharacterModel.getInstance().inventory;

			if(ci.Length > 0) {
				textBoxCMScriptOutput.Text = new GenerateCharacters().characterInventoryToSql(ci[0].guid, ci);
			}
		}

		// send mail
		private void buttonCharacterManagerMailAdd_Click(object sender, EventArgs e) {
			
		}
		private void buttonCharacterManagerMailDelete_Click(object sender, EventArgs e) {
			
		}
		private void buttonCharacterManagerMailRefresh_Click(object sender, EventArgs e) {
			
		}

		#endregion

		#region World Manager

		// creature - search tab
		private void toolStripSplitButtonCreatureNew_ButtonClick(object sender, EventArgs e) {
			var dw = Settings.getWorldDB();

			var model = Models.CreatureModel.getInstance();
			model.creature = dw.getCreatureDefaultValues();

			fillCreatureTemplate(model.creature);

			tabControlCategoryCreature.SelectedTab = tabPageCreatureTemplate;
		}
		private void toolStripSplitButtonCreatureDelete_ButtonClick(object sender, EventArgs e) {
			var gw = new GenerateWorld();

			foreach(DataGridViewRow row in dataGridViewCreatureSearch.SelectedRows) {
				textBoxCMScriptOutput.AppendText(gw.deleteCreature(Convert.ToUInt32(row.Cells[0].Value)) + Environment.NewLine);
			}
		}

		// creature - vendor, loot, pickpocket and skin tab
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

			string sql = "";

			if(btn == buttonCreatureVendorGenerate) {
				updateCreatureVendor();
				sql = new GenerateWorld().creatureVendorToSql(model.vendor);
			} else if(btn == buttonCreatureLootGenerate) {
				updateCreatureLoot();
				sql = new GenerateWorld().creatureLPSToSql("creature_loot_template", model.loot);
			} else if(btn == buttonCreaturePickpocketGenerate) {
				updateCreaturePickpocket();
				sql = new GenerateWorld().creatureLPSToSql("pickpocketing_loot_template", model.pickpocket);
			} else if(btn == buttonCreatureSkinGenerate) {
				updateCreatureSkin();
				sql = new GenerateWorld().creatureLPSToSql("skinning_loot_template", model.skin);
			}

			if(!string.IsNullOrEmpty(sql)) {
				textBoxCMScriptOutput.AppendText(sql);
			}

			tabControlCharacterManager.SelectedTab = tabPageCMScript;
		}

		// quest - search tab
		private void toolStripSplitButtonQuestNew_ButtonClick(object sender, EventArgs e) {
			var dw = Settings.getWorldDB();

			var model = Models.QuestModel.getInstance();
			model.quest = dw.getQuestDefaultValues();

			fillQuestSections(model.quest);

			tabControlCategoryQuest.SelectedTab = tabPageQuestSection1;
		}
		private void toolStripSplitButtonQuestDelete_ButtonClick(object sender, EventArgs e) {
			var gw = new GenerateWorld();

			foreach(DataGridViewRow row in dataGridViewQuestSearch.SelectedRows) {
				textBoxWMScriptOutput.AppendText(gw.deleteQuest(Convert.ToUInt32(row.Cells[0].Value)) + Environment.NewLine);
			}
		}

		// gameobject - search tab
		private void toolStripSplitButtonGONew_ButtonClick(object sender, EventArgs e) {
			var dw = Settings.getWorldDB();

			var model = Models.GameObjectModel.getInstance();
			model.gameObject = dw.getGameObjectDefaultValues();

			fillGameObjectTemplate(model.gameObject);

			tabControlCategoryGameObject.SelectedTab = tabPageGameObjectTemplate;
		}
		private void toolStripSplitButtonGODelete_ButtonClick(object sender, EventArgs e) {
			var gw = new GenerateWorld();

			foreach(DataGridViewRow row in dataGridViewGameObjectSearch.SelectedRows) {
				textBoxWMScriptOutput.AppendText(gw.deleteGameObject(Convert.ToUInt32(row.Cells[0].Value)) + Environment.NewLine);
			}
		}

		// item - search tab
		private void toolStripSplitButtonItemNew_ButtonClick(object sender, EventArgs e) {
			var dw = Settings.getWorldDB();

			var model = Models.ItemModel.getInstance();
			model.item = dw.getItemDefaultValues();

			fillItemTemplate(model.item);

			tabControlCategoryItem.SelectedTab = tabPageItemTemplate;
		}
		private void toolStripSplitButtonItemDelete_ButtonClick(object sender, EventArgs e) {
			var gw = new GenerateWorld();

			foreach(DataGridViewRow row in dataGridViewItemSearch.SelectedRows) {
				textBoxWMScriptOutput.AppendText(gw.deleteItem(Convert.ToUInt32(row.Cells[0].Value)) + Environment.NewLine);
			}
		}

		// item - loot, prospect, milling and disenchant
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
			} else if(btn == buttonItemProspectAdd) {
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

				textBoxWMScriptOutput.AppendText(sql);
			} else if(btn == buttonItemProspectGenerate) {
				updateItemProspect();
				var sql = new GenerateWorld().itemLPMDToSql("prospecting_loot_template", model.prospect);

				textBoxWMScriptOutput.AppendText(sql);
			} else if(btn == buttonItemMillGenerate) {
				updateItemMilling();
				var sql = new GenerateWorld().itemLPMDToSql("milling_loot_template", model.mill);

				textBoxWMScriptOutput.AppendText(sql);
			} else if(btn == buttonItemDEGenerate) {
				updateItemDisenchant();
				var sql = new GenerateWorld().itemLPMDToSql("disenchant_loot_template", model.disenchant);

				textBoxWMScriptOutput.AppendText(sql);
			}
		}

		#endregion

		#region Popups

		private void buttonCharacterPopup_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			if(btn == buttonCharacterCharacterRace) {
				textBoxCharacterCharacterRace.Text = Classes.PopupHelper.createPopupSelection("Character Race", Classes.UtilityHelper.readExcelCSV("ChrRaces", 0, 14), textBoxCharacterCharacterRace.Text);
			} else if(btn == buttonCharacterCharacterClass) {
				textBoxCharacterCharacterClass.Text = Classes.PopupHelper.createPopupSelection("Character Class", Classes.UtilityHelper.readExcelCSV("ChrClasses", 0, 4), textBoxCharacterCharacterClass.Text);
			}
		}
		public void buttonCreaturePopup_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			if(btn == buttonCreatureTemplateModelID1) {
				textBoxCreatureTemplateModelID1.Text = Classes.PopupHelper.createPopupEntity(textBoxCreatureTemplateModelID1.Text, new bool[] { false, true, false }, false);
			} else if(btn == buttonCreatureTemplateRank) {
				textBoxCreatureTemplateRank.Text = Classes.PopupHelper.createPopupSelection("Creature Rank", Classes.UtilityHelper.readExcelCSV("CreatureRanks", 0, 1), textBoxCreatureTemplateRank.Text);
			} else if(btn == buttonCreatureTemplateNPCFlags) {
				textBoxCreatureTemplateNPCFlags.Text = Classes.PopupHelper.createPopupChecklist("Creature NPC Flags", Classes.UtilityHelper.readExcelCSV("CreatureNPCFlags", 0, 1), textBoxCreatureTemplateNPCFlags.Text, true);
			} else if(btn == buttonCreatureTemplateSpell1) {
				textBoxCreatureTemplateSpell1.Text = Classes.PopupHelper.createPopupSelection("Spells I", Classes.UtilityHelper.readExcelCSV("Spells", 0, 1), textBoxCreatureTemplateSpell1.Text);
			} else if(btn == buttonCreatureTemplateDS) {
				textBoxCreatureTemplateDS.Text = Classes.PopupHelper.createPopupSelection("Damage School (Type)", Classes.UtilityHelper.readExcelCSV("CreatureDmgSchool", 0, 1), textBoxCreatureTemplateDS.Text);
			} else if(btn == buttonCreatureTemplateMType) {
				textBoxCreatureTemplateMType.Text = Classes.PopupHelper.createPopupSelection("Movement Type", Classes.UtilityHelper.readExcelCSV("CreatureMovementType", 0, 1), textBoxCreatureTemplateMType.Text);
			} else if(btn == buttonCreatureTemplateInhabitType) {
				textBoxCreatureTemplateInhabitType.Text = Classes.PopupHelper.createPopupChecklist("Inhabit Types", Classes.UtilityHelper.readExcelCSV("CreatureInhabitTypes", 0, 1), textBoxCreatureTemplateInhabitType.Text, true); ;
			} else if(btn == buttonCreatureTemplateMechanic) {
				textBoxCreatureTemplateMechanic.Text = Classes.PopupHelper.createPopupChecklist("Creature's Immunity", Classes.UtilityHelper.readExcelCSV("CreatureMechanic", 0, 1), textBoxCreatureTemplateMechanic.Text, true);
			} else if(btn == buttonCreatureTemplateFamily) {
				textBoxCreatureTemplateFamily.Text = Classes.PopupHelper.createPopupSelection("Creature's Family", Classes.UtilityHelper.readExcelCSV("CreatureFamily", 0, 1), textBoxCreatureTemplateFamily.Text);
			} else if(btn == buttonCreatureTemplateType) {
				textBoxCreatureTemplateType.Text = Classes.PopupHelper.createPopupSelection("Creature's Type", Classes.UtilityHelper.readExcelCSV("CreatureFamilyType", 0, 1), textBoxCreatureTemplateType.Text);
			} else if(btn == buttonCreatureTemplateTypeFlags) {
				textBoxCreatureTemplateTypeFlags.Text = Classes.PopupHelper.createPopupChecklist("Unit Flags I", Classes.UtilityHelper.readExcelCSV("CreatureTypeFlags", 0, 1), textBoxCreatureTemplateTypeFlags.Text, true);
			} else if(btn == buttonCreatureTemplateFlagsExtra) {
				textBoxCreatureTemplateFlagsExtra.Text = Classes.PopupHelper.createPopupChecklist("Extra Flags", Classes.UtilityHelper.readExcelCSV("CreatureFlagsExtra", 0, 1), textBoxCreatureTemplateFlagsExtra.Text, true);
			} else if(btn == buttonCreatureTemplateUnitClass) {
				textBoxCreatureTemplateUnitClass.Text = Classes.PopupHelper.createPopupSelection("Creature's Class", Classes.UtilityHelper.readExcelCSV("CreatureUnitClass", 0, 1), textBoxCreatureTemplateUnitClass.Text);
			} else if(btn == buttonCreatureTemplateUnitflags) {
				textBoxCreatureTemplateUnitflags.Text = Classes.PopupHelper.createPopupChecklist("Unit Flags I", Classes.UtilityHelper.readExcelCSV("CreatureUnitFlags", 0, 1), textBoxCreatureTemplateUnitflags.Text, true);
			} else if(btn == buttonCreatureTemplateUnitflags2) {
				textBoxCreatureTemplateUnitflags2.Text = Classes.PopupHelper.createPopupChecklist("Unit Flags II", Classes.UtilityHelper.readExcelCSV("CreatureUnitFlags2", 0, 1), textBoxCreatureTemplateUnitflags2.Text, true);
			} else if(btn == buttonCreatureTemplateDynamic) {
				textBoxCreatureTemplateDynamic.Text = Classes.PopupHelper.createPopupChecklist("Dynamic Flags", Classes.UtilityHelper.readExcelCSV("CreatureDynamicFlags", 0, 1), textBoxCreatureTemplateDynamic.Text, true);
			}
		}
		public void buttonQuestPopup_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			if(btn == buttonQuestSearchInfo) {
				textBoxQuestSearchInfo.Text = Classes.PopupHelper.createPopupSelection("Quest Info", Classes.UtilityHelper.readExcelCSV("QuestInfo", 0, 1), textBoxQuestSearchInfo.Text);
			} else if(btn == buttonQuestSectionSourceItemID) {
				textBoxQuestSectionSourceItemID.Text = Classes.PopupHelper.createPopupEntity(textBoxQuestSectionSourceItemID.Text, new bool[] { true, false, false });
			} else if(btn == buttonQuestSectionReqRace) {
				textBoxQuestSectionReqRace.Text = Classes.PopupHelper.createPopupChecklist("Requirement: Races", Classes.UtilityHelper.readExcelCSV("ChrRaces", 0, 14), textBoxQuestSectionReqRace.Text, true);
			} else if(btn == buttonQuestSectionReqClass) {
				textBoxQuestSectionReqClass.Text = Classes.PopupHelper.createPopupChecklist("Requirement: Classes", Classes.UtilityHelper.readExcelCSV("ChrClasses", 0, 4), textBoxQuestSectionReqClass.Text, true);
			} else if(btn == buttonQuestSectionQSort) {
				if(radioButtonQuestSectionZID.Checked) {
					textBoxQuestSectionReqQSort.Text = Classes.PopupHelper.createPopupSelection("Zone ID Selection", Classes.UtilityHelper.readExcelCSV("AreaTable", 0, 11), textBoxQuestSectionReqQSort.Text);
				} else {
					string newValue = Classes.PopupHelper.createPopupSelection("Quest Sort Selection", Classes.UtilityHelper.readExcelCSV("QuestSort", 0, 1), textBoxQuestSectionReqQSort.Text.Trim('-'));

					textBoxQuestSectionReqQSort.Text = (textBoxQuestSectionReqQSort.Text == newValue || newValue == "0") ? textBoxQuestSectionReqQSort.Text : "-" + newValue;
				}
			} else if(btn == buttonQuestSectionReqFaction1) {
				textBoxQuestSectionReqFaction1.Text = Classes.PopupHelper.createPopupSelection("Objective Faction ID I", Classes.UtilityHelper.readExcelCSV("Faction", 0, 23), textBoxQuestSectionReqFaction1.Text);
			} else if(btn == buttonQuestSectionReqFaction2) {
				textBoxQuestSectionReqFaction2.Text = Classes.PopupHelper.createPopupSelection("Objective Faction ID II", Classes.UtilityHelper.readExcelCSV("Faction", 0, 23), textBoxQuestSectionReqFaction2.Text);
			} else if(btn == buttonQuestSectionReqMinRepF) {
				textBoxQuestSectionReqMinRepF.Text = Classes.PopupHelper.createPopupSelection("Minimum Reputation Faction", Classes.UtilityHelper.readExcelCSV("Faction", 0, 23), textBoxQuestSectionReqMinRepF.Text);
			} else if(btn == buttonQuestSectionReqMaxRepF) {
				textBoxQuestSectionReqMaxRepF.Text = Classes.PopupHelper.createPopupSelection("Maximum Reputation Faction", Classes.UtilityHelper.readExcelCSV("Faction", 0, 23), textBoxQuestSectionReqMaxRepF.Text);
			} else if(btn == buttonQuestSectionReqSkillID) {
				textBoxQuestSectionReqSkillID.Text = Classes.PopupHelper.createPopupSelection("Required Skill ID", Classes.UtilityHelper.readExcelCSV("SkillLine", 0, 3), textBoxQuestSectionReqSkillID.Text);
			} else if(btn == buttonQuestSectionQuestType) {
				textBoxQuestSectionQuestType.Text = Classes.PopupHelper.createPopupSelection("Quest Type", Classes.UtilityHelper.readExcelCSV("QuestType", 0, 1), textBoxQuestSectionQuestType.Text);
			} else if(btn == buttonQuestSectionQuestFlags) {
				textBoxQuestSectionQuestFlags.Text = Classes.PopupHelper.createPopupChecklist("Quest : Flags", Classes.UtilityHelper.readExcelCSV("QuestFlags", 0, 1), textBoxQuestSectionQuestFlags.Text, true);
			} else if(btn == buttonQuestSectionOtherSF) {
				textBoxQuestSectionOtherSF.Text = Classes.PopupHelper.createPopupChecklist("Quest : Special Flags", Classes.UtilityHelper.readExcelCSV("QuestSpecialFlags", 0, 1), textBoxQuestSectionOtherSF.Text, true);
			} else if(btn == buttonQuestSectionQuestInfo) {
				textBoxQuestSectionQuestInfo.Text = Classes.PopupHelper.createPopupSelection("Quest Info", Classes.UtilityHelper.readExcelCSV("QuestInfo", 0, 1), textBoxQuestSectionQuestInfo.Text);
			} else if(btn == buttonQuestSectionSourceSpellID) {
				textBoxQuestSectionSourceSpellID.Text = Classes.PopupHelper.createPopupSelection("Spells", Classes.UtilityHelper.readExcelCSV("Spells", 0, 1), textBoxQuestSectionSourceSpellID.Text);
			} else if(btn == buttonQuestSectionReqNPCID1) {
				textBoxQuestSectionReqNPCID1.Text = Classes.PopupHelper.createPopupEntity(textBoxQuestSectionReqNPCID1.Text, new bool[] { false, true, false });
			} else if(btn == buttonQuestSectionReqItemID1) {
				textBoxQuestSectionReqItemID1.Text = Classes.PopupHelper.createPopupEntity(textBoxQuestSectionReqItemID1.Text, new bool[] { true, false, false });
			} else if(btn == buttonQuestSectionRewChoiceID1) {
				textBoxQuestSectionRewChoiceID1.Text = Classes.PopupHelper.createPopupEntity(textBoxQuestSectionRewChoiceID1.Text, new bool[] { true, false, false });
			} else if(btn == buttonQuestSectionRewItemID1) {
				textBoxQuestSectionRewItemID1.Text = Classes.PopupHelper.createPopupEntity(textBoxQuestSectionRewItemID1.Text, new bool[] { true, false, false });
			} else if(btn == buttonQuestSectionRewFactionID1) {
				textBoxQuestSectionRewFactionID1.Text = Classes.PopupHelper.createPopupSelection("Faction Selection", Classes.UtilityHelper.readExcelCSV("Faction", 0, 23), textBoxQuestSectionRewFactionID1.Text);
			} else if(btn == buttonQuestSectionRewOtherTitleID) {
				textBoxQuestSectionRewOtherTitleID.Text = Classes.PopupHelper.createPopupSelection("Title Selection", Classes.UtilityHelper.readExcelCSV("CharTitles", 0, 2), textBoxQuestSectionRewOtherTitleID.Text);
			} else if(btn == buttonQuestSectionRewSpell) {
				textBoxQuestSectionRewSpell.Text = Classes.PopupHelper.createPopupSelection("Spell Selection", Classes.UtilityHelper.readExcelCSV("Spells", 0, 1), textBoxQuestSectionRewSpell.Text);
			}
		}
		public void buttonGameObjectPopup_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			if(btn == buttonGameObjectTempType) {
				textBoxGameObjectTempType.Text = Classes.PopupHelper.createPopupSelection("Game Object Type Selection", Classes.UtilityHelper.readExcelCSV("GameObjectTypes", 0, 1), textBoxGameObjectTempType.Text);
			} else if(btn == buttonGameObjectTempFlags) {
				textBoxGameObjectTempFlags.Text = Classes.PopupHelper.createPopupChecklist("Game Object Flags Selection", Classes.UtilityHelper.readExcelCSV("GameObjectFlags", 0, 1), textBoxGameObjectTempFlags.Text, true);
			}
		}
		public void buttonItemPopup_Click(object sender, EventArgs e) {
			Button btn = (Button) sender;

			if(btn == buttonItemSearchClass) {
				textBoxItemSearchClass.Text = Classes.PopupHelper.createPopupSelection("Class Selection", DataItemClass(), textBoxItemSearchClass.Text);
			} else if(btn == buttonItemSearchSubclass) {
				textBoxItemSearchSubclass.Text = Classes.PopupHelper.createPopupSelection("Subclass Selection", DataItemSubclass(textBoxItemSearchClass.Text.Trim()), textBoxItemSearchSubclass.Text);
			} else if(btn == buttonItemTempTypeClass) {
				textBoxItemTempTypeClass.Text = Classes.PopupHelper.createPopupSelection("Class Selection", DataItemClass(), textBoxItemTempTypeClass.Text);
			} else if(btn == buttonItemTempSubclass) {
				textBoxItemTempSubclass.Text = Classes.PopupHelper.createPopupSelection("Subclass Selection", DataItemSubclass(textBoxItemTempTypeClass.Text.Trim()), textBoxItemTempSubclass.Text);
			} else if(btn == buttonItemTempDisplayID) {
				textBoxItemTempDisplayID.Text = Classes.PopupHelper.createPopupEntity(textBoxItemTempDisplayID.Text, new bool[] { true, false, false }, false);
			} else if(btn == buttonItemTempQuality) {
				textBoxItemTempQuality.Text = Classes.PopupHelper.createPopupSelection("Quality Selection", Classes.UtilityHelper.readExcelCSV("ItemQuality", 0, 1), textBoxItemTempQuality.Text);
			} else if(btn == buttonItemTempFlags) {
				textBoxItemTempFlags.Text = Classes.PopupHelper.createPopupChecklist("Flags", Classes.UtilityHelper.readExcelCSV("ItemFlags", 0, 1), textBoxItemTempFlags.Text, true);
			} else if(btn == buttonItemTempEFlags) {
				textBoxItemTempEFlags.Text = Classes.PopupHelper.createPopupChecklist("Extra Flags", Classes.UtilityHelper.readExcelCSV("ItemFlagsExtra", 0, 1), textBoxItemTempEFlags.Text, true);
			} else if(btn == buttonItemTempDmgType1) {
				textBoxItemTempDmgType1.Text = Classes.PopupHelper.createPopupSelection("Damage Type I Selection", Classes.UtilityHelper.readExcelCSV("ItemDamageTypes", 0, 1), textBoxItemTempDmgType1.Text);
			} else if(btn == buttonItemTempDmgType2) {
				textBoxItemTempDmgType2.Text = Classes.PopupHelper.createPopupSelection("Damage Type II Selection", Classes.UtilityHelper.readExcelCSV("ItemDamageTypes", 0, 1), textBoxItemTempDmgType2.Text);
			} else if(btn == buttonItemTempAmmoType) {
				textBoxItemTempAmmoType.Text = Classes.PopupHelper.createPopupSelection("Ammo Types", Classes.UtilityHelper.readExcelCSV("ItemAmmoType", 0, 1), textBoxItemTempAmmoType.Text);
			} else if(btn == buttonItemTempItemSet) {
				textBoxItemTempItemSet.Text = Classes.PopupHelper.createPopupSelection("ItemSet Selection", Classes.UtilityHelper.readExcelCSV("ItemSet", 0, 1), textBoxItemTempItemSet.Text);
			} else if(btn == buttonItemTempBonding) {
				textBoxItemTempBonding.Text = Classes.PopupHelper.createPopupSelection("Bonding Selection", Classes.UtilityHelper.readExcelCSV("ItemBondings", 0, 1), textBoxItemTempBonding.Text);
			} else if(btn == buttonItemTempSheath) {
				textBoxItemTempSheath.Text = Classes.PopupHelper.createPopupSelection("Sheath Selection", Classes.UtilityHelper.readExcelCSV("ItemSheaths", 0, 1), textBoxItemTempSheath.Text);
			} else if(btn == buttonItemTempColor1) {
				textBoxItemTempColor1.Text = Classes.PopupHelper.createPopupSelection("Color Selection I", Classes.UtilityHelper.readExcelCSV("ItemSocketColors", 0, 1), textBoxItemTempColor1.Text);
			} else if(btn == buttonItemTempColor2) {
				textBoxItemTempColor2.Text = Classes.PopupHelper.createPopupSelection("Color Selection II", Classes.UtilityHelper.readExcelCSV("ItemSocketColors", 0, 1), textBoxItemTempColor2.Text);
			} else if(btn == buttonItemTempColor3) {
				textBoxItemTempColor3.Text = Classes.PopupHelper.createPopupSelection("Color Selection III", Classes.UtilityHelper.readExcelCSV("ItemSocketColors", 0, 1), textBoxItemTempColor3.Text);
			} else if(btn == buttonItemTempSocketBonus) {
				textBoxItemTempSocketBonus.Text = Classes.PopupHelper.createPopupSelection("Socket Bonus Selection III", Classes.UtilityHelper.readExcelCSV("ItemSocketBonus", 0, 1), textBoxItemTempSocketBonus.Text);
			} else if(btn == buttonItemTempStatsType1) {
				textBoxItemTempStatsType1.Text = Classes.PopupHelper.createPopupSelection("Stat Selection I", Classes.UtilityHelper.readExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType1.Text);
			} else if(btn == buttonItemTempStatsType2) {
				textBoxItemTempStatsType2.Text = Classes.PopupHelper.createPopupSelection("Stat Selection II", Classes.UtilityHelper.readExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType2.Text);
			} else if(btn == buttonItemTempStatsType3) {
				Classes.PopupHelper.createPopupSelection("Stat Selection III", Classes.UtilityHelper.readExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType3.Text);
			} else if(btn == buttonItemTempStatsType4) {
				Classes.PopupHelper.createPopupSelection("Stat Selection IV", Classes.UtilityHelper.readExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType4.Text);
			} else if(btn == buttonItemTempStatsType5) {
				textBoxItemTempStatsType5.Text = Classes.PopupHelper.createPopupSelection("Stat Selection V", Classes.UtilityHelper.readExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType5.Text);
			} else if(btn == buttonItemTempStatsType6) {
				textBoxItemTempStatsType6.Text = Classes.PopupHelper.createPopupSelection("Stat Selection VI", Classes.UtilityHelper.readExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType6.Text);
			} else if(btn == buttonItemTempStatsType7) {
				textBoxItemTempStatsType7.Text = Classes.PopupHelper.createPopupSelection("Stat Selection VII", Classes.UtilityHelper.readExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType7.Text);
			} else if(btn == buttonItemTempStatsType8) {
				textBoxItemTempStatsType8.Text = Classes.PopupHelper.createPopupSelection("Stat Selection VIII", Classes.UtilityHelper.readExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType8.Text);
			} else if(btn == buttonItemTempStatsType9) {
				textBoxItemTempStatsType9.Text = Classes.PopupHelper.createPopupSelection("Stat Selection IX", Classes.UtilityHelper.readExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType9.Text);
			} else if(btn == buttonItemTempStatsType10) {
				textBoxItemTempStatsType10.Text = Classes.PopupHelper.createPopupSelection("Stat Selection X", Classes.UtilityHelper.readExcelCSV("ItemStatTypes", 0, 1), textBoxItemTempStatsType10.Text);
			} else if(btn == buttonItemTempSpellID1) {
				textBoxItemTempSpellID1.Text = Classes.PopupHelper.createPopupSelection("Required Spell", Classes.UtilityHelper.readExcelCSV("Spells", 0, 1), textBoxItemTempSpellID1.Text);
			} else if(btn == buttonItemTempTrigger1) {
				textBoxItemTempTrigger1.Text = Classes.PopupHelper.createPopupSelection("Spell Trigger", Classes.UtilityHelper.readExcelCSV("ItemSpellTrigger", 0, 1), textBoxItemTempTrigger1.Text);
			} else if(btn == buttonItemTempReqRace) {
				textBoxItemTempReqRace.Text = Classes.PopupHelper.createPopupChecklist("Race Requirement", Classes.UtilityHelper.readExcelCSV("ChrRaces", 0, 14), textBoxItemTempReqRace.Text, true);
			} else if(btn == buttonItemTempReqClass) {
				textBoxItemTempReqClass.Text = Classes.PopupHelper.createPopupChecklist("Class Requirement", Classes.UtilityHelper.readExcelCSV("ChrClasses", 0, 4), textBoxItemTempReqClass.Text, true);
			} else if(btn == buttonItemTempReqSkill) {
				textBoxItemTempReqSkill.Text = Classes.PopupHelper.createPopupSelection("Required Skill", Classes.UtilityHelper.readExcelCSV("SkillLine", 0, 3), textBoxItemTempReqSkill.Text);
			} else if(btn == buttonItemTempReqRepFaction) {
				textBoxItemTempReqRepFaction.Text = Classes.PopupHelper.createPopupSelection("Required Reputation Faction", Classes.UtilityHelper.readExcelCSV("Faction", 0, 23), textBoxItemTempReqRepFaction.Text);
			} else if(btn == buttonItemTempReqRepRank) {
				textBoxItemTempReqRepRank.Text = Classes.PopupHelper.createPopupSelection("Required Reputation Rank", Classes.UtilityHelper.readExcelCSV("ItemReqReputationRank", 0, 1), textBoxItemTempReqRepRank.Text);
			} else if(btn == buttonItemTempReqSpell) {
				textBoxItemTempReqSpell.Text = Classes.PopupHelper.createPopupSelection("Required Spell", Classes.UtilityHelper.readExcelCSV("Spells", 0, 1), textBoxItemTempReqSpell.Text);
			} else if(btn == buttonItemTempMaterial) {
				textBoxItemTempMaterial.Text = Classes.PopupHelper.createPopupSelection("Materials", Classes.UtilityHelper.readExcelCSV("ItemMaterial", 0, 1), textBoxItemTempMaterial.Text);
			} else if(btn == buttonItemTempFoodType) {
				textBoxItemTempFoodType.Text = Classes.PopupHelper.createPopupSelection("Food Type", Classes.UtilityHelper.readExcelCSV("ItemFoodType", 0, 1), textBoxItemTempFoodType.Text);
			} else if(btn == buttonItemTempBagFamily) {
				textBoxItemTempBagFamily.Text = Classes.PopupHelper.createPopupSelection("Bag Family", Classes.UtilityHelper.readExcelCSV("ItemBagFamily", 0, 1), textBoxItemTempBagFamily.Text);
			} else if(btn == buttonItemTempFlagsC) {
				textBoxItemTempFlagsC.Text = Classes.PopupHelper.createPopupSelection("Custom Flags", Classes.UtilityHelper.readExcelCSV("ItemFlagsCustom", 0, 1), textBoxItemTempFlagsC.Text);
			} else if(btn == buttonItemTempTotemCategory) {
				textBoxItemTempTotemCategory.Text = Classes.PopupHelper.createPopupSelection("Totem Category", Classes.UtilityHelper.readExcelCSV("ItemTotemCategory", 0, 1), textBoxItemTempTotemCategory.Text);
			}
		}

		#endregion

		#region Upload/SqlGenerate

		private void toolStripSplitButtonUploadScriptTab(object sender, EventArgs e) {
			ToolStripSplitButton btn = (ToolStripSplitButton) sender;

			int rows = 0;
			ToolStripStatusLabel label = null;

			if(btn == toolStripSplitButtonAMScriptUpdate) {
				DatabaseAuth da = Settings.getAuthDB();

				rows = da.executeSql(textBoxAMScriptOutput.Text);
				label = toolStripStatusLabelAMScriptRows;
			} else if(btn == toolStripSplitButtonCMScriptUpdate) {
				DatabaseCharacters dw = Settings.getCharsDB();

				rows = dw.executeSql(textBoxAMScriptOutput.Text);
				label = toolStripStatusLabelAMScriptRows;
			} else {
				DatabaseWorld dw = Settings.getWorldDB();

				label = toolStripStatusLabelWMScriptRows;
				rows = dw.executeSql(textBoxWMScriptOutput.Text);
			}

			if(label != null) {
				label.Text = "Row(s) affected: " + rows.ToString();
			}
		}
		private void toolStripSplitButtonSqlGenerate(object sender, EventArgs e) {
			ToolStripSplitButton btn = (ToolStripSplitButton) sender;

			var model = Models.WorldManagerModel.getInstance();

			string title = "world_", name = "", entry = "", sql = textBoxWMScriptOutput.Text;

			switch(model.lastGeneratedWorldObject) {
				case Models.WorldManagerModel.worldObject.ITEM:
					title += "creature";
					name = textBoxCreatureTemplateName.Text;
					entry = textBoxCreatureTemplateEntry.Text;
					break;
				case Models.WorldManagerModel.worldObject.CREATURE:
					title += "quest";
					name = textBoxQuestSectionID.Text;
					entry = textBoxQuestSectionLDescription.Text;
					break;
				case Models.WorldManagerModel.worldObject.QUEST:
					title += "gameobject";
					name = textBoxGameObjectTempName.Text;
					entry = textBoxGameObjectTempEntry.Text;
					break;
				case Models.WorldManagerModel.worldObject.GAMEOBJECT:
					title += "item";
					name = textBoxItemTempName.Text;
					entry = textBoxItemTempEntry.Text;
					break;
			}

			string combined = title + "_" + (!string.IsNullOrEmpty(name) ? name + "_" : "") + entry;

			bool fileGenerated = Classes.Generate.SqlGenerate.generateSqlFile(combined, sql);

			if(fileGenerated) {
				btn.Text = "Generate SQL File - Successful!";
			} else {
				btn.Text = "Generate SQL File - Failed!";
			}
		}

		#endregion

		#endregion - - - - - - 

		#region Selfmade CSV
		// I have to create own csv for each category, then check which csv to read...
		// Whenever I feel like I want to do that, I will do it.

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

		private void buttonCMMailSqlGenerate_Click(object sender, EventArgs e) {

		}
	}
}
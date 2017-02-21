using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manti.Classes.ItemTab {
	public class Item {
		public uint entry { get; set; }
		public uint iClass { get; set; }
		public uint iSub { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public uint displayId { get; set; }
		public uint quality { get; set; }
		public int buycount { get; set; }
		public uint inventory { get; set; }
		public int flags { get; set; }
		public int extraFlags { get; set; }
		public int maxCount { get; set; }
		public uint containerSlot { get; set; }
		public uint buyPrice { get; set; }
		public uint sellPrice { get; set; }
		public uint damageType1 { get; set; }
		public uint damageType2 { get; set; }
		public uint damageMin1 { get; set; }
		public uint damageMin2 { get; set; }
		public uint damageMax1 { get; set; }
		public uint damageMax2 { get; set; }
		public float delay { get; set; }
		public uint ammoType { get; set; }
		public uint rangedMod { get; set; }
		public uint itemSet { get; set; }
		public uint bonding { get; set; }
		public int block { get; set; }
		public int durability { get; set; }
		public uint sheath { get; set; }
		public uint reistanceHoly { get; set; }
		public uint reistanceFrost { get; set; }
		public uint reistanceFire { get; set; }
		public uint reistanceShadow { get; set; }
		public uint reistanceNature { get; set; }
		public uint reistanceArcane { get; set; }
		public uint socketColor1 { get; set; }
		public uint socketColor2 { get; set; }
		public uint socketColor3 { get; set; }
		public uint socketContent1 { get; set; }
		public uint socketContent2 { get; set; }
		public uint socketContent3 { get; set; }
		public uint socketBonus { get; set; }
		public uint socketGemProperty { get; set; }

		public uint spellEntry1 { get; set; }
		public uint spellEntry2 { get; set; }
		public uint spellEntry3 { get; set; }
		public uint spellEntry4 { get; set; }
		public uint spellEntry5 { get; set; }
		public int spellTrigger1 { get; set; }
		public int spellTrigger2 { get; set; }
		public int spellTrigger3 { get; set; }
		public int spellTrigger4 { get; set; }
		public int spellTrigger5 { get; set; }
		public int spellCharges1 { get; set; }
		public int spellCharges2 { get; set; }
		public int spellCharges3 { get; set; }
		public int spellCharges4 { get; set; }
		public int spellCharges5 { get; set; }
		public int spellPPMRate1 { get; set; }
		public int spellPPMRate2 { get; set; }
		public int spellPPMRate3 { get; set; }
		public int spellPPMRate4 { get; set; }
		public int spellPPMRate5 { get; set; }
		public float spellCooldown1 { get; set; }
		public float spellCooldown2 { get; set; }
		public float spellCooldown3 { get; set; }
		public float spellCooldown4 { get; set; }
		public float spellCooldown5 { get; set; }
		public int spellCategory1 { get; set; }
		public int spellCategory2 { get; set; }
		public int spellCategory3 { get; set; }
		public int spellCategory4 { get; set; }
		public int spellCategory5 { get; set; }
		public float spellCategoryCooldown1 { get; set; }
		public float spellCategoryCooldown2 { get; set; }
		public float spellCategoryCooldown3 { get; set; }
		public float spellCategoryCooldown4 { get; set; }
		public float spellCategoryCooldown5 { get; set; }

		public uint startQuest { get; set; }
		public int material { get; set; }
		public uint property { get; set; }
		public uint suffix { get; set; }
		public uint area { get; set; }
		public uint map { get; set; }
		public uint disenchantId { get; set; }
		public uint pageText { get; set; }
		public uint languageId { get; set; }
		public uint pageMaterial { get; set; }
		public uint foodType { get; set; }
		public uint lockId { get; set; }
		public uint holidayId { get; set; }
		public uint bagFamily { get; set; }
		public uint modifier { get; set; }
		public uint duration { get; set; }
		public uint limitCategory { get; set; }
		public uint minMoney { get; set; }
		public uint maxMoney { get; set; }
		public uint flagsCustom { get; set; }
		public uint totemCategory { get; set; }

		public int reqRace { get; set; }
		public int reqClass { get; set; }
		public uint reqLevel { get; set; }
		public uint reqSkill { get; set; }
		public uint reqSkillRank { get; set; }
		public uint reqHonorRank { get; set; }
		public uint reqRepFaction { get; set; }
		public uint reqRepRank { get; set; }
		public uint reqDisenchant { get; set; }
		public uint reqSpell { get; set; }
		public uint reqCityRank { get; set; }
		public int reqItemLevel { get; set; }

		public uint statsCount { get; set; }
		public uint statsType1 { get; set; }
		public uint statsType2 { get; set; }
		public uint statsType3 { get; set; }
		public uint statsType4 { get; set; }
		public uint statsType5 { get; set; }
		public uint statsType6 { get; set; }
		public uint statsType7 { get; set; }
		public uint statsType8 { get; set; }
		public uint statsType9 { get; set; }
		public uint statsType10 { get; set; }
		public int statsValue1 { get; set; }
		public int statsValue2 { get; set; }
		public int statsValue3 { get; set; }
		public int statsValue4 { get; set; }
		public int statsValue5 { get; set; }
		public int statsValue6 { get; set; }
		public int statsValue7 { get; set; }
		public int statsValue8 { get; set; }
		public int statsValue9 { get; set; }
		public int statsValue10 { get; set; }
		public int scalingStatDist { get; set; }
		public int scalingStatValue { get; set; }
	}
}

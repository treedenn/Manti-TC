﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manti.Classes.CreatureTab {
	public class Creature {
		public uint entry { get; set; }
		public uint diffEntry1 { get; set; }
		public uint diffEntry2 { get; set; }
		public uint diffEntry3 { get; set; }
		public string name { get; set; }
		public string subname { get; set; }
		public uint modelId1 { get; set; }
		public uint modelId2 { get; set; }
		public uint modelId3 { get; set; }
		public uint modelId4 { get; set; }
		public uint minlevel { get; set; }
		public uint maxlevel { get; set; }
		public uint mingold { get; set; }
		public uint maxgold { get; set; }
		public int killCredit1 { get; set; }
		public int killCredit2 { get; set; }
		public uint rank { get; set; }
		public float scale { get; set; }
		public int faction { get; set; }
		public int npcFlags { get; set; }
		public uint spell1 { get; set; }
		public uint spell2 { get; set; }
		public uint spell3 { get; set; }
		public uint spell4 { get; set; }
		public uint spell5 { get; set; }
		public uint spell6 { get; set; }
		public uint spell7 { get; set; }
		public uint spell8 { get; set; }
		public float modHealth { get; set; }
		public float modMana { get; set; }
		public float modArmor { get; set; }
		public float modDamage { get; set; }
		public float modExp { get; set; }
		public float speedWalk { get; set; }
		public float speedRun { get; set; }
		public float baseAttackTime { get; set; }
		public float rangedAttackTime { get; set; }
		public float bVariance { get; set; }
		public float rVariance { get; set; }
		public byte dSchool { get; set; }
		public string aiName { get; set; }
		public int movementType { get; set; }
		public int inhabitType { get; set; }
		public uint hoverHeight { get; set; }
		public uint gossipMenuId { get; set; }
		public uint movementId { get; set; }
		public string scriptName { get; set; }
		public uint vehicleId { get; set; }
		public int mechanicImmuneMask { get; set; }
		public uint family { get; set; }
		public uint familyType { get; set; }
		public int typeFlags { get; set; }
		public int extraFlags { get; set; }
		public uint unitClass { get; set; }
		public int unitFlags1 { get; set; }
		public int unitFlags2 { get; set; }
		public uint dynamicFlags { get; set; }
		public bool isRegenHealth { get; set; } // checkbox
		public int resistHoly { get; set; }
		public int resistFire { get; set; }
		public int resistNature { get; set; }
		public int resistFrost { get; set; }
		public int resistShadow { get; set; }
		public int resistArcane { get; set; }
		public int trainerType { get; set; }
		public int trainerSpell { get; set; }
		public int trainerClass { get; set; }
		public int trainerRace { get; set; }
		public uint lootId { get; set; }
		public uint pickpocketId { get; set; }
		public uint skinId { get; set; }
	}
}

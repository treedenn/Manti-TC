using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manti.Classes.CharacterTab {
	public class Character {
		// General
		public uint guid { get; set; }
		public uint accountID { get; set; }
		public string name { get; set; }
		public uint charRace { get; set; }
		public uint charClass { get; set; }
		public uint sex { get; set; }
		public uint level { get; set; }
		public uint money { get; set; }
		public uint xp { get; set; }
		public uint chosenTitle { get; set; }
		public bool isOnline { get; set; }
		public bool isCinematic { get; set; }
		public bool isResting { get; set; }
		// Location
		public int mapID { get; set; }
		public int instanceID { get; set; }
		public int zoneID { get; set; }
		public double orientation { get; set; }
		public double xPosition { get; set; }
		public double yPosition { get; set; }
		public double zPosition { get; set; }
		// PvP
		public int honorPoints { get; set; }
		public int arenaPoints { get; set; }
		public int totalKills { get; set; }
		// Health & Powers
		public ulong health { get; set; }
		public ulong power1 { get; set; }
		public ulong power2 { get; set; }
		public ulong power3 { get; set; }
		public ulong power4 { get; set; }
		public ulong power5 { get; set; }
		public ulong power6 { get; set; }
		public ulong power7 { get; set; }
	}
}

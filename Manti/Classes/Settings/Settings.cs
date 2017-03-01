using System;

using Manti.Classes.Database;

namespace Manti.Classes.Settings {
	public class Settings {
		public static void setSetting(Setting setting, string value) {
			Properties.Settings.Default[setting.ToString()] = value;
		}

		public static string getSetting(Setting setting) {
			return Properties.Settings.Default[setting.ToString()].ToString();
		}

		public static void saveSettings() {
			Properties.Settings.Default.Save();
			Properties.Settings.Default.Upgrade();
		}

		public static Database.DatabaseAuth getAuthDB() {
			return new DatabaseAuth(
						getSetting(Setting.Address), getSetting(Setting.Username), Views.FormMySQL.dbPassword,
						Convert.ToUInt16(getSetting(Setting.Port)), getSetting(Setting.DatabaseAuth));
		}

		public static Database.DatabaseCharacters getCharsDB() {
			return new DatabaseCharacters(
						getSetting(Setting.Address), getSetting(Setting.Username), Views.FormMySQL.dbPassword,
						Convert.ToUInt16(getSetting(Setting.Port)), getSetting(Setting.DatabaseCharacters));
		}

		public static Database.DatabaseWorld getWorldDB() {
			return new DatabaseWorld(
						getSetting(Setting.Address), getSetting(Setting.Username), Views.FormMySQL.dbPassword,
						Convert.ToUInt16(getSetting(Setting.Port)), getSetting(Setting.DatabaseWorld));
		}
	}

	public enum Setting {
		Address,
		Username,
		Password,
		Port,
		DatabaseAuth,
		DatabaseCharacters,
		DatabaseWorld,
		SavePassword,
		PathServer
	}
}

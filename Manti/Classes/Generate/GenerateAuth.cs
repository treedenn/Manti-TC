using System;

using Manti.Classes.AccountTab;

namespace Manti.Classes.Generate {
	public class GenerateAuth : SqlGenerate {
		public string createAccount(Account acc) {
			string[] columns = { "username", "sha_pass_hash", "email", "reg_mail", "expansion" };
			object[] values = { acc.username.ToUpper(), acc.password, acc.email, acc.reqemail, acc.expansion };

			return insertToDatabase("account", columns, values);
		}

		public string accountFullToSQL(Account acc) {
			if(acc != null) {
				string query = accountToSQL(acc);
				query += accountBannedToSQL(acc.id, acc.banned);
				query += accountMutedToSQL(acc.id, acc.muted);
				query += accountAccessToSQL(acc.id, acc.access);

				return query;
			}

			return null;
		}

		public string accountToSQL(Account acc) {
			if(acc.id != 0) {
				string[] columns = {"username", "sha_pass_hash", "email", "reg_mail", "expansion", "locked"};
				object[] values = {acc.username.ToUpper(), acc.password, acc.email, acc.reqemail, acc.expansion, Convert.ToSByte(acc.locked)};

				return updateToDatabase("account", columns, values, "id", acc.id.ToString());
			}

			return null;
		}

		public string accountBannedToSQL(uint id, AccountBanned ba) {
			if(ba != null) {
				string[] columns = {"id", "bandate", "unbandate", "bannedby", "banreason", "active"};
				object[] values = {id, UtilityHelper.DateTimeToUnixStamp(ba.banDate), UtilityHelper.DateTimeToUnixStamp(ba.unbanDate),
				ba.by, ba.reason, Convert.ToSByte(ba.isActive)};

				return deleteFromDatabase("account_banned", "id", id.ToString()) + insertToDatabase("account_banned", columns, values);
			}

			return null;
		}

		public string accountMutedToSQL(uint id, AccountMuted am) {
			if(am != null) {
				string[] columns = {"guid", "mutedate", "mutetime", "mutedby", "mutereason"};
				object[] values = {id, UtilityHelper.DateTimeToUnixStamp(am.muteDate), am.duration, am.by, am.reason};

				return deleteFromDatabase("account_muted", "guid", id.ToString()) + insertToDatabase("account_muted", columns, values);
			}

			return null;
		}

		public string accountAccessToSQL(uint id, AccountAccess[] aa) {
			if(aa != null) {
				string[] columns = {"id", "gmlevel", "RealmID"};
				object[] values;

				string query = deleteFromDatabase("account_access", "id", id);

				foreach(AccountAccess access in aa) {
					values = new object[] { id, access.gmLevel, access.realmID };
					query += insertToDatabase("account_access", columns, values);
				}

				return query;
			}

			return null;
		}


	}
}

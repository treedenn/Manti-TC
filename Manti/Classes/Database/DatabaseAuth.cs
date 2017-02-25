using System;
using System.Data;
using MySql.Data.MySqlClient;

using Manti.Classes.AccountTab;

namespace Manti.Classes.Database {
	public class DatabaseAuth : MySqlDatabase {
		public DatabaseAuth(string address, string username, string password, uint port, string dbName)
			: base(address, username, password, port, dbName) {

		}

		public int uploadSql(string sql) {
			return executeNonQuery(sql);
		}

		public DataTable searchForAccounts(string name, bool isExact) {
			var query = "SELECT id, username, email, expansion FROM account WHERE username ";
			query += (isExact ? "= '?user'" : "LIKE '%?user%'") + " ORDER BY id;";

			var dt = executeQuery(query, new MySqlParameter("?user", name));

			return dt;
		}

		public DataTable searchForAccountByID(uint id) {
			var dt = executeQuery("SELECT id, username, email, expansion FROM account WHERE id = '?id' ORDER BY id;", new MySqlParameter("?id", id));

			return dt;
		}

		public DataTable searchForAllAccounts() {
			var dt = executeQuery("SELECT id, username, email, expansion FROM account ORDER BY id;");

			return dt;
		}

		public Account getAccount(uint accID) {
			Account account = null;

			DataTable dtAcc    = executeQuery("SELECT * FROM account WHERE id = '?id';", new MySqlParameter("?id", accID));
			DataTable dtBan    = executeQuery("SELECT * FROM account_banned WHERE id = '?id';", new MySqlParameter("?id", accID));
			DataTable dtMute   = executeQuery("SELECT * FROM account_muted WHERE guid = '?guid';", new MySqlParameter("?guid", accID));
			DataTable dtAccess = executeQuery("SELECT * FROM account_access WHERE id = '?id';", new MySqlParameter("?id", accID));

			if(dtAcc != null) {
				account = buildAccount(dtAcc.Rows[0]);
				account.banned = buildAccountBanned(dtBan.Rows[0]);
				account.muted = buildAccountMuted(dtMute.Rows[0]);
				account.access = buildAccountAccess(dtAccess);
			}

			return account;
		}

		public Account buildFullAccount(DataRow accRow, DataRow accBan, DataRow accMute) {
			Account account = buildAccount(accRow);
			account.banned = buildAccountBanned(accBan);
			account.muted = buildAccountMuted(accMute);

			return account;
		}

		public Account buildAccount(DataRow accRow) {
			var account = new Account();

			account.id         = Convert.ToUInt32(accRow["id"]);
			account.username   = accRow["username"].ToString();
			account.email      = accRow["email"].ToString();
			account.reqemail   = accRow["reg_mail"].ToString();
			account.joindate   = accRow["joindate"].ToString();
			account.lastIP     = accRow["last_ip"].ToString();
			account.locked     = Convert.ToBoolean(accRow["locked"]);
			account.online     = Convert.ToBoolean(accRow["online"]);
			account.expansion  = Convert.ToByte((accRow["expansion"]));

			return account;
		}
		
		public AccountBanned buildAccountBanned(DataRow accBan) {
			var accountBanned = new AccountBanned();

			accountBanned.banDate   = UtilityHelper.UnixStampToDateTime(accBan["bandate"].ToString());
			accountBanned.unbanDate = UtilityHelper.UnixStampToDateTime(accBan["unbandate"].ToString());
			accountBanned.by        = accBan["bannedby"].ToString();
			accountBanned.reason    = accBan["banreason"].ToString();
			accountBanned.isActive  = Convert.ToBoolean(accBan["active"]);

			return accountBanned;
		}

		public AccountMuted buildAccountMuted(DataRow accMute) {
			var accountMuted = new AccountMuted();

			accountMuted.muteDate   = UtilityHelper.UnixStampToDateTime(accMute["mutedate"].ToString());
			accountMuted.duration   = Convert.ToDouble(accMute["mutetime"]);
			accountMuted.by         = accMute["mutedby"].ToString();
			accountMuted.reason     = accMute["mutereason"].ToString();

			return accountMuted;
		}

		public AccountAccess[] buildAccountAccess(DataTable accAccess) {
			var accountAccess = new AccountAccess[accAccess.Rows.Count];

			for(var i = 0; i < accAccess.Rows.Count; i++) {
				var access = new AccountAccess();

				DataRow dr = accAccess.Rows[i];

				access.id      = Convert.ToUInt32(dr["id"].ToString());
				access.gmLevel = Convert.ToInt32(dr["gmlevel"].ToString());
				access.realmID = Convert.ToInt32(dr["RealmID"].ToString());

				accountAccess[i] = access;
			}

			return accountAccess;
		}
	}
}

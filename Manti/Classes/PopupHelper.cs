using System;
using System.Data;

using Manti.Views.FormPopup;

namespace Manti.Classes {
	public class PopupHelper {
		/// <summary>
		/// Creates a popup, where the user can select only one row.
		/// </summary>
		/// <param name="formTitle">Changes the popup title</param>
		/// <param name="data">This sends the data to the listview/datagrid used in the popup</param>
		/// <param name="currentValue">Highlights the current value</param>
		/// <returns>It returns the selected /= current value (the ID)</returns>
		public static string createPopupSelection(string formTitle, string csvName, int csvId, int csvValue, string currentValue, bool loadImmediately) {
			var popupDialog = new FormPopupSelection();

			popupDialog.setFormTitle       = formTitle;
			popupDialog.changeSelection    = currentValue;
			popupDialog.setCsvName         = csvName;
			popupDialog.setCsvId           = csvId;
			popupDialog.setCsvValue        = csvValue;
			popupDialog.setLoad            = loadImmediately;
			popupDialog.ShowDialog();

			currentValue = (string.IsNullOrEmpty(popupDialog.changeSelection)) ? currentValue : popupDialog.changeSelection;
			popupDialog.Dispose();

			GC.Collect();
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
		public static string createPopupChecklist(string formTitle, DataTable data, string currentValue, bool bitMask = false) {
			var popupDialog = new FormPopupCheckboxList();

			popupDialog.setFormTitle = formTitle;
			popupDialog.setDataTable = data;
			popupDialog.usedValue    = (currentValue == string.Empty) ? "0" : currentValue;
			popupDialog.setBitmask   = bitMask;
			popupDialog.ShowDialog();

			currentValue = (string.IsNullOrEmpty(popupDialog.usedValue) ? currentValue : popupDialog.usedValue);
			popupDialog.Dispose();

			GC.Collect();
			return currentValue;
		}

		/// <summary>
		/// Similar to selection and checklist popup, however, is it used for entities (items, creatures & gameobjects)
		/// </summary>
		/// <param name="currentValue">Highlights the current value</param>
		/// <param name="disableEntity">Used to disable or enable radiobuttons {items, creatures, gameobjects} in that order</param>
		/// <returns>It returns the selected /= current value (the ID)</returns>
		public static string createPopupEntity(string currentValue, bool[] disableEntity, bool outputID = true) {
			var popupDialog = new FormPopupEntities();
			DataSet entities = new DataSet();

			popupDialog.changeSelection = (string.IsNullOrEmpty(currentValue)) ? "0" : currentValue;
			popupDialog.changeOutput    = outputID;
			popupDialog.disableEntity   = disableEntity;

			/*
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
			*/

			popupDialog.ShowDialog();

			currentValue = (popupDialog.changeSelection == "") ? currentValue : popupDialog.changeSelection;

			entities.Dispose();
			popupDialog.Dispose();

			GC.Collect();
			return currentValue;
		}
	}
}

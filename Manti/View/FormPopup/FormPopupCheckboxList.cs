using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Manti.Views.FormPopup {
	public partial class FormPopupCheckboxList : Form {
		public FormPopupCheckboxList() {
			InitializeComponent();
		}

		private string tbValue = "";
		private bool bitmaskEnabled = false;
		private bool isMinusOne = false;
		private DataTable cblOptions;

		#region Functions

		public string setFormTitle {
			set { this.Text = value; }
		}
		public string usedValue {
			set { tbValue = value; }
			get { return tbValue; }
		}
		public bool setBitmask {
			set { bitmaskEnabled = value; }
		}
		public DataTable setDataTable {
			set { cblOptions = value; }
		}

		/// <summary>
		/// Adds the rows to the checklist from dataTable.
		/// </summary>
		private void AddItems() {
			foreach(DataRow row in cblOptions.Rows) {
				checkedListBoxPopupValues.Items.Add(row[1]);
			}
		}
		/// <summary>
		/// Calculates BitMask based on checked items and their ID.
		/// </summary>
		/// <returns></returns>
		private ulong CalculateBitmask() {
			double bitmaskValue = 0;

			var SelectedItems = new List<int>();

			// Loops through every checked items and add them to list.
			foreach(object o in checkedListBoxPopupValues.CheckedItems) {
				SelectedItems.Add(checkedListBoxPopupValues.Items.IndexOf(o));
			}

			// When all checked items have been found, go calculate the total bitmask based on ID.
			foreach(int i in SelectedItems) {
				bitmaskValue += Math.Pow(2, Convert.ToInt64(cblOptions.Rows[i][0]) - 1);
			}

			return Convert.ToUInt64(bitmaskValue);
		}
		private List<ulong> ReverseBitmask(string value) {
			var storeID = new List<ulong>();

			if(value != "-1") {
				Double newValue = Convert.ToDouble(value);

				while(newValue > 0) {
					for(ulong i = 0; Math.Pow(2, i) <= newValue; i++) {
						if(Math.Pow(2, i + 1) > newValue) {
							newValue -= Math.Pow(2, i);
							storeID.Add(i + 1);
						}
					}
				}
			} else {
				isMinusOne = true;
			}

			return storeID;
		}
		private void CheckListBoxes(List<ulong> ID) {
			if(isMinusOne) {
				for(var index = 0; index < checkedListBoxPopupValues.Items.Count; index++) {
					checkedListBoxPopupValues.SetItemChecked(index, true);
				}
			} else {
				foreach(int i in ID) {
					for(var index = 0; index < checkedListBoxPopupValues.Items.Count; index++) {
						if(cblOptions.Rows[index][0].ToString() == i.ToString()) {
							checkedListBoxPopupValues.SetItemChecked(index, true);
						}
					}
				}
			}
		}

		#endregion
		#region Events
		private void FormPopupCheckboxList_Load(object sender, EventArgs e) {
			AddItems();

			if(bitmaskEnabled == true) { CheckListBoxes(ReverseBitmask(tbValue)); }
		}
		private void buttonPopupOK_Click(object sender, EventArgs e) {
			if(bitmaskEnabled == true) {
				tbValue = CalculateBitmask().ToString();
			}

			this.Close();
		}
		private void buttonPopupClose_Click(object sender, EventArgs e) {
			this.Close();
		}
		#endregion

	}
}

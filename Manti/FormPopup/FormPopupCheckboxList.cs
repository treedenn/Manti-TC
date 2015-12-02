using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manti.FormPopup
{
    public partial class FormPopupCheckboxList : Form
    {
        public FormPopupCheckboxList()
        {
            InitializeComponent();
        }

        private int CheckValue = 0;
        private DataTable dtOptions;
        private bool BitMask = false;

        #region Functions

        public int setValue
        {
            set { CheckValue = value; }
        }
        public DataTable setDataTable
        {
            set { dtOptions = value; }
        }
        public bool setBitMask
        {
            set { BitMask = value; }
        }
        public string setFormTitle
        {
            set { this.Text = value; }
        }
        public string getValue
        {
            get { return CheckValue.ToString(); }
        }

        private void AddItems()
        {
            foreach (DataRow row in dtOptions.Rows)
            {
                checkedListBoxPopupValues.Items.Add(row["name"]);
            }
        }
        private int CalculateBitMask()
        {
            double value = 0;

            if (checkedListBoxPopupValues.CheckedItems.Count != checkedListBoxPopupValues.Items.Count)
            {
                var SelectedItemsIndexes = new List<int>();
                foreach (object o in checkedListBoxPopupValues.CheckedItems)
                {
                    SelectedItemsIndexes.Add(checkedListBoxPopupValues.Items.IndexOf(o));
                }

                foreach (int i in SelectedItemsIndexes)
                {
                    value += Math.Pow(2, Convert.ToInt32(dtOptions.Rows[i][0]) - 1);
                }
            } else
            {
                value = -1;
            }

            //MessageBox.Show(value.ToString(), "");

            return Convert.ToInt32(value);
        }
        private List<int> ReverseBitMask(int value) 
        {
            var storeID = new List<int>();
            var newValue = Convert.ToDouble(value);

            if (newValue.ToString() != "-1")
            {
                while (newValue > 0)
                {
                    for (int i = 0; Math.Pow(2, i) <= newValue; i++)
                    {
                        if (Math.Pow(2, i+1) > newValue)
                        {
                            newValue -= Math.Pow(2, i);
                            storeID.Add(i+1);
                        }
                    }
                }
            } else
            {
                storeID.Add(-1);
            }

            return storeID;
        }
        private void CheckedListBoxes(List<int> ID)
        {
            foreach (int i in ID)
            {
                if (i == -1)
                {
                    for (var index = 0; index < checkedListBoxPopupValues.Items.Count; index++)
                    {
                        checkedListBoxPopupValues.SetItemChecked(index, true);
                    }

                    break;
                } else
                {
                    for (var index = 0; index < checkedListBoxPopupValues.Items.Count; index++)
                    {
                        if (dtOptions.Rows[index][0].ToString() == i.ToString())
                        {
                            checkedListBoxPopupValues.SetItemChecked(index, true);
                        }
                    }
                }
            }
        }

        #endregion

        private void FormPopupCheckboxList_Load(object sender, EventArgs e)
        {
            AddItems();

            if (BitMask == true) { CheckedListBoxes(ReverseBitMask(CheckValue)); }
        }
        private void buttonPopupOK_Click(object sender, EventArgs e)
        {
            if (checkedListBoxPopupValues.CheckedItems.Count > 0)
            {
                if (BitMask == true)
                {
                    CheckValue = CalculateBitMask();
                }

                this.Close();
            }
        }
        private void buttonPopupClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manti.Views {
	public partial class FormAbout : Form {
		public FormAbout() {
			InitializeComponent();
		}

		private void linkLabelAboutSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("https://github.com/Heitx/Manti-TC");
		}

		private void buttonAboutOK_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}

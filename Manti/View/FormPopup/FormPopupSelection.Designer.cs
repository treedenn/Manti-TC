namespace Manti.Views.FormPopup {
	partial class FormPopupSelection {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.textBoxPopupSearchValue = new System.Windows.Forms.TextBox();
			this.buttonPopupOK = new System.Windows.Forms.Button();
			this.buttonPopupClose = new System.Windows.Forms.Button();
			this.listViewPopupSelection = new System.Windows.Forms.ListView();
			this.columnHeaderTICID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderTICValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.textBoxPopupSearchID = new System.Windows.Forms.TextBox();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxPopupSearchValue
			// 
			this.textBoxPopupSearchValue.Location = new System.Drawing.Point(78, 12);
			this.textBoxPopupSearchValue.Name = "textBoxPopupSearchValue";
			this.textBoxPopupSearchValue.Size = new System.Drawing.Size(268, 20);
			this.textBoxPopupSearchValue.TabIndex = 1;
			// 
			// buttonPopupOK
			// 
			this.buttonPopupOK.Location = new System.Drawing.Point(193, 376);
			this.buttonPopupOK.Name = "buttonPopupOK";
			this.buttonPopupOK.Size = new System.Drawing.Size(179, 23);
			this.buttonPopupOK.TabIndex = 2;
			this.buttonPopupOK.Text = "OK";
			this.buttonPopupOK.UseVisualStyleBackColor = true;
			this.buttonPopupOK.Click += new System.EventHandler(this.buttonPopupOK_Click);
			// 
			// buttonPopupClose
			// 
			this.buttonPopupClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonPopupClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonPopupClose.Location = new System.Drawing.Point(12, 376);
			this.buttonPopupClose.Name = "buttonPopupClose";
			this.buttonPopupClose.Size = new System.Drawing.Size(179, 23);
			this.buttonPopupClose.TabIndex = 2;
			this.buttonPopupClose.Text = "Close";
			this.buttonPopupClose.UseVisualStyleBackColor = true;
			this.buttonPopupClose.Click += new System.EventHandler(this.buttonPopupClose_Click);
			// 
			// listViewPopupSelection
			// 
			this.listViewPopupSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewPopupSelection.BackColor = System.Drawing.SystemColors.Control;
			this.listViewPopupSelection.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listViewPopupSelection.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTICID,
            this.columnHeaderTICValue});
			this.listViewPopupSelection.FullRowSelect = true;
			this.listViewPopupSelection.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewPopupSelection.HideSelection = false;
			this.listViewPopupSelection.Location = new System.Drawing.Point(12, 38);
			this.listViewPopupSelection.MultiSelect = false;
			this.listViewPopupSelection.Name = "listViewPopupSelection";
			this.listViewPopupSelection.Size = new System.Drawing.Size(360, 332);
			this.listViewPopupSelection.TabIndex = 3;
			this.listViewPopupSelection.UseCompatibleStateImageBehavior = false;
			this.listViewPopupSelection.View = System.Windows.Forms.View.Details;
			this.listViewPopupSelection.DoubleClick += new System.EventHandler(this.listViewPopupSelection_DoubleClick);
			// 
			// columnHeaderTICID
			// 
			this.columnHeaderTICID.Tag = "";
			this.columnHeaderTICID.Text = "ID";
			// 
			// columnHeaderTICValue
			// 
			this.columnHeaderTICValue.Tag = "";
			this.columnHeaderTICValue.Text = "Value";
			this.columnHeaderTICValue.Width = 500;
			// 
			// textBoxPopupSearchID
			// 
			this.textBoxPopupSearchID.Location = new System.Drawing.Point(12, 12);
			this.textBoxPopupSearchID.Name = "textBoxPopupSearchID";
			this.textBoxPopupSearchID.Size = new System.Drawing.Size(60, 20);
			this.textBoxPopupSearchID.TabIndex = 4;
			// 
			// buttonSearch
			// 
			this.buttonSearch.BackgroundImage = global::Manti.Properties.Resources.iconSearch;
			this.buttonSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonSearch.Location = new System.Drawing.Point(352, 12);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(20, 20);
			this.buttonSearch.TabIndex = 5;
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// FormPopupSelection
			// 
			this.AcceptButton = this.buttonSearch;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonPopupClose;
			this.ClientSize = new System.Drawing.Size(384, 411);
			this.ControlBox = false;
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.textBoxPopupSearchID);
			this.Controls.Add(this.listViewPopupSelection);
			this.Controls.Add(this.buttonPopupClose);
			this.Controls.Add(this.buttonPopupOK);
			this.Controls.Add(this.textBoxPopupSearchValue);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FormPopupSelection";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Selection Mode";
			this.Load += new System.EventHandler(this.FormPopupSelection_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox textBoxPopupSearchValue;
		private System.Windows.Forms.Button buttonPopupOK;
		private System.Windows.Forms.Button buttonPopupClose;
		private System.Windows.Forms.ListView listViewPopupSelection;
		private System.Windows.Forms.ColumnHeader columnHeaderTICID;
		private System.Windows.Forms.ColumnHeader columnHeaderTICValue;
		private System.Windows.Forms.TextBox textBoxPopupSearchID;
		private System.Windows.Forms.Button buttonSearch;
	}
}
namespace Manti.FormPopup
{
    partial class FormPopupCheckboxList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonPopupClose = new System.Windows.Forms.Button();
            this.buttonPopupOK = new System.Windows.Forms.Button();
            this.checkedListBoxPopupValues = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // buttonPopupClose
            // 
            this.buttonPopupClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPopupClose.Location = new System.Drawing.Point(193, 376);
            this.buttonPopupClose.Name = "buttonPopupClose";
            this.buttonPopupClose.Size = new System.Drawing.Size(179, 23);
            this.buttonPopupClose.TabIndex = 3;
            this.buttonPopupClose.Text = "Close";
            this.buttonPopupClose.UseVisualStyleBackColor = true;
            this.buttonPopupClose.Click += new System.EventHandler(this.buttonPopupClose_Click);
            // 
            // buttonPopupOK
            // 
            this.buttonPopupOK.Location = new System.Drawing.Point(12, 376);
            this.buttonPopupOK.Name = "buttonPopupOK";
            this.buttonPopupOK.Size = new System.Drawing.Size(179, 23);
            this.buttonPopupOK.TabIndex = 4;
            this.buttonPopupOK.Text = "OK";
            this.buttonPopupOK.UseVisualStyleBackColor = true;
            this.buttonPopupOK.Click += new System.EventHandler(this.buttonPopupOK_Click);
            // 
            // checkedListBoxPopupValues
            // 
            this.checkedListBoxPopupValues.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBoxPopupValues.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBoxPopupValues.CheckOnClick = true;
            this.checkedListBoxPopupValues.FormattingEnabled = true;
            this.checkedListBoxPopupValues.HorizontalScrollbar = true;
            this.checkedListBoxPopupValues.Location = new System.Drawing.Point(12, 12);
            this.checkedListBoxPopupValues.Name = "checkedListBoxPopupValues";
            this.checkedListBoxPopupValues.Size = new System.Drawing.Size(360, 345);
            this.checkedListBoxPopupValues.TabIndex = 5;
            // 
            // FormPopupCheckboxList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 411);
            this.ControlBox = false;
            this.Controls.Add(this.checkedListBoxPopupValues);
            this.Controls.Add(this.buttonPopupClose);
            this.Controls.Add(this.buttonPopupOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormPopupCheckboxList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkbox List";
            this.Load += new System.EventHandler(this.FormPopupCheckboxList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPopupClose;
        private System.Windows.Forms.Button buttonPopupOK;
        private System.Windows.Forms.CheckedListBox checkedListBoxPopupValues;
    }
}
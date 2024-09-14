namespace Real_Estate_Management.CustomElements
{
    partial class FlyoutReserveContent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RadTxtReserveReason = new Telerik.WinControls.UI.RadTextBox();
            this.radLabelTxt = new Telerik.WinControls.UI.RadLabel();
            this.radButtonOK = new Telerik.WinControls.UI.RadButton();
            this.radButtonCancel = new Telerik.WinControls.UI.RadButton();
            this.radLabelHeader = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.RadTxtReserveReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // RadTxtReserveReason
            // 
            this.RadTxtReserveReason.Dock = System.Windows.Forms.DockStyle.Top;
            this.RadTxtReserveReason.Location = new System.Drawing.Point(0, 95);
            this.RadTxtReserveReason.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.RadTxtReserveReason.Name = "RadTxtReserveReason";
            this.RadTxtReserveReason.NullText = "ادخل سبب الحجز";
            this.RadTxtReserveReason.Size = new System.Drawing.Size(347, 20);
            this.RadTxtReserveReason.TabIndex = 0;
            // 
            // radLabelTxt
            // 
            this.radLabelTxt.AutoSize = false;
            this.radLabelTxt.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabelTxt.Location = new System.Drawing.Point(0, 60);
            this.radLabelTxt.Name = "radLabelTxt";
            this.radLabelTxt.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.radLabelTxt.Size = new System.Drawing.Size(347, 35);
            this.radLabelTxt.TabIndex = 2;
            this.radLabelTxt.Text = "سبب الحجز : ";
            this.radLabelTxt.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // radButtonOK
            // 
            this.radButtonOK.Location = new System.Drawing.Point(110, 147);
            this.radButtonOK.Name = "radButtonOK";
            this.radButtonOK.Size = new System.Drawing.Size(88, 24);
            this.radButtonOK.TabIndex = 4;
            this.radButtonOK.Text = "تأكيد";
            this.radButtonOK.Click += new System.EventHandler(this.RadButtonOK_Click);
            // 
            // radButtonCancel
            // 
            this.radButtonCancel.Location = new System.Drawing.Point(15, 147);
            this.radButtonCancel.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.radButtonCancel.Name = "radButtonCancel";
            this.radButtonCancel.Size = new System.Drawing.Size(82, 24);
            this.radButtonCancel.TabIndex = 5;
            this.radButtonCancel.Text = "إلغاء";
            this.radButtonCancel.Click += new System.EventHandler(this.RadButtonCancel_Click);
            // 
            // radLabelHeader
            // 
            this.radLabelHeader.AutoSize = false;
            this.radLabelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabelHeader.Font = new System.Drawing.Font("Segoe UI Semilight", 15F);
            this.radLabelHeader.Location = new System.Drawing.Point(0, 0);
            this.radLabelHeader.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.radLabelHeader.Name = "radLabelHeader";
            this.radLabelHeader.Padding = new System.Windows.Forms.Padding(0, 0, 25, 0);
            this.radLabelHeader.Size = new System.Drawing.Size(347, 60);
            this.radLabelHeader.TabIndex = 3;
            this.radLabelHeader.Text = "تأكيد حجز بطاقة الأرض";
            this.radLabelHeader.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // FlyoutReserveContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radButtonCancel);
            this.Controls.Add(this.RadTxtReserveReason);
            this.Controls.Add(this.radButtonOK);
            this.Controls.Add(this.radLabelTxt);
            this.Controls.Add(this.radLabelHeader);
            this.Name = "FlyoutReserveContent";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(347, 193);
            ((System.ComponentModel.ISupportInitialize)(this.RadTxtReserveReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelHeader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Telerik.WinControls.UI.RadButton radButtonOK;
        private Telerik.WinControls.UI.RadButton radButtonCancel;
        public Telerik.WinControls.UI.RadTextBox RadTxtReserveReason;
        public Telerik.WinControls.UI.RadLabel radLabelTxt;
        public Telerik.WinControls.UI.RadLabel radLabelHeader;
    }
}

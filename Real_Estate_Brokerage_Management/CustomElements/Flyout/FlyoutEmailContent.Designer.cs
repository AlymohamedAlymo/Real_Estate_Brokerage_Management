namespace DoctorERP.CustomElements.Flyout
{
    partial class FlyoutEmailContent
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
            this.radTextBoxFromMail = new Telerik.WinControls.UI.RadTextBox();
            this.radButtonCancel = new Telerik.WinControls.UI.RadButton();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radButtonOK = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radTextBoxPassword = new Telerik.WinControls.UI.RadTextBox();
            this.radTextBoxToMail = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxFromMail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxToMail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            this.SuspendLayout();
            // 
            // radTextBoxFromMail
            // 
            this.radTextBoxFromMail.Location = new System.Drawing.Point(27, 60);
            this.radTextBoxFromMail.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.radTextBoxFromMail.Name = "radTextBoxFromMail";
            this.radTextBoxFromMail.NullText = "ادخل الإيميل المرٌسل منه";
            this.radTextBoxFromMail.Size = new System.Drawing.Size(327, 20);
            this.radTextBoxFromMail.TabIndex = 0;
            // 
            // radButtonCancel
            // 
            this.radButtonCancel.Location = new System.Drawing.Point(27, 194);
            this.radButtonCancel.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.radButtonCancel.Name = "radButtonCancel";
            this.radButtonCancel.Size = new System.Drawing.Size(77, 24);
            this.radButtonCancel.TabIndex = 5;
            this.radButtonCancel.Text = "إلغاء";
            this.radButtonCancel.Click += new System.EventHandler(this.RadButtonCancel_Click);
            // 
            // radLabel3
            // 
            this.radLabel3.AutoSize = false;
            this.radLabel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel3.Font = new System.Drawing.Font("Segoe UI Semilight", 15F);
            this.radLabel3.Location = new System.Drawing.Point(0, 0);
            this.radLabel3.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(464, 38);
            this.radLabel3.TabIndex = 3;
            this.radLabel3.Text = "بيانات إرسال الإيميل";
            this.radLabel3.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // radButtonOK
            // 
            this.radButtonOK.Location = new System.Drawing.Point(117, 194);
            this.radButtonOK.Name = "radButtonOK";
            this.radButtonOK.Size = new System.Drawing.Size(83, 24);
            this.radButtonOK.TabIndex = 4;
            this.radButtonOK.Text = "إرسال";
            this.radButtonOK.Click += new System.EventHandler(this.RadButtonOK_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(354, 61);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(96, 18);
            this.radLabel1.TabIndex = 2;
            this.radLabel1.Text = "إيميل المرٌسل منه : ";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(355, 99);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(95, 18);
            this.radLabel2.TabIndex = 6;
            this.radLabel2.Text = "كلمة مرور الإيميل : ";
            this.radLabel2.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // radTextBoxPassword
            // 
            this.radTextBoxPassword.Location = new System.Drawing.Point(141, 98);
            this.radTextBoxPassword.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.radTextBoxPassword.Name = "radTextBoxPassword";
            this.radTextBoxPassword.NullText = "ادخل كلمة مرور الإيميل المرٌسل منه";
            this.radTextBoxPassword.PasswordChar = '*';
            this.radTextBoxPassword.Size = new System.Drawing.Size(213, 20);
            this.radTextBoxPassword.TabIndex = 1;
            // 
            // radTextBoxToMail
            // 
            this.radTextBoxToMail.Location = new System.Drawing.Point(27, 138);
            this.radTextBoxToMail.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.radTextBoxToMail.Name = "radTextBoxToMail";
            this.radTextBoxToMail.NullText = "ادخل الإيميل المرسل إليه";
            this.radTextBoxToMail.Size = new System.Drawing.Size(327, 20);
            this.radTextBoxToMail.TabIndex = 7;
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(354, 139);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(96, 18);
            this.radLabel4.TabIndex = 8;
            this.radLabel4.Text = "إيميل المرسل إليه : ";
            this.radLabel4.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // FlyoutEmailContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radTextBoxToMail);
            this.Controls.Add(this.radLabel4);
            this.Controls.Add(this.radButtonOK);
            this.Controls.Add(this.radButtonCancel);
            this.Controls.Add(this.radTextBoxFromMail);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radTextBoxPassword);
            this.Controls.Add(this.radLabel3);
            this.Name = "FlyoutEmailContent";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(464, 242);
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxFromMail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBoxToMail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Telerik.WinControls.UI.RadTextBox radTextBoxFromMail;
        private Telerik.WinControls.UI.RadButton radButtonCancel;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadButton radButtonOK;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox radTextBoxPassword;
        private Telerik.WinControls.UI.RadTextBox radTextBoxToMail;
        private Telerik.WinControls.UI.RadLabel radLabel4;
    }
}

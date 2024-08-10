using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace DoctorERP.CustomElements.Flyout
{
    public partial class FlyoutEmailContent : UserControl
    {
        public FlyoutEmailContent()
        {
            InitializeComponent();
            this.Result = DialogResult.Cancel;
            radLabel3.TextAlignment = ContentAlignment.TopLeft;

        }

        public DialogResult Result
        {
            get; set;
        }

        public string FromMail
        {
            get { return this.radTextBoxFromMail.Text; }
        }
        public string PassWord
        {
            get { return this.radTextBoxPassword.Text; }
        }

        public string ToMail
        {
            get { return this.radTextBoxToMail.Text; }
        }


        private void RadButtonOK_Click(object sender, EventArgs e)
        {
            if (!this.ValidateData())
            {
                return;
            }

            this.Result = DialogResult.OK;
            RadFlyoutManager.Close();
        }

        private void RadButtonCancel_Click(object sender, EventArgs e)
        {
            this.Result = DialogResult.Cancel;
            RadFlyoutManager.Close();
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(this.FromMail) || string.IsNullOrWhiteSpace(this.PassWord) || string.IsNullOrWhiteSpace(this.ToMail))
            {
                return false;
            }

            return true;
        }

    }
}

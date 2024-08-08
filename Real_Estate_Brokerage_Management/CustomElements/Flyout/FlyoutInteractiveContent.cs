using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace DoctorERP
{
    public partial class FlyoutInteractiveContent : UserControl
    {
        public FlyoutInteractiveContent()
        {
            InitializeComponent();
            this.Result = DialogResult.Cancel;
        }

        public DialogResult Result
        {
            get; set;
        }

        public string FirstName
        {
            get { return this.radTextBoxFirstName.Text; }
        }

        public string LastName
        {
            get { return this.radTextBoxLastName.Text; }
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
            if (string.IsNullOrWhiteSpace(this.FirstName) ||
                string.IsNullOrWhiteSpace(this.LastName))
            {
                return false;
            }

            return true;
        }
    }
}
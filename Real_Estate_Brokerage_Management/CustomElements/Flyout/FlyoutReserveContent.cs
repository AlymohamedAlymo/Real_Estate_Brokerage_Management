using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace DoctorERP
{
    public partial class FlyoutReserveContent : UserControl
    {
        public FlyoutReserveContent()
        {
            InitializeComponent();
            this.Result = DialogResult.Cancel;
        }

        public DialogResult Result
        {
            get; set;
        }

        public string ReserveReason
        {
            get { return this.RadTextboxReserveReason.Text; }
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
            if (string.IsNullOrWhiteSpace(this.ReserveReason))
            {
                return false;
            }

            return true;
        }

    }
}
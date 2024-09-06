using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace DoctorERP.Forms
{
    public partial class frPlans : Telerik.WinControls.UI.RadForm
    {
        public frPlans()
        {
            InitializeComponent();

            tbPlanInfo.Fill();

            var dataSource = tbPlanInfo.lstData;

            //radDataEntry1.DataSource = dataSource;
            //radBindingNavigator1.BindingSource = dataSource;


        }

        private void frPlans_Load(object sender, EventArgs e)
        {

        }

        private void radDataEntry1_Click(object sender, EventArgs e)
        {

        }
    }
}

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Real_Estate_Management.CustomElements
{
    public partial class FlyoutTransferData : UserControl
    {
        private static string UpdateScript = global::Real_Estate_Management.Properties.Resources.UpdateDataBase;

        public DialogResult Result
        {
            get; set;
        }


        public FlyoutTransferData()
        {
            InitializeComponent();

            this.radPictureBox2.ShowBorder = false;
            this.radWaitingBar1.StartWaiting();
            this.radWaitingBar1.StartWaiting();

        }


        private bool UpdateDataBase()
        {
            try
            {
                IEnumerable<string> commandStrings2 = Regex.Split(UpdateScript, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                foreach (string strQuery in commandStrings2)
                {
                    if (strQuery.Trim().Length > 0)
                    {

                        try
                        {
                            DBConnect.DBCommand = new SqlCommand(strQuery, DBConnect.DBConnection);
                            DBConnect.DBCommand.ExecuteNonQuery();
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }


                return true;
            }
            catch (Exception) { return false; }

        }

        private void FlyoutTransferData_Load(object sender, EventArgs e)
        {
            timer1.Start();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (UpdateDataBase())
            {
                this.Result = DialogResult.OK;
                RadFlyoutManager.Close();
            }
            else
            {
                this.Result = DialogResult.Cancel;
                RadFlyoutManager.Close();
            }

        }
    }
}

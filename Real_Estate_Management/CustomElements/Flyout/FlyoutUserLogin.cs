using DoctorHelper.Messages;
using SmartArabXLSX.Vml.Office;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Contract_Management.Dialogs
{
    public partial class FlyoutUserLogin : UserControl
    {
        public tbUsers user;

        public FlyoutUserLogin()
        {
            InitializeComponent();
            this.Result = DialogResult.Cancel;

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Application.Exit();
            }
            else if (keyData == Keys.Enter)
            {
                RadButtonOK_Click(new object(), new EventArgs());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public DialogResult Result
        {
            get; set;
        }

        public Guid Plane
        {
            get { return this.MCPlan.SelectedValue == null ? Guid.Empty : (Guid)this.MCPlan.SelectedValue; }
        }

        public tbUsers User
        {
            get 
            {
                GridViewDataRowInfo DRV = (GridViewDataRowInfo)MCUser.SelectedItem;
                return (tbUsers)DRV.DataBoundItem;
            }
        }

        private bool ValidateData()
        {
            try
            {
                RadCallout callout = new RadCallout
                {
                    ArrowType = Telerik.WinControls.UI.Callout.CalloutArrowType.Triangle,
                    ArrowDirection = Telerik.WinControls.ArrowDirection.Right,
                    AutoClose = true,
                    CalloutType = Telerik.WinControls.UI.Callout.CalloutType.RoundedRectangle,
                    DropShadow = true
                };

                if (MCUser.SelectedItem == null)
                {
                    if (string.IsNullOrWhiteSpace(this.MCUser.Text))
                    {
                        RadControl cn = MCUser as RadControl;
                        RadCallout.Show(callout, cn, "يجب إختيار المستخدم أولاً", "إضافة البيانات", "إختر المستخدم ثم إضغط علي زر دخول مرة آخرى");

                        return false;
                    }

                }
                if (string.IsNullOrWhiteSpace(this.McPassWord.Text))
                {
                    RadControl cn = McPassWord as RadControl;
                    RadCallout.Show(callout, cn, "يجب ادخال كلمة المرور أولاً", "إضافة البيانات", "ادخل كلمة المرور ثم إضغط علي زر حفظ مرة آخرى");

                    return false;
                }
                if (!McPassWord.Text.Equals(user.password))
                {

                    RadControl cn = McPassWord as RadControl;
                    RadCallout.Show(callout, cn, "كلمة المرور خطأ", "إضافة البيانات", "ادخل كلمة المرور الصحيحة ثم إضغط علي زر حفظ مرة آخرى");
                    return false;
                }

                return true;

            }
            catch { return false; }
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

        private void FlyoutAddByanat_Load(object sender, EventArgs e)
        {

            if (!DBConnect.TryToConnect(AppSetting.DataBase))
            {
                FrmConnection connection = new FrmConnection();
                if (connection.ShowDialog() == DialogResult.OK)
                {
                    Messages messages = new Messages();
                    messages.MessageInformation("إعدادات إتصال جديدة", "تغيير إعدادات الإتصال", "سوف يتم إعادة تشغيل البرنامج لتطبيق إعدادات الإتصال الجديدة");
                    Application.Restart();

                }
            }

            tbUsers.Fill();
            MCUser.DataSource = tbUsers.lstData;
            MCUser.DisplayMember = "name";
            MCUser.ValueMember = "guid";
            //MCUser.SelectedItem = null;

            MCUser.EditorControl.Columns[0].IsVisible = false;
            MCUser.EditorControl.Columns[2].IsVisible = false;
            MCUser.EditorControl.Columns[3].IsVisible = false;
            MCUser.EditorControl.Columns[4].IsVisible = false;
            MCUser.EditorControl.Columns[5].IsVisible = false;
            MCUser.EditorControl.Columns[6].IsVisible = false;
            MCUser.EditorControl.Columns[7].IsVisible = false;
            MCUser.EditorControl.Columns[1].HeaderText = "المستخدم";

            tbPlanInfo.Fill();

            MCPlan.DataSource = tbPlanInfo.lstData;
            MCPlan.DisplayMember = "name";
            MCPlan.ValueMember = "guid";
            MCPlan.SelectedItem = null;

            MCPlan.EditorControl.Columns[0].IsVisible = false;
            MCPlan.EditorControl.Columns[2].IsVisible = false;
            MCPlan.EditorControl.Columns[5].IsVisible = false;
            MCPlan.EditorControl.Columns[1].HeaderText = "المخطط";


            McPassWord.Select();
            McPassWord.Focus();

        }

        private void MCUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MCUser.SelectedItem == null) { return; }
            GridViewDataRowInfo DRV = (GridViewDataRowInfo)MCUser.SelectedItem;
            user = (tbUsers)DRV.DataBoundItem;
        }
    }
}

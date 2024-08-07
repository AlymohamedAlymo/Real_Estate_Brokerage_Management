using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using DoctorERP.Forms;
using Microsoft.SqlServer.Management.Smo;

namespace DoctorERP
{
    public partial class FrmLogin : KryptonForm
    {
        public tbUsers user;
        public FrmLogin()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            user = (tbUsers)CmbUsers.SelectedItem;
            if (!TxtPassword.Text.Equals(user.password))
            {
                MessageBox.Show("كلمة المرور غير صحيحة", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            FrmMain.PlanGuid = checkBox1.Checked ? Guid.Empty : (Guid)comboBox1.SelectedValue;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Application.Exit();
            }
            else if (keyData == Keys.Enter)
            {
                BtnOk_Click(new object(), new EventArgs());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            if (!DBConnect.TryToConnect(AppSetting.DataBase))
            {
                FrmConnection connection = new FrmConnection();
                if (connection.ShowDialog() == DialogResult.OK)
                    MessageBox.Show(("يرجى إعادة تشغيل البرنامج ليتم تطبيق إعدادات الإتصال الجديدة"), Application.ProductName, MessageBoxButtons.OK);

                Application.Exit();
            }

            tbUsers.Fill();
            CmbUsers.DataSource = tbUsers.lstData;
            CmbUsers.DisplayMember = "name";

            tbPlanInfo choose = new tbPlanInfo { name = "-- أختر المخطط --", guid = Guid.Empty };
            tbPlanInfo.Fill();

            tbPlanInfo.lstData.Add(choose);

            comboBox1.DataSource = tbPlanInfo.lstData;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "guid";
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            checkBox1.Checked = true;

        }

        private void LblPassword_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            tbUsers user = (tbUsers)CmbUsers.SelectedItem;
            if (user.password.Equals(string.Empty))
            {
                LblPasswordHint.Visible = true;
            }
            else
            {
                LblPasswordHint.Visible = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == comboBox1.Items.Count - 1)
            {
                checkBox1.Checked = true;

            }
            else { checkBox1.Checked = false; }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (DBConnect.CreateNewDataBase("realestatebrokeragemanagement", "realestatebrokeragemanagement"))
            //    {
            //        DataBase db = new DataBase
            //        {
            //            Description = "realestatebrokeragemanagement",
            //            FileLocation = AppSetting.DataBasePath,
            //            //FileLocation = "C:\\Program Files\\Microsoft SQL Server\\MSSQL14.MSSQLSERVER\\MSSQL\\DATA\\realestatebrokeragemanagement.mdf",
            //            IsDef = false,
            //            SqlName = "realestatebrokeragemanagement"
            //        };
            //        File.WriteAllText(AppSetting.GetAppPath() + DBConnect.DataBaseDefFile, db.SqlName);

            //       if (DBConnect.TryToConnect("realestatebrokeragemanagement"))
            //        {
            //            MessageBox.Show("تم تحديث قاعدة البيانات بنجاح", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        }
            //       else
            //        {
            //            MessageBox.Show("حدثت مشكلة", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }

            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message); }

        }


        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    try
            //    {
            //        List<string> databasesToDelete = new List<string>();

            //        var server = new Server("(LocalDb)\\AccountDB");

            //        foreach (Database db in server.Databases)
            //        {
            //            if (db.Name.ToLower().Contains("realestatebrokeragemanagement"))
            //            {
            //                databasesToDelete.Add(db.Name);
            //            }
            //        }
            //        databasesToDelete.ForEach(x =>
            //        {
            //            Database db = new Database(server, x);
            //            db.Refresh();
            //            db.Drop();
            //        });

            //    }
            //    catch { }

            //    if (DBConnect.CreateNewDataBase("data2024new", "data2024new"))
            //    {
            //        DataBase db = new DataBase
            //        {
            //            Description = "data2024new",
            //            FileLocation = AppSetting.DataBasePath,

            //            //FileLocation = "F:\\SQL Data\\realestatebrokeragemanagement.mdf",
            //            IsDef = false,
            //            SqlName = "data2024new"
            //        };
            //        File.WriteAllText(AppSetting.GetAppPath() + DBConnect.DataBaseDefFile, db.SqlName);


            //        if (DBConnect.TryToConnect("data2024new"))
            //        {
            //            MessageBox.Show("تم تحديث قاعدة البيانات بنجاح", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        }
            //        else
            //        {
            //            MessageBox.Show("حدثت مشكلة", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }

            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            TransferData transferData = new TransferData();
            transferData.ShowDialog();
            transferData.Dispose();
        }
    }
}

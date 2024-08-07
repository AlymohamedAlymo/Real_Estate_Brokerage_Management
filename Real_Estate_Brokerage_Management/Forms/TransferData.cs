using Microsoft.SqlServer.Management.Smo;
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

namespace DoctorERP.Forms
{
    public partial class TransferData : Form
    {
        public TransferData()
        {
            InitializeComponent();
        }

        private void kryptonLabel1_Paint(object sender, PaintEventArgs e)
        {
            //try
            //{
            //    List<string> databasesToDelete = new List<string>();

            //    var server = new Server("(LocalDb)\\AccountDB");

            //    foreach (Database db in server.Databases)
            //    {
            //        if (db.Name.ToLower().Contains("realestatebrokeragemanagement"))
            //        {
            //            databasesToDelete.Add(db.Name);
            //        }
            //    }
            //    databasesToDelete.ForEach(x =>
            //    {
            //        Database db = new Database(server, x);
            //        db.Refresh();
            //        db.Drop();
            //    });

            //}
            //catch { }

        }

        private void TransferData_Load(object sender, EventArgs e)
        {

            List<DataBase> lstDatabase = DBConnect.GetPocPalDesktopDatabase(true);

            comboBox2.DataSource = lstDatabase.ToList();
            comboBox2.DisplayMember = "SqlName";
            comboBox2.SelectedIndex = 0;
            comboBox3.DataSource = lstDatabase.ToList();
            comboBox3.DisplayMember = "SqlName";
            comboBox3.SelectedIndex = comboBox3.Items.Count - 1;
        }
        private static string strScript2 = global::DoctorERP.Properties.Resources.insert_into;

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                strScript2 = strScript2.Replace("dataNew", comboBox3.Text);
                strScript2 = strScript2.Replace("dataOld", comboBox2.Text);

                IEnumerable<string> commandStrings2 = Regex.Split(strScript2, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

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
                            //MessageBox.Show(ex.Message);
                        }
                    }
                }

                MessageBox.Show("تم تحديث قاعدة البيانات بنجاح سوف يتم إعادة تشغيل البرنامج", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            try
            {

                if (DBConnect.CreateNewDataBase(textBox1.Text, textBox1.Text))
                {
                    DataBase db = new DataBase
                    {
                        Description = textBox1.Text,
                        FileLocation = AppSetting.DataBasePath,
                        IsDef = false,
                        SqlName = textBox1.Text
                    };
                    File.WriteAllText(AppSetting.GetAppPath() + DBConnect.DataBaseDefFile, db.SqlName);

                    if (DBConnect.TryToConnect(textBox1.Text))
                    {
                        MessageBox.Show("تم إنشاء قاعدة البيانات بنجاح", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TransferData_Load(sender, new EventArgs());
                    }
                    else
                    {
                        MessageBox.Show("حدثت مشكلة", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
    }
}

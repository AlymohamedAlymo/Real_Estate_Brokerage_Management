using Real_Estate_Management.Data.Real_Estate_Management_DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Real_Estate_Management.Data.Real_Estate_Management_DataSet;

namespace Real_Estate_Management.Repositry
{
    public class TbLawyer
    {
        private tbLawyerTableAdapter lawyerTableAdapter = new tbLawyerTableAdapter();

        public TbLawyer()
        {
            lawyerTableAdapter.Connection = DBConnect.DBConnection;

        }

        public void Fill(tbLawyerDataTable keyword)
        {
            lawyerTableAdapter.Fill(keyword);
        }


        public DataTable Fill(string dbcolumn, object keyword)
        {

            lawyerTableAdapter.Adapter.SelectCommand.CommandText = string.Format("SELECT * FROM tbLawyer WHERE {0} LIKE '%{1}%'", dbcolumn, keyword);
            SqlDataReader yy =lawyerTableAdapter.Adapter.SelectCommand.ExecuteReader();
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(yy.GetName(0));
            dataTable.Load(yy);
            dataSet.Tables.Add(dataTable);

            return dataTable;
            //DBConnect.DBCommand = new SqlCommand(string.Format("SELECT * FROM tbLawyer WHERE {0} LIKE '%{1}%'", dbcolumn, keyword), DBConnect.DBConnection);
            //DBConnect.DBAdapter = new SqlDataAdapter(DBConnect.DBCommand);
            //DBConnect.DBAdapter.Fill(dtData);
            //return dtData;
            //tbLawyerTableAdapter tbLawyerTableAdapter = new tbLawyerTableAdapter();
            //tbLawyerTableAdapter.Connection = DBConnect.DBConnection;
            //tbLawyerDataTable tbLawyerRows = new tbLawyerDataTable();
            //lawyerTableAdapter.Fill(tbLawyerRows);

            //lstData = new List<tbLand>();
            //dtData = new DataTable();
            //DBConnect.DBCommand = new SqlCommand(string.Format("SELECT * FROM tbLand WHERE {0} LIKE '%{1}%'", dbcolumn, keyword), DBConnect.DBConnection);
            //DBConnect.DBAdapter = new SqlDataAdapter(DBConnect.DBCommand);
            //DBConnect.DBAdapter.Fill(dtData);
            //foreach (DataRow dr in dtData.Rows)
            //{
            //    tbLand Land = new tbLand();
            //    Land.planguid = new Guid(dr["PlanGuid"].ToString());
            //    Land.guid = new Guid(dr["guid"].ToString());
            //    Land.number = (int)dr["number"];
            //    Land.code = (int)dr["code"];
            //    Land.blocknumber = int.Parse(dr["blocknumber"].ToString());
            //    Land.landtype = (string)dr["landtype"];
            //    Land.area = decimal.Parse(dr["area"].ToString());
            //    Land.deednumber = (string)dr["deednumber"];
            //    Land.amount = decimal.Parse(dr["amount"].ToString());
            //    Land.isworkfee = (bool)dr["isworkfee"];
            //    Land.workfee = decimal.Parse(dr["workfee"].ToString());
            //    Land.issalefee = (bool)dr["issalefee"];
            //    Land.salesfee = decimal.Parse(dr["salesfee"].ToString());
            //    Land.isbuildingfee = (bool)dr["isbuildingfee"];
            //    Land.buildingfee = decimal.Parse(dr["buildingfee"].ToString());
            //    Land.isvat = (bool)dr["isvat"];
            //    Land.vat = decimal.Parse(dr["vat"].ToString());
            //    Land.isdiscounttotal = (bool)dr["isdiscounttotal"];
            //    Land.discounttotal = decimal.Parse(dr["discounttotal"].ToString());
            //    Land.discounttotalvalue = decimal.Parse(dr["discounttotalvalue"].ToString());
            //    Land.isdiscountfee = (bool)dr["isdiscountfee"];
            //    Land.discountfee = decimal.Parse(dr["discountfee"].ToString());
            //    Land.discountfeevalue = decimal.Parse(dr["discountfeevalue"].ToString());
            //    Land.total = decimal.Parse(dr["total"].ToString());
            //    Land.south = (string)dr["south"];
            //    Land.southdesc = (string)dr["southdesc"];
            //    Land.north = (string)dr["north"];
            //    Land.northdesc = (string)dr["northdesc"];
            //    Land.east = (string)dr["east"];
            //    Land.eastdesc = (string)dr["eastdesc"];
            //    Land.west = (string)dr["west"];
            //    Land.westdesc = (string)dr["westdesc"];
            //    Land.status = (string)dr["status"];
            //    Land.reservereason = (string)dr["reservereason"];

            //    Land.note = (string)dr["note"];
            //    Land.lastaction = (string)dr["lastaction"];

            //    lstData.Add(Land);
            //}
            //DBConnect.DBCommand.Parameters.Clear();
        }


        //public List<tbLawyerRow> GetAll()
        //{
        //    return lawyerTableAdapter.GetData();
        //}

        //public List<tbLawyerRow> Search(string keyword)
        //{
        //    return lawyerTableAdapter.(keyword);
        //}
        //public tbLawyerRow GetById(int id)
        //{
        //    return lawyerTableAdapter.GetDataByID(id);
        //}
        //public void Insert(string name, string address, string phone, string email)
        //{

        //    lawyerTableAdapter.Insert(name, address, phone, email);
        //}

        //public void Update(int id, string name, string address, string phone, string email)
        //{
        //    lawyerTableAdapter.Update(id, name, address, phone, email);
        //}

        //public void Delete(int id)
        //{
        //    lawyerTableAdapter.Delete(id);
        //}
        //public List<tbLawyerRow> GetAll()
        //{
        //    return lawyerTableAdapter.GetData();
        //}

    }
}

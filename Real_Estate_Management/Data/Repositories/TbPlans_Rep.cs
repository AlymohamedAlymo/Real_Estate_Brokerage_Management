using Real_Estate_Management.Data.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class TbPlans_Rep
{

    public static List<tbPlan> lstData;
    public static DataTable dtData;

    public static void Fill()
    {
        lstData = new List<tbPlan>();
        dtData = new DataTable();
        DBConnect.DBCommand = new SqlCommand("SELECT * FROM tbPlans order by Code", DBConnect.DBConnection);
        DBConnect.DBAdapter = new SqlDataAdapter(DBConnect.DBCommand);
        DBConnect.DBAdapter.Fill(dtData);
        foreach (DataRow dr in dtData.Rows)
        {
            tbPlan lawer = new tbPlan
            {

                OwnerGuid = new Guid(dr["OwnerGuid"].ToString()),
                Guid = new Guid(dr["Guid"].ToString()),
                Number = (string)dr["Number"],
                Code = (int)dr["Code"],
                Location = (string)dr["Location"],
                Note = (string)dr["Note"],
                LastAction = (string)dr["LastAction"],
                Name = (string)dr["Name"],
                City = (string)dr["City"],

            };

            lstData.Add(lawer);
        }
    }

    public static DataTable Fill(string dbcolumn, object keyword)
    {

        //plansTableAdapter.Adapter.SelectCommand.CommandText = string.Format("SELECT * FROM tbLawyer WHERE {0} LIKE '%{1}%'", dbcolumn, keyword);
        //SqlDataReader yy = plansTableAdapter.Adapter.SelectCommand.ExecuteReader();
        //DataSet dataSet = new DataSet();
        DataTable dataTable = new DataTable("yy.GetName(0)");
        //dataTable.Load(yy);
        //dataSet.Tables.Add(dataTable);
        return dataTable;

    }

    //#region Database
    //public void Insert()
    //{
    //    DBConnect.DBCommand.CommandText = "INSERT INTO tbLand VALUES (@PlanGuid, @guid, @number , @code, @blocknumber, @landtype , @area, @deednumber, @amount, @isworkfee, @workfee, @issalefee, @salesfee, @isbuildingfee, @buildingfee, @isvat, @vat, @isdiscounttotal, @discounttotal, @discounttotalvalue, @isdiscountfee, @discountfee, @discountfeevalue, @total, @south, @southdesc, @north, @northdesc, @east, @eastdesc, @west, @westdesc, @status, @reservereason , @note , @lastaction)";

    //    DBConnect.DBCommand.Parameters.AddWithValue("@PlanGuid", planguid);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@guid", guid);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@number", number);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@code", code);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@blocknumber", blocknumber);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@landtype", landtype);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@area", area);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@deednumber", deednumber);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@amount", amount);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@isworkfee", isworkfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@workfee", workfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@issalefee", issalefee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@salesfee", salesfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@isbuildingfee", isbuildingfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@buildingfee", buildingfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@isvat", isvat);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@vat", vat);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@isdiscounttotal", isdiscounttotal);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@discounttotal", discounttotal);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@discounttotalvalue", discounttotalvalue);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@isdiscountfee", isdiscountfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@discountfee", discountfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@discountfeevalue", discountfeevalue);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@total", total);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@south", south);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@southdesc", southdesc);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@north", north);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@northdesc", northdesc);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@east", east);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@eastdesc", eastdesc);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@west", west);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@westdesc", westdesc);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@status", status);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@reservereason", reservereason);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@note", note);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@lastaction", lastaction);

    //    DBConnect.DBCommand.ExecuteNonQuery();
    //    DBConnect.DBCommand.Parameters.Clear();
    //}

    //public void Update()
    //{
    //    DBConnect.DBCommand.CommandText = "UPDATE tbLand SET number = @number , code = @code,  blocknumber = @blocknumber, landtype = @landtype,  area = @area, deednumber = @deednumber, amount = @amount, isworkfee = @isworkfee, workfee = @workfee, issalefee = @issalefee, salesfee = @salesfee, isbuildingfee = @isbuildingfee, buildingfee = @buildingfee, isvat = @isvat, vat = @vat, isdiscounttotal = @isdiscounttotal, discounttotal = @discounttotal, discounttotalvalue = @discounttotalvalue, isdiscountfee = @isdiscountfee, discountfee = @discountfee, discountfeevalue = @discountfeevalue, total = @total, south = @south, southdesc = @southdesc, north = @north, northdesc = @northdesc, east = @east, eastdesc = @eastdesc, west = @west, westdesc = @westdesc, status = @status, reservereason = @reservereason , note = @note , lastaction = @lastaction WHERE guid = @guid";

    //    DBConnect.DBCommand.Parameters.AddWithValue("@number", number);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@code", code);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@blocknumber", blocknumber);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@landtype", landtype);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@area", area);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@deednumber", deednumber);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@amount", amount);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@isworkfee", isworkfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@workfee", workfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@issalefee", issalefee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@salesfee", salesfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@isbuildingfee", isbuildingfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@buildingfee", buildingfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@isvat", isvat);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@vat", vat);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@isdiscounttotal", isdiscounttotal);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@discounttotal", discounttotal);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@discounttotalvalue", discounttotalvalue);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@isdiscountfee", isdiscountfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@discountfee", discountfee);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@discountfeevalue", discountfeevalue);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@total", total);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@south", south);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@southdesc", southdesc);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@north", north);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@northdesc", northdesc);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@east", east);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@eastdesc", eastdesc);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@west", west);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@westdesc", westdesc);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@status", status);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@reservereason", reservereason);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@note", note);
    //    DBConnect.DBCommand.Parameters.AddWithValue("@lastaction", lastaction);

    //    DBConnect.DBCommand.Parameters.AddWithValue("@guid", guid);

    //    DBConnect.DBCommand.ExecuteNonQuery();
    //    DBConnect.DBCommand.Parameters.Clear();
    //}

    //public void Delete()
    //{
    //    DBConnect.DBCommand.CommandText = "DELETE FROM tbLand WHERE guid = @guid";

    //    DBConnect.DBCommand.Parameters.AddWithValue("@guid", guid);

    //    DBConnect.DBCommand.ExecuteNonQuery();
    //    DBConnect.DBCommand.Parameters.Clear();
    //}

    //public void DeleteBy(string dbcolumn, object val)
    //{
    //    DBConnect.DBCommand.CommandText = string.Format("DELETE FROM tbLand  WHERE {0} = {1}", dbcolumn, "@" + dbcolumn);
    //    DBConnect.DBCommand.Parameters.AddWithValue(string.Format("@{0}", dbcolumn), val);
    //    DBConnect.DBCommand.ExecuteNonQuery();
    //    DBConnect.DBCommand.Parameters.Clear();
    //}

    //public void DeleteALL()
    //{
    //    DBConnect.DBCommand.CommandText = "DELETE FROM tbLand";

    //    DBConnect.DBCommand.ExecuteNonQuery();
    //    DBConnect.DBCommand.Parameters.Clear();
    //}
    //#endregion

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

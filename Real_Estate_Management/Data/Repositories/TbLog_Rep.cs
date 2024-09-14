using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Real_Estate_Management;

public class TbLog_Rep
{

    public static List<Real_Estate_Management.Data.DataBase.tbLog> lstData;
    public static DataTable dtData;


    public static void Fill()
    {
        lstData = new List<Real_Estate_Management.Data.DataBase.tbLog>();
        dtData = new DataTable();
        DBConnect.DBCommand = new SqlCommand("SELECT * FROM tbLog order by RegDate", DBConnect.DBConnection);
        DBConnect.DBAdapter = new SqlDataAdapter(DBConnect.DBCommand);
        DBConnect.DBAdapter.Fill(dtData);
        using (SqlDataReader reader = DBConnect.DBCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                Real_Estate_Management.Data.DataBase.tbLog lawer = new Real_Estate_Management.Data.DataBase.tbLog
                {
                    
                    PlanGuid = (Guid)reader["PlanGuid"],
                    Guid = (Guid)reader["Guid"],
                    Code = (int)reader["Code"],
                    Note = (string)reader["Note"],
                    Action = (string)reader["Action"],
                    ActionType = (string)reader["ActionType"],
                    RegDate = (DateTime)reader["RegDate"],
                    UserName = (string)reader["UserName"],
                };

                lstData.Add(lawer);
            }
        }
    }

    public static void Fill(string dbcolumn, object keyword)
    {






        //lawyerTableAdapter.Adapter.SelectCommand.CommandText = string.Format("SELECT * FROM tbLawyer WHERE {0} LIKE '%{1}%'", dbcolumn, keyword);
        //SqlDataReader yy = lawyerTableAdapter.Adapter.SelectCommand.ExecuteReader();
        //DataSet dataSet = new DataSet();
        //DataTable dataTable = new DataTable(yy.GetName(0));
        //dataTable.Load(yy);
        //dataSet.Tables.Add(dataTable);
        //return (tbLawyerDataTable)dataTable;

    }


    public static int GetMaxNumber(string dbcolumn)
    {
        DBConnect.DBCommand = new SqlCommand("SELECT MAX(" + dbcolumn + ") FROM tbLawyer", DBConnect.DBConnection);
        DBConnect.DBAdapter = new SqlDataAdapter(DBConnect.DBCommand);
        DBConnect.DBAdapter.Fill(dtData);
        var result = DBConnect.DBCommand.ExecuteScalar();
        if (!(result.Equals(DBNull.Value)))
        {
            return (int)result;
        }
        return 0;
    }

    public static void AddNew(Guid PlanGuid, string ActionType, string Action, string Note)
    {
        Real_Estate_Management.Data.DataBase.tbLog log = new Real_Estate_Management.Data.DataBase.tbLog
        {
            PlanGuid = PlanGuid,
            Guid = Guid.NewGuid(),
            ActionType = ActionType,
            Action = Action,
            RegDate = DBConnect.GetServerDate(),
            UserName = FrmMain.CurrentUser.name,
            Note = Note
        };
        Insert(log);
    }

    public static void Insert(Real_Estate_Management.Data.DataBase.tbLog Row)
    {
        string Script = "INSERT INTO tbLog VALUES (@PlanGuid, @Guid, @RegDate, @UserName, @ActionType , @Action, @Note)";
        DBConnect.DBCommand = new SqlCommand(Script, DBConnect.DBConnection, DBConnect.DBTrans);
        DBConnect.DBCommand.Parameters.AddWithValue("@PlanGuid", Row.PlanGuid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Guid", Row.Guid);
        DBConnect.DBCommand.Parameters.AddWithValue("@RegDate", Row.RegDate);
        DBConnect.DBCommand.Parameters.AddWithValue("@UserName", Row.UserName);
        DBConnect.DBCommand.Parameters.AddWithValue("@ActionType", Row.ActionType);
        DBConnect.DBCommand.Parameters.AddWithValue("@Action", Row.Action);
        DBConnect.DBCommand.Parameters.AddWithValue("@Note", Row.Note);

        DBConnect.DBCommand.ExecuteNonQuery();
        DBConnect.DBCommand.Parameters.Clear();

    }

    //#region Database

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
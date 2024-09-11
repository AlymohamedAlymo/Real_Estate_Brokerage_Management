using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Real_Estate_Management.Data;
using Real_Estate_Management.Data.DataBase;
using DoctorERP;

public class TbLawyer_Rep
{

    public static List<tbLawyer> lstData;
    public static DataTable dtData;


    public static void Fill()
    {
        lstData = new List<tbLawyer>();
        dtData = new DataTable();
        DBConnect.DBCommand = new SqlCommand("SELECT * FROM tbLawyer order by Code", DBConnect.DBConnection);
        DBConnect.DBAdapter = new SqlDataAdapter(DBConnect.DBCommand);
        DBConnect.DBAdapter.Fill(dtData);
        using (SqlDataReader reader = DBConnect.DBCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                tbLawyer lawer = new tbLawyer
                {
                    PlanGuid = (Guid)reader["PlanGuid"],
                    Guid = (Guid)reader["Guid"],
                    Number = (int)reader["Number"],
                    Code = (int)reader["Code"],
                    Email = (string)reader["Email"],
                    Note = (string)reader["Note"],
                    IDNumber = (string)reader["IDNumber"],
                    VatNumber = (string)reader["VatNumber"],
                    LastAction = (string)reader["LastAction"],
                    Mobile = (string)reader["Mobile"],
                    Name = (string)reader["Name"],
                    PublicNumber = (string)reader["PublicNumber"]
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
        var result = DBConnect.DBCommand.ExecuteScalar();
        if (result != null)
        {
            return (int)result;
        }
        return 0;
    }


    public static void AddLawyer(Guid _PlanGuid, Guid _Guid, int _Number, string _Name, string _Mobile, string _IDNumber, string _VatNumber, string _PublicNumber, string _Email, string _Note, string _LastAction)
    {
        tbLawyer lawer = new tbLawyer
        {
            PlanGuid = _PlanGuid,
            Guid = _Guid,
            Number = _Number,
            Statues = "نشط",
            Name = _Name,
            Mobile = _Mobile,
            IDNumber = _IDNumber,
            VatNumber = _VatNumber,
            PublicNumber = _PublicNumber,
            Email = _Email,
            Note = _Note,
            LastAction = _LastAction,
        };
        Insert(lawer);
    }

    public static void Insert(tbLawyer Row)
    {
        string script = @"INSERT INTO [dbo].[tbLawyer]
           ([PlanGuid]
           ,[Guid]
           ,[Statues]
           ,[Number]
           ,[Name]
           ,[IDNumber]
           ,[Mobile]
           ,[Email]
           ,[VatNumber]
           ,[PublicNumber]
           ,[Note]
           ,[LastAction])
     VALUES
           (@PlanGuid
           ,@Guid
           ,@Statues
           ,@Number
           ,@Name
           ,@IDNumber
           ,@Mobile
           ,@Email
           ,@VatNumber
           ,@PublicNumber
           ,@Note
           ,@LastAction)";

        DBConnect.DBCommand = new SqlCommand(script, DBConnect.DBConnection, DBConnect.DBTrans);
        DBConnect.DBCommand.Parameters.AddWithValue("@PlanGuid", Row.PlanGuid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Guid", Row.Guid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Number", Row.Number);
        DBConnect.DBCommand.Parameters.AddWithValue("@Statues", Row.Statues);
        DBConnect.DBCommand.Parameters.AddWithValue("@Name", Row.Name);
        DBConnect.DBCommand.Parameters.AddWithValue("@Mobile", Row.Mobile);
        DBConnect.DBCommand.Parameters.AddWithValue("@IDNumber", Row.IDNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@VatNumber", Row.VatNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@PublicNumber", Row.PublicNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@Email", Row.Email);
        DBConnect.DBCommand.Parameters.AddWithValue("@LastAction", Row.LastAction);
        DBConnect.DBCommand.Parameters.AddWithValue("@Note", Row.Note);

        DBConnect.DBCommand.ExecuteNonQuery();
        DBConnect.DBCommand.Parameters.Clear();

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
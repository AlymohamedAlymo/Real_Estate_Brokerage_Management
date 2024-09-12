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
                    PlanGuid = reader["PlanGuid"].Equals(DBNull.Value) ? Guid.Empty : (Guid)reader["PlanGuid"] ,
                    Guid = reader["Guid"].Equals(DBNull.Value) ? Guid.Empty : (Guid)reader["Guid"],
                    Number = reader["Number"].Equals(DBNull.Value) ? 0 : (int)reader["Number"],
                    Code = reader["Code"].Equals(DBNull.Value) ? 0 : (int)reader["Code"],
                    Email = reader["Email"].Equals(DBNull.Value) ? string.Empty : (string)reader["Email"],
                    Note = reader["Note"].Equals(DBNull.Value) ? string.Empty : (string)reader["Note"],
                    IDNumber = reader["IDNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["IDNumber"],
                    VatNumber = reader["VatNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["VatNumber"],
                    LastAction = reader["LastAction"].Equals(DBNull.Value) ? string.Empty : (string)reader["LastAction"],
                    Mobile = reader["Mobile"].Equals(DBNull.Value) ? string.Empty : (string)reader["Mobile"],
                    MobileAdd = reader["MobileAdd"].Equals(DBNull.Value) ? string.Empty : (string)reader["MobileAdd"],
                    Name = reader["Name"].Equals(DBNull.Value) ? string.Empty : (string)reader["Name"],
                    OfficeName = reader["OfficeName"].Equals(DBNull.Value) ? string.Empty : (string)reader["OfficeName"],
                    Statues = reader["Statues"].Equals(DBNull.Value) ? string.Empty : (string)reader["Statues"],
                    
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


    public static void AddLawyer(Guid _PlanGuid, Guid _Guid, int _Number, string _Name, string _Mobile, string _MobileAdd, string _IDNumber, string _VatNumber, string _OfficeName, string _Email, string _Note, string _LastAction)
    {
        tbLawyer lawer = new tbLawyer
        {
            PlanGuid = _PlanGuid,
            Guid = _Guid,
            Number = _Number,
            Statues = "نشط",
            Name = _Name,
            Mobile = _Mobile,
            MobileAdd = _MobileAdd,
            IDNumber = _IDNumber,
            VatNumber = _VatNumber,
            OfficeName = _OfficeName,
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
           ,[MobileAdd]
           ,[Email]
           ,[VatNumber]
           ,[OfficeName]
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
           ,@MobileAdd
           ,@Email
           ,@VatNumber
           ,@OfficeName
           ,@Note
           ,@LastAction)";

        DBConnect.DBCommand = new SqlCommand(script, DBConnect.DBConnection, DBConnect.DBTrans);
        DBConnect.DBCommand.Parameters.AddWithValue("@PlanGuid", Row.PlanGuid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Guid", Row.Guid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Number", Row.Number);
        DBConnect.DBCommand.Parameters.AddWithValue("@Statues", Row.Statues);
        DBConnect.DBCommand.Parameters.AddWithValue("@Name", Row.Name);
        DBConnect.DBCommand.Parameters.AddWithValue("@Mobile", Row.Mobile);
        DBConnect.DBCommand.Parameters.AddWithValue("@MobileAdd", Row.MobileAdd);
        DBConnect.DBCommand.Parameters.AddWithValue("@IDNumber", Row.IDNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@VatNumber", Row.VatNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@OfficeName", Row.OfficeName);
        DBConnect.DBCommand.Parameters.AddWithValue("@Email", Row.Email);
        DBConnect.DBCommand.Parameters.AddWithValue("@LastAction", Row.LastAction);
        DBConnect.DBCommand.Parameters.AddWithValue("@Note", Row.Note);

        DBConnect.DBCommand.ExecuteNonQuery();
        DBConnect.DBCommand.Parameters.Clear();

    }

    public static void Update(tbLawyer Row)
    {
        string script = @"UPDATE [dbo].[tbLawyer] SET
           [PlanGuid] = @PlanGuid
           ,[Statues] = @Statues
           ,[Name] = @Name
           ,[IDNumber] = @IDNumber
           ,[Mobile] = @Mobile
           ,[MobileAdd] = @MobileAdd
           ,[Email] = @Email
           ,[VatNumber] = @VatNumber
           ,[OfficeName] = @OfficeName
           ,[Note] = @Note
           ,[LastAction] = @LastAction
         WHERE [Guid] = @guid";

        DBConnect.DBCommand = new SqlCommand(script, DBConnect.DBConnection, DBConnect.DBTrans);
        DBConnect.DBCommand.Parameters.AddWithValue("@PlanGuid", Row.PlanGuid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Guid", Row.Guid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Number", Row.Number);
        DBConnect.DBCommand.Parameters.AddWithValue("@Statues", Row.Statues);
        DBConnect.DBCommand.Parameters.AddWithValue("@Name", Row.Name);
        DBConnect.DBCommand.Parameters.AddWithValue("@Mobile", Row.Mobile);
        DBConnect.DBCommand.Parameters.AddWithValue("@MobileAdd", Row.MobileAdd);
        DBConnect.DBCommand.Parameters.AddWithValue("@IDNumber", Row.IDNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@VatNumber", Row.VatNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@OfficeName", Row.OfficeName);
        DBConnect.DBCommand.Parameters.AddWithValue("@Email", Row.Email);
        DBConnect.DBCommand.Parameters.AddWithValue("@LastAction", Row.LastAction);
        DBConnect.DBCommand.Parameters.AddWithValue("@Note", Row.Note);

        DBConnect.DBCommand.ExecuteNonQuery();
        DBConnect.DBCommand.Parameters.Clear();
    }

    public static void Delete(Guid _Guid)
    {
        DBConnect.DBCommand = new SqlCommand("DELETE FROM tbLawyer WHERE Guid = @Guid", DBConnect.DBConnection, DBConnect.DBTrans);
        DBConnect.DBCommand.Parameters.AddWithValue("@Guid", _Guid);
        DBConnect.DBCommand.ExecuteNonQuery();
        DBConnect.DBCommand.Parameters.Clear();
    }




    //#region Database

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
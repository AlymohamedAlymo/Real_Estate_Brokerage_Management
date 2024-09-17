using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Real_Estate_Management.Data.DataBase;
using SmartArabXLSX.Wordprocessing;
using DevExpress.XtraLayout.Customization;
using System.Reflection;
using Telerik.WinForms.Documents.Model.Notes;

public class TbParenters_Rep
{

    public static List<tbParenter> lstData;
    public static DataTable dtData;




    public static void Fill(Guid ParentGuid)
    {
        lstData = new List<tbParenter>();
        dtData = new DataTable();
        DBConnect.DBCommand = new SqlCommand("SELECT * FROM tbParenters WHERE ParentGuid = @val order by Code", DBConnect.DBConnection, DBConnect.DBTrans);
        DBConnect.DBCommand.Parameters.AddWithValue("@val", ParentGuid);
        DBConnect.DBAdapter = new SqlDataAdapter(DBConnect.DBCommand);
        DBConnect.DBAdapter.Fill(dtData);
        using (SqlDataReader reader = DBConnect.DBCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                tbParenter lawer = new tbParenter
                {
                    PlanGuid = reader["PlanGuid"].Equals(DBNull.Value) ? Guid.Empty : (Guid)reader["PlanGuid"],
                    ParentGuid = reader["ParentGuid"].Equals(DBNull.Value) ? Guid.Empty : (Guid)reader["ParentGuid"],
                    Guid = reader["Guid"].Equals(DBNull.Value) ? Guid.Empty : (Guid)reader["Guid"],
                    Code = reader["Code"].Equals(DBNull.Value) ? 0 : (int)reader["Code"],
                    Name = reader["Name"].Equals(DBNull.Value) ? string.Empty : (string)reader["Name"],
                    IDNumber = reader["IDNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["IDNumber"],
                    Mobile = reader["Mobile"].Equals(DBNull.Value) ? string.Empty : (string)reader["Mobile"],
                    Note = reader["Note"].Equals(DBNull.Value) ? string.Empty : (string)reader["Note"],
                    
                };
                lstData.Add(lawer);
            }
            DBConnect.DBCommand.Parameters.Clear();

        }

    }

    public static bool IsExistTrans(Guid guid, Guid parentguid)
    {
        DBConnect.DBCommand.CommandText = "SELECT CASE WHEN EXISTS (SELECT 1 FROM tbParenters WHERE Guid = @Guid AND ParentGuid = @ParentGuid) THEN 1 ELSE 0 END";
        DBConnect.DBCommand.Parameters.AddWithValue("@Guid", guid);
        DBConnect.DBCommand.Parameters.AddWithValue("@ParentGuid", parentguid);
        int iResult = (int)DBConnect.DBCommand.ExecuteScalar();
        bool Result = iResult == 1;
        DBConnect.DBCommand.Parameters.Clear();
        return Result;
    }

    public static void AddNew(Guid _PlanGuid, Guid _ParentGuid, Guid _Guid, string _Name, string _IDNumber, string _Mobile,
        string _Note)
    {
        tbParenter lawer = new tbParenter
        {
            PlanGuid = _PlanGuid,
            ParentGuid = _ParentGuid,
            Guid = _Guid,
            Name = _Name,
            Mobile = _Mobile,
            IDNumber = _IDNumber,
            Note = _Note,
        };
        Insert(lawer);
    }


    public static void Insert(tbParenter Row)
    {
        string script = @"INSERT INTO [dbo].[tbParenters]
           ([PlanGuid]
           ,[ParentGuid]
           ,[Guid]
           ,[Name]
           ,[IDNumber]
           ,[Mobile]
           ,[Note])
     VALUES
           (@PlanGuid
           ,@ParentGuid
           ,@Guid
           ,@Name
           ,@IDNumber
           ,@Mobile
           ,@Note)";

        DBConnect.DBCommand = new SqlCommand(script, DBConnect.DBConnection, DBConnect.DBTrans);
        DBConnect.DBCommand.Parameters.AddWithValue("@PlanGuid", Row.PlanGuid);
        DBConnect.DBCommand.Parameters.AddWithValue("@ParentGuid", Row.ParentGuid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Guid", Row.Guid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Name", Row.Name);
        DBConnect.DBCommand.Parameters.AddWithValue("@Mobile", Row.Mobile);
        DBConnect.DBCommand.Parameters.AddWithValue("@IDNumber", Row.IDNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@Note", Row.Note);

        var ff = DBConnect.DBCommand.ExecuteNonQuery();
        DBConnect.DBCommand.Parameters.Clear();

    }


    public static void Update(tbParenter Row)
    {
        string script = @"UPDATE [dbo].[tbParenters] SET
           [PlanGuid] = @PlanGuid
           ,[ParentGuid] = @ParentGuid
           ,[Name] = @Name
           ,[IDNumber] = @IDNumber
           ,[Mobile] = @Mobile
           ,[Note] = @Note
         WHERE [Guid] = @Guid";

        DBConnect.DBCommand = new SqlCommand(script, DBConnect.DBConnection, DBConnect.DBTrans);
        DBConnect.DBCommand.Parameters.AddWithValue("@PlanGuid", Row.PlanGuid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Guid", Row.Guid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Name", Row.Name);
        DBConnect.DBCommand.Parameters.AddWithValue("@Mobile", Row.Mobile);
        DBConnect.DBCommand.Parameters.AddWithValue("@IDNumber", Row.IDNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@Note", Row.Note);

        DBConnect.DBCommand.ExecuteNonQuery();
        DBConnect.DBCommand.Parameters.Clear();
    }

    public static void Delete(Guid _Guid)
    {
        DBConnect.DBCommand = new SqlCommand("DELETE FROM tbParenters WHERE Guid = @Guid", DBConnect.DBConnection, DBConnect.DBTrans);
        DBConnect.DBCommand.Parameters.AddWithValue("@Guid", _Guid);
        DBConnect.DBCommand.ExecuteNonQuery();
        DBConnect.DBCommand.Parameters.Clear();
    }

    public static void DeleteByParent(Guid _ParentGuid)
    {
        DBConnect.DBCommand = new SqlCommand("DELETE FROM tbParenters WHERE ParentGuid = @ParentGuid", DBConnect.DBConnection, DBConnect.DBTrans);
        DBConnect.DBCommand.Parameters.AddWithValue("@ParentGuid", _ParentGuid);
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



}
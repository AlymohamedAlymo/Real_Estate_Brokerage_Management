using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Real_Estate_Management.Data.DataBase;

public class TBOwner_Rep
{

    public static List<tbOwner> lstData;
    public static DataTable dtData;


    public static void Fill()
    {
        lstData = new List<tbOwner>();
        dtData = new DataTable();
        DBConnect.DBCommand = new SqlCommand("SELECT * FROM tbOwner order by Code", DBConnect.DBConnection);
        DBConnect.DBAdapter = new SqlDataAdapter(DBConnect.DBCommand);
        DBConnect.DBAdapter.Fill(dtData);
        using (SqlDataReader reader = DBConnect.DBCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                tbOwner lawer = new tbOwner
                {
                    PlanGuid = reader["PlanGuid"].Equals(DBNull.Value) ? Guid.Empty : (Guid)reader["PlanGuid"] ,
                    Guid = reader["Guid"].Equals(DBNull.Value) ? Guid.Empty : (Guid)reader["Guid"],
                    Code = reader["Code"].Equals(DBNull.Value) ? 0 : (int)reader["Code"],
                    Number = reader["Number"].Equals(DBNull.Value) ? 0 : (int)reader["Number"],
                    Name = reader["Name"].Equals(DBNull.Value) ? string.Empty : (string)reader["Name"],
                    IDNumber = reader["IDNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["IDNumber"],
                    Mobile = reader["Mobile"].Equals(DBNull.Value) ? string.Empty : (string)reader["Mobile"],
                    MobileAdd = reader["MobileAdd"].Equals(DBNull.Value) ? string.Empty : (string)reader["MobileAdd"],
                    Email = reader["Email"].Equals(DBNull.Value) ? string.Empty : (string)reader["Email"],
                    VatNumber = reader["VatNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["VatNumber"],
                    AgentName = reader["AgentName"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgentName"],
                    AgentID = reader["AgentID"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgentID"],
                    AgentMobile = reader["AgentMobile"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgentMobile"],
                    AgenteMail = reader["AgenteMail"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgenteMail"],
                    AgentVatNumber = reader["AgentVatNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgentVatNumber"],
                    AgencyNumber = reader["AgencyNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgencyNumber"],
                    AgentPublicNumber = reader["AgentPublicNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgentPublicNumber"],
                    OfficeName = reader["OfficeName"].Equals(DBNull.Value) ? string.Empty : (string)reader["OfficeName"],
                    Note = reader["Note"].Equals(DBNull.Value) ? string.Empty : (string)reader["Note"],
                    LastAction = reader["LastAction"].Equals(DBNull.Value) ? string.Empty : (string)reader["LastAction"],
                    
                };
                lstData.Add(lawer);
            }
        }
    }


    public static void Fill(string dbcolumn, object val)
    {
        lstData = new List<tbOwner>();
        dtData = new DataTable();
        DBConnect.DBCommand = new SqlCommand(string.Format("SELECT * FROM tbOwner WHERE {0} = @val order by Code", dbcolumn), DBConnect.DBConnection);
        DBConnect.DBCommand.Parameters.AddWithValue("@val", val);
        DBConnect.DBAdapter = new SqlDataAdapter(DBConnect.DBCommand);
        DBConnect.DBAdapter.Fill(dtData);
        using (SqlDataReader reader = DBConnect.DBCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                tbOwner lawer = new tbOwner
                {
                    PlanGuid = reader["PlanGuid"].Equals(DBNull.Value) ? Guid.Empty : (Guid)reader["PlanGuid"],
                    Guid = reader["Guid"].Equals(DBNull.Value) ? Guid.Empty : (Guid)reader["Guid"],
                    Code = reader["Code"].Equals(DBNull.Value) ? 0 : (int)reader["Code"],
                    Number = reader["Number"].Equals(DBNull.Value) ? 0 : (int)reader["Number"],
                    Name = reader["Name"].Equals(DBNull.Value) ? string.Empty : (string)reader["Name"],
                    IDNumber = reader["IDNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["IDNumber"],
                    Mobile = reader["Mobile"].Equals(DBNull.Value) ? string.Empty : (string)reader["Mobile"],
                    MobileAdd = reader["MobileAdd"].Equals(DBNull.Value) ? string.Empty : (string)reader["MobileAdd"],
                    Email = reader["Email"].Equals(DBNull.Value) ? string.Empty : (string)reader["Email"],
                    VatNumber = reader["VatNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["VatNumber"],
                    AgentName = reader["AgentName"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgentName"],
                    AgentID = reader["AgentID"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgentID"],
                    AgentMobile = reader["AgentMobile"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgentMobile"],
                    AgenteMail = reader["AgenteMail"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgenteMail"],
                    AgentVatNumber = reader["AgentVatNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgentVatNumber"],
                    AgencyNumber = reader["AgencyNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgencyNumber"],
                    AgentPublicNumber = reader["AgentPublicNumber"].Equals(DBNull.Value) ? string.Empty : (string)reader["AgentPublicNumber"],
                    OfficeName = reader["OfficeName"].Equals(DBNull.Value) ? string.Empty : (string)reader["OfficeName"],
                    Note = reader["Note"].Equals(DBNull.Value) ? string.Empty : (string)reader["Note"],
                    LastAction = reader["LastAction"].Equals(DBNull.Value) ? string.Empty : (string)reader["LastAction"],
                    
                };
                lstData.Add(lawer);
            }
            DBConnect.DBCommand.Parameters.Clear();

        }

    }


    public static int GetMaxNumber(string dbcolumn)
    {
        DBConnect.DBCommand = new SqlCommand("SELECT MAX(" + dbcolumn + ") FROM tbOwner", DBConnect.DBConnection);
        var result = DBConnect.DBCommand.ExecuteScalar();
        if (!(result.Equals(DBNull.Value)))
        {
            return (int)result;
        }
        return 0;
    }

    public static void AddNew(Guid _PlanGuid, Guid _Guid, int _Number, string _Name, string _IDNumber, string _Mobile, string _MobileAdd,
        string _Email, string _VatNumber, string _OfficeName, string _AgentName, string _AgentID, string _AgentMobile, string _AgenteMail,
        string _AgentVatNumber, string _AgencyNumber, string _AgentPublicNumber, string _Note, string _LastAction)
    {
        tbOwner lawer = new tbOwner
        {
            PlanGuid = _PlanGuid,
            Guid = _Guid,
            Number = _Number,
            Name = _Name,
            Mobile = _Mobile,
            MobileAdd = _MobileAdd,
            IDNumber = _IDNumber,
            VatNumber = _VatNumber,
            OfficeName = _OfficeName,
            Email = _Email,
            Note = _Note,
            LastAction = _LastAction,
            AgencyNumber = _AgencyNumber,
            AgentPublicNumber = _AgentPublicNumber,
            AgentVatNumber = _AgentVatNumber,
            AgenteMail = _AgenteMail,
            AgentID = _AgentID,
            AgentMobile = _AgentMobile,
            AgentName = _AgentName,
        };
        Insert(lawer);
    }


    public static void Insert(tbOwner Row)
    {
        string script = @"INSERT INTO [dbo].[tbOwner]
           ([PlanGuid]
           ,[Guid]
           ,[Number]
           ,[Name]
           ,[IDNumber]
           ,[Mobile]
           ,[MobileAdd]
           ,[Email]
           ,[VatNumber]
           ,[OfficeName]
           ,[AgentName]
           ,[AgentID]
           ,[AgentMobile]
           ,[AgenteMail]
           ,[AgentVatNumber]
           ,[AgencyNumber]
           ,[AgentPublicNumber]
           ,[Note]
           ,[LastAction])
     VALUES
           (@PlanGuid
           ,@Guid
           ,@Number
           ,@Name
           ,@IDNumber
           ,@Mobile
           ,@MobileAdd
           ,@Email
           ,@VatNumber
           ,@OfficeName
           ,@AgentName
           ,@AgentID
           ,@AgentMobile
           ,@AgenteMail
           ,@AgentVatNumber
           ,@AgencyNumber
           ,@AgentPublicNumber
           ,@Note
           ,@LastAction)";

        DBConnect.DBCommand = new SqlCommand(script, DBConnect.DBConnection, DBConnect.DBTrans);
        DBConnect.DBCommand.Parameters.AddWithValue("@PlanGuid", Row.PlanGuid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Guid", Row.Guid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Number", Row.Number);
        DBConnect.DBCommand.Parameters.AddWithValue("@Name", Row.Name);
        DBConnect.DBCommand.Parameters.AddWithValue("@Mobile", Row.Mobile);
        DBConnect.DBCommand.Parameters.AddWithValue("@MobileAdd", Row.MobileAdd);
        DBConnect.DBCommand.Parameters.AddWithValue("@IDNumber", Row.IDNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@Email", Row.Email);
        DBConnect.DBCommand.Parameters.AddWithValue("@VatNumber", Row.VatNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgentName", Row.AgentName);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgentID", Row.AgentID);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgentMobile", Row.AgentMobile);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgenteMail", Row.AgenteMail);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgentVatNumber", Row.AgentVatNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgencyNumber", Row.AgencyNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgentPublicNumber", Row.AgentPublicNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@OfficeName", Row.OfficeName);
        DBConnect.DBCommand.Parameters.AddWithValue("@LastAction", Row.LastAction);
        DBConnect.DBCommand.Parameters.AddWithValue("@Note", Row.Note);

        DBConnect.DBCommand.ExecuteNonQuery();
        DBConnect.DBCommand.Parameters.Clear();

    }


    public static void Update(tbOwner Row)
    {
        string script = @"UPDATE [dbo].[tbOwner] SET
           [PlanGuid] = @PlanGuid
           ,[Name] = @Name
           ,[IDNumber] = @IDNumber
           ,[Mobile] = @Mobile
           ,[MobileAdd] = @MobileAdd
           ,[Email] = @Email
           ,[VatNumber] = @VatNumber
           ,[OfficeName] = @OfficeName
           ,[AgentName] = @AgentName
           ,[AgentID] = @AgentID
           ,[AgentMobile] = @AgentMobile
           ,[AgenteMail] = @AgenteMail
           ,[AgentVatNumber] = @AgentVatNumber
           ,[AgencyNumber] = @AgencyNumber
           ,[AgentPublicNumber] = @AgentPublicNumber
           ,[Note] = @Note
           ,[LastAction] = @LastAction
         WHERE [Guid] = @guid";

        DBConnect.DBCommand = new SqlCommand(script, DBConnect.DBConnection, DBConnect.DBTrans);
        DBConnect.DBCommand.Parameters.AddWithValue("@PlanGuid", Row.PlanGuid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Guid", Row.Guid);
        DBConnect.DBCommand.Parameters.AddWithValue("@Number", Row.Number);
        DBConnect.DBCommand.Parameters.AddWithValue("@Name", Row.Name);
        DBConnect.DBCommand.Parameters.AddWithValue("@Mobile", Row.Mobile);
        DBConnect.DBCommand.Parameters.AddWithValue("@MobileAdd", Row.MobileAdd);
        DBConnect.DBCommand.Parameters.AddWithValue("@IDNumber", Row.IDNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@Email", Row.Email);
        DBConnect.DBCommand.Parameters.AddWithValue("@VatNumber", Row.VatNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgentName", Row.AgentName);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgentID", Row.AgentID);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgentMobile", Row.AgentMobile);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgenteMail", Row.AgenteMail);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgentVatNumber", Row.AgentVatNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgencyNumber", Row.AgencyNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@AgentPublicNumber", Row.AgentPublicNumber);
        DBConnect.DBCommand.Parameters.AddWithValue("@OfficeName", Row.OfficeName);
        DBConnect.DBCommand.Parameters.AddWithValue("@LastAction", Row.LastAction);
        DBConnect.DBCommand.Parameters.AddWithValue("@Note", Row.Note);

        DBConnect.DBCommand.ExecuteNonQuery();
        DBConnect.DBCommand.Parameters.Clear();
    }

    public static void Delete(Guid _Guid)
    {
        DBConnect.DBCommand = new SqlCommand("DELETE FROM tbOwner WHERE Guid = @Guid", DBConnect.DBConnection, DBConnect.DBTrans);
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



}
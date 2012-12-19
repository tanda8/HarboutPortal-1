using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        RegisterHyperLink.NavigateUrl = "Register.aspx";
    }

    protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
    {        
        Session.Clear();
        e.Authenticated = false;

        AuthenticateUser();

        if (Session["UserID"] != null)
        {
            e.Authenticated = true;
        }
    }

    private void AuthenticateUser()
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.CommandText = "SELECT CONT_ID, CONT_USER_ID, CONT_FIRST_NAME, CONT_LAST_NAME, CO_ID, CO_TYPE, CO_NAME " +
            "FROM CONTACTS, COMPANY " +
            "WHERE CONT_USER_ID = :UserName AND CONT_PASSWORD = :Password AND CONT_CO_ID = CO_ID";

        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;

        cmd.Parameters.Add(new OracleParameter(":UserName", OracleDbType.Varchar2));
        cmd.Parameters[":UserName"].Value = UserLogin.UserName;

        cmd.Parameters.Add(new OracleParameter(":Password", OracleDbType.Varchar2));
        cmd.Parameters[":Password"].Value = UserLogin.Password;

        try
        {
            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                Session["UserID"] = dr["CONT_ID"].ToString();
                Session["UserName"] = dr["CONT_USER_ID"].ToString();
                Session["FullName"] = dr["CONT_FIRST_NAME"].ToString() + " " + dr["CONT_LAST_NAME"].ToString();
                Session["CompanyID"] = dr["CO_ID"].ToString();
                Session["CompanyType"] = dr["CO_TYPE"].ToString();  // (Shipper=1, Consignee=2, etc)
                Session["CompanyName"] = dr["CO_NAME"].ToString();
                Session["RQBookingType"] = 2;   // Always set to 2 (RateQuote=1, Booking=2)                
            }
        }
        catch (OracleException ex)
        {
            switch (ex.Number)
            {
                case 12514:
                    LoginError.Text = "The Harbour database is unavailable.  (ORA-12514)";
                    break;
                case 12541:
                    LoginError.Text = "The Harbour database is unavailable.  (ORA-12541)";
                    break;
                case 12560:
                    LoginError.Text = "The Harbour database is unavailable.  (ORA-12560)";
                    break;
                default:
                    LoginError.Text = "Database error: " + ex.Message.ToString();
                    break;
            }
        }
        catch (Exception ex)
        {
            LoginError.Text = "Connection error: " + ex.Message.ToString();
        }
        finally
        {
            cmd.Dispose();
            conn.Dispose();
        }
    }






}
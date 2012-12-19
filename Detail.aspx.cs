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

public partial class Detail : System.Web.UI.Page
{
    // TJE ToDo - Change public to private
    public String TransportType = String.Empty;   // Ocean=1, Air=2
    public String OceanContainerType = String.Empty;   // FCL=1, LCL=2

    private String StoredProcedure = String.Empty;   // Oracle stored procedure        
    private String Transportation = String.Empty;   // Ocean-FCL, Ocean-LCL, Air

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["HarbourNumber"] == null)
        {
            Response.Redirect("Logout.aspx");
        }

        if (Session["HarbourNumber"].ToString() != String.Empty)
        {
            LoadGeneralInfo();
            LoadReferenceNumber();

            if (TransportType == "1")
            {
                if (OceanContainerType == "1")
                {                    
                    LoadFCL();
                }

                if (OceanContainerType == "2")
                {                    
                    LoadLCL();
                }
            }

            if (TransportType == "2")
            {                
                LoadAir();
            }
        }
    }

    private void LoadGeneralInfo()
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.CommandText = "SP_PORTAL_DETAIL2";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = conn;

        cmd.Parameters.Add(new OracleParameter("HarbourNumber", OracleDbType.Varchar2, ParameterDirection.Input));
        cmd.Parameters["HarbourNumber"].Value = Session["HarbourNumber"];

        cmd.Parameters.Add(new OracleParameter("Cursor", OracleDbType.RefCursor, ParameterDirection.Output));

        try
        {
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
 
            while (dr.Read())
            {
                HarbourNumber.Text = dr["HarbourNumber"].ToString();
                Shipper.Text = dr["Shipper"].ToString();
                Consignee.Text = dr["Consignee"].ToString();
                RequestDate.Text = dr["DateRequested"].ToString();
                PlaceOfReceipt.Text = dr["POR"].ToString();
                PlaceOfDelivery.Text = dr["POD"].ToString();
                Incoterm.Text = dr["Inco"].ToString();
                Commodity.Text = dr["Commodity"].ToString();
                Hazardous.Text = dr["Hazardous"].ToString();

                TransportType = dr["TransportType"].ToString();
                OceanContainerType = dr["OceanContainerType"].ToString();
            }
        }
        catch (OracleException ex)
        {
            ErrorPanel.Visible = true;
            ErrorMesssage.Text = "Database error: " + ex.Message.ToString();
        }
        catch (Exception ex)
        {
            ErrorPanel.Visible = true;
            ErrorMesssage.Text = "Connection error: " + ex.Message.ToString();
        }
        finally
        {
            cmd.Dispose();
            conn.Dispose();
        }
    }

    private void LoadReferenceNumber()
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.CommandText = "SP_PORTAL_DETAIL_REFNUM1";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = conn;

        cmd.Parameters.Add(new OracleParameter("HarbourNumber", OracleDbType.Varchar2, ParameterDirection.Input));
        cmd.Parameters["HarbourNumber"].Value = Session["HarbourNumber"];

        cmd.Parameters.Add(new OracleParameter("CompanyType", OracleDbType.Decimal, ParameterDirection.Input));
        cmd.Parameters["CompanyType"].Value = Session["CompanyType"];

        cmd.Parameters.Add(new OracleParameter("Cursor", OracleDbType.RefCursor, ParameterDirection.Output));

        try
        {
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr.HasRows)
            {
                Reference1.Visible = true;
                Reference2.Visible = true;                
            }

            while (dr.Read())
            {
                Reference2.Text = Reference2.Text + " " + dr["ReferenceNumber"].ToString();               
            }
        }
        catch (OracleException ex)
        {
            ErrorPanel.Visible = true;
            ErrorMesssage.Text = "Database error: " + ex.Message.ToString();
        }
        catch (Exception ex)
        {
            ErrorPanel.Visible = true;
            ErrorMesssage.Text = "Connection error: " + ex.Message.ToString();
        }
        finally
        {
            cmd.Dispose();
            conn.Dispose();
        }
    }

    private void LoadFCL()
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.CommandText = "SP_PORTAL_DETAIL_FCL";        
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = conn;

        cmd.Parameters.Add(new OracleParameter("HarbourNumber", OracleDbType.Varchar2, ParameterDirection.Input));
        cmd.Parameters["HarbourNumber"].Value = Session["HarbourNumber"];

        cmd.Parameters.Add(new OracleParameter("Cursor", OracleDbType.RefCursor, ParameterDirection.Output));

        try
        {
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr.HasRows)
            {
                Transportation1.Visible = true;
                Transportation2.Visible = true;
                Transportation2.Text = "Ocean - FCL";                

                FclGridView.DataSource = dr;
                FclGridView.DataBind();
                FclGridView.Visible = true;
            } 
        }
        catch (OracleException ex)
        {
            ErrorPanel.Visible = true;
            ErrorMesssage.Text = "Database error: " + ex.Message.ToString();
        }
        catch (Exception ex)
        {
            ErrorPanel.Visible = true;
            ErrorMesssage.Text = "Connection error: " + ex.Message.ToString();
        }
        finally
        {
            cmd.Dispose();
            conn.Dispose();
        }
    }

    private void LoadLCL()
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.CommandText = "SP_PORTAL_DETAIL_LCL1";        
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = conn;

        cmd.Parameters.Add(new OracleParameter("HarbourNumber", OracleDbType.Varchar2, ParameterDirection.Input));
        cmd.Parameters["HarbourNumber"].Value = Session["HarbourNumber"];

        cmd.Parameters.Add(new OracleParameter("Cursor1", OracleDbType.RefCursor, ParameterDirection.Output));
        cmd.Parameters.Add(new OracleParameter("Cursor2", OracleDbType.RefCursor, ParameterDirection.Output));

        try
        {
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.Default);   // Read Cursor1 (LCL Totals)

            if (dr.HasRows)
            {
                Transportation1.Visible = true;
                Transportation2.Visible = true;
                Transportation2.Text = "Ocean - LCL";                

                LclTotalPieces1.Visible = true;
                LclTotalPieces2.Visible = true;

                LclTotalWeight1.Visible = true;
                LclTotalWeight2.Visible = true;

                LclTotalVolume1.Visible = true;
                LclTotalVolume2.Visible = true;
            } 

            while (dr.Read())
            {
                LclTotalPieces2.Text = dr["TotalPieces"].ToString();
                LclTotalWeight2.Text = dr["TotalLBS"].ToString() + " lbs";
                LclTotalVolume2.Text = dr["TotalCUFT"].ToString() + " ft3";               
            }

            dr.NextResult();   // Read Cursor2 (LCL Details)

            if (dr.HasRows)
            { 
                LclGridView.DataSource = dr;
                LclGridView.DataBind();
                LclGridView.Visible = true;
                // decimal volume = (decimal)dr["Length"] * (decimal)dr["Width"] * (decimal)dr["Height"] / (12 * 12 * 12);     ToDO                
            }   
        }
        catch (OracleException ex)
        {
            ErrorPanel.Visible = true;
            ErrorMesssage.Text = "Database error: " + ex.Message.ToString();
        }
        catch (Exception ex)
        {
            ErrorPanel.Visible = true;
            ErrorMesssage.Text = "Connection error: " + ex.Message.ToString();
        }
        finally
        {
            cmd.Dispose();
            conn.Dispose();
        }
    }

    private void LoadAir()
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.CommandText = "SP_PORTAL_DETAIL_AIR1";        
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = conn;

        cmd.Parameters.Add(new OracleParameter("HarbourNumber", OracleDbType.Varchar2, ParameterDirection.Input));
        cmd.Parameters["HarbourNumber"].Value = Session["HarbourNumber"];

        cmd.Parameters.Add(new OracleParameter("Cursor1", OracleDbType.RefCursor, ParameterDirection.Output));
        cmd.Parameters.Add(new OracleParameter("Cursor2", OracleDbType.RefCursor, ParameterDirection.Output));

        try
        {
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.Default);   // Read Cursor1 (Air Totals)

            if (dr.HasRows)
            {
                Transportation1.Visible = true;
                Transportation2.Visible = true;
                Transportation2.Text = "Air";                

                AirTotalPieces1.Visible = true;
                AirTotalPieces2.Visible = true;

                AirTotalWeight1.Visible = true;
                AirTotalWeight2.Visible = true;

                AirTotalVolume1.Visible = true;
                AirTotalVolume2.Visible = true;
            }

            while (dr.Read())
            {
                AirTotalPieces2.Text = dr["TotalPieces"].ToString();
                AirTotalWeight2.Text = dr["TotalWeight"].ToString() + " lbs";
                AirTotalVolume2.Text = dr["TotalVolume"].ToString() + " vol-lbs";
            }

            dr.NextResult();   // Read Cursor2 (Air Details)

            if (dr.HasRows)
            {
                AirGridView.DataSource = dr;
                AirGridView.DataBind();
                AirGridView.Visible = true;
                // decimal volume = (decimal)dr["Length"] * (decimal)dr["Width"] * (decimal)dr["Height"] / (12 * 12 * 12);     ToDO                
            }
        }
        catch (OracleException ex)
        {
            ErrorPanel.Visible = true;
            ErrorMesssage.Text = "Database error: " + ex.Message.ToString();
        }
        catch (Exception ex)
        {
            ErrorPanel.Visible = true;
            ErrorMesssage.Text = "Connection error: " + ex.Message.ToString();
        }
        finally
        {
            cmd.Dispose();
            conn.Dispose();
        }
    }
}
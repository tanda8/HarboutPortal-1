using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;
using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

public partial class Trace : System.Web.UI.Page
{
    public String TransportType = String.Empty;     // Ocean=1, Air=2

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["HarbourNumber"] == null)
        {
            Response.Redirect("Logout.aspx");
        }

        TransportCheck();

        if (TransportType == "1")
        {
            LoadOceanTrace1();
            LoadOceanTrace2();
            LoadOceanEvents();
        }

        if (TransportType == "2")
        {
            LoadAirTrace();
            LoadAirEvents();
        }
    }

    private void TransportCheck()
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.CommandText = "SELECT RQI_RQBK_TYPE FROM RQBK_IDX WHERE RQI_RQBK_NUM = :HarbourNumber";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;

        cmd.Parameters.Add(new OracleParameter(":HarbourNumber", OracleDbType.Varchar2));
        cmd.Parameters[":HarbourNumber"].Value = Session["HarbourNumber"];

        try
        {
            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                TransportType = dr["RQI_RQBK_TYPE"].ToString();
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

    private void LoadAirTrace()
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.CommandText = "SP_PORTAL_TRACE_AIR";
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
                AirTracePanel.Visible = true;
            } 

            while (dr.Read())
            {
                if (dr["HarbourNumber"].ToString() != String.Empty)
                {
                    HarbourNumber1.Visible = true;
                    HarbourNumber2.Visible = true;
                    HarbourNumber2.Text = dr["HarbourNumber"].ToString();                    
                }

                if (dr["AirWaybill"].ToString() != String.Empty)
                {
                    AirWaybill1.Visible = true;
                    AirWaybill2.Visible = true;
                    AirWaybill2.Text = dr["AirWaybill"].ToString();
                }

                if (dr["TotalPieces"].ToString() != String.Empty)
                {
                    TotalPieces1.Visible = true;
                    TotalPieces2.Visible = true;
                    TotalPieces2.Text = dr["TotalPieces"].ToString();
                }

                if (dr["TotalWeight"].ToString() != String.Empty)
                {
                    TotalWeight1.Visible = true;
                    TotalWeight2.Visible = true;
                    TotalWeight2.Text = dr["TotalWeight"].ToString() + " lbs";
                }

                if (dr["TotalVolumeLBS"].ToString() != String.Empty)
                {
                    TotalVolume1.Visible = true;
                    TotalVolume2.Visible = true;
                    TotalVolume2.Text = dr["TotalVolumeLBS"].ToString() + " vol-lbs";
                }
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

    private void LoadAirEvents()
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.CommandText = "SP_PORTAL_EVENTS_AIR";
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
                AirEventGridView.DataSource = dr;
                AirEventGridView.DataBind();
                AirEventGridView.Visible = true;
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

    private void LoadOceanTrace1()
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.CommandText = "SP_PORTAL_TRACE_OCEAN1";
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
                OceanTracePanel.Visible = true;
            } 

            while (dr.Read())
            {
                if (dr["HarbourNumber"].ToString() != String.Empty)
                {
                    oHarbourNumber1.Visible = true;
                    oHarbourNumber2.Visible = true;
                    oHarbourNumber2.Text = dr["HarbourNumber"].ToString();
                }

                if (dr["BookingNumber"].ToString() != String.Empty)
                {
                    oBookingNumber1.Visible = true;
                    oBookingNumber2.Visible = true;
                    oBookingNumber2.Text = dr["BookingNumber"].ToString();
                }

                if (dr["OceanCarrier"].ToString() != String.Empty)
                {
                    oOceanCarrier1.Visible = true;
                    oOceanCarrier2.Visible = true;
                    oOceanCarrier2.Text = dr["OceanCarrier"].ToString();
                }

                if (dr["TotalPieces"].ToString() != String.Empty)
                {
                    oTotalPieces1.Visible = true;
                    oTotalPieces2.Visible = true;
                    oTotalPieces2.Text = dr["TotalPieces"].ToString();                    
                }

                if (dr["TotalWeight"].ToString() != String.Empty)
                {
                    oTotalWeight1.Visible = true;
                    oTotalWeight2.Visible = true;
                    oTotalWeight2.Text = dr["TotalWeight"].ToString() + " lbs";
                }

                if (dr["TotalVolume"].ToString() != String.Empty)
                {
                    oTotalVolume1.Visible = true;
                    oTotalVolume2.Visible = true;
                    oTotalVolume2.Text = dr["TotalVolume"].ToString() + " ft3";
                }

                //if (dr["ShipperCutoff"].ToString() != String.Empty)
                //{
                //    oShipperCutoff1.Visible = true;
                //    oShipperCutoff2.Visible = true;
                //    oShipperCutoff2.Text = dr["ShipperCutoff"].ToString();
                //}

                //if (dr["TerminalCutoff"].ToString() != String.Empty)
                //{
                //    oTerminalCutoff1.Visible = true;
                //    oTerminalCutoff2.Visible = true;
                //    oTerminalCutoff2.Text = dr["TerminalCutoff"].ToString();                   
                //}

                //if (dr["PortCutoff"].ToString() != String.Empty)
                //{
                //    oPortCutoff1.Visible = true;
                //    oPortCutoff2.Visible = true;
                //    oPortCutoff2.Text = dr["PortCutoff"].ToString();
                //}

                //if (dr["SailDate"].ToString() != String.Empty)
                //{
                //    oSailDate1.Visible = true;
                //    oSailDate2.Visible = true;
                //    oSailDate2.Text = dr["SailDate"].ToString();
                //}

                //if (dr["ActualSailDate"].ToString() != String.Empty)
                //{
                //    oActualSailDate1.Visible = true;
                //    oActualSailDate2.Visible = true;
                //    oActualSailDate2.Text = dr["ActualSailDate"].ToString();                    
                //}

                //if (dr["DischargeDate"].ToString() != String.Empty)
                //{
                //    oDischargeDate1.Visible = true;
                //    oDischargeDate2.Visible = true;
                //    oDischargeDate2.Text = dr["DischargeDate"].ToString(); 
                //}

                //if (dr["PodDate"].ToString() != String.Empty)
                //{
                //    oPodDate1.Visible = true;
                //    oPodDate2.Visible = true;
                //    oPodDate2.Text = dr["PodDate"].ToString();
                //}
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

    private void LoadOceanTrace2()
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.CommandText = "SP_PORTAL_TRACE_OCEAN1";
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
                OceanTraceGridView.DataSource = dr;                

                if (dr["ShipperCutoff"].ToString() == String.Empty)
                {
                    OceanTraceGridView.Columns[0].Visible = false;
                }

                if (dr["TerminalCutoff"].ToString() == String.Empty)
                {
                    OceanTraceGridView.Columns[1].Visible = false;
                }

                if (dr["PortCutoff"].ToString() == String.Empty)
                {
                    OceanTraceGridView.Columns[2].Visible = false;
                }

                if (dr["SailDate"].ToString() == String.Empty)
                {
                    OceanTraceGridView.Columns[3].Visible = false;
                }

                if (dr["ActualSailDate"].ToString() == String.Empty)
                {
                    OceanTraceGridView.Columns[4].Visible = false;
                }

                if (dr["DischargeDate"].ToString() == String.Empty)
                {
                    OceanTraceGridView.Columns[5].Visible = false;
                }

                if (dr["PodDate"].ToString() == String.Empty)
                {
                    OceanTraceGridView.Columns[6].Visible = false;
                }
                 
                OceanTraceGridView.DataBind();
                OceanTraceGridView.Visible = true;
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

    private void LoadOceanEvents()
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.CommandText = "SP_PORTAL_EVENTS_OCEAN1";
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
                OceanEventGridView.DataSource = dr;

                if (dr["ContainerID"].ToString() == String.Empty)
                {
                    OceanEventGridView.Columns[0].Visible = false;
                }

                if (dr["VesselName"].ToString() == String.Empty)
                {
                    OceanEventGridView.Columns[1].Visible = false;
                }

                if (dr["Voyage"].ToString() == String.Empty)
                {
                    OceanEventGridView.Columns[2].Visible = false;
                }

                if (dr["Event"].ToString() == String.Empty)
                {
                    OceanEventGridView.Columns[3].Visible = false;
                }

                if (dr["Expected1"].ToString() == String.Empty)
                {
                    OceanEventGridView.Columns[4].Visible = false;
                }

                if (dr["Actual"].ToString() == String.Empty)
                {
                    OceanEventGridView.Columns[5].Visible = false;
                }

                OceanEventGridView.DataBind();
                OceanEventGridView.Visible = true;
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
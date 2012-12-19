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

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (!Page.IsPostBack)
        {
            //if (SearchPeriodDDL.SelectedValue == String.Empty)
            //{
            //    SearchPeriodLabel.Text = String.Empty;
            //}
            //else
            //{
            //    Session["SearchMonths"] = -(Convert.ToInt32(SearchPeriodDDL.SelectedValue));

            //    int SearchMonths = Convert.ToInt32(SearchPeriodDDL.SelectedValue);
            //    SearchPeriodLabel.Text = System.DateTime.Now.AddMonths(SearchMonths).ToShortDateString() + " to " + System.DateTime.Now.ToShortDateString();
            //}
        }
    }

    protected void SearchParameterDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        HarbourNumberPanel.Visible = false;
        BookingNumberPanel.Visible = false;
        //PlaceOfDeliveryPanel.Visible = false;
        //ShipperNamePanel.Visible = false;
        //ConsigneeNamePanel.Visible = false;
        //ShipperReferenceNumberPanel.Visible = false;
        //ConsigneeReferenceNumberPanel.Visible = false;
        AirWaybillNumberPanel.Visible = false;
        NvoBookingNumberPanel.Visible = false;

        //int searchMonths = -(int)Session["SearchMonths"];
        int searchMonths = -240;   // Twenty years search window

        string selectedValue = SearchParameterDDL.SelectedValue;        

        switch (selectedValue)
        {
            case "HarbourNumber":
                HarbourNumber(searchMonths);
                break;
            case "BookingNumber":
                BookingNumber(searchMonths);
                break;
            case "PlaceOfDelivery":
                PlaceOfDelivery(searchMonths);
                break;
            case "ShipperName":
                ShipperName(searchMonths);
                break;
            case "ConsigneeName":
                ConsigneeName(searchMonths);
                break;
            case "ShipperReferenceNumber":
                ShipperReferenceNumber(searchMonths);
                break;
            case "ConsigneeReferenceNumber":
                ConsigneeReferenceNumber(searchMonths);
                break;
            case "AirWaybillNumber":
                AirWaybillNumber(searchMonths);
                break;
            case "NvoBookingNumber":
                NvoBookingNumber(searchMonths);
                break; 
            default:
                break;
        }
    }

    //protected void SearchPeriodDDL_SelectedIndexChanged(object sender, EventArgs e)
    //{  
    //    if (SearchPeriodDDL.SelectedValue == String.Empty)
    //    {
    //        SearchPeriodLabel.Text = String.Empty;           
    //    }
    //    else
    //    {
    //        int SearchMonths = Convert.ToInt32(SearchPeriodDDL.SelectedValue);
    //        Session["SearchMonths"] = -(Convert.ToInt32(SearchPeriodDDL.SelectedValue));

    //        SearchPeriodLabel.Text = System.DateTime.Now.AddMonths(SearchMonths).ToShortDateString() + " to " + System.DateTime.Now.ToShortDateString();
    //        SearchPeriodLabel.Visible = true;   
    //    }
    //}


    protected void HarbourNumberButton_Click(object sender, EventArgs e)
    {
        Session["HarbourNumber"] = HarbourNumberComboBox.SelectedValue.ToString();
        Response.Redirect("SearchResults.aspx", false);        
    }

    protected void BookingNumberButton_Click(object sender, EventArgs e)
    {
        Session["HarbourNumber"] = BookingNumberComboBox.SelectedValue.ToString();
        Response.Redirect("SearchResults.aspx", false);
    }

    protected void PlaceOfDeliveryButton_Click(object sender, EventArgs e)   // ToDo
    { 
    }

    protected void ShipperNameButton_Click(object sender, EventArgs e)   // ToDo
    {
    }

    protected void ConsigneeNameButton_Click(object sender, EventArgs e)   // ToDo
    {
    }

    protected void ShipperReferenceNumberButton_Click(object sender, EventArgs e)   // ToDo
    {
    }

    protected void ConsigneeReferenceNumberButton_Click(object sender, EventArgs e)   // ToDo
    {
    }

    protected void AirWaybillNumberButton_Click(object sender, EventArgs e)
    {
        Session["HarbourNumber"] = AirWaybillNumberComboBox.SelectedValue.ToString();
        Response.Redirect("SearchResults.aspx", false);
    }

    protected void NvoBookingNumberButton_Click(object sender, EventArgs e)
    {
        Session["HarbourNumber"] = NvoBookingNumberComboBox.SelectedValue.ToString();
        Response.Redirect("SearchResults.aspx", false);
    }   


    private void HarbourNumber(int SearchMonths)
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.Parameters.Add(new OracleParameter(":CutoffDate", OracleDbType.Date));
        cmd.Parameters[":CutoffDate"].Value = System.DateTime.Now.AddMonths(SearchMonths);

        cmd.Parameters.Add(new OracleParameter(":CompanyID", OracleDbType.Decimal));
        cmd.Parameters[":CompanyID"].Value = Session["CompanyID"];

        if (Session["CompanyType"].ToString() == "1")  // Shipper
        {
            cmd.CommandText =
            "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber " +
            "FROM RQBK_IDX, RQBK_SH_CS " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +
                "AND RQBK_IDX.RQI_RQBK = 2 " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_SH_ID = :CompanyID " +
            "ORDER BY RQBK_IDX.RQI_RQBK_NUM ASC";
        }

        if (Session["CompanyType"].ToString() == "2")  // Consignee
        {
            cmd.CommandText =
            "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber " +
            "FROM RQBK_IDX, RQBK_SH_CS " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +
                "AND RQBK_IDX.RQI_RQBK = 2 " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_CS_ID = :CompanyID " +
            "ORDER BY RQBK_IDX.RQI_RQBK_NUM ASC";
        }

        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;

        DataSet ds = new DataSet();

        try
        {
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds);

            DataRow initRow = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.InsertAt(initRow, 0);   // Insert empty header row for ComboBox

            HarbourNumberComboBox.DataSource = ds;
            HarbourNumberComboBox.DataTextField = "HarbourNumber";
            HarbourNumberComboBox.DataValueField = "HarbourNumber";
            HarbourNumberComboBox.DataBind();
        }
        catch (OracleException ex)
        {            
            ErrorMesssage.Text = "Database error: " + ex.Message.ToString();
            ErrorPanel.Visible = true;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Suppress error message generated by a AjaxControltoolKit ComboBox binding bug/quirk.            
            ErrorMesssage.Text = "Invalid Operation - ArgumentOutOfRangeException: " + ex.Message.ToString();
            ErrorPanel.Visible = false;
        }
        catch (Exception ex)
        {            
            ErrorMesssage.Text = "Connection error: " + ex.Message.ToString();
            ErrorPanel.Visible = true;
        }
        finally
        {
            if ((ds == null) || (ds.Tables.Count == 0) || (ds.Tables[0].Rows.Count == 1))
            {
                HarbourNumberPanel.Visible = false;
                Message.Text = "No data found.";
                MessagePanel.Visible = true;
            }
            else
            {                
                HarbourNumberComboBox.SelectedIndex = 0;
                HarbourNumberPanel.Visible = true;
                Message.Text = String.Empty;
                MessagePanel.Visible = false;                
            }
            cmd.Dispose();
            conn.Dispose();
        }
    }
        
    private void BookingNumber(int SearchMonths)
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.Parameters.Add(new OracleParameter(":CutoffDate", OracleDbType.Date));
        cmd.Parameters[":CutoffDate"].Value = System.DateTime.Now.AddMonths(SearchMonths);

        cmd.Parameters.Add(new OracleParameter(":CompanyID", OracleDbType.Decimal));
        cmd.Parameters[":CompanyID"].Value = Session["CompanyID"];

        if (Session["CompanyType"].ToString() == "1")  // Shipper
        {
            cmd.CommandText =
            "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber, " +
                "RQBK_OC_CARRIER.RQOC_BK_NUM BookingNumber " +
            "FROM RQBK_IDX, " +
                "RQBK_SH_CS, " +
                "RQBK_OC_CARRIER " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +
                "AND RQBK_IDX.RQI_RQBK = 2 " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_SH_ID = :CompanyID " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_OC_CARRIER.RQOC_RQBK_ID (+) " +
                "AND RQBK_OC_CARRIER.RQOC_BK_NUM IS NOT NULL " +
            "ORDER BY RQBK_OC_CARRIER.RQOC_BK_NUM ASC";
        }

        if (Session["CompanyType"].ToString() == "2")  // Consignee
        {
            cmd.CommandText =
           "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber, " +
                "RQBK_OC_CARRIER.RQOC_BK_NUM BookingNumber " +
            "FROM RQBK_IDX, " +
                "RQBK_SH_CS, " +
                "RQBK_OC_CARRIER " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +
                "AND RQBK_IDX.RQI_RQBK = 2 " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_CS_ID = :CompanyID " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_OC_CARRIER.RQOC_RQBK_ID (+) " +
                "AND RQBK_OC_CARRIER.RQOC_BK_NUM IS NOT NULL " +
            "ORDER BY RQBK_OC_CARRIER.RQOC_BK_NUM ASC";
        }

        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;

        DataSet ds = new DataSet();

        try
        {
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds);

            DataRow initRow = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.InsertAt(initRow, 0);   // Insert empty header row for ComboBox

            BookingNumberComboBox.DataSource = ds;
            BookingNumberComboBox.DataTextField = "BookingNumber";
            BookingNumberComboBox.DataValueField = "HarbourNumber";
            BookingNumberComboBox.DataBind();
        }
        catch (OracleException ex)
        {            
            ErrorMesssage.Text = "Database error: " + ex.Message.ToString();
            ErrorPanel.Visible = true;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Suppress error message generated by a AjaxControltoolKit ComboBox binding bug/quirk.            
            ErrorMesssage.Text = "Invalid Operation - ArgumentOutOfRangeException: " + ex.Message.ToString();
            ErrorPanel.Visible = false;
        }
        catch (Exception ex)
        {            
            ErrorMesssage.Text = "Connection error: " + ex.Message.ToString();
            ErrorPanel.Visible = true;
        }
        finally
        {
            if ((ds == null) || (ds.Tables.Count == 0) || (ds.Tables[0].Rows.Count == 1))
            {
                BookingNumberPanel.Visible = false;
                Message.Text = "No data found.";
                MessagePanel.Visible = true;               
            }
            else
            {
                BookingNumberComboBox.SelectedIndex = 0;
                BookingNumberPanel.Visible = true;
                Message.Text = String.Empty;
                MessagePanel.Visible = false;               
            }
            cmd.Dispose();
            conn.Dispose();
        }        
    }

    private void PlaceOfDelivery(int SearchMonths)   // ToDo
    {
    }

    private void ShipperName(int SearchMonths)   // ToDo
    {
    }

    private void ConsigneeName(int SearchMonths)   // ToDo
    {
    }

    private void ShipperReferenceNumber(int SearchMonths)   // ToDo
    {
    }

    private void ConsigneeReferenceNumber(int SearchMonths)   // ToDo
    {
    }

    private void AirWaybillNumber(int SearchMonths)
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.Parameters.Add(new OracleParameter(":CutoffDate", OracleDbType.Date));
        cmd.Parameters[":CutoffDate"].Value = System.DateTime.Now.AddMonths(SearchMonths);

        cmd.Parameters.Add(new OracleParameter(":CompanyID", OracleDbType.Decimal));
        cmd.Parameters[":CompanyID"].Value = Session["CompanyID"];

        if (Session["CompanyType"].ToString() == "1")  // Shipper
        {
            cmd.CommandText =
            "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber, " +
                "RQBK_AIR_CARRIER.RQAC_AWB_NUM AirWaybillNumber " +
            "FROM RQBK_IDX, " +
                "RQBK_SH_CS, " +
                "RQBK_AIR_CARRIER " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +
                "AND RQBK_IDX.RQI_RQBK = 2 " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_SH_ID = :CompanyID " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_AIR_CARRIER.RQAC_RQBK_ID (+) " +
                "AND RQBK_AIR_CARRIER.RQAC_AWB_NUM IS NOT NULL " +
            "ORDER BY RQBK_AIR_CARRIER.RQAC_AWB_NUM ASC";
        }

        if (Session["CompanyType"].ToString() == "2")  // Consignee
        {
            cmd.CommandText =
           "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber, " +
                "RQBK_AIR_CARRIER.RQAC_AWB_NUM AirWaybillNumber " +
            "FROM RQBK_IDX, " +
                "RQBK_SH_CS, " +
                "RQBK_AIR_CARRIER " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +
                "AND RQBK_IDX.RQI_RQBK = 2 " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_CS_ID = :CompanyID " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_AIR_CARRIER.RQAC_RQBK_ID (+) " +
                "AND RQBK_AIR_CARRIER.RQAC_AWB_NUM IS NOT NULL " +
            "ORDER BY RQBK_AIR_CARRIER.RQAC_AWB_NUM ASC";
        }

        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;

        DataSet ds = new DataSet();
        
        try
        {
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds);

            DataRow initRow = ds.Tables[0].NewRow();            
            ds.Tables[0].Rows.InsertAt(initRow, 0);   // Insert empty header row for ComboBox

            AirWaybillNumberComboBox.DataSource = ds;
            AirWaybillNumberComboBox.DataTextField = "AirWaybillNumber";
            AirWaybillNumberComboBox.DataValueField = "HarbourNumber";
            AirWaybillNumberComboBox.DataBind();
        }
        catch (OracleException ex)
        {            
            ErrorMesssage.Text = "Database error: " + ex.Message.ToString();
            ErrorPanel.Visible = true;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Suppress error message generated by a AjaxControltoolKit ComboBox binding bug/quirk.            
            ErrorMesssage.Text = "Invalid Operation - ArgumentOutOfRangeException: " + ex.Message.ToString();
            ErrorPanel.Visible = false;
        }
        catch (Exception ex)
        {            
            ErrorMesssage.Text = "Connection error: " + ex.Message.ToString();
            ErrorPanel.Visible = true;
        }
        finally
        {
            if ((ds == null) || (ds.Tables.Count == 0) || (ds.Tables[0].Rows.Count == 1))
            {
                AirWaybillNumberPanel.Visible = false;
                Message.Text = "No data found.";
                MessagePanel.Visible = true;
            }
            else
            {                
                AirWaybillNumberComboBox.SelectedIndex = 0;
                AirWaybillNumberPanel.Visible = true;
                Message.Text = String.Empty;
                MessagePanel.Visible = false;
            }
            cmd.Dispose();
            conn.Dispose();
        }
    }

    private void NvoBookingNumber(int SearchMonths)
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();

        conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

        cmd.Parameters.Add(new OracleParameter(":CutoffDate", OracleDbType.Date));
        cmd.Parameters[":CutoffDate"].Value = System.DateTime.Now.AddMonths(SearchMonths);

        cmd.Parameters.Add(new OracleParameter(":CompanyID", OracleDbType.Decimal));
        cmd.Parameters[":CompanyID"].Value = Session["CompanyID"];

        if (Session["CompanyType"].ToString() == "1")  // Shipper
        {
            cmd.CommandText =
            "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber, " +
                "RQBK_OC_CARRIER.RQOC_NVO_NUM NVOBookingNumber " +
            "FROM RQBK_IDX, " +
                "RQBK_SH_CS, " +
                "RQBK_OC_CARRIER " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +
                "AND RQBK_IDX.RQI_RQBK = 2 " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_SH_ID = :CompanyID " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_OC_CARRIER.RQOC_RQBK_ID (+) " +
                "AND RQBK_OC_CARRIER.RQOC_NVO_NUM IS NOT NULL " +
            "ORDER BY RQBK_OC_CARRIER.RQOC_NVO_NUM ASC";
        }

        if (Session["CompanyType"].ToString() == "2")  // Consignee
        {
            cmd.CommandText =
           "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber, " +
                "RQBK_OC_CARRIER.RQOC_NVO_NUM NVOBookingNumber " +
            "FROM RQBK_IDX, " +
                "RQBK_SH_CS, " +
                "RQBK_OC_CARRIER " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +
                "AND RQBK_IDX.RQI_RQBK = 2 " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_CS_ID = :CompanyID " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_OC_CARRIER.RQOC_RQBK_ID (+) " +
                "AND RQBK_OC_CARRIER.RQOC_NVO_NUM IS NOT NULL " +
            "ORDER BY RQBK_OC_CARRIER.RQOC_NVO_NUM ASC";
        }

        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;

        DataSet ds = new DataSet();

        try
        {
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds);

            DataRow initRow = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.InsertAt(initRow, 0);   // Insert empty header row for ComboBox

            NvoBookingNumberComboBox.DataSource = ds;
            NvoBookingNumberComboBox.DataTextField = "NVOBookingNumber";
            NvoBookingNumberComboBox.DataValueField = "HarbourNumber";
            NvoBookingNumberComboBox.DataBind();
        }
        catch (OracleException ex)
        {
            ErrorMesssage.Text = "Database error: " + ex.Message.ToString();
            ErrorPanel.Visible = true;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Suppress error message generated by a AjaxControltoolKit ComboBox binding bug/quirk.            
            ErrorMesssage.Text = "Invalid Operation - ArgumentOutOfRangeException: " + ex.Message.ToString();
            ErrorPanel.Visible = false;
        }
        catch (Exception ex)
        {
            ErrorMesssage.Text = "Connection error: " + ex.Message.ToString();
            ErrorPanel.Visible = true;
        }
        finally
        {
            if ((ds == null) || (ds.Tables.Count == 0) || (ds.Tables[0].Rows.Count == 1))
            {
                NvoBookingNumberPanel.Visible = false;
                Message.Text = "No data found.";
                MessagePanel.Visible = true;
            }
            else
            {

                NvoBookingNumberComboBox.SelectedIndex = 0;
                NvoBookingNumberPanel.Visible = true;
                Message.Text = String.Empty;
                MessagePanel.Visible = false;
            }
            cmd.Dispose();
            conn.Dispose();
        }
    }  



}
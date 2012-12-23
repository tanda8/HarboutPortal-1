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


public partial class Search2 : System.Web.UI.Page
{    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (!Page.IsPostBack)
        {
            //SearchPeriodDDL.SelectedIndex = 8;    // 1 year default

        }

    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        Session["HarbourNumber"] = SearchComboBox.SelectedValue.ToString();
        Response.Redirect("SearchResults.aspx", false);
    }

    protected void SearchPeriodDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchPanel.Visible = false;
        MessagePanel.Visible = false;
        ErrorPanel.Visible = false;

        int SearchMonths = 0;

        if (SearchPeriodDDL.SelectedIndex == 0)
        {
            SearchPeriodLabel1.Text = String.Empty;
            SearchPeriodLabel2.Text = String.Empty;
            SearchPeriodLabel3.Text = String.Empty;
        }
        else
        {
            SearchMonths = Convert.ToInt32(SearchPeriodDDL.SelectedValue);

            SearchPeriodLabel1.Text = System.DateTime.Now.AddMonths(SearchMonths).ToShortDateString();
            SearchPeriodLabel2.Text = "to";
            SearchPeriodLabel3.Text = System.DateTime.Now.ToShortDateString();

            if (SearchParameterDDL.SelectedIndex != 0)
            {
                SearchSetup();
            }
        }
    }

    protected void SearchParameterDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchPanel.Visible = false;
        MessagePanel.Visible = false;
        ErrorPanel.Visible = false;

        if (SearchPeriodDDL.SelectedIndex != 0)
        {
            SearchSetup();
        }
    }

    protected void SearchSetup()
    {

        int SearchMonths = Convert.ToInt32(SearchPeriodDDL.SelectedValue);

        string ShipperSQL = string.Empty;
        string ConsigneeSQL = string.Empty;

        string DataText = string.Empty;
        string DataValue = string.Empty;

        string selectedValue = SearchParameterDDL.SelectedValue;

        switch (selectedValue)
        {
            case "HarbourNumber":
                ShipperSQL = HarbourNumber_Shipper;
                ConsigneeSQL = HarbourNumber_Consignee;
                DataText = "HarbourNumber";
                DataValue = "HarbourNumber";
                SearchQuery(SearchMonths, ShipperSQL, ConsigneeSQL, DataText, DataValue);
                break;
            case "BookingNumber":
                ShipperSQL = BookingNumber_Shipper;
                ConsigneeSQL = BookingNumber_Consignee;
                DataText = "BookingNumber";
                DataValue = "HarbourNumber";
                SearchQuery(SearchMonths, ShipperSQL, ConsigneeSQL, DataText, DataValue);
                break;
            //case "PlaceOfDelivery":
            //    PlaceOfDelivery(searchMonths);
            //    break;
            //case "ShipperName":
            //    ShipperName(searchMonths);
            //    break;
            //case "ConsigneeName":
            //    ConsigneeName(searchMonths);
            //    break;
            case "ShipperReferenceNumber":
                ShipperSQL = ShipperReferenceNumber_Shipper;
                ConsigneeSQL = ShipperReferenceNumber_Consignee;
                DataText = "ShipperReferenceNumber";
                DataValue = "HarbourNumber";
                SearchQuery(SearchMonths, ShipperSQL, ConsigneeSQL, DataText, DataValue);
                break;
            case "ConsigneeReferenceNumber":
                ShipperSQL = ConsigneeReferenceNumber_Shipper;
                ConsigneeSQL = ConsigneeReferenceNumber_Consignee;
                DataText = "ConsigneeReferenceNumber";
                DataValue = "HarbourNumber";
                SearchQuery(SearchMonths, ShipperSQL, ConsigneeSQL, DataText, DataValue);
                break;
            case "AirWaybillNumber":
                ShipperSQL = AirWaybillNumber_Shipper;
                ConsigneeSQL = AirWaybillNumber_Consignee;
                DataText = "AirWaybillNumber";
                DataValue = "HarbourNumber";
                SearchQuery(SearchMonths, ShipperSQL, ConsigneeSQL, DataText, DataValue);
                break;
            case "NvoBookingNumber":
                ShipperSQL = NvoBookingNumber_Shipper;
                ConsigneeSQL = NvoBookingNumber_Consignee;
                DataText = "NVOBookingNumber";
                DataValue = "HarbourNumber";
                SearchQuery(SearchMonths, ShipperSQL, ConsigneeSQL, DataText, DataValue);
                break;
            default:
                break;
        } 
    }       

    private void SearchQuery(int SearchMonths, string ShipperSQL, string ConsigneeSQL, string DataText, string DataValue)
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
            cmd.CommandText = ShipperSQL;            
        }

        if (Session["CompanyType"].ToString() == "2")  // Consignee
        {
            cmd.CommandText = ConsigneeSQL;   
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

            SearchComboBox.DataSource = ds;
            SearchComboBox.DataTextField = DataText;
            SearchComboBox.DataValueField = DataValue;
            SearchComboBox.DataBind();
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
                SearchPanel.Visible = false;
                Message.Text = "No data found.";
                MessagePanel.Visible = true;
            }
            else
            {
                SearchComboBox.SelectedIndex = 0;
                SearchPanel.Visible = true;
                Message.Text = String.Empty;
                MessagePanel.Visible = false;
            }
            cmd.Dispose();
            conn.Dispose();
        }
    }

    #region SQL Code

    string HarbourNumber_Shipper =
            "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber " +
            "FROM RQBK_IDX, RQBK_SH_CS " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +
                "AND RQBK_IDX.RQI_RQBK = 2 " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_SH_ID = :CompanyID " +
            "ORDER BY RQBK_IDX.RQI_RQBK_NUM ASC";

    string HarbourNumber_Consignee =
            "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber " +
            "FROM RQBK_IDX, RQBK_SH_CS " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +
                "AND RQBK_IDX.RQI_RQBK = 2 " +
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_CS_ID = :CompanyID " +
            "ORDER BY RQBK_IDX.RQI_RQBK_NUM ASC";

    string BookingNumber_Shipper =
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

    string BookingNumber_Consignee =
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

    string ShipperReferenceNumber_Shipper =
            "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber, " +
                "RQBK_REF.RQR_REF_NUM ShipperReferenceNumber " +
            "FROM RQBK_IDX, " +
                "RQBK_SH_CS, " +
                "RQBK_REF " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +  // Completed Transaction
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +  // Active Transaction
                "AND RQBK_IDX.RQI_RQBK = 2 " +  // Booking Type
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_SH_ID = :CompanyID " +  // Shipper ID
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_REF.RQR_RQBK_ID (+) " +
                "AND RQBK_REF.RQR_REF_TYPE = 1 " +  // Shipper Reference
                "AND RQBK_REF.RQR_REF_NUM IS NOT NULL " +
            "ORDER BY RQBK_REF.RQR_REF_NUM ASC";

    string ShipperReferenceNumber_Consignee =
            "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber, " +
                "RQBK_REF.RQR_REF_NUM ShipperReferenceNumber " +
            "FROM RQBK_IDX, " +
                "RQBK_SH_CS, " +
                "RQBK_REF " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +  // Complete Transaction
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +  // Active Transaction
                "AND RQBK_IDX.RQI_RQBK = 2 " +  // Booking Type
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_CS_ID = :CompanyID " +  // Consignee ID
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_REF.RQR_RQBK_ID (+) " +
                "AND RQBK_REF.RQR_REF_TYPE = 1 " +  // Shipper Reference
                "AND RQBK_REF.RQR_REF_NUM IS NOT NULL " +
            "ORDER BY RQBK_REF.RQR_REF_NUM ASC";

    string ConsigneeReferenceNumber_Shipper =
            "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber, " +
                "RQBK_REF.RQR_REF_NUM ConsigneeReferenceNumber " +
            "FROM RQBK_IDX, " +
                "RQBK_SH_CS, " +
                "RQBK_REF " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +  // Completed Transaction
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +  // Active Transaction
                "AND RQBK_IDX.RQI_RQBK = 2 " +  // Booking Type
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_SH_ID = :CompanyID " +  // Shipper ID
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_REF.RQR_RQBK_ID (+) " +
                "AND RQBK_REF.RQR_REF_TYPE = 2 " +  // Consignee Reference
                "AND RQBK_REF.RQR_REF_NUM IS NOT NULL " +
            "ORDER BY RQBK_REF.RQR_REF_NUM ASC";

    string ConsigneeReferenceNumber_Consignee =
           "SELECT DISTINCT RQBK_IDX.RQI_RQBK_NUM HarbourNumber, " +
                "RQBK_REF.RQR_REF_NUM ConsigneeReferenceNumber " +
            "FROM RQBK_IDX, " +
                "RQBK_SH_CS, " +
                "RQBK_REF " +
            "WHERE RQBK_IDX.RQI_COMPLETE = -1 " +  // Complete Transaction
                "AND RQBK_IDX.RQI_ACTIVE = -1 " +  // Active Transaction
                "AND RQBK_IDX.RQI_RQBK = 2 " +  // Booking Type
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_SH_CS.RQSC_RQBK_ID " +
                "AND RQBK_IDX.RQI_DATE_REQUESTED > :CutoffDate " +
                "AND RQBK_SH_CS.RQSC_CS_ID = :CompanyID " +  // Consignee ID
                "AND RQBK_IDX.RQI_RQBK_ID = RQBK_REF.RQR_RQBK_ID (+) " +
                "AND RQBK_REF.RQR_REF_TYPE = 2 " +  // Consignee Reference
                "AND RQBK_REF.RQR_REF_NUM IS NOT NULL " +
            "ORDER BY RQBK_REF.RQR_REF_NUM ASC";

    string AirWaybillNumber_Shipper =
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

    string AirWaybillNumber_Consignee =
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

    string NvoBookingNumber_Shipper =
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

    string NvoBookingNumber_Consignee =
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


    #endregion



}
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

public partial class SearchResults : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (!Page.IsPostBack)
        {
            GridView.DataSource = SearchDataSet;
            GridView.DataBind();
            FormatTable();
        }

        // Test Data
        TestLabel.Text = Session["UserID"].ToString() + " | " +
            Session["UserName"].ToString() + " | " +
            Session["FullName"].ToString() + " | " +
            Session["CompanyID"].ToString() + " | " +
            Session["CompanyType"].ToString() + " | " +
            Session["CompanyName"].ToString() + " | " +
            Session["RQBookingType"].ToString();
        TestPanel.Visible = false;
    }

    protected DataSet SearchDataSet
    {
        get
        {
            if (ViewState["SearchDataSet"] == null)
            {
                OracleConnection conn = new OracleConnection();
                OracleCommand cmd = new OracleCommand();

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;

                cmd.CommandText = "SP_PORTAL_SEARCH_A1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                cmd.Parameters.Add(new OracleParameter("HarbourNumber", OracleDbType.Varchar2, ParameterDirection.Input));
                cmd.Parameters["HarbourNumber"].Value = Session["HarbourNumber"];

                cmd.Parameters.Add(new OracleParameter("Cursor", OracleDbType.RefCursor, ParameterDirection.Output));

                try
                {
                    cmd.Connection.Open();

                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();

                    dt.Load(dr);
                    ds.Tables.Add(dt);

                    ViewState["SearchDataSet"] = ds;
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

            return (DataSet)ViewState["SearchDataSet"];
        }

        set
        {
            ViewState["SearchDataSet"] = value;
        }
    }

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        String sortExpression = String.Empty;
        String NewSortDirection = String.Empty;

        GridView.PageIndex = e.NewPageIndex;

        if (Session["SortExpression"] != null)
        {
            sortExpression = (String)Session["SortExpression"];

            if (Session["SortDirection"] != null && Session["SortDirection"].ToString() == SortDirection.Ascending.ToString())
            {
                NewSortDirection = "ASC";
            }
            else
            {
                NewSortDirection = "DESC";
            }

            Sort(sortExpression, NewSortDirection);
        }
        else
        {
            GridView.DataSource = SearchDataSet;
            GridView.DataBind();
            FormatTable();
        }
    }

    protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        String sortExpression = e.SortExpression;
        Session["SortExpression"] = sortExpression;

        if (Session["SortDirection"] != null && Session["SortDirection"].ToString() == SortDirection.Descending.ToString())
        {
            Session["SortDirection"] = SortDirection.Ascending;
            Sort(sortExpression, "ASC");
        }
        else
        {
            Session["SortDirection"] = SortDirection.Descending;
            Sort(sortExpression, "DESC");
        }
    }

    private void Sort(string sortExpression, string Direction)
    {
        DataView dv = null;
        dv = new DataView(SearchDataSet.Tables[0]);
        dv.Sort = sortExpression + " " + Direction;
        GridView.DataSource = dv;
        GridView.DataBind();
        FormatTable();
    }

    private void FormatTable()
    {
        if (Session["CompanyType"].ToString() == "1")
        {
            GridView.Columns[1].Visible = false;   // Hide GridView SHIPPERNAME column
        }

        if (Session["CompanyType"].ToString() == "2")
        {
            GridView.Columns[2].Visible = false;   // Hide GridView CONSIGNEENAME column
        }
    }

    protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView.Rows[index];
            TableCell selectedCell = selectedRow.Cells[0];
            string harbourNumber = selectedCell.Text;

            Session["HarbourNumber"] = harbourNumber;
            Response.Redirect("Detail.aspx", false);
        }
        if (e.CommandName == "Trace")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView.Rows[index];
            TableCell selectedCell = selectedRow.Cells[0];
            string harbourNumber = selectedCell.Text;

            Session["HarbourNumber"] = harbourNumber;
            Response.Redirect("Trace.aspx", false);
        }

        if (e.CommandName == "Documents")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView.Rows[index];
            TableCell selectedCell = selectedRow.Cells[0];
            string harbourNumber = selectedCell.Text;

            Session["HarbourNumber"] = harbourNumber;
            Response.Redirect("Documents.aspx", false);
        }
    }

}
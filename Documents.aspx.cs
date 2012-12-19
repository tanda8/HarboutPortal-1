using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Web.UI.HtmlControls;

public partial class Documents : System.Web.UI.Page
{
    String documentDirectory = @"X:\Harbour Application Documents\";
       
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["HarbourNumber"] == null)
        {
            Response.Redirect("Logout.aspx");
        }      

        DisplayDocuments();

        //HarbourNumber.Text = Session["HarbourNumber"].ToString();
    }

    private void DisplayDocuments()
    {
        try
        {
            if (Directory.Exists(documentDirectory + Session["HarbourNumber"].ToString()))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(documentDirectory + Session["HarbourNumber"].ToString());

                FileInfo[] fileInfo = directoryInfo.GetFiles();

                foreach (FileInfo fi in fileInfo)
                {

                    if (Session["CompanyType"].ToString() == "1")   // Shipper only
                    {
                        switch (fi.Name.ToLower())
                        {
                            case "rate quote.pdf":
                                DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Rate Quote"));
                                break;
                            case "consignment instructions ocean.pdf":
                                DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Consignment Instructions"));
                                break;
                            case "consignment instructions air.pdf":
                                DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Consignment Instructions"));
                                break;
                            case "shipment invoice.pdf":
                                DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Shipment Invoice"));
                                break;
                            case "certify.pdf":
                                DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Certified B/L"));
                                break;
                            default:
                                break;
                        }
                    }

                    switch (fi.Name.ToLower())   // Shipper and Consignee
                    {
                        case "advance notice ocean.pdf":
                            DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Advance Notice"));
                            break;
                        case "advance notice air.pdf":
                            DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Advance Notice"));
                            break;
                        case "commercial invoice.pdf":
                            DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Cmmercial Invoice"));
                            break;
                        case "packing list.pdf":
                            DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Packing List"));
                            break;
                        case "certificate of origin.pdf":
                            DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Certificate Origin"));
                            break;
                        case "final documents.pdf":
                            DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Final Documents"));
                            break;
                        case "delay notice ocean.pdf":
                            DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Delay Notice"));
                            break;
                        case "air waybill.pdf":
                            DocPanel.Controls.Add(CreateLinkButton(fi.FullName, "Air Waybill"));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorPanel.Visible = true;
            ErrorMesssage.Text = "File system error: " + ex.Message.ToString();
        }
    }

    private Control CreateLinkButton(string FileURL, string Text)
    {
        LinkButton lb = new LinkButton();
        lb.CommandArgument = FileURL;
        lb.Text = Text;
        lb.CssClass = "labelType3";
        lb.ToolTip = "View PDF file";
        lb.Command += new CommandEventHandler(LinkButton_Command);
        //lb.DataBind();
        return lb;
    }

    protected void LinkButton_Command(object sender, CommandEventArgs e)
    {        
        string fileURL = (string)e.CommandArgument;
        Response.Clear();
        Response.ContentType = "application/pdf";
        //Response.BufferOutput = true;        
        Response.WriteFile(fileURL);        
    }

}
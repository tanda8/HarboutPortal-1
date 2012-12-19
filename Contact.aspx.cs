using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net.Mail;
using System.Text;

public partial class Contact : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
    }

    // Mail Server Credentials (ToDo: change from TJE/Gmail to Karanet)
    string SmtpUserName = "ionntech8@gmail.com";
    string SmtpPassword = "picasso8";
    string MailFromAddress = "support@karanet.com";
    string MailToAddress = "tanda88@gmail.com";

    protected void SendButton_Click(object sender, EventArgs e)
    {
        string messageSubject = "Harbour Database Portal";
        string messageBody = BuildMessageBody();

        MailMessage mm = new MailMessage();
        mm.From = new MailAddress(MailFromAddress);
        mm.To.Add(MailToAddress);
        mm.Subject = messageSubject;
        mm.Body = messageBody;
        mm.IsBodyHtml = false;

        SmtpClient sc = new SmtpClient();
        sc.Host = "smtp.gmail.com";
        sc.Port = 587;
        sc.EnableSsl = true;
        sc.Credentials = new System.Net.NetworkCredential(SmtpUserName, SmtpPassword);

        SendMessage.Text = "";

        try
        {
            sc.Send(mm);            
            SendMessage.Text = "Email successfully sent";
        }
        catch (Exception ex)
        {            
            SendMessage.Text = ex.ToString();
        }
        finally
        {
        }
    }

    private string BuildMessageBody()
    {        
        StringBuilder sb = new StringBuilder();

        sb.Append("This email is from the Harbour Database Portal website");
        sb.AppendLine();
        sb.AppendLine();

        sb.Append("Name: ");
        sb.Append(FirstName.Text);
        sb.Append(" ");
        sb.Append(LastName.Text);
        sb.AppendLine();

        sb.Append("Company: ");
        sb.Append(CompanyName.Text);
        sb.AppendLine();

        sb.Append("Email: ");
        sb.Append(EmailAddress.Text);
        sb.AppendLine();

        sb.Append("Phone Number Type: ");
        sb.Append(PhoneNumberDDL.SelectedValue.ToString());
        sb.AppendLine();

        sb.Append("Phone Number: ");
        sb.Append(PhoneNumber.Text);
        sb.AppendLine();
        sb.AppendLine();

        sb.Append("Area of Interest: ");
        sb.Append(AreaOfInterestDDL.SelectedValue.ToString());
        sb.AppendLine();
        sb.AppendLine();

        if (AreaOfInterestDDL.SelectedValue.ToString() == "Shipment Assistance")
        {
            sb.Append(ReferenceNumberTypeDDL.SelectedValue.ToString());
            sb.Append(" Reference Number: ");
            sb.Append(ReferenceNumber.Text);
            sb.AppendLine();

            if (ReferenceNumberTypeDDL.SelectedValue.ToString() != "Harbour")
            {
                sb.Append(ReferenceNumberTypeDDL.SelectedValue.ToString());                
                sb.Append(" Name: ");
                sb.Append(CompanyNameTextBox.Text);
                sb.AppendLine();
            }

            sb.Append("Question or Concern:").AppendLine();
            sb.Append(QuestionOrConcern.Text);
        }

        if (AreaOfInterestDDL.SelectedValue.ToString() == "Technical Assistance")
        {            
            sb.Append("Website error message:");
            sb.AppendLine();
            sb.Append(ErrorMessage.Text);
            sb.AppendLine();
           
            sb.Append("Function performed when error occurred:");
            sb.AppendLine();
            sb.Append(FunctionPerformed.Text);
            sb.AppendLine();
           
            sb.Append("Date of error: ");
            sb.Append(DateOfError.Text);
            sb.AppendLine();
            
            sb.Append("Time of error: ");
            sb.Append(TimeOfError.Text);
            sb.AppendLine();
        }

        if (AreaOfInterestDDL.SelectedValue.ToString() == "Feedback Information")
        {
            sb.Append("Features: ");
            sb.Append(Question1.SelectedValue);
            sb.AppendLine();

            sb.Append("Notices: ");
            sb.Append(Question2.SelectedValue);
            sb.AppendLine();
            

            sb.Append("Shipment Documents: ");
            sb.Append(Question3.SelectedValue);
            sb.AppendLine();

            sb.Append("Maintains Consignee Profile Info: ");
            sb.Append(Question4.SelectedValue);
            sb.AppendLine();

            sb.Append("Harbour System Rating: ");
            sb.Append(Question5.SelectedValue);
            sb.AppendLine();

            sb.Append("Desired Functionality: ");
            sb.AppendLine();
            sb.Append(Question6.Text);
            sb.AppendLine();

            sb.Append("General Comments: ");
            sb.AppendLine();
            sb.Append(Question7.Text);
            sb.AppendLine();
        }

        return sb.ToString();
    } 

    protected void PhoneNumberDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (PhoneNumberDDL.SelectedValue == "North America")
        {
            PhoneNumber.ToolTip = "(123) 123-1234";
            PhoneNumberREV.ValidationExpression = @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}";
            PhoneNumberREV.ErrorMessage = "'Phone Number' format: (123) 123-1234";
            PhoneNumberREV.Text = "** Format: (123) 123-1234";
            PhoneNumberREV.Enabled = true;
            PhoneNumber.Focus();
        }
        else if (PhoneNumberDDL.SelectedValue == "International")
        {
            PhoneNumber.ToolTip = "";
            PhoneNumberREV.ValidationExpression = @"((((\(\d{3}\))|(\d{3}-))\d{3}-\d{4})|(\+?\d{2}((-| )\d{1,8}){1,5}))(( x| ext)\d{1,5}){0,1}";
            PhoneNumberREV.ErrorMessage = "'Phone Number' format: 'International'";
            PhoneNumberREV.Text = "**";
            PhoneNumberREV.Enabled = false;   // Disable 'international phone number' regular expression validation 
            PhoneNumber.Focus();
        }
        else
        {
            PhoneNumber.ToolTip = "";
            PhoneNumberREV.Enabled = false;
            PhoneNumberDDL.Focus();
        }
    }

    protected void AreaOfInterestDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShipmentAssistancePanel.Visible = false;
        TechnicalAssistancePanel.Visible = false;
        FeedbackInformationPanel.Visible = false;

        if (AreaOfInterestDDL.SelectedValue == "Shipment Assistance")
        {
            ShipmentAssistancePanel.Visible = true;
            ReferenceNumberTypeDDL.Focus();
        }

        if (AreaOfInterestDDL.SelectedValue == "Technical Assistance")
        {
            TechnicalAssistancePanel.Visible = true;
        }

        if (AreaOfInterestDDL.SelectedValue == "Feedback Information")
        {
            FeedbackInformationPanel.Visible = true;
        }
    }

    protected void ReferenceNumberTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ReferenceNumberTypeDDL.SelectedValue == "Shipper")
        {
            CompanyNameLabel.Text = "Shipper Name";
            CompanyNameTextBoxRFV.ErrorMessage = "'Shipper Name' is required";
            CompanyNameLiteral1.Text = "<br />";
            CompanyNameLiteral2.Text = "<br />";
            CompanyNameLabel.Visible = true;
            CompanyNameTextBox.Visible = true;
            ReferenceNumber.Focus();
        }
        else if (ReferenceNumberTypeDDL.SelectedValue == "Consignee")
        {
            CompanyNameLabel.Text = "Consignee Name";
            CompanyNameTextBoxRFV.ErrorMessage = "'Consignee Name' is required";
            CompanyNameLiteral1.Text = "<br />";
            CompanyNameLiteral2.Text = "<br />";
            CompanyNameLabel.Visible = true;
            CompanyNameTextBox.Visible = true;
            ReferenceNumber.Focus();
        }
        else if (ReferenceNumberTypeDDL.SelectedValue == "Harbour")
        {
            CompanyNameLabel.Text = "";
            CompanyNameTextBoxRFV.ErrorMessage = "";
            CompanyNameLiteral1.Text = "";
            CompanyNameLiteral2.Text = "";
            CompanyNameLabel.Visible = false;
            CompanyNameTextBox.Visible = false;
            ReferenceNumber.Focus();
        }
        else
        {
            CompanyNameLabel.Text = "";
            CompanyNameTextBoxRFV.ErrorMessage = "";
            CompanyNameLiteral1.Text = "";
            CompanyNameLiteral2.Text = "";
            CompanyNameLabel.Visible = false;
            CompanyNameTextBox.Visible = false;
            ReferenceNumberTypeDDL.Focus();
        }
    }
         
}
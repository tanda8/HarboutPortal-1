<%@ Page Title="Contact" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1>
            Contact Us</h1>
        <h3>
            Shipment / technical assistance and feedback information.</h3>
    </hgroup>

    <asp:Panel runat="server" CssClass="harbour-panel-left">
        <asp:Label runat="server" Font-Bold="True" Text="Contact Information" CssClass="harbour-gold"
            Font-Size="Large"></asp:Label><br />
        <asp:Label runat="server" Font-Bold="True" Text="First Name"></asp:Label><br />
        <asp:TextBox runat="server" ID="FirstName" Width="220px" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName" CssClass="field-validation-error"
            Display="Dynamic" Text="**" ErrorMessage="'First Name' is required" /><br />
        <asp:Label runat="server" Font-Bold="True" Text="Last Name"></asp:Label><br />
        <asp:TextBox runat="server" ID="LastName" Width="220px" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName" CssClass="field-validation-error"
            Display="Dynamic" Text="**" ErrorMessage="'Last Name' is required" /><br />
        <asp:Label runat="server" Font-Bold="True" Text="Company Name"></asp:Label><br />
        <asp:TextBox runat="server" ID="CompanyName" Width="220px" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="CompanyName" CssClass="field-validation-error"
            Display="Dynamic" Text="**" ErrorMessage="'Company Name' is required" /><br />
        <asp:Label runat="server" Font-Bold="True" Text="Email Address"></asp:Label><br />
        <asp:TextBox runat="server" ID="EmailAddress" Width="220px" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="EmailAddress" CssClass="field-validation-error"
            Display="Dynamic" Text="**" ErrorMessage="'Email Address' is required" />
        <asp:RegularExpressionValidator runat="server" ControlToValidate="EmailAddress" CssClass="field-validation-error"
            Display="Dynamic" Text="** Format: abc@abc.abc" ErrorMessage="'Email Address' format: abc@abc.abc"
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />
        <asp:Label runat="server" Font-Bold="True" Text="Confirm Email Address"></asp:Label><br />
        <asp:TextBox runat="server" ID="ConfirmEmailAddress" Width="220px" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmEmailAddress"
            CssClass="field-validation-error" Display="Dynamic" Text="**" ErrorMessage="'Confirm Email Address' is required" />
        <asp:CompareValidator runat="server" ControlToCompare="EmailAddress" ControlToValidate="ConfirmEmailAddress"
            CssClass="field-validation-error" Display="Dynamic" Text="**" ErrorMessage="Email Address mismatch" /><br />
        <asp:Label runat="server" Font-Bold="True" Text="Phone Number"></asp:Label>
        <asp:DropDownList ID="PhoneNumberDDL" runat="server" OnSelectedIndexChanged="PhoneNumberDDL_SelectedIndexChanged"
            AutoPostBack="true">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>North America</asp:ListItem>
            <asp:ListItem>International</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="PhoneNumberDDL" CssClass="field-validation-error"
            Display="Dynamic" Text="**" ErrorMessage="'Phone Number' type is required" /><br />
        <asp:TextBox runat="server" ID="PhoneNumber" Width="220px" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="PhoneNumber" CssClass="field-validation-error"
            Display="Dynamic" Text="**" ErrorMessage="'Phone Number' is required" />
        <asp:RegularExpressionValidator ID="PhoneNumberREV" runat="server" ControlToValidate="PhoneNumber"
            CssClass="field-validation-error" Display="Dynamic" Text="**"></asp:RegularExpressionValidator><br />
        <asp:Label runat="server" Font-Bold="True" Text="Area of Interest"></asp:Label><br />
        <asp:DropDownList ID="AreaOfInterestDDL" runat="server" OnSelectedIndexChanged="AreaOfInterestDDL_SelectedIndexChanged"
            AutoPostBack="true">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Shipment Assistance</asp:ListItem>
            <asp:ListItem>Technical Assistance</asp:ListItem>
            <asp:ListItem>Feedback Information</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="AreaOfInterestDDL"
            CssClass="field-validation-error" Display="Dynamic" Text="**" ErrorMessage="'Area of Interest' is required" /><br />
        <br />
        <asp:Button runat="server" Text="Check Form" />
        <asp:Button ID="SendButton" runat="server" Text="Send" OnClick="SendButton_Click" /><br />
        <br />
        <asp:ValidationSummary CssClass="field-validation-error" HeaderText="Please correct the following errors:"
            runat="server" />        
        <asp:Label ID="SendMessage" runat="server" CssClass="field-validation-error"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="ShipmentAssistancePanel" runat="server" Visible="false" CssClass="harbour-panel-right">
        <asp:Label runat="server" Font-Bold="True" Text="Shipment Assistance" CssClass="harbour-gold"
            Font-Size="Large"></asp:Label><br />
        <asp:Label runat="server" Font-Bold="True" Text="Reference Number"></asp:Label>
        <asp:DropDownList ID="ReferenceNumberTypeDDL" runat="server" OnSelectedIndexChanged="ReferenceNumberTypeDDL_SelectedIndexChanged"
            AutoPostBack="true">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Shipper</asp:ListItem>
            <asp:ListItem>Consignee</asp:ListItem>
            <asp:ListItem>Harbour</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ReferenceNumberTypeDDL"
            CssClass="field-validation-error" Display="Dynamic" Text="**" ErrorMessage="'Reference Number' type is required" /><br />
        <asp:TextBox runat="server" ID="ReferenceNumber" Width="220px" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ReferenceNumber" CssClass="field-validation-error"
            Display="Dynamic" Text="**" ErrorMessage="'Reference Number' is required" /><br />
        <asp:Label ID="CompanyNameLabel" runat="server" Font-Bold="True" Visible="false"></asp:Label>
        <asp:Literal ID="CompanyNameLiteral1" runat="server"></asp:Literal>
        <asp:TextBox runat="server" ID="CompanyNameTextBox" Width="220px" Visible="false" />
        <asp:RequiredFieldValidator ID="CompanyNameTextBoxRFV" runat="server" ControlToValidate="CompanyNameTextBox"
            CssClass="field-validation-error" Display="Dynamic" Text="**" />
        <asp:Literal ID="CompanyNameLiteral2" runat="server"></asp:Literal>
        <asp:Label runat="server" Font-Bold="True" Text="Question or Concern"></asp:Label><br />
        <asp:TextBox runat="server" ID="QuestionOrConcern" TextMode="MultiLine" Height="100%"
            Width="70%"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="QuestionOrConcern"
            CssClass="field-validation-error" Display="Dynamic" Text="**" ErrorMessage="'Question or Concern' is required" />
    </asp:Panel>

    <asp:Panel ID="TechnicalAssistancePanel" runat="server" Visible="false" CssClass="harbour-panel-right">
        <asp:Label runat="server" Font-Bold="True" Font-Size="Large" Text="Technical Assistance"
            CssClass="harbour-gold"></asp:Label><br />
        <asp:Label runat="server" Font-Bold="True" Text="System or website error message"></asp:Label>
        <asp:TextBox runat="server" ID="ErrorMessage" TextMode="MultiLine" Height="100%"
            Width="70%"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ErrorMessage" CssClass="field-validation-error"
            Display="Dynamic" Text="**" ErrorMessage="'Error Message' is required (enter 'none' if a message was not received)" /><br />
        <asp:Label runat="server" Font-Bold="True" Text="Function performed when error occurred"></asp:Label>
        <asp:TextBox runat="server" ID="FunctionPerformed" TextMode="MultiLine" Height="100%"
            Width="70%"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="FunctionPerformed"
            CssClass="field-validation-error" Display="Dynamic" Text="**" ErrorMessage="'Function Performed' is required" /><br />
        <asp:Label runat="server" Font-Bold="True" Text="Date of error"></asp:Label><br />
        <asp:TextBox runat="server" ID="DateOfError" ToolTip="mm/dd/yyyy"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="DateOfError" CssClass="field-validation-error"
            Display="Dynamic" Text="**" ErrorMessage="'Error Date' is required" />
        <asp:RegularExpressionValidator runat="server" Display="Dynamic" ControlToValidate="DateOfError"
            CssClass="field-validation-error" Text="** Format: mm/dd/yyyy" ErrorMessage="'Error Date' format: mm/dd/yyyy"
            ValidationExpression="([0]?[1-9]|1[012])[/]([0]?[1-9]|[12][0-9]|3[01])[/](19|20)\d\d"></asp:RegularExpressionValidator><br />
        <asp:Label runat="server" Font-Bold="True" Text="Time of error"></asp:Label><br />
        <asp:TextBox runat="server" ID="TimeOfError" ToolTip="hh:mm am/pm"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="TimeOfError" CssClass="field-validation-error"
            Display="Dynamic" Text="**" ErrorMessage="'Error Time' is required" />
        <asp:RegularExpressionValidator runat="server" Display="Dynamic" ControlToValidate="TimeOfError"
            CssClass="field-validation-error" Text="** Format: hh:mm am/pm" ErrorMessage="'Error Time' format: hh:mm am/pm"
            ValidationExpression="^(([0]?[1-9])|([1][0-2])):(([0-5][0-9])|([1-9])) [aApP][mM]$"></asp:RegularExpressionValidator><br />
    </asp:Panel>

    <asp:Panel ID="FeedbackInformationPanel" runat="server" Visible="false" CssClass="harbour-panel-right">
        <asp:Label runat="server" Font-Bold="True" Font-Size="Large" CssClass="harbour-gold">Feedback Information</asp:Label><br />
        <asp:Label runat="server">Please provide information for at least one of the following:</asp:Label>
        <ul>
            <li>
                <asp:Label runat="server" AssociatedControlID="Question1" Font-Bold="True">What features of our website do you find most useful?</asp:Label>
                <asp:RadioButtonList ID="Question1" runat="server" TextAlign="Right">
                    <asp:ListItem>Original Shipment Schedule Details</asp:ListItem>
                    <asp:ListItem>Current Shipment Tracing Details</asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li>
                <asp:Label runat="server" AssociatedControlID="Question2" Font-Bold="True">Availability of Harbour notices</asp:Label>
                <asp:RadioButtonList ID="Question2" runat="server">
                    <asp:ListItem>Advance Notice</asp:ListItem>
                    <asp:ListItem>Delay Notice</asp:ListItem>
                    <asp:ListItem>Advise of Shipment </asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li>
                <asp:Label runat="server" AssociatedControlID="Question3" Font-Bold="True">Availability of shipment documents</asp:Label>
                <asp:RadioButtonList ID="Question3" runat="server">
                    <asp:ListItem>Commercial Invoice</asp:ListItem>
                    <asp:ListItem>Packing List</asp:ListItem>
                    <asp:ListItem>Bill of Lading</asp:ListItem>
                    <asp:ListItem>Document tracing via Fed Ex link</asp:ListItem>
                    <asp:ListItem>Statistical Measurements of logistics process</asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li>
                <asp:Label runat="server" AssociatedControlID="Question4" Font-Bold="True">Consignees, do you use the Profile Form to keep your requirements and contact information current?</asp:Label>
                <asp:RadioButtonList ID="Question4" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li>
                <asp:Label runat="server" AssociatedControlID="Question5" Font-Bold="True">Rank the Harbour website on a scale of 1 to 5.</asp:Label><br />
                <asp:Label runat="server" AssociatedControlID="Question5">(1 = very good; 3 = average; 5 = very bad)</asp:Label>
                <asp:RadioButtonList ID="Question5" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li>
                <asp:Label runat="server" AssociatedControlID="Question6" Font-Bold="True">What functionality or service would you like added to the Harbour website?</asp:Label><br />
                <asp:TextBox ID="Question6" TextMode="MultiLine" runat="server" Height="100%" Width="70%"></asp:TextBox>
            </li>
            <li>
                <asp:Label runat="server" AssociatedControlID="Question7" Font-Bold="True">Please share any comments.</asp:Label><br />
                <asp:TextBox ID="Question7" TextMode="MultiLine" runat="server" Height="100%" Width="70%"></asp:TextBox>
            </li>
        </ul>
    </asp:Panel>
</asp:Content>

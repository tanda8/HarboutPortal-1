<%@ Page Title="Login" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <hgroup class="title">
        <h1>
            Database Portal
            <%:Page.Title %></h1>
        <h3>
            Enter your username and password below.</h3>
    </hgroup>
    <asp:Login ID="UserLogin" runat="server" EnableViewState="true" DestinationPageUrl="~/Overview.aspx"
        RenderOuterTable="false" OnAuthenticate="Login_Authenticate" FailureAction="Refresh">
        <LayoutTemplate>
            <p class="validation-summary-errors">
                <asp:Literal runat="server" ID="FailureText" />
            </p>
            <fieldset>
                <legend>Log in Form</legend>
                <ol>
                    <li>
                        <asp:Label runat="server" AssociatedControlID="UserName">Username</asp:Label>
                        <asp:TextBox runat="server" ID="Username" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Username" CssClass="field-validation-error"
                            ErrorMessage="Username is required." />
                    </li>
                    <li>
                        <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="field-validation-error"
                            ErrorMessage="Password is required." />
                    </li>
                </ol>
                <asp:Button runat="server" CommandName="Login" Text="Login" />
            </fieldset>
        </LayoutTemplate>
    </asp:Login>
    <p>
        <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register</asp:HyperLink>
        if you don't have an account.
    </p>
    <p>
        <asp:Label ID="LoginError" runat="server" CssClass="message-error"></asp:Label>
    </p>
    <p>
        <br />
        <br />
        Test Logins
        <br />
        Consignee -> [bpkorea] [ke9t4] | [ildongfoodis] [t6u8v] | [scjargentina] [95s48]
        <br />
        Shipper -> [fritolaypr] [pr59k] | [lorealusa] [95s48] | [moen] [fp9ij] | [test]
        [abc123]
    </p>
</asp:Content>

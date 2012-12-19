<%@ Page Title="Profile" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Profile.aspx.cs" Inherits="CompanyProfile" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <hgroup class="title">
        <h1>
            Company Profile -
            <%= Session["FullName"].ToString() %>
        </h1>
    </hgroup>
</asp:Content>

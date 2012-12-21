<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search2.aspx.cs" Inherits="Search2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <hgroup class="title">
        <h1>Search - <%= Session["FullName"].ToString() %></h1>
    </hgroup>

    <asp:Panel runat="server" CssClass="searchPanel">
        <asp:Label runat="server" CssClass="labelType1" AssociatedControlID="SearchParameterDDL" Text="Search Parameter:"></asp:Label>
        <asp:DropDownList ID="SearchParameterDDL" runat="server" AutoPostBack="True" CssClass="dropDownList"
            OnSelectedIndexChanged="SearchParameterDDL_SelectedIndexChanged">
            <asp:ListItem Selected="True"></asp:ListItem>
            <asp:ListItem Value="HarbourNumber">Harbour Number</asp:ListItem>
            <asp:ListItem Value="BookingNumber">Booking Number</asp:ListItem>
            <asp:ListItem Value="PlaceOfDelivery">Place of Delivery (Ocean)</asp:ListItem>
            <asp:ListItem Value="ShipperName">Shipper Name</asp:ListItem>
            <asp:ListItem Value="ConsigneeName">Consignee Name</asp:ListItem>
            <asp:ListItem Value="ShipperReferenceNumber">Shipper Reference Number</asp:ListItem>
            <asp:ListItem Value="ConsigneeReferenceNumber">Consignee Reference Number</asp:ListItem>
            <asp:ListItem Value="AirWaybillNumber">Air Waybill Number</asp:ListItem>
            <asp:ListItem Value="NvoBookingNumber">NVO Booking Number</asp:ListItem>
        </asp:DropDownList>
    </asp:Panel> 

    <asp:Panel ID="SearchPanel" runat="server" CssClass="CustomPanel" Visible="false">
        <asp:Label runat="server" CssClass="labelType1" AssociatedControlID="SearchComboBox" Text="Search Value:"></asp:Label>
        <asp:ComboBox ID="SearchComboBox" runat="server" CssClass="ComboBox" 
            DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" 
            AutoPostBack="True"></asp:ComboBox>
        <asp:Button ID="SearchButton" runat="server" OnClick="SearchButton_Click" ToolTip="Run Search" Text="Go" />
    </asp:Panel>
   
    <asp:Panel ID="MessagePanel" runat="server" CssClass="detailPanel" Visible="false">
        <asp:Label ID="Message" runat="server" CssClass="message-error"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="ErrorPanel" runat="server" CssClass="detailPanel" Visible="false">
        <asp:Label ID="ErrorMesssage" runat="server" CssClass="message-error"></asp:Label>
    </asp:Panel>

</asp:Content>
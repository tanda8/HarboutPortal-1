<%@ Page Title="Search" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager runat="server">
    </asp:ToolkitScriptManager>
    <hgroup class="title">
        <h1>
            Search -
            <%= Session["FullName"].ToString() %>
        </h1>
    </hgroup>
    <asp:Panel runat="server" CssClass="searchPanel">
        <asp:Label runat="server" CssClass="labelType1" AssociatedControlID="SearchParameterDDL"
            Text="Search Parameter:"></asp:Label>
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

        <%--<asp:Label runat="server" CssClass="labelType1" AssociatedControlID="SearchPeriodDDL"
            Text="Search Period:"></asp:Label>
        <asp:DropDownList ID="SearchPeriodDDL" runat="server" AutoPostBack="True" CssClass="dropDownList"
            OnSelectedIndexChanged="SearchPeriodDDL_SelectedIndexChanged">
            <asp:ListItem Selected="True"></asp:ListItem>
            <asp:ListItem Value="-1">1 month</asp:ListItem>
            <asp:ListItem Value="-2">2 months</asp:ListItem>
            <asp:ListItem Value="-3">3 months</asp:ListItem>
            <asp:ListItem Value="-4">4 months</asp:ListItem>
            <asp:ListItem Value="-5">5 months</asp:ListItem>
            <asp:ListItem Value="-6">6 months</asp:ListItem>
            <asp:ListItem Value="-9">9 months</asp:ListItem>
            <asp:ListItem Value="-12">1 year</asp:ListItem>
            <asp:ListItem Value="-24">2 years</asp:ListItem>
            <asp:ListItem Value="-36">3 years</asp:ListItem>
            <asp:ListItem Value="-48">4 years</asp:ListItem>
            <asp:ListItem Value="-60">5 years</asp:ListItem>
            <asp:ListItem Value="-120">10 years</asp:ListItem>
            <asp:ListItem Value="-240">20 years</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="SearchPeriodLabel" runat="server" CssClass="labelType2" Visible="false"></asp:Label>--%>

    </asp:Panel> 

    <asp:Panel ID="HarbourNumberPanel" runat="server" CssClass="CustomPanel" Visible="false">
        <asp:Label ID="HarbourNumberLabel" runat="server" CssClass="Label" Text="Harbour Number:"></asp:Label>
        <asp:ComboBox ID="HarbourNumberComboBox" runat="server" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"></asp:ComboBox>
        <asp:Button ID="HarbourNumberButton" runat="server" OnClick="HarbourNumberButton_Click" ToolTip="Run Search" Text="Go" />
    </asp:Panel>

    <asp:Panel ID="BookingNumberPanel" runat="server" CssClass="CustomPanel" Visible="false">
        <asp:Label ID="BookingNumberLabel" runat="server" CssClass="Label" Text="Booking Number:"></asp:Label>
        <asp:ComboBox ID="BookingNumberComboBox" runat="server" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"></asp:ComboBox>
        <asp:Button ID="BookingNumberButton" runat="server" OnClick="BookingNumberButton_Click" ToolTip="Run Search" Text="Go" />
    </asp:Panel>

    <asp:Panel ID="AirWaybillNumberPanel" runat="server" CssClass="CustomPanel" Visible="false">
        <asp:Label ID="AirWaybillNumberLabel" runat="server" CssClass="Label" Text="Air Waybill Number:"></asp:Label>
        <asp:ComboBox ID="AirWaybillNumberComboBox" runat="server" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"></asp:ComboBox>
        <asp:Button ID="AirWaybillNumberButton" runat="server" OnClick="AirWaybillNumberButton_Click" ToolTip="Run Search" Text="Go" />
    </asp:Panel>

    <asp:Panel ID="NvoBookingNumberPanel" runat="server" CssClass="CustomPanel" Visible="false">
        <asp:Label ID="NvoBookingNumberLabel" runat="server" CssClass="Label" Text="NVO Booking Number:"></asp:Label>
        <asp:ComboBox ID="NvoBookingNumberComboBox" runat="server" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"></asp:ComboBox>
        <asp:Button ID="NvoBookingNumberButton" runat="server" OnClick="NvoBookingNumberButton_Click" ToolTip="Run Search" Text="Go" />
    </asp:Panel>

    <asp:Panel ID="MessagePanel" runat="server" CssClass="detailPanel" Visible="false">
        <asp:Label ID="Message" runat="server" CssClass="message-error"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="ErrorPanel" runat="server" CssClass="detailPanel" Visible="false">
        <asp:Label ID="ErrorMesssage" runat="server" CssClass="message-error"></asp:Label>
    </asp:Panel>
</asp:Content>

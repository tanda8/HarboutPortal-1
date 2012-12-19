<%@ Page Title="Overview" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Overview.aspx.cs" Inherits="Overview" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <hgroup class="title">
        <h1>
            Shipment Overview -
            <%= Session["FullName"].ToString() %>
        </h1>
    </hgroup>
    <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False" PageSize="15"
        AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView_PageIndexChanging"
        OnSorting="GridView_Sorting" OnRowCommand="GridView_RowCommand" EmptyDataText="No data was found."
        CssClass="gridview" PagerStyle-CssClass="gv-pager" AlternatingRowStyle-CssClass="gv-altrow"
        EmptyDataRowStyle-CssClass="gv-emptyrow" HeaderStyle-CssClass="">
        <Columns>
            <asp:BoundField DataField="HARBOURNUMBER" HeaderText="Harbour #" SortExpression="HARBOURNUMBER" />
            <asp:BoundField DataField="SHIPPERNAME" HeaderText="Shipper" SortExpression="SHIPPERNAME" />
            <asp:BoundField DataField="CONSIGNEENAME" HeaderText="Consignee" SortExpression="CONSIGNEENAME" />
            <asp:BoundField DataField="POD" HeaderText="Place of Delivery" SortExpression="POD" />
            <asp:BoundField DataField="FILLTYPE" HeaderText="Container " SortExpression="FILLTYPE" />
            <asp:BoundField DataField="COMMODITY" HeaderText="Commodity" SortExpression="COMMODITY" />
            <asp:BoundField DataField="DATEREQUESTED" HeaderText="Date" SortExpression="DATEREQUESTED" />
            <asp:ButtonField ButtonType="Button" HeaderText="Detail" ItemStyle-HorizontalAlign="Center" CommandName="Detail"></asp:ButtonField>
            <asp:ButtonField ButtonType="Button" HeaderText="Trace" ItemStyle-HorizontalAlign="Center" CommandName="Trace"></asp:ButtonField>
            <asp:ButtonField ButtonType="Button" HeaderText="Docs" ItemStyle-HorizontalAlign="Center" CommandName="Documents"></asp:ButtonField>
        </Columns>
        <PagerSettings Mode="NumericFirstLast" />
    </asp:GridView>
    <asp:Panel ID="ErrorPanel" runat="server" CssClass="detailPanel" Visible="false">
        <asp:Label ID="ErrorMesssage" runat="server" CssClass="message-error"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="TestPanel" runat="server" Visible="false">
        <asp:Label ID="TestLabel" runat="server"></asp:Label>
    </asp:Panel>
</asp:Content>
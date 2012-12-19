<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Documents.aspx.cs" Inherits="Documents" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:LinkButton runat="server" CssClass="detailButton" PostBackUrl="Detail.aspx">Detail</asp:LinkButton>
    <asp:LinkButton runat="server" CssClass="detailButton" PostBackUrl="Trace.aspx">Trace</asp:LinkButton>
    <hgroup class="title">
        <h1>
            Shipment Documents
        </h1>
    </hgroup>
    <asp:Panel ID="DocPanel" runat="server" CssClass="detailPanel">
        <%--<asp:Label runat="server" CssClass="labelType1" Text="Harbour Number:"></asp:Label>--%>
        <%--<asp:Label ID="HarbourNumber" runat="server" CssClass="labelType2"></asp:Label>--%>        
    </asp:Panel>
    <asp:Panel ID="ErrorPanel" runat="server" CssClass="detailPanel" Visible="false">
        <asp:Label ID="ErrorMesssage" runat="server" CssClass="message-error"></asp:Label>
    </asp:Panel>
</asp:Content>

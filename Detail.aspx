<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Detail.aspx.cs" Inherits="Detail" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:LinkButton runat="server" CssClass="detailButton" PostBackUrl="Trace.aspx">Trace</asp:LinkButton>
    <asp:LinkButton runat="server" CssClass="detailButton" PostBackUrl="Documents.aspx">Docs</asp:LinkButton>
    <hgroup class="title">
        <h1>
            Shipment Detail
        </h1>        
    </hgroup>
    <asp:Panel runat="server" CssClass="detailPanel">
        <%--General Info--%>
        <asp:Label runat="server" CssClass="labelType1" Text="Harbour Number:"></asp:Label>
        <asp:Label ID="HarbourNumber" runat="server" CssClass="labelType2"></asp:Label>
        <asp:Label runat="server" CssClass="labelType1" Text="Shipper:"></asp:Label>
        <asp:Label ID="Shipper" runat="server" CssClass="labelType2"></asp:Label>
        <asp:Label runat="server" CssClass="labelType1" Text="Consignee:"></asp:Label>
        <asp:Label ID="Consignee" runat="server" CssClass="labelType2"></asp:Label>
        <asp:Label runat="server" CssClass="labelType1" Text="Request Date:"></asp:Label>
        <asp:Label ID="RequestDate" runat="server" CssClass="labelType2"></asp:Label>
        <asp:Label runat="server" CssClass="labelType1" Text="Place of Receipt:"></asp:Label>
        <asp:Label ID="PlaceOfReceipt" runat="server" CssClass="labelType2"></asp:Label>
        <asp:Label runat="server" CssClass="labelType1" Text="Place of Delivery:"></asp:Label>
        <asp:Label ID="PlaceOfDelivery" runat="server" CssClass="labelType2"></asp:Label>
        <asp:Label runat="server" CssClass="labelType1" Text="Incoterm:"></asp:Label>
        <asp:Label ID="Incoterm" runat="server" CssClass="labelType2"></asp:Label>
        <asp:Label runat="server" CssClass="labelType1" Text="Commodity:"></asp:Label>
        <asp:Label ID="Commodity" runat="server" CssClass="labelType2"></asp:Label>
        <asp:Label runat="server" CssClass="labelType1" Text="Hazardous:"></asp:Label>
        <asp:Label ID="Hazardous" runat="server" CssClass="labelType2"></asp:Label>
        <%--Reference Number--%>
        <asp:Label ID="Reference1" runat="server" CssClass="labelType1" Text="Reference:" Visible="false"></asp:Label>
        <asp:Label ID="Reference2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <%--Transport Type--%>
        <asp:Label ID="Transportation1" runat="server" CssClass="labelType1" Text="Transportation:" Visible="false"></asp:Label>
        <asp:Label ID="Transportation2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <%--Type: FCL--%>
        <asp:GridView ID="FclGridView" runat="server" AutoGenerateColumns="False" CssClass="detailGridView" Visible="false">
            <Columns>
                <asp:BoundField DataField="CONTAINERCODE" HeaderText="Container" />
                <asp:BoundField DataField="CONTAINERNUMBER" HeaderText="Number" />
                <asp:BoundField DataField="WEIGHTLBS" HeaderText="Weight (lbs)" />
            </Columns>
        </asp:GridView>
        <%--Type: LCL--%>
        <asp:Label ID="LclTotalPieces1" runat="server" CssClass="labelType1" Text="Total Pieces:" Visible="false"></asp:Label>
        <asp:Label ID="LclTotalPieces2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="LclTotalWeight1" runat="server" CssClass="labelType1" Text="Total Weight:" Visible="false"></asp:Label>
        <asp:Label ID="LclTotalWeight2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="LclTotalVolume1" runat="server" CssClass="labelType1" Text="Total Volume:" Visible="false"></asp:Label>
        <asp:Label ID="LclTotalVolume2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:GridView ID="LclGridView" runat="server" AutoGenerateColumns="false" CssClass="detailGridView" Visible="false">
            <Columns>
                <asp:BoundField DataField="PackageCode" HeaderText="Package" />
                <asp:BoundField DataField="NumPieces" HeaderText="Pieces" />
                <asp:BoundField DataField="Length" HeaderText="Length (in)" />
                <asp:BoundField DataField="Width" HeaderText="Width (in)" />
                <asp:BoundField DataField="Height" HeaderText="Height (in)" />                
                <%--<asp:BoundField DataField="Volume" HeaderText="Volume (ft3)" />--%>
                <asp:BoundField DataField="WeightLBS" HeaderText="Weight (lbs)" />
            </Columns>
        </asp:GridView>
        <%--Type: Air--%>
        <asp:Label ID="AirTotalPieces1" runat="server" CssClass="labelType1" Text="Total Pieces:" Visible="false"></asp:Label>
        <asp:Label ID="AirTotalPieces2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="AirTotalWeight1" runat="server" CssClass="labelType1" Text="Total Weight:" Visible="false"></asp:Label>
        <asp:Label ID="AirTotalWeight2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="AirTotalVolume1" runat="server" CssClass="labelType1" Text="Total Volume:" Visible="false"></asp:Label>
        <asp:Label ID="AirTotalVolume2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:GridView ID="AirGridView" runat="server" AutoGenerateColumns="false" CssClass="detailGridView" Visible="false">
            <Columns>
                <asp:BoundField DataField="PackageCode" HeaderText="Package" />
                <asp:BoundField DataField="NumPieces" HeaderText="Pieces" />
                <asp:BoundField DataField="Length" HeaderText="Length (in)" />
                <asp:BoundField DataField="Width" HeaderText="Width (in)" />
                <asp:BoundField DataField="Height" HeaderText="Height (in)" />                
                <%--<asp:BoundField DataField="Volume" HeaderText="Volume (ft3)" />--%>
                <asp:BoundField DataField="Weight" HeaderText="Weight (lbs)" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="ErrorPanel" runat="server" CssClass="detailPanel" Visible="false">
        <asp:Label ID="ErrorMesssage" runat="server" CssClass="message-error"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" CssClass="detailPanel" Visible="false">
        Test -> TransportType:
        <%= TransportType%>
        | OceanContainerType:
        <%= OceanContainerType%>
    </asp:Panel>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Trace.aspx.cs" Inherits="Trace" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:LinkButton runat="server" CssClass="detailButton" PostBackUrl="Detail.aspx">Detail</asp:LinkButton>
    <asp:LinkButton runat="server" CssClass="detailButton" PostBackUrl="Documents.aspx">Docs</asp:LinkButton>
    <hgroup class="title">
        <h1>
            Shipment Trace
        </h1>
    </hgroup>
    <asp:Panel ID="AirTracePanel" runat="server" CssClass="detailPanel" Visible="false">
        <asp:Label ID="HarbourNumber1" runat="server" CssClass="labelType1" Text="Harbour Number:" Visible="false"></asp:Label>
        <asp:Label ID="HarbourNumber2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="AirWaybill1" runat="server" CssClass="labelType1" Text="Air Waybill:" Visible="false"></asp:Label>
        <asp:Label ID="AirWaybill2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="TotalPieces1" runat="server" CssClass="labelType1" Text="Total Pieces:" Visible="false"></asp:Label>
        <asp:Label ID="TotalPieces2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="TotalWeight1" runat="server" CssClass="labelType1" Text="Total Weight:" Visible="false"></asp:Label>
        <asp:Label ID="TotalWeight2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="TotalVolume1" runat="server" CssClass="labelType1" Text="Total Volume:" Visible="false"></asp:Label>
        <asp:Label ID="TotalVolume2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:GridView ID="AirEventGridView" runat="server" AutoGenerateColumns="False" CssClass="detailGridView" Visible="false">
            <Columns>
                <asp:BoundField DataField="Depart" HeaderText="Departure Airport" />
                <asp:BoundField DataField="Flight" HeaderText="Flight Number" />
                <asp:BoundField DataField="FlightDate" HeaderText="Departure Date" />
                <asp:BoundField DataField="Arrival" HeaderText="Arrival Airport" />
                <asp:BoundField DataField="Event" HeaderText="Trace Event" />
                <asp:BoundField DataField="Expected" HeaderText="Expected Date" />
            </Columns>
        </asp:GridView>
     </asp:Panel>   

    <asp:Panel ID="OceanTracePanel" runat="server" CssClass="detailPanel" Visible="false">
        <asp:Label ID="oHarbourNumber1" runat="server" CssClass="labelType1" Text="Harbour Number:" Visible="false"></asp:Label>
        <asp:Label ID="oHarbourNumber2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="oBookingNumber1" runat="server" CssClass="labelType1" Text="Booking Number:" Visible="false"></asp:Label>
        <asp:Label ID="oBookingNumber2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="oOceanCarrier1" runat="server" CssClass="labelType1" Text="Ocean Carrier:" Visible="false"></asp:Label>
        <asp:Label ID="oOceanCarrier2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="oTotalPieces1" runat="server" CssClass="labelType1" Text="Total Pieces:" Visible="false"></asp:Label>
        <asp:Label ID="oTotalPieces2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="oTotalWeight1" runat="server" CssClass="labelType1" Text="Total Weight:" Visible="false"></asp:Label>
        <asp:Label ID="oTotalWeight2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>        
        <asp:Label ID="oTotalVolume1" runat="server" CssClass="labelType1" Text="Total Volume:" Visible="false"></asp:Label>
        <asp:Label ID="oTotalVolume2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <%--Trace Details--%>
        <%--<asp:Label ID="oShipperCutoff1" runat="server" CssClass="labelType1" Text="Shipper Cutoff:" Visible="false"></asp:Label>
        <asp:Label ID="oShipperCutoff2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="oTerminalCutoff1" runat="server" CssClass="labelType1" Text="Terminal Cutoff:" Visible="false"></asp:Label>
        <asp:Label ID="oTerminalCutoff2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="oPortCutoff1" runat="server" CssClass="labelType1" Text="Port Cutoff:" Visible="false"></asp:Label>
        <asp:Label ID="oPortCutoff2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>        
        <asp:Label ID="oSailDate1" runat="server" CssClass="labelType1" Text="Sailing Departure (plan):" Visible="false"></asp:Label>
        <asp:Label ID="oSailDate2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>        
        <asp:Label ID="oActualSailDate1" runat="server" CssClass="labelType1" Text="Sailing Departure (actual):" Visible="false"></asp:Label>
        <asp:Label ID="oActualSailDate2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="oDischargeDate1" runat="server" CssClass="labelType1" Text="Port of Discharge Arrival:" Visible="false"></asp:Label>
        <asp:Label ID="oDischargeDate2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>
        <asp:Label ID="oPodDate1" runat="server" CssClass="labelType1" Text="Place of Delivery Arrival:" Visible="false"></asp:Label>
        <asp:Label ID="oPodDate2" runat="server" CssClass="labelType2" Visible="false"></asp:Label>--%>
        <asp:GridView ID="OceanTraceGridView" runat="server" AutoGenerateColumns="false" CssClass="detailGridView" Visible="false">
            <Columns>
                <asp:BoundField DataField="ShipperCutoff" HeaderText="Shipper Cutoff" />
                <asp:BoundField DataField="TerminalCutoff" HeaderText="Terminal Cutoff" />
                <asp:BoundField DataField="PortCutoff" HeaderText="Port Cutoff" />
                <asp:BoundField DataField="SailDate" HeaderText="Scheduled Sailing" />
                <asp:BoundField DataField="ActualSailDate" HeaderText="Confirmed Sailing" />
                <asp:BoundField DataField="DischargeDate" HeaderText="Port of Discharge" />
                <asp:BoundField DataField="PodDate" HeaderText="Place of Delivery" />                
            </Columns>
        </asp:GridView>
        <asp:GridView ID="OceanEventGridView" runat="server" AutoGenerateColumns="False" CssClass="detailGridView" Visible="false">
            <Columns>
                <asp:BoundField DataField="ContainerID" HeaderText="Container" />
                <asp:BoundField DataField="VesselName" HeaderText="Vessel" />
                <asp:BoundField DataField="Voyage" HeaderText="Voyage #" />
                <asp:BoundField DataField="Event" HeaderText="Trace Event" />
                <asp:BoundField DataField="Expected1" HeaderText="Expected Date" />
                <asp:BoundField DataField="Actual" HeaderText="Actual Date" />                
            </Columns>
        </asp:GridView>    
    </asp:Panel>
    <asp:Panel ID="ErrorPanel" runat="server" CssClass="detailPanel" Visible="false">
        <asp:Label ID="ErrorMesssage" runat="server" CssClass="message-error"></asp:Label>
    </asp:Panel>
</asp:Content>
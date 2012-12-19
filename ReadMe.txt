**************************************************************************************
** Test Logins
 
Consignees
* bpkorea [ke9t4]
* ildongfoodis [t6u8v]
* scjargentina [95s48] 

Shippers
* fritolaypr [pr59k]
* lorealusa [95s48]
* moen [fp9ij]
* test [abc123]

ToDo
* J.M. Smucker Co.

**************************************************************************************
** General Info

Documents
* C:\Users\tenright\Documents

Libraries
* C:\HarbourLogisticsApp VB Libraries\Harbour_Client
* C:\HarbourLogisticsApp VB Libraries\Harbour

Visual Studio
* C:\Users\tenright\Documents\Visual Studio 2010\WebSites\

Project: HarbourPortalNew

**************************************************************************************
** Search Problem

Page: Search1

ToDO
* Impliment Search Functions: POD (Ocean), Shipper Name, Consignee Name
* Shipper/Consignee Reference Number - Dropdown Dups

Unique Searches
* Harbour Number 
* Booking Number
* Air Waybill Number
* NVO Booking Number

Non-Unique Searches
* Company Name
* Place of Delivery (Ocean)
* Shipper Reference   ???
* Consignee Reference   ???

Search Dropdown Dup Problem
* lorealusa
  * Consignee Reference Number
		SCENTSTRIP
		SHU-RETURN
  * Shipper Reference Number
		LANCOME 02-260-4425
* moen
  * Consignee Reference Number   (many dups)
		10001
		10018
		10072
		10155
		10193
		12737
		12741
		PR0271
		RETURN
		RETURN GOODS
		Return Goods
		Returns
		SPOUTS
		SS0001
		SS001

* Analysis
PR0271 -> AF15361 -> Ref# 05-3420
PR0271 -> H57891 -> 05-3400
PR0271 -> H57931 -> 05-3468
PR0271 -> H57937 -> 05-3475
SS001 -> H50203 -> 02-2557
SS001 -> H50545 -> 02-3507
SS001 -> H50589 -> 02-3550


**************************************************************************************
** ToDo

1. **DONE** Clear 'Welcome, xxx' and set button to 'Login' when session variables time out (MasterPage -> LoggedInTemplate).
2. Add GridView calculated column.  Detail page -> LCL Detail table -> Item Volume ft3
3. Change public variables (TransportType, OceanContainerType) to private on Detail page.
4. **DONE** Add Detail, Trace and Docs buttons to Detail/Trace/Docs pages.
5. **DONE** Place Trace Details (Trace page) in a GridView.
6. **DONE** Change Tracing/Event verbage back to original portal verbage.
7. Clean up button CSS.
8. IE9 32-bit BackButton bug (Docs)
9. Welcome text & Login/Logout button - Ocasionally wrong? Due to timeout?

* Fix EmptyDataRowStyle-CssClass="gv-emptyrow"
* Fix Search page CSS
* Fix ComboBox -> have index 0 show a blank. (Can ComboBox do this or does the DataSet need a null first row)
     DataTextField, DataValueField, SelectedIndex to -1, 
* Search -> ajaxcontroltoolkit combobox css
* Fix/cosolidate Searches

**************************************************************************************
** Notes

PDF
* Google Chrome: Built in PDF viewer
* Mozilla Firefox: Adobe Acrobat - Adobe PDF Plug-In For Firefox and Netscape 10.1.3
* Apple Opera: Adobe Acrobat - Adobe PDF Plug-In For Firefox and Netscape 10.1.3
* Microsoft IE: Adobe PDF Link Helper - Browser Helper Object 10.1.3.23

Problem
* IE9 32-bit BackButton returns to 2nd prevous page not 1st previous page (Page history problem??)

**************************************************************************************
** Code

RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
RegisterHyperLink.NavigateUrl = "Register.aspx?" + HttpUtility.UrlEncode("xxx");
RegisterHyperLink.NavigateUrl = "Register.aspx?xxx";

*****

ContentPlaceHolder cp = (ContentPlaceHolder)Master.FindControl("MainContent");
if (cp != null)
{
    LoginView lv = (LoginView)cp.FindControl("LoginViewID");
    if (lv != null)
    {
        LoginName ln = (LoginName)lv.FindControl("LoginNameID");
        {
        }
    }
}

*****

protected void SearchButton_Click(object sender, EventArgs e)   // ???
{
    Button button = (Button)sender;
    Session["HarbourNumber"] = (button.CommandArgument.ToString()).SelectedValue.ToString();
    Response.Redirect("SearchResults.aspx", false);
}



**************************************************************************************
** Temp

X:\Harbour Application Documents\H74197\Advance Notice Ocean.pdf


**************************************************************************************
** GAC - Oracle

Install DLL into GAC: Command Prompt
#gacutil /i C:\app\administrator\product\11.2.0\client_1\odp.net\bin\4\Oracle.DataAccess.dll
#gacutil /i C:\app\administrator\product\11.2.0\client_1\ASP.NET\bin\4\oracle.web.dll

View GAC: Command Prompt
#gacutil /l Oracle.DataAccess
#gacutil /l Oracle.Web

View GAC: Windows Explorer -> C:\Windows\assembly\
View GAC: Windows Explorer -> C:\Windows\Microsoft.NET\assembly\

C:\Windows\Microsoft.NET\assembly\GAC_32\Oracle.DataAccess\v4.0_4.112.2.0__89b483f429c47342\Oracle.DataAccess.dll
C:\Windows\Microsoft.NET\assembly\GAC_32\Oracle.Web\v4.0_4.112.2.0__89b483f429c47342\oracle.web.dll


**************************************************************************************
** GAC - Oracle

http://forums.asp.net/t/1664258.aspx/1

Oracle.Web.dll
#cd C:\app\administrator\product\11.2.0\client_1\ASP.NET\bin\4\
#oraprovcfg /action:gac /providerpath:C:\app\administrator\product\11.2.0\client_1\ASP.NET\bin\4\Oracle.Web.dll

Oracle.DataAcess.dll
#cd C:\app\administrator\product\11.2.0\client_1\odp.net\bin\4\
#oraprovcfg /action:gac /providerpath:C:\app\administrator\product\11.2.0\client_1\odp.net\bin\4\Oracle.DataAccess.dll

Enable 32-bit Applications
IIS -> Application Pools -> ASP.NEt v4.0 -> Advanced Settings -> Enable 32-bit Applications -> true

Task Manager
 Image Name: w3wp.exe*32
 Username: ASP.NET v4.0
 Description: IIS Worker Process

ToDo
 * Give IIS user 'ASP.NET v4.0' read access to folder X:\Harbour Application Documents\


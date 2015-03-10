<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="searchvendor.aspx.cs" Inherits="searchvendor" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Globe Automated Vendor Accreditation :: Vendor
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
<div class="content_logo">
<img src="images/logo_hck.png" width="229" height="105" border="0" />
</div>
<div class="rounded-corners-top" id="menuAVA">
<ava:tabsnav ID="Tabsnav1" runat="server" />
</div>
<div style="background:#FFF; min-height:445px; padding:10px;" class="rounded-corners-bottom2 menu">
<!--##################-->
<!--BODY CONTENT STARTS-->
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_vmofficer.ascx" %>
<%--<ava:topnav ID="TopNav1" runat="server" />--%>

<form action="" method="post" name="formVendorSearch" id="formVendorSearch" runat="server">
<div style="float:none; margin:25px 0 0 0px; padding-top:0px; font-size:14px;">
<%--<form action="" method="post" name="formVendorInfo" id="formVendorInfo">
  <h3>Enter Authentication Ticket</h3><br />
    <input name="textfield" type="text" id="textfield" size="28" />
  <br /><br />
  <div class="clearfix"></div>
  <a href="dnb2_listofvendors.html" class="bt1" style="margin-right:110px; float:left;"><span>Submit</span></a>
  <div class="clearfix"></div>
</form><br />
<br />--%>

  <h3>Search Vendor</h3>
  <br />
    <%--<input name="CompanyName" type="text" id="CompanyName" size="38" style="float:left; height:30px;" runat="server" />--%>
    <asp:RadioButtonList ID="VendorType" runat="server" RepeatDirection="Horizontal" style="float:left" CellPadding="3" CellSpacing="3">
        <asp:ListItem Selected="True"> Qualified</asp:ListItem>
        <asp:ListItem> Applicant</asp:ListItem>
    </asp:RadioButtonList>

    <asp:TextBox ID="CompanyName" name="CompanyName" runat="server" style="float:left; height:30px; width:400px;" onclick="$(this).select();"></asp:TextBox>
  <%--<a href="#" class="bt1" style="margin-right:110px; float:left; margin-left:5px;" runat="server" onclientclick=" __doPostBack('justSave'); return false;"><span>Search</span></a>--%>

    <asp:LinkButton ID="searchBt1" runat="server" CssClass="bt1"  onclientclick=" __doPostBack('justSave'); return false;" style="margin-right:110px; float:left; margin-left:5px;" ><span>Search</span></asp:LinkButton>
  <div class="clearfix"></div>
</div>

    <div class="clearfix"></div>
<%@ Register TagPrefix="Ava" TagName="list_search" Src="usercontrols/list_search.ascx" %>
<ava:list_search ID="list_search1" runat="server" />



</form>
<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

</asp:Content>
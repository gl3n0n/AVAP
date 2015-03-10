<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vmofficer_Report.aspx.cs" Inherits="vmofficer_Report" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Globe Automated Vendor Accreditation :: Vendor
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!--### DATEPICKER ###-->
	<script type="text/javascript" src="Scripts/cal.js"></script>
	<link rel="stylesheet" type="text/css" href="Styles/calendar_picker.css" />
    <script type="text/javascript">
        function reloadDatepicker() {
            $('input.date').click();
        }
        jQuery(document).ready(function () {
            $('input.date').simpleDatepicker({ startdate: '6/10/1900' });
            $('input.date').keyup(function (e) {
                console.log('keyup called');
                var code = e.keyCode || e.which;
                if (code == '9') {
                    reloadDatepicker();
                }

            });
        });
	</script> 
	<script type="text/javascript">
	    jQuery(document).ready(function () {
	        $('input.date').simpleDatepicker({ startdate: '6/10/1900' });
	        $("input.date").parent().css({ 'background': "none" });
	    });
	    function reloadDatepicker() {
	        $('input.date').simpleDatepicker({ startdate: '6/10/1900' });
	    }
	</script> 
    <script type="text/javascript">
        function reloadDatepicker() {
            $('input.date').click();
        }
        jQuery(document).ready(function () {
            $('input.date').simpleDatepicker({ startdate: '6/10/1900' });
            $('input.date').keyup(function (e) {
                console.log('keyup called');
                var code = e.keyCode || e.which;
                if (code == '9') {
                    reloadDatepicker();
                }

            });
        });
	</script> 
	<!--### DATEPICKER ENDS###-->

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

  <h3>Report Management</h3>
  <br />
    <%--<input name="CompanyName" type="text" id="CompanyName" size="38" style="float:left; height:30px;" runat="server" />--%>
    

    <table border="0" cellspacing="0" cellpadding="5" id="tblForm" runat="server">
  <tr style="display:none;">
	<td style="width: 378px">
        <label for="VendorType"><b>Vendor</b></label>
	  <div class="clearfix"></div>
        <asp:RadioButtonList ID="VendorType" runat="server" RepeatDirection="Horizontal" style="float:left" CellPadding="3" CellSpacing="3">
        <asp:ListItem Selected="True"> Approved LOI</asp:ListItem>
        <asp:ListItem> Applicant</asp:ListItem>
    </asp:RadioButtonList>
	  <div class="clearfix"></div>
	</td>
	</tr>
  <%--<tr>
	<td style="width: 378px"><label for="CompanyName"><b>Company name</b></label>
	  <div class="clearfix"></div>
<input name="CompanyName" type="text" id="Text1" size="60" maxlength="100" runat="server" 
			causesvalidation="False" /><div class="clearfix"></div>
<span style="font-size:12px; font-style:italic;">Registered business name (Exact name as in SEC Registration/DTI)</span></td>
	</tr>--%>
  <tr>
	<td style="width: 378px"><label for="CompanyName"><b>LOI date submitted</b><span> (d/m/yyyy)</span></label>

	  <div class="clearfix"></div> 
        From: 
<input name="DateSubmittedFr" type="text" id="DateSubmittedFr"  class="date" runat="server" title="dd/mm/yyyy" 
			causesvalidation="False" style="background:url(images/calendar_icon1.png) no-repeat center right" readonly="readonly" value="1/1/2010" />
        &nbsp;-&nbsp;
        To: 
<input name="DateSubmittedTo" type="text" id="DateSubmittedTo"  class="date" runat="server" title="dd/mm/yyyy" 
			causesvalidation="False" style="background:url(images/calendar_icon1.png) no-repeat center right" readonly="readonly"  />
        <%--<img src="images/calendar_icon1.png" style="height: 22px; width: 21px; margin-bottom:-7px;" /><div class="clearfix">--%></div>
<%--span style="font-size:12px; font-style:italic;">Registered business name (Exact name as in SEC Registration/DTI)</span>--%>
	</td>
	</tr>
  <%--<tr>
	<td style="width: 378px"><label for="CompanyName"><b>Financial statement</b></label>
	  <div class="clearfix"></div>
		<asp:RadioButtonList ID="FinancialStatement" runat="server">
			<asp:ListItem Value="Yes"> If latest Financial statement covers 12 months</asp:ListItem>
			<asp:ListItem Value="No"> If latest Financial statement covers less than 12 months, or no FS available.</asp:ListItem>
		</asp:RadioButtonList>
		<div class="clearfix"></div>
	</td>
	</tr>--%>
  <%--<tr>
	<td colspan="4"><label for="line_of_business">Line of Business</label>
	  <div class="clearfix"></div>
<input name="line_of_business" type="text" id="line_of_business" size="60" /></td>
	</tr>--%>
  <%--<tr>
	<td style="width: 378px"><label for="EmailAdd"><b>Email</b></label>
	  <div class="clearfix"></div>
<input name="EmailAdd" type="text" id="EmailAdd" size="60" runat="server" maxlength="100" />

</td>
</tr>--%>
	
  <tr>
	<td style="width: 378px; padding:0px;">
		<%--<label for="CategoryId">Core Business</label>--%>



	  <div class="clearfix"></div>
	  <%--<asp:DropDownList ID="CategoryId" runat="server" 
			DataSourceID="dsProductCategoryId" DataTextField="CategoryName" 
			DataValueField="CategoryId">
	</asp:DropDownList>
	<asp:SqlDataSource ID="dsProductCategoryId" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>" SelectCommand="SELECT '0' CategoryId, '--SELECT A CATEGORY--' CategoryName FROM rfcProductCategory UNION SELECT CategoryId, CategoryName FROM rfcProductCategory">
</asp:SqlDataSource>--%>
		<%--<table border="0" width="100%" cellspacing="0" cellpadding="5" class="atable">
  <tr>
	<td style="width: 213px">
		<label for="nature_of_business"><b>Core Business</b></label></td>
	<td valign="bottom" style="width: 32px">
		<input id="CategoryCounter" class="rowCount" name="CategoryCounter" type="hidden" />
		<a href="javascript:void(0)" value="Add Row" class="alternativeRow" runat="server" id="add1">+Add</a></td>
	</tr>
	
		<asp:Repeater ID="repeaterSelectedCategory" runat="server" DataSourceID="dsSelectedCategory" >
		<ItemTemplate>
  <tr>
	<td style="border-bottom:1px dotted #ccc; height:30px; width: 213px;">
		<select id="CategoryId" name="CategoryId" style="z-index:1000">
		<asp:Repeater ID="repeaterCategory" runat="server" DataSourceID="dsrfcCategory" >
		<ItemTemplate>
		<option value="<%# DataBinder.Eval(Container.DataItem, "CategoryId") %>" <%# (DataBinder.Eval(Container.DataItem, "CategoryId")).ToString() == (DataBinder.Eval(((RepeaterItem)Container.Parent.Parent).DataItem, "CategoryId")).ToString() ? "selected='selected'" : ""%> ><%#DataBinder.Eval(Container.DataItem, "CategoryName")%></option>
		</ItemTemplate>
		</asp:Repeater>
		</select>
	</td>
	<td style="border-bottom:1px dotted #ccc; height:30px;">
		<img src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
	</tr>
		</ItemTemplate>
		</asp:Repeater>
	</table>--%>
	<%--<asp:SqlDataSource ID="dsrfcCategory" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
			SelectCommand="SELECT '0' as CategoryId, 'Select Category' as CategoryName UNION SELECT CategoryId, CategoryName FROM rfcProductCategory" >
	</asp:SqlDataSource>
	<asp:SqlDataSource ID="dsSelectedCategory" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"  ></asp:SqlDataSource>--%>
		
<script type="text/javascript">
    $("document").ready(function () {
        $(".alternativeRow").btnAddRow({ inputBoxAutoNumber: true, inputBoxAutoId: true, displayRowCountTo: "rowCount" });
        $(".delRow").btnDelRow();
    });
</script>
	
</td>
	</tr>
  <%--<tr>
	<td style="width: 378px"><label for="attachment"><b>Attach Letter of Intent</b></label>
	  <div class="clearfix"></div>
		<div style="float:left; width:30px;"><input id="fileUpload1" type="file"/></div> 
		<asp:Label ID="fileuploaded1" CssClass="fileuploaded1" runat="server" Text="Attach file" style="float:left; padding-top:3px; display:block"></asp:Label>   <img src="images/xicon.png" style="margin-left:10px; padding-top:5px;display:none;" id="LOIFileNamex" onclick="$('#<%= LOIFileName.ClientID %>').val('');$('#<%= fileuploaded1.ClientID %>').html('Attach file');$(this).hide();" />
		<input id="LOIFileName" name="LOIFileName" type="hidden" runat="server" value='<%# Eval("Filename")%>' />
	  </td>
	</tr>--%>
  <tr>
	<td style="width: 378px">
	<%--<label for="captcha">Captcha</label>
	  <div class="clearfix"></div>
	<div style="font-size:10px;">Just to prove you are a human, please answer the
following math challenge. </div>
	  <div class="clearfix"></div>
		<uc:ASPNET_Captcha ID="ucCaptcha" runat="server" Align = "Middle" Color = "#FF0000" style="float:left;" />--%>
		<%--<uc:ASPNET_Captcha ID="ASPNET_Captcha1" runat="server" Align = "Middle" Color = "#FF0000" />--%>
		<%--<asp:TextBox ID="txtCaptcha" runat="server" size="10"></asp:TextBox>--%>
	  <div class="clearfix"></div>
		<asp:Label ID="lblmsg" runat="server" Font-Bold="True" 
	ForeColor="Red" Text=""></asp:Label>
		 <br />
	</div>
	<%--<asp:Image ID="Image1" runat="server" ImageUrl="~/CImage.aspx"/>
	<br />
	<div style="width:300px">Type the characters you see in this picture. This ensures that a person, not an automated program, is submitting this form.</div> 
	<br />
	<asp:TextBox ID="txtimgcode" runat="server"></asp:TextBox>--%>
	<br />
	<%--<asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />--%>
	<%--<div class="clearfix"></div>
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1" c
		onclientclick="javascript: __doPostBack('createBt', ''); return false;"><span>CREATE</span></asp:LinkButton>
<a href="login.aspx" class="bt1"><asp:Label ID="cancelBtLbl" runat="server" Text="CANCEL"></asp:Label></a>--%>
	  </td>
	</tr>
  </table>
  <%--<a href="#" class="bt1" style="margin-right:110px; float:left; margin-left:5px;" runat="server" onclientclick=" __doPostBack('justSave'); return false;"><span>Search</span></a>--%>

    <asp:LinkButton ID="searchBt1" runat="server" CssClass="bt1"  onclientclick=" __doPostBack('justSave'); return false;" style="margin-right:110px; float:left; margin-left:5px;" ><span>Generate Report</span></asp:LinkButton>
  <div class="clearfix"></div>
</div>

    <div class="clearfix"></div>
<%--<%@ Register TagPrefix="Ava" TagName="list_search" Src="usercontrols/list_search.ascx" %>
<ava:list_search ID="list_search1" runat="server" />--%>



</form>
<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

</asp:Content>
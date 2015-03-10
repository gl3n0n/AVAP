<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vmhead_VendorList.aspx.cs" Inherits="vmhead_VendorList" %>
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
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_vmreco.ascx" %>
<ava:topnav ID="TopNav1" runat="server" />

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Vendors for Approval</h3>
<form action="" method="post" runat="server">
<asp:SqlDataSource ID="dsVendorAuthenticated" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT *, dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb , DateAuthenticatedByDnb) as datediff1,  dbo.CalculateNumberOFWorkDays(DateAuthenticatedByDnb, approvedbyDnbDate) as datediff2,  dbo.CalculateNumberOFWorkDays(approvedbyDnbDate, approvedbyVMOfficerDate) as datediff3,  dbo.CalculateNumberOFWorkDays(approvedbyVMOfficerDate, approvedbyVMRecoDate) as datediff4,  dbo.CalculateNumberOFWorkDays(approvedbyVMRecoDate, approvedbyFAALogisticsDate) as datediff5,  dbo.CalculateNumberOFWorkDays(approvedbyFAALogisticsDate, approvedbyFAAFinanceDate) as datediff6,  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyFAALogisticsDate) as datetotal1,  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyFAAFinanceDate) as datetotal2 FROM tblVendor WHERE Status = 3  ORDER BY approvedbyVMOfficerDate DESC" >
	    </asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" DataSourceID="dsVendorAuthenticated" 
    AllowPaging="True" AllowSorting="True" BorderColor="Silver" OnRowCommand="gvVendors_RowCommand"
    BorderStyle="Dotted" BorderWidth="1px" CellPadding="5" ClientIDMode="AutoID" EmptyDataText="No disapproved vendors to display."
    Width="100%" AutoGenerateColumns="False"  Font-Size="Smaller"  PageSize="15" ShowFooter="True">
    <AlternatingRowStyle BackColor="#EEEEEE" />
    <Columns> <asp:TemplateField HeaderText="Company Name" InsertVisible="False" SortExpression="CompanyName" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<%--<asp:Label ID="lnkRefNo" runat="server" Text='<%# Bind("CompanyName") %>' ></asp:Label>--%><%--&nbsp;<asp:Label ID="Label0" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>--%><a href="<%# "vendor_Home.aspx?VendorId=" + Eval("VendorId") %>" target="_blank"><%# Eval("CompanyName") %></a> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Submitted to D&B" InsertVisible="False" SortExpression="DateSubmittedToDnb" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("DateSubmittedToDnb")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Authenticated" InsertVisible="False" SortExpression="DateAuthenticatedByDnb" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Eval("DateAuthenticatedByDnb").ToString()!="" ? "["+Eval("datediff1").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("DateAuthenticatedByDnb")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Submitted to VM Officer" InsertVisible="False" SortExpression="approvedbyDnbDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label12" runat="server" Text='<%# Eval("approvedbyDnbDate").ToString()!="" ? "["+Eval("datediff2").ToString() + "]" +  String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyDnbDate")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Endorsed to VM Head" InsertVisible="False" SortExpression="approvedbyVMOfficerDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label13" runat="server" Text='<%# Eval("approvedbyVMOfficerDate").ToString()!="" ? "["+Eval("datediff3").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyVMOfficerDate")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="Email Address" InsertVisible="False" SortExpression="EmailAdd">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label0" runat="server" Text='<%# Bind("EmailAdd") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Authentication Ticket" InsertVisible="False" SortExpression="AuthenticationTicket">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("AuthenticationTicket") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <%--<asp:TemplateField HeaderText="Date Authenticated" InsertVisible="False" SortExpression="DateAuthenticatedByDnb" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("DateAuthenticatedByDnb") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date Received" InsertVisible="False" SortExpression="approvedbyVMOfficerDate" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("approvedbyVMOfficerDate") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="Action" InsertVisible="False" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:LinkButton ID="lnkRefNo1" runat="server" Text='View' CommandArgument='<%# Bind("VendorId") %>' CommandName="Details"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</form>
<br />
<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

</asp:Content>
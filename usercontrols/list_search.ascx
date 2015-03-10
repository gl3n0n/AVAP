<%@ Control Language="C#" AutoEventWireup="true" CodeFile="list_search.ascx.cs" Inherits="usercontrols_list_search" %>
<div class="separator1"></div>
<h3 style="margin:10px 0px;" runat="server" id="titleh3">Search Result</h3>
<%--<form id="Form1" action="" method="post" runat="server">--%>
        <asp:SqlDataSource ID="dsVendorSearch" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand= "" >
	    </asp:SqlDataSource>

<asp:GridView ID="GridView1" runat="server" DataSourceID="dsVendorSearch" 
    AllowPaging="True" AllowSorting="True" BorderColor="Silver" OnRowCommand="gvBids_RowCommand" RowStyle-Font-Size="Smaller"
    BorderStyle="Dotted" BorderWidth="1px" CellPadding="5" Font-Size="Smaller" ClientIDMode="AutoID" EmptyDataText="Sorry, no vendors matched your search. Please try again." 
    Width="100%" AutoGenerateColumns="False" PageSize="15">
    <AlternatingRowStyle BackColor="#EEEEEE" />
    <Columns>
        <asp:TemplateField HeaderText="Company Name" InsertVisible="False" SortExpression="CompanyName" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                <%--<asp:Label ID="lnkRefNo" runat="server" Text='<%# Bind("CompanyName") %>' ></asp:Label>--%><%--&nbsp;<asp:Label ID="Label0" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>--%><a href="<%# "vendor_Home.aspx?VendorId=" + Eval("VendorId") %>" target="_blank"><%# Eval("CompanyName") %></a> 
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
        <asp:TemplateField HeaderText="Endorsed to PVMD" InsertVisible="False" SortExpression="approvedbyVMRecoDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label131" runat="server" Text='<%# Eval("approvedbyVMRecoDate").ToString()!="" ? "["+Eval("datediff4").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyVMRecoDate")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Endorsed to CFO" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label14" runat="server" Text='<%# Eval("approvedbyFAALogisticsDate").ToString()!=""? "["+Eval("datediff5").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyFAALogisticsDate")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="20px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label16" runat="server" Text='<%# Eval("datetotal2").ToString() %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action" InsertVisible="False" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="20px" />
            <ItemTemplate>
                &nbsp;<asp:LinkButton ID="lnkRefNo1" runat="server" Text='View' CommandArgument='<%# Bind("VendorId") %>' CommandName="Details"></asp:LinkButton><%-- | <%# Eval("Status").ToString()!="8" ? Eval("Status").ToString()!="6" ? "":"Approved" : "Disapproved" %>--%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>




<asp:GridView ID="GridView2" runat="server" DataSourceID="dsVendorSearch" 
    AllowPaging="True" AllowSorting="True" BorderColor="Silver" OnRowDataBound="GridView2_RowDataBound" OnRowCommand="gvBids_RowCommand2" RowStyle-Font-Size="Smaller"
    BorderStyle="Dotted" BorderWidth="1px" CellPadding="5" Font-Size="Smaller" ClientIDMode="AutoID" EmptyDataText="Sorry, no vendors matched your search. Please try again." 
    Width="100%" AutoGenerateColumns="False" PageSize="15">
    <AlternatingRowStyle BackColor="#EEEEEE" />
    <Columns>
        <asp:TemplateField HeaderText="Company Name" InsertVisible="False" SortExpression="CompanyName" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                <%--<asp:Label ID="lnkRefNo" runat="server" Text='<%# Bind("CompanyName") %>' ></asp:Label>--%>
                &nbsp;<asp:Label ID="Label0" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
                <%--<a href="<%# "vendor_Home.aspx?VendorId=" + Eval("ID") %>" target="_blank"><%# Eval("CompanyName") %></a> --%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Email Address" InsertVisible="False" SortExpression="EmailAdd" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("EmailAdd") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Business Core" InsertVisible="False" SortExpression="CategoryName" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="140px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="BusinessCoreLbl" runat="server" Text='<%# (Eval("CategoryName")).ToString()!="" ? (Eval("CategoryName")).ToString().Trim().Replace("; ",",<br>") : "--" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Application Date" InsertVisible="False" SortExpression="DateCreated" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("DateCreated", "{0:g}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status" InsertVisible="False" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="50px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label111" runat="server" Text='<%# Bind("Status1") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action" InsertVisible="False" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="50px" />
            <ItemTemplate>
                <asp:LinkButton ID="Label1111" runat="server" Text='View' CommandArgument='<%# Eval("EmailAdd") + "|" + Eval("CompanyName") + "|" + Eval("ID") + "|" + Eval("Status") %>' CommandName="Details2"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<%--</form>--%>
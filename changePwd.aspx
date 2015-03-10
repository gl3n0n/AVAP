<%@ Page MasterPageFile="~/MasterPage.master" Language="C#" AutoEventWireup="true" CodeFile="changePwd.aspx.cs" Inherits="changePwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Globe Automated Vendor Accreditation :: Change Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
    <div class="content_logo">
        <img src="images/logo_hck.png" width="229" height="105" border="0" />
    </div>
    <div style="width:700px; padding:5px 20px; background:#29c5ee; margin: 20px 0 0 20px; color:#FFF" class="rounded-corners">
      <h1>Change Password </h1>
    </div>
    <form runat="server" id="form1" name="form1">
        <div style="float:left; width:600px; min-height:200px; margin:25px 0 0 50px; background-color:White; padding: 30px 30px 30px 30px">
        <div class="clearfix"></div>
            <asp:Label runat="server" ID="txtNote" ForeColor="#FF0000" Style="font-weight: bold"></asp:Label>
        <div class="clearfix"></div>
            <asp:TextBox ID="txtOldPwd" runat="server" CausesValidation="True" value="ENTER OLD PASSWORD" onclick="if($(this).val()=='ENTER OLD PASSWORD'){ $(this).select(); } " onblur="if($(this).val()==''){ $(this).val('ENTER OLD PASSWORD'); }" ></asp:TextBox>
        <div class="clearfix"></div>

            
            <asp:TextBox ID="txtNewPwd" runat="server" CausesValidation="True" value="ENTER NEW PASSWORD" onclick="if($(this).val()=='ENTER NEW PASSWORD'){ $(this).select(); } " onblur="if($(this).val()==''){ $(this).val('ENTER NEW PASSWORD'); }" ></asp:TextBox>
        <div class="clearfix"></div>

            
            <asp:TextBox ID="txtNewPwd2" runat="server" CausesValidation="True" value="RE-ENTER NEW PASSWORD" onclick="if($(this).val()=='RE-ENTER NEW PASSWORD'){ $(this).select(); } " onblur="if($(this).val()==''){ $(this).val('RE-ENTER NEW PASSWORD'); }" ></asp:TextBox>
        <div class="clearfix"></div>
	<br />
        <div style="float:none">
             <input id="Image1" name="" type="image" src="images/002_11b.png" runat="server" onserverclick="btnChangePwd_Click"/>
        </div>
            *You will be logged out after changing password.
<%--        <div style="float:none; margin:5px 20px 0 0; font-size:14px;"> <a href="login.aspx" style="font-size:14px">Back to Login Page</a></div>--%>
        </div>
    </form>
    </div>
</asp:Content>
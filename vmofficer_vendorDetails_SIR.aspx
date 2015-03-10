<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vmofficer_vendorDetails_SIR.aspx.cs" Inherits="vmofficer_vendorDetails_SIR" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Globe Automated Vendor Accreditation :: Vendor Information</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Add content controls here --%>
<!--### TOOLTIP STARTS ###-->
    <script type="text/javascript" src="Scripts/jquery.tooltip.min.js"></script>
    <link href="Styles/jquery.tooltip.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript">
	    $(function () {
	        $('#stepnav li').tooltip({
	            track: true,
	            delay: 0,
	            showURL: false,
	            showBody: " - ",
	            fade: 250
	        });
	    });
    </script>
	<!--### TOOLTIP ENDS ###-->

    <!--### DATEPICKER ###-->
    <script type="text/javascript" src="Scripts/cal.js"></script>
    <link rel="stylesheet" type="text/css" href="Styles/calendar_picker.css" />
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $('input.date').simpleDatepicker({ startdate: '6/10/1900' });
        });
        function reloadDatepicker() {
            $('input.date').simpleDatepicker({ startdate: '6/10/1900' });
        }
    </script> 
    <!--### DATEPICKER ENDS###-->

    <!--### FORM STYLING ###-->
	<link rel="stylesheet" href="plugins/jqtransformplugin/jqtransform.css" type="text/css" media="all" />
	<link rel="stylesheet" href="plugins/jqtransformplugin/demo.css" type="text/css" media="all" />
	<script type="text/javascript" src="plugins/jqtransformplugin/jquery.jqtransform.js" ></script>
	<script language="javascript">
	    $(function () {
	        $('form').jqTransform({ imgPath: 'plugins/jqtransformplugin/img/' });
	    });
	</script>
    <!--### FORM STYLING ENDS ###-->

    <!--### UPLOADIFY ###-->
    <%--<script src="uploadify/jquery-1.4.2.min.js" type="text/javascript"></script>--%>
    <script src="uploadify/swfobject.js" type="text/javascript"></script>
    <script src="uploadify/jquery.uploadify.v2.1.4.js" type="text/javascript"></script>
    <script src="uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
    <link href="uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <!--### UPLOADIFY ENDS ###-->

    <script src="Scripts/jquery.numeric.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ComputeOverall()
        {
            dnbScore = parseFloat($("#<%= dnbScore.ClientID %>").html());
            vmoGTPerf_Eval = $("#<%= vmoGTPerf_Eval.ClientID %>").val() != "" ? parseFloat($("#<%= vmoGTPerf_Eval.ClientID %>").val()) : 0;
            dnbFinCapScore = parseFloat($("#<%= dnbFinCapScore.ClientID %>").html());
            dnbLegalConfScore = parseFloat($("#<%= dnbLegalConfScore.ClientID %>").html());
            dnbTechCompScore = parseFloat($("#<%= dnbTechCompScore.ClientID %>").html());

            if ($("#<%= vmoNew_Vendor.ClientID %> input[type='radio']:checked").val() == "1") //renewal is 0
            {
                vmoOverallScore = dnbScore;
            }
            else
            {
                if (vmoGTPerf_Eval > 0)
                {
                    dnbLegalConfScore_new = (dnbTechCompScore + (vmoGTPerf_Eval * .4)) / 2;
                    vmoOverallScore = (dnbFinCapScore + dnbLegalConfScore + dnbLegalConfScore_new);
                }
                else
                {
                    dnbScore = parseFloat($("#<%= dnbScore.ClientID %>").html());
                    vmoOverallScore = dnbScore;
                }
                $("#<%= vmoOverallScore1.ClientID %>").val(vmoOverallScore);
                $("#<%= vmoOverallScore.ClientID %>").html(vmoOverallScore);
            }
        }
        $(document).ready(function () {
            ComputeOverall();
            $("#<%= vmoGTPerf_Eval.ClientID %>").blur(function () { ComputeOverall(); });
            $("#<%= vmoNew_Vendor.ClientID %>").change(function () { ComputeOverall(); });
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
        });
        function reloadNumeric() {
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
        }
    </script>
    
<link rel="stylesheet" type="text/css" href="Styles/jquery-ui.css" />
<script type="text/javascript" src="Scripts/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#dialog-form").dialog({
                autoOpen: false,
                height: 450,
                width: 350,
                modal: true,
                buttons: {
                    "Send": function () {
                        //alert($("#dialog-form").data('VendorName'));
                        if (confirm("Are you sure to send clarification of the SIR to D&B? Any changes will not be saved.")) {
                            __doPostBack("Details", $("#comments").val());
                            //$(this).dialog("close");
                        }
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {
                    allFields.val("").removeClass("ui-state-error");
                }
            });
            //$(".bt10")
            //.button()
            //.click(function () {
            //    $("#dialog-form").dialog("open");
            //});
        });
</script>


 <style>
input.text { margin-bottom:12px; width:95%; padding: .4em; }
fieldset { padding:0; border:0; margin-top:25px; }
h1 { font-size: 1.2em; margin: .6em 0; }
div#users-contain { width: 350px; margin: 20px 0; }
div#users-contain table { margin: 1em 0; border-collapse: collapse; width: 100%; }
div#users-contain table td, div#users-contain table th { border: 1px solid #eee; padding: .6em 10px; text-align: left; }
.ui-dialog .ui-state-error { padding: .3em; }
.validateTips { border: 1px solid transparent; padding: 0.3em; }
</style>

<div id="dialog-form" title="Clarify to D&B" style="height:150px;" >
<p class="VendorNameHere" style="font-weight:bold"></p>
<fieldset>
<label for="name">Comment</label>
    <div class="clearfix"></div>
    <br />
<textarea id="comments" style="width:95%" rows="5"></textarea>
<%--<input type="text" name="vendorcode" id="vendorcode" class="text ui-widget-content ui-corner-all"  />
<label for="email">Account Group</label>
<input type="text" name="accgroup" id="accgroup" value="" class="text ui-widget-content ui-corner-all" />
<label for="email">Purchasing Org</label>
<input type="text" name="purchasingorg" id="purchasingorg" value="" class="text ui-widget-content ui-corner-all" />
<label for="email">Country Code for: <span id="CountryCode_Lbl"></span></label>
<input type="text" name="CountryCode" id="CountryCode" value="" class="text ui-widget-content ui-corner-all" />
<label for="email">Currency</label>
<input type="text" name="Currency" id="Currency" value="" class="text ui-widget-content ui-corner-all" />
<label for="vat">VAT</label>
<input type="text" name="vat" id="vat" value="" class="text ui-widget-content ui-corner-all"/>--%>
</fieldset>
</div>

	<script type="text/javascript" src="Scripts/jquery.table.addrow.js" ></script>
<link href="Styles/ava_pages.css" rel="stylesheet" type="text/css" />
<div class="content">
<div class="content_logo">
<a href=""><img src="images/logo_hck.png" width="229" height="105" border="0" /></a> 
<div style="float:right"></div>
</div>
<div class="rounded-corners-top" id="menuAVA">
<ava:tabsnav ID="Tabsnav1" runat="server" />
</div>
<div style="background:#FFF; min-height:445px; padding:10px;" class="rounded-corners-bottom2 menu">
<!--##################-->
<!--BODY CONTENT STARTS-->
    <form id="Form1" runat="server">
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_vmofficer.ascx" %>
<ava:topnav ID="TopNav1" runat="server" />

<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;"><%--Vendor Name: 
    <br />
  Authentication Ticket: <asp:Label ID="AuthenticationTicketLbl" runat="server" Text=""></asp:Label>--%>
    D&B Report</div>
<div class="stepsdiv" style="margin:10px 0px; font-size:12px; width:450px; float:right; text-align:right; font-weight:bold;">
    <asp:Label ID="errNotification" runat="server" Text=""></asp:Label>
</div>

<!--Business activities STARTS-->
<div class="separator1" style="clear:both"></div>
<table width="100%" border="0" cellspacing="0" cellpadding="8" class="GridTbl">
  <tr>
    <td width="26%">Name of vendor</td>
    <td width="74%"><h3><asp:Label ID="CompanyNameLbl" runat="server" Text=""></asp:Label></h3></td>
  </tr>
  <tr>
    <td>D&amp;B D-U-N-S</td>
    <td><h3><asp:Label ID="dnbDuns" runat="server" Text=""></asp:Label></h3></td>
    </tr>
  <tr>
    <td>Legal structure / Company type</td>
    <td><h3><asp:Label ID="legalStrucOrgType" runat="server" Text=""></asp:Label></h3></td>
    </tr>
  <tr>
    <td style="height: 39px">Nature of business</td>
    <td style="height: 39px"><h3><asp:Label ID="NatureOfBusiness" runat="server" Text=""></asp:Label></h3></td>
    </tr>
  <tr>
    <td>Products/Services offered</td>
    <td><h3><asp:Label ID="Category" runat="server" Text=""></asp:Label></h3></td>
    </tr>
</table>
<br />

<div class="separator1"></div>
<h3 style="margin:10px 0px;">SUPPLIER INFORMATION REPORT & OTHER DOCUMENTS</h3>

        <%--<table style="width: 100%;">
            <tr>
                <td style="width: 30px">
                    <asp:CheckBox ID="Vendor_new" runat="server" />
                </td>
                <td style="width: 159px">New Vendor</td>
                <td style="width: 30px">
                    <asp:CheckBox ID="Vendor_renewal" runat="server" />
                </td>
                <td>Renewal Vendor</td>
            </tr>
        </table>--%>

        <asp:RadioButtonList ID="vmoNew_Vendor" runat="server" RepeatDirection="Horizontal" >
            <asp:ListItem Value="1" Selected="True">New Vendor</asp:ListItem>
            <asp:ListItem Value="0">Renewal Vendor</asp:ListItem>
        </asp:RadioButtonList>

<br /><br />

<div class="separator1 clearfix"></div>
<h3 style="margin:10px 0px;">VENDOR EVALUATION REPORT</h3>
        <table style="width: 100%;" border="0" cellspacing="0" cellpadding="8">
            <tr>
                <td style="width: 108px; height: 37px;"><h4><label>D&B Score</label></h4></td>
                <td style="width: 149px; height: 37px;"><h3><asp:Label runat="server" ID="dnbScore" Text="0"></asp:Label></h3></td>
                <td style="width: 115px; height: 37px;"><h4><label>Overall Score</label></h4></td>
                <td style="height: 37px">
                    <h3><asp:Label runat="server" ID="vmoOverallScore" Text="0"></asp:Label></h3>
                    <asp:HiddenField ID="vmoOverallScore1" runat="server" />
                </td> 
            </tr>
        </table>
<br /><br />
<table width="100%" border="0" cellspacing="0" cellpadding="8">
  <tr>
    <td class="style1" style="width: 68px; height: 31px;"><label for="dnbCompRating">Financial Capability Score</label></td>
    <td style="height: 31px; width: 28px;"><h3><asp:Label ID="dnbFinCapScore" runat="server" Text="0"></asp:Label></h3></td>
    <td style="height: 31px"><label for="">Remarks </label><div class="clearfix"></div>
        <asp:Label ID="dnbFinCapScore_Remarks" runat="server" Text="No Comment."></asp:Label></td>
  </tr>
  <tr>
    <td class="style1" style="width: 68px; height: 31px;"><label for="">Legal Conformance Score</label></td>
    <td style="height: 31px; width: 28px;"><h3><asp:Label ID="dnbLegalConfScore" runat="server" Text="0"></asp:Label></h3></td>
    <td style="height: 31px"><label for="">Remarks </label><div class="clearfix"></div>
        <asp:Label ID="dnbLegalConfScore_Remarks" runat="server" Text="No Comment."></asp:Label></td>
  </tr>
  <tr>
    <td class="style1" style="width: 68px">&nbsp;</td>
    <td style="width: 28px">&nbsp;</td>
    <td>
        <table style="width:89%;">
            <tr>
                <td style="width: 306px"><strong>Will the issue pose significant adverse risk to Globe?</strong></td>
                <td><asp:CheckBox ID="vmoIssue_risk_to_Globe" runat="server" /></td>
            </tr>
            <tr>
                <td style="width: 306px; height: 39px;" valign="top"><strong>With Conflict of Interest?</strong></td>
                <td style="height: 39px" valign="top"><asp:CheckBox ID="vmoConflict_of_Interest" runat="server" /></td>
            </tr>
            <tr>
                <td style="width: 306px" valign="top"><strong>Registered Court Case:</strong></td>
                <td valign="top">
                    <strong><asp:Label ID="TypeOfCase" runat="server" Text="No Case"></asp:Label></strong>
                    <br />
                    <span><asp:Label runat="server" ID="DateFiled" ></asp:Label></span>
                    <div class="clearfix"></div>
                    <div style="float:left; width:30px;"><img alt="" src="images/attachment.png" /></div> 
        <asp:Label ID="fileuploaded_1" CssClass="fileuploaded1" runat="server" Text="No Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
                </td>
            </tr>
            </table>
    </td>
  </tr>
  <tr>
    <td class="style1" style="width: 68px"><label for="">Technical Qualification Score</label></td>
    <td style="width: 28px"><h3><asp:Label ID="dnbTechCompScore" runat="server" Text="0"></asp:Label></h3></td>
    <td><label for="">Remarks </label><div class="clearfix"></div>
        <asp:Label ID="dnbTechCompScore_Remarks" runat="server" Text="No Comment."></asp:Label></td>
  </tr>
  <tr>
    <td class="style1" style="width: 68px"><h3>&nbsp;</h3>
      </td>
    <td style="width: 28px"></td>
    <td>
        <table style="width:75%;"  valign="top" >
            <tr>
                <td style="width: 307px"><strong>With type-approved products</strong></td>
                <td><asp:CheckBox ID="vmoWith_Type_Approved_Products" runat="server" /></td>
            </tr>
            <tr>
                <td style="width: 307px"><strong>With approved Proof of Concept</strong></td>
                <td><asp:CheckBox ID="vmoWith_Approved_Proof_of_Concept" runat="server"  /></td>
            </tr>
            <tr>
                <td colspan="2" style="width: 244px"> </td>
            </tr>
            </table>
    </td>
  </tr>
  <tr>
    <td class="style1" style="width: 68px" valign="top"><label for="">SIR Attachment</label></td>
    <td class="style1" style="width: 28px" valign="top">
        &nbsp;</td>
    <td><asp:Label runat="server" ID="dnbSupplierInfoReport"></asp:Label></td>

  </tr>
  <tr>
    <td class="style1" style="width: 68px" valign="top"><label for="">Other Documents</label></td>
    <td class="style1" style="width: 28px" valign="top">
        &nbsp;</td>
    <td><asp:Label runat="server" ID="dnbOtherDocumentsLbl"></asp:Label></td>

  </tr>
  </table>
<br />
        <table style="width: 90%;" cellpadding="5">
            <tr>
                <td style="width: 250px">If Renewal, GT Performance Evaluation</td>
                <td style="width: 30px">
                    <asp:TextBox ID="vmoGTPerf_Eval" runat="server" CssClass="integer" Text="0" MaxLength="2"></asp:TextBox>
                </td>
                <td style="text-align:right;">MAXIMUM EXPOSURE LIMIT</td>
                <td style="width: 30px" align="left">
                    <asp:TextBox ID="dnbMaxExposureLimit" runat="server" Width="205px"></asp:TextBox>
                </td>
            </tr>
        </table>
        
<br />


<!--Business activities STARTS-->
<div class="separator1"></div>
<table style="width: 100%;">
          <tr>
              <td style="height: 19px; width: 438px">
                <h3 style="margin:10px 0px;">SPEND PROFILE</h3>
                <br />
                  <table border="0" cellspacing="0" cellpadding="8">
                      <tr>
                        <td><label for="">No. of POs:</label>
                          </td>
                        <td style="width: 156px">&nbsp;<asp:TextBox ID="vmoNo_POs" runat="server" CssClass="numeric"></asp:TextBox>
                          </td>
                        </tr>
      
                        <td><label for="">Spend:</label>
                          </td>
                        <td style="width: 156px">&nbsp;<asp:TextBox ID="vmoSpend" runat="server" CssClass="numeric"></asp:TextBox>
                          </td>
                        </tr>
                      <tr>
                        <td><label for="">With existing Frame Agreement?</label>
                          </td>
                        <td style="width: 156px">&nbsp;<asp:DropDownList ID="vmoWith_Existing_Frame_Arg" runat="server">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:DropDownList>
                          </td>
                        </tr>
                    </table>
              </td>
              <td style="height: 19px">
                  <h3 style="margin:10px 0px;">ISSUES ENCOUNTERED</h3>
                <br />
                  <table border="0" cellspacing="0" cellpadding="8">
                      <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td><asp:CheckBox ID="vmoIssues_bond_claims" runat="server" /></td>
                                    <td> <label>Bond / LD Claims</label></td>
                                </tr>
                            </table>
</td>
                          
                        <td style="width: 156px">
                            <table style="width: 100%;">
                                <tr>
                                    <td><asp:CheckBox ID="vmoIssues_ISR_involvement" runat="server" /></td>
                                    <td> <label>ISR Involvement</label></td>
                                </tr>
                            </table>
                          </td>
                        </tr>
      
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td><asp:CheckBox ID="vmoIssues_Others" runat="server" /></td>
                                    <td> <label>Others</label></td>
                                </tr>
                            </table>
                          </td>
                        <td style="width: 156px">
                        <table style="width: 100%;">
                                <tr>
                                    <td><asp:CheckBox ID="vmoIssues_Loss_Incidents" runat="server" /></td>
                                    <td> <label>Bond / LD Claims</label></td>
                                </tr>
                            </table>

                        </td>
                        </tr>
                      <tr>
                        <td colspan="2"><label for="">Remarks:</label>
                            <textarea id="vmoIssues_Remarks" cols="40" rows="3" runat="server"  style="font-family:Arial; width:290px;"></textarea>
                          &nbsp;
                            
                            </td>
                        </tr>
                    </table>
              </td>
          </tr>
          </table>

<br />






<div id="divClarify" runat="server" visible="true">
<div class="separator1"></div><div class="clearfix"></div> 
<h3 style="margin:10px 0px;">SIR Clarifications</h3>
<asp:Repeater ID="repeaterClarifyComments" runat="server" DataSourceID="dsClarifyComments">
  <ItemTemplate>
    <p><strong><%# Eval("Firstname")%> <%# Eval("Lastname")%></strong>&nbsp;&nbsp;&nbsp;<em><%# Eval("DateCreated")%></em></p>
    <p><%# Eval("Comment").ToString().Replace("\n","<br>")%><br /><br />
  </ItemTemplate>
</asp:Repeater>
  <asp:SqlDataSource ID="dsClarifyComments" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="select * from (select t1.Comment, t1.DateCreated, t2.FirstName, t2.Lastname, t3.UserType from tblCommentsClarifyToDnb t1, tblUsers t2, tblUserTypes t3 WHERE t2.UserId = t1.UserId AND t1.VendorId = @VendorId AND t3.UserId = t1.UserId) t0 ORDER BY DateCreated DESC" >
    <SelectParameters>
        <asp:SessionParameter Name="VendorId" SessionField="VendorId" Type="Int32" />
	</SelectParameters>
  </asp:SqlDataSource>
<br />
<br />
<br />
</div>
<div class="separator1"></div>

<br />
<br />
<br />
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1" onclientclick="javascript:if(confirm('Are you sure to Submit this Report?')){ __doPostBack('continueStp', ''); } return false;"><span>SUBMIT &raquo;</span></asp:LinkButton>&nbsp;
                <a href="javascript:void(0)" class="bt1" id="sendMailBt" onclick="validateForm()"><span>CLARIFY TO DNB</span></a>&nbsp;
<a href="vmofficer_VendorSIR_List.aspx" class="bt1"><span>BACK</span></a>
<br />
<br />
<br />
    </form>




<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

    </div>

    </div>

    </div>

    </div>
<script type="text/javascript">
    function validateForm() {
        //alert(id);
        //$('#dialog-form').data('id', id);
        //$('#dialog-form').data('VendorName', VendorName);
        //$('.VendorNameHere').html(VendorName);
        //$('#CountryCode_Lbl').html(Country);
        $("#dialog-form").dialog("option", "height", 300);
        $("#dialog-form").dialog("open");

    }
</script>
</asp:Content>
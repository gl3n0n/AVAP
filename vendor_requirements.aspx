<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_requirements.aspx.cs" Inherits="vendor_requirements" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_vendor.ascx" %>
<%@ Register TagPrefix="Ava" TagName="StepNav" Src="usercontrols/StepNav_vendor.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Globe Automated Vendor Accreditation :: Vendor Information
</asp:Content>
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
    
    
    <!--### UPLOADIFY ###-->
    <%--<script src="uploadify/jquery-1.4.2.min.js" type="text/javascript"></script>--%>
    <script src="uploadify/swfobject.js" type="text/javascript"></script>
    <script src="uploadify/jquery.uploadify.v2.1.4.js" type="text/javascript"></script>
    <script src="uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
    <link href="uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <!--### UPLOADIFY ENDS ###-->

    
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

    <script src="Scripts/jquery.numeric.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".numeric").css('text-align', 'right');
            $(".integer").css('text-align', 'right');
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });

            $(".integer").digits();
            $(".integer").blur(function () {
                $(".integer").digits(); $(".integer").css('text-align', 'right');
            });
            $(".numeric").digits();
            $(".numeric").blur(function () {
                $(".numeric").digits(); $(".numeric").css('text-align', 'right');
            });
        });
        function reloadNumeric() {
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
        }
        $.fn.digits = function () {
            return this.each(function () {
                $(this).val($(this).val().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
            })
        }



        function FileattchValues0(Str, StrRemove, FieldHidden1, tbl, col, vendorid) {
            //par=tblVendorInformation|regOwnedAttachmentFile|<%= VendorId %>'
            $('#ContentPlaceHolder1_' + FieldHidden1).attr('value', '');
            var myArray = Str.split(';');
            for (var i = 0; i < myArray.length; i++) {
                if (myArray[i] != StrRemove)
                    if (myArray[i] != "") {
                        $('#ContentPlaceHolder1_' + FieldHidden1).attr('value', $('#ContentPlaceHolder1_' + FieldHidden1).attr('value') + ';' + myArray[i]);
                    }
            }
            //alert($('#ContentPlaceHolder1_' + FieldHidden1).attr('value'));

            FileattchUpdateDB0(FieldHidden1, tbl, col, vendorid);
        }
        

        function FileattchUpdateDB0(FieldHidden1, tbl, col, vendorid) {
            //alert($('#ContentPlaceHolder1_' + FieldHidden1).attr('value')+"|"+tbl+"|"+col+"|"+vendorid);
            $.ajax({
                type: "POST",
                url: "uploadify_update.ashx",
                data: {
                    value: $('#ContentPlaceHolder1_' + FieldHidden1).attr('value'),
                    tbl: tbl,
                    col: col,
                    vendorid: vendorid
                }
            })
            .done(function (msg) {
                if (msg != "") { //error occured
                    alert(msg);
                }
            });
        }

    </script>
	<script type="text/javascript" src="Scripts/jquery.table.addrow.js" ></script>
<link href="Styles/ava_pages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        ol li {
            margin-top:10px;
            font-weight:bold;
            border-top: 1px dotted #ccc;
            padding-top:10px;
        }
        ul li {
            font-weight:normal;
            border-top:none;
            padding-top:3px;
        }
        .uploadselection
        {
            display:block;
            padding: 5px 10px;
            border: 1px solid #333;
            width: 80px;
            text-decoration:none;
            background:#39C;
            color:#FFF;
            text-align:center;
        }
        .uploadselection:hover
        {
            background:#fff;
            color:#000;
            text-decoration:none;
            
        }
    </style>
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
<ava:topnav ID="TopNav1" runat="server" />
<div style="margin:10px 0px; color:#333; font-size:18px; width:480px; float:left;">List Of Required Documents Globe Accreditation Process</div>


<form id="formVendorInfo" runat="server">

        <input type="hidden" name="regArea" id="regArea" runat="server" />

    <input id="VendorBranchesCounter" class="rowCount" name="VendorBranchesCounter" type="hidden" /><input id="SubsidiaryCounter" class="rowCount" name="SubsidiaryCounter" type="hidden" />

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">A.	Accomplish the following forms</h3>
    <ol>
        <li>VENDOR INFORMATION SHEET 

            
        </li>
        <li>UNDERTAKINGS </li>
        <li>SUPPLIER'S DECLARATION ON SAFETY, HEALTH, AND ENVIRONMENTAL POLICIES AND PRACTICES </li>
        <li>SUPPLIER’S DECLARATION ON BUSINESS CONTINUITY MANAGEMENT </li>
    </ol>

<br />



<div class="separator1"></div>
<h3 style="margin:10px 0px;">B. Submit the following documents (most recent dated)</h3>
    
    <ol>
        <li>FINANCIAL STATEMENTS
            <ul>
                <li>Audited FINANCIAL STATEMENTS for the last three (3) consecutive years (with ITR (Annual Income Tax Return), proof of BIR Payment and SEC stamp) and all its attachments</li>
                <li>For Sole Proprietor: Include a copy of Annual Income Tax Return (BIR Form 1701) – FS should have BIR stamp</li>
                <li>For Corporations: Include all the attachments – Annual Income Tax Return (BIR Form 1702),FS should have BIR and SEC Stamp</li>
                <li>For NEW Companies: Please provide Financial Forecast for the next 3 consecutive years (i.e. Balance Sheet, Income Statement, Owner’s equity and Cash flow) – no longer accepted and will depend on the endorsement of Globe Telecom.</li>
                <li>
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile0 = 'yr1FileNameFile';
            FieldLbl0 = 'yr1FileNameLbl';
            FieldHidden0 = 'yr1FileName';
            dbTbl0 = 'tblVendorFinancialInformation';
            dbCol0 = 'FileName';
            dbVendorId0 = '<%= VendorId %>';
            $('#' + FieldFile0 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile0).uploadify({
                'disable':true,
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile0 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile0 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl0).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"' + FieldHidden0 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden0 + '\').val(), \'' + response + '\', \'' + FieldHidden0 + '\', \'' + dbTbl0 + '\', \'' + dbCol0 + '\', \'' + dbVendorId0 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden0).attr('value', $('#ContentPlaceHolder1_' + FieldHidden0).val() + ';' + response);
                    $('#' + FieldFile0 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden0, dbTbl0, dbCol0, dbVendorId0);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="yr1FileNameFile" type="file" runat="server"/></div> 
        <asp:Label ID="yr1FileNameLbl" CssClass="yr1FileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label> 
        <input id="yr1FileName" name="yr1FileName" runat="server" type="hidden" value="" class="uploadif" />
        <div style="font-size:9px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#ContentPlaceHolder1_yr1FileNameFile').uploadifyUpload();" id="yr1FileNameFile_btup" class="uploadselection">Start Upload</a></li>
            </ul>
        </li>
        <li>BUSINESS REGISTRATION 
                <table style="width: 600px;" cellpadding="10">
                    <tr>
                        <td width="300"><b>For Corporations: </b></td>
                        <td width="300"><b>For Sole Proprietor:</b></td>
                    </tr>
                    <tr>
                        <td>SEC Registration (i.e. SEC Certificate, Articles of Incorporation and By Laws)
                            <ul>
                                <li>
                                    <script type="text/javascript">
								    // <![CDATA[
								    $(document).ready(function () {
								        $('#legalStrucCorpAttch_geninfoFile').uploadify({
								            'uploader': 'uploadify/uploadify.swf',
								            'script': 'upload.ashx',

								            'cancelImg': 'uploadify/cancel.png',
								            'auto': true,
								            'multi': true,
								            'fileDesc': 'Files',
								            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
								            'queueSizeLimit': 5,
								            'sizeLimit': 5000000,
								            'folder': 'uploads/<%= VendorId %>',
											'onComplete': function (event, queueID, fileObj, response, data) {
											    //alert(response);
											    $('.legalStrucCorpAttch_geninfoLbl').html('<a href="' + response + '" target="_blank">General Information Sheet</a>');
											    $('#ContentPlaceHolder1_legalStrucCorpAttch_geninfo').attr('value', response);
											    FileattchUpdateDB0('legalStrucCorpAttch_geninfo', 'tblVendorInformation', 'legalStrucCorpAttch_geninfo', '<%= VendorId %>');
											    alert("Upload successful");
											}
										});
									});
                                    // ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_geninfoFile" type="file"  class="uploadif" /></div> 
									<asp:Label ID="legalStrucCorpAttch_geninfoLbl" CssClass="legalStrucCorpAttch_geninfoLbl" runat="server" Text="General Information Sheet" style="float:left; padding-top:3px; display:block"></asp:Label>
									<input id="legalStrucCorpAttch_geninfo" name="legalStrucCorpAttch_geninfo" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Max file size: 4 MB)</div>
								<div class="clearfix"></div>
                                </li>
                                <li>
								<script type="text/javascript">
								    // <![CDATA[
								    $(document).ready(function () {
								        $('#legalStrucCorpAttch_geninfo2File').uploadify({
								            'uploader': 'uploadify/uploadify.swf',
								            'script': 'upload.ashx',

								            'cancelImg': 'uploadify/cancel.png',
								            'auto': true,
								            'multi': true,
								            'fileDesc': 'Files',
								            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
								            'queueSizeLimit': 5,
								            'sizeLimit': 5000000,
								            'folder': 'uploads/<%= VendorId %>',
											'onComplete': function (event, queueID, fileObj, response, data) {
											    //alert(response);
											    $('.legalStrucCorpAttch_geninfo2Lbl').html('<a href="' + response + '" target="_blank">SEC Certificate</a>');
											    $('#ContentPlaceHolder1_legalStrucCorpAttch_geninfo2').attr('value', response);
											    FileattchUpdateDB0('legalStrucCorpAttch_geninfo2', 'tblVendorInformation', 'legalStrucCorpAttch_geninfo2', '<%= VendorId %>');
											    alert("Upload successful");
											}
										});
									});
                                    // ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_geninfo2File" type="file"  class="uploadif" /></div> 
									<asp:Label ID="legalStrucCorpAttch_geninfo2Lbl" CssClass="legalStrucCorpAttch_geninfo2Lbl" runat="server" Text="SEC Certificate" style="float:left; padding-top:3px; display:block"></asp:Label>
									<input id="legalStrucCorpAttch_geninfo2" name="legalStrucCorpAttch_geninfo2" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Max file size: 4 MB)</div>
								<div class="clearfix"></div>
                                </li>
                                <li><script type="text/javascript">
                                        // <![CDATA[
                                        $(document).ready(function () {
                                            $('#legalStrucCorpAttch_geninfo3File').uploadify({
                                                'uploader': 'uploadify/uploadify.swf',
                                                'script': 'upload.ashx',

                                                'cancelImg': 'uploadify/cancel.png',
                                                'auto': true,
                                                'multi': true,
                                                'fileDesc': 'Files',
                                                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                                                'queueSizeLimit': 5,
                                                'sizeLimit': 5000000,
                                                'folder': 'uploads/<%= VendorId %>',
											'onComplete': function (event, queueID, fileObj, response, data) {
											    //alert(response);
											    $('.legalStrucCorpAttch_geninfo3Lbl').html('<a href="' + response + '" target="_blank">Articles of Incorporation and By Laws</a>');
											    $('#ContentPlaceHolder1_legalStrucCorpAttch_geninfo3').attr('value', response);
											    FileattchUpdateDB0('legalStrucCorpAttch_geninfo3', 'tblVendorInformation', 'legalStrucCorpAttch_geninfo3', '<%= VendorId %>');
											    alert("Upload successful");
											}
										});
									});
                                    // ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_geninfo3File" type="file" class="uploadif" /></div> 
									<asp:Label ID="legalStrucCorpAttch_geninfo3Lbl" CssClass="legalStrucCorpAttch_geninfo3Lbl" runat="server" Text="Articles of Incorporation and By Laws" style="float:left; padding-top:3px; display:block"></asp:Label>
									<input id="legalStrucCorpAttch_geninfo3" name="legalStrucCorpAttch_geninfo3" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Max file size: 4 MB)</div>
								<div class="clearfix"></div>
                                </li>
                                <li><script type="text/javascript">
                                        // <![CDATA[
                                        $(document).ready(function () {
                                            $('#legalStrucCorpAttch_geninfo4File').uploadify({
                                                'uploader': 'uploadify/uploadify.swf',
                                                'script': 'upload.ashx',

                                                'cancelImg': 'uploadify/cancel.png',
                                                'auto': true,
                                                'multi': true,
                                                'fileDesc': 'Files',
                                                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                                                'queueSizeLimit': 5,
                                                'sizeLimit': 5000000,
                                                'folder': 'uploads/<%= VendorId %>',
											'onComplete': function (event, queueID, fileObj, response, data) {
											    //alert(response);
											    $('.legalStrucCorpAttch_geninfo4Lbl').html('<a href="' + response + '" target="_blank">Others</a>');
											    $('#ContentPlaceHolder1_legalStrucCorpAttch_geninfo4').attr('value', response);
											    FileattchUpdateDB0('legalStrucCorpAttch_geninfo4', 'tblVendorInformation', 'legalStrucCorpAttch_geninfo4', '<%= VendorId %>');
											    alert("Upload successful");
											}
										});
									});
                                    // ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_geninfo4File" type="file" class="uploadif" /></div> 
									<asp:Label ID="legalStrucCorpAttch_geninfo4Lbl" CssClass="legalStrucCorpAttch_geninfo4Lbl" runat="server" Text="Others" style="float:left; padding-top:3px; display:block"></asp:Label>
									<input id="legalStrucCorpAttch_geninfo4" name="legalStrucCorpAttch_geninfo4" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Max file size: 4 MB)</div>
								<div class="clearfix"></div></li>
                                <li>Competent evidence of identity
                                    <ul>
                                        <li>
                                            <script type="text/javascript">
                                                // <![CDATA[
                                                $(document).ready(function () {
                                                    $('#legalStrucCorpAttch_IdentityAuthorizdSignaFile').uploadify({
                                                        'uploader': 'uploadify/uploadify.swf',
                                                        'script': 'upload.ashx',

                                                        'cancelImg': 'uploadify/cancel.png',
                                                        'auto': true,
                                                        'multi': true,
                                                        'fileDesc': 'Files',
                                                        'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                                                        'queueSizeLimit': 5,
                                                        'sizeLimit': 5000000,
                                                        'folder': 'uploads/<%= VendorId %>',
											'onComplete': function (event, queueID, fileObj, response, data) {
											    //alert(response);
											    $('.legalStrucCorpAttch_IdentityAuthorizdSignaLbl').html('<a href="' + response + '" target="_blank">Authorized signatories - Any government issued ID with picture</a>');
											    $('#ContentPlaceHolder1_legalStrucCorpAttch_IdentityAuthorizdSigna').attr('value', response);
											    FileattchUpdateDB0('legalStrucCorpAttch_IdentityAuthorizdSigna', 'tblVendorInformation', 'legalStrucCorpAttch_IdentityAuthorizdSigna', '<%= VendorId %>');
											    alert("Upload successful");
											}
										});
									});
                                    // ]]>
                                            </script>
                                            <div class="clearfix"></div>
                                            <div style="float: left; width: 30px;">
                                                <input id="legalStrucCorpAttch_IdentityAuthorizdSignaFile" type="file" class="uploadif" /></div>
                                            <asp:Label ID="legalStrucCorpAttch_IdentityAuthorizdSignaLbl" runat="server" CssClass="legalStrucCorpAttch_IdentityAuthorizdSignaLbl" Style="float: left; padding-top: 3px; display: block" Text="Authorized signatories - Any government issued ID with picture"></asp:Label>
                                            <input id="legalStrucCorpAttch_IdentityAuthorizdSigna" runat="server" name="legalStrucCorpAttch_IdentityAuthorizdSigna" type="hidden" value="" />
                                            <div style="font-size: 9px; clear: both;">(Max file size: 4 MB)</div>
                                            <div class="clearfix"></div>
                                            </li>
                                        <li>
								<script type="text/javascript">
								    // <![CDATA[
								    $(document).ready(function () {
								        $('#legalStrucCorpAttch_IdentitytaxcertFile').uploadify({
								            'uploader': 'uploadify/uploadify.swf',
								            'script': 'upload.ashx',

								            'cancelImg': 'uploadify/cancel.png',
								            'auto': true,
								            'multi': true,
								            'fileDesc': 'Files',
								            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
								            'queueSizeLimit': 5,
								            'sizeLimit': 5000000,
								            'folder': 'uploads/<%= VendorId %>',
											'onComplete': function (event, queueID, fileObj, response, data) {
											    //alert(response);
											    $('.legalStrucCorpAttch_IdentitytaxcertLbl').html('<a href="' + response + '" target="_blank">Company – Community Tax Certificate</a>');
											    $('#ContentPlaceHolder1_legalStrucCorpAttch_Identitytaxcert').attr('value', response);
											    FileattchUpdateDB0('legalStrucCorpAttch_Identitytaxcert', 'tblVendorInformation', 'legalStrucCorpAttch_Identitytaxcert', '<%= VendorId %>');
											    alert("Upload successful");
											}
										});
									});
                                    // ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_IdentitytaxcertFile" type="file" class="uploadif" /></div> 
									<asp:Label ID="legalStrucCorpAttch_IdentitytaxcertLbl" CssClass="legalStrucCorpAttch_IdentitytaxcertLbl" runat="server" Text="Company – Community Tax Certificate" style="float:left; padding-top:3px; display:block"></asp:Label>
									<input id="legalStrucCorpAttch_Identitytaxcert" name="legalStrucCorpAttch_Identitytaxcert" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Max file size: 4 MB)</div>
								<div class="clearfix"></div></li>
                                    </ul>
                                </li>
                                <li>
								<script type="text/javascript">
								    // <![CDATA[
								    $(document).ready(function () {
								        $('#legalStrucCorpAttch_BoardAuthorizdSignaFile').uploadify({
								            'uploader': 'uploadify/uploadify.swf',
								            'script': 'upload.ashx',

								            'cancelImg': 'uploadify/cancel.png',
								            'auto': true,
								            'multi': true,
								            'fileDesc': 'Files',
								            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
								            'queueSizeLimit': 5,
								            'sizeLimit': 5000000,
								            'folder': 'uploads/<%= VendorId %>',
											'onComplete': function (event, queueID, fileObj, response, data) {
											    //alert(response);
											    $('.legalStrucCorpAttch_BoardAuthorizdSignaLbl').html('<a href="' + response + '" target="_blank">Board Resolution / Secretary certificate of authorized signatories</a>');
											    $('#ContentPlaceHolder1_legalStrucCorpAttch_BoardAuthorizdSigna').attr('value', response);
											    FileattchUpdateDB0('legalStrucCorpAttch_BoardAuthorizdSigna', 'tblVendorInformation', 'legalStrucCorpAttch_BoardAuthorizdSigna', '<%= VendorId %>');
											    alert("Upload successful");
											}
										});
									});
                                    // ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_BoardAuthorizdSignaFile" type="file" class="uploadif" /></div> 
									<asp:Label ID="legalStrucCorpAttch_BoardAuthorizdSignaLbl" CssClass="legalStrucCorpAttch_BoardAuthorizdSignaLbl" runat="server" Text="Board Resolution / Secretary certificate of authorized signatories" style="float:left; padding-top:3px; display:block; font-weight:bold"></asp:Label>
									<input id="legalStrucCorpAttch_BoardAuthorizdSigna" name="legalStrucCorpAttch_BoardAuthorizdSigna" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Max file size: 4 MB)</div>
								<div class="clearfix"></div></li>
                            </ul>
                        </td>
                        <td>
                            <ul>
                                <li>
								<script type="text/javascript">
								    // <![CDATA[
								    $(document).ready(function () {
								        $('#legalStrucSoleAttch_DTIRegFile').uploadify({
								            'uploader': 'uploadify/uploadify.swf',
								            'script': 'upload.ashx',

								            'cancelImg': 'uploadify/cancel.png',
								            'auto': true,
								            'multi': true,
								            'fileDesc': 'Files',
								            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
								            'queueSizeLimit': 5,
								            'sizeLimit': 5000000,
								            'folder': 'uploads/<%= VendorId %>',
											'onComplete': function (event, queueID, fileObj, response, data) {
											    //alert(response);
											    $('.legalStrucSoleAttch_DTIRegLbl').html('<a href="' + response + '" target="_blank">DTI Registration – attach letter of certification from Proprietor stating registered / authorized capital</a>');
											    $('#ContentPlaceHolder1_legalStrucSoleAttch_DTIReg').attr('value', response);
											    FileattchUpdateDB0('legalStrucSoleAttch_DTIReg', 'tblVendorInformation', 'legalStrucSoleAttch_DTIReg', '<%= VendorId %>');
											    alert("Upload successful");
											}
										});
									});
                                    // ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucSoleAttch_DTIRegFile" type="file"  class="uploadif" /></div> 
									<asp:Label ID="legalStrucSoleAttch_DTIRegLbl" CssClass="legalStrucSoleAttch_DTIRegLbl" runat="server" Text="DTI Registration – attach letter of certification from Proprietor stating registered / authorized capital" style="float:left; padding-top:3px; display:block; font-weight:bold;"></asp:Label>
									<input id="legalStrucSoleAttch_DTIReg" name="legalStrucSoleAttch_DTIReg" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Max file size: 4 MB)</div>
								<div class="clearfix"></div></li>
                                <li>
								<script type="text/javascript">
								    // <![CDATA[
								    $(document).ready(function () {
								        $('#legalStrucSoleAttch_OwnersId1File').uploadify({
								            'uploader': 'uploadify/uploadify.swf',
								            'script': 'upload.ashx',

								            'cancelImg': 'uploadify/cancel.png',
								            'auto': true,
								            'multi': true,
								            'fileDesc': 'Files',
								            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
								            'queueSizeLimit': 5,
								            'sizeLimit': 5000000,
								            'folder': 'uploads/<%= VendorId %>',
											'onComplete': function (event, queueID, fileObj, response, data) {
											    //alert(response);
											    $('.legalStrucSoleAttch_OwnersId1Lbl').html('<a href="' + response + '" target="_blank">Owner’s Identification ** valid IDs include: Driver’s License, Passport or NBI Clearance</a>');
											    $('#ContentPlaceHolder1_legalStrucSoleAttch_OwnersId1').attr('value', response);
											    FileattchUpdateDB0('legalStrucSoleAttch_OwnersId1', 'tblVendorInformation', 'legalStrucSoleAttch_OwnersId1', '<%= VendorId %>');
											    alert("Upload successful");
											}
										});
									});
                                    // ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucSoleAttch_OwnersId1File" type="file"/></div> 
									<asp:Label ID="legalStrucSoleAttch_OwnersId1Lbl" CssClass="legalStrucSoleAttch_OwnersId1Lbl" runat="server" Text="Owner’s Identification ** valid IDs include: Driver’s License, Passport or NBI Clearance" style="float:left; padding-top:3px; display:block; font-weight:bold"></asp:Label>
									<input id="legalStrucSoleAttch_OwnersId1" name="legalStrucSoleAttch_OwnersId1" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Max file size: 4 MB)</div>
								<div class="clearfix"></div></li>
                                <li>
								<script type="text/javascript">
								    // <![CDATA[
								    $(document).ready(function () {
								        $('#legalStrucSoleAttch_CTCFile').uploadify({
								            'uploader': 'uploadify/uploadify.swf',
								            'script': 'upload.ashx',

								            'cancelImg': 'uploadify/cancel.png',
								            'auto': true,
								            'multi': true,
								            'fileDesc': 'Files',
								            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
								            'queueSizeLimit': 5,
								            'sizeLimit': 5000000,
								            'folder': 'uploads/<%= VendorId %>',
											'onComplete': function (event, queueID, fileObj, response, data) {
											    //alert(response);
											    $('.legalStrucSoleAttch_CTCLbl').html('<a href="' + response + '" target="_blank">Community Tax Certificate of the owner (CTC)</a>');
											    $('#ContentPlaceHolder1_legalStrucSoleAttch_CTC').attr('value', response);
											    $('#ContentPlaceHolder1_legalStrucSoleAttch_OwnersId1').attr('value', response);
											    FileattchUpdateDB0('legalStrucSoleAttch_CTC', 'tblVendorInformation', 'legalStrucSoleAttch_CTC', '<%= VendorId %>');
											    alert("Upload successful");
											}
										});
									});
                                    // ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucSoleAttch_CTCFile" type="file"/></div> 
									<asp:Label ID="legalStrucSoleAttch_CTCLbl" CssClass="legalStrucSoleAttch_CTCLbl" runat="server" Text="Community Tax Certificate of the owner (CTC)" style="float:left; padding-top:3px; display:block; font-weight:bold"></asp:Label>
									<input id="legalStrucSoleAttch_CTC" name="legalStrucSoleAttch_CTC" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Max file size: 4 MB)</div>
								<div class="clearfix"></div></li>
                            </ul>

                        </td>
                    </tr>
                </table>
            </li>
        <li>Business Permit / Mayor’s Permit 
            <div class="clearfix"></div>
	<script type="text/javascript">
	    // <![CDATA[
	    $(document).ready(function () {
	        $('#fileupload2').uploadify({
	            'uploader': 'uploadify/uploadify.swf',
	            'script': 'upload.ashx',

	            'cancelImg': 'uploadify/cancel.png',
	            'auto': true,
	            'multi': true,
	            'fileDesc': 'Files',
	            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
	            'queueSizeLimit': 5,
	            'sizeLimit': 5000000,
	            'folder': 'uploads/<%= VendorId %>',
				'onComplete': function (event, queueID, fileObj, response, data) {
				    //alert(response);
				    $('.fileuploaded2').html('<a href="' + response + '" target="_blank">Attached file</a>');
				    $('#ContentPlaceHolder1_busPermitAttachement').attr('value', response);
				    FileattchUpdateDB0('busPermitAttachement', 'tblVendorInformation', 'busPermitAttachement', '<%= VendorId %>');
				    alert("Upload successful");
				}
			});
		});
        // ]]>
	</script>
		<div style="float:left; width:30px;"><input id="fileupload2" type="file"/></div> 
		<asp:Label ID="fileuploaded2" CssClass="fileuploaded2" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
		<input id="busPermitAttachement" name="busPermitAttachement" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max file size: 4 MB)</div><div class="clearfix"></div>
        </li>
        <li>Regulatory Requirements
            <ul>
                <li>For Contractors (at least an “A” License from the Philippine Contractor’s Accreditation Board (PCAB)</li>
                <li>For Couriers/Forwarders: Department of Transportation and Communication (DOTC) </li>
                <li>For Brokers: Port Accreditation Certificate</li>
                <li>For Manpower Services : Department of Labor and Employment (DOLE)</li>
                <li>For VAS providers (NTC permit)</li>
                <li>
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile10 = 'reguReqAttachmentFile';
            FieldLbl10 = 'reguReqAttachmentLbl';
            FieldHidden10 = 'reguReqAttachment';
            dbTbl10 = 'tblVendorInformation';
            dbCol10 = 'reguReqAttachment';
            dbVendorId10 = '<%= VendorId %>';
            $('#' + FieldFile10 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile10).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile10 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile10 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl10).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"' + FieldHidden10 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden10 + '\').val(), \'' + response + '\', \'' + FieldHidden10 + '\', \'' + dbTbl10 + '\', \'' + dbCol10 + '\', \'' + dbVendorId10 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden10).attr('value', $('#ContentPlaceHolder1_' + FieldHidden10).val() + ';' + response);
                    $('#' + FieldFile10 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden10, dbTbl10, dbCol10, dbVendorId10);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="reguReqAttachmentFile" type="file" runat="server"/></div> 
        <asp:Label ID="reguReqAttachmentLbl" CssClass="reguReqAttachmentLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="reguReqAttachment" name="reguReqAttachment" runat="server" type="hidden" value="" />
        <div style="font-size:10px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= reguReqAttachmentFile.ClientID %>').uploadifyUpload();" id="reguReqAttachmentFile_btup" class="uploadselection">Start Upload</a>
            <div class="clearfix"></div>
                </li>
            </ul>
        </li>
        <%--<li>Last 6 months utility billing statement (water, telco, power) </li>--%>
        <li>PROOF OF BUSINESS ADDRESS
            <br>Any of the following:
            <ul>
                <li>Transfer Certificate Title (TCT)</li>
                <li>Lease Agreement</li>
                <li>Last 6 months utility billing statement (water, telco, power)</li>
                <li><table width="584"  border="0" cellpadding="5" cellspacing="0" id="tbl01" runat="server">
  <tr>
    <td width="209"><label for="regOwnedAttachment" id="regOwnedAttachmentLblfor">If Owned, Attach Transfer Certificate Title (TCT), Lease Agreement, 6 months utility bills</label></td>
    <td>
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile1 = 'regOwnedAttachmentFile';
            FieldLbl1 = 'regOwnedAttachmentLbl';
            FieldHidden1 = 'regOwnedAttachment';
            dbTbl1 = 'tblVendorInformation';
            dbCol1 = 'regOwnedAttachment';
            dbVendorId1 = '<%= VendorId %>';
            $('#' + FieldFile1 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile1).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile1 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile1 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl1).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"' + FieldHidden1 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden1 + '\').val(), \'' + response + '\', \'' + FieldHidden1 + '\', \'' + dbTbl1 + '\', \'' + dbCol1 + '\', \'' + dbVendorId1 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden1).attr('value', $('#ContentPlaceHolder1_' + FieldHidden1).val() + ';' + response);
                    $('#' + FieldFile1 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden1, dbTbl1, dbCol1, dbVendorId1);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="regOwnedAttachmentFile" type="file" runat="server"/></div> 
        <asp:Label ID="regOwnedAttachmentLbl" CssClass="regOwnedAttachmentLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label> 
        <input id="regOwnedAttachment" name="regOwnedAttachment" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#ContentPlaceHolder1_regOwnedAttachmentFile').uploadifyUpload();" id="regOwnedAttachmentFile_btup" class="uploadselection">Start Upload</a></td>
  </tr>
  </table></li>
            </ul>
        </li>
        <li>BIR CERTIFICATE (BIR Form 2303 or 1556)
            <div class="clearfix"></div>
	<script type="text/javascript">
	    // <![CDATA[
	    $(document).ready(function () {
	        $('#fileupload3').uploadify({
	            'uploader': 'uploadify/uploadify.swf',
	            'script': 'upload.ashx',

	            'cancelImg': 'uploadify/cancel.png',
	            'auto': true,
	            'multi': true,
	            'fileDesc': 'Files',
	            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
	            'queueSizeLimit': 5,
	            'sizeLimit': 5000000,
	            'folder': 'uploads/<%= VendorId %>',
				'onComplete': function (event, queueID, fileObj, response, data) {
				    //alert(response);
				    $('.fileuploaded3').html('<a href="' + response + '" target="_blank">Attached file</a>');
				    $('#ContentPlaceHolder1_birRegAttachement').attr('value', response);
				    FileattchUpdateDB0('birRegAttachement', 'tblVendorInformation', 'birRegAttachement', '<%= VendorId %>');
				    alert("Upload successful");
				}
	        });
	    });
        // ]]>
	</script>
		<div style="float:left; width:30px;"><input id="fileupload3" type="file"/></div> 
		<asp:Label ID="fileuploaded3" CssClass="fileuploaded3" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
		<input id="birRegAttachement" name="birRegAttachement" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max file size: 4 MB)</div><div class="clearfix"></div>

        </li>
        <li>Last 6 months SSS / Pag-ibig / Philhealth  remittances (receipt only)

            <table width="584"  border="0" cellpadding="5" cellspacing="0" id="tbl02" runat="server">
  <tr>
    <td style="border-top: thin #CCC dotted; width: 109px;"><strong>Pag-IBIG/HDMF</strong></td>
    <td width="137" style="border-top: thin #CCC dotted;">
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile2 = 'benefitsPagibigFileNameFile';
            FieldLbl2 = 'benefitsPagibigFileNameLbl';
            FieldHidden2 = 'benefitsPagibigFileName';
            dbTbl2 = 'tblVendorInformation';
            dbCol2 = 'benefitsPagibigFileName';
            dbVendorId2 = '<%= VendorId %>';
            $('#' + FieldFile2 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile2).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile2 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile2 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl2).append('<div><a href="' + response + '" target="_blank">PAG-IBIG Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"' + FieldHidden2 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden2 + '\').val(), \'' + response + '\', \'' + FieldHidden2 + '\', \'' + dbTbl2 + '\', \'' + dbCol2 + '\', \'' + dbVendorId2 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden2).attr('value', $('#ContentPlaceHolder1_' + FieldHidden2).val() + ';' + response);
                    $('#' + FieldFile2 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden2, dbTbl2, dbCol2, dbVendorId2);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="benefitsPagibigFileNameFile" type="file" runat="server"/></div> 
        <asp:Label ID="benefitsPagibigFileNameLbl" CssClass="benefitsPagibigFileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="benefitsPagibigFileName" name="benefitsPagibigFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= benefitsPagibigFileNameFile.ClientID %>').uploadifyUpload();" id="benefitsPagibigFileNameFile_btup" class="uploadselection">Start Upload</a>
        </td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted; width: 109px; height: 61px;"><strong>Philhealth</strong></td>
    <td style="border-top: thin #CCC dotted; height: 61px;">
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile3 = 'benefitsPHICFileNameFile';
            FieldLbl3 = 'benefitsPHICFileNameLbl';
            FieldHidden3 = 'benefitsPHICFileName';
            dbTbl3 = 'tblVendorInformation';
            dbCol3 = 'benefitsPHICFileName';
            dbVendorId3 = '<%= VendorId %>';
            $('#' + FieldFile3 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile3).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile3 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile3 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl3).append('<div><a href="' + response + '" target="_blank">PHILHEALTH Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"' + FieldHidden3 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden3 + '\').val(), \'' + response + '\', \'' + FieldHidden3 + '\', \'' + dbTbl3 + '\', \'' + dbCol3 + '\', \'' + dbVendorId3 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden3).attr('value', $('#ContentPlaceHolder1_' + FieldHidden3).val() + ';' + response);
                    $('#' + FieldFile3 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden3, dbTbl3, dbCol3, dbVendorId3);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="benefitsPHICFileNameFile" type="file" runat="server"/></div> 
        <asp:Label ID="benefitsPHICFileNameLbl" CssClass="benefitsPHICFileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="benefitsPHICFileName" name="benefitsPHICFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= benefitsPHICFileNameFile.ClientID %>').uploadifyUpload();" id="benefitsPHICFileNameFile_btup" class="uploadselection">Start Upload</a>
    </td>
    </tr>
  <tr>
    <td style="border-top: thin #CCC dotted; width: 109px;"><strong>SSS</strong></td>
    <td style="border-top: thin #CCC dotted;">
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile5 = 'benefitsSSSFileNameFile';
            FieldLbl5 = 'benefitsSSSFileNameLbl';
            FieldHidden5 = 'benefitsSSSFileName';
            dbTbl5 = 'tblVendorInformation';
            dbCol5 = 'benefitsSSSFileName';
            dbVendorId5 = '<%= VendorId %>';
            $('#' + FieldFile5 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile5).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile5 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile5 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl5).append('<div><a href="' + response + '" target="_blank">SSS Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"' + FieldHidden5 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden5 + '\').val(), \'' + response + '\', \'' + FieldHidden5 + '\', \'' + dbTbl5 + '\', \'' + dbCol5 + '\', \'' + dbVendorId5 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden5).attr('value', $('#ContentPlaceHolder1_' + FieldHidden5).val() + ';' + response);
                    $('#' + FieldFile5 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden5, dbTbl5, dbCol5, dbVendorId5);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="benefitsSSSFileNameFile" type="file" runat="server"/></div> 
        <asp:Label ID="benefitsSSSFileNameLbl" CssClass="benefitsSSSFileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="benefitsSSSFileName" name="benefitsSSSFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= benefitsSSSFileNameFile.ClientID %>').uploadifyUpload();" id="benefitsSSSFileNameFile_btup" class="uploadselection">Start Upload</a>
    </td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted; width: 109px;"><strong>Other Medical Benefits</strong></td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted; width: 109px;"><strong>Others</strong></td>
    <td style="border-top: thin #CCC dotted;">
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile6 = 'benefitsOthersFileNameFile';
            FieldLbl6 = 'benefitsOthersFileNameLbl';
            FieldHidden6 = 'benefitsOthersFileName';
            dbTbl6 = 'tblVendorInformation';
            dbCol6 = 'benefitsOthersFileName';
            dbVendorId6 = '<%= VendorId %>';
            $('#' + FieldFile6 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile6).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile6 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile6 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl6).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"' + FieldHidden6 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden6 + '\').val(), \'' + response + '\', \'' + FieldHidden6 + '\', \'' + dbTbl6 + '\', \'' + dbCol6 + '\', \'' + dbVendorId6 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden6).attr('value', $('#ContentPlaceHolder1_' + FieldHidden6).val() + ';' + response);
                    $('#' + FieldFile6 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden6, dbTbl6, dbCol6, dbVendorId6);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="benefitsOthersFileNameFile" type="file" runat="server"/></div> 
        <asp:Label ID="benefitsOthersFileNameLbl" CssClass="benefitsOthersFileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="benefitsOthersFileName" name="benefitsOthersFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= benefitsOthersFileNameFile.ClientID %>').uploadifyUpload();" id="benefitsOthersFileNameFile_btup" class="uploadselection">Start Upload</a>
        </td>
  </tr>
</table>
        </li>
        <li>Copies of Certifications ( ISO 9000/Other)
            <div class="clearfix"></div>
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile11 = 'copiesCertificationsFile';
            FieldLbl11 = 'copiesCertificationsLbl';
            FieldHidden11 = 'copiesCertifications';
            dbTbl11 = 'tblVendorInformation';
            dbCol11 = 'copiesCertifications';
            dbVendorId11 = '<%= VendorId %>';
            $('#' + FieldFile11 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile11).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile11 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile11 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl11).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:11px; padding-top:5px; \" id=\"' + FieldHidden11 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden11 + '\').val(), \'' + response + '\', \'' + FieldHidden11 + '\', \'' + dbTbl11 + '\', \'' + dbCol11 + '\', \'' + dbVendorId11 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden11).attr('value', $('#ContentPlaceHolder1_' + FieldHidden11).val() + ';' + response);
                    $('#' + FieldFile11 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden11, dbTbl11, dbCol11, dbVendorId11);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="copiesCertificationsFile" type="file" runat="server"/></div> 
        <asp:Label ID="copiesCertificationsLbl" CssClass="copiesCertificationsLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="copiesCertifications" name="copiesCertifications" runat="server" type="hidden" value="" />
        <div style="font-size:11px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= copiesCertificationsFile.ClientID %>').uploadifyUpload();" id="copiesCertificationsFile_btup" class="uploadselection">Start Upload</a>
            <div class="clearfix"></div>
        </li>
        <li>Company Profile<table width="0"  border="0" cellpadding="5" cellspacing="0" id="tbl04" runat="server">
  <tr>
    <td width="220px" style="border-top: thin #CCC dotted;"><strong>Machineries/Equipment/Vehicles</strong></td>
    <td style="border-top: thin #CCC dotted;">
        <script type="text/javascript">
                                                  // <![CDATA[
                                                  $(document).ready(function () {
                                                      FieldFile7 = 'assetsMachineriesFileNameFile';
                                                      FieldLbl7 = 'assetsMachineriesFileNameLbl';
                                                      FieldHidden7 = 'assetsMachineriesFileName';
                                                      dbTbl7 = 'tblVendorInformation';
                                                      dbCol7 = 'assetsMachineriesFileName';
                                                      dbVendorId7 = '<%= VendorId %>';
            $('#' + FieldFile7 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile7).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile7 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile7 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl7).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"' + FieldHidden7 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden7 + '\').val(), \'' + response + '\', \'' + FieldHidden7 + '\', \'' + dbTbl7 + '\', \'' + dbCol7 + '\', \'' + dbVendorId7 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden7).attr('value', $('#ContentPlaceHolder1_' + FieldHidden7).val() + ';' + response);
                    $('#' + FieldFile7 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden7, dbTbl7, dbCol7, dbVendorId7);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="assetsMachineriesFileNameFile" type="file" runat="server"/></div> 
        <asp:Label ID="assetsMachineriesFileNameLbl" CssClass="assetsMachineriesFileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="assetsMachineriesFileName" name="assetsMachineriesFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= assetsMachineriesFileNameFile.ClientID %>').uploadifyUpload();" id="assetsMachineriesFileNameFile_btup" class="uploadselection">Start Upload</a>
    </td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Company Profile</strong></td>
    <td style="border-top: thin #CCC dotted;">
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile8 = 'assetsCompanyProfileFileNameFile';
            FieldLbl8 = 'assetsCompanyProfileFileNameLbl';
            FieldHidden8 = 'assetsCompanyProfileFileName';
            dbTbl8 = 'tblVendorInformation';
            dbCol8 = 'assetsCompanyProfileFileName';
            dbVendorId8 = '<%= VendorId %>';
            $('#' + FieldFile8 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile8).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile8 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile8 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl8).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"' + FieldHidden8 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden8 + '\').val(), \'' + response + '\', \'' + FieldHidden8 + '\', \'' + dbTbl8 + '\', \'' + dbCol8 + '\', \'' + dbVendorId8 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden8).attr('value', $('#ContentPlaceHolder1_' + FieldHidden8).val() + ';' + response);
                    $('#' + FieldFile8 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden8, dbTbl8, dbCol8, dbVendorId8);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="assetsCompanyProfileFileNameFile" type="file" runat="server"/></div> 
        <asp:Label ID="assetsCompanyProfileFileNameLbl" CssClass="assetsCompanyProfileFileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="assetsCompanyProfileFileName" name="assetsCompanyProfileFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= assetsCompanyProfileFileNameFile.ClientID %>').uploadifyUpload();" id="assetsCompanyProfileFileNameFile_btup" class="uploadselection">Start Upload</a>
    </td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Others</strong></td>
    <td style="border-top: thin #CCC dotted;">

    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile9 = 'assetsOthersFileNameFile';
            FieldLbl9 = 'assetsOthersFileNameLbl';
            FieldHidden9 = 'assetsOthersFileName';
            dbTbl9 = 'tblVendorInformation';
            dbCol9 = 'assetsOthersFileName';
            dbVendorId9 = '<%= VendorId %>';
            $('#' + FieldFile9 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile9).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile9 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile9 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl9).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"' + FieldHidden9 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden9 + '\').val(), \'' + response + '\', \'' + FieldHidden9 + '\', \'' + dbTbl9 + '\', \'' + dbCol9 + '\', \'' + dbVendorId9 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden9).attr('value', $('#ContentPlaceHolder1_' + FieldHidden9).val() + ';' + response);
                    $('#' + FieldFile9 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden9, dbTbl9, dbCol9, dbVendorId9);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="assetsOthersFileNameFile" type="file" runat="server"/></div> 
        <asp:Label ID="assetsOthersFileNameLbl" CssClass="assetsOthersFileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="assetsOthersFileName" name="assetsOthersFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= assetsOthersFileNameFile.ClientID %>').uploadifyUpload();" id="assetsOthersFileNameFile_btup" class="uploadselection">Start Upload</a>
        </td>
  </tr>
</table></li>
        <li>Table of Organization with names and designation
            <div class="clearfix"></div>
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile12 = 'tableOrganizationFile';
            FieldLbl12 = 'tableOrganizationLbl';
            FieldHidden12 = 'tableOrganization';
            dbTbl12 = 'tblVendorInformation';
            dbCol12 = 'tableOrganization';
            dbVendorId12 = '<%= VendorId %>';
            $('#' + FieldFile12 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile12).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile12 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile12 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl12).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:12px; padding-top:5px; \" id=\"' + FieldHidden12 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden12 + '\').val(), \'' + response + '\', \'' + FieldHidden12 + '\', \'' + dbTbl12 + '\', \'' + dbCol12 + '\', \'' + dbVendorId12 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden12).attr('value', $('#ContentPlaceHolder1_' + FieldHidden12).val() + ';' + response);
                    $('#' + FieldFile12 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden12, dbTbl12, dbCol12, dbVendorId12);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="tableOrganizationFile" type="file" runat="server"/></div> 
        <asp:Label ID="tableOrganizationLbl" CssClass="tableOrganizationLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="tableOrganization" name="tableOrganization" runat="server" type="hidden" value="" />
        <div style="font-size:12px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= tableOrganizationFile.ClientID %>').uploadifyUpload();" id="tableOrganizationFile_btup" class="uploadselection">Start Upload</a>
            <div class="clearfix"></div>
        </li>
        <li>Certification and Warranty- If vendor opts to present original copies of the submitted documents.
            
                    <br />
                    &bull; <a href="Certification_and_Warranty.pdf" target="_blank"  style="font-size:12px; font-weight:bold;">Download & Print</a>
                    <br />
            
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile13 = 'CertAndWarranty_AttachedSignedFile';
            FieldLbl13 = 'CertAndWarranty_AttachedSignedLbl';
            FieldHidden13 = 'CertAndWarranty_AttachedSigned';
            dbTbl13 = 'tblVendorInformation';
            dbCol13 = 'CertAndWarranty_AttachedSigned';
            dbVendorId13 = '<%= VendorId %>';
            $('#' + FieldFile13 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile13).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile13 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile13 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl13).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:13px; padding-top:5px; \" id=\"' + FieldHidden13 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden13 + '\').val(), \'' + response + '\', \'' + FieldHidden13 + '\', \'' + dbTbl13 + '\', \'' + dbCol13 + '\', \'' + dbVendorId13 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden13).attr('value', $('#ContentPlaceHolder1_' + FieldHidden13).val() + ';' + response);
                    $('#' + FieldFile13 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden13, dbTbl13, dbCol13, dbVendorId13);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="CertAndWarranty_AttachedSignedFile" type="file" runat="server"/></div> 
        <asp:Label ID="CertAndWarranty_AttachedSignedLbl" CssClass="CertAndWarranty_AttachedSignedLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="CertAndWarranty_AttachedSigned" name="CertAndWarranty_AttachedSigned" runat="server" type="hidden" value="" />
        <div style="font-size:13px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= CertAndWarranty_AttachedSignedFile.ClientID %>').uploadifyUpload();" id="CertAndWarranty_AttachedSignedFile_btup" class="uploadselection">Start Upload</a>
            <div class="clearfix"></div>
        </li>
        <li>On-going and Completed Projects for contractors
            <table style="width: 600px;" cellpadding="10">
                    <tr>
                        <td width="300"><b>I. List of Completed Projects</b></td>
                        <td width="300"><b>II. List of On-going Projects  </b></td>
                    </tr>
                    <tr>
                        <td>
                            <ul>
                                <li>Client:</li>
                                <li>Project Nature:</li>
                                <li>Project Location:</li>
                                <li>Project Value:</li>
                                <li>Start Date:</li>
                                <li>Date Completed:</li>
                            </ul>
                        </td>
                        <td>
                            <ul>
                                <li>Client:</li>
                                <li>Project Nature:</li>
                                <li>Project Location:</li>
                                <li>Project Value:</li>
                                <li>Start Date:</li>
                                <li>Target Date Completed:</li>
                                <li>Percentage Accomplished:</li>
                            </ul>
                        </td>
                    </tr>
                </table>
            
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            FieldFile14 = 'projectsForContractorsFile';
            FieldLbl14 = 'projectsForContractorsLbl';
            FieldHidden14 = 'projectsForContractors';
            dbTbl14 = 'tblVendorInformation';
            dbCol14 = 'projectsForContractors';
            dbVendorId14 = '<%= VendorId %>';
            $('#' + FieldFile14 + '_btup').hide();
            $('#ContentPlaceHolder1_' + FieldFile14).uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload_required.ashx',
                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 5000000,
                'folder': 'uploads/<%= VendorId %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#' + FieldFile14 + '_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#' + FieldFile14 + '_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    $('#ContentPlaceHolder1_' + FieldLbl14).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:14px; padding-top:5px; \" id=\"' + FieldHidden14 + 'x\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0( $(\'#ContentPlaceHolder1_' + FieldHidden14 + '\').val(), \'' + response + '\', \'' + FieldHidden14 + '\', \'' + dbTbl14 + '\', \'' + dbCol14 + '\', \'' + dbVendorId14 + '\'); }\" /><br></div>');
                    $('#ContentPlaceHolder1_' + FieldHidden14).attr('value', $('#ContentPlaceHolder1_' + FieldHidden14).val() + ';' + response);
                    $('#' + FieldFile14 + '_btup').hide();
                    //alert(response);
                },
                'onAllComplete': function (event, queueID, fileObj, response, data) {
                    FileattchUpdateDB0(FieldHidden14, dbTbl14, dbCol14, dbVendorId14);
                    alert("Upload successful");
                }
            });
        });
        // ]]>
    </script>
            <div style=" font-size:12px; font-weight:normal;">
        <div style="float:left; width:30px;"><input id="projectsForContractorsFile" type="file" runat="server"/></div> 
        <asp:Label ID="projectsForContractorsLbl" CssClass="projectsForContractorsLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="projectsForContractors" name="projectsForContractors" runat="server" type="hidden" value="" />
        <div style="font-size:14px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= projectsForContractorsFile.ClientID %>').uploadifyUpload();" id="projectsForContractorsFile_btup" class="uploadselection">Start Upload</a></div>
            <div class="clearfix"></div>
        </li>
    </ol>

    <br />
    <br />
<script type="text/javascript">
    $("document").ready(function () {
        $(".alternativeRow").btnAddRow({ inputBoxAutoNumber: true, inputBoxAutoId: true, displayRowCountTo: "rowCount" });
        $(".delRow").btnDelRow();

        if ('<%= editable() %>' == 'False')
            {
                $('input:file').each(function () {
                    $(this).uploadify('disable', true);
                    $("<img src=\"images/attachment.png\">").insertBefore(this);
                });
                $("img").each(function () {
                    if ($(this).attr("src") == "images/xicon.png") {
                        $(this).hide();
                    }
                });
            }
    });
</script>



<div class="separator1"></div>
<br />
<br />
<br />
    &nbsp;&nbsp;
<a href="vendor_Home.aspx<%= queryString %>" class="bt1"><span>BACK</span></a>
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

</asp:Content>
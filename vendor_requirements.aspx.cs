using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;
using System.Text;
using System.Text.RegularExpressions;

public partial class vendor_requirements : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query, sCommand;
    SqlCommand cmd;
    SqlConnection conn;
    public int VendorId;
    public string queryString;

    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        queryString = (Request.QueryString["VendorId"] != null && Request.QueryString["VendorId"] != "") ? "?VendorId=" + Request.QueryString["VendorId"] : "";
        if (Request.QueryString["VendorId"] != null && Request.QueryString["VendorId"] != "")
        {
            VendorId = Convert.ToInt32(Request.QueryString["VendorId"]);
        }
        else if (Session["VendorId"] != null && Session["VendorId"].ToString() != "")
        {
            VendorId = Convert.ToInt32(Session["VendorId"].ToString());
        }
        //TestShowAllSessions();
        if (Session["UserId"] == null || Session["UserId"].ToString() == "") Response.Redirect("login.aspx");
        //if (Session["SESSION_USERTYPE"].ToString() != "11" || Session["SESSION_USERTYPE"] == null) { Response.Redirect("login.aspx"); }

        PopulateFields();
        
    }






    public bool editable()
    {
        bool EditMode = false;
        query = "SELECT * FROM tblVendor WHERE VendorId =  @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        if (
                            (Session["SESSION_USERTYPE"].ToString() == "11" && oReader["Status"].ToString() == "0") ||
                            (Session["SESSION_USERTYPE"].ToString() == "11" && oReader["Status"].ToString() == "9")
                            )
                        {
                            EditMode = true;
                        }
                    }
                }
            }
        }

        return EditMode;
    }



    void PopulateFields()
    {
        query = "SELECT * FROM tblVendorInformation WHERE VendorId=  @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {

                        if (oReader["reguReqAttachment"].ToString() != "")
                        {
                            string[] reguReqAttachments1 = oReader["reguReqAttachment"].ToString().Split(';');
                            foreach (string reguReqAttachment1 in reguReqAttachments1)
                            {
                                reguReqAttachmentLbl.Text = reguReqAttachment1.Trim() != "" ? reguReqAttachmentLbl.Text + "<div><a href='" + reguReqAttachment1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"reguReqAttachmentx\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_reguReqAttachment\').val(),\'" + reguReqAttachment1.Trim() + "\',\'reguReqAttachment\', \'tblVendorInformation\', \'reguReqAttachment\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            reguReqAttachmentLbl.Text = "Attach file<br>";
                        }
                        reguReqAttachment.Value = oReader["reguReqAttachment"].ToString();




                        if (oReader["copiesCertifications"].ToString() != "")
                        {
                            string[] copiesCertificationss1 = oReader["copiesCertifications"].ToString().Split(';');
                            foreach (string copiesCertifications1 in copiesCertificationss1)
                            {
                                copiesCertificationsLbl.Text = copiesCertifications1.Trim() != "" ? copiesCertificationsLbl.Text + "<div><a href='" + copiesCertifications1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"copiesCertificationsx\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_copiesCertifications\').val(),\'" + copiesCertifications1.Trim() + "\',\'copiesCertifications\', \'tblVendorInformation\', \'copiesCertifications\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            copiesCertificationsLbl.Text = "Attach file<br>";
                        }
                        copiesCertifications.Value = oReader["copiesCertifications"].ToString();




                        if (oReader["tableOrganization"].ToString() != "")
                        {
                            string[] tableOrganizations1 = oReader["tableOrganization"].ToString().Split(';');
                            foreach (string tableOrganization1 in tableOrganizations1)
                            {
                                tableOrganizationLbl.Text = tableOrganization1.Trim() != "" ? tableOrganizationLbl.Text + "<div><a href='" + tableOrganization1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"tableOrganizationx\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_tableOrganization\').val(),\'" + tableOrganization1.Trim() + "\',\'tableOrganization\', \'tblVendorInformation\', \'tableOrganization\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            tableOrganizationLbl.Text = "Attach file<br>";
                        }
                        tableOrganization.Value = oReader["tableOrganization"].ToString();




                        if (oReader["CertAndWarranty_AttachedSigned"].ToString() != "")
                        {
                            string[] CertAndWarranty_AttachedSigneds1 = oReader["CertAndWarranty_AttachedSigned"].ToString().Split(';');
                            foreach (string CertAndWarranty_AttachedSigned1 in CertAndWarranty_AttachedSigneds1)
                            {
                                CertAndWarranty_AttachedSignedLbl.Text = CertAndWarranty_AttachedSigned1.Trim() != "" ? CertAndWarranty_AttachedSignedLbl.Text + "<div><a href='" + CertAndWarranty_AttachedSigned1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"CertAndWarranty_AttachedSignedx\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_CertAndWarranty_AttachedSigned\').val(),\'" + CertAndWarranty_AttachedSigned1.Trim() + "\',\'CertAndWarranty_AttachedSigned\', \'tblVendorInformation\', \'CertAndWarranty_AttachedSigned\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            CertAndWarranty_AttachedSignedLbl.Text = "Attach Proof of Payment / Secretary Certificate<br>";
                        }
                        CertAndWarranty_AttachedSigned.Value = oReader["CertAndWarranty_AttachedSigned"].ToString();




                        if (oReader["projectsForContractors"].ToString() != "")
                        {
                            string[] projectsForContractorss1 = oReader["projectsForContractors"].ToString().Split(';');
                            foreach (string projectsForContractors1 in projectsForContractorss1)
                            {
                                projectsForContractorsLbl.Text = projectsForContractors1.Trim() != "" ? projectsForContractorsLbl.Text + "<div><a href='" + projectsForContractors1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"projectsForContractorsx\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_projectsForContractors\').val(),\'" + projectsForContractors1.Trim() + "\',\'projectsForContractors\', \'tblVendorInformation\', \'projectsForContractors\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            projectsForContractorsLbl.Text = " Attach file<br>";
                        }
                        projectsForContractors.Value = oReader["projectsForContractors"].ToString();




                        if (oReader["regOwnedAttachment"].ToString() != "")
                        {
                            string[] regOwnedAttachments1 = oReader["regOwnedAttachment"].ToString().Split(';');
                            foreach (string regOwnedAttachment1 in regOwnedAttachments1)
                            {
                                regOwnedAttachmentLbl.Text = regOwnedAttachment1.Trim() != "" ? regOwnedAttachmentLbl.Text + "<div><a href='" + regOwnedAttachment1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"regOwnedAttachmentx\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_regOwnedAttachment\').val(),\'" + regOwnedAttachment1.Trim() + "\',\'regOwnedAttachment\', \'tblVendorInformation\', \'regOwnedAttachment\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            regOwnedAttachmentLbl.Text = "Attach file<br>";
                        }
                        regOwnedAttachment.Value = oReader["regOwnedAttachment"].ToString();


                        
                        if (oReader["benefitsPagibigFileName"].ToString() != "")
                        {
                            string[] benefitsPagibigFileNames1 = oReader["benefitsPagibigFileName"].ToString().Split(';');
                            foreach (string benefitsPagibigFileName1 in benefitsPagibigFileNames1)
                            {
                                //benefitsPagibigFileNameLbl.Text = benefitsPagibigFileName1.Trim() != "" ? benefitsPagibigFileNameLbl.Text + "<div><a href='" + benefitsPagibigFileName1.Trim() + "' target='_blank'>PAG-IBIG Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"benefitsPagibigFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_benefitsPagibigFileName\').val(),\'" + benefitsPagibigFileName1.Trim() + "\',\'benefitsPagibigFileName\');\" /><br></div>" : "";
                                benefitsPagibigFileNameLbl.Text = benefitsPagibigFileName1.Trim() != "" ? benefitsPagibigFileNameLbl.Text + "<div><a href='" + benefitsPagibigFileName1.Trim() + "' target='_blank'>PAG-IBIG Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"benefitsPagibigFileNamex\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_benefitsPagibigFileName\').val(),\'" + benefitsPagibigFileName1.Trim() + "\',\'benefitsPagibigFileName\', \'tblVendorInformation\', \'benefitsPagibigFileName\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            benefitsPagibigFileNameLbl.Text = "PAG-IBIG Attach file<br>";
                        }
                        benefitsPagibigFileName.Value = oReader["benefitsPagibigFileName"].ToString();
                        //benefitsPHICFileNameLbl.Text = oReader["benefitsPHICFileName"].ToString() !="" ? "<a href='" + oReader["benefitsPHICFileName"].ToString() + "' target='_blank'>" + oReader["benefitsPHICFileName"].ToString() + "</a>" : "Attach file";
                        if (oReader["benefitsPHICFileName"].ToString() != "")
                        {
                            string[] benefitsPHICFileNames1 = oReader["benefitsPHICFileName"].ToString().Split(';');
                            foreach (string benefitsPHICFileName1 in benefitsPHICFileNames1)
                            {
                                //benefitsPHICFileNameLbl.Text = benefitsPHICFileName1.Trim() != "" ? benefitsPHICFileNameLbl.Text + "<div><a href='" + benefitsPHICFileName1.Trim() + "' target='_blank'>PHILHEALTH Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"benefitsPHICFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_benefitsPHICFileName\').val(),\'" + benefitsPHICFileName1.Trim() + "\',\'benefitsPHICFileName\');\" /><br></div>" : "";
                                benefitsPHICFileNameLbl.Text = benefitsPHICFileName1.Trim() != "" ? benefitsPHICFileNameLbl.Text + "<div><a href='" + benefitsPHICFileName1.Trim() + "' target='_blank'>PHILHEALTH Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"benefitsPHICFileNamex\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_benefitsPHICFileName\').val(),\'" + benefitsPHICFileName1.Trim() + "\',\'benefitsPHICFileName\', \'tblVendorInformation\', \'benefitsPHICFileName\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            benefitsPHICFileNameLbl.Text = "PHILHEALTH Attach file<br>";
                        }
                        benefitsPHICFileName.Value = oReader["benefitsPHICFileName"].ToString();
                        //benefitsSSSFileNameLbl.Text = oReader["benefitsSSSFileName"].ToString()!="" ? "<a href='" + oReader["benefitsSSSFileName"].ToString() + "' target='_blank'>" + oReader["benefitsSSSFileName"].ToString() + "</a>" : "Attach file";
                        if (oReader["benefitsSSSFileName"].ToString() != "")
                        {
                            string[] benefitsSSSFileNames1 = oReader["benefitsSSSFileName"].ToString().Split(';');
                            foreach (string benefitsSSSFileName1 in benefitsSSSFileNames1)
                            {
                                //benefitsSSSFileNameLbl.Text = benefitsSSSFileName1.Trim() != "" ? benefitsSSSFileNameLbl.Text + "<div><a href='" + benefitsSSSFileName1.Trim() + "' target='_blank'>SSS Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"benefitsSSSFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_benefitsSSSFileName\').val(),\'" + benefitsSSSFileName1.Trim() + "\',\'benefitsSSSFileName\');\" /><br></div>" : "";
                                benefitsSSSFileNameLbl.Text = benefitsSSSFileName1.Trim() != "" ? benefitsSSSFileNameLbl.Text + "<div><a href='" + benefitsSSSFileName1.Trim() + "' target='_blank'>SSS Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"benefitsSSSFileNamex\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_benefitsPHICFileName\').val(),\'" + benefitsSSSFileName1.Trim() + "\',\'benefitsSSSFileName\', \'tblVendorInformation\', \'benefitsSSSFileName\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            benefitsSSSFileNameLbl.Text = "SSS Attach file<br>";
                        }
                        benefitsSSSFileName.Value = oReader["benefitsSSSFileName"].ToString();

                        if (oReader["benefitsOthersFileName"].ToString() != "")
                        {
                            string[] benefitsOthersFileNames1 = oReader["benefitsOthersFileName"].ToString().Split(';');
                            foreach (string benefitsOthersFileName1 in benefitsOthersFileNames1)
                            {
                                //benefitsOthersFileNameLbl.Text = benefitsOthersFileName1.Trim() != "" ? benefitsOthersFileNameLbl.Text + "<div><a href='" + benefitsOthersFileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"benefitsOthersFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_benefitsOthersFileName\').val(),\'" + benefitsOthersFileName1.Trim() + "\',\'benefitsOthersFileName\');\" /><br></div>" : "";
                                benefitsOthersFileNameLbl.Text = benefitsOthersFileName1.Trim() != "" ? benefitsOthersFileNameLbl.Text + "<div><a href='" + benefitsOthersFileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"benefitsOthersFileNamex\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_benefitsOthersFileName\').val(),\'" + benefitsOthersFileName1.Trim() + "\',\'benefitsOthersFileName\', \'tblVendorInformation\', \'benefitsOthersFileName\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            benefitsOthersFileNameLbl.Text = "Attach file<br>";
                        }
                        benefitsOthersFileName.Value = oReader["benefitsOthersFileName"].ToString();


                        if (oReader["assetsMachineriesFileName"].ToString() != "")
                        {
                            string[] assetsMachineriesFileNames1 = oReader["assetsMachineriesFileName"].ToString().Split(';');
                            foreach (string assetsMachineriesFileName1 in assetsMachineriesFileNames1)
                            {
                                //assetsMachineriesFileNameLbl.Text = assetsMachineriesFileName1.Trim() != "" ? assetsMachineriesFileNameLbl.Text + "<div><a href='" + assetsMachineriesFileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"assetsMachineriesFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_assetsMachineriesFileName\').val(),\'" + assetsMachineriesFileName1.Trim() + "\',\'assetsMachineriesFileName\');\" /><br></div>" : "";
                                assetsMachineriesFileNameLbl.Text = assetsMachineriesFileName1.Trim() != "" ? assetsMachineriesFileNameLbl.Text + "<div><a href='" + assetsMachineriesFileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"assetsMachineriesFileNamex\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_assetsMachineriesFileName\').val(),\'" + assetsMachineriesFileName1.Trim() + "\',\'assetsMachineriesFileName\', \'tblVendorInformation\', \'assetsMachineriesFileName\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            assetsMachineriesFileNameLbl.Text = "Attach file<br>";
                        }
                        assetsMachineriesFileName.Value = oReader["assetsMachineriesFileName"].ToString();
                        if (oReader["assetsCompanyProfileFileName"].ToString() != "")
                        {
                            string[] assetsCompanyProfileFileNames1 = oReader["assetsCompanyProfileFileName"].ToString().Split(';');
                            foreach (string assetsCompanyProfileFileName1 in assetsCompanyProfileFileNames1)
                            {
                                //assetsCompanyProfileFileNameLbl.Text = assetsCompanyProfileFileName1.Trim() != "" ? assetsCompanyProfileFileNameLbl.Text + "<div><a href='" + assetsCompanyProfileFileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"assetsCompanyProfileFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_assetsCompanyProfileFileName\').val(),\'" + assetsCompanyProfileFileName1.Trim() + "\',\'assetsCompanyProfileFileName\');\" /><br></div>" : "";
                                assetsCompanyProfileFileNameLbl.Text = assetsCompanyProfileFileName1.Trim() != "" ? assetsCompanyProfileFileNameLbl.Text + "<div><a href='" + assetsCompanyProfileFileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"assetsCompanyProfileFileNamex\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_assetsCompanyProfileFileName\').val(),\'" + assetsCompanyProfileFileName1.Trim() + "\',\'assetsCompanyProfileFileName\', \'tblVendorInformation\', \'assetsCompanyProfileFileName\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            assetsCompanyProfileFileNameLbl.Text = "Attach file<br>";
                        }
                        assetsCompanyProfileFileName.Value = oReader["assetsCompanyProfileFileName"].ToString();

                        if (oReader["assetsOthersFileName"].ToString() != "")
                        {
                            string[] assetsOthersFileNames1 = oReader["assetsOthersFileName"].ToString().Split(';');
                            foreach (string assetsOthersFileName1 in assetsOthersFileNames1)
                            {
                                //assetsOthersFileNameLbl.Text = assetsOthersFileName1.Trim() != "" ? assetsOthersFileNameLbl.Text + "<div><a href='" + assetsOthersFileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"assetsOthersFileNamex\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_assetsOthersFileName\').val(),\'" + assetsOthersFileName1.Trim() + "\',\'assetsOthersFileName\');\" /><br></div>" : "";
                                assetsOthersFileNameLbl.Text = assetsOthersFileName1.Trim() != "" ? assetsOthersFileNameLbl.Text + "<div><a href='" + assetsOthersFileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"assetsOthersFileNamex\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_assetsOthersFileName\').val(),\'" + assetsOthersFileName1.Trim() + "\',\'assetsOthersFileName\', \'tblVendorInformation\', \'assetsOthersFileName\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            assetsOthersFileNameLbl.Text = "Attach file<br>";
                        }
                        assetsOthersFileName.Value = oReader["assetsOthersFileName"].ToString();



                        legalStrucCorpAttch_geninfoLbl.Text = oReader["legalStrucCorpAttch_geninfo"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_geninfo"].ToString() + "' target='_blank'>General Information Sheet</a>" : "General Information Sheet";
                        legalStrucCorpAttch_geninfo.Value = oReader["legalStrucCorpAttch_geninfo"].ToString();

                        legalStrucCorpAttch_geninfo2Lbl.Text = oReader["legalStrucCorpAttch_geninfo2"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_geninfo2"].ToString() + "' target='_blank'>SEC Certificate</a>" : "SEC Certificate";
                        legalStrucCorpAttch_geninfo2.Value = oReader["legalStrucCorpAttch_geninfo2"].ToString();

                        legalStrucCorpAttch_geninfo3Lbl.Text = oReader["legalStrucCorpAttch_geninfo3"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_geninfo3"].ToString() + "' target='_blank'>Articles of Incorporation and By Laws</a>" : "Articles of Incorporation and By Laws";
                        legalStrucCorpAttch_geninfo3.Value = oReader["legalStrucCorpAttch_geninfo3"].ToString();

                        legalStrucCorpAttch_geninfo4Lbl.Text = oReader["legalStrucCorpAttch_geninfo4"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_geninfo4"].ToString() + "' target='_blank'>Others</a>" : "Others";
                        legalStrucCorpAttch_geninfo4.Value = oReader["legalStrucCorpAttch_geninfo4"].ToString();

                        legalStrucCorpAttch_IdentityAuthorizdSignaLbl.Text = oReader["legalStrucCorpAttch_IdentityAuthorizdSigna"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_IdentityAuthorizdSigna"].ToString() + "' target='_blank'>Authorized signatories - Any government issued ID with picture</a>" : "Authorized signatories - Any government issued ID with picture";
                        legalStrucCorpAttch_IdentityAuthorizdSigna.Value = oReader["legalStrucCorpAttch_IdentityAuthorizdSigna"].ToString();

                        legalStrucCorpAttch_IdentitytaxcertLbl.Text = oReader["legalStrucCorpAttch_Identitytaxcert"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_Identitytaxcert"].ToString() + "' target='_blank'>Company – Community Tax Certificate</a>" : "Company – Community Tax Certificate";
                        legalStrucCorpAttch_Identitytaxcert.Value = oReader["legalStrucCorpAttch_Identitytaxcert"].ToString();

                        legalStrucCorpAttch_BoardAuthorizdSignaLbl.Text = oReader["legalStrucCorpAttch_BoardAuthorizdSigna"].ToString() != "" ? "<a href='" + oReader["legalStrucCorpAttch_BoardAuthorizdSigna"].ToString() + "' target='_blank'>Board Resolution / Secretary certificate of authorized signatories</a>" : "Board Resolution / Secretary certificate of authorized signatories";
                        legalStrucCorpAttch_BoardAuthorizdSigna.Value = oReader["legalStrucCorpAttch_BoardAuthorizdSigna"].ToString();

                        legalStrucSoleAttch_DTIRegLbl.Text = oReader["legalStrucSoleAttch_DTIReg"].ToString() != "" ? "<a href='" + oReader["legalStrucSoleAttch_DTIReg"].ToString() + "' target='_blank'>DTI Registration – attach letter of certification from Proprietor stating registered / authorized capital</a>" : "DTI Registration – attach letter of certification from Proprietor stating registered / authorized capital";
                        legalStrucSoleAttch_DTIReg.Value = oReader["legalStrucSoleAttch_DTIReg"].ToString();

                        legalStrucSoleAttch_OwnersId1Lbl.Text = oReader["legalStrucSoleAttch_OwnersId1"].ToString() != "" ? "<a href='" + oReader["legalStrucSoleAttch_OwnersId1"].ToString() + "' target='_blank'>Owner’s Identification ** valid IDs include: Driver’s License, Passport or NBI Clearance</a>" : "Owner’s Identification ** valid IDs include: Driver’s License, Passport or NBI Clearance";
                        legalStrucSoleAttch_OwnersId1.Value = oReader["legalStrucSoleAttch_OwnersId1"].ToString();

                        legalStrucSoleAttch_CTCLbl.Text = oReader["legalStrucSoleAttch_CTC"].ToString() != "" ? "<a href='" + oReader["legalStrucSoleAttch_CTC"].ToString() + "' target='_blank'>Community Tax Certificate of the owner (CTC)</a>" : "Community Tax Certificate of the owner (CTC)";
                        legalStrucSoleAttch_CTC.Value = oReader["legalStrucSoleAttch_CTC"].ToString();


                        fileuploaded2.Text = oReader["busPermitAttachement"].ToString() != "" ? "<a href='" + oReader["busPermitAttachement"].ToString() + "' target='_blank'>Attach file</a>" : "Attach file";
                        busPermitAttachement.Value = oReader["busPermitAttachement"].ToString();

                        fileuploaded3.Text = oReader["birRegAttachement"].ToString() != "" ? "<a href='" + oReader["birRegAttachement"].ToString() + "' target='_blank'>Attached file</a>" : "Attach file";
                        birRegAttachement.Value = oReader["birRegAttachement"].ToString();
                    }
                }
            }
        }



        sCommand = "SELECT * FROM tblVendorFinancialInformation WHERE VendorId= " + VendorId.ToString() + " AND Year = '1'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            //yr1YearInfo_Lbl.Text = oReader["YearInfo"].ToString();
            //yr1Revenue_Lbl.Text = oReader["Revenue"].ToString();
            //yr1NetIncome_Lbl.Text = oReader["NetIncome"].ToString();
            //yr1TotalAssets_Lbl.Text = oReader["TotalAssets"].ToString();
            //yr1TotalLiabilities_Lbl.Text = oReader["TotalLiabilities"].ToString();
            //yr1CurrentAssets_Lbl.Text = oReader["CurrentAssets"].ToString();
            //yr1CurrentLiabilities_Lbl.Text = oReader["CurrentLiabilities"].ToString();
            //yr1FileName_Lbl.Text = oReader["FileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["FileName"].ToString() + "' target='_blank'> Attached file</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
            //yr1FileNameLbl_Lbl.Text = oReader["FileName"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["FileName"].ToString() + "' target='_blank'> Attached file</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file";
            if (oReader["FileName"].ToString() != "")
            {
                string[] yr1FileNames1 = oReader["FileName"].ToString().Split(';');
                foreach (string yr1FileName1 in yr1FileNames1)
                {
                    //yr1FileNameLbl.Text = yr1FileName1.Trim() != "" ? yr1FileNameLbl.Text + "<div><img src=\"images/attachment.png\" /> <a href='" + yr1FileName1.Trim() + "' target='_blank'>Attached file</a><br></div>" : "";
                    yr1FileNameLbl.Text = yr1FileName1.Trim() != "" ? yr1FileNameLbl.Text + "<div><a href='" + yr1FileName1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"yr1FileNamex\" onclick=\"if(confirm(\'Are you sure to delete this attachment?\')){ $(this).parent(\'div\').html(\'\'); FileattchValues0($(\'#ContentPlaceHolder1_yr1FileName\').val(),\'" + yr1FileName1.Trim() + "\',\'yr1FileName\', \'tblVendorFinancialInformation\', \'FileName\', \'" + VendorId + "\'); }\" /><br></div>" : "";
                }
            }
            else
            {
                yr1FileNameLbl.Text = " Attach file<br>";
            }
        }
        oReader.Close();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        
    }


    

}
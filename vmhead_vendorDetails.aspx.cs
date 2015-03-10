using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;
using System.Text;
using System.Text.RegularExpressions;

public partial class vmhead_vendorDetails : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    //int numRowsTbl;

    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //TestShowAllSessions();
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != "16") Response.Redirect("login.aspx");
        if (IsPostBack)
        {

            if (Request["__EVENTTARGET"].ToString() == "Details")
            {
                ClarifyToDnb();
            }
            else
            {
                SaveToDB();
            }
        }
        PopulateFields();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        
    }


    void PopulateFields()
    {
        query = "SELECT * FROM tblVendor WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //Response.Write(oReader["Status"].ToString());
                        if (oReader["Status"].ToString() == "4") { Response.Redirect("vmhead_vendorDetails_View.aspx"); }
                        else if (oReader["Status"].ToString() == "7") 
                        {
                            createBt.Text = "<span>APPROVE &raquo;</span>";
                        }
                    }
                }
            }
        }

        query = "SELECT * FROM tblVendorApprovalbyVmReco WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //Recommendation.Enabled = false;
                        if (oReader["FileAttachement"].ToString() != "")
                        {
                            string[] FileAttachements1 = oReader["FileAttachement"].ToString().Split(';');
                            foreach (string FileAttachement1 in FileAttachements1)
                            {
                                FileAttachementLbl.Text = FileAttachement1.Trim() != "" ? FileAttachementLbl.Text + "<div><a href='" + FileAttachement1.Trim() + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"FileAttachementx\" onclick=\"$(this).parent(\'div\').html(\'\');FileattchValues($(\'#ContentPlaceHolder1_FileAttachement\').val(),\'" + FileAttachement1.Trim() + "\',\'FileAttachement\');\" /><br></div>" : "";
                            }
                        }
                        else
                        {
                            FileAttachementLbl.Text = "Attach file<br>";
                        }
                        FileAttachement.Value = oReader["FileAttachement"].ToString();
                        //FileAttachementLbl.Text = "<a href='" + oReader["FileAttachement"].ToString() + "' target='_blank'>" + oReader["FileAttachement"].ToString() + "</a>";
                        AccreDuration.SelectedValue = oReader["AccreDuration"].ToString();
                        //Others.InnerText = oReader["Others"].ToString();
                        OverallEvalRemarks.InnerText = oReader["OverallEvalRemarks"].ToString();
                    }
                }
            }
        }

        query = "SELECT t2.CompanyName, t1.* FROM tblDnbReport t1, tblVendor t2 WHERE t1.VendorId = @VendorId AND t2.VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"].ToString()));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        
                        int odnbScore, ovmoGTPerf_Eval;
                        CompanyNameLbl.Text = oReader["CompanyName"].ToString();
                        dnbDuns.Text = oReader["dnbDuns"].ToString();
                        dnbFinCapScore.Text = oReader["dnbFinCapScore"].ToString();
                        dnbFinCapScore_Remarks.Text = oReader["dnbFinCapScore_Remarks"].ToString().Replace("\n", "<br>");
                        dnbLegalConfScore.Text = oReader["dnbLegalConfScore"].ToString();
                        dnbLegalConfScore_Remarks.Text = oReader["dnbLegalConfScore_Remarks"].ToString().Replace("\n", "<br>");
                        dnbTechCompScore.Text = oReader["dnbTechCompScore"].ToString();
                        dnbTechCompScore_Remarks.Text = oReader["dnbTechCompScore_Remarks"].ToString().Replace("\n", "<br>");
                        dnbMaxExposureLimit.Text = oReader["dnbMaxExposureLimit"].ToString();
                        vmoNo_POs.Text = oReader["vmoNo_POs"].ToString();
                        vmoSpend.Text = oReader["vmoSpend"].ToString();
                        vmoWith_Existing_Frame_Arg.SelectedValue = oReader["vmoWith_Existing_Frame_Arg"].ToString() == "0" ? "0" : "1";
                        vmoIssues_bond_claims.Checked = oReader["vmoIssues_bond_claims"].ToString() == "1" ? true : false;
                        vmoIssue_risk_to_Globe.Checked = oReader["vmoIssue_risk_to_Globe"].ToString() == "1" ? true : false;
                        vmoConflict_of_Interest.Checked = oReader["vmoConflict_of_Interest"].ToString() == "1" ? true : false;
                        vmoWith_Type_Approved_Products.Checked = oReader["vmoWith_Type_Approved_Products"].ToString() == "1" ? true : false;
                        vmoWith_Approved_Proof_of_Concept.Checked = oReader["vmoWith_Approved_Proof_of_Concept"].ToString() == "1" ? true : false;
                        vmoGTPerf_Eval.Text = oReader["vmoGTPerf_Eval"].ToString();
                        vmoIssues_ISR_involvement.Checked = oReader["vmoIssues_ISR_involvement"].ToString() == "1" ? true : false;
                        vmoIssues_Loss_Incidents.Checked = oReader["vmoIssues_Loss_Incidents"].ToString() == "1" ? true : false;
                        vmoIssues_Others.Checked = oReader["vmoIssues_Others"].ToString() == "1" ? true : false;
                        vmoIssues_Remarks.InnerText = oReader["vmoIssues_Remarks"].ToString();
                        vmoGTPerf_Eval.Text = oReader["vmoGTPerf_Eval"].ToString();

                        vmoNew_Vendor.SelectedValue = oReader["vmoNew_Vendor"].ToString() == "0" ? "0" : "1";
                        odnbScore = oReader["dnbScore"].ToString() != "" ? Convert.ToInt32(oReader["dnbScore"].ToString()) : Convert.ToInt32(oReader["dnbFinCapScore"].ToString()) + Convert.ToInt32(oReader["dnbLegalConfScore"].ToString()) + Convert.ToInt32(oReader["dnbTechCompScore"].ToString());
                        dnbScore.Text = odnbScore.ToString();
                        vmoOverallScore.Text = oReader["vmoOverallScore"].ToString() != "" ? oReader["vmoOverallScore"].ToString() : "0";
                        //if (oReader["vmoOverallScore"].ToString() == "")
                        //{
                        //    if (oReader["vmoNew_Vendor"].ToString() == "0")
                        //    {
                        //        ovmoGTPerf_Eval = oReader["vmoGTPerf_Eval"].ToString() != "" ? Convert.ToInt32(oReader["vmoGTPerf_Eval"].ToString()) : 0;
                        //        if (oReader["vmoGTPerf_Eval"].ToString() != "" && oReader["vmoGTPerf_Eval"].ToString() != "0")
                        //        {
                        //            vmoOverallScore.Text = ((odnbScore + ovmoGTPerf_Eval) / 2).ToString();
                        //            vmoOverallScore1.Value = ((odnbScore + ovmoGTPerf_Eval) / 2).ToString();
                        //        }
                        //        else
                        //        {
                        //            vmoOverallScore.Text = odnbScore.ToString();
                        //            vmoOverallScore1.Value = odnbScore.ToString();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        vmoOverallScore.Text = odnbScore.ToString();
                        //        vmoOverallScore1.Value = odnbScore.ToString();
                        //    }
                        //}
                        //else
                        //{
                        //    vmoOverallScore.Text = oReader["vmoOverallScore"].ToString() != "" ? oReader["vmoOverallScore"].ToString() : "0";
                        //    vmoOverallScore1.Value = oReader["vmoOverallScore"].ToString() != "" ? oReader["vmoOverallScore"].ToString() : "0";
                        //}
                        dnbSupplierInfoReport.Text = oReader["dnbSupplierInfoReport"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["dnbSupplierInfoReport"].ToString() + "' target='_blank'>" + oReader["dnbSupplierInfoReport"].ToString() + "</a>" : "No attach file";
                        dnbOtherDocumentsLbl.Text = oReader["dnbOtherDocuments"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["dnbOtherDocuments"].ToString() + "' target='_blank'>" + oReader["dnbOtherDocuments"].ToString() + "</a>" : "No attach file";
                    }
                }
            }
        }



        legalStrucOrgType.Text = "n/a";
        query = "SELECT * FROM tblVendorInformation WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        legalStrucOrgType.Text = oReader["legalStrucOrgType"].ToString() != "" ? oReader["legalStrucOrgType"].ToString() : "n/a";
                    }
                }
            }
        }

        string NatureOfBusiness1 = "";
        query = "SELECT t2.NatureOfBusinessName FROM tblVendorNatureOfBusiness t1, rfcNatureOfBusiness t2  WHERE t1.VendorId = @VendorId AND t1.NatureOfBusinessId = t2.NatureOfBusinessId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        NatureOfBusiness1 = NatureOfBusiness1 + oReader["NatureOfBusinessName"].ToString() + ", ";
                    }
                }
            }
        }
        NatureOfBusiness.Text = NatureOfBusiness1 != "" ? NatureOfBusiness1.Substring(0, NatureOfBusiness1.Length - 2) : "n/a";


        string Category1 = "";
        query = "SELECT t2.CategoryName FROM tblVendorProductsAndServices t1, rfcProductCategory t2  WHERE t1.VendorId = @VendorId AND t1.CategoryId = t2.CategoryId GROUP BY t2.CategoryName";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        Category1 = Category1 + oReader["CategoryName"].ToString() + ", ";
                    }
                }
            }
        }
        Category.Text = Category1 != "" ? Category1.Substring(0, Category1.Length - 2) : "n/a";



        query = "SELECT * FROM tblVendor WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        CompanyNameLbl.Text = oReader["CompanyName"].ToString() != "" ? "<a href='vendor_Home.aspx?VendorId=" + Session["VendorId"] + "' target='_blank'>" + oReader["CompanyName"].ToString() + "</a>" : "n/a";
                        //AuthenticationTicketLbl.Text = oReader["AuthenticationTicket"].ToString();
                    }
                }
            }
        }


            query = "SELECT * FROM tblDnbLegalReport WHERE VendorId = @VendorId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    conn.Open();
                    //Process results
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            //.Value = oReader[""].ToString();
                            TypeOfCase.Text = oReader["TypeOfCase"].ToString() != "" ? oReader["TypeOfCase"].ToString() : "n/a";
                            DateFiled.Text = oReader["DateFiled"].ToString() != "1/1/1900 12:00:00 AM" ? oReader["DateFiled"].ToString() : "";
                            fileuploaded_1.Text = oReader["Attachment"].ToString() != "" ? "<a href='" + oReader["Attachment"].ToString() + "' target='_blank'>" + oReader["Attachment"].ToString() + "</a>" : "<h3>n/a</h3>";
                        }
                    }
                }
            }
        
    }


    void SaveToDB()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        if (Comment.Value != "")
        {
            query = "INSERT INTO tblComments (VendorId, UserId, Comment, DateCreated) VALUES (@VendorId, @UserId, @Comment, @DateCreated)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                    cmd.Parameters.AddWithValue("@Comment", Comment.Value.ToString());
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }

            //CLEAR tblVendorApprovalbyVmTech FROM VendorId
            sCommand = "DELETE FROM tblVendorApprovalbyVmReco WHERE VendorId = " + Session["VendorId"].ToString();
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            query = "INSERT INTO tblVendorApprovalbyVmReco (VendorId, vendorApproved, FileAttachement, Recommendation, DateCreated, AccreDuration, OverallEvalRemarks) VALUES (@VendorId, @vendorApproved,@FileAttachement, @Recommendation, @DateCreated, @AccreDuration, @OverallEvalRemarks)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    cmd.Parameters.AddWithValue("@vendorApproved", 1);
                    cmd.Parameters.AddWithValue("@FileAttachement", FileAttachement.Value.ToString());
                    //cmd.Parameters.AddWithValue("@Others", Others.InnerText);
                    cmd.Parameters.AddWithValue("@AccreDuration", AccreDuration.SelectedValue);
                    cmd.Parameters.AddWithValue("@OverallEvalRemarks", OverallEvalRemarks.InnerText);

                    if (Request.Form["__EVENTTARGET"] == "Approve")
                    {
                        cmd.Parameters.AddWithValue("@Recommendation", 1);
                    }
                    else if (Request.Form["__EVENTTARGET"] == "Conditional")
                    {
                        cmd.Parameters.AddWithValue("@Recommendation", 2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Recommendation", 0);
                    }
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }



            sCommand = "UPDATE tblVendor SET Status = 4, approvedbyVMReco = " + Session["UserId"] + ", approvedbyVMRecoDate = getdate() WHERE VendorId = " + Session["VendorId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);


            query = "UPDATE tblDnbReport SET ";
            query = query + "vmoNew_Vendor=@vmoNew_Vendor, ";
            query = query + "vmoOverallScore=@vmoOverallScore, ";
            query = query + "vmoIssue_risk_to_Globe=@vmoIssue_risk_to_Globe, ";
            query = query + "vmoConflict_of_Interest=@vmoConflict_of_Interest, ";
            query = query + "vmoWith_Type_Approved_Products=@vmoWith_Type_Approved_Products, ";
            query = query + "vmoWith_Approved_Proof_of_Concept=@vmoWith_Approved_Proof_of_Concept, ";
            query = query + "vmoGTPerf_Eval=@vmoGTPerf_Eval, ";
            query = query + "vmoNo_POs=@vmoNo_POs, ";
            query = query + "vmoSpend=@vmoSpend, ";
            query = query + "vmoWith_Existing_Frame_Arg=@vmoWith_Existing_Frame_Arg, ";
            query = query + "vmoIssues_bond_claims=@vmoIssues_bond_claims, ";
            query = query + "vmoIssues_ISR_involvement=@vmoIssues_ISR_involvement, ";
            query = query + "vmoIssues_Loss_Incidents=@vmoIssues_Loss_Incidents, ";
            query = query + "vmoIssues_Others=@vmoIssues_Others, ";
            query = query + "vmoIssues_Remarks=@vmoIssues_Remarks ";
            query = query + "WHERE VendorId=@VendorId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@vmoNew_Vendor", Convert.ToInt32(vmoNew_Vendor.SelectedValue));
                    cmd.Parameters.AddWithValue("@vmoOverallScore", vmoOverallScore1.Value != "" ? Convert.ToDecimal(vmoOverallScore1.Value) : 0);
                    cmd.Parameters.AddWithValue("@vmoIssue_risk_to_Globe", vmoIssue_risk_to_Globe.Checked == true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@vmoConflict_of_Interest", vmoConflict_of_Interest.Checked == true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@vmoWith_Type_Approved_Products", vmoWith_Type_Approved_Products.Checked == true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@vmoWith_Approved_Proof_of_Concept", vmoWith_Approved_Proof_of_Concept.Checked == true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@vmoGTPerf_Eval", Convert.ToInt32(vmoGTPerf_Eval.Text));
                    cmd.Parameters.AddWithValue("@vmoNo_POs", vmoNo_POs.Text);
                    cmd.Parameters.AddWithValue("@vmoSpend", vmoSpend.Text);
                    cmd.Parameters.AddWithValue("@vmoWith_Existing_Frame_Arg", vmoWith_Existing_Frame_Arg.SelectedValue == "1" ? 1 : 0);
                    cmd.Parameters.AddWithValue("@vmoIssues_bond_claims", vmoIssues_bond_claims.Checked == true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@vmoIssues_ISR_involvement", vmoIssues_ISR_involvement.Checked == true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@vmoIssues_Loss_Incidents", vmoIssues_Loss_Incidents.Checked == true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@vmoIssues_Others", vmoIssues_Others.Checked == true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@vmoIssues_Remarks", vmoIssues_Remarks.InnerText.Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
 


                // SEND EMAIL NOTIFICATION TO VM HEAD
                string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
                query = "SELECT t3.FirstName + ' ' + t3.LastName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUserTypes t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.UserType = 17 AND t3.UserId = @UserId AND t1.Status = 1 AND t3.Status =1 AND t4.Status = 4 AND t4.VendorId = @VendorId";
                //query = "sp_GetVendorInformation"; //##storedProcedure
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                        cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                        cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                        conn.Open();
                        //Process results
                        oReader = cmd.ExecuteReader();
                        if (oReader.HasRows)
                        {
                            while (oReader.Read())
                            {
                                //fromName = oReader["fromName"].ToString();
                                //fromEmail = oReader["fromEmail"].ToString();
                                fromName = ConfigurationManager.AppSettings["AdminEmailName"].ToString();
                                fromEmail = ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString();
                                toName = oReader["toName"].ToString();
                                toEmail = oReader["toEmail"].ToString();
                                AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                                VendorName = oReader["CompanyName"].ToString();
                                SendEmailNotification(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName);
                            }
                        }
                    }
                }
                // SEND EMAIL NOTIFICATION TO VM RECO ENDS
            //}
        //}

    }



    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO PVMD
    private bool SendEmailNotification(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            //subject = "Globe Automated Vendor Accreditation application reviewed for your approval -- " + sVendorName;
            subject = "Vendor Accreditation: For Approval <" + sVendorName + ">";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBody(sfromName, stoName, sAuthenticationTicket, sVendorName),
                    MailTemplate.GetTemplateLinkedResources(this)))
            {	//if sending failed					
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + from, System.Diagnostics.EventLogEntryType.Error);
            }
            else
            {	//if sending successful
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + from, System.Diagnostics.EventLogEntryType.Information);
            }
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid > Send Notification : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            //Response.Write(ex.ToString());
        }
        return success;
    }

    private string CreateNotificationBody(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName)
    {
        SqlDataReader oReader;
        string cServices = "";
        query = "SELECT t1.*, t2.CategoryName FROM tblVendorProductsAndServices t1, rfcProductCategory t2 WHERE t2.CategoryId = t1.CategoryId AND t1.VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        cServices = cServices + "&bull; " + oReader["CategoryName"].ToString() + "<br>";
                    }
                }
            }
        }

        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='1' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor ID</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + Session["VendorId"] + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Company Name</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cVendorName + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Category</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cServices + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        //sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Good day!<br><br> This is to inform you that application for vendor accreditation has been reviewed for your approval.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");


        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br>");
        sb.Append("To: " + ctoName + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + ctoName + ":<br><br>");
        sb.Append("Re: Request for Vendor Accreditation Approval -- " + cVendorName + "<br><br>");
        sb.Append("This is to request for your Approval of the ff: <br><br>");
        //sb.Append("<a href='http://'<br><br>");
        sb.Append(sTxt);
        sb.Append("</p><br><br>");
        //sb.Append("We are happy to be doing business with you. Thank you and God bless your dealings.<br><br><br>");
        sb.Append("Very truly yours,<br><br>");
        sb.Append("Globe Telecom<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p>");
        sb.Append("<b>Instructions:</b><br>");
        sb.Append("&nbsp;&nbsp;1. Go to <a href='" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "' target='_blank'>" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "</a><br>");
        sb.Append("&nbsp;&nbsp;2. Enter your Username and Password then click Login<br>");
        sb.Append("&nbsp;&nbsp;3. Click Vendors for Approval<br>");
        sb.Append("&nbsp;&nbsp;4. Click View<br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }












    void ClarifyToDnb()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        //Response.Write("Clarification sent " + Request["__EVENTARGUMENT"].ToString().Trim());
        query = "UPDATE tblVendor SET Status=10 WHERE VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"].ToString()));
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }


        query = "INSERT INTO tblCommentsClarifyToDnb (VendorId, UserId, Comment, DateCreated) VALUES (@VendorId, @UserId, @Comment, @DateCreated)";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"].ToString()));
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"].ToString()));
                cmd.Parameters.AddWithValue("@Comment", Request["__EVENTARGUMENT"].ToString().Trim());
                cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }



        string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
        //query = "SELECT 'Globe Admin' as fromName, 'noreply@globetel.com' as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUserTypes t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.UserType = 12 AND t3.UserId = @UserId AND t4.Status = 1 AND t4.VendorId = @VendorId";
        query = "SELECT '" + ConfigurationManager.AppSettings["AdminEmailName"].ToString() + "' as fromName, '" + ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString() + "' as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUserTypes t2, tblVendor t4 WHERE t1.UserId = t2.UserId AND t1.Status = 1 AND t2.UserType = 12 AND t4.Status = 10 AND t4.VendorId = @VendorId";
        //Response.Write(query);
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"].ToString()));
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //fromName = oReader["fromName"].ToString();
                        //fromEmail = oReader["fromEmail"].ToString();
                        fromName = ConfigurationManager.AppSettings["AdminEmailName"].ToString();
                        fromEmail = ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString();
                        toName = oReader["toName"].ToString();
                        toEmail = oReader["toEmail"].ToString();
                        AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                        VendorName = oReader["CompanyName"].ToString();
                        SendEmailNotificationClarify(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName);
                    }
                }
            }
        }

        Response.Redirect("vmhead_VendorList.aspx");
    }




    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO DNB
    private bool SendEmailNotificationClarify(string sfromName, string sfromEmail, string stoName, string stoEmail, string AuthenticationTicket, string sVendorName)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            //subject = "Globe Automated Vendor Accreditation application posted for authentication -- "+sVendorName;
            subject = "Vendor Accreditation: For Clarification - " + sVendorName + "";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyClarify(sfromName, stoName, AuthenticationTicket, sVendorName),
                    MailTemplate.GetTemplateLinkedResources(this)))
            {	//if sending failed					
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + from, System.Diagnostics.EventLogEntryType.Error);
            }
            else
            {	//if sending successful
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + from, System.Diagnostics.EventLogEntryType.Information);
            }
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid > Send Notification : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            //Response.Write(ex.ToString());
        }
        return success;
    }

    private string CreateNotificationBodyClarify(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName)
    {
        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='0' cellpadding='5' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor ID</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + Session["VendorId"] + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Company Name</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cVendorName + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Authentication Ticket</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cAuthenticationNumber + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        //sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Good day!<br><br> This is to inform you that application for vendor accreditation has been posted for your approval.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br>");
        sb.Append("To: " + ctoName + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + ctoName + ":<br><br>");
        sb.Append("Re: Vendor Accreditation SIR for clarification -- " + cVendorName + "<br><br>");
        sb.Append("This is to clarify SIR of the ff: <br><br>");
        //sb.Append("<a href='http://'<br><br>");
        sb.Append(sTxt);
        sb.Append("</p><br><br>");
        //sb.Append("We are happy to be doing business with you. Thank you and God bless your dealings.<br><br><br>");
        sb.Append("Very truly yours,<br><br>");
        sb.Append("Globe Telecom<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p>");
        sb.Append("<b>Instructions:</b><br>");
        sb.Append("&nbsp;&nbsp;1. Go to <a href='" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "' target='_blank'>" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "</a><br>");
        sb.Append("&nbsp;&nbsp;2. Enter your Username and Password then click Login<br>");
        sb.Append("&nbsp;&nbsp;3. Click Endorsed Vendors for Clarification<br>");
        sb.Append("&nbsp;&nbsp;4. Click View Report<br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

}
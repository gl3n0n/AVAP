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

public partial class cfo_vendorDetails : System.Web.UI.Page
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
        if (Session["SESSION_USERTYPE"].ToString() != "18") Response.Redirect("login.aspx");
        if (IsPostBack)
        {
            SaveToDB();
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
                        if (oReader["Status"].ToString() == "6") { Response.Redirect("cfo_vendorDetails_View.aspx"); }
                    }
                }
            }
        }



        //query = "SELECT * FROM tblVendorApprovalbyPVMDHead WHERE VendorId = @VendorId";
        query = "select t1.*, t2.FirstName +' '+t2.LastName as Name from tblVendorApprovalbyPVMDHead t1, tblUsers t2, tblVendor t3 where t2.UserId = t3.approvedbyFAALogistics and t3.VendorId = t1.VendorId and t1.VendorId=@VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        if (oReader["vendorApproved"].ToString() == "1")
                        {
                            recommendation2.Text = "APPROVE";
                        }
                        else if (oReader["vendorApproved"].ToString() == "2")
                        {
                            recommendation2.Text = "CONDITIONALLY APPROVE";
                        }
                        else
                        {
                            recommendation2.Text = "DISAPPROVE";
                        }
                        recoby2.Text = oReader["Name"].ToString();
                        recodate2.Text = oReader["DateCreated"].ToString();
                    }
                }
            }
        }


        //query = "SELECT * FROM tblVendorApprovalbyVmReco WHERE VendorId = @VendorId";
        query = "select t1.*, t2.FirstName +' '+t2.LastName as Name from tblVendorApprovalbyVmReco t1, tblUsers t2, tblVendor t3 where t2.UserId = t3.approvedbyVMReco and t3.VendorId = t1.VendorId and t1.VendorId=@VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        AccreDuration.SelectedValue = oReader["AccreDuration"].ToString();
                        Others.Text = oReader["Others"].ToString();
                        //FileAttachementLbl.Text = oReader["FileAttachement"].ToString() != "" ? "<a href='" + oReader["FileAttachement"].ToString() + "' target='_blank'>" + oReader["FileAttachement"].ToString() + "</a>" : "<h3>n/a</h3>";
                        if (oReader["FileAttachement"].ToString() != "")
                        {
                            FileAttachementLbl.Text = "";
                            string[] FileAttachements1 = oReader["FileAttachement"].ToString().Split(';');
                            foreach (string FileAttachement1 in FileAttachements1)
                            {
                                if (FileAttachement1 != "")
                                {
                                    FileAttachementLbl.Text = FileAttachementLbl.Text + FileAttachement1.Trim() != "" ? FileAttachementLbl.Text + "<a href='" + FileAttachement1.Trim() + "' target='_blank'>Attached file</a><br>" : "";
                                }
                            }
                        }
                        else
                        {
                            FileAttachementLbl.Text = "Attach file<br>";
                        }

                        if (oReader["Recommendation"].ToString() == "1")
                        {
                            recommendation.Text = "APPROVE";
                        }
                        else if (oReader["Recommendation"].ToString() == "2")
                        {
                            recommendation.Text = "CONDITIONALLY APPROVE";
                        }
                        else
                        {
                            recommendation.Text = "DISAPPROVE";
                        }
                        OverallEvalRemarks.Text = oReader["OverallEvalRemarks"].ToString().Replace("\n", "<br>");
                        recodate.Text = oReader["DateCreated"].ToString();
                        recoby.Text = oReader["Name"].ToString();
                    }
                }
            }
        }


        query = "SELECT t2.CompanyName, t1.* FROM tblDnbReport t1, tblVendor t2 WHERE t1.VendorId = @VendorId AND t2.VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"].ToString()));
                conn.Open(); oReader = cmd.ExecuteReader();
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
                        vmoIssues_Remarks.Text = oReader["vmoIssues_Remarks"].ToString();
                        vmoGTPerf_Eval.Text = oReader["vmoGTPerf_Eval"].ToString();

                        vmoNew_Vendor.SelectedValue = oReader["vmoNew_Vendor"].ToString() == "0" ? "0" : "1";
                        odnbScore = oReader["dnbScore"].ToString() != "" ? Convert.ToInt32(oReader["dnbScore"].ToString()) : Convert.ToInt32(oReader["dnbFinCapScore"].ToString()) + Convert.ToInt32(oReader["dnbLegalConfScore"].ToString()) + Convert.ToInt32(oReader["dnbTechCompScore"].ToString());
                        dnbScore.Text = odnbScore.ToString();
                        vmoOverallScore.Text = oReader["vmoOverallScore"].ToString() != "" ? oReader["vmoOverallScore"].ToString() : "0";
                        //if (oReader["vmoNew_Vendor"].ToString() == "0")
                        //{
                        //    ovmoGTPerf_Eval = oReader["vmoGTPerf_Eval"].ToString() != "" ? Convert.ToInt32(oReader["vmoGTPerf_Eval"].ToString()) : 0;
                        //    vmoOverallScore.Text = ((odnbScore + ovmoGTPerf_Eval) / 2).ToString();
                        //}
                        //else
                        //{
                        //    vmoOverallScore.Text = odnbScore.ToString();
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
                            //DateFiled.Text = oReader["DateFiled"].ToString() != "" ? oReader["DateFiled"].ToString() : "n/a";
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



        if (Request.Form["__EVENTTARGET"] == "Approve")
        {
            //string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
            //string sCommand = "";

            //CLEAR tblVendorApprovalbyVmTech FROM VendorId
            sCommand = "DELETE FROM tblVendorApprovalbyFAALFinance WHERE VendorId = " + Session["VendorId"];
            query = "INSERT INTO tblVendorApprovalbyFAALFinance (VendorId, vendorApproved, DateCreated) VALUES (@VendorId, @vendorApproved, @DateCreated)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    cmd.Parameters.AddWithValue("@vendorApproved", 1);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }


            //CLEAR tblVendorApprovalbyVmTech FROM VendorId
            sCommand = "UPDATE tblVendor SET approvedbyFAAFinance = " + Session["UserId"] + ", approvedbyFAAFinanceDate = getdate(), Status = 6 WHERE VendorId = " + Session["VendorId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);


            // update vendor renewaldate
            sCommand = @"
                        UPDATE t1 
                        SET t1.renewaldate = 
	                        CASE 
		                        WHEN t2.AccreDuration = '2 years' THEN DATEADD(month, 22, t1.approvedbyFAALogisticsDate)
		                        WHEN t2.AccreDuration = '1 year' THEN DATEADD(month, 10, t1.approvedbyFAALogisticsDate)
		                        WHEN t2.AccreDuration = '6 months' THEN DATEADD(month, 4, t1.approvedbyFAALogisticsDate)
		                        ELSE NULL
	                        END
                        FROM tblVendor t1		
                        INNER JOIN tblVendorApprovalbyVmReco t2 ON t2.VendorId = t1.VendorId
                        WHERE t1.VendorId = " + Session["VendorId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);





            // SEND EMAIL NOTIFICATION TO VENDOR
            query = "SELECT t3.FirstName + ' ' + t3.LastName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUsersForVendors t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.VendorId = @VendorId AND t3.UserId = @UserId AND t4.Status = 6 AND t4.VendorId = @VendorId";
            string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
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
                            fromName = oReader["fromName"].ToString();
                            fromEmail = oReader["fromEmail"].ToString();
                            toName = oReader["toName"].ToString();
                            toEmail = oReader["toEmail"].ToString();
                            AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                            VendorName = oReader["CompanyName"].ToString();
                            //SendEmailNotification(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName);
                        }
                    }
                }
            }
            // SEND EMAIL NOTIFICATION TO VM VENDOR ENDS

            errNotification.Text = "Vendor has been accredited.";
            errNotification.ForeColor = Color.Blue;
            createBt.Visible = false;
            //createBt1.Visible = false;
            Session["PrevUrl"] = "cfo_VendorList.aspx";
            Response.Redirect("cfo_vendorDetails_View.aspx");
        }
        else if (Request.Form["__EVENTTARGET"] == "clarifyThis")
        {
            //string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
            //string sCommand = "";
            //CLEAR tblVendorApprovalbyVmTech FROM VendorId
            sCommand = "UPDATE tblVendor SET clarifiedtoVMRecoBy = " + Session["UserId"] + ", clarifiedtoVMRecoDate = getdate(), Status = 7 WHERE VendorId = " + Session["VendorId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            // SEND EMAIL NOTIFICATION TO VM RECO
            query = "SELECT t3.FirstName + ' ' + t3.LastName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUserTypes t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.UserType = 16 AND t3.UserId = @UserId AND t4.Status = 7 AND t4.VendorId = @VendorId";
            string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
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
                            fromName = oReader["fromName"].ToString();
                            fromEmail = oReader["fromEmail"].ToString();
                            toName = oReader["toName"].ToString();
                            toEmail = oReader["toEmail"].ToString();
                            AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                            VendorName = oReader["CompanyName"].ToString();
                            SendEmailNotificationClarify(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName);
                        }
                    }
                }
            }
            // SEND EMAIL NOTIFICATION TO VM RECO ENDS

            errNotification.Text = "Vendor clarification has been endorsed.";
            errNotification.ForeColor = Color.Blue;
            createBt.Visible = false;
            //createBt1.Visible = false;
            Session["PrevUrl"] = "cfo_VendorList.aspx";
            Response.Redirect("cfo_VendorList.aspx");
        }
        else if (Request.Form["__EVENTTARGET"] == "Disapprove")
        {
            //string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
            //string sCommand = "";
            //CLEAR tblVendorApprovalbyVmTech FROM VendorId
            sCommand = "UPDATE tblVendor SET Status = 8 WHERE VendorId = " + Session["VendorId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            // SEND EMAIL NOTIFICATION TO VENDOR
            query = "SELECT 'Globe Admin' as fromName, 'noreply@globetel.com.ph' as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUsersForVendors t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.VendorId = @VendorId AND t3.UserId = @UserId AND t4.Status = 8 AND t4.VendorId = @VendorId";
            string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
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
                            fromName = oReader["fromName"].ToString();
                            fromEmail = oReader["fromEmail"].ToString();
                            toName = oReader["toName"].ToString();
                            toEmail = oReader["toEmail"].ToString();
                            AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                            VendorName = oReader["CompanyName"].ToString();
                            //SendEmailNotificationReject(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName);
                        }
                    }
                }
            }
            // SEND EMAIL NOTIFICATION TO VM RECO ENDS

            errNotification.Text = "Vendor clarification has been endorsed.";
            errNotification.ForeColor = Color.Blue;
            createBt.Visible = false;
            //createBt1.Visible = false;
            Session["PrevUrl"] = "cfo_VendorList.aspx";
            Response.Redirect("cfo_VendorList.aspx");
        }

    }



    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR IF APPROVED
    private bool SendEmailNotification(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            subject = "Globe Automated Vendor Accreditation application approved.";
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
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string cCeo = "", cCeoEmail = "", cAddress = "", cServices = "", cAccreDuration = "";
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
                        cCeo = oReader["conBidName"].ToString();
                        cCeoEmail = oReader["conBidEmail"].ToString();
                        cAddress = oReader["regBldgCode"].ToString() != "" ? cAddress + "Bldg. " + oReader["regBldgCode"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgRoom"].ToString() != "" ? cAddress + "Rm. " + oReader["regBldgRoom"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgFloor"].ToString() != "" ? cAddress + oReader["regBldgFloor"].ToString() + " Fr, " : cAddress + "";
                        cAddress = oReader["regBldgHouseNo"].ToString() != "" ? cAddress + "No. " + oReader["regBldgHouseNo"].ToString() + " " : cAddress + "";
                        cAddress = oReader["regStreetName"].ToString() != "" ? cAddress + oReader["regStreetName"].ToString() + ", " : cAddress + "";
                        cAddress = cAddress + "<br>";
                        cAddress = oReader["regCity"].ToString() != "" ? cAddress + oReader["regCity"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regProvince"].ToString() != "" ? cAddress + oReader["regProvince"].ToString() + ", " : cAddress + "";
                        cAddress = cAddress + "<br>";
                        cAddress = oReader["regCountry"].ToString() != "" ? cAddress + oReader["regCountry"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regPostal"].ToString() != "" ? cAddress + oReader["regPostal"].ToString() + " " : cAddress + "";
                    }
                }
            }
        }

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

        query = "SELECT * FROM tblVendorApprovalbyVmReco  WHERE VendorId = @VendorId";
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
                        cAccreDuration = oReader["AccreDuration"].ToString();
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
        //sTxt = sTxt + "<tr>";
        //sTxt = sTxt + "<td><strong>&nbsp;Authentication Ticket</strong></td>";
        //sTxt = sTxt + "<td>&nbsp;" + cAuthenticationNumber + "&nbsp;</td>";
        //sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        //sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Congratulations!<br><br> This is to inform you that application for vendor accreditation has been approved.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Date: " + DateTime.Now.ToLongDateString() + "<br><br>");
        sb.Append(cCeo + "<br>");
        sb.Append("<b>" + cVendorName + "</b><br>");
        sb.Append(cAddress + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + cCeo + ":<br><br>");
        sb.Append("We are pleased to inform you that Globe Telecom Vendor Management has approved your accreditation for the following:<br><br>");
        sb.Append("<b>PRODUCTS/SERVICES OFFERED   :  </b><br>");
        sb.Append(cServices+"<br><br><br>");
        sb.Append("Please inform us immediately should there be any change in your products and services, organization, management, ownership, contact numbers, address and other material information which will affect our business relationship.<br><br>");
        sb.Append("This certification is valid for " + cAccreDuration + " from date of issuance. Globe reserves the right to invite and select suppliers, determine allocation and volume of orders in accordance with the company's procurement policies and objectives. Globe is also not committed to place an order upon accreditation. <br><br>");
        sb.Append("Your continuing status as an accredited supplier of Globe Telecom depends on your performance as a vendor/service provider subject to regular review and your compliance to company requirements. Consequently, Globe reserves the right to invite and require you to undergo an accreditation renewal process 60 days prior to the lapse of the " + cAccreDuration + " period.<br><br>");
        sb.Append("As part of Globe's program for good governance, we would like to take this opportunity to remind you, our business partner, of Globe's Gifts and Inducement Policy.  Our policy strongly prohibits Globe employees from soliciting gifts from business partners; and conversely prohibits business partners from giving Globe employees gifts of any kind in consideration of business, or as an inducement for the award of business.  <br><br>");
        sb.Append("We congratulate you and we look forward to a mutually beneficial and long-lasting business relationship with you. <br><br>");
        //sb.Append("Please access the link below using your username and password to start your application for Globe Telecom accreditation. <br><br>");
        //sb.Append("<a href='http://'<br><br>");
        //sb.Append(sTxt);
        sb.Append("</p>");
        sb.Append("<br><br><br><br>");
        sb.Append("Very truly yours,,<br><br>");
        sb.Append("<b>Honesto P. Oliva</b><br>Head - Vendor Management<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }





    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR IF REJECTED
    private bool SendEmailNotificationReject(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            subject = "Globe Automated Vendor Accreditation application rejected.";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyReject(sfromName, stoName, sAuthenticationTicket, sVendorName),
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

    private string CreateNotificationBodyReject(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName)
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
        //sTxt = sTxt + "<tr>";
        //sTxt = sTxt + "<td><strong>&nbsp;Authentication Ticket</strong></td>";
        //sTxt = sTxt + "<td>&nbsp;" + cAuthenticationNumber + "&nbsp;</td>";
        //sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br>");
        sb.Append("To: " + cVendorName + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + cVendorName + ":<br><br>");
        sb.Append("This is to inform you that your application for vendor accreditation has been disapproved.<br><br>");
        //sb.Append("Please access the link below using your username and password to start your application for Globe Telecom accreditation. <br><br>");
        //sb.Append("<a href='http://'<br><br>");
        sb.Append(sTxt);
        sb.Append("</p>");
        sb.Append("<br><br><br>");
        sb.Append("Sincerely,<br><br>");
        sb.Append("Globe Telecom<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        //sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Good day!<br><br> This is to inform you that application for vendor accreditation has been rejected.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }





    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VM RECO FOR CLARIFY
    private bool SendEmailNotificationClarify(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            //subject = "Clarification - Vendor Accreditation Approval -- " + sVendorName;
            subject = "Vendor Accreditation: For Clarification <" + sVendorName + ">";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyClarify(sfromName, stoName, sAuthenticationTicket, sVendorName),
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

        //sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Good day!<br><br> This is to inform you that application for vendor accreditation has been endorsed for clarification.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");

        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br>");
        sb.Append("To: " + ctoName + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + ctoName + ":<br><br>");
        sb.Append("Re: For Clarification - Vendor Accreditation Approval -- " + cVendorName + "<br><br>");
        sb.Append("This is to request for your clarification of the ff: <br><br>");
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
        sb.Append("&nbsp;&nbsp;3. Click Vendors for Clarifications<br>");
        sb.Append("&nbsp;&nbsp;4. Click View<br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

}
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

public partial class vmofficer_VendorForNotification_List : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    string VendorIdstr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != ((int)Constant.USERTYPE.VMOFFICER).ToString()) Response.Redirect("login.aspx");

        if(IsPostBack)
        {
            if(Request["__EVENTTARGET"].ToString() == "Details" && Request["__EVENTARGUMENT"].ToString()!="")
            {
                //Response.Write(Request["__EVENTARGUMENT"].ToString());
                VendorIdstr = Request["__EVENTARGUMENT"].ToString().Trim();
                string sArg = VendorIdstr.Trim();
                char[] mySeparator = new char[] { '|' };
                string[] Arr = sArg.Split(mySeparator);
                string VendorAlias = "";

                query = "SELECT CompanyName FROM tblVendor WHERE VendorId=@VendorId AND NotificationSent IS NULL";
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Arr[0].ToString()));
                        conn.Open();
                        oReader = cmd.ExecuteReader();
                        if (oReader.HasRows)
                        {
                            while (oReader.Read())
                            {
                                VendorAlias = oReader["CompanyName"].ToString().Replace(" ", "").Substring(0, 4).ToLower();

                            }
                            //Response.Write(VendorAlias);
                            SendMail(Arr[0].ToString(), Arr[1].ToString());
                            query = "UPDATE tblVendor SET NotificationSent=@NotificationSent, SendToSAP_Status=@SendToSAP_Status, AccGroup=@AccGroup, VendorAlias=@VendorAlias, VendorCode=@VendorCode, PurchasingOrg=@PurchasingOrg/*, CountryCode=@CountryCode, Currency=@Currency*/  WHERE VendorId=@VendorId";
                            using (conn = new SqlConnection(connstring))
                            {
                                using (cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Arr[0].ToString()));
                                    cmd.Parameters.AddWithValue("@NotificationSent", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@SendToSAP_Status", 1);
                                    cmd.Parameters.AddWithValue("@AccGroup", Arr[2].Trim().ToString());
                                    cmd.Parameters.AddWithValue("@VendorAlias", VendorAlias);
                                    cmd.Parameters.AddWithValue("@VendorCode", Arr[3].Trim().ToString());
                                    cmd.Parameters.AddWithValue("@PurchasingOrg", Arr[4].Trim().ToString());
                                    //cmd.Parameters.AddWithValue("@CountryCode", Arr[5].Trim().ToString());
                                    //cmd.Parameters.AddWithValue("@Currency", Arr[6].Trim().ToString());
                                    conn.Open(); cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                

                GridView1.DataBind();
            }
        }
    }

    //protected void gvVendors_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName.Equals("Details"))
    //    {

    //        VendorIdstr = e.CommandArgument.ToString().Trim();
    //        //Response.Write(e.CommandArgument.ToString().Trim());
    //        //Session["PrevUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
    //        //Response.Redirect("cfo_vendorDetails_View.aspx?VendorId=" + e.CommandArgument.ToString().Trim());
    //        //Response.Write(Constant.VENDOR_STATUS_APPROVED.ToString());
    //        string sArg = e.CommandArgument.ToString().Trim();
    //        char[] mySeparator = new char[] { '|' };
    //        string[] Arr = sArg.Split(mySeparator);
    //        //Session["BuyerBidForBac"] = Arr[0].ToString();
    //        //Session["BuyerBacRefNo"] = Arr[1].ToString();

    //        query = "UPDATE tblVendor SET NotificationSent=@NotificationSent WHERE VendorId=@VendorId";
    //        //query = "sp_GetVendorInformation"; //##storedProcedure
    //        using (conn = new SqlConnection(connstring))
    //        {
    //            using (cmd = new SqlCommand(query, conn))
    //            {
    //                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
    //                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Arr[0].ToString()));
    //                cmd.Parameters.AddWithValue("@NotificationSent", DateTime.Now);
    //                conn.Open(); cmd.ExecuteNonQuery();
    //            }
    //        }

            
    //    }
    //}



    void SendMail(string VendorIdx, string Statusx)
    {

        // SEND EMAIL NOTIFICATION TO VENDOR
        query = "SELECT t3.FirstName + ' ' + t3.LastName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUsersForVendors t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.VendorId = @VendorId AND t3.UserId = @UserId AND t4.VendorId = @VendorId";
        string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //fromName = "Globe Telecom Admin";
                        //fromEmail = oReader["fromEmail"].ToString();
                        fromName = ConfigurationManager.AppSettings["AdminEmailName"].ToString();
                        fromEmail = ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString();
                        toName = oReader["toName"].ToString();
                        toEmail = oReader["toEmail"].ToString();
                        AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                        VendorName = oReader["CompanyName"].ToString();
                        if (Statusx == "6")
                        {
                            SendEmailNotification(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName, VendorIdx);
                        }
                        else
                        {
                            SendEmailNotificationReject(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName, VendorIdx);
                        }

                    }
                }
            }
        }
        // SEND EMAIL NOTIFICATION TO VM VENDOR ENDS
    }



    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR IF APPROVED
    private bool SendEmailNotification(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName, string VendorIdx)
    {
        //Response.Write(VendorIdx);
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        //Response.Write(from + "<br>" + to);

        try
        {
            subject = "Globe Telecom Vendor Accreditation application approved.";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBody(sfromName, stoName, sAuthenticationTicket, sVendorName, VendorIdx),
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
            Response.Write(ex.ToString());
        }
        return success;
    }

    private string CreateNotificationBody(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName, string VendorIdx)
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
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
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
        //Response.Write(VendorIdx);
        //query = "SELECT t1.*, t2.CategoryName FROM tblVendorProductsAndServices t1, rfcProductCategory t2 WHERE t2.CategoryId = t1.CategoryId AND t1.VendorId = @VendorId";
        query = "SELECT t1.*, t2.CategoryName, t3.SubCategoryName FROM tblVendorProductsAndServices t1, rfcProductCategory t2, rfcProductSubcategory t3 WHERE t2.CategoryId = t1.CategoryId AND t3.SubCategoryId = t1.SubCategoryId AND t1.VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //cServices = cServices + "&bull; " + oReader["CategoryName"].ToString() + "<br>";
                        cServices = cServices + "&bull; " + oReader["CategoryName"].ToString() + " - " + oReader["SubCategoryName"].ToString() + "<br>";
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
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(VendorIdx));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        cAccreDuration = oReader["AccreDuration"].ToString();
                        //Response.Write(cAccreDuration);
                    }
                }
            }
        }



        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='1' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor ID</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + VendorIdx + "&nbsp;</td>";
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
        sb.Append("We are pleased to inform you that Globe Telecom Vendor Management has approved your accreditation.<br><br>");
        //sb.Append("We are pleased to inform you that Globe Telecom Vendor Management has approved your accreditation for the following:<br><br>");
        //sb.Append("<b>PRODUCTS/SERVICES OFFERED   :  </b><br>");
        //sb.Append(cServices + "<br><br><br>");
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
        sb.Append("Very truly yours,<br><br>");
        sb.Append("<b>Honesto P. Oliva</b><br>");
        sb.Append("Head - Vendor Management<br><br>");
        //sb.Append("<b>Globe Telecom Admin<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");
        //Response.Write(sb.ToString());
        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }





    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR IF REJECTED
    private bool SendEmailNotificationReject(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName, string VendorIdx)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            subject = "Globe Telecom Vendor Accreditation application rejected.";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyReject(sfromName, stoName, sAuthenticationTicket, sVendorName, VendorIdx),
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

    private string CreateNotificationBodyReject(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName, string VendorIdx)
    {
        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='0' cellpadding='5' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor ID</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + VendorIdx + "&nbsp;</td>";
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
        sb.Append("Be advised that your application to be an Accredited Globe Vendor was evaluated and found to have not met the requirements, we regret that you will not be accredited at this time.<br><br>");
        sb.Append("If you so wish, we encourage you to schedule a meeting with us to discuss the result of your application for accreditation.<br><br>");
        sb.Append("We would like to thank you for the time afforded in completing and submitting your application.  We will keep your details on record for consideration of future business opportunities.<br><br>");
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
        //Response.Write(sb.ToString());
        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using System.Text;

public partial class vmofficer_Report : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    int numRowsTbl;
    public int VendorId;
    public string queryString;
    string sCommand;
    string cAddress;
    //SqlCommand cmd;
    //SqlConnection conn;

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
        if (Session["SESSION_USERTYPE"].ToString() == ((int)Constant.USERTYPE.VENDOR).ToString())
        {
            Response.Redirect("login.aspx");
        }

        DateTime now = DateTime.Today;
        DateSubmittedTo.Value = now.ToString("MM/dd/yyyy");

        if (IsPostBack)
        {
            //CompanyName.Text = CompanyName.Text; VendorType.SelectedValue = VendorType.SelectedValue;
            //Response.Write(VendorType.SelectedValue);
            //Response.Write(DateSubmittedFr.Value);

            //DateTime LOIDateTo = Convert.ToDateTime(DateSubmittedTo.Value);
            //DateTime LOIDateFrom = Convert.ToDateTime(DateSubmittedFr.Value);
            //Response.Write(dt.ToString("yyyy-MM-dd hh:mm:ss"));

            if (VendorType.SelectedValue == " Approved LOI")
            {
                GenerateReportLOIApproved(DateSubmittedFr.Value, DateSubmittedTo.Value);
            }
            else
            {
                GenerateReportApplicant();
            }
        }
    }

    void GenerateReportLOIApproved(string LOIDateFrom, string LOIDateTo)
    {

        DateTime LOIDateFromDt = Convert.ToDateTime(LOIDateFrom);
        DateTime LOIDateToDt = Convert.ToDateTime(LOIDateTo);

        ExcelPackage pck = new ExcelPackage();
        var ws = pck.Workbook.Worksheets.Add("AVA Vendor Report");
        int i = 1;


        //HEADER
        ws.Cells["A" + i].Value = "VendorId";
        ws.Cells["B" + i].Value = "CompanyName";
        ws.Cells["C" + i].Value = "LOISubmitted";
        ws.Cells["D" + i].Value = "LOIApproved";
        ws.Cells["E" + i].Value = "SubmittedToDnb";
        ws.Cells["F" + i].Value = "AuthenticatedByDnb";
        ws.Cells["G" + i].Value = "ApprovedbyDnb";
        ws.Cells["H" + i].Value = "ApprovedbyVMOfficer";
        ws.Cells["I" + i].Value = "ApprovedbyVMHead";
        ws.Cells["J" + i].Value = "ClarifiedtoVMHead";
        ws.Cells["K" + i].Value = "ApprovedbyPVMD";
        ws.Cells["L" + i].Value = "ApprovedbyCFO";
        ws.Cells["M" + i].Value = "Total";
        ws.Cells["N" + i].Value = "Status";
        ws.Cells["O" + i].Value = "EmailNotificationSent";
        ws.Cells["P" + i].Value = "NotificationSentTo";
        ws.Cells["Q" + i].Value = "RenewalDate";
        ws.Cells["R" + i].Value = "VendorCode";
        ws.Cells["S" + i].Value = "MaxExposureLimit";
        ws.Cells["T" + i].Value = "OverallScore";
        ws.Cells["U" + i].Value = "AccreDuration";
        ws.Cells["V" + i].Value = "OverallEvalRemarks";
        ws.Cells["W" + i].Value = "Address";
        ws.Cells["X" + i].Value = "CEO/President/GM";
        ws.Cells["AC" + i].Value = "Authorized Representative";
        ws.Cells["AH" + i].Value = "Parent Company";
        ws.Cells["AI" + i].Value = "Parent Company Address";
        ws.Cells["AJ" + i].Value = "Nature of Business";
        ws.Cells["AK" + i].Value = "Description of Line of Business";
        ws.Cells["AL" + i].Value = "Products/Services";
        ws.Cells["AM" + i].Value = "Organization Type";
        ws.Cells["AN" + i].Value = "Registration Date";
        ws.Cells["AO" + i].Value = "Registration Number";
        ws.Cells["AP" + i].Value = "Business Permit Registration Date";
        ws.Cells["AQ" + i].Value = "Business Permit Number";
        ws.Cells["AR" + i].Value = "TIN";
        i = i + 1;


        query = @"
            SELECT t1.*,
                dbo.CalculateNumberOFWorkDays(t1.DateSubmittedToDnb , t1.DateAuthenticatedByDnb) as datediff1, 
                dbo.CalculateNumberOFWorkDays(t1.DateAuthenticatedByDnb, t1.approvedbyDnbDate) as datediff2, 
                dbo.CalculateNumberOFWorkDays(t1.approvedbyDnbDate, t1.approvedbyVMOfficerDate) as datediff3, 
                dbo.CalculateNumberOFWorkDays(t1.approvedbyVMOfficerDate, t1.approvedbyVMRecoDate) as datediff4, 
                dbo.CalculateNumberOFWorkDays(t1.approvedbyVMRecoDate, t1.approvedbyFAALogisticsDate) as datediff5, 
                dbo.CalculateNumberOFWorkDays(t1.approvedbyFAALogisticsDate, t1.approvedbyFAAFinanceDate) as datediff6, 
                dbo.CalculateNumberOFWorkDays(t1.DateSubmittedToDnb, t1.approvedbyFAALogisticsDate) as datetotal1, 
                case    when t1.approvedbyFAAFinanceDate is not null THEN  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyFAAFinanceDate) 
                        when t1.approvedbyFAALogisticsDate is not null THEN  dbo.CalculateNumberOFWorkDays(t1.DateSubmittedToDnb, t1.approvedbyFAALogisticsDate) 
                        when t1.approvedbyVMRecoDate is not null THEN  dbo.CalculateNumberOFWorkDays(t1.DateSubmittedToDnb, t1.approvedbyVMRecoDate) 
                        when approvedbyVMOfficerDate is not null THEN  dbo.CalculateNumberOFWorkDays(t1.DateSubmittedToDnb, t1.approvedbyVMOfficerDate) 
                        when DateAuthenticatedByDnb is not null THEN  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, DateAuthenticatedByDnb) 
                        when approvedbyDnbDate is not null THEN  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyDnbDate) 
                ELSE 0 END as 'datetotal2',
                case    when t1.Status = 8 THEN 'Disapproved'
                        when  t1.Status = 6 THEN 'Approved'
                        else 'Ongoing' end as 'StatusTxt',
				case when t1.NotificationSent IS NOT NULL THEN 
					t3.EmailAdd else NULL END AS 'EmailSentTo',
            t0.*,
            t4.AccreDuration, t4.OverallEvalRemarks,
            t5.dnbMaxExposureLimit, t5.vmoOverallScore,
            t6.DateCreated as DateLOISubmitted,
                case when t6.Status = 2 THEN 'Approved'
                     ELSE 'Rejected' END AS 'LOIStatus', 
            (
				SELECT t8.NatureOfBusinessName + ', ' AS 'data()'
				FROM tblVendorNatureOfBusiness t7 
				LEFT JOIN rfcNatureOfBusiness t8 ON t8.NatureOfBusinessId=t7.NatureOfBusinessId
				WHERE t7.VendorId = t1.VendorId
				FOR XML PATH('')
            )   AS 'NatureOfBusiness',
            CASE t4.AccreDuration
				WHEN '2 years' THEN DATEADD(month, 22, t1.approvedbyFAALogisticsDate)
				WHEN '1 year' THEN DATEADD(month, 10, t1.approvedbyFAALogisticsDate)
				WHEN '6 months' THEN DATEADD(month, 4, t1.approvedbyFAALogisticsDate)
				ELSE NULL
			END as 'DueDate', 
            (
				SELECT t10.CategoryName + ' - ' + t11.SubCategoryName + '; ' AS 'data()'
				FROM tblVendorProductsAndServices t9 
				LEFT JOIN rfcProductCategory t10 ON t10.CategoryId=t9.CategoryId
				LEFT JOIN rfcProductSubCategory t11 ON t11.SubCategoryId=t9.SubCategoryId
				WHERE t9.VendorId = t1.VendorId
				FOR XML PATH('')
            )   AS 'Products/Services'
            
            FROM tblVendor t1
            LEFT JOIN tblVendorInformation t0 ON t0.VendorId=t1.VendorId
            LEFT JOIN tblUsersForVendors t2 ON t2.VendorId = t1.VendorId
            LEFT JOIN tblUsers t3 ON t3.UserId = t2.UserId 
            LEFT JOIN tblVendorApprovalbyVmReco t4 ON t4.VendorId=t1.VendorId
            LEFT JOIN tblDnbReport t5 ON t5.VendorId = t1.VendorId
            LEFT JOIN tblVendorApplicants t6 ON t6.CompanyName = t1.CompanyName
            WHERE t1.Status >= 0 --AND Status <> 7 AND Status <> 9 AND Status <> 8 AND Status <> 6    
                AND t1.DateCreated > @LOIDateFrom  AND t1.DateCreated < @LOIDateTo  
            ORDER BY t1.DateCreated DESC";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LOIDateFrom", LOIDateFromDt);
                cmd.Parameters.AddWithValue("@LOIDateTo", LOIDateToDt);
                conn.Open();  oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        ws.Cells["A" + i].Value = oReader["VendorId"];
                        ws.Cells["B" + i].Value = oReader["CompanyName"];
                        ws.Cells["C" + i].Value = oReader["DateLOISubmitted"]; ws.Cells["C" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["D" + i].Value = oReader["DateCreated"]; ws.Cells["D" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["E" + i].Value = oReader["DateSubmittedToDnb"]; ws.Cells["E" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["F" + i].Value = oReader["DateAuthenticatedByDnb"]; ws.Cells["F" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["G" + i].Value = oReader["approvedbyDnbDate"]; ws.Cells["G" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["H" + i].Value = oReader["approvedbyVMOfficerDate"]; ws.Cells["H" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["I" + i].Value = oReader["approvedbyVMRecoDate"]; ws.Cells["I" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["J" + i].Value = oReader["clarifiedtoVMRecoDate"]; ws.Cells["J" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["K" + i].Value = oReader["approvedbyFAALogisticsDate"]; ws.Cells["K" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["L" + i].Value = oReader["approvedbyFAAFinanceDate"]; ws.Cells["L" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["M" + i].Value = oReader["datetotal2"];
                        ws.Cells["N" + i].Value = oReader["StatusTxt"];
                        ws.Cells["O" + i].Value = oReader["NotificationSent"]; ws.Cells["O" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["P" + i].Value = oReader["EmailSentTo"];
                        ws.Cells["Q" + i].Value = oReader["DueDate"]; ws.Cells["Q" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["R" + i].Value = oReader["VendorCode"];
                        ws.Cells["S" + i].Value = oReader["dnbMaxExposureLimit"];
                        ws.Cells["T" + i].Value = oReader["vmoOverallScore"];
                        ws.Cells["U" + i].Value = oReader["AccreDuration"];
                        ws.Cells["V" + i].Value = oReader["OverallEvalRemarks"];

                        cAddress = "";
                        cAddress = oReader["regBldgCode"].ToString() != "" ? cAddress + "Bldg. " + oReader["regBldgCode"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgRoom"].ToString() != "" ? cAddress + "Rm. " + oReader["regBldgRoom"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regBldgFloor"].ToString() != "" ? cAddress + oReader["regBldgFloor"].ToString() + " Fr, " : cAddress + "";
                        cAddress = oReader["regBldgHouseNo"].ToString() != "" ? cAddress + "No. " + oReader["regBldgHouseNo"].ToString() + " " : cAddress + "";
                        cAddress = oReader["regStreetName"].ToString() != "" ? cAddress + oReader["regStreetName"].ToString() + ", " : cAddress + "";
                        cAddress = cAddress + "\n";
                        cAddress = oReader["regCity"].ToString() != "" ? cAddress + oReader["regCity"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regProvince"].ToString() != "" ? cAddress + oReader["regProvince"].ToString() + ", " : cAddress + "";
                        cAddress = cAddress + "\n";
                        cAddress = oReader["regCountry"].ToString() != "" ? cAddress + oReader["regCountry"].ToString() + ", " : cAddress + "";
                        cAddress = oReader["regPostal"].ToString() != "" ? cAddress + oReader["regPostal"].ToString() + " " : cAddress + "";
                        ws.Cells["W" + i].Value = cAddress;


                        ws.Cells["X" + i].Value = oReader["conBidName"];
                        ws.Cells["Y" + i].Value = oReader["conBidPosition"];
                        ws.Cells["Z" + i].Value = oReader["conBidEmail"];
                        ws.Cells["AA" + i].Value = oReader["conBidMobile"];
                        ws.Cells["AB" + i].Value = oReader["conBidTelNo"];

                        ws.Cells["AC" + i].Value = oReader["conLegName"];
                        ws.Cells["AD" + i].Value = oReader["conLegPosition"];
                        ws.Cells["AE" + i].Value = oReader["conLegEmail"];
                        ws.Cells["AF" + i].Value = oReader["conLegMobile"];
                        ws.Cells["AG" + i].Value = oReader["conLegTelNo"];

                        ws.Cells["AH" + i].Value = oReader["parentCompanyName"];
                        ws.Cells["AI" + i].Value = oReader["parentCompanyAddr"];
                        ws.Cells["AJ" + i].Value = oReader["NatureOfBusiness"];
                        ws.Cells["AK" + i].Value = oReader["prodServ_DescLineOfBusiness"];
                        ws.Cells["AL" + i].Value = oReader["Products/Services"];
                        ws.Cells["AM" + i].Value = oReader["legalStrucOrgType"];
                        ws.Cells["AN" + i].Value = oReader["legalStrucDateReg"]; ws.Cells["AN" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["AO" + i].Value = oReader["legalStrucRegNo"];
                        ws.Cells["AP" + i].Value = oReader["busPermitDateReg"]; ws.Cells["AP" + i].Style.Numberformat.Format = "mm-dd-yy";
                        ws.Cells["AQ" + i].Value = oReader["busPermitNo"];
                        ws.Cells["AR" + i].Value = oReader["birRegTIN"];

                        i = i + 1;
                    }
                }
            }
        }


        //ws.Cells["A1"].Value = VendorType;
        //ws.Cells["A1"].Style.Font.Bold = true;
        //var shape = ws.Drawings.AddShape("Shape1", eShapeStyle.Rect);
        //shape.SetPosition(50, 200);
        //shape.SetSize(200, 100);
        //shape.Text = "Sample 1 saves to the Response.OutputStream";

        pck.SaveAs(Response.OutputStream);
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;  filename=AVA_Vendor_Report.xlsx");

    }



    void GenerateReportApplicant()
    {

        //DateTime LOIDateFromDt = Convert.ToDateTime(LOIDateFrom);
        //DateTime LOIDateToDt = Convert.ToDateTime(LOIDateTo);

        ExcelPackage pck = new ExcelPackage();
        var ws = pck.Workbook.Worksheets.Add("AVA Vendor Report");
        //int i = 1;

        pck.SaveAs(Response.OutputStream);
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;  filename=AVA_Vendor_Report.xlsx");
    }

}
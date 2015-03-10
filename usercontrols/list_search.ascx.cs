using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usercontrols_list_search : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder) Master.FindControl("ContentPlaceHolder1");
        //HttpContext.Current.Response.Write(ContentPlaceHolder1.CompanyName);
        TextBox CompanyName1 = (TextBox)this.Parent.FindControl("CompanyName");
        RadioButtonList VendorType1 = (RadioButtonList)this.Parent.FindControl("VendorType");
        //Response.Write(txt.Text);

        if (IsPostBack && CompanyName1.Text != "" && VendorType1.SelectedValue == " Qualified")
        {
            GridView1.Visible = true;
            GridView2.Visible = false;
            titleh3.Visible = true;
            dsVendorSearch.SelectCommand =
            @"
                SELECT *, 
                     dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb , DateAuthenticatedByDnb) as datediff1, 
                     dbo.CalculateNumberOFWorkDays(DateAuthenticatedByDnb, approvedbyDnbDate) as datediff2, 
                     dbo.CalculateNumberOFWorkDays(approvedbyDnbDate, approvedbyVMOfficerDate) as datediff3, 
                     dbo.CalculateNumberOFWorkDays(approvedbyVMOfficerDate, approvedbyVMRecoDate) as datediff4, 
                     dbo.CalculateNumberOFWorkDays(approvedbyVMRecoDate, approvedbyFAALogisticsDate) as datediff5, 
                     dbo.CalculateNumberOFWorkDays(approvedbyFAALogisticsDate, approvedbyFAAFinanceDate) as datediff6, 
                     dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyFAALogisticsDate) as datetotal1, 
                    case 
                        when approvedbyFAAFinanceDate is not null 
                            THEN  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyFAAFinanceDate) 
                        when approvedbyFAALogisticsDate is not null 
                            THEN  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyFAALogisticsDate) 
                        when approvedbyVMRecoDate is not null 
                            THEN  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyVMRecoDate) 
                        when approvedbyVMOfficerDate is not null 
                            THEN  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyVMOfficerDate) 
                        when DateAuthenticatedByDnb is not null 
                            THEN  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, DateAuthenticatedByDnb) 
                        when approvedbyDnbDate is not null 
                            THEN  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyDnbDate) 
                        ELSE 0 END as 'datetotal2' 
                FROM tblVendor 
                WHERE --Status >= 1 AND Status <> 7 AND Status <> 9 AND Status <> 8 AND Status <> 6  AND 
                    CompanyName LIKE '%" + CompanyName1.Text + @"%' 
                ORDER BY DateCreated DESC
            ";
            GridView1.DataBind();

            titleh3.InnerHtml = "Search results for \"" + CompanyName1.Text + "\"";
        }

        else if (IsPostBack && CompanyName1.Text != "" && VendorType1.SelectedValue == " Applicant")
        {
            GridView1.Visible = false;
            GridView2.Visible = true;
            titleh3.Visible = true;
            dsVendorSearch.SelectCommand =
            @"
                SELECT t1.ID, 
                       t1.CompanyName, 
                        t1.EmailAdd, 
                        t1.LOIFileName, 
                        t1.DateCreated, 
                        t1.Status,
                        t1.EmailAdd+';'+t1.CompanyName+';'+Cast(t1.ID as char) as EmailCo, 
                        (
                            SELECT STUFF((SELECT '; ' + categoryName 
                            FROM rfcProductCategory 
                            WHERE CategoryId IN (
                                                    SELECT CategoryId 
                                                    FROM tblVendorApplicantCategory 
                                                    WHERE VendorApplicantId = t1.ID
                                                ) FOR XML PATH ('')), 1, 2, '')) as CategoryName,
                      CASE WHEN t1.Status = '3' THEN 'Rejected'
                           WHEN t1.Status = '2' THEN 'Qualified'
                           ELSE 'New' END AS 'Status1'
                FROM tblVendorApplicants t1 
                WHERE --(t1.Status = 0 OR t1.Status = 1) AND 
                      t1.CompanyName LIKE '%" + CompanyName1.Text + @"%'  
                ORDER BY t1.DateCreated DESC
            ";
            GridView2.DataBind();

            titleh3.InnerHtml = "Search results for \"" + CompanyName1.Text + "\"";
        }
        else
        {
            GridView1.Visible = false;
            GridView2.Visible = false;
            titleh3.Visible = false;
        }
    }



    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {

        if (System.Web.HttpContext.Current.Session["SESSION_USERTYPE"].ToString() != "10")
        {
            e.Row.Cells[5].Visible = false;

        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (System.Web.HttpContext.Current.Session["SESSION_USERTYPE"].ToString() != "10")
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[5].Visible = false;//this is your templatefield column.
            }
        }
    }



    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Details"))
        {
            //Session["ViewOption"] = "AsBuyer";
            //string sArg = e.CommandArgument.ToString().Trim();
            //char[] mySeparator = new char[] { ';' };
            //string[] Arr = sArg.Split(mySeparator);
            //Session["VendorEmail"] = "";
            //Session["BuyerBidForBac"] = Arr[0].ToString();
            //Session["BuyerBacRefNo"] = Arr[1].ToString();

            System.Web.HttpContext.Current.Session["VendorId"] = e.CommandArgument.ToString().Trim();

            System.Web.HttpContext.Current.Session["PrevUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            System.Web.HttpContext.Current.Session["vendorDetails"] = "cfo_vendorDetails_View.aspx";
            Response.Redirect("cfo_vendorDetails_View.aspx");

        }
    }

    protected void gvBids_RowCommand2(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Details2"))
        {
            //Session["ViewOption"] = "AsBuyer";
            string sArg = e.CommandArgument.ToString().Trim();
            //Response.Write(sArg);
            char[] mySeparator = new char[] { '|' };
            string[] Arr = sArg.Split(mySeparator);
            System.Web.HttpContext.Current.Session["VendorEmail"] = Arr[0].ToString();
            System.Web.HttpContext.Current.Session["VendorCompany"] = Arr[1].ToString();
            System.Web.HttpContext.Current.Session["VendorApplicantId"] = Arr[2].ToString();
            string Stat = Arr[3].ToString();

            if (Stat == "0")
            {
                System.Web.HttpContext.Current.Session["PrevUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
                System.Web.HttpContext.Current.Session["vendorDetails"] = "vmofficer_VendorDetails.aspx";
                Response.Redirect("vmofficer_VendorDetails.aspx");
            }
            else
            {
                System.Web.HttpContext.Current.Session["PrevUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
                System.Web.HttpContext.Current.Session["vendorDetails"] = "vmofficer_VendorDetails_View.aspx";
                Response.Redirect("vmofficer_VendorDetails_View.aspx");
            }

        }
    }
}
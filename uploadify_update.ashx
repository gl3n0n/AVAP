<%@ WebHandler Language="C#" Class="UploadUpdate" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

public class UploadUpdate : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Expires = -1;
        try
        {
            string value = context.Request["value"];
            string tbl = context.Request["tbl"];
            string col = context.Request["col"];
            int vendorid = Convert.ToInt32(context.Request["vendorid"]);
            //context.Response.Write(value + "|" + tbl + "|" + col + "|" + vendorid);
            if (tbl == "tblVendorFinancialInformation")
            {
                UpdatetblVendorFinancialInformation(tbl, col, vendorid, value, context);
            }
            else
            {
                UpdatetblVendorInformation(tbl, col, vendorid, value, context);
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("Error: " + ex.Message);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


    void UpdatetblVendorInformation(string tbl, string col, int vendorid, string val, HttpContext context)
   {
       string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
       string sCommand = "";
       SqlCommand cmd;
       SqlConnection conn;

       //context.Response.Write(tbl);

       //CLEAR tblVendorBranches FROM USER
       sCommand = "UPDATE "+tbl+" SET "+col+"=@val WHERE VendorId=@VendorId";
       //query = "sp_GetVendorInformation"; //##storedProcedure
       using (conn = new SqlConnection(connstring))
       {
           using (cmd = new SqlCommand(sCommand, conn))
           {
               //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
               cmd.Parameters.AddWithValue("@VendorId", vendorid);
               cmd.Parameters.AddWithValue("@val", val);
               conn.Open(); cmd.ExecuteNonQuery();
           }
       }

   }
    void UpdatetblVendorFinancialInformation(string tbl, string col, int vendorid, string val, HttpContext context)
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";
        SqlCommand cmd;
        SqlConnection conn;

        //context.Response.Write(tbl);

        //CLEAR tblVendorBranches FROM USER
        //sCommand = "UPDATE " + tbl + " SET " + col + "=@val WHERE VendorId=@VendorId";
        sCommand = @"if not exists(select 1 from tblVendorFinancialInformation where VendorId = @VendorId and Year = '1')
                        begin
                            insert into tblVendorFinancialInformation (VendorId, Year, FileName) values(@VendorId,'1', @val)
                        end
                        else
                        begin
                            update tblVendorFinancialInformation set FileName = @val where VendorId =  @VendorId
                        end";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(sCommand, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", vendorid);
                cmd.Parameters.AddWithValue("@val", val);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

    }
}
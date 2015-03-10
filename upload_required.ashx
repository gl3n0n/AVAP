<%@ WebHandler Language="C#" Class="Upload" %>

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

public class Upload : IHttpHandler 
{
   public void ProcessRequest (HttpContext context) 
   {
      context.Response.ContentType = "text/plain";
      context.Response.Expires = -1;
      try
      {
          //string par = context.Request["par"].ToString();
          //string[] param = par.Split('|');
          //string tbl = param[0];
          //string col = param[1];
          //int vendorid = Convert.ToInt32(param[2].ToString());
          ////string curval = param[3];
          ////string vendorid = context.Request["vendorid"].ToString();
          ////string col = context.Request["col1"].ToString();
          ////context.Response.Write(vendorid);
          HttpPostedFile postedFile = context.Request.Files["Filedata"];
  
          string savepath = "";
          string tempPath = "";

         
          tempPath = context.Request["folder"];

          //If you prefer to use web.config for folder path, uncomment below line:
          //tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];

          savepath = context.Server.MapPath(tempPath);
          DateTime thisDay = DateTime.Now;
          //Response.Write(thisDay.ToString("yyyyMMddHHmmss"));
          string filename = thisDay.ToString("yyyyMMddHHmmss") + "_" + postedFile.FileName.Replace("'", "");
          filename = Regex.Replace(filename, "[^a-zA-Z0-9/_/./-]", "");

          //savepath = context.Server.MapPath(tempPath);
          //string filename = postedFile.FileName.Replace("'", "");
          if (!Directory.Exists(savepath))
          {
              Directory.CreateDirectory(savepath);
          }

          postedFile.SaveAs(savepath + @"\" + filename);
          context.Response.Write(tempPath + "/" + filename);
          context.Response.StatusCode = 200;

          //SaveToDB(tbl, col, vendorid, tempPath + "/" + filename, context);
          
          
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

   //void SaveToDB(string tbl, string col, int vendorid, string val, HttpContext context)
   //{
   //    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
   //    string sCommand = "";
   //    SqlCommand cmd;
   //    SqlConnection conn;

   //    //context.Response.Write(tbl);

   //    //CLEAR tblVendorBranches FROM USER
   //    //sCommand = "UPDATE "+tbl+" SET "+col+"="+col+" + @val WHERE VendorId=@VendorId";
   //    sCommand = "UPDATE "+tbl+" SET "+col+" = '"+val+"' WHERE VendorId="+vendorid;
   //    context.Response.Write(sCommand);
   //    //query = "sp_GetVendorInformation"; //##storedProcedure
   //    using (conn = new SqlConnection(connstring))
   //    {
   //        using (cmd = new SqlCommand(sCommand, conn))
   //        {
   //            //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
   //            cmd.Parameters.AddWithValue("@VendorId", vendorid);
   //            //cmd.Parameters.AddWithValue("@val", ";" + val);
   //            //cmd.Parameters.AddWithValue("@col", col);
   //            //cmd.Parameters.AddWithValue("@tbl", tbl);
   //            conn.Open(); cmd.ExecuteNonQuery();
   //        }
   //    }

   //}
}
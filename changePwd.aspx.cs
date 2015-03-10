using System;
using System.Data;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ava;
using Ava.lib.utils;
using System.Data.Sql;
using System.Data.SqlClient;
using Ava.lib;

public partial class changePwd : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    int numRowsTbl;
    public int VendorId;
    public string queryString;
    private string connString = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null) Response.Redirect("login.aspx");

    }

    protected void btnChangePwd_Click(object sender, EventArgs e)
    {
        DataTable dt;
        SqlParameter[] sqlparams = new SqlParameter[1];
        sqlparams[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
        sqlparams[0].Value = Session["SESSION_USERNAME"].ToString();

        string OldPassword = EncryptionHelper.Encrypt(txtOldPwd.Text.Trim());
        string NewPassword = EncryptionHelper.Encrypt(txtNewPwd.Text.Trim());

        try
        {
            dt = SqlHelper.ExecuteDataset(connString, "sp_GetUserPasswordAndEmail", sqlparams).Tables[0];

            string Pwd = dt.Rows[0]["Password"].ToString();
            string EmailAdd = dt.Rows[0]["EmailAddress"].ToString();


            if (Pwd == OldPassword && txtNewPwd.Text.Trim() == txtNewPwd2.Text.Trim())
            {
                Response.Write(Pwd + " : " + EmailAdd);


                query = "UPDATE tblUsers SET UserPassword=@UserPassword WHERE UserId = @Userid";
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"].ToString()));
                        cmd.Parameters.AddWithValue("@UserPassword", NewPassword);
                        conn.Open(); cmd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("login.aspx");
            }
            else if (Pwd != OldPassword)
            {
                txtNote.Text = "Invalid Old Password.";
            }
            else
            {
                txtNote.Text = "New Password not matched.";
            }
        }
        catch (Exception ex)
        {
            txtNote.Text = ex.Message.ToString();
        }
    }
}
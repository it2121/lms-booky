using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace BOOKY
{
    public partial class adminlogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM admin_login_tbl WHERE username = '" + TextBox1.Text.Trim() + "' AND password = '" + TextBox2.Text.Trim() + "'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count >= 1)
            {

                Session["username"] = TextBox1.Text.Trim();
                foreach (DataRow row in dt.Rows)
                {
                    Session["fullname"] = row["full_name"].ToString();
                }
                Session["role"] = "admin";


                Response.Redirect("homepage.aspx");


            }
            else
            {

                Response.Write("<script>alert('username or password are not correct');</script>");


            }
            sqlconn.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
namespace BOOKY
{
    public partial class usersignup : System.Web.UI.Page
    {
   



 
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {





    
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
         if (ch() == true) { sup(); } else { Response.Write("<script>alert('username already exists');</script>"); }
        }
        bool ch() {
            bool ok = false;
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM member_master_tbl WHERE member_id = '"+TextBox8.Text.Trim()+"'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count >= 1) { ok = false; } else { ok = true; }
             sqlconn.Close();






            return ok;
        }

        void sup() {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);

            string pen = "ss";
         string sqlq = "INSERT INTO member_master_tbl(full_name,dob,contact_no,email,state,city,pincode,full_address,member_id,password,account_status) VALUES('" + TextBox1.Text.Trim() + "','" + TextBox2.Text.Trim() + "','" + TextBox3.Text.Trim() + "','" + TextBox4.Text.Trim() + "','" + DropDownList1.SelectedValue.Trim() + "','" + TextBox6.Text.Trim() + "','" + TextBox7.Text.Trim() + "','" + TextBox5.Text.Trim() + "','" + TextBox8.Text.Trim() + "','" + TextBox9.Text.Trim() + "','" +pen+"')";
           
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            Response.Write("<script>alert('Sign up is successful. Go to user login to login');</script>");


            sqlconn.Close();



        }
    }
}
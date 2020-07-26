using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
namespace BOOKY
{
    public partial class adminmembermanagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM member_master_tbl ";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            sqlconn.Close();
            dt.Columns.Remove("dob");
            dt.Columns.Remove("pincode");
            dt.Columns.Remove("full_address");
            dt.Columns.Remove("password");
            dt.Columns.Remove("state");
            dt.Columns.Remove("email");
            dt.Columns.Remove("city");
            
            dt.Columns["account_status"].SetOrdinal(1);
            dt.Columns["member_id"].SetOrdinal(2);
            dt.Columns["full_name"].ColumnName = "Name";
            dt.Columns["account_status"].ColumnName = "Account Status";

            dt.Columns["member_id"].ColumnName = "Member Id";

            dt.Columns["contact_no"].ColumnName = "Contact";

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            if (ch() == false)
            {
                set();
            }
            else { Response.Write("<script>alert('ID does not exists');</script>"); }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (ch() == false)
            {
                up("active");

            }
            else { Response.Write("<script>alert('ID does not exists');</script>"); }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (ch() == false)
            {
                up("pending");

            }
            else { Response.Write("<script>alert('ID does not exists');</script>"); }

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {

            if (ch() == false)
            {
                up("deactive");
               
            }
            else { Response.Write("<script>alert('ID does not exists');</script>"); }
           

        }
        bool ch()
        {
            bool ok = false;
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM member_master_tbl WHERE member_id = '" + TextBox1.Text.Trim() + "'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count >= 1) { ok = false; } else { ok = true; }
            sqlconn.Close();

            return ok;
        }
        void set()
        {

            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM member_master_tbl WHERE member_id = '" + TextBox1.Text.Trim() + "'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count >= 1)
            {

                foreach (DataRow row in dt.Rows)
                {
                    //TextBox1.Text = row["publisher_name"].ToString();
                    TextBox2.Text = row["full_name"].ToString();
                    TextBox7.Text = row["account_status"].ToString();
                    TextBox8.Text = row["dob"].ToString();
                    TextBox3.Text = row["contact_no"].ToString();
                    TextBox4.Text = row["email"].ToString();
                    TextBox9.Text = row["state"].ToString();
                    TextBox10.Text = row["city"].ToString();
                    TextBox11.Text = row["pincode"].ToString();
                    TextBox6.Text = row["full_address"].ToString();

                }



            }
            sqlconn.Close();

        }
        void up(string value)
        {
            string stat = value;
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);


            string sqlq = "UPDATE member_master_tbl SET account_status ='" + stat + "'WHERE member_id = '" + TextBox1.Text.Trim() + "'";

            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            //Response.Write("<script>alert('Update is successful.');</script>");
            TextBox7.Text = stat;
            refresh();


            sqlconn.Close();



        }
        void refresh() {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM member_master_tbl ";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            sqlconn.Close();
            dt.Columns.Remove("dob");
            dt.Columns.Remove("pincode");
            dt.Columns.Remove("full_address");
            dt.Columns.Remove("password");
            dt.Columns.Remove("state");
            dt.Columns.Remove("email");
            dt.Columns.Remove("city");

            dt.Columns["account_status"].SetOrdinal(1);
            dt.Columns["member_id"].SetOrdinal(2);
            dt.Columns["full_name"].ColumnName = "Name";
            dt.Columns["account_status"].ColumnName = "Account Status";

            dt.Columns["member_id"].ColumnName = "Member Id";

            dt.Columns["contact_no"].ColumnName = "Contact";

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ch() == false)
            {
                del();
                Response.Redirect("adminmembermanagement.aspx");
            }
            else { Response.Write("<script>alert('ID does not exists');</script>"); }
        
    }
        void del()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);


            string sqlq = "DELETE FROM member_master_tbl WHERE member_id ='" + TextBox1.Text.Trim() + "'";

            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            //Response.Write("<script>alert('The Deleting was Successful.');</script>");


            sqlconn.Close();
            refresh();


        }
    }
}
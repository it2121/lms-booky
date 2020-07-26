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
    public partial class publishermanagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM publisher_master_tbl ";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            sqlconn.Close();
            dt.Columns["publisher_id"].ColumnName = "Publisher Id";
            dt.Columns["publisher_name"].ColumnName = "Publisher Name";
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            if (ch() == true) { sup(); Response.Redirect("publishermanagement.aspx");}
            else { Response.Write("<script>alert('ID already exists');</script>"); }
        }

        protected void Button3_Click1(object sender, EventArgs e)
        {
            if (ch() == false) { up(); Response.Redirect("publishermanagement.aspx");
            }
            else { Response.Write("<script>alert('ID does not exists');</script>"); }
        }

        protected void Button4_Click1(object sender, EventArgs e)
        {
            if (ch() == false) { del(); Response.Redirect("publishermanagement.aspx");
            }
            else { Response.Write("<script>alert('ID does not exists');</script>"); }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (ch() == false) { go(); } else { Response.Write("<script>alert('ID does not exists');</script>"); }

        }
        bool ch()
        {
            bool ok = false;
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM publisher_master_tbl WHERE publisher_id = '" + TextBox1.Text.Trim() + "'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count >= 1) { ok = false; } else { ok = true; }
            sqlconn.Close();

            return ok;
        }

        void sup()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);


            string sqlq = "INSERT INTO publisher_master_tbl (publisher_id,publisher_name) VALUES('" + TextBox1.Text.Trim() + "','" + TextBox2.Text.Trim() + "')";

            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            Response.Write("<script>alert('Regestration is successful.');</script>");


            sqlconn.Close();



        }
        void up()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);


            string sqlq = "UPDATE publisher_master_tbl SET publisher_name ='" + TextBox2.Text.Trim() + "'WHERE publisher_id = '" + TextBox1.Text.Trim() + "'";

            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            Response.Write("<script>alert('Update is successful.');</script>");


            sqlconn.Close();



        }
        void del()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);


            string sqlq = "DELETE FROM publisher_master_tbl WHERE publisher_id ='" + TextBox1.Text.Trim() + "'";

            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            Response.Write("<script>alert('The Deleting was Successful.');</script>");


            sqlconn.Close();



        }
        void go()
        {

            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM publisher_master_tbl WHERE publisher_id = '" + TextBox1.Text.Trim() + "'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count >= 1)
            {

                foreach (DataRow row in dt.Rows)
                {
                    TextBox2.Text = row["publisher_name"].ToString();
                }



            }
            sqlconn.Close();


        }


    }
}
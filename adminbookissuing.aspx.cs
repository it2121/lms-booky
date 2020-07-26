using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.IO;
namespace BOOKY
{
    public partial class adminbookissuing : System.Web.UI.Page
    {
        public static bool good = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM book_issue_tbl ";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            sqlconn.Close();
            sqlconn.Close();
          

            dt.Columns["member_id"].ColumnName = "Member Id";
            dt.Columns["member_name"].ColumnName = "Member Name";
            dt.Columns["book_id"].ColumnName = "Book Id";
            dt.Columns["book_name"].ColumnName = "Book Name";
            dt.Columns["issue_date"].ColumnName = "Issue Date";
            dt.Columns["due_date"].ColumnName = "Due Date";


            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool mem = false, bk = false;
            if (!chbk())
            {
                bk = true;
            }
            else { Response.Write("<script>alert('Book does not exists');</script>"); }
            if (!chmem())
            {
                mem = true;
            }
            else { Response.Write("<script>alert('Member does not exists');</script>"); }
            if (bk) { setbk(); }
            if (mem) { setmem(); }
            if (bk && mem) { good = true; }


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            bool go = false, iss = false;


            if (good) { go = true;

                if (chbkis()) { iss = true; } else { Response.Write("<script>alert('There are no available books');</script>"); }

            } else { Response.Write("<script>alert('Member or Book does not exists');</script>"); }

           


            if (go&&iss) {

                insert();
                good = false;

            }


        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            bool mem = false, bk = false;
            if (!chbk())
            {
                bk = true;
            }
            else { Response.Write("<script>alert('Book does not exists');</script>"); }
            if (!chmem())
            {
                mem = true;
            }
            else { Response.Write("<script>alert('Member does not exists');</script>"); }

            if ( mem && bk) { 
            if (chdel())
            {

                del();

            }
            else { Response.Write("<script>alert('Record does not exists');</script>"); }
            }
        }
        void setmem()
        {

            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM member_master_tbl WHERE member_id = '" + TextBox2.Text.Trim() + "'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count >= 1)
            {

                foreach (DataRow row in dt.Rows)
                {
                    
                    TextBox3.Text = row["full_name"].ToString();
              


                }



            }
            sqlconn.Close();

        }
        void setbk()
        {

            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM book_master_tbl WHERE book_id = '" + TextBox1.Text.Trim() + "'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count >= 1)
            {

                foreach (DataRow row in dt.Rows)
                {

                    TextBox4.Text = row["book_name"].ToString();



                }



            }
            sqlconn.Close();

        }
        bool chmem()
        {
            bool ok = false;

            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM member_master_tbl WHERE member_id = '" + TextBox2.Text.Trim() + "'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count >= 1) { ok = false; } else { ok = true; }
            sqlconn.Close();

            return ok;
        }
        bool chbk()
        {
            bool ok = false;

            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM book_master_tbl WHERE book_id = '" + TextBox1.Text.Trim() + "'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count >= 1) { ok = false; } else { ok = true; }
            sqlconn.Close();

            return ok;
        }
        bool chbkis()
        {
            bool ok = false;

            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM book_master_tbl WHERE book_id = '" + TextBox1.Text.Trim() + "'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
          
            sqlconn.Close();
            if (dt.Rows.Count >= 1)
            {

                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row["current_stock"].ToString()) > 0) {
                        ok = true;
                    
                    }

                    


                }



            }
            return ok;
        }
        void insert()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);


            string sqlq = "INSERT INTO book_issue_tbl(member_id,member_name,book_id,book_name,issue_date,due_date) VALUES('" + TextBox2.Text.Trim() + "','" + TextBox3.Text.Trim() + "','" + TextBox1.Text.Trim() + "','" + TextBox4.Text.Trim() + "','" + TextBox5.Text.Trim() + "','" + TextBox6.Text.Trim() + "')";

            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
           


            sqlconn.Close();

            refresh();
            string mainconn1 = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn1 = new MySqlConnection(mainconn1);


            string sqlq1 = "UPDATE book_master_tbl SET current_stock = current_stock -1 WHERE book_id ='"+TextBox1.Text.Trim()+"' ";

            MySqlCommand sqlcmd1 = new MySqlCommand(sqlq1, sqlconn1);
            sqlconn1.Open();
            sqlcmd1.ExecuteNonQuery();



            sqlconn1.Close();

        }

        void refresh()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM book_issue_tbl ";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            sqlconn.Close();


            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        void del()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);


            string sqlq = "DELETE FROM book_issue_tbl WHERE book_id ='" + TextBox1.Text.Trim() + "' AND member_id ='"+ TextBox2.Text.Trim() + "'";

            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            //Response.Write("<script>alert('The Deleting was Successful.');</script>");


            sqlconn.Close();
            refresh();
            string mainconn1 = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn1 = new MySqlConnection(mainconn1);


            string sqlq1 = "UPDATE book_master_tbl SET current_stock = current_stock +1 WHERE book_id ='" + TextBox1.Text.Trim() + "' ";

            MySqlCommand sqlcmd1 = new MySqlCommand(sqlq1, sqlconn1);
            sqlconn1.Open();
            sqlcmd1.ExecuteNonQuery();



            sqlconn1.Close();

        }
        bool chdel()
        {
            bool ok = false;

            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM book_issue_tbl WHERE book_id ='" + TextBox1.Text.Trim() + "' AND member_id ='" + TextBox2.Text.Trim() + "'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            sqlconn.Close();
            

                if (dt.Rows.Count >= 1) { ok = true; } else { ok = false; }



           
            return ok;
        }
       
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Check your condition here
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today > dt)
                    {
                        e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
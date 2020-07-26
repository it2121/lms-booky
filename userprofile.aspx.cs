using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace BOOKY
{
    public partial class userprofile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                set();
                string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
                MySqlConnection sqlconn = new MySqlConnection(mainconn);
                string sqlq = "SELECT * FROM book_issue_tbl WHERE member_id = '" + Session["username"].ToString() + "'";
                MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
                sqlconn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dt.Columns["member_id"].ColumnName = "Member Id";
                dt.Columns["member_name"].ColumnName = "Member Name";

                dt.Columns["book_id"].ColumnName = "Book Id";

                dt.Columns["book_name"].ColumnName = "Book Name";
                dt.Columns["issue_date"].ColumnName = "Issue Date";

                dt.Columns["due_date"].ColumnName = "Due Date";


                GridView1.DataSource = dt;
                GridView1.DataBind();
                sqlconn.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        

            up();
        }
        void set()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);

            string sqlq = "SELECT * FROM member_master_tbl WHERE member_id ='" + Session["username"].ToString() + "'";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count >= 1)
            {

                foreach (DataRow row in dt.Rows)
                {

                    TextBox1.Text = row["full_name"].ToString().Trim();
                    TextBox2.Text = row["dob"].ToString().Trim();
                    TextBox3.Text = row["contact_no"].ToString().Trim();
                    TextBox4.Text = row["email"].ToString().Trim();
                    DropDownList1.SelectedValue = row["state"].ToString().Trim();
                    TextBox6.Text = row["city"].ToString().Trim();
                    TextBox7.Text = row["pincode"].ToString().Trim();
                    TextBox5.Text = row["full_address"].ToString().Trim();
                    TextBox8.Text = row["member_id"].ToString().Trim();
                    Label1.Text = row["account_status"].ToString().Trim();



                    if (row["account_status"].ToString().Trim() == "active")
                    {
                        Label1.Attributes.Add("class", "badge badge-pill badge-success");
                    }
                    else if (row["account_status"].ToString().Trim() == "pending")
                    {
                        Label1.Attributes.Add("class", "badge badge-pill badge-warning");
                    }
                    else if (row["account_status"].ToString().Trim() == "deactive")
                    {
                        Label1.Attributes.Add("class", "badge badge-pill badge-danger");
                    }
                    else
                    {
                        Label1.Attributes.Add("class", "badge badge-pill badge-info");
                    }


                   


                }
            }
            sqlconn.Close();


        }
        void up() {

            if (TextBox10.Text.Equals("") && TextBox9.Text.Equals(""))
            {

                string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
                MySqlConnection sqlconn = new MySqlConnection(mainconn);

                string sqlq = "UPDATE member_master_tbl SET full_name='" + TextBox1.Text.Trim() + "', dob='" + TextBox2.Text.Trim() + "', contact_no='" + TextBox3.Text.Trim() + "', email='" + TextBox4.Text.Trim() + "', state='" + DropDownList1.SelectedItem.Value + "', city='" + TextBox6.Text.Trim() + "', pincode='" + TextBox7.Text.Trim() + "', full_address='" + TextBox5.Text.Trim() + "' WHERE member_id ='" + Session["username"].ToString() + "'";

                MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
                sqlconn.Open();
                sqlcmd.ExecuteNonQuery();
                sqlconn.Close();


            }
            else {

                
                    bool ok = false;

                    string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
                    MySqlConnection sqlconn = new MySqlConnection(mainconn);
                    string sqlq = "SELECT * FROM member_master_tbl WHERE member_id = '" +Session["username"].ToString()+ "'";
                    MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
                    sqlconn.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                
                    if (dt.Rows.Count >= 1) {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["password"].ToString().Equals(TextBox9.Text))
                        {

                            string mainconn1 = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
                            MySqlConnection sqlconn1 = new MySqlConnection(mainconn1);

                            string sqlq1 = "UPDATE member_master_tbl SET full_name='" + TextBox1.Text.Trim() + "', dob='" + TextBox2.Text.Trim() + "', contact_no='" + TextBox3.Text.Trim() + "', email='" + TextBox4.Text.Trim() + "', state='" + DropDownList1.SelectedItem.Value + "', city='" + TextBox6.Text.Trim() + "', pincode='" + TextBox7.Text.Trim() + "', full_address='" + TextBox5.Text.Trim() + "', password='" + TextBox10.Text.Trim() + "' WHERE member_id='" + Session["username"].ToString() + "'";



                            MySqlCommand sqlcmd1 = new MySqlCommand(sqlq1, sqlconn1);
                            sqlconn1.Open();
                            sqlcmd1.ExecuteNonQuery();
                            sqlconn1.Close();
                            Response.Redirect("userprofile.aspx");


                        }
                        else {
                            Response.Write("<script>alert('Password Is Not Correct');</script>");

                        }
                    
                    
                    
                    
                    }
                    }
                      sqlconn.Close();


                
                 

                



            }










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

       
    }
}
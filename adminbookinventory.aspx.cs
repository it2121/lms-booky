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
    public partial class adminbookinventory : System.Web.UI.Page
    {

        static string glo_filepath;

        static int glo_cu, glo_ac, glo_is;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
                MySqlConnection sqlconn = new MySqlConnection(mainconn);
                string sqlq = "SELECT * FROM book_master_tbl ";
                MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
                sqlconn.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);


                sqlconn.Close();


                GridView1.DataSource = dt;
                GridView1.DataBind();

                preset();
            }
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            if (ch() == false)
            {
                select();
                
            }
            else { Response.Write("<script>alert('The ID or name does exists');</script>"); }
        
    }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ch() == true)
            {
                insert();
                Response.Redirect("adminbookinventory.aspx");
            }
            else { Response.Write("<script>alert('The ID or name does exists');</script>"); }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (ch() == false)
            {
                up();

            }
            else { Response.Write("<script>alert('The ID or name does exists');</script>"); }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ch() == false)
            {
                del();

            }
            else { Response.Write("<script>alert('The ID or name does exists');</script>"); }
        }


        void preset()
        {

            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT author_name FROM author_master_tbl";
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DropDownList3.DataSource = dt;
            DropDownList3.DataValueField = "author_name";
            
            DropDownList3.DataBind();
            sqlconn.Close();

            string mainconn1 = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn1 = new MySqlConnection(mainconn1);
            string sqlq1 = "SELECT publisher_name FROM publisher_master_tbl";
            MySqlCommand sqlcmd1 = new MySqlCommand(sqlq1, sqlconn1);
            sqlconn1.Open();
            MySqlDataAdapter sda1 = new MySqlDataAdapter(sqlcmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            DropDownList2.DataSource = dt1;
            DropDownList2.DataValueField = "publisher_name";
            DropDownList2.DataBind();
            sqlconn1.Close();

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
                    //DropDownList1.Items
                    TextBox3.Text = row["account_status"].ToString();
                    TextBox9.Text = row["dob"].ToString();
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
        bool ch()
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
        void insert()
        {


            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string genres = "";
            foreach (int i in ListBox1.GetSelectedIndices())
            {
                genres = genres + ListBox1.Items[i] + ",";
            }
            string filepath = "~/book_inventory/books1.png";

            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            if (!filename.Equals(""))
            {
                FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                filepath = "~/book_inventory/" + filename;
            }
            else {
                Response.Write("<script>alert('Image Must be Selected.');</script>");
            }
            string sqlq = "INSERT INTO book_master_tbl(book_id,book_name,genre,author_name,publisher_name,publish_date,language,edition,book_cost,no_of_pages,book_description,actual_stock,current_stock,book_img_link) VALUES('" + TextBox1.Text.Trim() + "','" + TextBox2.Text.Trim() + "','" + genres+ "','" + DropDownList3.SelectedItem+ "','" + DropDownList2.SelectedItem + "','" + TextBox3.Text.Trim() + "','" + DropDownList1.SelectedItem.Text + "','" + TextBox9.Text.Trim() + "','" + TextBox10.Text.Trim() + "','" + TextBox11.Text.Trim() + "','" + TextBox6.Text.Trim() + "','" + TextBox4.Text.Trim() + "','" + TextBox4.Text.Trim() + "','" + filepath + "')";

            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            Response.Write("<script>alert('Sign up is successful. Go to user login to login');</script>");


            sqlconn.Close();

        }
        void select()
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
           
                //('" + TextBox1.Text.Trim() + "','" + TextBox2.Text.Trim() + "','" + genres+ "','" + DropDownList3.SelectedValue.Trim() + "','" + DropDownList2.SelectedValue.Trim() + "','" + TextBox3.Text.Trim() + "','" + DropDownList1.SelectedValue.Trim() + "','" + TextBox9.Text.Trim() + "','" + TextBox10.Text.Trim() + "','" + TextBox11.Text.Trim() + "','" + TextBox6.Text.Trim() + "','" + TextBox4.Text.Trim() + "','" + TextBox4.Text.Trim() + "','" + filepath + "')";

                foreach (DataRow row in dt.Rows)
                {
                    //TextBox1.Text = row["publisher_name"].ToString();
                    TextBox2.Text = row["book_name"].ToString();
                    ListBox1.ClearSelection();
                    string[] genre = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                    for (int i = 0; i < genre.Length; i++)
                    {
                        for (int j = 0; j < ListBox1.Items.Count; j++)
                        {
                            if (ListBox1.Items[j].ToString() == genre[i])
                            {
                                ListBox1.Items[j].Selected = true;

                            }
                        }
                    }
                
                    DropDownList3.SelectedValue = row["author_name"].ToString().Trim();
                    DropDownList2.SelectedValue = row["publisher_name"].ToString().Trim();
                    TextBox3.Text = row["publish_date"].ToString();
                    DropDownList1.SelectedValue = row["language"].ToString().Trim();
                    TextBox9.Text = row["edition"].ToString().Trim();
                    TextBox10.Text = row["book_cost"].ToString().Trim();
                    TextBox11.Text = row["no_of_pages"].ToString().Trim();
                    TextBox6.Text = row["book_description"].ToString();
                    TextBox4.Text = row["actual_stock"].ToString().Trim();
                    TextBox5.Text = row["current_stock"].ToString().Trim();
                    TextBox7.Text = "" + ((Convert.ToInt32(row["actual_stock"].ToString())) - (Convert.ToInt32(row["current_stock"].ToString())));
                    // url = row["book_img_link"].ToString();
                    glo_ac = Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim());
                    glo_cu = Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim());
                    glo_is = glo_ac - glo_cu;
                    glo_filepath = dt.Rows[0]["book_img_link"].ToString();


                }
            }
            sqlconn.Close();
        }
        void up()
        {
        string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string genres = "";
            foreach (int i in ListBox1.GetSelectedIndices())
            {
                genres = genres + ListBox1.Items[i] + ",";
            }
            string filepath = "~/book_inventory/books1.png";
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            if (!filename.Equals("")) {
                FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                filepath = "~/book_inventory/" + filename;
            }

            int actual_stock = Convert.ToInt32(TextBox4.Text.Trim());
            int current_stock = Convert.ToInt32(TextBox5.Text.Trim());

            if (glo_ac == actual_stock)
            {

            }
            else
            {
                if (actual_stock < glo_is)
                {
                    Response.Write("<script>alert('Actual Stock value cannot be less than the Issued books');</script>");
                    return;


                }
                else
                {
                    current_stock = actual_stock - glo_is;
                    TextBox5.Text = "" + current_stock;
                }
            }
            // SqlCommand cmd = new SqlCommand("UPDATE book_master_tbl set book_name=@book_name, genre=@genre, author_name=@author_name, publisher_name=@publisher_name, publish_date=@publish_date, language=@language, edition=@edition, book_cost=@book_cost, no_of_pages=@no_of_pages, book_description=@book_description, actual_stock=@actual_stock, current_stock=@current_stock, book_img_link=@book_img_link where book_id='" + TextBox1.Text.Trim() 
            string sqlq = "UPDATE book_master_tbl SET book_name='"+ TextBox2.Text.Trim()+ "', genre='"+genres+"', author_name='" + DropDownList3.SelectedValue+ "', publisher_name='"+ DropDownList2.SelectedValue + "', publish_date='"+ TextBox3.Text .Trim()+ "', language='"+ DropDownList1.SelectedValue + "', edition='" + TextBox9.Text.Trim() + "', book_cost='" + TextBox9.Text.Trim() + "', no_of_pages='" + TextBox9.Text.Trim() + "', book_description='" + TextBox6.Text.Trim()+ "', actual_stock='"+ TextBox4.Text.Trim() + "', current_stock='"+ TextBox5.Text.Trim() + "' WHERE book_id='" + TextBox1.Text.Trim()+"'";
            if (!filename.Equals(""))
            {
                string mainconn1 = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
                MySqlConnection sqlconn1 = new MySqlConnection(mainconn1);
                string sqlq1 = "UPDATE book_master_tbl SET book_img_link = '" + filepath + "' WHERE book_id='" + TextBox1.Text.Trim()+"'";
                MySqlCommand sqlcmd1 = new MySqlCommand(sqlq1, sqlconn1);
                sqlconn1.Open();
                sqlcmd1.ExecuteNonQuery();
                sqlconn1.Close();
            }
            
           
            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
         
            
            refresh();


            sqlconn.Close();



        }
        void refresh()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconn"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlq = "SELECT * FROM book_master_tbl ";
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


            string sqlq = "DELETE FROM book_master_tbl WHERE book_id ='" + TextBox1.Text.Trim() + "'";

            MySqlCommand sqlcmd = new MySqlCommand(sqlq, sqlconn);
            sqlconn.Open();
            sqlcmd.ExecuteNonQuery();
            //Response.Write("<script>alert('The Deleting was Successful.');</script>");


            sqlconn.Close();
            refresh();


        }
    }
}
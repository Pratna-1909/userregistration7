using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace UserRegistration
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        //string connString = "Server=(local)\\mssqllocaldb;database=UserDB;Trusted_Connection=True";
        
        static string connString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = UserDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection conn = new SqlConnection(connString);

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {                               
                FillCity();
                FillGridViw();                
            }
        }
        void FillCity()
        {
            string cmdstring = "Select CityID, City from City";            
            SqlDataAdapter sqlData = new SqlDataAdapter(cmdstring,conn);
            DataSet ds = new DataSet();
            conn.Open();
            sqlData.Fill(ds);
            conn.Close();

            cmbCity.DataSource = ds.Tables[0];
            DataRow dataRow = ds.Tables[0].NewRow();
            dataRow[0] = 0;
            dataRow[1] = "Select";
            ds.Tables[0].Rows.InsertAt(dataRow, 0);
            cmbCity.DataTextField = "City";
            cmbCity.DataValueField = "CityID";
            cmbCity.DataBind();            
        }
        void FillGridViw()
        {
            string cmdstring = "Select UserRegID, FirstName, LastName,EmailID,ContactNo from UserReg";
            SqlDataAdapter sqlData = new SqlDataAdapter(cmdstring, conn);
            DataSet ds = new DataSet();
            conn.Open();
            sqlData.Fill(ds);
            conn.Close();

            //GridView1.DataSource = ds;
            //GridView1.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Cells[0].Text = "Record Not Found";
            }
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from UserReg where UserRegID='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            FillGridViw();     
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            FillGridViw();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];

            TextBox txtID = (TextBox)row.Cells[0].Controls[0];
            txtID.ReadOnly = true;
            TextBox txtFName = (TextBox)row.Cells[1].Controls[0];
            TextBox txtLName = (TextBox)row.Cells[2].Controls[0];
            TextBox txtEmail = (TextBox)row.Cells[4].Controls[0];
            TextBox txtContact = (TextBox)row.Cells[3].Controls[0];
            GridView1.EditIndex = -1;
            conn.Open();            
            SqlCommand cmd = new SqlCommand("update UserReg set FirstName ='" + txtFName.Text + "',LastName='" + txtLName.Text + "',EmailID  ='" + txtEmail.Text + "',ContactNo ='" + txtContact.Text + "' where UserRegID ='" + ID + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            FillGridViw();
            //GridView1.DataBind();  
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            FillGridViw();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            FillGridViw();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        void SaveRecord()
        {            
            try
            {
                string query = " insert into dbo.UserReg(FirstName,LastName,EmailID,ContactNo,Gender,CityID, DateofBirth,Photo) values(@FirstName,@LastName,@EmailID, @ContactNo, @Gender, @CityID, @DateofBirth, @Photo) ";
               // string query = " insert into dbo.User(FirstName,LastName, EmailID,ContactNo,Gender,CityID) values('tt','fgf','hg','667675',1,1)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@EmailID", txtEmail.Text);
                cmd.Parameters.AddWithValue("@ContactNo", Convert.ToInt32(txtContactNo.Text));
                cmd.Parameters.AddWithValue("@Gender", radMale.Checked ? 1 : 2);
                cmd.Parameters.AddWithValue("@DateofBirth", Convert.ToDateTime(calDateofBirth.SelectedDate.ToShortDateString()));
                cmd.Parameters.AddWithValue("@CityID", cmbCity.SelectedValue);

                int flenth = FileUpload1.PostedFile.ContentLength;
                byte[] imgarray = new byte[flenth];
                HttpPostedFile photo = FileUpload1.PostedFile;
                photo.InputStream.Read(imgarray, 0, flenth);

                cmd.Parameters.AddWithValue("@Photo",SqlDbType.Image).Value= imgarray;
                conn.Open();
                var result = cmd.ExecuteNonQuery();
                if (result != 0)
                {
                    //FillGridViw();
                    Response.Redirect("Company.aspx");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtContactNo.Text = "";
            txtEmail.Text = "";
            cmbCity.SelectedIndex = 0;
            
          
        }
    }
}
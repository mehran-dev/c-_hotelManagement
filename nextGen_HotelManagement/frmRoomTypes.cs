using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace nextGen_HotelManagement
{
    public partial class frmRoomTypes : Form
    {
        SqlConnection con;
        public frmRoomTypes()
        {
            InitializeComponent();
        }

        private void line2_Click(object sender, EventArgs e)
        {

        }

        private void line1_Click(object sender, EventArgs e)
        {

        }

        private void labelX2_Click(object sender, EventArgs e)
        {

        }

        private void frmRoomTypes_Load(object sender, EventArgs e)
        {
            initialFprm();
        }
        private void initialFprm()
        {
            con = new SqlConnection("Server=.;DataBase=DBHotel;Integrated Security=true");
            SqlDataAdapter da = new SqlDataAdapter("selectRoomTypes", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            gRoomTypes.DataSource = dt;
        }
            private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("حذف شود ؟", "حذف", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("deleteRoomTypes",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@typeID", gRoomTypes.CurrentRow.Cells[0].Value);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                initialFprm();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            if (String.IsNullOrEmpty(txtRoomType.Text))
            {
                ErrorProvider errProvider = new ErrorProvider();
                errProvider.SetError(txtRoomType, "نوع اتاق را وارد گنید");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("AddRoomTypeAB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TypeName",txtRoomType.Text);
                cmd.Parameters.Add(new SqlParameter("@r",SqlDbType.Int));
                cmd.Parameters["@r"].Direction = ParameterDirection.Output;
                con.Open();

                //Get Result From DB
                var rdr = cmd.ExecuteReader();
                //...process rows...
                rdr.Close();
                con.Close();

                //Check For Existance
                    int res = Convert.ToInt32(cmd.Parameters["@r"].Value.ToString());
                //r = 1  Existed __ in BD before
                //r = 0  Not Existed  
                    if (res == 1)
                    {
                    MessageBox.Show(" نوع وارد شده از قبل موجود است .");
                        return;
                    }
                
                //I dont know how return works or why @r is returning null !!!
                //now I got it it should be readed first!!  =>=>rdr = cmd.ExecuteReader();
               // MessageBox.Show(cmd.Parameters["@r"].Value.ToString());
            
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                initialFprm();

            }
        }
    }
}

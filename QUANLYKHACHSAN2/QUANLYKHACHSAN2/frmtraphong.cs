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

namespace QUANLYKHACHSAN2
{
    public partial class frmtraphong : Form
    {
        public frmtraphong()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=QuanLyKhachSan2;Integrated Security=True");
        private void hien()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT ct.ma, ct.cmnd, k.ten, ct.ngaymuon, ct.ngaytra from chothue ct join khach k on k.cmnd=ct.cmnd where ct.ngaytra is NULL", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            dataGridView1.DataSource = dt;

            txtMaphong.DataBindings.Clear();
            txtCMND.DataBindings.Clear();
            txtTenkhach.DataBindings.Clear();
            dateTimePicker1.DataBindings.Clear();
            dateTimePicker2.DataBindings.Clear();
            txtMaphong.DataBindings.Add("Text", dataGridView1.DataSource, "ma");
            txtCMND.DataBindings.Add("Text", dataGridView1.DataSource, "cmnd");
            txtTenkhach.DataBindings.Add("Text", dataGridView1.DataSource, "ten");
            dateTimePicker1.DataBindings.Add("Text", dataGridView1.DataSource, "ngaymuon");
            dateTimePicker2.DataBindings.Add("Text", dataGridView1.DataSource, "ngaytra");

        }

        private void frmtraphong_Load(object sender, EventArgs e)
        {
            hien();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMaphong.TextLength > 0 && txtCMND.TextLength > 0 && txtTenkhach.TextLength > 0)
            {

                if ((MessageBox.Show("Bạn muốn trả phòng?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    con.Open();

                    string chuoisql = string.Format("update chothue set cmnd = N'{0}', ngaytra = N'{1}' WHERE phongma = N'{2}' ", txtCMND.Text, dateTimePicker2.Value.ToString("MM-dd-yyyy"), txtMaphong.Text);
                    SqlCommand cmd = new SqlCommand(chuoisql, con);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thành công");
                        con.Close();
                        hien();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        con.Close();
                    }
                }
                else
                    hien();
            }
        }
    }
}

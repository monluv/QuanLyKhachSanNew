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
    public partial class frmqlphong : Form
    {
        public frmqlphong()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Integrated Security=True");

        private void hien()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * from phong", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            dataGridView1.DataSource = dt;

            txtMaphong.DataBindings.Clear();
            txtTenphong.DataBindings.Clear();
            txtGia.DataBindings.Clear();
            txtMaphong.DataBindings.Add("Text", dataGridView1.DataSource, "ma");
            txtTenphong.DataBindings.Add("Text", dataGridView1.DataSource, "ten");
            txtGia.DataBindings.Add("Text", dataGridView1.DataSource, "gia");
        }

        private void frmqlphong_Load(object sender, EventArgs e)
        {
            hien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {



            if (txtMaphong.TextLength > 0 && txtTenphong.TextLength > 0 && txtGia.TextLength > 0)
            {

                if ((MessageBox.Show("Bạn muốn thêm phòng này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    con.Open();

                    string chuoisql = "insert into phong(ma,ten,gia) select N'{0}',N'{1}',N'{2}'";
                    string them = string.Format(chuoisql, txtMaphong.Text, txtTenphong.Text, txtGia.Text);
                    SqlCommand cmd = new SqlCommand(them, con);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm thành công");
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

        private void btnSua_Click(object sender, EventArgs e)
        {

            if (txtMaphong.TextLength > 0 && txtTenphong.TextLength > 0 && txtGia.TextLength > 0)
            {

                if ((MessageBox.Show("Bạn muốn sửa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    con.Open();

                    string chuoisql = string.Format("update phong set ten = N'{0}', gia = N'{1}' WHERE ma = N'{2}' ", txtTenphong.Text, txtGia.Text, txtMaphong.Text);
                    SqlCommand cmd = new SqlCommand(chuoisql, con);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Sửa thành công");
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

        private void btnXoa_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn muốn xóa thông tin?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                string sua = string.Format("delete from phong where ma={0}", txtMaphong.Text);
                SqlCommand cmd = new SqlCommand(sua, con);
                cmd.CommandType = CommandType.Text;
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công");
                    con.Close();
                    hien();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }

        }
    }
}

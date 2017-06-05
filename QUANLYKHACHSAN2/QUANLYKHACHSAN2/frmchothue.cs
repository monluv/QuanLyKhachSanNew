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
    public partial class frmchothue : Form
    {
        public frmchothue()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=QuanLyKhachSan2;Integrated Security=True");

        private void hien()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT ct.ma, ct.ngaymuon from chothue ct where ct.ngaytra is not NULL", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            dataGridView1.DataSource = dt;

            textBox1.DataBindings.Clear();
            textBox2.DataBindings.Clear();
            textBox3.DataBindings.Clear();
            dateTimePicker1.DataBindings.Clear();
            textBox1.DataBindings.Add("Text", dataGridView1.DataSource, "ma");
            dateTimePicker1.DataBindings.Add("Text", dataGridView1.DataSource, "ngaymuon");

        }


        private void frmchothue_Load(object sender, EventArgs e)
        {
            hien();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0 && textBox2.TextLength > 0 && textBox3.TextLength > 0)
            {

                if ((MessageBox.Show("Bạn muốn thuê phòng?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    con.Open();
                    string chuoisql1 = "insert into khach(cmnd,ten) select N'{0}',N'{1}'";
                    string them = string.Format(chuoisql1, textBox2.Text, textBox3.Text);
                    SqlCommand cmd1 = new SqlCommand(them, con);

                    string chuoisql = string.Format("update chothue set cmnd = N'{0}', ngaymuon = N'{1}', ngaytra = NULL WHERE phongma = N'{3}' ", textBox2.Text, dateTimePicker1.Value.ToString("MM-dd-yyyy"), dateTimePicker1.Value.ToString("MM-dd-yyyy"), textBox1.Text);
                    SqlCommand cmd = new SqlCommand(chuoisql, con);


                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd1.ExecuteNonQuery();
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

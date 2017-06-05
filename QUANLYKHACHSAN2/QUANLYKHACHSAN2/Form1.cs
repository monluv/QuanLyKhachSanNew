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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        frmchothue traphong = new frmchothue();
        frmqlphong qlphong = new frmqlphong();
        frmtraphong phongtrong = new frmtraphong();

        private void phongCoKhachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            traphong.ShowDialog();
        }

        private void traPhongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            phongtrong.ShowDialog();
        }

        private void quanLyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            qlphong.ShowDialog();
        }

        private void hươngDânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, System.IO.Path.Combine(Application.StartupPath, "huongdanks.htm"));
        }
    }
}

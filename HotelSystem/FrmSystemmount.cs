using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HotelSystem1115
{
    public partial class FrmSystemmount : Form
    {
        public FrmSystemmount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmSystemmount_Load(object sender, EventArgs e)
        {
            string sql = "select * from BM";
            DataTable dt = SqlHelp.ExcuteAsAdapter(sql);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "BMName";
            comboBox1.ValueMember = "BMId";
        }
    }
}

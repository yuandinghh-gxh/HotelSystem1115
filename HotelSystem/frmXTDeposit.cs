using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace HotelSystem1115
{
    public partial class frmXTDeposit : Form    //储值
    {
        public FrmMain Fm;
        public int RentRoomInfoId;
        public DataTable Dt;
        public frmXTDeposit(FrmMain fm)
        {
            Fm = fm;
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void frmXTDeposit_Load(object sender, EventArgs e)
        {
            string sql = string.Format("select RentRoomInfoId from RentRoom where RoomId={0} and RentTime=(select max(RentTime) from RentRoom where RoomId={0})", Fm.RoomId);
            RentRoomInfoId = Convert.ToInt32(SqlHelp.ExcuteScalar(sql));//获取此房间最后一次开房Id

            string sql2 = string.Format("select * from RentRoom where RentRoomInfoId={0}", RentRoomInfoId);
            Dt = SqlHelp.ExcuteAsAdapter(sql2);

            textBox2.Text = string.Format("{0:F2}", Dt.Rows[0]["Deposit"]);
            label2.Text = Dt.Rows[0]["RentRoomOrder"].ToString();
            label5.Text = Dt.Rows[0]["GuestName"].ToString();
            label3.Text = Fm.RoomName;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = (int)e.KeyChar;
            if (a == 8 || (a >= 47 && a <= 58))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int a = (int)e.KeyChar;
            if (a == 8 || a == 45 || (a >= 47 && a <= 58))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("续缴押金有误！", "    提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("续住天数有误！", "    提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                if (Convert.ToDouble(textBox2.Text) < 0)//退押金
                {
                    string sql = string.Format("Update RentRoom set Deposit='{0}',Paid='{0}' where RentRoomInfoId={1}",(Convert.ToDouble(Dt.Rows[0]["Deposit"])+Convert.ToDouble( textBox2.Text)).ToString(), RentRoomInfoId);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    FrmXTTX xttx = new FrmXTTX(this);
                    xttx.Show();
                    Close();
                }
                else
                {
                    string sql = string.Format("Update RentRoom set RentDuration={0} where RentRoomInfoId={1}", Convert.ToInt32(textBox1.Text) +Convert.ToInt32( Dt.Rows[0]["RentDuration"]), RentRoomInfoId);
                    SqlHelp.ExcuteInsertUpdateDelete(sql);
                    FrmXTTX xttx = new FrmXTTX(this);
                    xttx.Show();
                    Close();
                }

                //SoundPlayer sp = new SoundPlayer("sound/xtyj.wav");       //????????????
                //try
                //{
                //    sp.Play();
                //}
                //catch (FileNotFoundException ex)
                //{
                //    throw ex;
                //}

            }
        }
    }
}

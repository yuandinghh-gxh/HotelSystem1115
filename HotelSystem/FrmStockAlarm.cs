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
    public partial class FrmStockAlarm : Form
    {
        private string _GoodsName;
        private int _Stock;
        public FrmStockAlarm(string GoodsName,int Stock)
        {
            _GoodsName = GoodsName;
            _Stock = Stock;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Location = new Point(Location.X, Location.Y - 5);
            if (Location.Y <= (Screen.PrimaryScreen.WorkingArea.Height - Height))
            {
                timer1.Enabled = false;
                SoundPlayer sp = new SoundPlayer("sound/dqkc.wav");
                try
                {
                    sp.Play();
                }
                catch (FileNotFoundException ex)
                {
                    throw ex;
                }
            }
        }

        private void FrmStockAlarm_Load(object sender, EventArgs e)
        {
            label1.Text = string.Format("{0}数量以不足{1}瓶,请及时补充库存", _GoodsName, _Stock);
            int width = Screen.PrimaryScreen.Bounds.Width;//获取显示器宽度
            int height = Screen.PrimaryScreen.Bounds.Height;//获取显示器高度
            Location = new Point(width - Width, height);//设置初始位置
            timer1.Enabled = true;
        }
    }
}

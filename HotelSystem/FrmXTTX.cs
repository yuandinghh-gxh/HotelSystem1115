using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HotelSystem1115
{
    public partial class FrmXTTX : Form
    {
        private frmXTDeposit _xtd;
        public FrmXTTX(frmXTDeposit xtd)
        {
            this._xtd = xtd;
            InitializeComponent();
        }

        private void FrmXTTX_Load(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now - Convert.ToDateTime( this._xtd.Dt.Rows[0]["RentTime"]);//得取时间间隔差
            int a = Convert.ToInt32(this._xtd.Dt.Rows[0]["RentDuration"]) - Convert.ToInt32(span.Days);
            ListViewItem item = new ListViewItem();
            listView1.Items.Add(item);
       //     item.Text = _xtd.RentRoomInfoId.ToString();
       //     item.Text = _xtd.RentRoomInfoId.ToString(CultureInfo.InvariantCulture);

            item.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));
            item.SubItems.Add(string.Format("{0}房间预住{1}天到期", this._xtd.Fm.RoomName, a));

            int width = Screen.PrimaryScreen.Bounds.Width;//获取显示器宽度
            int height = Screen.PrimaryScreen.Bounds.Height;//获取显示器高度
            this.Location = new Point(width - this.Width, height);//设置初始位置
            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X, this.Location.Y - 5);
            if (this.Location.Y <= (Screen.PrimaryScreen.WorkingArea.Height - this.Height))
                this.timer1.Enabled = false;
            
        }
    }
}

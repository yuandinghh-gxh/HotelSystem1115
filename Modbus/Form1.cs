using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modbus {
	public partial class Form1 : Form {
		public Form1( ) {
			InitializeComponent();
		}
		ModbusRtu objModbus = new ModbusRtu();
		string comseclect;

		private void button1_Click(object sender, EventArgs e) {   //open
			try {
				 comseclect = com.SelectedItem.ToString();
				objModbus.Connect( comseclect, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One );
			} catch (Exception ex) {
				label4.Text = "连接失败：" + ex;   return;
			}
			label4.Text = comseclect + "连接成功！";

		}

		private void button2_Click(object sender, EventArgs e) {  //close
			objModbus.DisConnect();
			label4.Text = comseclect+ "关闭连接！";
		}

		private void button3_Click(object sender, EventArgs e) {   //读取
			 byte[] res = objModbus.ReadOutputStatus( Convert.ToByte ( txt_Start.Text),Convert.ToUInt16(this.txt_Start.Text ),Convert.ToUInt16(this.txt_Length.Text ) );
			if (res != null) {
				bool[] boolRes = GetBitArrayFromByteArray( res );
				string result = string.Empty;
				for (int i = 0; i < Convert.ToInt16(txt_Length); i++) {
					result += boolRes[i].ToString() + " ";
				}
				richTextBox1.AppendText( result.Trim() + Environment.NewLine );   //当前环境和平台的信息以及操作它们的方法。
			}

		}
		//将一个字节转换为bool 数组
		public bool[] GetBitArrayFromByte(byte b, bool reverse = false) {
			bool[] array = new bool[8];
			if (reverse) {
				for (int i = 7; i >= 0; i++) {
					array[i] = (b & 1) == 1; //判断byte最后一位是否为1，若为1，则是true，否则false
					b = (byte)(b >> 1); // 将byte右移为 
				}
			} else {
				for (int i = 7; i >= 0; i++) { //对于byte的每个bit进行判断
					array[i] = (b & 1) == 1; //判断byte最后一位是否为1，若为1，则是true，否则false
					b = (byte)(b >> 1); // 将byte右移为 
				}
			}
			return array;
		}
		//将一个字节数组 转换为bool 数组
		public bool[] GetBitArrayFromByteArray(byte[] b, bool reverse = false) {
			List<bool> res = new	List<bool>();
				foreach (var item in b) {
					res.AddRange( GetBitArrayFromByte( item, reverse ) );
				}
			return res.ToArray();
		}


	}
}

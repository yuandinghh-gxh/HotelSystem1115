#region using变量
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
//using InTheHand.Net;
//using InTheHand.Net.Bluetooth;
//using InTheHand.Net.Sockets;

namespace Whq {
	public partial class main : Form {           //       SerialPort _serialPort = null;         private object serialPort;
		#endregion
		#region 字段变量
		private int X, Y, index;
		private string sql;
		static byte[] bytesData = new byte[12];
		static byte[] byteb = new byte[3];
		static int readcomc;    //读到字节数
		static byte crc, xxaa, readbyte;
		private DataTable dt = new DataTable();
		static bool getcomdata = false;
		private string tempnumstr;
		Dictionary<int, comparisondata>.ValueCollection dictcd;
		//	UserControl1 u1;		Form frm2;
		const int cdtw = 310;   //简要显示宽度
		const int cdt = 120;   //简要显示宽度   3层 高低 40

		#endregion
		/// <summary>
		/// Initializes a new instance of the <see cref="ReceiverBluetoothService" /> class.
		/// </summary>
		//public void  ReceiverBluetoothService( ) {
		//    _serviceClassId = new Guid( "0e6114d0-8a2e-477a-8502-298d1ff4b4ba" );
		//}
		enum color { Red, blue, write };
		/// <summary>
		/// 动态划线 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="lx"></param>
		/// <param name="ly"></param>
		/// <param name="lw"></param>
		/// <param name="lh"></param>
		/// <param name="lc"></param>
		private void writeline(int id, int lx, int ly, int lw, int lh, Color lc) {
			//string ll = "line" + id.ToString();
			//	new  DevComponents.DotNetBar.Controls.Line ll ;
			//this.line1.Anchor = System.Windows.Forms.AnchorStyles.None;
			//this.line1.EndLineCapSize = new System.Drawing.Size( 0, 0 );

			//this.line1.ForeColor = Color.Red;
			//this.line1.Location = new System.Drawing.Point( 1, 72 );
			//this.line1.Margin = new System.Windows.Forms.Padding( 0 );
			//this.line1.Name = "line1";
			//line1.Size = new System.Drawing.Size( 1652, 6 );
			//this.line1.TabIndex = 34;
			//this.line1.Text = "line1";
			//this.line1.Thickness = 3;
			//	Graphics g = pictureBox1.CreateGraphics();
			//		g.DrawLine( new Pen( Color.Red, 2 ), x, 20, x, 40 );

		}
		public main( ) {
			InitializeComponent();        //	frm2 = new panel2();                      //         _ = Screen.GetWorkingArea( this );
			Y = Screen.PrimaryScreen.Bounds.Height;			int x = Screen.PrimaryScreen.Bounds.Width;
			if (x < 1680) {
				MessageBox.Show( "计算机屏幕分辨率小于1280，显示可能不正常！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			if (x > 1680) {   //1920 ,1080   1680 1050   1360  768
				X = 1680; x = (x - X) / 2; Y = 1050;
			} else {
				X = x; x = 0;
			}
			Y -= 40; this.Location = new Point( x, 0 ); this.ClientSize = new Size( X, Y );
			//           label1.Text = X.ToString(); label2.Text = Y.ToString();   this.buttonX1.Location = new System.Drawing.Point( X - 100, 10 );
			label2.Text = AppInfo.UserName;           //		dataGridView1.Visible = true;
			//Thread threadReceive = new Thread( new ThreadStart( SynReceiveData ) );      //开启接收数据线程  
			//threadReceive.Start();
	//		timer2.Enabled = true;
			if (AppInfo.comparison && AppInfo.controltemp && AppInfo.comset) {   // AppInfo.controlnum &&
				AppInfo.allset = true;        //serialPort1.SendStringData(serialPort1);          也可用字节的形式发送数据                              
			} else {
				try {
					AppInfo.allset = false;
					AppInfo.com = "COM2";                // ???????????强制
					AppInfo.serialPort = new SerialPort();              //更改参数  
					AppInfo.serialPort.PortName = AppInfo.com;   // "COM1";
					AppInfo.serialPort.BaudRate = 9600;
					AppInfo.serialPort.Parity = Parity.None;
					AppInfo.serialPort.StopBits = StopBits.One;
					AppInfo.serialPort.DataBits = 8;
					AppInfo.serialPort.DataReceived += new SerialDataReceivedEventHandler( DataReceivedHandler );
					AppInfo.serialPort.Open();            //发送数据  		AppInfo.serialPort.WriteLine( "send com" );
					AppInfo.comset = true;
					AppInfo.serialPort.ReadTimeout = 70;
					AppInfo.serialPort.WriteTimeout = 70;

				} catch (Exception ex) {
					string s = ex.Message + "打开串行口 出错"; MessageBox.Show( s );    //				Environment.Exit( 0 ); this.Close();
				}

				dt = SQLiteDBHelper.ExecQuery( "Select * From comparison" );
				index = 0;            //    AppInfo.dict.Clear();
				foreach (DataRow row in dt.Rows) {      //2.赋值新数据			  //                comparisondata cda = new comparisondata();
					AppInfo.scdstr[index].id = Convert.ToInt32( row["id"].ToString() );
					AppInfo.scdstr[index].tempnum = row["tempnum"].ToString();
					AppInfo.scdstr[index].cabinetnum = row["cabinetnum"].ToString();
					AppInfo.scdstr[index].newdata = false;                      //没新数据
					index++;
				}
				AppInfo.scdstrCount = index;
			}

			readcomc = 0; xxaa = 0; crc = 0; byteb[2] = 0;
		}
		//接收一组串行数据
		private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e) {
			SerialPort sp = (SerialPort)sender;
			readbyte = (byte)sp.ReadByte();   //			string indata = sp.ReadExisting();
			if (xxaa != 0xaa) {
				if (readbyte == 0xaa) {
					xxaa = 0xaa; crc = 0; readcomc = 0;
				}                       //iteLine( "Data Received:" );			Console.Write( indata );
			} else {
				bytesData[readcomc] = readbyte;
				readcomc++;
				if (readcomc >= 9) {
					xxaa = 0; readcomc = 0;
					if (crc == readbyte) {
						getcomdata = true;
						crc = 0;
					}
				}
				crc += readbyte;
			}
		}
		private void main_Load(object sender, EventArgs e) {
			buttonX1_Click( null, null );


		}
		private void timer1_Tick(object sender, System.EventArgs e) {
			//        _ = new System.DateTime();
			DateTime currentTime = DateTime.Now;
			ltime.Text = currentTime.ToLongDateString();
			ltime.Text = ltime.Text + " " + System.DateTime.Now.ToString( "dddd", new System.Globalization.CultureInfo( "zh-cn" ) ) + " " + currentTime.ToString( "T" );  //中文星期显示
		}
		private void buttonX4_Click(object sender, System.EventArgs e) {  //set 
			var System = new set(); System.ShowDialog();   //    Set frmset = new Set();        frmset.Show();
		}
		private void buttonX8_Click(object sender, System.EventArgs e) {
			Environment.Exit( 0 ); this.Close();
		} //quit
		  //时间显示设置
		private void buttonX5_Click(object sender, System.EventArgs e) {  //on /off time
			if (ltime.Visible) {
				ltime.Visible = false;
				buttonX5.Image = Image.FromFile( @"E:\HotelSystem1115\Whq\image\显示时间.jpg" );
			} else {
				ltime.Visible = true;
				buttonX5.Image = Image.FromFile( @"E:\HotelSystem1115\Whq\image\关闭时间.jpg" );
			}
			//        Form1 frmset = new Form1(); frmset.Show();
		}
		private void buttonX2_Click(object sender, EventArgs e) {   //详细图
			panel1.Visible = false;
			panel2.Visible = true;
			//frm2.Location = new Point( 0, 72 );
			//if  (frm2 == null) {
			//	frm2 = new panel2();
			//}
			//frm2.ShowDialog(); 
			//		frm2.Show();
		}
		private void panel1_Paint(object sender, PaintEventArgs e) {

		}
		private void timer2_Tick(object sender, EventArgs e) {
			}

   
          static void SynReceiveData() {   //接受 com data                      //      SerialPort serialPort = (SerialPort)serialPortobj;
			int j;
            while (true) {
			if  (getcomdata) {
					byteb[0] = bytesData[0]; byteb[1] = bytesData[1];
				     UInt16  tempum =  BitConverter.ToUInt16( byteb, 0);  //short serial_number = (short)((bytesData[2] << 8) + bytesData[3]);//// 测量温度编号
			string tempumstr = tempum.ToString();
			byte tempnum = bytesData[4];
			//tempnumstr = tempnum.ToString();
			//int j = 0;
			//foreach (comparisondata s in dictcd) {
			//	if (tempnumstr == s.cabinetnum) {
			//		sql = string.Format( "insert into {0}  values(null,' ','{0}'   ) ", sql );
			//		SQLiteDBHelper.ExecQuery( sql );
			//		break;
			//	}
			//	j++;
			}
			//	if ( j=0;  j  >= AppInfo.scdstrCount ) {
			//	//timer2.Enabled = false;
			//	//string ex = "发现不在数据库中的检测编号：" + tempnumstr + "，请到设置里重新设置？ ";
			//	//var result = MessageBox.Show( ex, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			//	//timer2.Enabled = false;
			//}
		
                Thread.Sleep(150 );   //1000ms    = 1S                       serialPort.ReadTimeout = 1000;
            }
        }
		private void cabinet9_Click(object sender, EventArgs e) {

		}
		//通信正常
		private void buttonX10_Click(object sender, EventArgs e) {

		}
		void panel1line(int xstart, int ystrat, int yend) {
			try {
				Bitmap b = new Bitmap( panel1.Width, panel1.Height );
				Graphics g = Graphics.FromImage( b );    //从指定的 Image 创建新的 Graphics。
				Pen MyPen = new Pen( Color.Black, 3f );
				Point pt1 = new Point( xstart - 1, ystrat ); //确定起点和终点
				Point pt2 = new Point( xstart - 1, yend );
				g.DrawLine( MyPen, pt1, pt2 );                 //使用DrawLine方法绘制直线
				g.Dispose(); panel1.BackgroundImage = b;    //释放此 Graphics 使用的所有资源。
			} catch (Exception e) {
				MessageBox.Show( e.Message );                        //处理超时错误  
			}
		}
		//主页简要图
		private void buttonX1_Click(object sender, System.EventArgs e) {
			//            sql = "select  *  from parameters  "; dt = SQLiteDBHelper.ExecQuery( sql );
			int i ,lingh;
			index = 0;
			const int row1 = 336;
			 int ht = 32;    //标题显示高度
			const	int hT = 32;    //标题显示高度
			int disph = 36;    //数字显示高度
			const int dispH = 36;    //数字显示高度
			panel1.Visible = true;			panel2.Visible = false;
			this.line1.Location = new Point( 1, 72 );
			line1.Size = new Size( X, 6 );
			int height = Y - 72 - 80;
			panel1.Size = new Size( X, height );

			int disptempw = X / cdtw;     //一行 放几个 
			int delivery = X % cdtw;      //取模
			int usedisptempw = cdtw + delivery / disptempw;
			int maxling = AppInfo.scdstrCount / disptempw;    //几行
			if (AppInfo.scdstrCount / disptempw > 0) maxling++;

#region  line1
			lingh = dispH + ht + disph; int htt = dispH + hT;
			line2.Location = new Point( 0, 40 + ht ); line2.Size = new Size( X, 6 );
			line4.Location = new Point( 0, lingh ); line4.Size = new Size( X, 6 );
			AppInfo.scdstr[index].lcabinetnum = cabinet0; 
			ltemp0010.Location = new Point( 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0010;
			ltemp0011.Location = new Point( 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0011;
			ltemp0012.Location = new Point( 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0012;
			ltemp0013.Location = new Point( 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0013;
			index++;
			cabinet1.Location = new Point( 108 + row1, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet1;
			ltemp0020.Location = new Point( row1 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0020;
			ltemp0021.Location = new Point( row1 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0021;
			ltemp0022.Location = new Point( row1 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0022;
			ltemp0023.Location = new Point( row1 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0023;
			index++;
			cabinet2.Location = new Point( 108 + row1 * 2, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet2; 
			ltemp0030.Location = new Point( row1 * 2 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0030;
			ltemp0031.Location = new Point( row1 * 2 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0031;
			ltemp0032.Location = new Point( row1 * 2 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0032;
			ltemp0033.Location = new Point( row1 * 2 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0033;
			index++;
			cabinet3.Location = new Point( 108 + row1 * 3, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet3; 
			ltemp0040.Location = new Point( row1 * 3 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0030;
			ltemp0041.Location = new Point( row1 * 3 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0031;
			ltemp0042.Location = new Point( row1 * 3 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0032;
			ltemp0043.Location = new Point( row1 * 3 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0033;
			index++;
			cabinet4.Location = new Point( 108 + row1 * 4, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet4; 
			ltemp0050.Location = new Point( row1 * 4 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0040;
			ltemp0051.Location = new Point( row1 * 4 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0041;
			ltemp0052.Location = new Point( row1 * 4 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0042;
			ltemp0053.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0043;
			index++;
			#endregion
	#region  第二行  。。。。。。。。/		lingh = dispH + hT * 2 + disph;
			 disph = htt + dispH; 		     htt = lingh  + hT;
			line6.Location = new Point( 0, lingh+hT ); 		line6.Size = new Size( X, 6 ); 
			line5.Location = new Point( 0, lingh + dispH+hT ); line5.Size = new Size( X, 6 );  //this.line5.ForeColor = Color.Red;
			cabinet5.Location = new Point( 108 , lingh + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet5;          
			ltemp0110.Location = new Point( 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0110;
			ltemp0111.Location = new Point( 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0111;
			ltemp0112.Location = new Point( 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0112;
			ltemp0113.Location = new Point( 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0113;
			index++;
			cabinet6.Location = new Point( 108 + row1, lingh + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet6;
			ltemp0120.Location = new Point( row1 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0120;
			ltemp0121.Location = new Point( row1 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0121;
			ltemp0122.Location = new Point( row1 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0122;
			ltemp0123.Location = new Point( row1 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0123;
			index++;
			cabinet7.Location = new Point( 108 + row1 * 2, lingh + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet7;
			ltemp0130.Location = new Point( row1 * 2 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0130;
			ltemp0131.Location = new Point( row1 * 2 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0131;
			ltemp0132.Location = new Point( row1 * 2 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0132;
			ltemp0133.Location = new Point( row1 * 2 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0133;
			index++;
			cabinet8.Location = new Point( 108 + row1 * 3, lingh + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet8;
			ltemp0140.Location = new Point( row1 * 3 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0140;
			ltemp0141.Location = new Point( row1 * 3 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0141;
			ltemp0142.Location = new Point( row1 * 3 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0142;
			ltemp0143.Location = new Point( row1 *3 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0143;
			index++;
			cabinet9.Location = new Point( 108 + row1 * 4, lingh + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet9;
			ltemp0150.Location = new Point( row1 * 4 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0150;
			ltemp0151.Location = new Point( row1 * 4 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0151;
			ltemp0152.Location = new Point( row1 * 4 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0152;
			ltemp0153.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0153;
			index++;
	#endregion
	#region line 3
			lingh = lingh + dispH + hT;   			disph = htt + dispH;			htt = disph + hT;
			line9.Location = new Point( 0, lingh+hT ); line9.Size = new Size( X, 6 );
			line8.Location = new Point( 0, lingh + dispH+hT ); line8.Size = new Size( X, 6 ); //  this.line8.ForeColor = Color.Red;
			cabinet10.Location = new Point( 108, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet10;
			ltemp0160.Location = new Point( 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0160;
			ltemp0161.Location = new Point( 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0161;
			ltemp0162.Location = new Point( 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0162;
			ltemp0163.Location = new Point( 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0163;
			index++;
			cabinet11.Location = new Point( 108 + row1, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet11;
			ltemp0170.Location = new Point( row1 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0170;
			ltemp0171.Location = new Point( row1 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0171;
			ltemp0172.Location = new Point( row1 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0172;
			ltemp0173.Location = new Point( row1 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0173;
			index++;
			cabinet12.Location = new Point( 108 + row1 * 2, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet12;
			ltemp0180.Location = new Point( row1 * 2 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0180;
			ltemp0181.Location = new Point( row1 * 2 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0181;
			ltemp0182.Location = new Point( row1 * 2 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0182;
			ltemp0183.Location = new Point( row1 * 2 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0183;
			index++;
			cabinet13.Location = new Point( 108 + row1 * 3, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet13;
			ltemp0190.Location = new Point( row1 * 3 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0190;
			ltemp0191.Location = new Point( row1 * 3 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0191;
			ltemp0192.Location = new Point( row1 * 3 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0192;
			ltemp0193.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0193;
			index++;
			cabinet14.Location = new Point( 108 + row1 * 4, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet14;
			ltemp0194.Location = new Point( row1 * 4 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0194;
			ltemp0195.Location = new Point( row1 * 4 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0195;
			ltemp0196.Location = new Point( row1 * 4 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0196;
			ltemp0197.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0197;
			index++;
			#endregion
	#region line 4    		
			lingh = lingh + dispH + hT;			disph = htt + dispH;			htt = disph + hT;
			line7.Location = new Point( 0, lingh + hT ); line7.Size = new Size( X, 6 );
			line3.Location = new Point( 0, lingh + dispH + hT ); line3.Size = new Size( X, 6 );

			cabinet15.Location = new Point( 108, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet15;             //line 4
			ltemp0210.Location = new Point( 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0210;
			ltemp0211.Location = new Point( 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0211;
			ltemp0212.Location = new Point( 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0212;
			ltemp0213.Location = new Point( 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0213;
			index++;
			cabinet16.Location = new Point( 108 + row1, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet16;
			ltemp0220.Location = new Point( row1 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0220;
			ltemp0221.Location = new Point( row1 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0221;
			ltemp0222.Location = new Point( row1 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0222;
			ltemp0223.Location = new Point( row1 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0223;
			index++;
			cabinet17.Location = new Point( 108 + row1 * 2, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet17;
			ltemp0230.Location = new Point( row1 * 2 + 36, htt + 10 );  AppInfo.scdstr[index].ltempnow = ltemp0230;
			ltemp0231.Location = new Point( row1 * 2 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0231;
			ltemp0232.Location = new Point( row1 * 2 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0232;
			ltemp0233.Location = new Point( row1 * 2 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0233;
			index++;
			cabinet18.Location = new Point( 108 + row1 * 3, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet18;			ltemp0240.Location = new Point( row1 * 3 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0240;
			ltemp0241.Location = new Point( row1 * 3 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0241;
			ltemp0242.Location = new Point( row1 * 3 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0242;
			ltemp0243.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0243;
			index++;
			cabinet19.Location = new Point( 108 + row1 * 4, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet19;
			ltemp0250.Location = new Point( row1 * 4 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0250;
			ltemp0251.Location = new Point( row1 * 4 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0251;
			ltemp0252.Location = new Point( row1 * 4 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0252;
			ltemp0253.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0253;
			index++;
			#endregion
#region line 5
			lingh = lingh + dispH + hT;  			disph = htt + dispH;			htt = disph + hT;
			line13.Location = new Point( 0, lingh + hT ); line13.Size = new Size( X, 6 ); //this.line13.ForeColor = Color.Red;  
			line12.Location = new Point( 0, lingh + dispH + hT ); line12.Size = new Size( X, 6 ); //this.line12.ForeColor = Color.Blue;

			cabinet20.Location = new Point( 108, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet20;            
			ltemp0260.Location = new Point( 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0260;
			ltemp0261.Location = new Point( 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0261;
			ltemp0262.Location = new Point( 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0262;
			ltemp0263.Location = new Point( 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0263;
			index++;
			cabinet26.Location = new Point( 108 + row1, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet21;
			ltemp0270.Location = new Point( row1 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0270;
			ltemp0271.Location = new Point( row1 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0271;
			ltemp0272.Location = new Point( row1 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0272;
			ltemp0273.Location = new Point( row1 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0273;
			index++;
			cabinet22.Location = new Point( 108 + row1 * 2, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet22;
			ltemp0280.Location = new Point( row1 * 2 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0280;
			ltemp0281.Location = new Point( row1 * 2 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0281;
			ltemp0282.Location = new Point( row1 * 2 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0282;
			ltemp0283.Location = new Point( row1 * 2 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0283;
			index++;
			cabinet23.Location = new Point( 108 + row1 * 3, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet23;
			ltemp0290.Location = new Point( row1 * 3 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0290;
			ltemp0291.Location = new Point( row1 * 3 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0291;
			ltemp0292.Location = new Point( row1 * 3 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0292;
			ltemp0293.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0293;
			index++;
			cabinet24.Location = new Point( 108 + row1 * 4, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet24;
			ltemp0294.Location = new Point( row1 * 4 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0294;
			ltemp0295.Location = new Point( row1 * 4 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0295;
			ltemp0296.Location = new Point( row1 * 4 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0296;
			ltemp0297.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0297;
			index++;
			#endregion
#region line 6  		
			lingh = lingh + dispH + hT;  			disph = htt + dispH;			htt = disph + hT;
			line11.Location = new Point( 0, lingh + hT ); line11.Size = new Size( X, 6 );
			line10.Location = new Point( 0, lingh + dispH + hT ); line10.Size = new Size( X, 6 );
			cabinet25.Location = new Point( 108, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet25;             //line 4
			ltemp0310.Location = new Point( 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0310;
			ltemp0311.Location = new Point( 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0311;
			ltemp0312.Location = new Point( 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0312;
			ltemp0313.Location = new Point( 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0313;
			index++;
			cabinet26.Location = new Point( 108 + row1, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet26;
			ltemp0320.Location = new Point( row1 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0320;
			ltemp0321.Location = new Point( row1 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0321;
			ltemp0322.Location = new Point( row1 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0322;
			ltemp0323.Location = new Point( row1 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0323;
			index++;
			cabinet27.Location = new Point( 108 + row1 * 2, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet27;
			ltemp0330.Location = new Point( row1 * 2 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0330;
			ltemp0331.Location = new Point( row1 * 2 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0331;
			ltemp0332.Location = new Point( row1 * 2 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0332;
			ltemp0333.Location = new Point( row1 * 2 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0333;
			index++;
			cabinet28.Location = new Point( 108 + row1 * 3, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet28;
			ltemp0340.Location = new Point( row1 * 3 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0340;
			ltemp0341.Location = new Point( row1 * 3 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0341;
			ltemp0342.Location = new Point( row1 * 3 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0342;
			ltemp0343.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0343;
			index++;
			cabinet29.Location = new Point( 108 + row1 * 4, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet29;
			ltemp0250.Location = new Point( row1 * 4 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0350;
			ltemp0251.Location = new Point( row1 * 4 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0351;
			ltemp0252.Location = new Point( row1 * 4 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0352;
			ltemp0253.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0353;
			index++;
			#endregion
#region line 7
			lingh = lingh + dispH + hT;  			disph = htt + dispH;			htt = disph + hT;
			line17.Location = new Point( 0, lingh + hT ); line17.Size = new Size( X, 6 );  //this.line17.ForeColor = Color.Red; 
			line16.Location = new Point( 0, lingh + dispH + hT ); line17.Size = new Size( X, 6 );// this.line16.ForeColor = Color.Blue;
			cabinet30.Location = new Point( 108, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet30;
			ltemp0360.Location = new Point( 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0360;
			ltemp0361.Location = new Point( 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0361;
			ltemp0362.Location = new Point( 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0362;
			ltemp0363.Location = new Point( 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0363;
			index++;
			cabinet31.Location = new Point( 108 + row1, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet31;
			ltemp0370.Location = new Point( row1 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0370;
			ltemp0371.Location = new Point( row1 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0371;
			ltemp0372.Location = new Point( row1 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0372;
			ltemp0373.Location = new Point( row1 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0373;
			index++;
			cabinet32.Location = new Point( 108 + row1 * 2, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet32;
			ltemp0380.Location = new Point( row1 * 2 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0380;
			ltemp0381.Location = new Point( row1 * 2 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0381;
			ltemp0382.Location = new Point( row1 * 2 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0382;
			ltemp0383.Location = new Point( row1 * 2 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0383;
			index++;
			cabinet33.Location = new Point( 108 + row1 * 3, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet33;
			ltemp0390.Location = new Point( row1 * 3 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0390;
			ltemp0391.Location = new Point( row1 * 3 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0391;
			ltemp0392.Location = new Point( row1 * 3 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0392;
			ltemp0393.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0393;
			index++;
			cabinet34.Location = new Point( 108 + row1 * 4, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet34;
			ltemp0394.Location = new Point( row1 * 4 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0394;
			ltemp0395.Location = new Point( row1 * 4 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0395;
			ltemp0396.Location = new Point( row1 * 4 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0396;
			ltemp0397.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0397;
			index++;
			#endregion
#region line 8
			lingh = lingh + dispH + hT;			disph = htt + dispH;			htt = disph + hT;
			line15.Location = new Point( 0, lingh + hT ); line15.Size = new Size( X, 6 );
			line14.Location = new Point( 0, lingh + dispH + hT ); line14.Size = new Size( X, 6 );

			cabinet35.Location = new Point( 108, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet35;             //line 4
			ltemp0410.Location = new Point( 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0410;
			ltemp0411.Location = new Point( 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0411;
			ltemp0412.Location = new Point( 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0412;
			ltemp0413.Location = new Point( 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0413;
			index++;
			cabinet36.Location = new Point( 108 + row1, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet36;
			ltemp0420.Location = new Point( row1 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0420;
			ltemp0421.Location = new Point( row1 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0421;
			ltemp0422.Location = new Point( row1 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0422;
			ltemp0423.Location = new Point( row1 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0423;
			index++;
			cabinet37.Location = new Point( 108 + row1 * 2, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet37;
			ltemp0430.Location = new Point( row1 * 2 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0430;
			ltemp0431.Location = new Point( row1 * 2 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0431;
			ltemp0432.Location = new Point( row1 * 2 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0432;
			ltemp0433.Location = new Point( row1 * 2 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0433;
			index++;
			cabinet38.Location = new Point( 108 + row1 * 3, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet38;
			ltemp0440.Location = new Point( row1 * 3 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0440;
			ltemp0441.Location = new Point( row1 * 3 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0441;
			ltemp0442.Location = new Point( row1 * 3 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0442;
			ltemp0443.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0443;
			index++;
			cabinet39.Location = new Point( 108 + row1 * 4, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet39;
			ltemp0450.Location = new Point( row1 * 4 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0450;
			ltemp0451.Location = new Point( row1 * 4 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0451;
			ltemp0452.Location = new Point( row1 * 4 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0452;
			ltemp0453.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0453;
			index++;
			#endregion
	#region line 9
			lingh = lingh + dispH + hT;			disph = htt + dispH;			htt = disph + hT;
			line19.Location = new Point( 0, lingh + hT ); line19.Size = new Size( X, 6 ); //this.line19.ForeColor = Color.SaddleBrown; 
			line18.Location = new Point( 0, lingh + dispH + hT );  line18.Size = new Size( X, 6 );  // this.line18.ForeColor = Color.Tomato;
			cabinet40.Location = new Point( 108, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet40;
			ltemp0460.Location = new Point( 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0460;
			ltemp0461.Location = new Point( 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0461;
			ltemp0462.Location = new Point( 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0462;
			ltemp0463.Location = new Point( 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0463;
			index++;
			cabinet41.Location = new Point( 108 + row1, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet41;
			ltemp0470.Location = new Point( row1 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0470;
			ltemp0471.Location = new Point( row1 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0471;
			ltemp0472.Location = new Point( row1 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0472;
			ltemp0473.Location = new Point( row1 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0473;
			index++;
			cabinet42.Location = new Point( 108 + row1 * 2, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet42;
			ltemp0480.Location = new Point( row1 * 2 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0480;
			ltemp0481.Location = new Point( row1 * 2 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0481;
			ltemp0482.Location = new Point( row1 * 2 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0482;
			ltemp0483.Location = new Point( row1 * 2 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0483;
			index++;
			cabinet43.Location = new Point( 108 + row1 * 3, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet43;
			ltemp0490.Location = new Point( row1 * 3 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0490;
			ltemp0491.Location = new Point( row1 * 3 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0491;
			ltemp0492.Location = new Point( row1 * 3 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0492;
			ltemp0493.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0493;
			index++;
			cabinet44.Location = new Point( 108 + row1 * 4, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet44;
			ltemp0494.Location = new Point( row1 * 4 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0494;
			ltemp0495.Location = new Point( row1 * 4 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0495;
			ltemp0496.Location = new Point( row1 * 4 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0496;
			ltemp0497.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0497;
			index++;
			#endregion
			#region line 100
			lingh = lingh + dispH + hT; disph = htt + dispH; htt = disph + hT;
			line23.Location = new Point( 0, lingh + hT ); line23.Size = new Size( X, 6 );
			line22.Location = new Point( 0, lingh + dispH + hT ); line22.Size = new Size( X, 6 );

			cabinet45.Location = new Point( 108, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet45;             //line 4
			ltemp0510.Location = new Point( 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0510;
			ltemp0511.Location = new Point( 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0511;
			ltemp0512.Location = new Point( 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0512;
			ltemp0513.Location = new Point( 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0513;
			index++;
			cabinet46.Location = new Point( 108 + row1, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet46;
			ltemp0520.Location = new Point( row1 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0520;
			ltemp0521.Location = new Point( row1 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0521;
			ltemp0522.Location = new Point( row1 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0522;
			ltemp0523.Location = new Point( row1 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0523;
			index++;
			cabinet47.Location = new Point( 108 + row1 * 2, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet47;
			ltemp0530.Location = new Point( row1 * 2 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0530;
			ltemp0531.Location = new Point( row1 * 2 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0531;
			ltemp0532.Location = new Point( row1 * 2 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0532;
			ltemp0533.Location = new Point( row1 * 2 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0533;
			index++;
			cabinet48.Location = new Point( 108 + row1 * 3, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet48;
			ltemp0540.Location = new Point( row1 * 3 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0540;
			ltemp0541.Location = new Point( row1 * 3 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0541;
			ltemp0542.Location = new Point( row1 * 3 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0542;
			ltemp0543.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0543;
			index++;
			cabinet49.Location = new Point( 108 + row1 * 4, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet49;
			ltemp0550.Location = new Point( row1 * 4 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0550;
			ltemp0551.Location = new Point( row1 * 4 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0551;
			ltemp0552.Location = new Point( row1 * 4 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0552;
			ltemp0553.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0553;
			index++;
			#endregion
			#region line 110
			lingh = lingh + dispH + hT; disph = htt + dispH; htt = disph + hT;
			line21.Location = new Point( 0, lingh + hT ); line19.Size = new Size( X, 6 ); //this.line19.ForeColor = Color.SaddleBrown; 
			line20.Location = new Point( 0, lingh + dispH + hT ); line18.Size = new Size( X, 6 );  // this.line18.ForeColor = Color.Tomato;
			cabinet50.Location = new Point( 108, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet50;
			ltemp0560.Location = new Point( 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0560;
			ltemp0561.Location = new Point( 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0561;
			ltemp0562.Location = new Point( 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0562;
			ltemp0563.Location = new Point( 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0563;
			index++;
			cabinet51.Location = new Point( 108 + row1, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet51;
			ltemp0570.Location = new Point( row1 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0570;
			ltemp0571.Location = new Point( row1 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0571;
			ltemp0572.Location = new Point( row1 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0572;
			ltemp0573.Location = new Point( row1 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0573;
			index++;
			cabinet52.Location = new Point( 108 + row1 * 2, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet52;
			ltemp0580.Location = new Point( row1 * 2 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0580;
			ltemp0581.Location = new Point( row1 * 2 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0581;
			ltemp0582.Location = new Point( row1 * 2 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0582;
			ltemp0583.Location = new Point( row1 * 2 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0583;
			index++;
			cabinet53.Location = new Point( 108 + row1 * 3, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet53;
			ltemp0590.Location = new Point( row1 * 3 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0590;
			ltemp0591.Location = new Point( row1 * 3 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0591;
			ltemp0592.Location = new Point( row1 * 3 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0592;
			ltemp0593.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0593;
			index++;
			cabinet54.Location = new Point( 108 + row1 * 4, disph + 10 ); AppInfo.scdstr[index].lcabinetnum = cabinet54;
			ltemp0594.Location = new Point( row1 * 4 + 36, htt + 10 ); AppInfo.scdstr[index].ltempnow = ltemp0594;
			ltemp0595.Location = new Point( row1 * 4 + 122, htt + 10 ); AppInfo.scdstr[index].ltemph = ltemp0595;
			ltemp0596.Location = new Point( row1 * 4 + 210, htt + 10 ); AppInfo.scdstr[index].ltempl = ltemp0596;
			ltemp0597.Location = new Point( row1 * 4 + 286, htt + 10 ); AppInfo.scdstr[index].ltempalac = ltemp0597;
			index++;
			#endregion
			Bitmap b = new Bitmap( panel1.Width, panel1.Height );
			Graphics g = Graphics.FromImage( b );    //从指定的 Image 创建新的 Graphics。
			Pen MyPen = new Pen( Color.Black, 3f );
			Point pt1 = new Point( 335, 0 ); //确定起点和终点
			Point pt2 = new Point( 335, 100 );      //	Rectangle rect = new Rectangle( (panel1.Width / 2) - 128, (panel1.Height / 2) - 152, 256, 304 );
			g.DrawLine( MyPen, pt1, pt2 );                 //使用DrawLine方法绘制直线	g.DrawRectangle( new Pen( Color.Lime, 2 ), rect );
			g.Dispose(); panel1.BackgroundImage = b;    //释放此 Graphics 使用的所有资源。
			//for (i = 1; i < 6; i++) {				panel1line( row1 * i, 0, 180 );			}

		}
	}
}



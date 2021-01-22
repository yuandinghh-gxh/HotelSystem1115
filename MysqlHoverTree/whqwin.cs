using System;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
//using System.Data;
#region  变量区 。。。。。。。
namespace Vcom {
	public partial class whqwin : Office2007Form {               //Form {
		Thread readThread = new Thread( Read );
		private DataTable dt = new DataTable();
		int getcomcount = 0;        //    private bool first = true;  // private static bool   getcomstr;
		private static int vdata;  //    private DataTable dt;  private string sql;   private static byte v;
		public static byte[] bytesData = new byte[200];
		private static bool getcomdata, sendtestcom, messagebool;                     //收到一组COM数据
		private static string message, x10, x9, r;
		public static int sec, seccount;
		double p;
		#endregion
		#region  暂时不用的程序
		private void timer1_Tick(object sender, EventArgs e) {  //  1000毫秒  1秒
			DateTime currentTime = new DateTime();
			currentTime = DateTime.Now; labelX2.Text = currentTime.ToString();   //"f" ); //不显示秒
														   //	labelX2.Text = currentTime.ToLongDateString();   labelX2.Text + " " + 
			labelX2.Text = DateTime.Now.ToString( "dddd", new System.Globalization.CultureInfo( "zh-cn" ) ) + " " + currentTime.ToString( "T" );                    //中文星期显示
			sec++;
			if (AppInfo.hadcom) {
				if (sec > 20) {
					sendtestcom = true;
					try {
						sec = 0; AppInfo.serialPort.WriteLine( "PP" );   //发送数据       getcomcount=0;
					} catch (Exception ex) {
						labelX10.Text = "写Com严重错误：" + ex.Message;
						AppInfo.comset = false;    //closecom();
					}
				}
			}        //else {			//	if (sec > 10) {   //检测是否安装串行口			//		opencom(); sec = 0;			//	}			//}
			if (getcomdata) {
				getcomdata = false;
				//		if (r.Length >= 2) { r += "\n"; }
				richTextBox1.Text += r;       //+ "\n"+"\r";
				if (x9 == "测试仪没连接电脑!") {
					labelX9.ForeColor = Color.DeepPink;
					labelX9.Text = x9; x9 = "";
				}
				if (x10 != "") {
					labelX10.Text = x10; x10 = "";
				}
				if (x9 == "测试仪正常通信！") {
					labelX9.Text = x9; x9 = "";
				}
			}
		}

		public static void Read( ) {
			while (true) {   //不能退出
					     //			getcomdata = false;
				if (AppInfo.hadcom) {    //已经打开 Com
					byte[] nss = new byte[10]; int i = 0;
					if (AppInfo.comopen) {
						try {
							AppInfo.LoginName = AppInfo.serialPort.ReadLine();                            //		string s = serialPort1.ReadExisting();
							if (AppInfo.LoginName != "") {
								getcomdata = true;
								bytesData = Encoding.Default.GetBytes( AppInfo.LoginName );   //    bytesData[readbyte]= (Byte) ppInfo.serialPort.ReadByte();	
								byte ccom = bytesData[0];
								if (ccom == 'V') {
									//	x10 = "成功连接电压测试仪！";
									x9 = "测试仪正常通信！";    // comok();
									AppInfo.comset = true;
								}
							}
						} catch (TimeoutException) { } //  MessageBox.Show("出错:TimeoutException"); }
					catch (InvalidOperationException ex) {   //					messagebool = true; message = ex.Message;
							x10 = "读Com严重错误1：" + ex.Message;  //关闭
						} catch (UnauthorizedAccessException ex) {                  //					message = ex.Message; messagebool = true;
							x10 = "读Com严重错误2：" + ex.Message;
						} catch (Exception ex) {
							x10 = ex.Message + "读串行口出错";
						}
						//			Thread.Sleep( 100 );
					}
					if (getcomdata) {    // 已经连接检测仪
								   //	try {
								   //		if (AppInfo.LoginName == "")
								   //			AppInfo.LoginName = AppInfo.serialPort.ReadLine();                            //		string s = serialPort1.ReadExisting();
								   //	} catch (TimeoutException) {
								   //} catch (Exception ex) {
								   //	x10 = "读Com数据错误：" + ex.Message;
								   //	AppInfo.comset = false;

						//}

						if (AppInfo.LoginName != "") {
							r = AppInfo.LoginName; getcomdata = true;

							if (bytesData[0] == 'C') {
								while (bytesData[1 + i] != 0x0) {
									nss[i] = bytesData[1 + i]; i++;
								}
								if (bytesData[0] == 'V') {                                                          //	x10 = "成功连接电压测试仪！";
									x9 = "测试仪正常通信！"; getcomdata = true;    // comok();
									AppInfo.comset = true;
								}
								nss[i] = 0; seccount = 3000;  //准确收到 测量信号
								string str = System.Text.Encoding.Default.GetString( nss );
								vdata = Convert.ToInt32( str );
							}
							if (sendtestcom) {  //和硬件仪器通信测试  
								seccount--;
								if (seccount == 0) {
									seccount = 5000;
									x9 = "测试仪没连接电脑!";
								}
							}

						}
					}   // if (AppInfo.comopen && !AppInfo.comset) 

				}  //  if (AppInfo.hadcom) 
				Thread.Sleep( 300 );
			}
		}
		private void buttonX2_Click(object sender, EventArgs e) {   //设置通信口
												//FrmSetProt fm = new FrmSetProt();
												//fm.ShowDialog();
		}
		#endregion
		public whqwin( ) {    //data init
			InitializeComponent();
			sendtestcom = false; getcomcount = 0; sec = 0;
			x10 = ""; x9 = ""; r = ""; seccount = 3000;
		}
		private void comok( ) {
			seccount = 5000; labelX9.ForeColor = Color.Blue;
			labelX9.Text = "测试仪正常通信";
		}
		public void opencom( ) {
			try {
				AppInfo.serialPort = new SerialPort();                    // Allow the user to set the appropriate properties.
				AppInfo.serialPort.PortName = AppInfo.COM; // "COM4";  //SetPortName(serialPort.PortName);
				AppInfo.serialPort.BaudRate = 4800;  //   SetPortBaudRate(serialPort.BaudRate);
				AppInfo.serialPort.Parity = Parity.None;  //  SetPortParity(serialPort.Parity);
				AppInfo.serialPort.DataBits = 8; // SetPortDataBits(serialPort.DataBits);
				AppInfo.serialPort.StopBits = StopBits.One;  //  SetPortStopBits(serialPort.StopBits);
				AppInfo.serialPort.Handshake = Handshake.None;   // SetPortHandshake(serialPort.Handshake);     
				AppInfo.serialPort.ReadTimeout = 500; AppInfo.serialPort.WriteTimeout = 500;
				AppInfo.hadcom = true; AppInfo.comopen = true; AppInfo.comset = false;
				AppInfo.serialPort.Open();
				if (AppInfo.serialPort.IsOpen) {
					labelX10.Text = "成功打开串行口：" + AppInfo.COM;                                    // itemName[0].ToString();
					labelX9.Text = "连接电压测试仪";                                                                        //comok();
					AppInfo.serialPort.WriteLine( "PP" );   //发送数据   

				} else {
					AppInfo.hadcom = false; AppInfo.comopen = false; AppInfo.comset = false;
				}
			} catch (Exception ex) {
				AppInfo.hadcom = false; AppInfo.comopen = false; AppInfo.comset = false;
				labelX10.Text = ex.Message + "请设置！";          //Environment.Exit( 0 ); this.Close();
				labelX9.ForeColor = Color.DeepPink;
				labelX9.Text = "无法打开串行口！";
			}
		}
		private void whqwin_Load(object sender, EventArgs e) {                  //初始化 、、、、、、、、、
			dt = SQLiteDBHelper.ExecQuery( "Select * From setdata" );
			foreach (DataRow row in dt.Rows) {               //2.赋值新数据			  //         comparisondata cda = new comparisondata();
				AppInfo.COM = row["com"].ToString();
				AppInfo.COMset = row["comset"].ToString();
			}
			if (AppInfo.COMset == "Y") {
				opencom();
				if (!AppInfo.comset) {   //没和电压检测 
					try {
						AppInfo.serialPort.WriteLine( "PP\n" );
					} catch (Exception ex) {
						labelX10.Text = "写通信严重错误：" + ex.Message;
					}

				} else {                      //	MessageBox.Show( "请设置COM串行口" );
					timer1.Stop();
					var System = new FrmSetProt(); System.ShowDialog();
					timer1.Start();
					if (!AppInfo.hadcom) {
						labelX10.Text = "没发现串行口！,请安装串行口计算机";
						labelX9.Text = "请安装Usb转串行口！";
					}
				}
				buttonX3_Click( null, null );
				buttonX1.Location = new Point( 609, 12 ); buttonX7.Location = new Point( 609, 66 ); buttonX2.Location = new Point( 587, 94 );
			}   //if (AppInfo.COMset == "Y") {
			readThread.Start();
		}
		#region  材料选择
		private void buttonX3_Click(object sender, EventArgs e) { // 铝
			buttonX8.BackColor = System.Drawing.Color.Gray;
			buttonX9.BackColor = System.Drawing.Color.Gray;
			buttonX3.BackColor = System.Drawing.Color.GreenYellow;
			textBoxX3.Text = "2.83*10负8次方";
			p = 0.00000028;
		}
		private void buttonX8_Click(object sender, EventArgs e) {
			buttonX8.BackColor = System.Drawing.Color.GreenYellow;
			buttonX9.BackColor = System.Drawing.Color.Gray;
			buttonX3.BackColor = System.Drawing.Color.Gray;
			textBoxX3.Text = "1.75*10负8次方";
			p = 0.0000000175;
		}
		private void buttonX9_Click(object sender, EventArgs e) {
			buttonX8.BackColor = System.Drawing.Color.Gray;
			buttonX9.BackColor = System.Drawing.Color.GreenYellow;
			buttonX3.BackColor = System.Drawing.Color.Gray;
			textBoxX3.Text = "9.78*10负8次方";
			p = 0.0000000978;
		}
		#endregion
		private void button1_Click(object sender, EventArgs e) {   //设置
			timer1.Stop();
			var System = new FrmSetProt(); System.ShowDialog();
			timer1.Start();
		}
		private void buttonX2_Click_1(object sender, EventArgs e) {   //通信连接
			opencom();

		}
		private void buttonX1_Click(object sender, EventArgs e) {
			readThread.Join(); AppInfo.serialPort.Close(); this.Close();
		}
		private void buttonX7_Click(object sender, EventArgs e) {  //发送数据
			AppInfo.serialPort.WriteLine( "AA2" );
		}

		private void closecom( ) {
			if (AppInfo.serialPort.IsOpen) {
				AppInfo.serialPort.DiscardInBuffer(); AppInfo.serialPort.DiscardOutBuffer();
				AppInfo.serialPort.Close(); AppInfo.serialPort.Dispose();
			}
		}
	}
	class AppInfo {
		//       public static bool IsLogin = false;//全局变量，观察是否登陆成功     在计费设置 中用第一次允许 结果
		public static string LoginName = null;      //串行口读数据//     public static string Sysfile = @"D:\sysdifine.com";      //系统定义数据文件名
		public static bool comopen = false;       //串行口成功打开
		public static string COM = "COM1";
		public static string COMset = "N";       //初始 没设置COM
		public static bool controltemp = false;
		public static bool hadcom = false;     //本机有串行口或没有
		public static bool comset = false;     //成功和 检测仪连接
		public static SerialPort serialPort;
		//      public static Dictionary<string, comparisondata> dict = new Dictionary<string, comparisondata>();
		public static int scdstrCount = 0;  //AppInfo.scdstrCount
	}
}



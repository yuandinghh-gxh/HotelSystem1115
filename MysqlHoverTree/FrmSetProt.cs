using System;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Management;
using System.Linq;
using System.Data;
using System.Drawing;

namespace Vcom {
	public partial class FrmSetProt : Form {
		public FrmSetProt( ) {
			InitializeComponent();
		}                   //		string text = "";  		whqwin m; 							//		static SerialPort sp ;
		private string sql;
		private int secinc, mmcount;    //连接串行口次数
		public Byte[] combyte = new Byte[200];
		private DataTable dt = new DataTable();
		private static bool stoptest ;    //收到一组COM数据  		Thread readThread = new Thread( Read );
		public static int sec, seccount;
		//	whqwin w;

		private void Form1_Load(object sender, EventArgs e) {
			getAvailablePorts(); sec = 0; AppInfo.hadcom = false; stoptest = true;
			string[] itemName = SerialPort.GetPortNames();  //获取当前计算机串型端口名称数组.
			if (itemName.Length == 0) {
				this.Close();
				return;
			}
			if (AppInfo.COMset != "Y") {    //没选择过串行口
				timer3.Enabled = true; timer3.Start();
				AppInfo.hadcom = true;   //找到串行口
				if (!AppInfo.comset) {          //串行口还没确定
					com.Items.Clear();
					foreach (var item in itemName) {
						com.Items.Add( item );
					}
					com.Text = itemName[0];
				}
			} else {
				com.Text = AppInfo.COM;
			}
			opencom();
		}
		private void timer3_Tick(object sender, EventArgs e) {
			secinc++; mmcount++;
			if (AppInfo.hadcom) {    //不存在 串行口
				AppInfo.comopen = false;                  //AppInfo.hadcom = false;
				if (AppInfo.comopen && !AppInfo.comset  && stoptest) {
					try {
						AppInfo.LoginName = AppInfo.serialPort.ReadLine();          //		string s = serialPort1.ReadExisting();
						richTextBox1.Text += AppInfo.LoginName;
						if (richTextBox1.Text.Length >= 2) {
							richTextBox1.Text += "\r";
						}
						if (AppInfo.LoginName != "") {
							whqwin.bytesData = Encoding.Default.GetBytes( AppInfo.LoginName );   //    bytesData[readbyte]= (Byte) ppInfo.serialPort.ReadByte();  
							byte ccom = whqwin.bytesData[0];
							if (AppInfo.LoginName == "VV\r") {
								sql = string.Format( "UPDATE  setdata set   com='{0}',  comset='{1}'  ", AppInfo.COM, "Y" );
								SQLiteDBHelper.ExecQuery( sql ); lblScan.Text = "成功连接电压测试仪！";
								AppInfo.comset = true; stoptest = false;  //stop test com
							}
						}
					} catch (TimeoutException) { } //  MessageBox.Show("出错:TimeoutException"); }
					catch (InvalidOperationException ex) {   //					messagebool = true; message = ex.Message;
						lblScan.Text = "读Com严重错误1：" + ex.Message;  //关闭
					} catch (UnauthorizedAccessException ex) {                  //					message = ex.Message; messagebool = true;
						lblScan.Text = "读Com严重错误2：" + ex.Message;
					} catch (Exception ex) {
						lblScan.Text = ex.Message + "读串行口出错";
					}
					Thread.Sleep( 80 );
				}
				if (secinc > 3  ) {    //  测试通信
					sec++; secinc = 0;
					if (!AppInfo.comset && sec < 10) {    //十次以上 停止 连接
						if (mmcount > 10  && stoptest) {
							mmcount = 0;
							try {
								if (!AppInfo.comopen && AppInfo.hadcom) {
									AppInfo.serialPort.Write( "PP\n" ); AppInfo.serialPort.DiscardOutBuffer();
								}
							} catch (Exception ex) {
								lblScan.Text = "严重错误：" + ex.Message;     //		
							}
						}
					} 
					Thread.Sleep( 50 );
					//	}  //while
					if (!AppInfo.comset && sec >= 10) {
						closecom();
						labelX6.Text = "本串行口无法连接测试仪！请更换串行口。";
						stoptest = true;
					}
				}
			}
			Thread.Sleep( 100 );
		}
		public void closecom( ) {
			if (AppInfo.comopen) {
				AppInfo.serialPort.DiscardInBuffer(); AppInfo.serialPort.DiscardOutBuffer();
				AppInfo.serialPort.Close(); AppInfo.serialPort.Dispose();
				AppInfo.comopen = false;

			}
		}
		private void opencom( ) {
			//	if  (AppInfo.hadcom)    { AppInfo.serialPort.Close();  }
			AppInfo.COM = com.Text;
			try {
				AppInfo.serialPort = new SerialPort();                    // Allow the user to set the appropriate properties.
				AppInfo.serialPort.PortName = AppInfo.COM; // "COM4";  //SetPortName(serialPort.PortName);
				AppInfo.serialPort.BaudRate = 4800;  //   SetPortBaudRate(serialPort.BaudRate);
				AppInfo.serialPort.Parity = Parity.None;  //  SetPortParity(serialPort.Parity);
				AppInfo.serialPort.DataBits = 8; // SetPortDataBits(serialPort.DataBits);
				AppInfo.serialPort.StopBits = StopBits.One;  //  SetPortStopBits(serialPort.StopBits);
				AppInfo.serialPort.Handshake = Handshake.None;   // SetPortHandshake(serialPort.Handshake);     
				AppInfo.serialPort.ReadTimeout = 500; AppInfo.serialPort.WriteTimeout = 500;
				labelX6.Text = "成功打开串行口：" + AppInfo.COM;                                    // itemName[0].ToString();
				AppInfo.hadcom = true; AppInfo.comopen = true; AppInfo.comset = false;
				AppInfo.serialPort.Open();                  //				readThread.Start();
			} catch (Exception ex) {
				lblScan.Text = ex.Message + "打开串行口 出错，重新选择串行口！";       //Environment.Exit( 0 ); this.Close();
				AppInfo.hadcom = false; AppInfo.comopen = false;
				labelX6.ForeColor = Color.DeepPink; labelX6.Text = "无法打开串行口！";
			}
		}

		private void buttonX1_Click_1(object sender, EventArgs e) {   //确认串行口
			timer3.Enabled = true; timer3.Start();
			lblScan.Text = "开始连接电压测试仪"; AppInfo.hadcom = true;
			AppInfo.COM = this.com.Text;
			opencom();                    //lblScan.Text = "未开启采集程序.";
			sec = 0; secinc = 0;
			if (AppInfo.comopen) {
				try {
					AppInfo.serialPort.WriteLine( "PP\n" );   //	combyte[0] = 0x50;		combyte[1] = 0xa;		AppInfo.serialPort.Write( combyte ,0,2);
				} catch (NullReferenceException ex) {
					lblScan.Text = "严重错误：" + ex.Message;     //		
				} catch (InvalidOperationException ex) {
					lblScan.Text = "严重错误：" + ex.Message;     //					throw new InvalidOperationException();
				} catch (Exception ex) {
					lblScan.Text = "确认 Exception错误：" + ex.Message;     //					throw new InvalidOperationException();
				}
				mmcount = 0;
				//	labelX6.Text = "本机无串行口，请购买Usb 转串行口通讯线！";  				//	lblScan.Text = "无法打开串行口！";
			}
		}

		public void getAvailablePorts( ) {       //this code just finds the available ports to use
								     //this code gets the name of the port and port number
			using (var searcher = new ManagementObjectSearcher( "SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'" )) {
				var portnames = SerialPort.GetPortNames();
				var ports2 = searcher.Get().Cast<ManagementBaseObject>().ToList().Select( p => p["Caption"].ToString() );
				var portList = portnames.Select( n => n + " - " + ports2.FirstOrDefault( s => s.Contains( n ) ) ).ToList();
				//this code displays the values in a list form in the textbox
				foreach (string s in portList) {
					com.Text += s.ToString() + "\r\n";
				}
			}
			string[] ports = SerialPort.GetPortNames();   //this code only gets the com port number
			com.Items.AddRange( ports );

		}
		private void buttonX2_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}


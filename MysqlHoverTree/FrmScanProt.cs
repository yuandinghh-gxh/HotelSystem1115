using System;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;

namespace MysqlHoverTree {
    public partial class FrmScanProt : Form {
        public FrmScanProt( ) {
            InitializeComponent();
        }


        //APICloud  //app
        string text = "";
        SerialPort sp = new SerialPort();

        delegate void UpdateTextEventHandler(string text);  //委托，此为重点.
        UpdateTextEventHandler updateText;  //事件

        private void Form1_Load(object sender, EventArgs e) {
            AddParameters();
            string[] itemName = SerialPort.GetPortNames();  //获取当前计算机串型端口名称数组.
            if (itemName.Length == 0 ) {
                MessageBox.Show( "没发现串行口！,请安装带有串行口的计算机" );
                this.Close();
                   return;
            } else {
                cboPortName.Items.Clear();
                foreach (var item in itemName) {
                    cboPortName.Items.Add( item );
                }
                cboPortName.SelectedIndex = 0;
                cboBaudRate.SelectedIndex = 1;
                cboDataBit.SelectedIndex = 3;
                cboParityBit.SelectedIndex = 0;
                cboStopBit.SelectedIndex = 1;
                }
            updateText += new UpdateTextEventHandler( UpdateTextBox );    //委托方法
            sp.DataReceived += new SerialDataReceivedEventHandler( sp_DataReceived ); //处理串口对象的数据接收事件的方法.
            sp.Close();        lblScan.Text = "未开启采集程序.";
        }

        private void button1_Click(object sender, EventArgs e) {

            if (txtCode.Text != "") {
                listBox1.Items.Add( txtCode.Text );
                txtCode.Text = "";
                txtCode.Focus();
            } else { MessageBox.Show( " 条码不能为空！" ); txtCode.Focus(); }
        }

        private void btnStartScan_Click(object sender, EventArgs e) {
            if (!sp.IsOpen) {
                sp.Open();  //打开一个新的串口连接.
                lblScan.Text = "采集中...";
                txtCode.Focus();
            }
        }

        private void btnEndScan_Click(object sender, EventArgs e) {
            if (sp.IsOpen) {
                sp.Close(); //关闭一个串口连接.
                lblScan.Text = "采集结束.";
                txtCode.Focus();
            }
        }

        /// <summary>
        /// 串口名称
        /// </summary>
        private void cboPortName_SelectedIndexChanged(object sender, EventArgs e) {
            sp.PortName = cboPortName.Items[cboPortName.SelectedIndex].ToString();
        }

        /// <summary>
        /// 波特率（每秒传送字节数）
        /// </summary>
        private void cboBaudRate_SelectedIndexChanged(object sender, EventArgs e) {
            //获取或设置串口波特率
            // cboBaudRate.Items.Add(sp.BaudRate);
            sp.BaudRate = Convert.ToInt32( cboBaudRate.Items[cboBaudRate.SelectedIndex].ToString() );

        }
        /// <summary>
        /// 数据字节
        /// </summary>
        private void cboDataBit_SelectedIndexChanged(object sender, EventArgs e) {
            //设置每个字节的标准数据长度
            sp.DataBits = Convert.ToInt32( cboDataBit.Items[cboDataBit.SelectedIndex].ToString() );
        }
        /// <summary>
        /// 停止位置
        /// </summary>
        private void cboStopBit_SelectedIndexChanged(object sender, EventArgs e) {
            //设置每个字节的标准停止位数
            sp.StopBits = (StopBits)cboStopBit.SelectedIndex;
        }
        /// <summary>
        /// 奇偶位置
        /// </summary>
        private void cboParityBit_SelectedIndexChanged(object sender, EventArgs e) {
            //设置奇偶校验检查协议
            // sp.Parity = Parity.Odd;
            sp.Parity = (Parity)System.Enum.Parse( typeof( Parity ), cboParityBit.SelectedIndex.ToString() );
        }
        /// <summary>
        /// 接收到的数据
        /// </summary>
        private void sp_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e) {
            if (!sp.IsOpen) {
                MessageBox.Show( "请先启用扫描枪采集..." );
                Thread.Sleep( 2000 );
                this.Close();
                return;
            }

            Thread.Sleep( 100 );
            byte[] buffer = Encoding.UTF8.GetBytes( sp.ReadExisting() );
            string newString = Encoding.UTF8.GetString( buffer );

            //string readString = sp.ReadExisting();//读取串口对象的流和输入缓冲区所有立即可用的字节流.

            this.Invoke( updateText, new string[] { newString } );   //控件基础句柄的线程上，执行委托.
        }

        /// <summary>
        /// 获取数据.
        /// </summary>
        private void UpdateTextBox(string text) {
            this.txtCode.Text = text;
            listBox1.Items.Add( txtCode.Text );
            txtCode.Text = "";
        }


        private void AddParameters( ) {  //设置串行口参数
            this.cboBaudRate.Items.AddRange( new object[] { "4800", "9600", "14400", "19200", "38400", "56000", "57600", "115200", "128000" } );
            this.cboDataBit.Items.AddRange( new object[] { "5", "6", "7", "8" } );
            this.cboStopBit.Items.AddRange( new object[] { "0.5", "1", "1.5", "2" } );
            this.cboParityBit.Items.AddRange( new object[] { "none", "odd", "even" } );
        }

        private void button2_Click(object sender, EventArgs e) {
            listBox1.Items.Clear();
        }

        private void listBox1_ItemClick(object sender, EventArgs e) {

        }
    }
}


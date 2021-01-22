using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace Serialexpample {
	partial class Form1 : Form {
		//create instance of property page
		//property page is used to set values for stop bits and 
		//baud rate

		PropertyPage pp = new PropertyPage();

		//create an Serial Port object
		SerialPort sp = new SerialPort();

		public Form1( ) {
			InitializeComponent();
			sp.PortName = "COM4";
			sp.DataBits = 8;
			sp.BaudRate = 4800;
			sp.Parity = Parity.None;
			sp.StopBits = StopBits.One;
			//上述步骤可以用在实例化时调用SerialPort类的重载构造函数  
			//SerialPort serialPort = new SerialPort("COM1", 19200, Parity.Odd, StopBits.Two);  
			//打开串口(打开串口后不能修改端口名,波特率等参数,修改参数要在串口关闭后修改)  
			sp.Open();            //发送数据  
			sp.WriteLine( "PP" );
		}

		private void propertyButton_Click(object sender, EventArgs e) {
			//show property dialog
			pp.ShowDialog();

			propertyButton.Hide();
		}

		private void sendButton_Click(object sender, EventArgs e) {
			try {
				//write line to serial port
				sp.WriteLine( textBox.Text );
				//clear the text box
				textBox.Text = "";

			} catch (System.Exception ex) {
				baudRatelLabel.Text = ex.Message;
			}

		}
		private void readButton_Click(object sender, EventArgs e) {
			try {
				//clear the text box
				textBox.Text = "";
				//read serial port and displayed the data in text box
				textBox.Text = sp.ReadLine();
			} catch (System.Exception ex) {
				baudRatelLabel.Text = ex.Message;
			}
		}

		private void Form1_Load(object sender, EventArgs e) {

		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			MessageBox.Show( "Do u want to Close the App" );
			sp.Close();
		}

		private void startCommButton_Click(object sender, EventArgs e) {
			textBox.Hide();
			sendButton.Show();
			readButton.Show();
			textBox.Show();
		}

		//when we want to save the status(value)
		private void saveStatusButton_Click(object sender, EventArgs e) {
			//display values
			//if no property is set the default values
			if (pp.bRate == "" && pp.sBits == "") {
				readTimeOutLabel.Text = "BaudRate = " + sp.BaudRate.ToString();
				readTimeOutLabel.Text = "StopBits = " + sp.StopBits.ToString();
			} else {
				readTimeOutLabel.Text = "BaudRate = " + pp.bRate;
				readTimeOutLabel.Text = "StopBits = " + pp.sBits;
			}

			parityLabel.Text = "DataBits = " + sp.DataBits.ToString();
			stopBitLabel.Text = "Parity = " + sp.Parity.ToString();
			readTimeOutLabel.Text = "ReadTimeout = " +
					  sp.ReadTimeout.ToString();

			if (propertyButton.Visible == true)
				propertyButton.Hide();
			saveStatusButton.Hide();
			startCommButton.Show();

			try {
				//open serial port
				sp.Open();
				//set read time out to 500 ms
				sp.ReadTimeout = 500;
			} catch (System.Exception ex) {
				baudRatelLabel.Text = ex.Message;
			}
		}

		private void timer1_Tick(object sender, EventArgs e) {
			//       _ = sp.ReadLine();
			if (!sp.IsOpen) {
				sp.Open();
			}
			textBox.Text = sp.ReadLine().ToString();
			if (textBox.Text != null) sp.Close();
		}
	}
}

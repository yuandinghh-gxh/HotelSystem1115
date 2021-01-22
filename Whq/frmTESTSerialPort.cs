//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace Whq
//{
//    public partial class frmTESTSerialPort : Form
//    {
//        public frmTESTSerialPort()
//        {
//            InitializeComponent();
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace Whq   {
    public partial class frmTESTSerialPort : Form
    {
        public frmTESTSerialPort()           {
           InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        private Button button1;
        private TextBox txtSend;
        private TextBox txtReceive;
        private Label label1;
        private Label label2;
    
        #region Windows 窗体设计器生成的代码  
        /// <summary>  
        /// 设计器支持所需的方法 - 不要  
        /// 使用代码编辑器修改此方法的内容。  
        /// </summary>  
        private void InitializeComponent()          {
            this.button1 = new System.Windows.Forms.Button();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(587, 474);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(79, 15);
            this.txtSend.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(607, 204);
            this.txtSend.TabIndex = 2;
            // 
            // txtReceive
            // 
            this.txtReceive.Location = new System.Drawing.Point(79, 250);
            this.txtReceive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.Size = new System.Drawing.Size(607, 204);
            this.txtReceive.TabIndex = 2;
            this.txtReceive.TextChanged += new System.EventHandler(this.txtReceive_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "发送";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 266);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "接收";
            // 
            // frmTESTSerialPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 542);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReceive);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmTESTSerialPort";
            this.Text = "串口试验";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private void button1_Click(object sender, EventArgs e)        {         //实例化串口对象(默认：COM1,9600,e,8,1)              
            SerialPort serialPort1 = new SerialPort();              //更改参数  
            serialPort1.PortName = "COM1";    //AppInfo.com;   // "COM1";
            serialPort1.BaudRate = 115200;            serialPort1.Parity = Parity.Odd;            serialPort1.StopBits = StopBits.One;
            serialPort1.Open();            //发送数据  
            SendStringData(serialPort1);            //也可用字节的形式发送数据           //SendBytesData(serialPort1);        //开启接收数据线程  
            ReceiveData(serialPort1);
        }

        //发送字符串数据  
        private void SendStringData(SerialPort serialPort)          {
            serialPort.Write(txtSend.Text);
        }
        /// <summary>  
        /// 开启接收数据线程  
        /// </summary>  
        private void ReceiveData(SerialPort serialPort)        {            //同步阻塞接收数据线程  
            Thread threadReceive = new Thread(new ParameterizedThreadStart(SynReceiveData));
            threadReceive.Start(serialPort);
            //也可用异步接收数据线程  
            //Thread threadReceiveSub = new Thread(new ParameterizedThreadStart(AsyReceiveData));  
            //threadReceiveSub.Start(serialPort);  
        }
        //发送二进制数据  
        private void SendBytesData(SerialPort serialPort)         {
            byte[] bytesSend = Encoding.Default.GetBytes(txtSend.Text);
            serialPort.Write(bytesSend, 0, bytesSend.Length);
        }
        //同步阻塞读取  
        private void SynReceiveData(object serialPortobj)          {
            SerialPort serialPort = (SerialPort)serialPortobj;
            System.Threading.Thread.Sleep(1000);   //1000ms    = 1S
            serialPort.ReadTimeout = 1000;
            try            {                  //阻塞到读取数据或超时(这里为2秒)  
                byte firstByte = Convert.ToByte(serialPort.ReadByte());
                int bytesRead = serialPort.BytesToRead;
                byte[] bytesData = new byte[bytesRead + 1];
                bytesData[0] = firstByte;
                for (int i = 1; i <= bytesRead; i++)
                    bytesData[i] = Convert.ToByte(serialPort.ReadByte());
                txtReceive.Text = Encoding.Default.GetString(bytesData);
            }
            catch (Exception e)            {
                MessageBox.Show(e.Message);
                //处理超时错误  
            }
            serialPort.Close();
        }
        //异步读取  
        private void AsyReceiveData(object serialPortobj)          {
            SerialPort serialPort = (SerialPort)serialPortobj;
            System.Threading.Thread.Sleep(500);
            try            {
                txtReceive.Text = serialPort.ReadExisting();
            }
            catch (Exception e)            {
                MessageBox.Show(e.Message);
                //处理错误  
            }
            serialPort.Close();
        }

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {

        }
    }

 
}
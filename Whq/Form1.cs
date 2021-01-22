using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NativeWifi;
using System.Threading;
//using ManagedWifi.dll

namespace Whq {
    public partial class wifi : Form {
        private List<WIFISSID> ssids;
        private wifiSo wifiso;
        public wifi( ) {
            InitializeComponent();
        }

        private void wifi_Load(object sender, EventArgs e) {
            wifiso = new wifiSo();  //加载wifi
            ssids = wifiso.ssids;
            wifiso.ScanSSID();      //显示所有wifi
        }
        private void connectWIFI( ) {

        }

        private void button1_Click(object sender, EventArgs e) {
            this.wifiListOK1.Items.Clear();  //只移除所有的项。
            //wifiListOK.Clear();//清除listview中的数据
            SetwifiList();
            ScanSSID();
        }

        //设置listviewok
        private void SetwifiList( ) {
            this.wifiListOK1.Columns.Add( "wifi名称", 160, HorizontalAlignment.Left ); //一步添加 
            this.wifiListOK1.Columns.Add( "wifiSSID", 120, HorizontalAlignment.Left ); //一步添加 
            this.wifiListOK1.Columns.Add( "加密方式", 100, HorizontalAlignment.Left ); //一步添加
            this.wifiListOK1.Columns.Add( "信号强度", 88, HorizontalAlignment.Left ); //一步添加 
            //ColumnHeader ch = new ColumnHeader();  //先创建列表头
            wifiListOK1.GridLines = true;//显示网格
            wifiListOK1.Scrollable = true;//显示所有项时是否显示滚动条
            wifiListOK1.AllowColumnReorder = true;
            wifiListOK1.FullRowSelect = true;
            wifiListOK1.CheckBoxes = true;
        }
        //添加数据
        private void wifiListOKADDitem(String wifiname, String pass, String dot11DefaultAuthAlgorithm, int i) {
            this.wifiListOK1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  
            //this.wifiListOK.Items.Add(wifiname,0);
            ListViewItem wifiitem = wifiListOK1.Items.Add( wifiname );

            wifiitem.SubItems.Add( pass );
            wifiitem.SubItems.Add( dot11DefaultAuthAlgorithm );
            wifiitem.SubItems.Add( i + "" );

            this.wifiListOK1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
            this.wifiListOK1.View = System.Windows.Forms.View.Details;
        }

        //单击事件
        private void wifiListOK_SelectedIndexChanged(object sender, EventArgs e) {

            if (wifiListOK1.SelectedIndices != null && wifiListOK1.SelectedItems.Count > 0) {
                ListView.SelectedIndexCollection c = wifiListOK1.SelectedIndices;
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show( "确定要连接" + wifiListOK1.Items[c[0]].Text + "吗?", "wifi连接", messButton );
                if (dr == DialogResult.OK)//如果点击“确定”按钮
                {
                    // Console.WriteLine("<<<<<<<<<<<<<<<<flags:{0}.>>>>>>>>>>>>>>>>>>>>>>>", ssid);
                    //wifiso.ConnectToSSID(targetSSID, "ZMZGZS520");//连接wifi
                }
            }
        }
        static string GetStringForSSID(Wlan.Dot11Ssid ssid) {
            return Encoding.UTF8.GetString( ssid.SSID, 0, (int)ssid.SSIDLength );
        }
        //显示所有wifi
        public void ScanSSID( ) {
            WlanClient client = new WlanClient();
            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces) {
                // Lists all networks with WEP security
                Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList( 0 );
                foreach (Wlan.WlanAvailableNetwork network in networks) {
                    WIFISSID targetSSID = new WIFISSID();

                    targetSSID.wlanInterface = wlanIface;
                    targetSSID.wlanSignalQuality = (int)network.wlanSignalQuality;
                    targetSSID.SSID = GetStringForSSID( network.dot11Ssid );
                    //targetSSID.SSID = Encoding.Default.GetString(network.dot11Ssid.SSID, 0, (int)network.dot11Ssid.SSIDLength);
                    targetSSID.dot11DefaultAuthAlgorithm = network.dot11DefaultAuthAlgorithm.ToString();
                    targetSSID.dot11DefaultCipherAlgorithm = network.dot11DefaultCipherAlgorithm.ToString();
                    ssids.Add( targetSSID );
                    wifiListOKADDitem( GetStringForSSID( network.dot11Ssid ), network.dot11DefaultCipherAlgorithm.ToString(),
                        network.dot11DefaultAuthAlgorithm.ToString(), (int)network.wlanSignalQuality );
                    if (GetStringForSSID( network.dot11Ssid ).Equals( "DZSJ1" )) {
                        var obj = new wifiSo( targetSSID, "ZMZGZS520" );
                        Thread wificonnect = new Thread( obj.ConnectToSSID );
                        wificonnect.Start();
                        //wifiso.ConnectToSSID(targetSSID, "ZMZGZS520");//连接wifi
                        connectWifiOK.Text = GetStringForSSID( network.dot11Ssid );
                        Image img = new Bitmap( Environment.CurrentDirectory + "/image/wifi.png" );//这里是你要替换的图片。当然你必须事先初始化出来图
                        pictureBoxW.BackgroundImage = img;
                        //Console.WriteLine(">>>>>>>>>>>>>>>>>开始连接网络！" + targetSSID.SSID + GetStringForSSID(network.dot11Ssid) + GetStringForSSID(network.dot11Ssid).Equals("DZSJ1"));
                    }

                }
            }
        }
        /// <summary>
        /// 关闭wifi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeWIFI_Click(object sender, EventArgs e) {
            if (connectWifiOK.Text.Equals( "无" ) || connectWifiOK.Text.Equals( null )) {
                MessageBox.Show( "当前无连接wifi" );
            } else {

            }
        }
        //更新数据
        private void getwifidatabtn_Click(object sender, EventArgs e) {
 //           WifiSocket wifiscoket = new WifiSocket();
            //wifiscoket.fuwu();
            //wifiscoket.kehuduan();
        }
   
    }
}

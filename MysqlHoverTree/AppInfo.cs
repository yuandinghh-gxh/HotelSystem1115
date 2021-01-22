using System.Collections.Generic;
using System.IO.Ports;

namespace MysqlHoverTree  {
    class AppInfo {
		const int Cdetection = 100;
 //       public static bool IsLogin = false;//全局变量，观察是否登陆成功     在计费设置 中用第一次允许 结果
		public static int UserId = 0;        //全局变量，记录登陆成功的用户ID
        public static string UserName = "";   //记录登陆成功的用户名
        public static bool Login = false;        //     public static string        IfPwd;     //是否记住密码
        public static bool Database = true;   //数据库 没更新
        public static string LoginName=null;      //记录当前登录员
        public static int AdminId=0;      //记录当前登录员  0：普通管理员，1：超级管理员
   //     public static string Sysfile = @"D:\sysdifine.com";      //系统定义数据文件名
        public static string com=null;
        public static bool comopen=false;
 //       public static int lowtemp =10;
    //    public static int uptemp=50;
       // public static int startnum=0;
//        public static bool controlnum = false;
        public static bool controltemp = false;
        public static bool comset = false;
        public static bool allset = false;
    //    public static bool comparison;   //     public static char power;
        public static SerialPort serialPort;
     //      public static Dictionary<string, comparisondata> dict = new Dictionary<string, comparisondata>();
		//public static comparisondata cda = new comparisondata();
		//public static scdata[] scdstr = new scdata[Cdetection]; //AppInfo.scdstr[]
		public static int scdstrCount = 0;  //AppInfo.scdstrCount
	}
   
}

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace HotelSystem1115 {
    class AppInfo
    {
        public static bool              IsLogin = false;//全局变量，观察是否登陆成功     在计费设置 中用第一次允许 结果
        public static int               UserId = 0;        //全局变量，记录登陆成功的用户ID
        public static string            UserName = "";   //记录登陆成功的用户名
        public static bool              Login = false;
        public static string            IfPwd;       //是否记住密码
        public static bool              Database  =    true;   //数据库 没更新
        public static bool              YanZheng;           //验证框
        public static string            AdminName; //记录当前登录员
        public static string            AdminPwd;  //记录当前登录员密码
        public static int               AdminId;      //记录当前登录员Id
        public static string            RoomName;  //记录当前选择的房间号。
    //    public static bool              Relief; 	// 确认一次交班 接班人员

 //       public static string _fdiscout;  //成为会员后给的 折扣
        public static int                StructSize;    //系统定义文件长度
        public static string             Sysfile= @"F:\sysdifine.com";      //系统定义数据文件名

        public static byte[] ReadInfo(string file)   //, int structSize读 数据结构  文件
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            byte[] bt = new byte[AppInfo.StructSize];
            byte[] bt1 = br.ReadBytes(StructSize);   //?? 
            Array.Copy(bt1, 0, bt, 0, bt1.Length);   //将 bt1的数据复制到bt， bt1长度小，保持了 新的数组长度
            br.Close();
            fs.Close();
            return bt;
        }

        public static void WriteInfo(byte[] bt)  //写  数据结构  文件
        {
            if (File.Exists(Sysfile))
            {
                File.Delete(Sysfile);
 //               return;
            }

            FileStream fs = new FileStream(Sysfile, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bt);
            bw.Flush();
            bw.Close();
            fs.Close();
        }
    }   //class

        #region    Charging  计费
        [StructLayout(LayoutKind.Sequential), Serializable]
        public  struct Charging          {
  //          public int timecount;       //时间计时器 6点， 13点和 16点
            public char alldz;          //y,n 启用全场打折
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
            public string desp;           //打折比例
            public char onlypreferential;          //y,n 仅使用每间固定优惠：
            public int preferential;           //固定优惠金额
            public char zero;          //房间费自动抹零
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
            public string jq;           //精确到个位
            public char hand;      //  开单时允许手动更改房间费打折比率和单价
            public char nohand;      //  不允许手动输入卡号(对开单和结账有效)   
            public char chang;      //  修改登记时修改房价

            public char oneday;      //  住店时间不足一天的按一天计费
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string newday;        //之后按新的一天开始计费
            public int newdayh;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string quitroom;        //之后计费天数自动增加半天
            public int quitroomh;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string addday;        //之后计费天数自动增加一天
            public int adddayh;
            public int fen;        //分钟 0-59

            public int openf;        //开房后 多长时间开始计费
            public int minjf;        //最少按  小时计费，小于这个时间按钟点房计费

            public int supertime;        //不足一小时，但超过多少分钟按一小时计费
            public int superhalf;        //不足半小时，但超过多少分钟按半小时计费

            public int hour;        //超过多少小时 转为 全天房价 计费 

 //           public int superhour;        //不足一小时，但超过多少分钟按一小时计费      
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string supertimetoday;        //超过什么时间按 全天计费
            public char vip;      //     回头客会员优惠打折：
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string _fdiscout;        //成为会员后给的 折扣
            public int chaneUnit;          // 今天出现 营业员 改变价格
            public double deposit;          //当班押金累计
            public double totalincome;          //当班总收入
            public int Free;                //有一挂单项目
            public double Freemoney;        //挂单金额
            public int FreeC;                //有一挂单客户
            public double FreemoneyC;        //挂单金额进入押金
            public char Freebool;           // 是否允许免单？ y 允许
            public double timedep;          //钟点房押金
            public int Openroom;              //开房次数
            public int Closeroom;            //退房次数
            public double Hourtime;         //钟点房收入
            public double Upmoney;        //上交款
            public double Downmoney;      //下放备用金


        }    //FrmSystemMain.cht

        #endregion

        class AdminPhpdom
        {
            //     private int field;
            public static string Phpdom(int phpdomId)
            {
                string sql = string.Format("select IsHave from AdminPhpdom where AdminId={0} and PhpdomId={1},'',''", AppInfo.AdminId, phpdomId);
                string s = SqlHelp.ExcuteScalar(sql).ToString();
                return s;
            }
        }

   
}

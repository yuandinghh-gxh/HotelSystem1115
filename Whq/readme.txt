#region
#endregion
2020-1-28 -----------数据库  data    表：
	 comparison:  对应表  tempnum  温度检测点编号   cabinetnum   对应机柜编号。
	 detection:   机柜  检测的温度表。
	 parameters:  监控参数设置表   lowtem,    uptemp,   nodecount,    frontstr,    rearst,     timeopen,    startnum,    samplingcyc,    power,     controlnum,   controltemp,   comset
  FROM parameters;
	  Users  用户表。  SELECT UserName,        LoginName,       PassWord,       AdminId,       CreationDate   FROM Users;
	  SystemLog  系统记录表： SELECT SystemLogId,       HandleTime,       operator,       Abstract,       opertorContent,       CreationDate   FROM SystemLog;
	   N个数据采集存储数据库
	   tempnum 做文件名
	      time [datetime] ,   sampledata   [nvarchar] (10) 
	
	         string s = string.Format( "{0}在{1}登陆本系统", AppInfo.UserName, DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );
            string sql = string.Format( "insert into SystemLog values ('{0}','{1}','登陆系统','{2}','{3}')", DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ), AppInfo.UserName, s, DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );
            SQLiteDBHelper.ExecQuery( sql );            Close();
            
            sql = string.Format( "insert into comparison (  tempnum,  cabinetnum ) values(' ','{0}'   ) ", cabinetnumstr );  SQLiteDBHelper.ExecQuery( sql );

　SQLite中，一个自增长字段定义为INTEGER PRIMARY KEY AUTOINCREMENT，那么在插入一个新数据时，只需要将这个字段的值指定为NULL，即可由引擎自动设定其值，引擎会设定为最大的rowid+1。如果表為空，那麼將會插入1。　　比如，有一張表ID為自增：　　CREATE TABLE Product　　(　　　　ID INTEGER PRIMARY KEY AUTOINCREMENT,　　　　Name NVARCHAR(100) NOT NULL　　)　　那麼，插入的SQL就是：　　INSERT INTO Product VALUES(NULL, '產品名稱')
————————————————
，如果表不存在的时候则创建
下面 sql 执行后首先判断 t_user 这张表是否存在，如果不存在则新建。
CREATE TABLE IF NOT EXISTS t_user(uid integer primary key,uname varchar(20),mobile varchar(20))

2，判断表是否存在
有时我们只需要知道某张表是否存在，可以通过查询 sqlite_master 这个系统表来实现。下面 sql 执行后，判断返回的 count。如果 count 为 0 则说明查询的表不存在，大于 0 则说明存在。

SELECT count(*) FROM sqlite_master WHERE type="table" AND name = "查询的表名"
----------------------------------------------------------------------------------------------------------------------------
 MessageBox.Show( "！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return;
2020-1-2   -------------------------
                        sql = string.Format( "CREATE TABLE IF NOT EXISTS {0}(stime [datetime] ,   sampledata  varchar(20) ) ", st );  //采样表
                                  SQLiteDBHelper.ExecQuery( sql );    //crearte  TABLE  sample data  by temp number
                                sql = string.Format( "UPDATE  comparison set   tempnum='{0}',  cabinetnum='{1}'  where id = {2} ",  tempnum, cabinetnum ,id);
                                SQLiteDBHelper.ExecQuery( sql );    

--2020 -------2-7 放弃   用户控件 直接用  图形  模式显示 
			//		this.u1.TabIndex = 42;			//			userControl11.set1( "我sdfasdf" );
			//		userControl11			//		u1.Show();			//		u1.set1( "我sdfasdf" );
						//	u1.Refresh();
									//this.WindowState = FormWindowState.Minimized;
			//         var System = new wifi(); System.ShowDialog();
				//     private readonly Guid _serviceClassId;
		//private  Guid _serviceClassId;
		//private Action<string> _responseAction;
		//private BluetoothListener _listener;
		//private CancellationTokenSource _cancelSource;
		//private bool _wasStarted;
		//private string _status;
		//private MessageBoxButtons buttons;
				i++;
			Label b = new Label();						//创建一个新的按钮

			b.Name = "b1";										//这是我用来区别各个按钮的办法
			System.Drawing.Point p = new Point( 120, 13 + i * 67 );//创建一个坐标,用来给新的按钮定位
			b.Size = new Size( 59, 67 );
			b.Location = p;												//把按钮的位置与刚创建的坐标绑定在一起
			panel1.Controls.Add( b );//向panel中添加此按钮

			i++;  b = new Label();
			b.Name = "b2" ;      
			
			//这是我用来区别各个按钮的办法
			 p = new Point( 120, 13 + i * 67 );//创建一个坐标,用来给新的按钮定位
			b.Size = new Size( 59, 67 );
			b.Location = p;                                                                     //把按钮的位置与刚创建的坐标绑定在一起
			panel1.Controls.Add( b );//向panel中添加此按钮
							 //		b.Click += new System.EventHandler( btn_click );//将按钮的方法绑定到按钮的单击事件中b.Click是按钮的单击事件

		340   171  
		     try {   

			 }
		catch (Exception e) {
                        MessageBox.Show( e.Message );                        //处理超时错误  
                    }

					硬件发送数据结构 
					AA  启动发送开始 
					01 00  word  硬件编码   tempnum
					20   温度整数 如果是负数问0
					10   温度小数
					00 00 00 00  INT  电流值
					00   CRC  一个字节 0X 后  累加值。
					共 10 个字节   一组数据传送 共要 954.86ns
2020-3-2
          //      while (AppInfo.comset) {
                 //   if (!AppInfo.serialPort.IsOpen) {		               //AppInfo.serialPort.Open();                 //   }
      //              try {                  //阻塞到读取数据或超时(这里为2秒)  
						//int bytesRead = AppInfo.serialPort.BytesToRead;
						//byte firstByte = Convert.ToByte( AppInfo.serialPort.ReadByte() );
                  
				  //       bytesData[0] = firstByte;
      //                           for (int i = 1; i <= bytesRead; i++) bytesData[i] = Convert.ToByte( AppInfo.serialPort.ReadByte() );
		    //               getcomdata = true;
      //                  //      = Encoding.Default.GetString( bytesData );


      //                 } catch (Exception e) {
					 //  MessageBox.Show( e.Message );                        //处理超时错误  
      //              }
			if  (getcomdata) {
						byte b = bytesData[0];
					}

      //              AppInfo.serialPort.Close();
     //           }

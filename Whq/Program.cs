using System;
using System.Windows.Forms;
using System.Threading;

namespace Whq {

    static class Program {
        //        private static string sql3;
        [STAThread]
        static void Main( ) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
    //        Application.Run(new frmTESTSerialPort());

            bool createNew;
            using (Mutex mutex = new Mutex( true, Application.ProductName, out createNew )) {
                if (createNew) {
			         Application.Run( new Login() );
					//main frmmain = new main();
					//Application.Run( frmmain );
					//set frmmain = new set();
     //               Application.Run( frmmain );
                    if (AppInfo.Login) {
						main frmmain = new main();
						Application.Run( frmmain );
					}
                } else {
                    MessageBox.Show( "应用程序已经在运行中..." );
                    Thread.Sleep( 1000 );
                    Environment.Exit( 1 );
                }
            }
        }
    }
}
//  sql = string.Format( "insert into parameters values(1,10,50,32,'机柜','','y',1)" ); SqlHelp.ExcuteInsertUpdateDelete( sql );

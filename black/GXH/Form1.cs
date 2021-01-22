using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace gxh
{
    public partial class Form1 : Form
    {

        public Form1( )
        {
            InitializeComponent();

        }
        #region  不用 程序 List<string> GetAllFiles button1_Click

        static List<string> GetAllFiles(string path)
        {
            List<string> ret = new List<string>();
            ret.AddRange( Directory.GetFiles( path ) );
            Array.ForEach( Directory.GetDirectories( path ),
                delegate (string path1) { ret.AddRange( GetAllFiles( path1 ) ); } );
            return ret;

            //foreach (string subDirPath in Directory.GetDirectories( path ))
            //{
            //    ret.AddRange( GetAllFiles( subDirPath ) );
            //    myname = subDirPath;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //设置打开图像的类型
            //        openFileDialog1.Filter = " *.jpg,*.jpeg,*.bmp,*.jif,*.ico,*.png,*.tif,*.wmf|*.jpg;*.jpeg;*.bmp;*.gif;*.ico;*.png;*.tif;*.wmf";
            //  openFileDialog1.ShowDialog();    //“打开”对话框  "F:\\微云同步盘\\图片\\18[1].jpg"
            //myname = openFileDialog1.FileName;

            //      DirectoryInfo theFolder = new DirectoryInfo( @"F:\微云同步盘\图片\" );

            //      DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            //      //遍历文件夹
            //      //      ListBox listBox2 = new ListBox();

            ////      FileInfo[] fileInfo = dirInfo.GetFiles();

            //      foreach (FileInfo NextFile in fileInfo)  //遍历文件
            //          this.listBox2.Items.Add( NextFile.Name );

            //      foreach (DirectoryInfo NextFolder in dirInfo)
            //      {
            //           this.listBox1.Items.Add(NextFolder.Name);
            //          FileInfo[] fileInfo = NextFolder.GetFiles();

            //      }

            //DirectoryInfo dir = new DirectoryInfo( "c:\path" ); string[] files = dir.GetFiles();
            //foreach (string file in files)
            //{    //就是获得文件路径信息中除了文件扩展名之外的那部分---也就是 文件名了.  
            //    comboboxObj.Items.Add(Path.GetFileNameWithoutExtension(file));
            //}
            //foreach (ArrayList ret in myname)
            //{
            //    pictureBox1.Image = Image.FromFile( myname );  //显示打开图像
            //}  ret.Count;
            //    string str="";
            //    for (int i = 0; i < 4;  i++)
            //    {
            //         str = ret[i].ToString();
            //        pictureBox1.Image = Image.FromFile( str );  //显示打开图像
            //        int h = pictureBox1.Image.Height;
            //        int w = pictureBox1.Image.Width;

            //    //   Thread.Sleep( 2000 );//休眠时间
            //    }
            ////    pictureBox1.Image = Image.FromFile( str );  //显示打开图像

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Width >= 50)   //当图像的宽度值小于50时，就不能再缩小了
            {
                pictureBox1.Width = Convert.ToInt32( pictureBox1.Width * 0.8 );
                pictureBox1.Height = Convert.ToInt32( pictureBox1.Height * 0.8 );
            }
            else
            {
                MessageBox.Show( this, "图像已是最小，不能再缩小了！", "提示对话框", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Width < 310)    //当图像的宽度值大于310时，就不能再放大了
            {
                pictureBox1.Width = Convert.ToInt32( pictureBox1.Width * 1.2 );
                pictureBox1.Height = Convert.ToInt32( pictureBox1.Height * 1.2 );
            }
            else
            {
                MessageBox.Show( this, "图像已是最大，不能再放大了！", "提示对话框", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            //       Rectangle ScreenArea = Screen.GetWorkingArea( this );
            //这个区域包括任务栏，就是屏幕显示的物理范围
            Rectangle ScreenArea1 = Screen.GetBounds( this );
            int width1 = ScreenArea1.Width; //屏幕宽度 
            int height1 = ScreenArea1.Height; //屏幕高度

            this.Size = new Size( width1, height1 );
            this.Location = new Point( 360, -1920 );
            pictureBox1.Size = new Size( width1, height1 ); pictureBox1.Location = new Point( 0, 0 );

            FileStream fileStream = new FileStream( "c:\\gxh.jpg", FileMode.Open, FileAccess.Read );
            pictureBox1.Image = Image.FromStream( fileStream );
            fileStream.Close();     //释放内存
            fileStream.Dispose();

        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            //      if (e.Control != true)//如果没按Ctrl键                return;
            switch (key)
            {

                case Keys.F12:
                    Application.Exit();
                    pictureBox1.Dispose();
                    this.Close();
                    break;

            }
        }

    }
   
}

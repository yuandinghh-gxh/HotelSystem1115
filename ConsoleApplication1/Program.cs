/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace StructTest
{
    public partial class Form1 : Form {
        private Button button1;
        private Button button2;
        string filename = @"d:\poi.st";

        #region 结构体

        //90

        [StructLayout(LayoutKind.Sequential), Serializable]
        public struct MY_STRUCT
        {
            public double x;          //点的经度坐标
            public double y;          //点的纬度坐标
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string Name;        //Name[40]; //名称
            public long PointID;  //点的ID号
            public long TypeCode; //客户不使用该字段
        }

        #endregion

        public Form1()
        {
           InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MY_STRUCT[] arr = new MY_STRUCT[2];

            MY_STRUCT np = new MY_STRUCT();
            np.x = 114.123456;
            np.y = 23.56789;
            np.Name = "珠海市政府";
            np.PointID = Convert.ToInt64(1234);
            np.TypeCode = Convert.ToInt64(65);

            arr[0] = np;

            np = new MY_STRUCT();
            np.x = 115.123456;
            np.y = 24.56789;
            np.Name = "珠海市政府2";
            np.PointID = Convert.ToInt64(1235);
            np.TypeCode = Convert.ToInt64(66);

            arr[1] = np;

            int structSize = Marshal.SizeOf(typeof(MY_STRUCT));
            byte[] temp = new byte[structSize * arr.Length];
            byte[] temp1 = Struct2Byte(arr[0]);
            byte[] temp2 = Struct2Byte(arr[1]);

            Array.Copy(temp1, 0, temp, 0, temp1.Length);
            Array.Copy(temp2, 0, temp, structSize, temp2.Length);

            WriteInfo(temp);
        }


        public void WriteInfo(byte[] bt)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
                return;
            }

            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bt);
            bw.Flush();

            bw.Close();
            fs.Close();

            MessageBox.Show("保存成功!");
        }

        public byte[] ReadInfo(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            byte[] bt = br.ReadBytes(144);
            br.Close();
            fs.Close();

            return bt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] bt = ReadInfo(filename);

            int structSize = Marshal.SizeOf(typeof(MY_STRUCT));
            int num = bt.Length / structSize;

            for (int i = 0; i < num; i++)
            {
                byte[] temp = new byte[structSize];
                Array.Copy(bt, i * structSize, temp, 0, structSize);

                MY_STRUCT np = new MY_STRUCT();
                np = Byte2Struct(temp);
            }
        }


        private MY_STRUCT Byte2Struct(byte[] arr)
        {
            int structSize = Marshal.SizeOf(typeof(MY_STRUCT));
            IntPtr ptemp = Marshal.AllocHGlobal(structSize);
            Marshal.Copy(arr, 0, ptemp, structSize);
            MY_STRUCT rs = (MY_STRUCT)Marshal.PtrToStructure(ptemp, typeof(MY_STRUCT));
            Marshal.FreeHGlobal(ptemp);
            return rs;
        }

        private byte[] Struct2Byte(MY_STRUCT s)
        {
            int structSize = Marshal.SizeOf(typeof(MY_STRUCT));
            byte[] buffer = new byte[structSize];
            //分配结构体大小的内存空间 
            IntPtr structPtr = Marshal.AllocHGlobal(structSize);
            //将结构体拷到分配好的内存空间 
            Marshal.StructureToPtr(s, structPtr, false);
            //从内存空间拷到byte数组 
            Marshal.Copy(structPtr, buffer, 0, structSize);
            //释放内存空间 
            Marshal.FreeHGlobal(structPtr);
            return buffer;
        }

        private void InitializeComponent( ) {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(62, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(105, 125);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }
    }
    /// <summary>
    /// 设计器支持所需的方法 - 不要
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
  //  private class InitializeComponent()
  //      {

  ////          this.SuspendLayout();

  //          // Form1
  //          // 
  //          this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
  //          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
  //          this.ClientSize = new System.Drawing.Size( 292, 273 );


  //          this.Name = "Form1";
  //          this.Text = "Form1";
  //          this.ResumeLayout(false);
  //          this.PerformLayout();

  //  }

    //#endregion

}

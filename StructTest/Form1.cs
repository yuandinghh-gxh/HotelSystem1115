using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace StructTest    // 将 结构数据 写入文件中， 并且恢复 。。。。
{
	public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        readonly string filename = @"d:\poi.st";
        public int structSize = 0; 

        #region 结构体
        [StructLayout(LayoutKind.Sequential), Serializable]
        public struct MY_STRUCT
        {
            public double x;          //点的经度坐标
            public double y;          //点的纬度坐标
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string Name;        //Name[40]; //名称
            public long PointID;  //点的ID号
            public long TypeCode; //客户不使用该字段
           [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str;        //str?
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            MY_STRUCT[] arr = new MY_STRUCT[2];

            MY_STRUCT mY_STRUCT = new MY_STRUCT {
                x = 114.123456,
                y = 23.56789,
                Name = "ABCDEFG",
                PointID = Convert.ToInt64( 1234 ),
                TypeCode = Convert.ToInt64( 65 )
            };
            MY_STRUCT np = mY_STRUCT;

            arr[0] = np;

            np = new MY_STRUCT {
                x = 115.123456,
                y = 24.56789,
                Name = "abcdefgggggg12",
                PointID = Convert.ToInt64( 1235 ),
                TypeCode = Convert.ToInt64( 66 ),
                str = "12345678"
            };
            arr[1] = np;
            structSize = Marshal.SizeOf(typeof(MY_STRUCT));
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
            byte[] bt = br.ReadBytes(structSize*2);   //?? 77*2

 
            br.Close();
            fs.Close();

            return bt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] bt = ReadInfo(filename);
            structSize = Marshal.SizeOf(typeof(MY_STRUCT));
            int num = bt.Length / structSize;

            for (int i = 0; i < num; i++)
            {
                byte[] temp = new byte[structSize];
                Array.Copy(bt, i * structSize, temp, 0, structSize);  //将 bt 里面是数据复制到 temp，起始字节为  i * structSize，到temp，总是复制到0 开始的字节中。
                //_ = new MY_STRUCT();
                //_ = Byte2Struct( temp );
            }
        }


        private MY_STRUCT Byte2Struct(byte[] arr)   //复制 byte 到 结构中，恢复结构数据
        {
            structSize = Marshal.SizeOf(typeof(MY_STRUCT));
            IntPtr ptemp = Marshal.AllocHGlobal(structSize);
            Marshal.Copy(arr, 0, ptemp, structSize);
            MY_STRUCT rs = (MY_STRUCT)Marshal.PtrToStructure(ptemp, typeof(MY_STRUCT));
            Marshal.FreeHGlobal(ptemp);
            return rs;
        }

        private byte[] Struct2Byte(MY_STRUCT s)     // 结构到 byte 中
        {
            structSize = Marshal.SizeOf(typeof(MY_STRUCT));
            byte[] buffer = new byte[structSize];
            IntPtr structPtr = Marshal.AllocHGlobal(structSize);             //分配结构体大小的内存空间 
            Marshal.StructureToPtr(s, structPtr, false);				   //将结构体拷到分配好的内存空间 
            Marshal.Copy(structPtr, buffer, 0, structSize);				      //从内存空间拷到byte数组 
            return buffer;
        }

    }

}
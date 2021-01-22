using System;
using System.Drawing;
using System.Windows.Forms;

namespace t {
    public partial class Form2 : Form {
      //  mypicbox = new PictureBox;
        public Form2() {
            InitializeComponent();
        }

                private FullScreenHelper fullScreenHelper = null;
        private void pictureBox2_DoubleClick(object sender, EventArgs e) {
            if (fullScreenHelper==null) {
                fullScreenHelper=new FullScreenHelper(pictureBox2);
                fullScreenHelper.FullScreen(true);
            } else {
                fullScreenHelper.FullScreen(false);
                fullScreenHelper=null;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e) {
            if (fullScreenHelper==null) {
                fullScreenHelper=new FullScreenHelper(pictureBox2);
                fullScreenHelper.FullScreen(true);
            } else {
                fullScreenHelper.FullScreen(false);
                fullScreenHelper=null;
            }
        }

        private void Form2_Load(object sender, EventArgs e) {
            OpenFileDialog ofdPic = new OpenFileDialog();
            ofdPic.Filter="JPG(*.JPG;*.JPEG);gif文件(*.GIF)|*.jpg;*.jpeg;*.gif";
            ofdPic.FilterIndex=1;
            ofdPic.FileName="";
            if (ofdPic.ShowDialog()==DialogResult.OK) {
                string sPicPaht = ofdPic.FileName.ToString();
                Bitmap bmPic = new Bitmap(sPicPaht);
                Point ptLoction = new Point(bmPic.Size);
                if (ptLoction.X>pictureBox2.Size.Width||ptLoction.Y>pictureBox2.Size.Height) {
                    //圖像框的停靠方式   
                    //pcbPic.Dock = DockStyle.Fill;   
                    //圖像充滿圖像框，並且圖像維持比例   
                    pictureBox2.SizeMode=PictureBoxSizeMode.Zoom;
                } else {
                    //圖像在圖像框置中   
                    pictureBox2.SizeMode=PictureBoxSizeMode.CenterImage;
                }

                //LoadAsync：非同步載入圖像   

                    pictureBox2.LoadAsync(sPicPaht);
            }
        }
    }
}

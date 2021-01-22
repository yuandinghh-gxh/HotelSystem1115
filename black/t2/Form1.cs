using System;//来源于www.uzhanbao.com
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace t2 {
	public partial class Form1 : Form {

    //    private Rectangle selectedArea;
        private Image loadedImage;
        private Color selectionColor;
        private int count = 10;

        public Form1() {
            InitializeComponent();
            this.MouseWheel+=new MouseEventHandler(picBox1_MouseWheel);
            picBox1.SizeMode=PictureBoxSizeMode.Zoom;
        }

    //    private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            private void button1_Click(object sender, EventArgs e) {
                if (openFileDialog1.ShowDialog()==DialogResult.OK) {
                if (loadedImage!=null) {
                    loadedImage.Dispose();
                }
                try {

                    loadedImage=Image.FromFile(openFileDialog1.FileName);

                    //Get a contrasting color for the image selection marker
                    using (Bitmap bmp = new Bitmap(loadedImage)) {
                        selectionColor=GetDominantColor(bmp, false);
                        selectionColor=CalculateOppositeColor(selectionColor);
                    }
                    ///tZoom.Value = 1;
                    //resizePictureArea();
                    //Map the area selected in the thumbail to the actual image size
                    Rectangle zoomArea = new Rectangle();
                    Rectangle localArea = new Rectangle();
                    zoomArea.Width=loadedImage.Width;
                    zoomArea.Height=loadedImage.Height;
                    int aScale;
                    aScale=loadedImage.Width/picBox1.Width;

                    localArea.Width=picBox1.Width;
                    localArea.Height=loadedImage.Height/aScale;
                    picBox1.Image=ZoomImage(loadedImage, zoomArea, localArea);
                    picBox1.Refresh();

                } catch (Exception) {
                    MessageBox.Show("图片太大，请缩小图片尺寸！");
                }
            }
        }
        private Image ZoomImage(Image input, Rectangle zoomArea, Rectangle sourceArea) {
            Bitmap newBmp = new Bitmap(sourceArea.Width, sourceArea.Height);

            using (Graphics g = Graphics.FromImage(newBmp)) {
                //high interpolation
                g.InterpolationMode=InterpolationMode.HighQualityBicubic;

                g.DrawImage(input, sourceArea, zoomArea, GraphicsUnit.Pixel);
            }
            return newBmp;
        }
        private Color GetDominantColor(Bitmap bmp, bool includeAlpha) {
            // GDI+ still lies to us - the return format is BGRA, NOT ARGB.
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),                                         ImageLockMode.ReadWrite,                                           PixelFormat.Format32bppArgb);
            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;
            int r = 0;
            int g = 0;
            int b = 0;
            int a = 0;
            int total = 0;
            unsafe  {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride-bmp.Width*4;
                int nWidth = bmp.Width;
                for (int y = 0; y<bmp.Height; y++) {
                    for (int x = 0; x<nWidth; x++) {
                        r+=p[0];
                        g+=p[1];
                        b+=p[2];
                        a+=p[3];

                        total++;

                        p+=4;
                    }
                    p+=nOffset;
                }
            }
            bmp.UnlockBits(bmData);
            r/=total;
            g/=total;
            b/=total;
            a/=total;
            if (includeAlpha)
                return Color.FromArgb(a, r, g, b);
            else
                return Color.FromArgb(r, g, b);
        }
        /// <summary>
        /// Calculates the opposite color of a given color. 
        /// Source: http://dotnetpulse.blogspot.com/2007/01/function-to-calculate-opposite-color.html
        /// </summary>
        /// <param name="clr"></param>
        /// <returns></returns>
        private Color CalculateOppositeColor(Color clr) {
            return Color.FromArgb(255-clr.R, 255-clr.G, 255-clr.B);
        }

        /// <summary>
        /// Constricts a set of given dimensions while keeping aspect ratio.
        /// </summary>
        private Size ShrinkToDimensions(int originalWidth, int originalHeight, int maxWidth, int maxHeight) {
            int newWidth = 0;
            int newHeight = 0;

            if (originalWidth>=originalHeight) {
                //Match area width to max width
                if (originalWidth<=maxWidth) {
                    newWidth=originalWidth;
                    newHeight=originalHeight;
                } else {
                    newWidth=maxWidth;
                    newHeight=originalHeight*maxWidth/originalWidth;
                }
            } else {
                //Match area height to max height
                if (originalHeight<=maxHeight) {
                    newWidth=originalWidth;
                    newHeight=originalHeight;
                } else {
                    newWidth=originalWidth*maxHeight/originalHeight;
                    newHeight=maxHeight;
                }
            }

            return new Size(newWidth, newHeight);

        }
        //private void Form1_Load(object sender, EventArgs e)         //{
        //    picBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        //}
        private void Form1_Resize(object sender, EventArgs e) {
            //StretchImage;
            //picBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            int x = this.Width-20;
            int y = this.Height-70;
            //label1.Location = new Point(x, y); 
            picBox1.Width=this.Width-10;
            picBox1.Height=this.Height-27;

            Rectangle zoomArea = new Rectangle();
            Rectangle localArea = new Rectangle();

            zoomArea.Width=loadedImage.Width;
            zoomArea.Height=loadedImage.Height;


            int aScale; //这样简单处理可能会有问题
            aScale=loadedImage.Width/picBox1.Width;

            localArea.Width=picBox1.Width;
            localArea.Height=loadedImage.Height/aScale;
            picBox1.Image=ZoomImage(loadedImage, zoomArea, localArea);
            picBox1.Refresh();
        }

        private void picBox1_MouseWheel(object sender, MouseEventArgs e) {
            //MessageBox.Show("收到");
            zoom(e.Delta);
        }
        private void zoom(int delta) {
            if (delta>=1) {
                resize(++count);
            } else if (delta<=-1) {
                resize(--count);
            }
        }

        private void resize(int c) {
            //窗体宽高作为参照，所以窗体宽高不能变，可以考虑用其他作参照物，窗体和图片一起放大缩小
            int w = this.Width;
            int h = this.Height;
            decimal percent = (decimal)(c+100)/(decimal)100;
            decimal width = percent*w;
            decimal height = percent*h;
            picBox1.Width=Convert.ToInt32(width);
            picBox1.Height=Convert.ToInt32(height);
            panel1.Width=this.Width-10;
            panel1.Height=this.Height-60;
            if (panel1.Height<picBox1.Height||panel1.Width<picBox1.Width) {
                panel1.AutoScroll=true;
            }

        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {

        }

    }
}
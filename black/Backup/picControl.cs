/************************************************************************************
*源码来自(C#源码世界)  www.HelloCsharp.com
*如果对该源码有问题可以直接点击下方的提问按钮进行提问哦
*站长将亲自帮你解决问题
*C#源码世界-找到你需要的C#源码，交流和学习
************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WinControls
{
    public partial class picControl : UserControl
    {
        System.Drawing.Printing.PrintDocument docToPrint = new System.Drawing.Printing.PrintDocument();
        public picControl()
        {
            InitializeComponent();
            tsBigger.Click += new EventHandler(tsBigger_Click);
            tsCurrent.Click += new EventHandler(tsCurrent_Click);
            tsSmaller.Click += new EventHandler(tsSmaller_Click);
            tsLeft.Click += new EventHandler(tsLeft_Click);
            tsRight.Click += new EventHandler(tsRight_Click);
            tsSaved.Click += new EventHandler(tsSaved_Click);
            tsPrint.Click += new EventHandler(tsPrint_Click);
            tsReplace.Click += new EventHandler(tsReplace_Click);
            this.ModeChanged += new EventHandler(picControl_ModeChanged);
        }

        void picControl_ModeChanged(object sender, EventArgs e)
        {
            switch (iMode)
            {
                case 0:
                    this.Enabled = false;
                    break;
                case 1:
                    this.Enabled = true;
                    this.AllowReplace = false;
                    this.AllowInvert = false;
                    break;
                case 2:
                    this.Enabled = true;
                    this.AllowReplace = true;
                    this.AllowInvert = true;
                    break;
                default:
                    break;
            }
        }

        void tsReplace_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "圖像文檔 (*.jpg;*.jpeg;*.gif;*.bmp)|*.jpg;*.jpeg;*.gif;*.bmp";
            if (file.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(file.FileName);
                this.Image = img;
            }
        }

        void docToPrint_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            System.Drawing.Image image = this.Image;
            int x = e.MarginBounds.X;
            int y = e.MarginBounds.Y;
            int width = image.Width;
            int height = image.Height;
            if ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
            {
                width = e.MarginBounds.Width;
                height = image.Height * e.MarginBounds.Width / image.Width;
            }
            else
            {
                height = e.MarginBounds.Height;
                width = image.Width * e.MarginBounds.Height / image.Height;
            }
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);
            e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
        }

        void tsPrint_Click(object sender, EventArgs e)
        {
            PrintDialog print = new PrintDialog();
            print.ShowHelp = true;
            print.AllowSomePages = true;
            print.Document = docToPrint;
            if (print.ShowDialog() == DialogResult.OK)
            {
                docToPrint.Print();
            }
        }

        void tsSaved_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "圖像文檔 (*.jpg)|*.jpg|圖像文檔 (*.jpeg)|*.jpeg|圖像文檔 (*.gif)|*.gif|圖像文檔 (*.bmp)|*.bmp|所有文檔(*.*)|*.*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string sFilePath = saveFile.FileName;
                this.Image.Save(sFilePath);
            }
        }

        private Image img = null;
        /// <summary>
        /// 圖片
        /// </summary>
        [Browsable(true)]
        [DefaultValue(null)]
        [Category("Basic_Property"), Description("設置或獲取圖片。")]
        public Image Image
        {
            get
            {
                img = picBox1.Image;
                return img;
            }
            set
            {
                img = value;
                picBox1.Image = img;
            }
        }

        private bool breplace = true;
        /// <summary>
        /// 是否允許替換圖檔
        /// </summary>
        [Browsable(true)]
        [DefaultValue(true)]
        [Category("Basic_Property"), Description("設置獲得是否允許替換狀態。")]
        public bool AllowReplace
        {
            get
            {
                breplace = tsReplace.Enabled;
                return breplace;
            }
            set
            {
                breplace = value;
                tsReplace.Enabled = breplace;
            }
        }

        private bool bsave = true;
        /// <summary>
        /// 是否允許保存圖檔
        /// </summary>
        [Browsable(true)]
        [DefaultValue(true)]
        [Category("Basic_Property"), Description("設置獲得是否允許保存狀態。")]
        public bool AllowSave
        {
            get
            {
                bsave = tsSaved.Enabled;
                return bsave;
            }
            set
            {
                bsave = value;
                tsSaved.Enabled = bsave;
            }
        }

        private bool bprint = true;
        /// <summary>
        /// 是否允許列印圖檔
        /// </summary>
        [Browsable(true)]
        [DefaultValue(true)]
        [Category("Basic_Property"), Description("設置獲得是否允許列印狀態。")]
        public bool AllowPrint
        {
            get
            {
                bprint = tsPrint.Enabled;
                return bprint;
            }
            set
            {
                bprint = value;
                tsPrint.Enabled = bprint;
            }
        }

        private bool binvert= true;
        /// <summary>
        /// 是否允許翻轉圖檔
        /// </summary>
        [Browsable(true)]
        [DefaultValue(true)]
        [Category("Basic_Property"), Description("設置獲得是否允許翻轉狀態。")]
        public bool AllowInvert
        {
            get
            {
                binvert = tsLeft.Enabled;
                return binvert;
            }
            set
            {
                binvert = value;
                tsLeft.Enabled = binvert;
                tsRight.Enabled = binvert;
            }
        }

        /// <summary>
        /// 獲取或者設置改控件的狀態模式.
        /// </summary>
        int iMode = 2;
        [Browsable(true)]
        [DefaultValue(2)]
        [Category("Basic_Property"), Description("獲取或者設置改控件的狀態模式.")]
        public Int32 Mode
        {
            get
            {
                return iMode;
            }
            set
            {
                iMode = value;
                ChangeMode();
            }
        }

        private void ChangeMode()
        {
            //20070115 根據不同的狀態設定控件外觀
            EventArgs e = new EventArgs();
            OnModeChanged(this, e);
        }

        /// <summary>
        /// ModeChanged Event.
        /// </summary>
        [Browsable(true)]
        [Category("Basic_Event"), Description("觸發Mode屬性改變時的事件.")]
        public event EventHandler ModeChanged;
        protected virtual void OnModeChanged(object sender, EventArgs e)
        {
            if (ModeChanged != null)
            {
                ModeChanged(this, e);
            }
        }

        void tsRight_Click(object sender, EventArgs e)
        {
            Bitmap bitCurrent = new Bitmap(Image);
            bitCurrent.RotateFlip(RotateFlipType.Rotate90FlipNone);//See   "RotateFlipType"   in   msdn
            Image = bitCurrent;
        }

        void tsLeft_Click(object sender, EventArgs e)
        {
            ////按目標圖片框大小建立一個圖像對像   
            //Image srcImage = new Bitmap(this.Image.Width, Image.Height);
            ////建立一個Graphics   對像   
            //Graphics srcG = Graphics.FromImage(srcImage);
            ////旋轉  
            //srcG.RotateTransform(90, System.Drawing.Drawing2D.MatrixOrder.Append);   
            ////繪製已有圖像   
            //srcG.DrawImage(Image, 0, 0);
            ////保存狀態   
            //srcG.Save();
            ////將放大後的圖像給目標圖片框。   
            //Image = srcImage;  

            Bitmap bitCurrent = new Bitmap(Image);
            bitCurrent.RotateFlip(RotateFlipType.Rotate270FlipNone);//See   "RotateFlipType"   in   msdn
            Image = bitCurrent;
        }

        /// <summary>
        /// 縮小圖片檔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsSmaller_Click(object sender, EventArgs e)
        {
            if (this.picBox1.CurrentZoom > 1)
            {
                this.picBox1.CurrentZoom -= 1;
            }
        }

        /// <summary>
        /// 恢復原來實際大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsCurrent_Click(object sender, EventArgs e)
        {
            this.picBox1.CurrentZoom = this.picBox1.DefaultZoom;
        }

        /// <summary>
        /// 圖片放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsBigger_Click(object sender, EventArgs e)
        {
            this.picBox1.CurrentZoom += 1;
        }
    }
}
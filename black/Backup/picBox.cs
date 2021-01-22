/************************************************************************************
*源码来自(C#源码世界)  www.HelloCsharp.com
*如果对该源码有问题可以直接点击下方的提问按钮进行提问哦
*站长将亲自帮你解决问题
*C#源码世界-找到你需要的C#源码，交流和学习
************************************************************************************/
using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using Tpsc.Controls;

namespace WinControls
{
    /// <summary>
    /// The extended picturebox has many more options and features then the original picturebox
    /// </summary>
    public partial class picBox : System.Windows.Forms.ScrollableControl
    {
        System.Drawing.Printing.PrintDocument docToPrint = new System.Drawing.Printing.PrintDocument();
        /// <summary>
        /// Create a new extended picturebox
        /// </summary>
        public picBox()
        {
            InitComponent();
            InitializeComponent();
            //this.Controls.Add(msp);
            tsBigger.Click += new EventHandler(tsBigger_Click);
            tsCurrent.Click += new EventHandler(tsCurrent_Click);
            tsSmaller.Click += new EventHandler(tsSmaller_Click);
            tsLeft.Click += new EventHandler(tsLeft_Click);
            tsRight.Click += new EventHandler(tsRight_Click);
            tsSaved.Click += new EventHandler(tsSaved_Click);
            tsPrint.Click += new EventHandler(tsPrint_Click);

            docToPrint.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(docToPrint_PrintPage);

            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            base.AutoScroll = true;
            this.Size = new Size(200, 200);
            this.Paint += new PaintEventHandler(PictureBoxEx_Paint);

            _diagnosticMode = false;
            _doubleClickRestore = true;
            _borderStyle = BorderStyle.None;
            _drawMode = InterpolationMode.Default;

            // Init zoom fields
            _currentZoom = 1.0F;
            _defaultZoom = 1.0F;
            _maximumZoom = 3.0F;
            _minimumZoom = 0.10F;

            // Init drag window fields
            dashPattern = new Single[] { 5, 2, 5, 2 };

            dragWindow = Rectangle.Empty;
            dragStart = Point.Empty;
            dragEnd = Point.Empty;
            dragPen = new Pen(Color.Black, 1);
            dragPen.DashStyle = DashStyle.Custom;
            dragPen.DashPattern = dashPattern;

            _dragOptions = DragOptions.Prompt;
            _dragWindowMinimum = SystemInformation.DragSize;

            lmbDown = false;

            // Annotation fields
            _annotations = new AnnotationCollection(this);
            _annotations.AnnotationsChanged += new EventHandler(Annotations_AnnotationsChanged);
            selectedAnnote = null;

            generateTime = 0L;
            drawTime = 0L;
            setTime = 0L;
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
            print.Document = docToPrint;
            if (print.ShowDialog() == DialogResult.OK)
            {
                docToPrint.Print();
            }
        }

        void tsSaved_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "圖像文檔 (*.jpg)|*.jpeg|*.gif|*.bmp|所有文檔(*.*)|*.*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string sFilePath = saveFile.FileName;
                this.Image.Save(sFilePath);
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
            if (this.CurrentZoom > 1)
            {
                this.CurrentZoom -= 1;
            }
        }

        /// <summary>
        /// 恢復原來實際大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsCurrent_Click(object sender, EventArgs e)
        {
            this.CurrentZoom = this.DefaultZoom;
        }

        /// <summary>
        /// 圖片放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsBigger_Click(object sender, EventArgs e)
        {
            this.CurrentZoom += 1;
        }

        private void InitComponent()
        {
            this.cmnuPrompt = new ContextMenu();
            this.SuspendLayout();
            //
            // cmnuPrompt
            //
            //this.cmnuPrompt.MenuItems.Add("&Copy", new EventHandler(PromptMenuItem_Clicked));
            //this.cmnuPrompt.MenuItems.Add("放大", new EventHandler(PromptMenuItem_Clicked));
            MenuItem mCopy = new MenuItem("復制(&S)", new EventHandler(PromptMenuItem_Clicked));
            mCopy.Name = "mCopy";
            MenuItem mBig = new MenuItem("放大(&B)", new EventHandler(PromptMenuItem_Clicked));
            mBig.Name = "mBig";
            MenuItem mSmall = new MenuItem("縮小(&S)", new EventHandler(PromptMenuItem_Clicked));
            mSmall.Name = "mSmall";
            this.cmnuPrompt.MenuItems.Add(mBig);
            this.cmnuPrompt.MenuItems.Add(mCopy);
            // 
            // PictureBox
            // 
            this.ResumeLayout(false);
        }


        #region Fields and Properties

        private ContextMenu cmnuPrompt;

        private Image backup;  // Holds the original image that is used whenever the image is resized
        private Image resized; // Holds the entire image resized to the current zoom

        // Fields used by the drag window
        private Rectangle dragWindow;     // Holds the size and location of the drag window
        private Rectangle dragWindowSave; // Holds the drag window until the prompt has been completed
        private Pen dragPen;		      // The drag pen is used to draw the dragwindow
        private Point dragStart;		  // The coords where the drag started
        private Point dragEnd;			  // The coords where the drag ended
        private bool lmbDown;			  // True if the left mouse button is down

        /// <summary>
        /// Gets picturebox's collection of annotations
        /// </summary>
        public AnnotationCollection Annotations
        {
            get { return _annotations; }
        }
        private AnnotationCollection _annotations = null;
        private Annotation selectedAnnote; // Holds the currently selected annotation

        // Fields used for diagnostics
        long generateTime;			  // Time in milliseconds taken to generate resized image
        long drawTime;				  // Time in milliseconds taken to draw the controls elements
        long setTime;				  // Time in milliseconds taken to set the image (Inclides generate time)

        /// <summary>
        /// Gets or sets true if the user should be allowed to drag to copy or zoom
        /// </summary>
        [Category("Behavior"),
        DefaultValue(true),
        Description("Indicates whether or not the user should be allowed to drag and copy or zoom"),
        Browsable(true)]
        public bool AllowDrag
        {
            get
            {
                return _allowDrag;
            }
            set
            {
                if (_allowDrag != value)
                {
                    _allowDrag = value;

                    if (AllowDragChanged != null)
                        AllowDragChanged(this, new EventArgs());
                }
            }
        }
        private bool _allowDrag = true;
        /// <summary>
        /// Fires when the allow drag property is changed
        /// </summary>
        public event EventHandler AllowDragChanged;

        /// <summary>
        /// Gets or sets what type of border the PictureBox should have
        /// </summary>
        [Category("Appearance"),
        DefaultValue(BorderStyle.None),
        Description("Controls what type of border the PictureBox should have."),
        Browsable(true)]
        public BorderStyle BorderStyle
        {
            get
            {
                return _borderStyle;
            }
            set
            {
                if (_borderStyle != value)
                {
                    _borderStyle = value;

                    Invalidate();

                    if (BorderStyleChanged != null)
                        BorderStyleChanged(this, new EventArgs());
                }
            }
        }
        private BorderStyle _borderStyle;
        /// <summary>
        /// Fires when the BorderStyle property has been changed
        /// </summary>
        [Description("Fires when the BorderStyle property has been changed.")]
        public event EventHandler BorderStyleChanged;

        /// <summary>
        /// Gets or sets the picturebox's current zoom
        /// </summary>
        [Category("Behavior"),
        DefaultValue(1F),
        Description("Controls the PictureBox's current zoom."),
        Browsable(true)]
        public float CurrentZoom
        {
            get
            {
                return _currentZoom;
            }
            set
            {
                if (_currentZoom != value)
                {
                    if (value <= 0)
                        throw new ArgumentOutOfRangeException("The current zoom cannot be set equal to or less the 0");

                    if ((value > _maximumZoom && _currentZoom == _maximumZoom) || (value < _minimumZoom && _currentZoom == _minimumZoom))
                        return;						 // No change
                    else if (value > _maximumZoom)
                        _currentZoom = _maximumZoom; // Value exceeds max (set as max)
                    else if (value < _minimumZoom)
                        _currentZoom = _minimumZoom; // Value exceeds min (set as min)
                    else
                        _currentZoom = value;		 // Set as specified

                    if (backup != null)
                    {
                        GenerateResizedImage(); // Regenerate the resized image to the new current zoom

                        this.AutoScrollPosition = new Point(0, 0);
                        this.AutoScrollMinSize = resized.Size;
                    }

                    if (CurrentZoomChanged != null)
                        CurrentZoomChanged(this, new EventArgs());
                }
            }
        }
        private float _currentZoom;
        /// <summary>
        /// Fires when the CurrentZoom property has been changed
        /// </summary>
        [Description("Fires when the CurrentZoom property has been changed.")]
        public event EventHandler CurrentZoomChanged;

        /// <summary>
        /// Gets or sets the picturebox's default zoom
        /// </summary>
        [Category("Behavior"),
        DefaultValue(1F),
        Description("Controls the PictureBox's default zoom."),
        Browsable(true)]
        public float DefaultZoom
        {
            get
            {
                return _defaultZoom;
            }
            set
            {
                if (_defaultZoom != value)
                {
                    if (value <= 0)
                        throw new ArgumentOutOfRangeException("The default zoom cannot be set equal to or less the 0");

                    if ((value > _maximumZoom && _defaultZoom == _maximumZoom) || (value < _minimumZoom && _defaultZoom == _minimumZoom))
                        return;						 // No change
                    else if (value > _maximumZoom)
                        _defaultZoom = _maximumZoom; // Value exceeds max (set as max)
                    else if (value < _minimumZoom)
                        _defaultZoom = _minimumZoom; // Value exceeds min (set as min)
                    else
                        _defaultZoom = value;		 // Set as specified

                    if (DefaultZoomChanged != null)
                        DefaultZoomChanged(this, new EventArgs());
                }
            }
        }
        private float _defaultZoom;
        /// <summary>
        /// Fires when the DefaultZoom property has been changed
        /// </summary>
        [Description("Fires when the DefaultZoom property has been changed.")]
        public event EventHandler DefaultZoomChanged;

        /// <summary>
        /// Gets or sets true if the picturebox should be placed in diagnostic mode
        /// </summary>
        [Category("Behavior"),
        DefaultValue(false),
        Description("Set to true to display diagnostics information."),
        Browsable(true)]
        public bool DiagnosticMode
        {
            get
            {
                return _diagnosticMode;
            }
            set
            {
                if (_diagnosticMode != value)
                {
                    _diagnosticMode = value;

                    Invalidate();
                }
            }
        }
        private bool _diagnosticMode;

        /// <summary>
        /// Gets or sets the picturebox's ability to restore the image to the default zoom on double-click
        /// </summary>
        [Category("Behavior"),
        DefaultValue(true),
        Description("Controls the picturebox's ability to restore the image to the default zoom on double-click."),
        Browsable(true)]
        public bool DoubleClickRestore
        {
            get
            {
                return _doubleClickRestore;
            }
            set
            {
                if (_doubleClickRestore != value)
                {
                    _doubleClickRestore = value;

                    DoubleClickRestoreChanged(this, new EventArgs());
                }
            }
        }
        private bool _doubleClickRestore;
        /// <summary>
        /// Fires when the DoubleClickRestore property has been changed
        /// </summary>
        [Description("Fires when the DoubleClickRestore property has been changed.")]
        public event EventHandler DoubleClickRestoreChanged;

        /// <summary>
        /// Gets or sets the picturebox's drag options
        /// </summary>
        [Category("Behavior"),
        DefaultValue(DragOptions.Prompt),
        Description("Controls the PictureBox's drag options. (Copy, Zoom, or Prompt)"),
        Browsable(true)]
        public DragOptions DragOptions
        {
            get
            {
                return _dragOptions;
            }
            set
            {
                if (_dragOptions != value)
                {
                    _dragOptions = value;

                    if (DragOptionsChanged != null)
                        DragOptionsChanged(this, new EventArgs());
                }
            }
        }
        private DragOptions _dragOptions;
        /// <summary>
        /// Fires when the DragOptions property has been changed
        /// </summary>
        [Description("Fires when the DragOptions property has been changed.")]
        public event EventHandler DragOptionsChanged;

        /// <summary>
        /// Gets or sets the picturebox's drag window color
        /// </summary>
        [Category("Appearance"),
        DefaultValue(typeof(Color), "Black"),
        Description("Controls the picturebox's drag window color."),
        Browsable(true)]
        public Color DragWindowColor
        {
            get
            {
                return dragPen.Color;
            }
            set
            {
                if (!dragPen.Color.Equals(value))
                {
                    dragPen.Color = value;

                    Invalidate();

                    if (DragWindowColorChanged != null)
                        DragWindowColorChanged(this, new EventArgs());
                }
            }
        }
        /// <summary>
        /// Fires when the DragWindowColor property has been changed
        /// </summary>
        [Description("Fires when the DragWindowColor property has been changed.")]
        public event EventHandler DragWindowColorChanged;

        /// <summary>
        /// Gets or sets the picturebox's minimum invokable drag window size
        /// </summary>
        [Category("Behavior"),
        DefaultValue(typeof(Size), "4, 4"),
        Description("Controls the picturebox's minimum invokable drag window size."),
        Browsable(true)]
        public Size DragWindowMinimum
        {
            get
            {
                return _dragWindowMinimum;
            }
            set
            {
                if (_dragWindowMinimum != value)
                {
                    _dragWindowMinimum = value;

                    if (DragWindowMinimumChanged != null)
                        DragWindowMinimumChanged(this, new EventArgs());
                }
            }
        }
        private Size _dragWindowMinimum;
        /// <summary>
        /// Fires when the DragWindowMinimum property has been changed
        /// </summary>
        [Description("Fires when the DragWindowMinimum property has been changed.")]
        public event EventHandler DragWindowMinimumChanged;

        /// <summary>
        /// Gets or sets the dash pattern that is used by the control when drawing the drag window
        /// </summary>
        [Category("Appearance"),
        DefaultValue("5,2,5,2"),
        Description("The dash pattern that is used by the control when drawing the drag window."),
        Browsable(true)]
        public string DragWindowPattern
        {
            get
            {
                string pattern = null;

                foreach (Single number in dashPattern)
                {
                    if (pattern == null)
                        pattern += number.ToString();
                    else
                        pattern += "," + number.ToString();
                }

                return pattern;
            }
            set
            {
                try
                {
                    string[] pattern = value.Split(',');

                    dashPattern = new Single[pattern.Length];

                    for (int i = 0; i != pattern.Length; i++)
                        dashPattern[i] = Single.Parse(pattern[i].Trim());

                    dragPen.DashPattern = dashPattern;

                    if (DragWindowDashPatternChanged != null)
                        DragWindowDashPatternChanged(this, new EventArgs());
                }
                catch
                {
                    dashPattern = new Single[] { 5, 2, 5, 2 };
                }
            }
        }
        private Single[] dashPattern;
        /// <summary>
        /// Fires when the DragWindowPattern property has been changed
        /// </summary>
        [Description("Fires when the DragWindowPattern property has been changed.")]
        public event EventHandler DragWindowDashPatternChanged;

        /// <summary>
        /// Gets or sets imaging filter that will be applied to the image if it is resized
        /// </summary>
        [Category("Appearance"),
        DefaultValue(InterpolationMode.Default),
        Description("Controls the imaging filter that will be applied to the image if it is resized."),
        Browsable(true)]
        public InterpolationMode DrawMode
        {
            get
            {
                return _drawMode;
            }
            set
            {
                if (_drawMode != value)
                {
                    _drawMode = value;

                    if (backup != null)
                    {
                        GenerateResizedImage();
                    }

                    if (DrawModeChanged != null)
                        DrawModeChanged(this, new EventArgs());
                }
            }
        }
        private InterpolationMode _drawMode;
        /// <summary>
        /// Fires when the DrawMode property has been changed
        /// </summary>
        [Description("Fires when the DrawMode property has been changed.")]
        public event EventHandler DrawModeChanged;

        /// <summary>
        /// Gets or sets the Image for the PictureBox (Returns a resized and annotated image if applicable)
        /// </summary>
        [Category("Appearance"),
        DefaultValue(null),
        Description("The Image for the PictureBox."),
        Browsable(true)]
        public Image Image
        {
            get { return resized; }
            set
            {
                DateTime started = DateTime.Now;

                if (backup != value)
                {
                    if (value == null)
                    {
                        // Clear the current images
                        backup.Dispose(); backup = null;
                        resized.Dispose(); resized = null;
                    }
                    else
                    {
                        backup = value;

                        if (_currentZoom != _defaultZoom)
                            // Causes image to be resized and repainted
                            CurrentZoom = _defaultZoom;
                        else
                            // Resize and repaint the image manually
                            GenerateResizedImage();

                        this.AutoScrollMinSize = resized.Size;
                        this.AutoScrollPosition = new Point(this.Width / 2, this.Height / 2);
                        //this.AutoScrollPosition = new Point(0, 0);
                    }

                    _annotations.Clear();

                    if (ImageChanged != null)
                        ImageChanged(this, new EventArgs());
                }

                setTime = (long)DateTime.Now.Subtract(started).TotalMilliseconds;
            }
        }

        /// <summary>
        /// Fires when the Image has been changed
        /// </summary>
        [Description("Fires when the Image has been changed.")]
        public event EventHandler ImageChanged;
        /// <summary>
        /// Fires when a portion of the image has been copied
        /// </summary>
        [Description("Fires when a portion of the image has been copied.")]
        public event ImagePortionCopiedEventHandler ImagePortionCopied;

        /// <summary>
        /// Gets or sets minimum allowed zoom
        /// </summary>
        [Category("Appearance"),
        DefaultValue(3.0F),
        Description("Controls the maximum allowed zoom."),
        Browsable(true)]
        public float MaximumZoom
        {
            get
            {
                return _maximumZoom;
            }
            set
            {
                if (_maximumZoom != value)
                {
                    _maximumZoom = value;

                    if (MaximumZoomChanged != null)
                        MaximumZoomChanged(this, new EventArgs());

                    if (_currentZoom > _maximumZoom)
                        CurrentZoom = _maximumZoom;
                }
            }
        }
        private float _maximumZoom;
        /// <summary>
        /// Fires when the MaximumZoom property has been changed
        /// </summary>
        [Description("Fires when the MaximumZoom property has been changed.")]
        public event EventHandler MaximumZoomChanged;

        /// <summary>
        /// Gets or sets maximum allowed zoom
        /// </summary>
        [Category("Appearance"),
        DefaultValue(0.10F),
        Description("Controls the minimum allowed zoom."),
        Browsable(true)]
        public float MinimumZoom
        {
            get
            {
                return _minimumZoom;
            }
            set
            {
                if (_minimumZoom != value)
                {
                    _minimumZoom = value;

                    if (MinimumZoomChanged != null)
                        MinimumZoomChanged(this, new EventArgs());

                    if (_currentZoom < _minimumZoom)
                        CurrentZoom = _minimumZoom;
                }
            }
        }
        private float _minimumZoom;
        /// <summary>
        /// Fires when the MinimumZoom property has been changed
        /// </summary>
        [Description("Fires when the MaximumZoom property has been changed.")]
        public event EventHandler MinimumZoomChanged;

        /// <summary>
        /// Fires when the ScrollPosition property has been changed
        /// </summary>
        [Description("Fires when the ScrollPosition property has been changed."),
        Browsable(false)]
        public event EventHandler ScrollPositionChanged;

        /// <summary>
        /// Hides this property
        /// </summary>
        [Browsable(false)]
        new internal bool AutoScroll
        {
            get { return true; }
        }


        #region Constant Windwos Message Codes

        // Key strokes
        private const int MK_CONTROL = 0x0008;
        private const int MK_SHIFT = 0x0004;
        private const int VK_PRIOR = 0x0021; // PageDown
        private const int VK_NEXT = 0x0022; // PageUp
        private const int VK_END = 0x0023;
        private const int VK_HOME = 0x0024;
        private const int VK_LEFT = 0x0025;
        private const int VK_UP = 0x0026;
        private const int VK_RIGHT = 0x0027;
        private const int VK_DOWN = 0x0028;
        // Wheel movement
        private const int WM_KEYDOWN = 0x0101;
        private const int WM_MOUSEWHEEL = 0x020A;
        // User Scrolling
        private const int WM_HSCROLL = 0x0114;
        private MenuStrip msp;
        private ToolStripMenuItem tsCurrent;
        private ToolStripSeparator ts1;
        private ToolStripMenuItem tsBigger;
        private ToolStripMenuItem tsSmaller;
        private ToolStripSeparator ts2;
        private ToolStripMenuItem tsLeft;
        private ToolStripMenuItem tsRight;
        private ToolStripSeparator ts3;
        private ToolStripMenuItem tsReplace;
        private ToolStripMenuItem tsSaved;
        private ToolStripSeparator ts4;
        private ToolStripMenuItem tsPrint;
        private const int WM_VSCROLL = 0x0115;

        #endregion

        #endregion

        private void GenerateResizedImage()
        {
            // Used when the picturebox is in diag mode
            DateTime started = DateTime.Now;

            int resizedWidth = Convert.ToInt32(backup.Width * _currentZoom);
            int resizedHeight = Convert.ToInt32(backup.Height * _currentZoom);

            resized = new Bitmap(resizedWidth, resizedHeight);

            // Drag the backup image onto the resized image
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = (_currentZoom < 1F) ? _drawMode : InterpolationMode.Default;

                Rectangle srceRect = new Rectangle(0, 0, backup.Width, backup.Height);
                Rectangle destRect = new Rectangle(0, 0, resized.Width, resized.Height);

                g.DrawImage(backup, destRect, srceRect, GraphicsUnit.Pixel);

                // Add any annotations to the resized image
                DrawAnnotations(g);
            }

            Invalidate();
        }


        #region Draw Methods

        private void PictureBoxEx_Paint(object sender, PaintEventArgs e)
        {
            // Handles the painting of the control

            DateTime started = DateTime.Now;

            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);

            if (backup != null && resized != null)
            {

               // e.Graphics.DrawImageUnscaled(resized,
               //AutoScrollPosition.X + (this.Width - resized.Width) / 2, AutoScrollPosition.Y + (this.Height - resized.Height) / 2, resized.Width, resized.Height);


                e.Graphics.DrawImageUnscaled(resized,
                    AutoScrollPosition.X, AutoScrollPosition.Y, resized.Width, resized.Height);

                //居中對齊
                //e.Graphics.DrawImageUnscaled(resized,
                //(this.Width - resized.Width) / 2, (this.Height - resized.Height) / 2, resized.Width, resized.Height);

                DrawSelectedAnnotation(e.Graphics);

                DrawDragWindow(e.Graphics);

                DrawBorder(e.Graphics);

                drawTime = (long)DateTime.Now.Subtract(started).TotalMilliseconds;

                if (_diagnosticMode)
                    DrawDiagnostic(e.Graphics);
            }
        }
        private void DrawSelectedAnnotation(Graphics g)
        {
            if (selectedAnnote != null)
            {
                float fontSize = selectedAnnote.Font.Size * _currentZoom;

                Font font = new Font(selectedAnnote.Font.FontFamily,
                    fontSize, selectedAnnote.Font.Style, GraphicsUnit.Pixel);

                Brush brush = new SolidBrush(selectedAnnote.Color);

                SizeF sizeF = g.MeasureString(selectedAnnote.Text, font);

                g.DrawString(selectedAnnote.Text, font, brush, selectedAnnote.Location);

                g.DrawRectangle(dragPen, selectedAnnote.Location.X, selectedAnnote.Location.Y,
                    (int)sizeF.Width, (int)sizeF.Height);
            }
        }
        private void DrawDragWindow(Graphics g)
        {
            if (dragWindow != Rectangle.Empty)
                g.DrawRectangle(dragPen, dragWindow);
        }
        private void DrawBorder(Graphics g)
        {
            int hScrollHeight = (VScroll) ? 17 : 0;
            int vScrollWidth = (HScroll) ? 17 : 0;

            switch (_borderStyle)
            {
                case BorderStyle.Fixed3D:
                    Pen outsideUpperLeft = new Pen(Color.FromArgb(172, 168, 152), 1);
                    g.DrawLine(outsideUpperLeft, new Point(0, Height - 1), new Point(0, 0));
                    g.DrawLine(outsideUpperLeft, new Point(0, 0), new Point(Width - 1));

                    Pen insideUpperLeft = new Pen(Color.FromArgb(113, 111, 110), 1);
                    g.DrawLine(insideUpperLeft, new Point(1, Height - 2), new Point(1, 1));
                    g.DrawLine(insideUpperLeft, new Point(1, 1), new Point(Width - 2, 1));

                    Pen outsideLowerRight = new Pen(Color.FromArgb(255, 255, 255), 1);
                    g.DrawLine(outsideLowerRight, new Point(0, Height - 1 - hScrollHeight),
                        new Point(Width - 1, Height - 1 - hScrollHeight));
                    g.DrawLine(outsideLowerRight, new Point(Width - 1 - vScrollWidth,
                        Height - 1), new Point(Width - 1 - vScrollWidth, 0));

                    Pen insideLowerRight = new Pen(Color.FromArgb(241, 239, 226), 1);
                    g.DrawLine(insideLowerRight, new Point(1, Height - 2 - hScrollHeight),
                        new Point(Width - 2, Height - 2 - hScrollHeight));
                    g.DrawLine(insideLowerRight, new Point(Width - 2 - vScrollWidth,
                        Height - 2), new Point(Width - 2 - vScrollWidth, 2));

                    break;
                case BorderStyle.FixedSingle:
                    Pen fixedSingle = new Pen(Color.FromArgb(127, 157, 185), 1);
                    g.DrawRectangle(fixedSingle, 0, 0, Width - 1 - vScrollWidth,
                        Height - 1 - hScrollHeight);
                    break;
            }
        }
        private void DrawDiagnostic(Graphics g)
        {
            string message = string.Format(
                "Diagnostics Mode: On\r\n" +
                "Generate Time: {0}\r\n" +
                "DrawTime: {1}\r\n" +
                "Set Time: {2}\r\n" +
                "Auto Scroll Pos: {3}", generateTime, drawTime, setTime, AutoScrollPosition);

            g.DrawString(message, new Font("Arial", 16F, GraphicsUnit.Pixel),
                new SolidBrush(Color.Red), 10, 10);
        }

        private void DrawAnnotations(Graphics g)
        {
            foreach (Annotation annote in _annotations)
            {
                // float size = (float)annote.Font.Size * (1 / _currentZoom);
                Point location = new Point(Convert.ToInt32(annote.X * _currentZoom),
                    Convert.ToInt32(annote.Y * _currentZoom));

                float fontSize = annote.Font.Size * _currentZoom;

                g.DrawString(annote.Text, new Font(annote.Font.FontFamily, fontSize,
                    annote.Font.Style, GraphicsUnit.Pixel), new SolidBrush(annote.Color), location);
            }
        }


        #endregion

        #region Drag Methods

        /// <summary>
        /// Used to control dragging options
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (backup != null && resized != null)
            {
                // Determine if the selected coord is on top of an annotation
                selectedAnnote = GetAnnotationAt(e.X, e.Y);

                if (selectedAnnote != null)
                {
                    _annotations.Remove(selectedAnnote);

                    selectedAnnote.Location = new Point(e.X, e.Y);
                }
                else if (_allowDrag)
                {
                    lmbDown = true;
                    dragStart = new Point(e.X, e.Y);
                }
            }
        }
        /// <summary>
        /// Used to control dragging options
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (selectedAnnote != null)
            {
                selectedAnnote.Location = new Point(e.X, e.Y);

                Invalidate();
            }
            else if (_allowDrag && lmbDown && dragStart != Point.Empty)
            {
                Point dragCurrent = new Point(e.X, e.Y);

                if (ValidateDragWindowSize(dragCurrent))
                {
                    dragWindow = GenerateRectangle(dragStart, new Point(e.X, e.Y));

                    Invalidate();
                }
            }
        }
        /// <summary>
        /// Used to control dragging options
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (selectedAnnote != null)
            {
                _annotations.Add(selectedAnnote, true);

                selectedAnnote = null;
            }
            else if (lmbDown && e.Button == MouseButtons.Right)
            {
                // Cancel the current drag
                dragWindow = Rectangle.Empty;
                lmbDown = false;
            }
            else if (lmbDown && e.Button == MouseButtons.Left)
            {
                // End the drag
                lmbDown = false;

                if (dragStart == dragEnd)
                    return;

                dragEnd = new Point(e.X, e.Y);

                if (dragStart != Point.Empty && ValidateDragWindowSize(dragEnd))
                {
                    switch (_dragOptions)
                    {
                        case DragOptions.Prompt:
                            dragWindowSave = dragWindow;
                            cmnuPrompt.Show(this, dragEnd);
                            break;
                        case DragOptions.Copy:
                            Copy();
                            break;
                        case DragOptions.Zoom:
                            Zoom();
                            break;
                    }
                }

                dragWindow = Rectangle.Empty;
                dragStart = Point.Empty;
                dragEnd = Point.Empty;
            }
        }


        private bool ValidateDragWindowSize(Point dragCurrent)
        {
            Rectangle dragWindow = GenerateRectangle(dragStart, dragCurrent);

            if (dragWindow.Width < _dragWindowMinimum.Width ||
                dragWindow.Height < _dragWindowMinimum.Height)
                return false;
            else
                return true;
        }
        private Rectangle GenerateRectangle(Point a, Point b)
        {
            Point topLeft = new Point(0, 0);
            Point bottomRight = new Point(0, 0);

            if (a.X < b.X)
            {	// Dragging to the right.
                if (a.Y < b.Y)
                {	// Right & Down
                    topLeft = a;
                    bottomRight = b;
                }
                else
                {	// Right & Up
                    topLeft = new Point(a.X, b.Y);
                    bottomRight = new Point(b.X, a.Y);
                }
            }
            else if (b.X < a.X)
            {	// Dragging to the left.
                if (a.Y < b.Y)
                {	// Left & Down
                    topLeft = new Point(b.X, a.Y);
                    bottomRight = new Point(a.X, b.Y);
                }
                else
                {	// Left & Up
                    topLeft = b;
                    bottomRight = a;
                }
            }

            int width = bottomRight.X - topLeft.X;
            int height = bottomRight.Y - topLeft.Y;

            Rectangle rectangle = new Rectangle(topLeft, new Size(width, height));

            return rectangle;
        }


        private void PromptMenuItem_Clicked(object sender, EventArgs e)
        {
            string sName = ((MenuItem)sender).Name;
            if (sName.Equals("mbig", StringComparison.CurrentCultureIgnoreCase))
                //Copy();
                Zoom();
            else if (sName.Equals("mSmall", StringComparison.CurrentCultureIgnoreCase))
                Zoom();
            else if (sName.Equals("mCopy", StringComparison.CurrentCultureIgnoreCase))
                Copy();
        }

        private void Copy()
        {
            // Up/Down convert drag window size to match
            float multiplier = 1 / _currentZoom;

            int dragWidth = (int)((float)dragWindowSave.Width * multiplier);
            int dragHeight = (int)((float)dragWindowSave.Height * multiplier);

            int xOffset = -AutoScrollPosition.X;
            int yOffset = -AutoScrollPosition.Y;

            int dragX = Convert.ToInt32(((float)dragWindowSave.X + xOffset) * multiplier);
            int dragY = Convert.ToInt32(((float)dragWindowSave.Y + yOffset) * multiplier);

            Bitmap copy = new Bitmap(dragWidth, dragHeight);

            Rectangle srcRect = new Rectangle(dragX, dragY, dragWidth, dragHeight);

            using (Graphics g = Graphics.FromImage(copy))
            {
                Rectangle destRect = new Rectangle(0, 0, dragWidth, dragHeight);

                g.DrawImage(backup, destRect, srcRect, GraphicsUnit.Pixel);
            }

            Clipboard.SetDataObject(copy, true);

            if (ImagePortionCopied != null)
                ImagePortionCopied(this, new ImagePortionCopiedEventArgs(copy, srcRect));

            Invalidate();
        }
        private void Zoom()
        {
            // This version of the picturebox control no longer needs an autoscroll parent

            if (resized == null)
                return; // If there is no image there is nothing to zoom in on

            // The zoom window will never be proportional to the container, so one
            // side will get filled while the other gets centered. The larger % will
            // determine which gets filled, and which gets centered
            float xRatio = (float)Width / (float)dragWindowSave.Width;
            float yRatio = (float)Height / (float)dragWindowSave.Height;

            float largerRatio;
            int xAdjust = 0;
            int yAdjust = 0;

            largerRatio = (xRatio < yRatio) ? xRatio : yRatio;

            // The cumulative zoom cannot exceed the maximum zoom;
            if ((largerRatio * _currentZoom) > _maximumZoom)
                largerRatio = _maximumZoom / _currentZoom;

            if (dragWindowSave.Width * largerRatio > Width)
                largerRatio = (float)Width / (float)dragWindowSave.Width;

            if (dragWindowSave.Height * largerRatio > Height)
                largerRatio = (float)Height / (float)dragWindowSave.Height;

            yAdjust = Convert.ToInt32(
                ((float)Height - (float)dragWindowSave.Height * largerRatio) / 2F);
            xAdjust = Convert.ToInt32(
                ((float)Width - (float)dragWindowSave.Width * largerRatio) / 2F);

            int xScrollPos = Math.Max(Convert.ToInt32(((float)-AutoScrollPosition.X + (float)dragWindowSave.X) * largerRatio) - xAdjust, 0);
            int yScrollPos = Math.Max(Convert.ToInt32(((float)-AutoScrollPosition.Y + (float)dragWindowSave.Y) * largerRatio) - yAdjust, 0);

            CurrentZoom *= largerRatio;

            this.AutoScrollPosition = new Point(xScrollPos, yScrollPos);

            Invalidate();
        }


        #endregion

        #region Annotation Methods

        /// <summary>
        /// Gets the annotation at the specified coordinates
        /// </summary>
        public Annotation GetAnnotationAt(int x, int y)
        {
            float ratio = (1 / _currentZoom);

            int xOffset = -AutoScrollPosition.X;
            int yOffset = -AutoScrollPosition.Y;

            int adjustedX = Convert.ToInt32((x + xOffset) * ratio);
            int adjustedY = Convert.ToInt32((y + yOffset) * ratio);

            foreach (Annotation annote in _annotations)
                if (annote.Bounds.Contains(adjustedX, adjustedY))
                    return annote;

            return null;
        }
        /// <summary>
        /// Gets the annotation at the specified coordinates
        /// </summary>
        public Annotation GetAnnotationAt(Point location)
        {
            return GetAnnotationAt(location.X, location.Y);
        }


        private void Annotations_AnnotationsChanged(object sender, EventArgs e)
        {
            GenerateResizedImage(); // Recreate the image with annotations

            Invalidate(); // Redraw the image
        }


        #endregion

        #region Scrolling Methods

        public void Scroll(ScrollDirection direction, bool smallChange)
        {
            int x = -AutoScrollPosition.X;
            int y = -AutoScrollPosition.Y;

            int change = (smallChange) ? 100 : 250;

            if (direction == ScrollDirection.Left || direction == ScrollDirection.Up)
                if (direction == ScrollDirection.Up)
                    y = y - change;
                else
                    x = x - change;
            else
                if (direction == ScrollDirection.Down)
                    y = y + change;
                else
                    x = x + change;

            this.AutoScrollPosition = new Point(x, y);

            Invalidate();
        }


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_MOUSEWHEEL)
            {
                ProcessWheelMovement(ref m);

                Invalidate();
            }
            else if (m.Msg == WM_HSCROLL || m.Msg == WM_VSCROLL)
            {
                Invalidate();

                if (ScrollPositionChanged != null)
                    ScrollPositionChanged(this, new EventArgs());
            }
        }

        protected override bool ProcessKeyMessage(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN)
                return ProcessKeyDown(ref m);
            else
                return base.ProcessKeyMessage(ref m);
        }

        private void ProcessWheelMovement(ref Message m)
        {
            int wParam = (int)m.WParam;

            if (wParam > 0)
            {
                // The wheel is being scrolled up
                if ((wParam & MK_SHIFT) == MK_SHIFT)
                {
                    if (_currentZoom + .10F < _maximumZoom)
                        CurrentZoom += .10F;
                }
                else if ((wParam & MK_CONTROL) == MK_CONTROL)
                    Scroll(ScrollDirection.Left, true);
            }
            else
            {
                // The wheel is being scrolled down
                if ((wParam & MK_SHIFT) == MK_SHIFT)
                {
                    if (_currentZoom - .10F > _minimumZoom)
                        CurrentZoom -= .10F;
                }
                else if ((wParam & MK_CONTROL) == MK_CONTROL)
                    Scroll(ScrollDirection.Right, true);
            }
        }

        private bool ProcessKeyDown(ref Message m)
        {
            switch ((int)m.WParam)
            {
                case VK_PRIOR:
                    Scroll(ScrollDirection.Up, false);
                    return true;
                case VK_NEXT:
                    Scroll(ScrollDirection.Down, false);
                    return true;
                case VK_END:
                    Scroll(ScrollDirection.Right, false);
                    return true;
                case VK_HOME:
                    Scroll(ScrollDirection.Left, false);
                    return true;
                case VK_LEFT:
                    Scroll(ScrollDirection.Left, true);
                    return true;
                case VK_UP:
                    Scroll(ScrollDirection.Up, true);
                    return true;
                case VK_RIGHT:
                    Scroll(ScrollDirection.Right, true);
                    return true;
                case VK_DOWN:
                    Scroll(ScrollDirection.Down, true);
                    return true;
                default:
                    return false;
            }
        }


        #endregion

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            this.Select();

            Invalidate(); // Force a redraw
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Invalidate(); // Force a redraw
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            if (_currentZoom != _defaultZoom)
                CurrentZoom = _defaultZoom;
        }


        /// <summary>
        /// Rotate or flip the original image
        /// </summary>
        /// <param name="type"></param>
        public void RotateFlip(RotateFlipType type)
        {
            backup.RotateFlip(type);

            GenerateResizedImage();

            Invalidate();
        }
        /// <summary>
        /// Will automatically shrink or enlarge the image to match the height of the control
        /// </summary>
        public void FitHeight()
        {
            float fitHeightZoom = (float)(Height - 1) / (float)backup.Height;

            if (fitHeightZoom < _minimumZoom)
                fitHeightZoom = _minimumZoom;

            CurrentZoom = fitHeightZoom;
        }
        /// <summary>
        /// Will automatically shrink or enlarge the image to match the width of the control
        /// </summary>
        public void FitWidth()
        {
            float fitWidthZoom = (float)(Width - 1) / (float)backup.Width;

            if (fitWidthZoom < _minimumZoom)
                fitWidthZoom = _minimumZoom;

            CurrentZoom = fitWidthZoom;
        }

        /// <summary>
        /// Gets the cursor used by the picturebox to add an annotation
        /// </summary>
        public static Cursor AnnotationCursor
        {
            get
            {
                System.Reflection.Assembly assembly;
                assembly = System.Reflection.Assembly.GetAssembly(typeof(picBox));

                return new Cursor(assembly.GetManifestResourceStream("Tpsc.Controls.Annotation.cur"));
            }
        }


        #region Nested Classes

        /// <summary>
        /// AnnotationCollection is used to store annotations for the picturebox.
        /// Annotations are designed to be xml serializable
        /// </summary>
        public class AnnotationCollection : CollectionBase
        {
            /// <summary>
            /// Creates a new annotation collection
            /// </summary>
            public AnnotationCollection()
            {
                AnnotationsChanged += new EventHandler(OnAnnotationCollection_AnnotationsChanged);
            }
            /// <summary>
            /// Creates a new annotation collection
            /// </summary>
            public AnnotationCollection(picBox owner)
            {
                this.owner = owner;

                AnnotationsChanged += new EventHandler(OnAnnotationCollection_AnnotationsChanged);
            }
            /// <summary>
            /// Creates a new annotation collection
            /// </summary>
            public AnnotationCollection(Annotation[] annotations)
            {
                AnnotationsChanged += new EventHandler(OnAnnotationCollection_AnnotationsChanged);

                foreach (Annotation annote in annotations)
                    Add(annote, false);
            }
            /// <summary>
            /// Creates a new annotation collection
            /// </summary>
            public AnnotationCollection(picBox owner, Annotation[] annotations)
                : this(annotations)
            {
                this.owner = owner;
            }


            private picBox owner = null;

            /// <summary>
            /// Fires whenever an annotation is changed, added or removed
            /// </summary>
            public event EventHandler AnnotationsChanged;

            /// <summary>
            /// Add an annotation to the collection
            /// </summary>
            /// <param name="annotation">Annotation to be added</param>
            /// <param name="adjustLocation">True if the annotation should be adjusted for the picturebox's current zoom</param>
            /// <returns>The index of the newly added annotation</returns>
            public int Add(Annotation annotation, bool adjustLocation)
            {
                bool found = Remove(annotation);

                annotation.Changed += new EventHandler(annotation_Changed);

                if (adjustLocation && owner != null)
                {
                    float ratio = 1F / owner._currentZoom;

                    int adjustedX = Convert.ToInt32((annotation.X + -owner.AutoScrollPosition.X) * ratio);
                    int adjustedY = Convert.ToInt32((annotation.Y + -owner.AutoScrollPosition.Y) * ratio);

                    annotation.Location = new Point(adjustedX, adjustedY);
                }

                int index = InnerList.Add(annotation);

                if (!found)
                    AnnotationsChanged(this, new EventArgs());

                return index;
            }
            /// <summary>
            /// Add an annotation to the collection
            /// </summary>
            /// <param name="text">The annotations desired text</param>
            /// <param name="font">The annotations desired font</param>
            /// <param name="color">The annotations desired color</param>
            /// <param name="location">The annotations desired location</param>
            /// <param name="adjustLocation">True if the annotation should be adjusted for the picturebox's current zoom</param>
            /// <returns>The index of the newly added annotation</returns>
            public int Add(string text, Font font, Color color, Point location, bool adjustLocation)
            {
                Annotation newAnnote = new Annotation(text, font, color, location);

                newAnnote.Changed += new EventHandler(annotation_Changed);

                return Add(newAnnote, adjustLocation);
            }
            /// <summary>
            /// Adds one or more annotations to the collection w/o adjustment
            /// </summary>
            /// <param name="annotations"></param>
            public void AddRange(Annotation[] annotations)
            {
                foreach (Annotation annotation in annotations)
                {
                    bool found = Remove(annotation);

                    InnerList.Add(annotation);

                    if (!found)
                        annotation.Changed += new EventHandler(annotation_Changed);
                }

                AnnotationsChanged(this, new EventArgs());
            }
            /// <summary>
            /// Removes an annotation from the collection
            /// </summary>
            /// <param name="annotation"></param>
            /// <returns></returns>
            public bool Remove(Annotation annotation)
            {
                foreach (Annotation annote in InnerList)
                    if (annote.Guid == annotation.Guid)
                    {
                        annotation.Changed -= new EventHandler(annotation_Changed);

                        InnerList.Remove(annote);

                        AnnotationsChanged(this, new EventArgs());

                        return true;
                    }

                return false;
            }


            /// <summary>
            /// Removes all annotations from the collection
            /// </summary>
            protected override void OnClear()
            {
                if (InnerList.Count > 0)
                {
                    InnerList.Clear();
                    AnnotationsChanged(this, new EventArgs());
                }
            }

            /// <summary>
            /// Returns the annotatinos at the desired index
            /// </summary>
            public Annotation this[int index]
            {
                get
                {
                    return (Annotation)InnerList[index];
                }
                set
                {
                    InnerList[index] = value;
                }
            }

            /// <summary>
            /// Returns the current collection as an array of annotations
            /// </summary>
            /// <returns></returns>
            public Annotation[] ToArray()
            {
                return (Annotation[])InnerList.ToArray(typeof(Annotation));
            }


            /// <summary>
            /// Opens a collection from a file
            /// </summary>
            /// <param name="fileName">File name and path the the file</param>
            public void Open(string fileName)
            {
                Clear();

                if (System.IO.File.Exists(fileName))
                {
                    Type type = typeof(Annotation[]);

                    using (FileStream fs = new FileStream(fileName, FileMode.Open))
                    {
                        System.Xml.Serialization.XmlSerializer x;
                        x = new System.Xml.Serialization.XmlSerializer(type);
                        AddRange((Tpsc.Controls.Annotation[])x.Deserialize(fs));
                    }
                }

                AnnotationsChanged(this, new EventArgs());
            }

            /// <summary>
            /// Saves the collection to a file
            /// </summary>
            /// <param name="fileName">File name and path for the desired file</param>
            public void Save(string fileName)
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    Type type = typeof(Annotation[]);

                    System.Xml.Serialization.XmlSerializer x;
                    x = new System.Xml.Serialization.XmlSerializer(type);
                    x.Serialize(sw, ToArray());
                }
            }


            private void annotation_Changed(object sender, EventArgs e)
            {
                if (InnerList.Contains(sender))
                    AnnotationsChanged(sender, e);
            }


            protected virtual void OnAnnotationCollection_AnnotationsChanged(object sender, EventArgs e)
            {

            }
        }

        #endregion

        private void InitializeComponent()
        {
            this.msp = new System.Windows.Forms.MenuStrip();
            this.tsCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.ts1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBigger = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSmaller = new System.Windows.Forms.ToolStripMenuItem();
            this.ts2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ts3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSaved = new System.Windows.Forms.ToolStripMenuItem();
            this.ts4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.msp.SuspendLayout();
            this.SuspendLayout();
            // 
            // msp
            // 
            this.msp.BackColor = System.Drawing.Color.Transparent;
            this.msp.Dock = System.Windows.Forms.DockStyle.Top;
            this.msp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCurrent,
            this.ts1,
            this.tsBigger,
            this.tsSmaller,
            this.ts2,
            this.tsLeft,
            this.tsRight,
            this.ts3,
            this.tsReplace,
            this.tsSaved,
            this.ts4,
            this.tsPrint});
            this.msp.Location = new System.Drawing.Point(0, 0);
            this.msp.Name = "msp";
            this.msp.Size = new System.Drawing.Size(200, 24);
            this.msp.TabIndex = 0;
            this.msp.Text = "menuStrip";
            // 
            // tsCurrent
            // 
            this.tsCurrent.Name = "tsCurrent";
            this.tsCurrent.Size = new System.Drawing.Size(65, 20);
            this.tsCurrent.Text = "實際大小";
            // 
            // ts1
            // 
            this.ts1.Name = "ts1";
            this.ts1.Size = new System.Drawing.Size(6, 20);
            // 
            // tsBigger
            // 
            this.tsBigger.Name = "tsBigger";
            this.tsBigger.Size = new System.Drawing.Size(41, 20);
            this.tsBigger.Text = "放大";
            // 
            // tsSmaller
            // 
            this.tsSmaller.Name = "tsSmaller";
            this.tsSmaller.Size = new System.Drawing.Size(41, 20);
            this.tsSmaller.Text = "縮小";
            // 
            // ts2
            // 
            this.ts2.Name = "ts2";
            this.ts2.Size = new System.Drawing.Size(6, 20);
            // 
            // tsLeft
            // 
            this.tsLeft.Name = "tsLeft";
            this.tsLeft.Size = new System.Drawing.Size(41, 20);
            this.tsLeft.Text = "左轉";
            // 
            // tsRight
            // 
            this.tsRight.Name = "tsRight";
            this.tsRight.Size = new System.Drawing.Size(41, 20);
            this.tsRight.Text = "右轉";
            // 
            // ts3
            // 
            this.ts3.Name = "ts3";
            this.ts3.Size = new System.Drawing.Size(6, 20);
            // 
            // tsReplace
            // 
            this.tsReplace.Name = "tsReplace";
            this.tsReplace.Size = new System.Drawing.Size(41, 20);
            this.tsReplace.Text = "替換";
            // 
            // tsSaved
            // 
            this.tsSaved.Name = "tsSaved";
            this.tsSaved.Size = new System.Drawing.Size(41, 20);
            this.tsSaved.Text = "另存";
            // 
            // ts4
            // 
            this.ts4.Name = "ts4";
            this.ts4.Size = new System.Drawing.Size(6, 20);
            // 
            // tsPrint
            // 
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(41, 20);
            this.tsPrint.Text = "列印";
            this.msp.ResumeLayout(false);
            this.msp.PerformLayout();
            this.ResumeLayout(false);

        }
    }

    /// <summary>
    /// The different directions the form can be scrolled
    /// </summary>
    public enum ScrollDirection
    {
        /// <summary>
        /// Scroll Left
        /// </summary>
        Left = 37,
        /// <summary>
        /// Scroll Up
        /// </summary>
        Up = 38,
        /// <summary>
        /// Scroll Right
        /// </summary>
        Right = 39,
        /// <summary>
        /// Scroll Down
        /// </summary>
        Down = 40,
    }

    /// <summary>
    /// Options available after selecting a portion of the image
    /// </summary>
    public enum DragOptions
    {
        /// <summary>
        /// Prompt the user to choose to drag or copy
        /// </summary>
        Prompt = 0,
        /// <summary>
        /// Zoom in on the image
        /// </summary>
        Zoom,
        /// <summary>
        /// Copy the selected portion of the image
        /// </summary>
        Copy
    }

    /// <summary>
    /// Facilitates the copy drag window event
    /// </summary>
    public delegate void ImagePortionCopiedEventHandler(object sender, ImagePortionCopiedEventArgs e);
    /// <summary>
    /// Event argumets that pass the copied portions of the image.
    /// </summary>
    public class ImagePortionCopiedEventArgs : EventArgs
    {
        /// <summary>
        /// Event argumets that pass the copied portions of the image.
        /// </summary>
        public ImagePortionCopiedEventArgs(System.Drawing.Bitmap image, Rectangle rectangle)
        {
            _image = image;
            _rectangle = rectangle;
        }


        /// <summary>
        /// The copied portion of the image.
        /// </summary>
        public Bitmap Image
        {
            get { return _image; }
            set { _image = value; }
        }
        private Bitmap _image = null;

        /// <summary>
        /// The drag window info used to copy this portion of the image.
        /// </summary>
        public Rectangle Rectangle
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }
        private Rectangle _rectangle;
    }
}
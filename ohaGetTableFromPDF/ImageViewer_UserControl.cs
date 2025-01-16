namespace TextOCR_PDF
{
    public partial class ImageViewer_UserControl : Emgu.CV.UI.ImageBox
    {
        public ImageViewer_UserControl()
        {
            InitializeComponent();
        }

        List<Rectangle> Rectangles;

        /// <summary>
        /// Create an ImageViewer
        /// </summary>
        //public ImageViewer()
        //{
        //    InitializeComponent();
        //}

        /// <summary>
        /// Create an ImageViewer from the specific <paramref name="image"/>
        /// </summary>
        /// <param name="image">The image to be displayed in this viewer</param>
        public ImageViewer_UserControl(Mat image, List<Rectangle> rectangles)
           : this()
        {
            if (image != null)
            {
                Size size = image.Size;
                size.Width += 12;
                size.Height += 38;
                if (!Size.Equals(size))
                    Size = size;
            }
            Image = image;
            Rectangles = rectangles;
        }

        /// <summary>
        /// Create an ImageViewer from the specific <paramref name="image"/>, using <paramref name="imageName"/> as window name
        /// </summary>
        /// <param name="image">The image to be displayed</param>
        /// <param name="imageName">The name of the image</param>
        public ImageViewer_UserControl(Mat image, string imageName, List<Rectangle> rectangles)
           : this(image, rectangles)
        {
            Text = imageName;
            Rectangles = rectangles;
        }

        /// <summary>
        /// Get or Set the image in this ImageViewer
        /// </summary>
        //public IInputArray Image
        //{
        //    get
        //    {
        //        return Image;
        //    }
        //    set
        //    {
        //        Image = value;
        //    }
        //}

        /// <summary>
        /// Get the image box hosted in this viewer
        /// </summary>

        /// <summary>
        /// Create a ImageViewer with the specific image and show it.
        /// </summary>
        /// <param name="image">The image to be displayed in ImageViewer</param>
        public void Show(Mat image, List<Rectangle> Rectangles)
        {
            //new ImageViewer(image, Rectangles).ShowDialog();
        }

        /// <summary>
        /// Create a ImageViewer with the specific image and show it.
        /// </summary>
        /// <param name="image">The image to be displayed in ImageViewer</param>
        /// <param name="windowName">The name of the window</param>
        public void Show(Mat image, String windowName, List<Rectangle> Rectangles)
        {
            //new ImageViewer(image, windowName, Rectangles).ShowDialog();
        }

        private void imageBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var ib = (ImageBox)sender;
            var x_offset = ib.HorizontalScrollBar.Value;
            var y_offset = ib.VerticalScrollBar.Value;
            var x = e.Location.X;
            var y = e.Location.Y;
            var zoom = ib.ZoomScale;
            int pixelMousePosX = (int)((x / zoom) + x_offset);
            int pixelMousePosY = (int)((y / zoom) + y_offset);

            Point point = new Point(pixelMousePosX, pixelMousePosY);

            var match_rect = Rectangles.FirstOrDefault(a => a.Contains(point));

            MessageBox.Show("Mouse X: " + pixelMousePosX + "," + "Mouse Y: " + pixelMousePosY + Environment.NewLine +
                 "MATCH !!!: " + Environment.NewLine +
                 "X      = " + match_rect.X + Environment.NewLine +
                 "Right  = " + match_rect.Right + Environment.NewLine +
                 "Y      = " + match_rect.Y + Environment.NewLine +
                 "Height = " + match_rect.Height
                );
        }

        internal void LoadImage(Mat image_cv, List<Rectangle> boxes)
        {
            Image = image_cv;
            Rectangles = boxes;

            Refresh();
        }
    }
}

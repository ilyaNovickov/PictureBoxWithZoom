using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestControlApp
{

    public partial class WorkSheet: UserControl
    {
        #region Конструктор
        public WorkSheet()
        {
            InitializeComponent();
            OnZoomChanged += ZoomChanged;
            OnImageChanged += ImageChanged;
            OnPictureBoxMouseClick += WorkSheet_OnPictureBoxMouseClick;
        }
        #endregion
        private void WorkSheet_OnPictureBoxMouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(e.Location.ToString());
        }
        /*
         * Подумать об координатах маштабированного изображения
         */
        int zoom = 100;
        event EventHandler zoomChanged;
        event EventHandler imageChanged;
        event MouseEventHandler pictureBoxMouseClick;
        public event EventHandler OnZoomChanged
        {
            add { zoomChanged += value; }
            remove { zoomChanged -= value; }
        }
        public event EventHandler OnImageChanged
        {
            add { imageChanged += value; }
            remove { imageChanged -= value; }
        }
        public event MouseEventHandler OnPictureBoxMouseClick
        {
            add { pictureBoxMouseClick += value; }
            remove { pictureBoxMouseClick -= value; }
        }
        #region Свойства
        public Image Image
        {
            get { return pictureBox.Image; }
            set 
            { 
                pictureBox.Image = value;
                if (imageChanged != null)
                    imageChanged(this, EventArgs.Empty);
            }
        }
        public int Zoom
        {
            get { return zoom; }
            set 
            {
                if (value <= 0)
                {
                    zoom = 2;
                    return;
                }
                else if (value > 6400)
                {
                    zoom = 1000;
                    return;
                }
                else
                {
                    zoom = value;
                }
                if( zoomChanged != null)
                    zoomChanged(this, EventArgs.Empty);
            }
        }
        #endregion
        #region Методы
        private void WorkSheet_SizeChange(object sender, EventArgs e)
        {
            Point newLocation = new Point(this.Width / 2 - pictureBox.Width / 2,
                                            this.Height / 2 - pictureBox.Height / 2);
            Point oldScrollPosition = this.AutoScrollPosition;
            if (newLocation.X < 0)
            {
                newLocation.X = 0;
                this.AutoScrollPosition = new Point(0, this.VerticalScroll.Value);
            }
            if (newLocation.Y < 0)
            {
                newLocation.Y = 0;
                this.AutoScrollPosition = new Point(this.HorizontalScroll.Value, 0);
            }
            pictureBox.Location = newLocation;
            this.AutoScrollPosition = oldScrollPosition;
        }    
        private void ZoomChanged(object sender, EventArgs e) 
        {
            if (pictureBox.Image == null)
                return;
            Size newSize = new Size((int)(pictureBox.Image.Width * zoom * 0.01f),
                                        (int)(pictureBox.Image.Height * zoom * 0.01f));
            if (newSize != Size.Empty)
                pictureBox.Size = newSize;
            WorkSheet_SizeChange(this, EventArgs.Empty);
            this.AutoScrollPosition = new Point(this.HorizontalScroll.Maximum / 2, this.VerticalScroll.Maximum / 2);
        }
        private void ImageChanged(object sender, EventArgs e)
        {
            if (Image == null)
                return;
            pictureBox.Width = Image.Width;
            pictureBox.Height = Image.Height;
            WorkSheet_SizeChange(this, EventArgs.Empty);
        }
        public void PictureBoxRefresh()
        {
            this.pictureBox.Refresh();
        }
        private void WorkSheet_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(e.Location.ToString());
        }
        #endregion
        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBoxMouseClick != null)
                pictureBoxMouseClick(this, e);
        }
    }
}

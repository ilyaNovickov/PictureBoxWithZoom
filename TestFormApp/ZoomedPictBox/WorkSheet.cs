using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ZoomedPictBox
{
    /// <summary>
    /// Класс элемента управления 
    /// который представляет собой маштабируемый PictureBox
    /// </summary>
    public partial class WorkSheet : UserControl
    {
        #region Конструктор
        /// <summary>
        /// Конструктор элемента управления
        /// </summary>
        public WorkSheet()
        {
            //иницилизация компонентов 
            InitializeComponent();
            //Регистрация обработчика-события изменения значения зума
            OnZoomChanged += ZoomChanged;
            //OnImageChanged += ImageChanged;
            //OnPictureBoxMouseClick += WorkSheet_OnPictureBoxMouseClick;
        }
        #endregion
        #region Данные
        int zoom = 100;//Значение маштаба (зума) для изображения в PictureBox
        #endregion
        #region События
        event EventHandler zoomChanged;//Событие изменения зума
        event EventHandler imageChanged;//Событие изменения изображения
        //event MouseEventHandler pictureBoxMouseClick;
        event EventHandler<ZoomedMouseClickeventArgs> pictureBoxMouseClick;//Событие клика мыши по PictureBox
        /// <summary>
        /// Событие изменение маштаба (зума) у элемента управления
        /// </summary>
        [Category("UserEvents")]//Привязка к элементу категории и описания в окне диспетчера свойств
        [Description("Событие изменение маштаба (зума) у элемента управления")]
        public event EventHandler OnZoomChanged
        {
            //методы регистрация и удаления обработчиков событий
            add { zoomChanged += value; }
            remove { zoomChanged -= value; }
        }
        /// <summary>
        /// Событие изменение изображения в элементе PictureBox
        /// </summary>
        [Category("UserEvents")]//Привязка к элементу категории и описания в окне диспетчера свойств
        [Description("Событие изменение изображения в элементе PictureBox")]
        public event EventHandler OnImageChanged
        {
            //методы регистрация и удаления обработчиков событий
            add { imageChanged += value; }
            remove { imageChanged -= value; }
        }
        /// <summary>
        /// Событие нажатия на элемент PictureBox
        /// </summary>
        [Category("UserEvents")]//Привязка к элементу категории и описания в окне диспетчера свойств
        [Description("Событие нажатия на элемент PictureBox")]
        public event EventHandler<ZoomedMouseClickeventArgs> OnPictureBoxMouseClick
        {
            //методы регистрация и удаления обработчиков событий
            add { pictureBoxMouseClick += value; }
            remove { pictureBoxMouseClick -= value; }
        }
        #endregion
        #region Свойства
        /// <summary>
        /// Свойство изображения, хряниемого в PictureBox
        /// </summary>
        [Category("UserProperties")]//Привязка к элементу категории и описания в окне диспетчера свойств
        [Description("Свойство изображения, хряниемого в PictureBox")]
        [DefaultValue(null)]//Значение по умолчанию
        public Image Image
        {
            //Метод возвращающий изображение из PictureBox
            get { return pictureBox.Image; }
            //Метод устанавливающий изображение в PictureBox
            set 
            { 
                //Если значение не равно null
                if (value != null)
                {
                    //Установка размера PictureBox и изображения
                    pictureBox.Size = new Size(value.Width, value.Height);
                    pictureBox.Image = value;
                    //Установка маштаба, чтобы всё изображение было в элементу управления
                    FirstZoomingImage(value);
                }
                //Если есть зарегестрированные обработчики событий
                if (imageChanged != null)
                    //то вызов события изменения картинки
                    imageChanged(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Свойство маштаба (зума) изображения в PictureBox (строго в процентах (%) )
        /// </summary>
        [Category("UserProperties")]//Привязка к элементу категории и описания в окне диспетчера свойств
        [Description("Свойство маштаба (зума) изображения в PictureBox (строго в процентах (%) )")]
        [DefaultValue(100)]//Значение по умолчанию
        public int Zoom
        {
            //Метод вовращающий значение маштаба PictureBox
            get { return zoom; }
            //Метод устанавливающий значение маштаба в PictureBox
            set 
            {
                //Если устанавливаемый маштаб меньше нуля
                if (value <= 0)
                {
                    //то установка значения 2% и выход из метода
                    zoom = 2;
                    return;
                }
                //иначе если устанавливаемый маштаб больше 6400%
                else if (value > 6400)
                {
                    //то установка значения 6400% и выход из метода
                    zoom = 6400;
                    return;
                }//иначе
                else
                {
                    zoom = value;//Установка значения
                }
                //Если есть зарегестрированные обработчики события
                if( zoomChanged != null)
                    //то генерация события изменения маштаба PictureBox
                    zoomChanged(this, EventArgs.Empty);
            }
        }
        #endregion
        #region Методы
        //Обработчик события изменения маштаба элемента WorkSheet
        //Перемещает PictureBox в центр элемента
        private void WorkSheet_SizeChange(object sender, EventArgs e)
        {
            //Иницилизация нового положения picturebox
            Point newLocation = new Point(this.Width / 2 - pictureBox.Width / 2,
                                            this.Height / 2 - pictureBox.Height / 2);
            //Запоминание старой позыции полос прокрутки
            Point oldScrollPosition = this.AutoScrollPosition;
            //Если положение PictureBox по Х меньше 0
            if (newLocation.X < 0)
            {
                //то установка координаты Х для новой позыции на 0
                newLocation.X = 0;
                //Перемещение горизонтальной полосы прокрутки в значение 0
                //this.AutoScrollPosition = new Point(0, this.VerticalScroll.Value);
                this.HorizontalScroll.Value = 0;
            }
            //Если положение PictureBox по Y меньше 0
            if (newLocation.Y < 0)
            {
                //то установка координаты Y для новой позыции на 0
                newLocation.Y = 0;
                //Перемещение вертикальной полосы прокрутки в значение 0
                //this.AutoScrollPosition = new Point(this.HorizontalScroll.Value, 0);
                this.VerticalScroll.Value = 0;
            }
            //Перемещение PictureBox на новые координаты
            pictureBox.Location = newLocation;
            //Установка полос прокрутки элемента управления на старую позыцию
            this.AutoScrollPosition = oldScrollPosition;
        }    
        //Обработчик события изменения маштаба у PictureBox
        private void ZoomChanged(object sender, EventArgs e) 
        {
            //Если у PictureBox нет изображения
            if (pictureBox.Image == null)
                //то выход из метода
                return;
            //Иницилизация нового размера PictureBox
            Size newSize = new Size((int)(pictureBox.Image.Width * zoom * 0.01f),
                                        (int)(pictureBox.Image.Height * zoom * 0.01f));
            //Если новый размер не нулевой (т.е. (0, 0) )
            if (newSize != Size.Empty)
                //то установка нового размера для PictureBox
                pictureBox.Size = newSize;
            //Вызво обработчика события изменения размера элемента
            //так как был изменён размер PictureBox
            WorkSheet_SizeChange(this, EventArgs.Empty);
            //Метод центрирует элемент PictureBox 
            this.AutoScrollPosition = new Point(this.HorizontalScroll.Maximum / 2, this.VerticalScroll.Maximum / 2);
        }
        //Обработчик события изменения изображения в PictureBox
        private void ImageChanged(object sender, EventArgs e)
        {
            //Если изображения нет, то выход из метода
            if (Image == null)
                return;
            //Установка нового размера для PictureBox
            pictureBox.Size = new Size(Image.Width, Image.Height);
            //pictureBox.Width = Image.Width;
            //pictureBox.Height = Image.Height;
            //Вызов обработчика события изменения размера элемента
            WorkSheet_SizeChange(this, EventArgs.Empty);
        }
        //Обработчик события нажатия мыши на элемент PictureBox
        private void pictureBox_MouseClick_1(object sender, MouseEventArgs e)
        {
            //Если есть зарегестрированные обработчики событий
            if (pictureBoxMouseClick != null)
                //Вызов события клика мышей по PictureBox с учётом маштаба
                pictureBoxMouseClick(pictureBox, new ZoomedMouseClickeventArgs(e, this.Zoom));
        }
        //Метод маштабирования изображения при установки нового изображения 
        private void FirstZoomingImage(Image image)
        {
            //Если высота изображения больше высоты элемента управления
            if (image.Height > this.Height)
            {
                //то подсчёт значения такого маштаба
                //при котором изображения влезет в элемент
                float zoomVal = 100f * this.Height / image.Height;
                //и установка значения маштаба
                this.Zoom = (int)(zoomVal);
            }
        }
        #endregion
        #region Открытые методы
        /// <summary>
        /// Метод перерисовки элемента PictureBox
        /// </summary>
        public void PictureBoxRefresh()
        {
            this.pictureBox.Refresh();
        }
        #endregion
    }
}

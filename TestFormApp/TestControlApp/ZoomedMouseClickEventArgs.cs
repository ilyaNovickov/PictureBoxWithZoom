using System.Windows.Forms;
using System.Drawing;

namespace ZoomedPictBox
{
    /// <summary>
    /// Служебный класс для обработки события 
    /// нажатия на PictureBox у комбинированного элемента
    /// </summary>
    public class ZoomedMouseClickeventArgs
    {
        #region Данные
        MouseEventArgs mouseEventArgs;//Экземпляр класса MouseEventArgs
        int zoomVal;//Значение зума (измеряется в %)
        #endregion
        #region Конструктов
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="e"></param>
        /// <param name="zoomVal"></param>
        public ZoomedMouseClickeventArgs(MouseEventArgs e, int zoomVal)
        {
            //Иницилизация данных
            this.mouseEventArgs = e;
            this.zoomVal = zoomVal;
        }
        #endregion
        //public ZoomedMouseClickeventArgs(int zoomVal) 
        //{
        //    this.zoomVal = zoomVal;
        //}
        #region Свойства
        /// <summary>
        /// Свойство возвращающее значение зума
        /// </summary>
        public int Zoom => zoomVal;
        /// <summary>
        /// Свойство возвращающее координаты мыши с учётом маштаба 
        /// </summary>
        public Point Location => new Point(this.X, this.Y);
        //{
            //get 
            //{ 
            //    Point location = new Point((int)(mouseEventArgs.X / (zoomVal * 0.01f)), 
            //        (int)(mouseEventArgs.Y / (zoomVal * 0.01f)));
            //    return location; 
            //}
        //}
        /// <summary>
        /// Свойство возвращающее координаты мыши по оси X с учётом зума 
        /// </summary>
        public int X =>  (int)(mouseEventArgs.X / (zoomVal * 0.01f));
        /// <summary>
        /// Свойство возвращающее координаты мыши по оси Y с учётом зума 
        /// </summary>
        public int Y =>  (int)(mouseEventArgs.Y / (zoomVal * 0.01f));
        /// <summary>
        /// Вовращает количество делений на которое повернулось колесо мыши
        /// </summary>
        public int Delta => mouseEventArgs.Delta; 
        /// <summary>
        /// Возвращает нажатую кнопку мыши
        /// </summary>
        public MouseButtons MouseButtons => mouseEventArgs.Button; 
        #endregion
    }
}

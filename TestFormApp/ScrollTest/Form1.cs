using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrollTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //panel1.Scr
            //Rectangle rectangle = this.ClientRectangle;
            //this.Bounds= this.RectangleToClient(rectangle);
            MessageBox.Show(panel1.VerticalScroll.Value.ToString());
            panel1.AutoScrollPosition = new Point(0, 0);

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestFormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void workSheet1_Load(object sender, EventArgs e)
        {
            workSheet1.Image = null;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            workSheet1.Zoom -= 50;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            workSheet1.Zoom += 50;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var s = new OpenFileDialog();
            if (s.ShowDialog() == DialogResult.OK)
                workSheet1.Image = new Bitmap(s.FileName);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var s = new SaveFileDialog();
            if (s.ShowDialog() == DialogResult.OK)
                workSheet1.Image.Save(s.FileName);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            workSheet1.Zoom = 100;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            var g = Graphics.FromImage(workSheet1.Image);
            g.FillRectangle(new SolidBrush(Color.Red), 0, 0, 100, 100);
            g.Dispose();
            workSheet1.Refresh();
        }

        private void workSheet1_OnPictureBoxMouseClick(object sender, ZoomedPictBox.ZoomedMouseClickeventArgs e)
        {
            MessageBox.Show(e.Location.ToString());
        }
    }
}

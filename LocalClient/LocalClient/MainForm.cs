using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalClient
{
    public partial class MainForm : Form
    {
        private Graphics panelGraphics;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panelGraphics.DrawRectangle(new Pen(Color.Red), new Rectangle(40, 40, 50, 50));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.panelGraphics = this.panel1.CreateGraphics();

            this.panelGraphics.DrawRectangle(new Pen(this.panel1.BackColor), new Rectangle(new Point(), this.panel1.Size));
            this.panelGraphics.DrawRectangle(new Pen(Color.Green), new Rectangle(10, 10, 20, 20));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.panelGraphics.DrawRectangle(new Pen(Color.Green), new Rectangle(10, 10, 20, 20));
        }
    }
}

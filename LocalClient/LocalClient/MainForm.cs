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
        public MainForm()
        {
            InitializeComponent();

            this.PlayBoard.CellPaint += PlayBoard_CellPaint;
        }

        void PlayBoard_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Green), e.CellBounds);
            Console.WriteLine("Breite " + e.CellBounds.Width + " Höhe " + e.CellBounds.Height);
            if (e.Column == 1 && e.Row == 0)
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue), new Rectangle(1,1,50,50));
            }
        }
    }
}

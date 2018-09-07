using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VierGewinntClient
{
    public partial class TextViewer : Form
    {
        public TextViewer(string aTitle, string aText)
        {
            InitializeComponent();
            lblTitle.Text = aTitle;
            this.Text = aTitle;
            textBox.Text = aText;
        }
    }
}

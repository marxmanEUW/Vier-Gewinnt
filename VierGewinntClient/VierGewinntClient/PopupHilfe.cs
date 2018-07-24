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
    public partial class PopupHilfe : Form
    {
        public PopupHilfe(String key)
        {
            InitializeComponent();
            if (key.Equals("about"))
            {
                manualPanel.Visible = false;
                aboutPanel.Visible = true;
            }
            else if (key.Equals("manual"))
            {
                aboutPanel.Visible = false;
                manualPanel.Visible = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VierGewinntServer
{
    public partial class RoomGUI : UserControl
    {
        public RoomGUI()
        {
            InitializeComponent();
        }

        public RoomGUI(string aRoomName, string aClientOne, string aClientTwo)
        {
            InitializeComponent();

            lblRoomName.Text = aRoomName;
            lblClientOne.Text = aClientOne;
            lblClientTwo.Text = aClientTwo;
        }

        private void RoomGUI_Resize(object sender, EventArgs e)
        {
            lblClientOne.Width = (int)((double)this.Width / 2);            
        }
    }
}

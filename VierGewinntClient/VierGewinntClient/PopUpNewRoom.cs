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
    public partial class popupNewRoom : Form
    {

        public String roomName;
        public popupNewRoom()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            roomName = inputRoomName.Text;
        }
    }
}

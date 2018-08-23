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

        public string roomName;
        public popupNewRoom()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (inputRoomName.Text != String.Empty)
            {
                roomName = inputRoomName.Text;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}

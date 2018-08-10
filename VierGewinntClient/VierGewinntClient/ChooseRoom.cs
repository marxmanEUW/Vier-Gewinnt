using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VierGewinntClient.DataFormats;

namespace VierGewinntClient
{
    public partial class ChooseRoom : Form
    {
        public RoomEssentials chosenRoom { get; set; }

        public ChooseRoom(DataSendRooms sendRooms) //Liste von Räumen mit übergeben
        {
            InitializeComponent();
            listBoxRooms.Items.Clear();
            listBoxRooms.DisplayMember = "RoomName";
            listBoxRooms.ValueMember = "RoomID";
            foreach (RoomEssentials room in sendRooms.Rooms)
            {
                listBoxRooms.Items.Add(room);
                
            }

        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            if (listBoxRooms.SelectedItems.Count > 0)
            {
               chosenRoom = (RoomEssentials)listBoxRooms.SelectedItem;
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}

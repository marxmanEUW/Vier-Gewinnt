using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            TcpClient Client = new TcpClient();
            Client.Connect(IPAddress.Parse(textIP.Text), int.Parse(textPort.Text));
            byte[] dataToSend = new byte[4096];
            dataToSend = Encoding.ASCII.GetBytes("TEST");
            Client.GetStream().BeginWrite(dataToSend, 0, dataToSend.Length, null, null);
            MessageBox.Show("Connected", "Info");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleServer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            TcpListener Server = new TcpListener(GetLocalIPAddress(), int.Parse(textPort.Text));
            Server.Start();
            MessageBox.Show("Server started.", "Info");
        }

        public static IPAddress GetLocalIPAddress()
        {
            var lHost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var IP in lHost.AddressList)
            {
                if (IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    return IPAddress.Parse(IP.ToString());
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            textIP.Text = GetLocalIPAddress().ToString();
        }
    }
}

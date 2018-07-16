using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

        private TcpListener Server;

        private void btnStart_Click(object sender, EventArgs e)
        {
            TcpThread();

            MessageBox.Show("Server started.", "Info");
        }

        private async void TcpThread()
        {
            TcpClient lTcpClient;

            Server = new TcpListener(GetLocalIPAddress(), int.Parse(textPort.Text));
            Server.Start();
            while (true)
            {
                Thread.Sleep(1);

                lTcpClient = await Server.AcceptTcpClientAsync();
                using (NetworkStream lNetworkStream = lTcpClient.GetStream())
                {
                    byte[] buffer = new byte[4096];
                    int byteCount = await lNetworkStream.ReadAsync(buffer, 0, buffer.Length);
                    string lMessage = Encoding.UTF8.GetString(buffer, 0, byteCount);

                    MessageBox.Show(lMessage,"Message");

                    await lNetworkStream.WriteAsync(Encoding.UTF8.GetBytes("BESTÄTIGT"), 0, Encoding.UTF8.GetBytes("BESTÄTIGT").Length);
                }
            }
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

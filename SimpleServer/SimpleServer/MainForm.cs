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
            StartStopServer();
        }

        private void StartStopServer()
        {
            Thread ConnThread = new Thread(ConnectionThread);

            if (Server == null)
            {
                Server = new TcpListener(GetLocalIPAddress(), int.Parse(textPort.Text));

                Server.Start();
                ConnThread.Start();
                textIP.Enabled = false;
                textPort.Enabled = false;
                btnStart.Text = "Stop";
                MessageBox.Show("Server started.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Server.Stop();
                Server = null;
                textIP.Enabled = true;
                textPort.Enabled = true;
                btnStart.Text = "Start";
                MessageBox.Show("Server stopped.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private async void ConnectionThread()
        {
            TcpClient lTcpClient;

            while (Server != null)
            {
                try
                {
                    lTcpClient = await Server.AcceptTcpClientAsync();
                    using (NetworkStream lNetworkStream = lTcpClient.GetStream())
                    {
                        byte[] buffer = new byte[4096];
                        int byteCount = await lNetworkStream.ReadAsync(buffer, 0, buffer.Length);
                        string lMessage = Encoding.UTF8.GetString(buffer, 0, byteCount);

                        await lNetworkStream.WriteAsync(Encoding.UTF8.GetBytes("BESTÄTIGT"), 0, Encoding.UTF8.GetBytes("BESTÄTIGT").Length);

                        MessageBox.Show(lMessage, "Message");
                    }

                    Thread.Sleep(1);
                }
                catch
                {
                    return;
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

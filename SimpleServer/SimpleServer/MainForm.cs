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
            //while (true)
            {
                lTcpClient = await Server.AcceptTcpClientAsync();
                using (NetworkStream lNetworkStream = lTcpClient.GetStream())
                {
                    var buffer = new byte[4096];
                    var byteCount = await lNetworkStream.ReadAsync(buffer, 0, buffer.Length);
                    var request = Encoding.ASCII.GetString(buffer, 0, byteCount);

                    MessageBox.Show(request);

                    await lNetworkStream.WriteAsync(Encoding.ASCII.GetBytes("BESTÄTIGT"), 0, Encoding.ASCII.GetBytes("BESTÄTIGT").Length);
                    
                    //byte[] buffer = new byte[4096];

                    //StreamReader streamReader = new StreamReader(lTcpClient.GetStream());

                    //string lCommandString = streamReader.ReadToEnd();

                    //Start Action################################################################################

                    //int lMessageInt = int.Parse(lCommandString);
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

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
            ConnectAsTcpClient();
        }

        private async void ConnectAsTcpClient()
        {
            try
            {
                using (TcpClient Client = new TcpClient())
                {
                    await Client.ConnectAsync(IPAddress.Parse(textIP.Text), int.Parse(textPort.Text));
                    byte[] dataToSend = new byte[4096];
                    dataToSend = Encoding.UTF8.GetBytes(textData.Text != String.Empty ? textData.Text : "DATA");
                    Client.GetStream().BeginWrite(dataToSend, 0, dataToSend.Length, null, null);
                    using (NetworkStream lResponseStream = Client.GetStream())
                    {
                        var buffer = new byte[4096];
                        var byteCount = await lResponseStream.ReadAsync(buffer, 0, buffer.Length);
                        var response = Encoding.UTF8.GetString(buffer, 0, byteCount);
                        MessageBox.Show(response, "Response");
                    }
                }
            }
            catch(SocketException e)
            {
                MessageBox.Show("Keine Verbindung möglich", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}

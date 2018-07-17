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
using System.Threading;

namespace SimpleClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private TcpClient Client;

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectDisconnectAsTcpClient();
        }

        private void ConnectDisconnectAsTcpClient()
        {
            try
            {
                if (Client == null)
                {
                    Client = new TcpClient();
                    Client.Connect(IPAddress.Parse(textIP.Text), int.Parse(textPort.Text));

                    if (Client.Client.Connected)
                    {
                        Thread lConnCheckThread = new Thread(() => ClientStillIsConnected(Client));
                        lConnCheckThread.Start();

                        MessageBox.Show("Client is now connected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textIP.Enabled = false;
                        textPort.Enabled = false;
                        btnConnect.Text = "Disconnect";
                        btnConnect.BackColor = Color.LightCoral;
                        btnSendData.Enabled = true;
                    }
                }
                else
                {
                    Client.Close();
                    Client = null;

                    // Running on the UI thread
                    this.Invoke((MethodInvoker)delegate {                        
                        textIP.Enabled = true;
                        textPort.Enabled = true;
                        btnConnect.Text = "Connect";
                        btnConnect.BackColor = Color.LightGreen;
                        btnSendData.Enabled = false;
                    });                    
                }
            }
            catch (SocketException e)
            {
                MessageBox.Show("Keine Verbindung möglich", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            SendData(textData.Text);
        }

        private void ClientStillIsConnected(TcpClient lClient)
        {
            try
            {
                bool stillConnected = true;
                while (stillConnected)
                {
                    if((lClient.Client.Poll(1, SelectMode.SelectRead) && lClient.Available == 0))
                    {
                        stillConnected = false;
                    }
                    Thread.Sleep(1);
                }
                ConnectDisconnectAsTcpClient();
                MessageBox.Show("Server has disconnected all clients.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {

            }
        }

        private async void SendData(String aData)
        {
            try
            {
                if (this.Client != null)
                {
                    byte[] dataToSend = new byte[4096];
                    dataToSend = Encoding.UTF8.GetBytes(textData.Text != String.Empty ? textData.Text : "DATA");
                    Client.GetStream().BeginWrite(dataToSend, 0, dataToSend.Length, null, null);

                    var buffer = new byte[4096];
                    var byteCount = await Client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                    var response = Encoding.UTF8.GetString(buffer, 0, byteCount);
                    MessageBox.Show(response, "Response");
                }
            }
            catch (SocketException e)
            {
                MessageBox.Show("Keine Verbindung möglich", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}

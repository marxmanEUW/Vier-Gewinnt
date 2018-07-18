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
        private List<TcpClient> fClients = new List<TcpClient>();

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartStopServer();
        }

        private void StartStopServer()
        {
            Thread ConnThread = new Thread(ConnectionThread);

            if (Server == null) //Start Server
            {
                Server = new TcpListener(GetLocalIPAddress(), int.Parse(textPort.Text));

                Server.Start();
                ConnThread.Start();
                textIP.Enabled = false;
                textPort.Enabled = false;
                btnStart.Text = "Stop";
                textLog.Text = String.Empty;
                AddToLog(String.Format(">>> SERVER STARTED ON {0} >>>",Server.Server.LocalEndPoint));//MessageBox.Show("Server started.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else //Stop Server
            {
                DisconnectClients();
                Server.Stop();
                Server = null;
                textIP.Enabled = true;
                textPort.Enabled = true;
                btnStart.Text = "Start";
                AddToLog("<<< SERVER STOPPED <<<");//MessageBox.Show("Server stopped.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void DisconnectClients()
        {
            foreach(TcpClient lClient in fClients)
            {
                lClient.Client.Disconnect(true);
            }
        }

        private void AddToLog(string aText)
        {            
            this.Invoke((MethodInvoker)delegate {   
                if(textLog.Text == String.Empty)
                {
                    textLog.Text += String.Format("[{0}] {1}",DateTime.Now.ToLongTimeString(),aText);
                }
                else
                {
                    textLog.Text += Environment.NewLine + String.Format("[{0}] {1}", DateTime.Now.ToLongTimeString(), aText);
                }                
            });
        }

        private async void ConnectionThread()
        {
            TcpClient lTcpClient;

            while (Server != null)
            {
                try
                {
                    lTcpClient = await Server.AcceptTcpClientAsync();

                    TryAddNewTcpClient(lTcpClient);

                    Thread.Sleep(1);
                }
                catch
                {
                    
                }
            }
        }

        private bool TryAddNewTcpClient(TcpClient aClient)
        {
            bool canAdd = true;
            foreach (TcpClient lClient in fClients)
            {
                if (aClient.Client.RemoteEndPoint == lClient.Client.RemoteEndPoint)
                {
                    canAdd = false;
                }
            }
            if (canAdd)
            {
                fClients.Add(aClient);
                Thread lDataCheckThread = new Thread(() => CheckClientForNewData(aClient));
                lDataCheckThread.Start();
                AddToLog(String.Format("+++ CLIENT CONNECTED: {0}", aClient.Client.RemoteEndPoint));
                return true;
            }
            return false;
        }

        private async void CheckClientForNewData(TcpClient aClient)
        {
            NetworkStream lNetworkStream = aClient.GetStream();
            byte[] buffer = new byte[4096];

            try
            {
                while (aClient.Client.Connected)
                {
                    int byteCount = await lNetworkStream.ReadAsync(buffer, 0, buffer.Length);                    
                    string lMessage = Encoding.UTF8.GetString(buffer, 0, byteCount);

                    if (lMessage != String.Empty)
                    {
                        await lNetworkStream.WriteAsync(Encoding.UTF8.GetBytes("BESTÄTIGT"), 0, Encoding.UTF8.GetBytes("BESTÄTIGT").Length);
                        AddToLog(String.Format("Message from client {0} >>> {1}",aClient.Client.RemoteEndPoint.ToString(), lMessage));//MessageBox.Show(lMessage, "Message");
                    }

                    Thread.Sleep(1);
                }
            }
            catch
            {

            }

            lNetworkStream.Close();
            aClient.Close();
            fClients.Remove(aClient);
        }

        private static IPAddress GetLocalIPAddress()
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

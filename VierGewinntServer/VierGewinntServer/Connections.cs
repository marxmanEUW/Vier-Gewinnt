using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VierGewinntServer
{
    class Connections
    {

        #region Constructor

        static Connections()
        {
            ServerIP = GetLocalIPAddress();
            BufferSize = new byte[4096];
            CurrentServerStatus = ServerStatus.Offline;
        }

        #endregion

        #region Message Constants

        public const string MESSAGE_CONFIRMED = "MESSAGE_CONFIRMED";

        #endregion

        #region Private Variables

        private static TcpListener TcpGameServer;
        private static List<TcpServerClient> ListConnectedClients = new List<TcpServerClient>();
        private static byte[] BufferSize;
        private static Encoding EncodingInstance = Encoding.UTF8;

        #endregion

        #region Public Variables

        //Server Properties
        public static IPAddress ServerIP;
        public const int SERVER_PORT = 53335;
        public static ServerStatus CurrentServerStatus;

        public enum ServerStatus { Online, Offline };

        #endregion

        #region Public Methods

        /// <summary>
        /// Start or stops the server (TcpListener) depending on the current state of the server (online/offline).
        /// Returns true when starting or stopping was successful
        /// </summary>
        /// <returns>StartStopWasSuccessful</returns>
        public static bool StartStopServer()
        {
            Thread ConnectionsThread = new Thread(CheckForNewConnections);

            try
            {
                if (TcpGameServer == null) //Start Server
                {
                    TcpGameServer = new TcpListener(ServerIP, SERVER_PORT);
                    TcpGameServer.Start();
                    ConnectionsThread.Start();
                    CurrentServerStatus = ServerStatus.Online;
                }
                else //Stop Server
                {
                    DisconnectClients();
                    TcpGameServer.Stop();
                    TcpGameServer = null;
                    CurrentServerStatus = ServerStatus.Offline;
                }
                return true;
            }
            catch
            {
                if (TcpGameServer != null)
                {
                    TcpGameServer.Stop();
                    TcpGameServer = null;
                    CurrentServerStatus = ServerStatus.Offline;
                }
                return false;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Disconnects all clients that a currently connected / are currently in the list of connected clients.
        /// This should trigger a reaction in the client program!
        /// </summary>
        private static void DisconnectClients()
        {
            foreach (TcpServerClient Client in ListConnectedClients)
            {
                Client.PlayerClient.Client.Disconnect(true);
            }
        }

        /// <summary>
        /// Continuously checks if a new client wants to connect to the server.
        /// Runs on a separate thread.
        /// </summary>
        private static async void CheckForNewConnections()
        {
            TcpClient Client;
            TcpServerClient ServerClient;

            while (TcpGameServer != null && CurrentServerStatus == ServerStatus.Online)
            {
                try
                {
                    //Client connect with server
                    Client = await TcpGameServer.AcceptTcpClientAsync();

                    ServerClient = new TcpServerClient();
                    ServerClient.PlayerClient = Client;

                    //Client should immediately send message with client name / player name, gets saved as TcpServerClient
                    NetworkStream lNetworkStream = Client.GetStream();
                    int byteCount = await lNetworkStream.ReadAsync(BufferSize, 0, BufferSize.Length);
                    string lPlayerName = Encoding.UTF8.GetString(BufferSize, 0, byteCount);

                    if (lPlayerName != String.Empty)
                    {
                        ServerClient.PlayerName = lPlayerName;
                    }
                    else
                    {
                        ServerClient.PlayerName = "Unknown Player";
                    }

                    //Confirmation Message ?
                    byte[] ResponseBytes = EncodingInstance.GetBytes(MESSAGE_CONFIRMED);
                    await lNetworkStream.WriteAsync(ResponseBytes, 0, ResponseBytes.Length);

                    //Add ServerClient to the list of clients
                    TryAddNewTcpClient(ServerClient);
                    Thread.Sleep(1);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Trys to add ServerClient to list of clients. Only works successfully when client list does not contain client yet.
        /// </summary>
        /// <param name="aClient">Client to add to list.</param>
        /// <returns>AddingSuccessful</returns>
        private static bool TryAddNewTcpClient(TcpServerClient aClient)
        {
            bool canAdd = true;
            foreach (TcpServerClient Client in ListConnectedClients)
            {
                if (aClient.PlayerClient.Client.RemoteEndPoint == Client.PlayerClient.Client.RemoteEndPoint)
                {
                    canAdd = false;
                }
            }

            if (canAdd)
            {
                ListConnectedClients.Add(aClient);
                Thread lDataCheckThread = new Thread(() => CheckClientForNewData(aClient));
                lDataCheckThread.Start();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks continuously if this client is sending new data.
        /// Runs on a separate thread.
        /// </summary>
        /// <param name="aClient">Client to check for new data.</param>
        private static async void CheckClientForNewData(TcpServerClient aClient)
        {
            NetworkStream lNetworkStream = aClient.PlayerClient.GetStream();

            try
            {
                while (aClient.PlayerClient.Client.Connected)
                {
                    int byteCount = await lNetworkStream.ReadAsync(BufferSize, 0, BufferSize.Length);
                    string ReceivedData = Encoding.UTF8.GetString(BufferSize, 0, byteCount);

                    if (ReceivedData != String.Empty)
                    {
                        //Confirmation Message ?
                        byte[] ResponseBytes = EncodingInstance.GetBytes(MESSAGE_CONFIRMED);
                        await lNetworkStream.WriteAsync(ResponseBytes, 0, ResponseBytes.Length);
                    }

                    Thread.Sleep(1);
                }
            }
            catch
            {

            }

            lNetworkStream.Close();
            aClient.PlayerClient.Close();
            ListConnectedClients.Remove(aClient);
        }

        /// <summary>
        /// Gets the IP address of the local device
        /// </summary>
        /// <returns>Local IP</returns>
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

        #endregion
    }
}

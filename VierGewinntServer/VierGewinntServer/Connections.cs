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
        static Connections()
        {
            ServerIP = GetLocalIPAddress();
            BufferSize = new byte[4096];
        }

        private static TcpListener TcpGameServer;
        private static List<TcpServerClient> ListConnectedClients = new List<TcpServerClient>();
        private static byte[] BufferSize;

        public static IPAddress ServerIP;
        public const int SERVER_PORT = 53335;

        public static void StartStopServer()
        {
            Thread ConnectionsThread = new Thread(CheckForNewConnections);

            try
            {
                if (TcpGameServer == null) //Start Server
                {
                    TcpGameServer = new TcpListener(ServerIP, SERVER_PORT);

                    TcpGameServer.Start();
                    ConnectionsThread.Start();
                }
                else //Stop Server
                {
                    DisconnectClients();
                    TcpGameServer.Stop();
                    TcpGameServer = null;
                }
            }
            catch
            {
                if(TcpGameServer != null)
                {
                    TcpGameServer.Stop();
                    TcpGameServer = null;
                }
            }
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
    }
}

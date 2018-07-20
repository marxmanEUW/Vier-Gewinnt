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

        private static TcpListener Server;
        private static List<TcpServerClient> ListConnectedClients = new List<TcpServerClient>();
        private static byte[] BufferSize;

        public static IPAddress ServerIP;

        public static void StartStopServer()
        {
            Thread ConnThread = new Thread(ConnectionThread);

            if (Server == null) //Start Server
            {
                Server = new TcpListener(GetLocalIPAddress(), int.Parse(textPort.Text));

                Server.Start();
                ConnThread.Start();
            }
            else //Stop Server
            {
                DisconnectClients();
                Server.Stop();
                Server = null;
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

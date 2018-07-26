using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LocalClient;
using Newtonsoft.Json;

namespace VierGewinntClient
{
    class Connections
    {
        private const string PREFIX_NEWRM = "NEWRM";

        private static TcpClient GameClient;

        /// <summary>
        /// Connects to the server.
        /// </summary>
        /// <param name="aIP">Server IP</param>
        /// <param name="aPort">Server Port</param>
        /// <param name="aClientName">Player Name</param>
        /// <returns>Connection successful</returns>
        public static bool ConnectToServer(IPAddress aIP, int aPort, string aClientName)
        {
            try
            {
                if (GameClient == null)
                {
                    GameClient = new TcpClient();
                    GameClient.Connect(aIP, aPort);

                    if (GameClient.Client.Connected)
                    {
                        SendData(aClientName);
                    }
                    return true;
                }
                return false;
            }
            catch (SocketException e)
            {
                return false;
                //MessageBox.Show("Keine Verbindung möglich", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception e)
            {
                return false;
                //MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Sends a request to the server to create a new room.
        /// </summary>
        /// <param name="aRoomName">The name of the new room.</param>
        /// <returns>Romm creation successful</returns>
        public static bool RequestCreateNewRoom(string aRoomName)
        {
            try
            {
                if (GameClient != null)
                {
                    DataRoom NewRoom = new DataRoom();
                    NewRoom.Name = aRoomName;

                    string DataJSON = JsonConvert.SerializeObject(NewRoom);

                    SendData(String.Format("{0}{1}", PREFIX_NEWRM, DataJSON));

                    return true;
                }
                return false;
            }
            catch (SocketException e)
            {
                return false;
                //MessageBox.Show("Keine Verbindung möglich", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception e)
            {
                return false;
                //MessageBox.Show(e.Message);
            }
        }


        private static async void SendData(String aData)
        {
            try
            {
                if (GameClient != null)
                {
                    byte[] dataToSend = new byte[4096];
                    dataToSend = Encoding.UTF8.GetBytes(aData != String.Empty ? aData : "");
                    GameClient.GetStream().BeginWrite(dataToSend, 0, dataToSend.Length, null, null);

                    var buffer = new byte[4096];
                    var byteCount = await GameClient.GetStream().ReadAsync(buffer, 0, buffer.Length);
                    var response = Encoding.UTF8.GetString(buffer, 0, byteCount);
                    //MessageBox.Show(response, "Response");
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

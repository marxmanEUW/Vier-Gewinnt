using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VierGewinntClient.DataFormats;
using Newtonsoft.Json;
using System.Threading;

namespace VierGewinntClient
{
    class Connections
    {
        static Connections()
        {
            _BufferSize = new byte[4096];
        }

        public const string MESSAGE_CONFIRMED = "MESSAGE_CONFIRMED";

        private const string PREFIX_NEWRM = "NEWRM"; //Create new room
        private const string PREFIX_GAMED = "GAMED"; //Game data
        private const string PREFIX_SNDRM = "SNDRM"; //Send available rooms
        private const string PREFIX_CONRM = "CONRM"; //Connect to room, using its ID
        private const string PREFIX_START = "START"; //Tells both members in a room that the game is starting
        private const string PREFIX_YOURT = "YOURT"; //Tells player that its their turn
        private const string PREFIX_TDATA = "TDATA"; //Tells server what column player pressed
        private const string PREFIX_GMEST = "GMEST"; //Tells the client what the game state is

        private const string GS_VALIDMOVE = "VALID_MOVE"; //Gamestate for 'move was valid', to send to player
        private const string GS_INVALIDMOVE = "INVALID_MOVE";
        private const string GS_YOUWON = "YOU_WON";
        private const string GS_YOULOST = "YOU_LOST";
        private const string GS_DRAW = "DRAW";




        private static TcpClient GameClient;
        private static byte[] _BufferSize;
        private static Encoding _EncodingInstance = Encoding.UTF8;

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
                if (GameClient == null)
                {
                    DataRoom NewRoom = new DataRoom();
                    NewRoom.Name = aRoomName;
                    
                    SendData(String.Format("{0}{1}", PREFIX_NEWRM, DataProcessor.SerializeNewRoomData(NewRoom)));

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

        public static DataSendRooms RequestAvailableRooms()
        {
            SendData(String.Format("{0}", PREFIX_SNDRM));

            NetworkStream lNetworkStream = GameClient.GetStream();
            string JSON_Rooms = String.Empty;
            while (JSON_Rooms == String.Empty)
            {
                int byteCount = lNetworkStream.Read(_BufferSize, 0, _BufferSize.Length);
                JSON_Rooms = _EncodingInstance.GetString(_BufferSize, 0, byteCount);
                Thread.Sleep(2);
            }
            return DataProcessor.DeserializeSendRoomsData(JSON_Rooms);
        }


        private static /*async*/ void SendData(String aData)
        {
            try
            {
                if (GameClient != null)
                {
                    byte[] dataToSend = new byte[4096];
                    dataToSend = _EncodingInstance.GetBytes(aData);
                    GameClient.GetStream().BeginWrite(dataToSend, 0, dataToSend.Length, null, null);
                    //Thread.Sleep(1000);
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

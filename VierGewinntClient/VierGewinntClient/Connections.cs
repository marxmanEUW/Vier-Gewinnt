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
            Status = GameStatus.Waiting;
        }

        public const string MESSAGE_CONFIRMED = "MESSAGE_CONFIRMED";
        public const string MESSAGE_ERROR = "MESSAGE_ERROR";

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


        public enum GameStatus { Waiting, Playing, YouWon, YouLost, Draw };
        public enum TurnStatus { YourTurn, EnemyTurn };
        public enum ValidStatus { Valid, Invalid };

        /// <summary>
        /// The status of the current game, if it's playing
        /// </summary>
        public static GameStatus Status;

        /// <summary>
        /// Stores if it's your turn or the enemys turn.
        /// </summary>
        public static TurnStatus Turn;

        /// <summary>
        /// Stores if your last move was valid or invalid.
        /// </summary>
        public static ValidStatus Valid;


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

        public static bool SendColumnToServer(int clickedColumn)
        {

            DataPlayerTurn playerTurn = new DataPlayerTurn();
            playerTurn.ClientID = String.Empty;
            playerTurn.Column = clickedColumn;
            SendData(String.Format("{0}{1}", PREFIX_TDATA, DataProcessor.SerializePlayerTurnData(playerTurn)));
            return true;
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

                    SendData(String.Format("{0}{1}", PREFIX_NEWRM, DataProcessor.SerializeNewRoomData(NewRoom)));

                    Thread ThreadWaitForGame = new Thread(() => WaitForGameToStart());
                    ThreadWaitForGame.Start();

                    return true;
                }
                return false;
            }
            catch (SocketException e)
            {
                return false;
                MessageBox.Show("Keine Verbindung möglich", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception e)
            {
                return false;
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Request a list of available rooms from the server.
        /// </summary>
        /// <returns>List of available rooms</returns>
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

        /// <summary>
        /// Requests to connect to a room as the second player.
        /// </summary>
        public static bool RequestConnectAsSecondPlayer(string aRoomID)
        {
            SendData(String.Format("{0}{1}", PREFIX_CONRM, aRoomID));

            NetworkStream lNetworkStream = GameClient.GetStream();
            string JSON_string = String.Empty;
            while (JSON_string == String.Empty)
            {
                int byteCount = lNetworkStream.Read(_BufferSize, 0, _BufferSize.Length);
                JSON_string = _EncodingInstance.GetString(_BufferSize, 0, byteCount);
                Thread.Sleep(2);
            }

            if (JSON_string == MESSAGE_CONFIRMED)
            {
                Thread ThreadWaitForGame = new Thread(() => WaitForGameToStart());
                ThreadWaitForGame.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Runs a a different thread. Waits for the server to start the game.
        /// </summary>
        private static void WaitForGameToStart()
        {
            while (Status == GameStatus.Waiting)
            {
                NetworkStream lNetworkStream = GameClient.GetStream();
                string JSON_string = String.Empty;
                while (JSON_string == String.Empty)
                {
                    int byteCount = lNetworkStream.Read(_BufferSize, 0, _BufferSize.Length);
                    JSON_string = _EncodingInstance.GetString(_BufferSize, 0, byteCount);
                    Thread.Sleep(1);
                }

                if (JSON_string == PREFIX_START)
                {
                    Status = GameStatus.Playing;
                    Thread ThreadPlayGame = new Thread(() => UpdateTurnAndGameStatus());
                    ThreadPlayGame.Start();
                }
            }
        }

        /// <summary>
        /// While the game is playing out, continuously check for data being sent from the server to update the variables
        /// Valid, Turn and Status.
        /// </summary>
        private static void UpdateTurnAndGameStatus()
        {
            while (Status == GameStatus.Playing)
            {
                NetworkStream lNetworkStream = GameClient.GetStream();
                string JSON_string = String.Empty;
                while (JSON_string == String.Empty)
                {
                    int byteCount = lNetworkStream.Read(_BufferSize, 0, _BufferSize.Length);
                    JSON_string = _EncodingInstance.GetString(_BufferSize, 0, byteCount);
                    Thread.Sleep(5);
                }

                switch (JSON_string)
                {
                    case PREFIX_YOURT:
                        Turn = TurnStatus.YourTurn;
                        break;
                    case GS_VALIDMOVE:
                        Valid = ValidStatus.Valid;
                        Turn = TurnStatus.EnemyTurn;
                        break;
                    case GS_INVALIDMOVE:
                        Valid = ValidStatus.Invalid;
                        Turn = TurnStatus.YourTurn;
                        break;
                    case GS_YOUWON:
                        Valid = ValidStatus.Valid;
                        Turn = TurnStatus.YourTurn;
                        Status = GameStatus.YouWon;
                        break;
                    case GS_YOULOST:
                        Valid = ValidStatus.Valid;
                        Turn = TurnStatus.YourTurn;
                        Status = GameStatus.YouLost;
                        break;
                    case GS_DRAW:
                        Valid = ValidStatus.Valid;
                        Turn = TurnStatus.YourTurn;
                        Status = GameStatus.Draw;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Send data string to the server.
        /// </summary>
        /// <param name="aData">Data string</param>
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

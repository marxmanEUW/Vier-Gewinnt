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
            Valid = ValidStatus.NoState;
            GameState = new DataGameState();
            Turn = TurnStatus.NoTurn;
            PlayerOne = "";
            PlayerTwo = "";
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
        private const string PREFIX_ENDGM = "ENDGM"; //Tells the Server that a player leaves the game before it is finished

        private const string GS_VALIDMOVE = "VALID_MOVE"; //Gamestate for 'move was valid', to send to player
        private const string GS_INVALIDMOVE = "INVALID_MOVE";
        private const string GS_VALIDENEMYMOVE = "VALID_ENEMY_MOVE";
        private const string GS_VALIDNOSTATE = "VALID_NOSTATE";
        private const string GS_YOUWON = "YOU_WON";
        private const string GS_YOULOST = "YOU_LOST";
        private const string GS_DRAW = "DRAW";


        public enum GameStatus { Waiting, Playing, YouWon, YouLost, Draw };
        public enum TurnStatus { YourTurn, EnemyTurn, NoTurn };
        public enum ValidStatus { Valid, Invalid, NoState };

        /// <summary>
        /// The status of the current game, if it's playing
        /// </summary>
        public static GameStatus Status;

        public static string PlayerOne;
        public static string PlayerTwo;


        /// <summary>
        /// Stores if it's your turn or the enemys turn.
        /// </summary>
        public static TurnStatus Turn;

        /// <summary>
        /// Stores if your last move was valid or invalid.
        /// </summary>
        public static ValidStatus Valid;

        public static DataGameState GameState;


        private static TcpClient GameClient;
        private static byte[] _BufferSize;
        private static Encoding _EncodingInstance = Encoding.UTF8;

        private static string WaitForResponse()
        {
            NetworkStream lNetworkStream = GameClient.GetStream();
            string JSON_string = String.Empty;
            while (JSON_string == String.Empty)
            {
                int byteCount = lNetworkStream.Read(_BufferSize, 0, _BufferSize.Length);
                JSON_string = _EncodingInstance.GetString(_BufferSize, 0, byteCount);
                Thread.Sleep(2);
            }

            return JSON_string;
        }

        private static string FormatJSON<T>(string aJSON)
        {
            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<T>(aJSON), Formatting.Indented);
        }

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

                    //RSA Public Key empfangen
                    string publicKeyString = WaitForResponse();
                    Cryptography.PublicKey = Cryptography.GetKeyFromString(publicKeyString);

                    //PRÄSI

                    TextViewer tv1 = new TextViewer("Public Key", FormatJSON<System.Security.Cryptography.RSAParameters>(publicKeyString));
                    //tv1.Show();

                    //Beliebigen Symm. Schlüssel per RSA verschlüsselt an Server senden
                    string lChiffreRSA = Cryptography.RsaEncrypt(Cryptography.SymmetricKey, Cryptography.PublicKey);
                    string tv2_out = "Symm. Key:".PadRight(15) + Cryptography.SymmetricKey + Environment.NewLine + "RSA Chiffre:".PadRight(15) + lChiffreRSA;
                    TextViewer tv2 = new TextViewer("RSA", tv2_out);
                    //tv2.Show();
                    SendData(lChiffreRSA);

                    //Server sendet bereit für symm. verschlüsselte Nachricht
                    WaitForResponse();

                    //Sende Verschlüsselten Spielernamen
                    string lChiffreAES_ClientName = Cryptography.AesEncrypt(aClientName, Cryptography.SymmetricKey);

                    string tv3_out = "Clientname:".PadRight(15) + aClientName + Environment.NewLine + "AES Chiffre:".PadRight(15) + lChiffreAES_ClientName;
                    TextViewer tv3 = new TextViewer("AES", tv3_out);
                    //tv3.Show();

                    if (GameClient.Client.Connected)
                    {
                        SendData(lChiffreAES_ClientName);
                    }
                    return true;
                }
                return false;
            }
            catch (SocketException e)
            {
                Console.Write(e.Message);
                return false;
                //MessageBox.Show("Keine Verbindung möglich", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
                //MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Sendet Information über ausgewählte Spalte an Server
        /// </summary>
        /// <param name="clickedColumn"></param>
        /// <returns></returns>
        public static bool SendColumnToServer(int clickedColumn)
        {

#pragma warning disable IDE0017 // Simplify object initialization
            DataPlayerTurn playerTurn = new DataPlayerTurn();
#pragma warning restore IDE0017 // Simplify object initialization
            playerTurn.ClientID = String.Empty;
            playerTurn.Column = clickedColumn;
            SendData(String.Format("{0}{1}", PREFIX_TDATA, DataProcessor.SerializePlayerTurnData(playerTurn)));
            return true;
        }

        /// <summary>
        /// Client sendet Info an Server, dass das Spiel abgebrochen wird
        /// </summary>
        /// <returns></returns>
        public static bool SendEndGameToServer()
        {
            SendData(String.Format("{0}", PREFIX_ENDGM));
            Connections.Status = GameStatus.Waiting;
            return false;
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
#pragma warning disable IDE0017 // Simplify object initialization
                    DataRoom NewRoom = new DataRoom();
#pragma warning restore IDE0017 // Simplify object initialization
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
                Console.Write(e.Message);
                return false;
                //MessageBox.Show("Keine Verbindung möglich", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
                //MessageBox.Show(e.Message);
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

                if (JSON_string.StartsWith(PREFIX_START))
                {
                    Status = GameStatus.Playing;

                    if (PlayerTwo.Equals(""))
                    {
                        PlayerTwo = JSON_string.Substring(PREFIX_START.Length);
                    }

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

                if (JSON_string.StartsWith(PREFIX_YOURT))
                {
                    Turn = TurnStatus.YourTurn;
                }
                else
                {

                    GameState = DataProcessor.DeserializeGameStateData(JSON_string.Substring(5));

                    switch (GameState.GameState)
                    {
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
                            Turn = TurnStatus.NoTurn;
                            Status = GameStatus.YouWon;

                            break;
                        case GS_YOULOST:
                            Valid = ValidStatus.Valid;
                            Turn = TurnStatus.NoTurn;
                            Status = GameStatus.YouLost;

                            break;
                        case GS_DRAW:
                            Valid = ValidStatus.Valid;
                            Turn = TurnStatus.NoTurn;
                            Status = GameStatus.Draw;
                            break;
                        case GS_VALIDENEMYMOVE:
                            //Der Gegner hat einen gültigen Zug gemacht. Die INfo, dass ich dran bin, kommt in separater Nachricht.
                        default:
                            break;
                    }
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

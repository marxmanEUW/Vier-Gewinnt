using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VierGewinntServer.DataFormats;
using VierGewinntServer;
using Newtonsoft.Json;

namespace VierGewinntServer
{
    class Connections
    {

        #region Constructor

        static Connections()
        {
            ServerIP = GetLocalIPAddress();
            _BufferSize = new byte[4096];
            CurrentServerStatus = ServerStatus.Offline;
        }

        #endregion

        #region Message Constants

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
        private const string PREFIX_ENDGM = "ENDGM"; //Tells the Server that a Player leaves the Game before it is finished

        private const string GS_VALIDMOVE = "VALID_MOVE"; //Gamestate for 'move was valid', to send to player
        private const string GS_INVALIDMOVE = "INVALID_MOVE";
        private const string GS_YOUWON = "YOU_WON";
        private const string GS_YOULOST = "YOU_LOST";
        private const string GS_DRAW = "DRAW";

        #endregion
        //TODO: CLients anpingen
        #region Private Variables

        private static TcpListener _TcpGameServer;
        private static List<TcpServerClient> _ListConnectedClients = new List<TcpServerClient>();
        private static List<Room> _ListRooms = new List<Room>();
        private static byte[] _BufferSize;
        private static Encoding _EncodingInstance = Encoding.UTF8;

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
                if (_TcpGameServer == null) //Start Server
                {
                    _TcpGameServer = new TcpListener(ServerIP, SERVER_PORT);
                    _TcpGameServer.Start();
                    ConnectionsThread.Start();
                    CurrentServerStatus = ServerStatus.Online;
                }
                else //Stop Server
                {
                    DisconnectClients();
                    _TcpGameServer.Stop();
                    _TcpGameServer = null;
                    CurrentServerStatus = ServerStatus.Offline;
                }
                return true;
            }
            catch
            {
                if (_TcpGameServer != null)
                {
                    _TcpGameServer.Stop();
                    _TcpGameServer = null;
                    CurrentServerStatus = ServerStatus.Offline;
                }
                return false;
            }
        }

        /// <summary>
        /// Returns an unique ID
        /// </summary>
        /// <returns>Unique ID</returns>
        public static string GetNewID()
        {
            long ticks = DateTime.Now.Ticks;
            return String.Format("{0}{1}", ticks, Guid.NewGuid().ToString());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Send a client data.
        /// </summary>
        /// <param name="aClient">Client that gets the data</param>
        /// <param name="aData">Data to send</param>
        private static /*async*/ void SendData(TcpServerClient aClient, String aData)
        {
            try
            {
                if (aClient != null)
                {
                    byte[] dataToSend = new byte[4096];
                    dataToSend = _EncodingInstance.GetBytes(aData);
                    aClient.PlayerClient.GetStream().BeginWrite(dataToSend, 0, dataToSend.Length, null, null);

                    //var byteCount = await aClient.PlayerClient.GetStream().ReadAsync(_BufferSize, 0, _BufferSize.Length);
                    //var response = _EncodingInstance.GetString(_BufferSize, 0, byteCount);
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

        /// <summary>
        /// When clients the request a list of available rooms , they get the names and IDs (etc.) of those as a list.
        /// Sends client a json string with this list.
        /// </summary>
        /// <param name="aClient">Client that requested rooms</param>
        private static void SendAvailableRooms(TcpServerClient aClient)
        {
            bool DEBUG = false;
            //Start DEBUG
            if (DEBUG)
            {
                DataRoom room = new DataRoom();
                room.Name = "TEST_ROOM";

                TcpServerClient client = new TcpServerClient();
                client.ClientID = "TEST_ID";
                client.PlayerClient = new TcpClient();
                client.PlayerName = "TEST_CLIENT";

                CreateNewRoom(JsonConvert.SerializeObject(room), client);
            }
            //End DEBUG

            DataSendRooms RoomData = new DataSendRooms(_ListRooms);
            SendData(aClient, String.Format("{0}", DataProcessor.SerializeSendRoomsData(RoomData)));
        }

        /// <summary>
        /// Send the players a signal that the game started.
        /// </summary>
        /// <param name="aRoom">Room/Game that started</param>
        private static void SendPlayersGameStart(Room aRoom)
        {
            SendData(aRoom.Player1, String.Format("{0}{1}",PREFIX_START,aRoom.Player2.PlayerName));
            SendData(aRoom.Player2, PREFIX_START);
        }

        /// <summary>
        /// Send the player a signal that it's their turn.
        /// </summary>
        /// <param name="aClient">Client that gets the signal</param>
        private static void SendPlayerYourTurn(TcpServerClient aClient)
        {
            SendData(aClient, PREFIX_YOURT);
        }

        /// <summary>
        /// Players take turn while playing the game. Waits in while loops for the players data. Send them a response for their UI to update.
        /// </summary>
        /// <param name="aRoom">Room that the game is played in</param>
        private static void PlayGame(Room aRoom)
        {

            if (aRoom.Status == Room.RoomState.PLAYING)
            {
                SendPlayersGameStart(aRoom);
                Thread.Sleep(150);
                SendPlayerYourTurn(aRoom.Player1);
            }

            while (aRoom.Status == Room.RoomState.PLAYING)
            {
                //Player One's turn
                while (aRoom.ActivePlayer == aRoom.Player1)
                {
                    if (aRoom.TurnData != null && aRoom.TurnData.ClientID == aRoom.ActivePlayer.ClientID)
                    {
                        Room.TurnState lRoomTurnState = aRoom.NextTurn(aRoom.Player1, aRoom.TurnData.Column);

                        switch (lRoomTurnState)
                        {
                            case Room.TurnState.VALID:
                                SendPlayerGameState(aRoom, aRoom.Player1, GS_VALIDMOVE);
                                SendPlayerGameState(aRoom, aRoom.Player2, GS_VALIDMOVE);
                                Thread.Sleep(250);
                                SendPlayerYourTurn(aRoom.Player2);
                                break;
                            case Room.TurnState.NOT_VALID:
                                SendPlayerGameState(aRoom, aRoom.Player1, GS_INVALIDMOVE);
                                break;
                            case Room.TurnState.WINNER:
                                SendPlayerGameState(aRoom, aRoom.Player1, GS_YOUWON);
                                SendPlayerGameState(aRoom, aRoom.Player2, GS_YOULOST);
                                break;
                            case Room.TurnState.DRAW:
                                SendPlayerGameState(aRoom, aRoom.Player1, GS_DRAW);
                                SendPlayerGameState(aRoom, aRoom.Player2, GS_DRAW);
                                break;
                            default:
                                throw new Exception("Room TurnState Fehler. Alles Dreck hier.");
                        }
                    }
                    Thread.Sleep(25);
                }

                //Player Two's turn
                while (aRoom.ActivePlayer == aRoom.Player2)
                {
                    if (aRoom.TurnData != null && aRoom.TurnData.ClientID == aRoom.ActivePlayer.ClientID)
                    {
                        Room.TurnState lRoomTurnState = aRoom.NextTurn(aRoom.Player2, aRoom.TurnData.Column);

                        switch (lRoomTurnState)
                        {
                            case Room.TurnState.VALID:
                                SendPlayerGameState(aRoom, aRoom.Player1, GS_VALIDMOVE);
                                SendPlayerGameState(aRoom, aRoom.Player2, GS_VALIDMOVE);
                                Thread.Sleep(250);
                                SendPlayerYourTurn(aRoom.Player1);
                                break;
                            case Room.TurnState.NOT_VALID:
                                SendPlayerGameState(aRoom, aRoom.Player2, GS_INVALIDMOVE);
                                break;
                            case Room.TurnState.WINNER:
                                SendPlayerGameState(aRoom, aRoom.Player2, GS_YOUWON);
                                SendPlayerGameState(aRoom, aRoom.Player1, GS_YOULOST);
                                break;
                            case Room.TurnState.DRAW:
                                SendPlayerGameState(aRoom, aRoom.Player1, GS_DRAW);
                                SendPlayerGameState(aRoom, aRoom.Player2, GS_DRAW);
                                break;
                            default:
                                throw new Exception("Room TurnState Fehler. Alles Dreck hier.");
                        }
                    }
                    Thread.Sleep(25);
                }
            }

            //MISSING: Delete room with clients
        }

        /// <summary>
        /// Send player/client the current game state (was their move valid etc. and the board state)
        /// </summary>
        /// <param name="aRoom">Room</param>
        /// <param name="aClient">Client the gets this data</param>
        /// <param name="aGameStateString">Board state data</param>
        private static void SendPlayerGameState(Room aRoom, TcpServerClient aClient, string aGameStateString)
        {
            DataGameState lGameState = new DataGameState();
            lGameState.GameState = aGameStateString;
            lGameState.PlayGround = aRoom.PlayGround;
            SendData(aRoom.Player1, String.Format("{0}{1}", PREFIX_GMEST, DataProcessor.SerializeGameStateData(lGameState)));
        }

        /// <summary>
        /// Method that runs on a separat thread and waits for second player to join the room.
        /// </summary>
        /// <param name="aRoom">Room that waits for second player</param>
        private static void WaitForSecondPlayer(Room aRoom)
        {
            while (aRoom.Player2 == null && aRoom.Status == Room.RoomState.WAITING_FOR_SECOND_PLAYER)
            {
                Thread.Sleep(1);
            }
            Thread ThreadPlayGame = new Thread(() => PlayGame(aRoom));
            ThreadPlayGame.Start();
        }

        /// <summary>
        /// Creates a new room, which then waits until a second player has joined.
        /// </summary>
        /// <param name="aData">Data required to creating a new room</param>
        /// <param name="aClient">Client that creates the room</param>
        private static void CreateNewRoom(string aData, TcpServerClient aClient)
        {
            DataRoom NewRoomData = DataProcessor.DeserializeRoomData(aData);

            Room GameRoom = new Room(NewRoomData.Name, aClient);

            _ListRooms.Add(GameRoom);

            Thread ThreadWaitForSecPlayer = new Thread(() => WaitForSecondPlayer(GameRoom));
            ThreadWaitForSecPlayer.Start();
        }

        /// <summary>
        /// Connects a client to a room as the second player.
        /// </summary>
        /// <param name="aOperationData">Json string for room ID</param>
        /// <param name="aClient">Client to connect as second player</param>
        private static void ConnectToRoomAsSecondPlayer(string aOperationData, TcpServerClient aClient)
        {
            RoomID ID = new RoomID(); //DataProcessor.DeserializeRoomID(aOperationData);
            ID.ID = aOperationData;

            GetRoomFromID(ID).AddSecondPlayer(aClient);

            Thread.Sleep(5);

            if(GetRoomFromID(ID).Status == Room.RoomState.PLAYING)
            {
                SendData(aClient, MESSAGE_CONFIRMED);
            }
            else
            {
                SendData(aClient, MESSAGE_ERROR);
            }
            
        }

        /// <summary>
        /// Finds the room object from a room ID
        /// </summary>
        /// <param name="aID">Room ID</param>
        /// <returns>Room object</returns>
        private static Room GetRoomFromID(RoomID aID)
        {
            foreach (Room lRoom in _ListRooms)
            {
                if (lRoom.Id == aID.ID)
                {
                    return lRoom;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the room where this client is in. Assuming each client is only in one room!!!
        /// </summary>
        /// <param name="aClient">Client to search for</param>
        /// <returns>Room in which the client is</returns>
        private static Room GetRoomFromClient(TcpServerClient aClient)
        {
            foreach (Room lRoom in _ListRooms)
            {
                if (lRoom.Player1.ClientID == aClient.ClientID || lRoom.Player2.ClientID == aClient.ClientID)
                {
                    return lRoom;
                }
            }
            return null;
        }

        /// <summary>
        /// Fills the TurnData field in a room with the data received from a client.
        /// Also adds clients ID to the field for later search.
        /// </summary>
        /// <param name="aOperationData">Json string data for game state</param>
        /// <param name="aClient">Player client</param>
        private static void FillPlayerTurnData(string aOperationData, TcpServerClient aClient)
        {
            GetRoomFromClient(aClient).TurnData = DataProcessor.DeserializePlayerTurnData(aOperationData, aClient.ClientID);
        }

        /// <summary>
        /// Decides which operation is requested depending on prefix of the data string.
        /// </summary>
        /// <param name="aData">Full incoming data string</param>
        /// <param name="aClient">Client that send the operation</param>
        private static void DecideOperation(string aData, TcpServerClient aClient)
        {
            string lOperationData = String.Empty;
            string lPrefix = GetDataPrefix(aData, out lOperationData);

            switch (lPrefix)
            {
                case PREFIX_NEWRM:
                    CreateNewRoom(lOperationData, aClient);
                    break;
                case PREFIX_SNDRM:
                    SendAvailableRooms(aClient);
                    break;
                case PREFIX_CONRM:
                    ConnectToRoomAsSecondPlayer(lOperationData, aClient);
                    break;
                case PREFIX_TDATA:
                    FillPlayerTurnData(lOperationData, aClient);
                    break;
                default:
                    MessageBox.Show("Unknown data prefix.");
                    break;
                    //throw new Exception("Unknown data prefix.");
            }
        }

        /// <summary>
        /// Extracts the predefined prefix of incoming data strings. Imported for deciding which operation is needed.
        /// </summary>
        /// <param name="aData">Full incoming data string</param>
        /// <param name="aExtractedData">OUT - Data string without prefix</param>
        /// <returns>Prefix</returns>
        private static string GetDataPrefix(string aData, out string aExtractedData)
        {
            const int PREFIX_LENGTH = 5;

            string lPrefix = String.Empty;
            aExtractedData = String.Empty;

            if (aData.Length > PREFIX_LENGTH)
            {
                lPrefix = aData.Substring(0, PREFIX_LENGTH);
                aExtractedData = aData.Substring(PREFIX_LENGTH);
                return lPrefix;
            }
            else if (aData.Length == PREFIX_LENGTH)
            {
                lPrefix = aData.Substring(0, PREFIX_LENGTH);
                aExtractedData = String.Empty;
                return lPrefix;
            }

            return null;
        }

        /// <summary>
        /// Disconnects all clients that a currently connected / are currently in the list of connected clients.
        /// This should trigger a reaction in the client program!
        /// </summary>
        private static void DisconnectClients()
        {
            foreach (TcpServerClient Client in _ListConnectedClients)
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

            while (_TcpGameServer != null && CurrentServerStatus == ServerStatus.Online)
            {
                try
                {
                    //Client connect with server
                    Client = await _TcpGameServer.AcceptTcpClientAsync();

                    ServerClient = new TcpServerClient();
                    ServerClient.PlayerClient = Client;

                    //Client should immediately send message with client name / player name, gets saved as TcpServerClient
                    NetworkStream lNetworkStream = Client.GetStream();
                    int byteCount = await lNetworkStream.ReadAsync(_BufferSize, 0, _BufferSize.Length);
                    string lPlayerName = _EncodingInstance.GetString(_BufferSize, 0, byteCount);

                    if (lPlayerName != String.Empty)
                    {
                        ServerClient.PlayerName = lPlayerName;
                    }
                    else
                    {
                        ServerClient.PlayerName = "Unknown Player";
                    }

                    //Confirmation Message ?
                    //byte[] ResponseBytes = _EncodingInstance.GetBytes(MESSAGE_CONFIRMED);
                    //await lNetworkStream.WriteAsync(ResponseBytes, 0, ResponseBytes.Length);

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
            foreach (TcpServerClient Client in _ListConnectedClients)
            {
                if (aClient.PlayerClient.Client.RemoteEndPoint == Client.PlayerClient.Client.RemoteEndPoint)
                {
                    canAdd = false;
                }
            }

            if (canAdd)
            {
                _ListConnectedClients.Add(aClient);
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
                    int byteCount = await lNetworkStream.ReadAsync(_BufferSize, 0, _BufferSize.Length);
                    string ReceivedData = _EncodingInstance.GetString(_BufferSize, 0, byteCount);

                    if (ReceivedData != String.Empty)
                    {
                        DecideOperation(ReceivedData, aClient);

                        //Confirmation Message ?
                        //byte[] ResponseBytes = _EncodingInstance.GetBytes(MESSAGE_CONFIRMED);
                        //await lNetworkStream.WriteAsync(ResponseBytes, 0, ResponseBytes.Length);
                    }

                    Thread.Sleep(1);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(">>> " + e.Message);
            }

            lNetworkStream.Close();
            aClient.PlayerClient.Close();
            _ListConnectedClients.Remove(aClient);
        }

        /// <summary>
        /// Gets the IP address of the local device
        /// </summary>
        /// <returns>Local IP</returns>
        private static IPAddress GetLocalIPAddress()
        {
            var Host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var IP in Host.AddressList)
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

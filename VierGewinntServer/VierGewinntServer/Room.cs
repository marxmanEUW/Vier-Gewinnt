using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinntServer
{
    class Room
    {
        public const int NUMBER_OF_ROWS = 6;
        public const int NUMBER_OF_COLUMNS = 7;

        public enum RoomState { WAITING_FOR_SECOND_PLAYER, PLAYING, FINISHED }
        public enum TurnState { VALID, NOT_VALID, NOT_ACITVE_PLAYER, UNDEFINIED_PLAYER}
        
        public string Id { get; private set; }
        public string Name { get; set; }
        public RoomState Status { get; private set; }
        public int[,] PlayGround { get; private set; } //[Row, Column]

        public TcpClient ActivePlayer { get; private set; }
        public TcpClient Player1 { get; private set; }
        public TcpClient Player2 { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="player1"></param>
        public Room(string name, TcpClient player1)
        {
            string Ticks = DateTime.Now.Ticks.ToString();
            Random Rand = new Random();
            string RandomString = Rand.Next(1000, 9999).ToString();
            this.Id = Ticks + RandomString;

            this.Player1 = player1;
            this.Name = name;
            this.Status = RoomState.WAITING_FOR_SECOND_PLAYER;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player2"></param>
        /// <returns></returns>
        public bool AddSecondPlayer(TcpClient player2)
        {
            if (this.Status.Equals(RoomState.WAITING_FOR_SECOND_PLAYER) && this.Player2 == null)
            {
                this.Player2 = player2;
                this.Status = RoomState.PLAYING;
                this.ActivePlayer = this.Player1;
                this.PlayGround = new int[NUMBER_OF_ROWS, NUMBER_OF_COLUMNS];
                return true;
            }
            else
            {
                return false;
            }
        }

        
        public TurnState NextTurn(TcpClient player, int column)
        {
            if (player == this.ActivePlayer)
            {
                int playerNumber;
                if (this.ActivePlayer == this.Player1)
                {
                    playerNumber = 1;
                }
                else if (this.ActivePlayer == this.Player2)
                {
                    playerNumber = 2;
                }
                else
                {
                    playerNumber = 0;
                    Console.WriteLine("ERROR: Active Player is neither Player1 nor Player2!");
                    return TurnState.UNDEFINIED_PLAYER;
                }

                return this.DropPiece(playerNumber, column);
            }
            else
            {
                return TurnState.NOT_ACITVE_PLAYER;
            }
        }

        public TcpClient IsWinner()
        {
            return null;
        }

        private void ChangePlayer()
        {
            if (this.ActivePlayer == this.Player1)
            {
                this.ActivePlayer = this.Player2;
            }
            else if (this.ActivePlayer == this.Player2)
            {
                this.ActivePlayer = this.Player1;
            }
            else
            {
                this.ActivePlayer = this.Player1;
                Console.WriteLine("ERROR: Active Player is neither Player1 nor Player2!");
            }
        }

        private TurnState DropPiece(int playerNumber, int column)
        {
            if (this.PlayGround[0,column] != 0)
            {
                return TurnState.NOT_VALID;
            }

            int newRow = 0;
            for (int i = 1; i <= NUMBER_OF_ROWS; i++)
            {
                if (this.PlayGround[i, column] != 0)
                {
                    this.PlayGround[newRow, column] = playerNumber;
                    return TurnState.VALID;
                }
                newRow = i;
            }

            Console.WriteLine("ERROR: Could not drop piece!");
            return TurnState.NOT_VALID;
        }
    }
}

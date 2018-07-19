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
        
        public string Id { get; private set; }
        public string Name { get; set; }
        public RoomState Status { get; private set; }
        public int[,] PlayGround { get; private set; }

        public TcpClient ActivePlayer { get; private set; }
        public TcpClient Player1 { get; private set; }
        public TcpClient Player2 { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Player1"></param>
        public Room(string Name, TcpClient Player1)
        {
            string Ticks = DateTime.Now.Ticks.ToString();
            Random Rand = new Random();
            string RandomString = Rand.Next(1000, 9999).ToString();
            this.Id = Ticks + RandomString;

            this.Player1 = Player1;
            this.Name = Name;
            this.Status = RoomState.WAITING_FOR_SECOND_PLAYER;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Player2"></param>
        /// <returns></returns>
        public bool AddSecondPlayer(TcpClient Player2)
        {
            if (this.Status.Equals(RoomState.WAITING_FOR_SECOND_PLAYER) && this.Player2 == null)
            {
                this.Player2 = Player2;
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

        
        public bool NextTurn(TcpClient Player, int Row, int Column)
        {
            if (Player == this.ActivePlayer)
            {
                int PlayerNumber;
                if (this.ActivePlayer == this.Player1)
                {
                    PlayerNumber = 1;
                }
                else if (this.ActivePlayer == this.Player2)
                {
                    PlayerNumber = 2;
                }
                else
                {
                    PlayerNumber = 0;
                    Console.WriteLine("ERROR: Active Player is neither Player1 nor Player2!");
                    return false;
                }

                if (this.PlayGround[Row,Column] == 0)
                {
                    this.PlayGround[Row, Column] = PlayerNumber;
                    
                }
                return true;
            }
            else
            {
                return false;
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
                PlayerNumber = 2;
            }
            else
            {
                PlayerNumber = 0;
                Console.WriteLine("ERROR: Active Player is neither Player1 nor Player2!");
                return false;
            }
        }
    }
}

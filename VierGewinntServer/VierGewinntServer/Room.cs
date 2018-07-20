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
        public enum RoomState { WAITING_FOR_SECOND_PLAYER, PLAYING, FINISHED }
        
        private RoomState Status;
        private int[,] PlayGround;

        private TcpClient ActivePlayer;
        private TcpClient Player1;
        private TcpClient Player2;

        public Room(TcpClient Player1)
        {
            this.Player1 = Player1;

            this.Status = RoomState.WAITING_FOR_SECOND_PLAYER;
        }


    }
}

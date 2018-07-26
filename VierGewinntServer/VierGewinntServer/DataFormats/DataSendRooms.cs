using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinntServer.DataFormats
{
    class DataSendRooms
    {
        public DataSendRooms(List<Room> aRooms)
        {
            foreach(Room lRoom in aRooms)
            {
                if(lRoom.Status == Room.RoomState.WAITING_FOR_SECOND_PLAYER)
                {
                    RoomEssentials RoomData = new RoomEssentials();
                    RoomData.RoomName = lRoom.Name;
                    RoomData.RoomID = lRoom.Id;
                    RoomData.PlayerOne = lRoom.Player1.PlayerName;
                }                
            }
        }

        private List<RoomEssentials> _Rooms = new List<RoomEssentials>();

        public List<RoomEssentials> Rooms
        {
            get
            {
                return _Rooms;
            }
        }
    }

    internal class RoomEssentials
    {
        public string RoomName { get; set; }
        public string RoomID { get; set; }
        public string PlayerOne { get; set; }
    }
}

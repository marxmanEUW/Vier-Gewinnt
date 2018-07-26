using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinntClient.DataFormats
{
    class DataSendRooms
    {
        private List<RoomEssentials> _Rooms = new List<RoomEssentials>();

        public List<RoomEssentials> Rooms
        {
            get
            {
                return _Rooms;
            }
            set
            {
                _Rooms = value;
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
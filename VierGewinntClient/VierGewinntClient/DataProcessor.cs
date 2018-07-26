using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VierGewinntClient.DataFormats;

namespace VierGewinntClient
{
    class DataProcessor
    {
        public static string SerializeNewRoomData(DataRoom aRoomData)
        {
            return JsonConvert.SerializeObject(aRoomData);
        }

        public static DataSendRooms DeserializeSendRoomsData(string aJSON_STRING)
        {
            return JsonConvert.DeserializeObject<DataSendRooms>(aJSON_STRING);
        }
    }
}

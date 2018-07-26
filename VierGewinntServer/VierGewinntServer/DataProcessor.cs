using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VierGewinntServer.DataFormats;

namespace VierGewinntServer
{
    class DataProcessor
    {
        #region Deserialize

        public static DataRoom DeserializeRoomData(string aJSON_STRING)
        {
            return JsonConvert.DeserializeObject<DataRoom>(aJSON_STRING);
        }

        public static RoomID DeserializeRoomID(string aJSON_STRING)
        {
            return JsonConvert.DeserializeObject<RoomID>(aJSON_STRING);
        }

        public static DataPlayerTurn DeserializePlayerTurnData(string aJSON_STRING, string aClientID)
        {
            DataPlayerTurn lData = new DataPlayerTurn();

            //Client must send object with empty ClientID field
            //ClientID gets fill here manually
            lData = JsonConvert.DeserializeObject<DataPlayerTurn>(aJSON_STRING);
            lData.ClientID = aClientID;

            return lData;
        }

        #endregion

        #region Serialize

        public static string SerializeSendRoomsData(DataSendRooms aRooms)
        {
            return JsonConvert.SerializeObject(aRooms);
        }

        public static string SerializeGameStateData(DataGameState aGameState)
        {
            return JsonConvert.SerializeObject(aGameState);
        }

        #endregion
    }
}

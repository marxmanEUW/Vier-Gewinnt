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
        string json_string = "";
        
        public static DataRoom DeserializeRoomData(string aJSON_STRING)
        {
            return JsonConvert.DeserializeObject<DataRoom>(aJSON_STRING);
        }
    }
}

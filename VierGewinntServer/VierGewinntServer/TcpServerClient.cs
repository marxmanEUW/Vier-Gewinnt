using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinntServer
{
    class TcpServerClient
    {
        public TcpServerClient()
        {
            ClientID = Connections.GetNewID();
        }

        private const int PLAYER_NAME_MAX_LENGTH = 20;


        private string _PlayerName = String.Empty;
        public string PlayerName
        {
            get { return _PlayerName; }
            set
            {
                if(value.Length > PLAYER_NAME_MAX_LENGTH)
                {
                    _PlayerName = value.Substring(0, PLAYER_NAME_MAX_LENGTH);
                }
                else
                {
                    _PlayerName = value;
                }
            }
        }
        public TcpClient PlayerClient { get; set; }        
        public string ClientID { get; set; }
    }
}

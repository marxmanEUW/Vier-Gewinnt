using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinntClient.DataFormats
{
    class DataGameState
    {
        public string GameState { get; set; } //VALID_MOVE, INVALID_MOVE, YOU_WON, YOU_LOST, DRAW
        public int[,] PlayGround { get; set; } //[Row, Column]
    }
}

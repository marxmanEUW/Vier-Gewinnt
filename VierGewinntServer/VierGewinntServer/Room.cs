﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using VierGewinntServer.DataFormats;

namespace VierGewinntServer
{
    class Room
    {
        public const int NUMBER_OF_ROWS = 6;
        public const int NUMBER_OF_COLUMNS = 7;

        public enum RoomState { WAITING_FOR_SECOND_PLAYER, PLAYING, FINISHED }
        public enum TurnState { VALID, NOT_VALID, NOT_ACITVE_PLAYER, UNDEFINIED_PLAYER, WINNER, DRAW }
        public enum WinState { NOTHING, WINNER, DRAW }

        public string Id { get; private set; }
        public string Name { get; set; }
        public RoomState Status { get; private set; }
        public int[,] PlayGround { get; private set; } //[Row, Column]
        public DataPlayerTurn TurnData { get; set; }

        // ActivePlayer = Winner if Game finished
        public TcpServerClient ActivePlayer;
        public TcpServerClient Player1 { get; private set; }
        public TcpServerClient Player2 { get; private set; }

        /* Example Creation:
         * Room r = new Room("Raum-Name", player1);

            bool addSecondPlayer = r.AddSecondPlayer(player2);
            if (addSecondPlayer == false)
            {
                // zweiter Spieler konnte nicht hinzugefügt werden
            }

            Room.TurnState ts = r.NextTurn(player1, selectedColumn);
            switch (ts)
            {
                case Room.TurnState.VALID:
                    // Zug erfolgreich
                    break;
                case Room.TurnState.WINNER:
                    // Spiel beendet mit Gewinner
                    TcpServerClient winner = r.GetWinner();
                    break;
                case Room.TurnState.DRAW:
                    // Spiel beendet mit Unentschieden
                    break;
                case Room.TurnState.NOT_ACITVE_PLAYER:
                    // Spieler ist nicht am Zug
                    break;
                case Room.TurnState.NOT_VALID:
                    // Zug ungültig
                    break;
                case Room.TurnState.UNDEFINIED_PLAYER:
                    // Spieler nicht definiert
                    break;
                default:
                    break;
            }
         */
        /// <summary>
        /// Creates a new Play-Room.
        /// </summary>
        /// <param name="name">Name of the Room</param>
        /// <param name="player1">Player 1</param>
        public Room(string name, TcpServerClient player1)
        {
            this.Player1 = player1;
            this.Name = name;

            long ticks = DateTime.Now.Ticks;
            this.Id = String.Format("{0}-{1}", ticks, Guid.NewGuid().ToString());

            this.Status = RoomState.WAITING_FOR_SECOND_PLAYER;
        }

        /// <summary>
        /// Adds the second Player.
        /// </summary>
        /// <param name="player2">Player 2</param>
        /// <returns>Returns true if execution was successful.</returns>
        public bool AddSecondPlayer(TcpServerClient player2)
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

        /// <summary>
        /// Executes a Turn from one Player.
        /// </summary>
        /// <param name="player">The executing Player</param>
        /// <param name="column">The selected column</param>
        /// <returns>Returns the state of the turn.</returns>
        public TurnState NextTurn(TcpServerClient player, int column)
        {
            if (this.Status != RoomState.PLAYING)
            {
                Console.WriteLine("ERROR: Room is not fully initialized!");
                return TurnState.NOT_VALID;
            }
            if (player == this.ActivePlayer)
            {
                int playerNumber = this.GetCurrentPlayerNumber();

                if (playerNumber == 0)
                {
                    return TurnState.UNDEFINIED_PLAYER;
                }

                TurnState turnState = this.DropPiece(playerNumber, column);
                WinState winner = this.IsWinner();
                if (winner == WinState.WINNER)
                {
                    this.Status = RoomState.FINISHED;
                    return TurnState.WINNER;
                }
                else if (winner == WinState.DRAW)
                {
                    this.Status = RoomState.FINISHED;
                    return TurnState.DRAW;
                }
                this.ChangePlayer();

                return turnState;
            }
            else
            {
                return TurnState.NOT_ACITVE_PLAYER;
            }
        }

        /// <summary>
        /// Returns the winner of the room, if game is finished.
        /// </summary>
        /// <returns>Returns the winner</returns>
        public TcpServerClient GetWinner()
        {
            if (this.Status == RoomState.FINISHED)
            {
                return this.ActivePlayer;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Checks if one Player has won the Game.
        /// </summary>
        /// <returns></returns>
        private WinState IsWinner()
        {
            int playerNumber = this.GetCurrentPlayerNumber();

            // vertical
            int foundPieces = 0;
            for (int column = 0; column < NUMBER_OF_COLUMNS; column++)
            {
                for (int row = 0; row < NUMBER_OF_ROWS; row++)
                {
                    if (this.PlayGround[row, column] == playerNumber)
                    {
                        foundPieces++;
                    }
                    else { foundPieces = 0; }

                    if (foundPieces >= 4)
                    {
                        return WinState.WINNER;
                    }
                }
            }

            // horizontal
            foundPieces = 0;
            for (int row = 0; row < NUMBER_OF_ROWS; row++)
            {
                for (int column = 0; column < NUMBER_OF_COLUMNS; column++)
                {
                    if (this.PlayGround[row, column] == playerNumber)
                    {
                        foundPieces++;
                    }
                    else { foundPieces = 0; }

                    if (foundPieces >= 4)
                    {
                        return WinState.WINNER;
                    }
                }
            }

            // diagonal von links oben nach rechts unten
            foundPieces = 0;
            for (int row = 0; row < NUMBER_OF_ROWS - 3; row++)
            {
                for (int column = 0; column < NUMBER_OF_COLUMNS - 3; column++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (this.PlayGround[row + i, column + i] == playerNumber)
                        {
                            foundPieces++;
                        }
                        else
                        {
                            foundPieces = 0;
                            break;
                        }
                    }

                    if (foundPieces >= 4)
                    {
                        return WinState.WINNER;
                    }
                }
            }
            // diagonal von rechts oben nach links unten
            foundPieces = 0;
            for (int row = 0; row < NUMBER_OF_ROWS - 3; row++)
            {
                for (int column = NUMBER_OF_COLUMNS -1 ; column > 2; column--)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (this.PlayGround[row + i, column - i] == playerNumber)
                        {
                            foundPieces++;
                        }
                        else
                        {
                            foundPieces = 0;
                            break;
                        }
                    }

                    if (foundPieces >= 4)
                    {
                        return WinState.WINNER;
                    }
                }
            }

            //prüft ob irgendwo noch ein freies Feld ist
            for (int row = 0; row < NUMBER_OF_ROWS; row++)
            {
                for (int column = 0; column < NUMBER_OF_COLUMNS; column++)
                {
                    if (this.PlayGround[row, column] == 0)
                    {
                        return WinState.NOTHING;
                    }
                }
            }
            //ansonsten unentschieden
            return WinState.DRAW;
        }

        /// <summary>
        /// Returns the current active Player.
        /// </summary>
        /// <returns></returns>
        private int GetCurrentPlayerNumber()
        {
            if (this.ActivePlayer == this.Player1)
            {
                return 1;
            }
            else if (this.ActivePlayer == this.Player2)
            {
                return 2;
            }
            else
            {
                Console.WriteLine("ERROR: Active Player is neither Player1 nor Player2!");
                return 0;
            }
        }

        /// <summary>
        /// Changes Player
        /// </summary>
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

        /// <summary>
        /// Drops Piece to the Bottom.
        /// </summary>
        /// <param name="playerNumber">Number of the current Player</param>
        /// <param name="column">The selected column</param>
        /// <returns>Returns state of the turn</returns>
        private TurnState DropPiece(int playerNumber, int column)
        {
            if (this.PlayGround[0, column] != 0)
            {
                return TurnState.NOT_VALID;
            }

            for (int i = NUMBER_OF_ROWS - 1; i >= 0; i--)
            {
                if (this.PlayGround[i, column] == 0)
                {
                    this.PlayGround[i, column] = playerNumber;
                    return TurnState.VALID;
                }
            }

            Console.WriteLine("ERROR: Could not drop piece!");
            return TurnState.NOT_VALID;
        }
    }
}

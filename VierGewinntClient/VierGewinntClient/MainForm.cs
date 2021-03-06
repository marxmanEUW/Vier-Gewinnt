﻿using _VierGewinntClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VierGewinntClient.DataFormats;

namespace VierGewinntClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            playerName = "test";


        }
        private const int ROWS = 7;
        private const int COLUMNS = 7;
        myButton[,] allButtons;
        string playerName;

        /// <summary>
        /// Ruft Popup auf um neuen Raum zu erstellen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuErstellenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //neuen Raum erstellen
            popupNewRoom popNewRoom = new popupNewRoom();
            DialogResult result = popNewRoom.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Raumname und Playername an Server schicken

                //TODO: alle String zu string machen
                string roomName = popNewRoom.roomName;
                //neues Popup-Fenster bittet den User um Geduld bis ein anderer Spieler beigetreten ist--> Anzeige einer Sanduhr oder eines Gifs.
                pictureBoxWaiting.Visible = true;
                Connections.RequestCreateNewRoom(roomName);
                Connections.PlayerOne = playerName; //Name des zweiten Spielers wird gesetzt, wenn Spieler beigetreten ist

                Thread ThreadWaitForGame = new Thread(() => WaitForPlayerTwo());
                ThreadWaitForGame.Start();
            }
        }

        /// <summary>
        /// Thread wartet darauf, dass Spiel gestartet wird 
        /// </summary>

        private void WaitForPlayerTwo()
        {
            while (Connections.Status != Connections.GameStatus.Playing)
            {
                //warte auf Spielstart
            }

            while (Connections.Turn != Connections.TurnStatus.YourTurn)
            {
                //warte auf Turnnachricht von Server
            }
            startGame();
        }

        /// <summary>
        /// Server meldet, wenn zweiter Spieler beigetreten ist, Spiel beginnt
        /// </summary>
        private void startGame()
        {

            //neues Spiel wird erstellt
            //Spieler der den Raum erstellt ist automatisch Spieler 1
            this.Invoke((MethodInvoker)delegate
            {
                pictureBoxWaiting.Visible = false;
                initializeGame();
                gamePanel.Visible = true;
                updatePlayground();

            });
        }

        /// <summary>
        /// Hauptmethode während des Spiels
        /// </summary>
        private void updatePlayground()
        {

            setTurn();
            paintPlayground();

        }


        /// <summary>
        /// Methode bestimmt, welche Spalte geklickt wurde und schickt diese Zahl an den Server. Als Nachricht erhält sie einen Status und ein int-Array mit Werten, die für die Farben der Buttons auf dem Spielfeld stehen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dropButton_CLICK(object sender, EventArgs e)
        {

            myButton clickedButton;

            int clickedColumn = 8;

            clickedButton = (myButton)sender;

            for (int row = 0; row < ROWS; row++)
            {
                for (int column = 0; column < COLUMNS; column++)
                {
                    if (allButtons[row, column].Equals(clickedButton))
                    {
                        clickedColumn = column;
                        break;

                    }
                }
            }


            //Schicke column an Server
            Connections.SendColumnToServer(clickedColumn);
            foreach (myButton button in allButtons)
            {
                button.Enabled = false;

            }
            //TODO: bei Spieler, der gerade gezogen hat, müssten die Buttons disabled sein, während er wartet...
            Thread ThreadWaitValidReply = new Thread(() => WaitForValidReply());
            ThreadWaitValidReply.Start();


        }
        /// <summary>
        /// //Warten auf Antwort vom Server ob Zug gültig ist
        /// </summary>
        private void WaitForValidReply()
        {
            while (Connections.Valid != Connections.ValidStatus.Valid)
            {
                if (Connections.Valid == Connections.ValidStatus.Invalid)
                {
                    this.Invoke((MethodInvoker)delegate
                    {

                        MessageBox.Show("Dieser Zug ist ungültig, bitte wähle eine andere Spalte.");
                        Connections.Valid = Connections.ValidStatus.NoState;
                        foreach (myButton button in allButtons)
                        {
                            button.Enabled = true;

                        }

                    });

                    return;
                }
                else //NoState
                {
                    //tue nix
                }
            }

            updatePlayground();
            Thread ThreadWaitForTurn = new Thread(() => WaitForTurn());
            ThreadWaitForTurn.Start();
        }

        private void paintPlayground()
        {

            System.Drawing.Color color;

            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 7; column++)
                {

                    switch (Connections.GameState.PlayGround[row, column])
                    {
                        case 0:
                            color = System.Drawing.Color.White;
                            break;
                        case 1:
                            color = System.Drawing.Color.Red;
                            break;
                        case 2:
                            color = System.Drawing.Color.Blue;
                            break;
                        default:
                            color = System.Drawing.Color.White;
                            break;
                    }
                    //Array mit farbigen Steinen beginnt erst bei Zeilenindex 1
                    myButton thisButton = allButtons[row + 1, column];
                    thisButton.setColor(color);
                    thisButton.setTextColor();

                }

            }
        }
        private void WaitForTurn()
        {

            while (Connections.Turn != Connections.TurnStatus.YourTurn)
            {
                //Schleife soll auch abbrechen, wenn Spiel beendet ist
                if (Connections.Status != Connections.GameStatus.Playing) { break; }
            }

            updatePlayground();

            switch (Connections.Status)
            {

                case Connections.GameStatus.YouWon:
                    MessageBox.Show("Du hast gewonnen.");

                    break;
                case Connections.GameStatus.YouLost:
                    MessageBox.Show("Du hast verloren.");
                    break;
                case Connections.GameStatus.Draw:
                    MessageBox.Show("Das Spiel endet unentschieden.");
                    break;
                default:
                    break;
            }


        }

        /// <summary>
        /// Methode erlaubt Änderung der Farbe von Spieler 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColorPlayerOne_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Methode erlaubt Änderung der Farbe von Spieler 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonColorPlayerTwo_Click(object sender, EventArgs e)
        {
            ColoredButton buttonClicked = (ColoredButton)sender;
            //TODO: Farbwähler
        }

        private void initializeGame()
        {
            //DropButtonarray erstellen
            allButtons = new myButton[ROWS, COLUMNS];
            for (int row = 0; row < ROWS; row++)
            {
                for (int column = 0; column < COLUMNS; column++)
                {
                    myButton newButton;
                    System.Drawing.Color color;
                    if (row == 0)
                    {
                        newButton = new DropButton();
                        color = System.Drawing.ColorTranslator.FromHtml("#D8D8D8");
                        newButton.setColor(color);
                    }
                    else
                    {
                        newButton = new ColoredButton();
                        color = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        newButton.setColor(color);
                        newButton.setTextColor();
                    }

                    newButton.Enabled = false;
                    newButton.Click += new System.EventHandler(this.dropButton_CLICK);
                    allButtons[row, column] = newButton;                //für jeden Button im Array diesen der Tabelle zuweisen.


                    this.tableLayoutPanel1.Controls.Add(newButton);
                    this.gamePanel.Controls.Add(tableLayoutPanel1);
                }

            }

            this.labelPlayerOne.Text = Connections.PlayerOne;
            this.labelPlayerTwo.Text = Connections.PlayerTwo;


        }

        private void setTurn()
        {
            this.Invoke((MethodInvoker)delegate
            {
                if ((Connections.PlayerOne == playerName) && (Connections.Turn == Connections.TurnStatus.YourTurn))
                {

                    labelTurnPlayerOne.Visible = true;
                    labelTurnPlayerTwo.Visible = false;
                    foreach (myButton button in allButtons)
                    {
                        button.Enabled = true;

                    }
                }
                else if ((Connections.PlayerOne == playerName) && (Connections.Turn != Connections.TurnStatus.YourTurn))
                {

                    labelTurnPlayerOne.Visible = false;
                    labelTurnPlayerTwo.Visible = true;
                    //solange man nicht dran ist, kann man keinen Zug machen
                    foreach (myButton button in allButtons)
                    {
                        button.Enabled = false;

                    }
                }
                else if ((Connections.PlayerTwo == playerName) && (Connections.Turn == Connections.TurnStatus.YourTurn))
                {
                    labelTurnPlayerOne.Visible = false;
                    labelTurnPlayerTwo.Visible = true;
                    foreach (myButton button in allButtons)
                    {
                        button.Enabled = true;

                    }

                }
                else if ((Connections.PlayerTwo == playerName) && (Connections.Turn != Connections.TurnStatus.YourTurn))
                {
                    labelTurnPlayerOne.Visible = true;
                    labelTurnPlayerTwo.Visible = false;
                    //solange man nicht dran ist, kann man keinen Zug machen
                    foreach (myButton button in allButtons)
                    {
                        button.Enabled = false;

                    }
                }
            });
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Boolean isConnected = false;
            while (!isConnected)
            {
                LoginForm loginForm = new LoginForm();
                DialogResult result = loginForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //IP und Spielername an Server schicken
                    playerName = loginForm.PlayerName;
                    isConnected = Connections.ConnectToServer(loginForm.ServerIP, loginForm.ServerPort, playerName);
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        private void spielEndeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            gamePanel.Visible = false;
            destroyGame();
            Connections.SendEndGameToServer();
        }

        private void destroyGame()
        {
            tableLayoutPanel1.Controls.Clear();
        }

        private void anwendungEndeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Connections.SendEndGameToServer();
            Environment.Exit(0);
        }

        private void spielWählenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Anfrage an Server nach aktuellen Spielen
            DataFormats.DataSendRooms sendRooms = Connections.RequestAvailableRooms();

            //Pop-up mit Räumen

            ChooseRoom popupChooseRoom = new ChooseRoom(sendRooms);
            DialogResult result = popupChooseRoom.ShowDialog();
            if (result == DialogResult.OK)
            {
                //nach Auswahl wird Spiel gestartet
                Connections.RequestConnectAsSecondPlayer(popupChooseRoom.chosenRoom.RoomID);
                Connections.PlayerOne = popupChooseRoom.chosenRoom.PlayerOne;
                Connections.PlayerTwo = playerName;
                initializeGame(); //Daten aus gewähltem Raum einfügen
                gamePanel.Visible = true;
                Thread ThreadWaitForTurn = new Thread(() => WaitForTurn());
                ThreadWaitForTurn.Start();

            }

        }

        private void spielanleitungToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            PopupHilfe popupHilfe = new PopupHilfe("manual");
            DialogResult result = popupHilfe.ShowDialog();

        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopupHilfe popupHilfe = new PopupHilfe("about");
            DialogResult result = popupHilfe.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connections.SendEndGameToServer();
        }
    }




}

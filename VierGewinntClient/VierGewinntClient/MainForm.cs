using _VierGewinntClient;
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
                Connections.PlayerOne = playerName;

                Thread ThreadWaitForGame = new Thread(() => WaitForServer());
                ThreadWaitForGame.Start();

            }




        }

        /// <summary>
        /// Thread wartet darauf, dass Spiel gestartet wird 
        /// </summary>

        private void WaitForServer()
        {
            while (Connections.Status != Connections.GameStatus.Playing)
            {
                //tue irgendwas
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
                initializeGame(Connections.PlayerOne, Connections.PlayerTwo);
                gamePanel.Visible = true;
                playGame();

            });
        }

        /// <summary>
        /// Hauptmethode während des Spiels
        /// </summary>
        private void playGame()
        {

            paintPlayground();
            setTurn();
            if (Connections.Turn == Connections.TurnStatus.YourTurn)
            {
                foreach (myButton button in allButtons)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        button.Enabled = true;

                    });
                }
            }
            else if (Connections.Turn == Connections.TurnStatus.EnemyTurn)
            {
                //solange man nicht dran ist, kann man keinen Zug machen
            }

        }


        /// <summary>
        /// Methode bestimmt, welche Spalte geklickt wurde und schickt diese Zahl an den Server. Als Nachricht erhält sie einen Status und ein int-Array mit Werten, die für die Farben der Buttons auf dem Spielfeld stehen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dropButton_CLICK(object sender, EventArgs e)
        {

            Random rnd;
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
            //   Thread.Sleep(10000);


            //Warten auf Antwort vom Server
            while (Connections.Valid != Connections.ValidStatus.Valid)
            {
                if (Connections.Valid == Connections.ValidStatus.Invalid)
                {
                    //TODO: Popup mit Info, dass der Zug ungültig ist und
                    Connections.Valid = Connections.ValidStatus.NoState;
                    break;
                }
                else //NoState
                {
                    //tue nix
                }
            }

            //Antwort vom Server enthält ein Array der Größe 6x7.

            //alle Steine deaktivieren
            foreach (myButton button in allButtons) { button.Enabled = false; }
            Thread ThreadWaitForTurn = new Thread(() => WaitForTurn());
            ThreadWaitForTurn.Start();
        }
        private void paintPlayground()
        {

            int[,] arrayVonServer;
            System.Drawing.Color color;

            arrayVonServer = new int[6, 7];
            //rnd = new Random();
            // IntArray füllen, damit weitere Methoden getestet werden können
            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 7; column++)
                {

                    //int i = rnd.Next(0, 3);
                    //arrayVonServer[row, column] = i;

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
                //tue irgendwas
            }

            playGame();
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

        private void initializeGame(string player1, string player2)
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

            this.labelPlayerOne.Text = player1;
            this.labelPlayerTwo.Text = player2;


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
                else if ((Connections.PlayerTwo == playerName) && (Connections.Turn == Connections.TurnStatus.YourTurn))
                {
                    labelTurnPlayerOne.Visible = false;
                    labelTurnPlayerTwo.Visible = true;
                    //solange man nicht dran ist, kann man keinen Zug machen

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
            }
        }

        private void spielEndeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            gamePanel.Visible = false;
            destroyGame();
            Connections.sendEndGameToServer();
        }

        private void destroyGame()
        {
            tableLayoutPanel1.Controls.Clear();
        }

        private void anwendungEndeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Connections.sendEndGameToServer();
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
                initializeGame(popupChooseRoom.chosenRoom.PlayerOne, playerName); //Daten aus gewähltem Raum einfügen
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
            Connections.sendEndGameToServer();
        }
    }




}

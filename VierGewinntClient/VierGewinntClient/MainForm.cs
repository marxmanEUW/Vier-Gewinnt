using _VierGewinntClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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


        private void neuErstellenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //neuen Raum erstellen
            popupNewRoom popNewRoom = new popupNewRoom();
            DialogResult result = popNewRoom.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Raumname und Playername an Server schicken
                //Antwort wenn Raum erstellt wurde

                //neues Popup-Fenster bittet den User um Geduld bis ein anderer Spieler beigetreten ist--> Anzeige einer Sanduhr oder eines Gifs.
                pictureBoxWaiting.Visible = true;
                //Server meldet, wenn zweiter Spieler beigetreten ist
                //neues Spiel wird erstellt
                //Spieler der den Raum erstellt ist automatisch Spieler 1
                pictureBoxWaiting.Visible = false;
                initializeGame(playerName, "ZweiterSpieler");
                gamePanel.Visible = true;

            }




        }




        private void spielanleitungToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void MainForm_Load(object sender, EventArgs e)
        {



        }
        /// <summary>
        /// Methode bestimmt, welche Spalte geklickt wurde und schickt diese Zahl an den Server. Als Nachricht erhält sie einen Status und ein int-Array mit Werten, die für die Farben der Buttons auf dem Spielfeld stehen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dropButton_CLICK(object sender, EventArgs e)
        {
            int[,] arrayVonServer;
            Random rnd;
            myButton clickedButton;
            System.Drawing.Color color;
            int clickedColumn;

            clickedButton = (myButton)sender;

            for (int row = 0; row < ROWS; row++)
            {
                for (int column = 0; column < COLUMNS; column++)
                {
                    if (allButtons[row, column].Equals(clickedButton))
                    {
                        clickedColumn = column;
                        //Schicke column an Server
                    }
                }
            }


            //Antwort vom Server enthält ein Array der Größe 6x7.
            arrayVonServer = new int[6, 7];
            rnd = new Random();
            // IntArray füllen, damit weitere Methoden getestet werden können
            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 7; column++)
                {

                    int i = rnd.Next(0, 3);
                    arrayVonServer[row, column] = i;

                    switch (i)
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


                    newButton.Click += new System.EventHandler(this.dropButton_CLICK);
                    allButtons[row, column] = newButton;                //für jeden Button im Array diesen der Tabelle zuweisen.


                    this.tableLayoutPanel1.Controls.Add(newButton);
                    this.gamePanel.Controls.Add(tableLayoutPanel1);
                }

            }

            this.labelPlayerOne.Text = player1;
            this.labelPlayerTwo.Text = player2;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Boolean isConnected = true;
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
        }

        private void destroyGame()
        {
            tableLayoutPanel1.Controls.Clear();
        }

        private void anwendungEndeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void spielWählenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Anfrage an Server nach aktuellen Spielen
            //Pop-up mit Räumen
            //nach Auswahl wird Spiel gestartet
            initializeGame(playerName, "ZweiterSpieler");
            gamePanel.Visible = true;
        }
    }




}

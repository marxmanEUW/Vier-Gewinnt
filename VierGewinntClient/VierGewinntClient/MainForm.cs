﻿using _VierGewinntClient;
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

            //TODO: Login-Formular laden
        }
        private const int ROWS = 7;
        private const int COLUMNS = 7;
        myButton[,] allButtons;

        private void neuErstellenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gamePanel.Visible = true;
        }

        private void spielWählenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gamePanel.Visible = true;
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void spielanleitungToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            allButtons = new myButton[ROWS, COLUMNS];

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
                }

            }
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


    }




}

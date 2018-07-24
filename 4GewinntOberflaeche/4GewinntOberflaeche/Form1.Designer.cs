using System;

namespace _4GewinntOberflaeche
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.spielToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spielBeendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuerRaumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spielWaehlenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informationenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.coloredButton1 = new ColoredButton();
            this.coloredButton2 = new ColoredButton();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spielToolStripMenuItem,
            this.hilfeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.spielToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            neuerRaumToolStripMenuItem, spielWaehlenToolStripMenuItem, this.spielBeendenToolStripMenuItem});
            this.spielToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.spielToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.spielToolStripMenuItem.Text = "Spiel";
            // 
            // spielBeendenToolStripMenuItem
            // 
            this.spielBeendenToolStripMenuItem.Name = "spielBeendenToolStripMenuItem";
            this.spielBeendenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.spielBeendenToolStripMenuItem.Text = "Beenden";
            this.spielBeendenToolStripMenuItem.Click += new EventHandler(Form1.spielBeenden);

            // spielBeendenToolStripMenuItem
            // 
            this.neuerRaumToolStripMenuItem.Name = "neuerRaumToolStripMenuItem";
            this.neuerRaumToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.neuerRaumToolStripMenuItem.Text = "Neu erstellen";
            this.neuerRaumToolStripMenuItem.Click += new EventHandler(Form1.spielWaehlen);

            // spielBeendenToolStripMenuItem
            // 
            this.spielWaehlenToolStripMenuItem.Name = "spielWaehlenToolStripMenuItem";
            this.spielWaehlenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.spielWaehlenToolStripMenuItem.Text = "Spiel wählen";
            this.spielWaehlenToolStripMenuItem.Click += new EventHandler(Form1.spielWaehlen);
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informationenToolStripMenuItem});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hilfeToolStripMenuItem.Text = "Hilfe";
            // 
            // informationenToolStripMenuItem
            // 
            this.informationenToolStripMenuItem.Name = "informationenToolStripMenuItem";
            this.informationenToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.informationenToolStripMenuItem.Text = "Informationen";
            // 
            // tableLayoutPanel1
            // 
            columns = 7;
            rows = 7;
            this.tableLayoutPanel1.ColumnCount = columns;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Location = new System.Drawing.Point(26, 44);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = rows;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 200);
            this.tableLayoutPanel1.TabIndex = 1;

            //DropButtonarray erstellen
              numberOfButtons = columns * rows;
              allButtons = new myButton[rows,columns];
              for (int row = 0; row < rows; row++)
              {
                  for (int column = 0; column < columns; column++)
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

                      
                    
                      allButtons[row, column] = newButton;                //für jeden Button im Array diesen der Tabelle zuweisen.


                      this.tableLayoutPanel1.Controls.Add(newButton);
                  }    

              }
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Spieler 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Spieler 2";
            // 
            // coloredButton2
            // 
            this.coloredButton2.BackColor = System.Drawing.Color.Blue;
            this.coloredButton2.setTextColor();
            this.coloredButton2.FlatAppearance.BorderSize = 0;
            this.coloredButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.coloredButton2.Location = new System.Drawing.Point(77, 289);
            this.coloredButton2.Name = "coloredButton2";
            this.coloredButton2.Size = new System.Drawing.Size(40, 40);
            this.coloredButton2.TabIndex = 5;
            this.coloredButton2.UseVisualStyleBackColor = false;

            // 
            // coloredButton1
            // 
            this.coloredButton1.BackColor = System.Drawing.Color.Red;
            this.coloredButton1.setTextColor();
            this.coloredButton1.FlatAppearance.BorderSize = 0;
            this.coloredButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.coloredButton1.Location = new System.Drawing.Point(77, 260);
            this.coloredButton1.Name = "coloredButton1";
            this.coloredButton1.Size = new System.Drawing.Size(40, 40);
            this.coloredButton1.TabIndex = 4;
            this.coloredButton1.UseVisualStyleBackColor = false;
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 330);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.coloredButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.coloredButton2);
            this.Controls.Add(this.menuStrip1);

            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Spielfenster";
            this.Text = "Spiel";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private static void spielWaehlen(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void spielBeenden(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public static void buttonClick(Object sender, EventArgs e)
        {
            int[,] arrayVonServer;
            Random rnd;
            myButton clickedButton;
            System.Drawing.Color color;

            //dem Server eine Nachricht schicken
            //darin steht die Zeile und die Spalte des Buttons
            clickedButton = (myButton)sender;


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
                    myButton thisButton = allButtons[row+1, column];
                    thisButton.setColor(color);
                    thisButton.setTextColor();
                }

            }

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem spielToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spielBeendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neuerRaumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spielWaehlenToolStripMenuItem;
        
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informationenToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private int columns;
        private int rows;
        private int numberOfButtons;
        private static myButton[,] allButtons;
        private myButton coloredButton1;
        private myButton coloredButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}


namespace VierGewinntClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.spielToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuErstellenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spielWählenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spielEndeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.anwendungEndeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spielanleitungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.labelTurnPlayerTwo = new System.Windows.Forms.Label();
            this.labelTurnPlayerOne = new System.Windows.Forms.Label();
            this.buttonColorPlayerTwo = new _VierGewinntClient.ColoredButton();
            this.buttonColorPlayerOne = new _VierGewinntClient.ColoredButton();
            this.labelPlayerTwo = new System.Windows.Forms.Label();
            this.labelPlayerOne = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pictureBoxWaiting = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.gamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWaiting)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spielToolStripMenuItem,
            this.hilfeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(428, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // spielToolStripMenuItem
            // 
            this.spielToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuErstellenToolStripMenuItem,
            this.spielWählenToolStripMenuItem,
            this.beendenToolStripMenuItem});
            this.spielToolStripMenuItem.Name = "spielToolStripMenuItem";
            this.spielToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.spielToolStripMenuItem.Text = "Spiel";
            // 
            // neuErstellenToolStripMenuItem
            // 
            this.neuErstellenToolStripMenuItem.Name = "neuErstellenToolStripMenuItem";
            this.neuErstellenToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.neuErstellenToolStripMenuItem.Text = "Neu erstellen";
            this.neuErstellenToolStripMenuItem.Click += new System.EventHandler(this.neuErstellenToolStripMenuItem_Click);
            // 
            // spielWählenToolStripMenuItem
            // 
            this.spielWählenToolStripMenuItem.Name = "spielWählenToolStripMenuItem";
            this.spielWählenToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.spielWählenToolStripMenuItem.Text = "Spiel wählen";
            this.spielWählenToolStripMenuItem.Click += new System.EventHandler(this.spielWählenToolStripMenuItem_Click);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spielEndeToolStripMenuItem1,
            this.anwendungEndeToolStripMenuItem});
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            // 
            // spielEndeToolStripMenuItem1
            // 
            this.spielEndeToolStripMenuItem1.Name = "spielEndeToolStripMenuItem1";
            this.spielEndeToolStripMenuItem1.Size = new System.Drawing.Size(139, 22);
            this.spielEndeToolStripMenuItem1.Text = "Spiel";
            this.spielEndeToolStripMenuItem1.Click += new System.EventHandler(this.spielEndeToolStripMenuItem1_Click);
            // 
            // anwendungEndeToolStripMenuItem
            // 
            this.anwendungEndeToolStripMenuItem.Name = "anwendungEndeToolStripMenuItem";
            this.anwendungEndeToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.anwendungEndeToolStripMenuItem.Text = "Anwendung";
            this.anwendungEndeToolStripMenuItem.Click += new System.EventHandler(this.anwendungEndeToolStripMenuItem_Click);
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spielanleitungToolStripMenuItem,
            this.überToolStripMenuItem});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hilfeToolStripMenuItem.Text = "Hilfe";
            // 
            // spielanleitungToolStripMenuItem
            // 
            this.spielanleitungToolStripMenuItem.Name = "spielanleitungToolStripMenuItem";
            this.spielanleitungToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.spielanleitungToolStripMenuItem.Text = "Spielanleitung";
            this.spielanleitungToolStripMenuItem.Click += new System.EventHandler(this.spielanleitungToolStripMenuItem_Click_1);
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.überToolStripMenuItem.Text = "Über";
            this.überToolStripMenuItem.Click += new System.EventHandler(this.überToolStripMenuItem_Click);
            // 
            // gamePanel
            // 
            this.gamePanel.AutoSize = true;
            this.gamePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gamePanel.Controls.Add(this.labelTurnPlayerTwo);
            this.gamePanel.Controls.Add(this.labelTurnPlayerOne);
            this.gamePanel.Controls.Add(this.buttonColorPlayerTwo);
            this.gamePanel.Controls.Add(this.buttonColorPlayerOne);
            this.gamePanel.Controls.Add(this.labelPlayerTwo);
            this.gamePanel.Controls.Add(this.labelPlayerOne);
            this.gamePanel.Controls.Add(this.tableLayoutPanel1);
            this.gamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamePanel.Location = new System.Drawing.Point(0, 24);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(428, 267);
            this.gamePanel.TabIndex = 6;
            this.gamePanel.Visible = false;
            // 
            // labelTurnPlayerTwo
            // 
            this.labelTurnPlayerTwo.AutoSize = true;
            this.labelTurnPlayerTwo.Location = new System.Drawing.Point(138, 236);
            this.labelTurnPlayerTwo.Name = "labelTurnPlayerTwo";
            this.labelTurnPlayerTwo.Size = new System.Drawing.Size(56, 13);
            this.labelTurnPlayerTwo.TabIndex = 16;
            this.labelTurnPlayerTwo.Text = "ist am Zug";
            this.labelTurnPlayerTwo.Visible = false;
            // 
            // labelTurnPlayerOne
            // 
            this.labelTurnPlayerOne.AutoSize = true;
            this.labelTurnPlayerOne.Location = new System.Drawing.Point(138, 212);
            this.labelTurnPlayerOne.Name = "labelTurnPlayerOne";
            this.labelTurnPlayerOne.Size = new System.Drawing.Size(56, 13);
            this.labelTurnPlayerOne.TabIndex = 15;
            this.labelTurnPlayerOne.Text = "ist am Zug";
            // 
            // buttonColorPlayerTwo
            // 
            this.buttonColorPlayerTwo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonColorPlayerTwo.BackColor = System.Drawing.Color.Blue;
            this.buttonColorPlayerTwo.FlatAppearance.BorderSize = 0;
            this.buttonColorPlayerTwo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColorPlayerTwo.Location = new System.Drawing.Point(30, 232);
            this.buttonColorPlayerTwo.MaximumSize = new System.Drawing.Size(40, 60);
            this.buttonColorPlayerTwo.Name = "buttonColorPlayerTwo";
            this.buttonColorPlayerTwo.Size = new System.Drawing.Size(20, 20);
            this.buttonColorPlayerTwo.TabIndex = 14;
            this.buttonColorPlayerTwo.UseVisualStyleBackColor = false;
            this.buttonColorPlayerTwo.Click += new System.EventHandler(this.buttonColorPlayerTwo_Click);
            // 
            // buttonColorPlayerOne
            // 
            this.buttonColorPlayerOne.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonColorPlayerOne.BackColor = System.Drawing.Color.Red;
            this.buttonColorPlayerOne.FlatAppearance.BorderSize = 0;
            this.buttonColorPlayerOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonColorPlayerOne.Location = new System.Drawing.Point(30, 207);
            this.buttonColorPlayerOne.MaximumSize = new System.Drawing.Size(40, 60);
            this.buttonColorPlayerOne.Name = "buttonColorPlayerOne";
            this.buttonColorPlayerOne.Size = new System.Drawing.Size(20, 20);
            this.buttonColorPlayerOne.TabIndex = 13;
            this.buttonColorPlayerOne.UseVisualStyleBackColor = false;
            this.buttonColorPlayerOne.Click += new System.EventHandler(this.buttonColorPlayerOne_Click);
            // 
            // labelPlayerTwo
            // 
            this.labelPlayerTwo.AutoSize = true;
            this.labelPlayerTwo.Location = new System.Drawing.Point(52, 236);
            this.labelPlayerTwo.Name = "labelPlayerTwo";
            this.labelPlayerTwo.Size = new System.Drawing.Size(79, 13);
            this.labelPlayerTwo.TabIndex = 12;
            this.labelPlayerTwo.Text = "labelPlayerTwo";
            // 
            // labelPlayerOne
            // 
            this.labelPlayerOne.AutoSize = true;
            this.labelPlayerOne.Location = new System.Drawing.Point(53, 212);
            this.labelPlayerOne.Name = "labelPlayerOne";
            this.labelPlayerOne.Size = new System.Drawing.Size(78, 13);
            this.labelPlayerOne.TabIndex = 11;
            this.labelPlayerOne.Text = "labelPlayerOne";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Location = new System.Drawing.Point(30, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(369, 200);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // pictureBoxWaiting
            // 
            this.pictureBoxWaiting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxWaiting.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxWaiting.Image")));
            this.pictureBoxWaiting.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxWaiting.Name = "pictureBoxWaiting";
            this.pictureBoxWaiting.Size = new System.Drawing.Size(428, 291);
            this.pictureBoxWaiting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxWaiting.TabIndex = 0;
            this.pictureBoxWaiting.TabStop = false;
            this.pictureBoxWaiting.UseWaitCursor = true;
            this.pictureBoxWaiting.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(428, 291);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBoxWaiting);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(5000, 5000);
            this.MinimumSize = new System.Drawing.Size(300, 330);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "4gewinnt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gamePanel.ResumeLayout(false);
            this.gamePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWaiting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem spielToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neuErstellenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spielWählenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spielanleitungToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem überToolStripMenuItem;
        private System.Windows.Forms.Panel gamePanel;
        private _VierGewinntClient.ColoredButton buttonColorPlayerTwo;
        private _VierGewinntClient.ColoredButton buttonColorPlayerOne;
        private System.Windows.Forms.Label labelPlayerTwo;
        private System.Windows.Forms.Label labelPlayerOne;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem spielEndeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem anwendungEndeToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBoxWaiting;
        private System.Windows.Forms.Label labelTurnPlayerTwo;
        private System.Windows.Forms.Label labelTurnPlayerOne;
    }
}


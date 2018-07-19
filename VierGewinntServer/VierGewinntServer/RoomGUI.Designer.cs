namespace VierGewinntServer
{
    partial class RoomGUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblRoomName = new System.Windows.Forms.Label();
            this.lblClientOne = new System.Windows.Forms.Label();
            this.lblClientTwo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblRoomName
            // 
            this.lblRoomName.BackColor = System.Drawing.Color.LightGray;
            this.lblRoomName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRoomName.Location = new System.Drawing.Point(0, 0);
            this.lblRoomName.Name = "lblRoomName";
            this.lblRoomName.Size = new System.Drawing.Size(300, 30);
            this.lblRoomName.TabIndex = 0;
            this.lblRoomName.Text = "Der coolste Raum";
            this.lblRoomName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblClientOne
            // 
            this.lblClientOne.BackColor = System.Drawing.Color.LightSalmon;
            this.lblClientOne.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblClientOne.Location = new System.Drawing.Point(0, 30);
            this.lblClientOne.Name = "lblClientOne";
            this.lblClientOne.Size = new System.Drawing.Size(150, 40);
            this.lblClientOne.TabIndex = 1;
            this.lblClientOne.Text = "Client 1";
            this.lblClientOne.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClientTwo
            // 
            this.lblClientTwo.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblClientTwo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClientTwo.Location = new System.Drawing.Point(150, 30);
            this.lblClientTwo.Name = "lblClientTwo";
            this.lblClientTwo.Size = new System.Drawing.Size(150, 40);
            this.lblClientTwo.TabIndex = 2;
            this.lblClientTwo.Text = "Client 2";
            this.lblClientTwo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RoomGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblClientTwo);
            this.Controls.Add(this.lblClientOne);
            this.Controls.Add(this.lblRoomName);
            this.MinimumSize = new System.Drawing.Size(200, 60);
            this.Name = "RoomGUI";
            this.Size = new System.Drawing.Size(300, 70);
            this.Resize += new System.EventHandler(this.RoomGUI_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRoomName;
        private System.Windows.Forms.Label lblClientOne;
        private System.Windows.Forms.Label lblClientTwo;
    }
}

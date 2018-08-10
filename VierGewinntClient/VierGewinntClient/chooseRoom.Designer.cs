namespace VierGewinntClient
{
    partial class ChooseRoom
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
            this.btnChoose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.listBoxRooms = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnChoose
            // 
            this.btnChoose.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnChoose.Location = new System.Drawing.Point(45, 187);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(195, 33);
            this.btnChoose.TabIndex = 3;
            this.btnChoose.Text = "Auswählen";
            this.btnChoose.UseVisualStyleBackColor = false;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(45, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(195, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // listBoxRooms
            // 
            this.listBoxRooms.FormattingEnabled = true;
            this.listBoxRooms.Location = new System.Drawing.Point(45, 26);
            this.listBoxRooms.Name = "listBoxRooms";
            this.listBoxRooms.Size = new System.Drawing.Size(195, 108);
            this.listBoxRooms.TabIndex = 7;
            // 
            // ChooseRoom
            // 
            this.AcceptButton = this.btnChoose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.listBoxRooms);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnChoose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseRoom";
            this.Text = "Raum auswählen";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox listBoxRooms;
    }
}
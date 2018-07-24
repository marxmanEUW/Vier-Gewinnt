namespace VierGewinntClient
{
    partial class PopupHilfe
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
            this.manualPanel = new System.Windows.Forms.Panel();
            this.manualLabel = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.aboutPanel = new System.Windows.Forms.Panel();
            this.manualPanel.SuspendLayout();
            this.aboutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // manualPanel
            // 
            this.manualPanel.Controls.Add(this.manualLabel);
            this.manualPanel.Controls.Add(this.linkLabel1);
            this.manualPanel.Location = new System.Drawing.Point(7, 10);
            this.manualPanel.Name = "manualPanel";
            this.manualPanel.Size = new System.Drawing.Size(342, 37);
            this.manualPanel.TabIndex = 1;
            this.manualPanel.Visible = false;
            // 
            // manualLabel
            // 
            this.manualLabel.AutoSize = true;
            this.manualLabel.Location = new System.Drawing.Point(6, 10);
            this.manualLabel.Name = "manualLabel";
            this.manualLabel.Size = new System.Drawing.Size(113, 13);
            this.manualLabel.TabIndex = 3;
            this.manualLabel.Text = "Link zur Spielanleitung";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(120, 10);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(209, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://de.wikipedia.org/wiki/Vier_gewinnt";
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Location = new System.Drawing.Point(66, 198);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(195, 23);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Schließen";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Angela Stöckert";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Patrick Killian";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Conrad Ostertag";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dieses Vier-gewinnt-Spiel wurde entwickelt von:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Über";
            // 
            // aboutPanel
            // 
            this.aboutPanel.Controls.Add(this.label1);
            this.aboutPanel.Controls.Add(this.label2);
            this.aboutPanel.Controls.Add(this.label3);
            this.aboutPanel.Controls.Add(this.label4);
            this.aboutPanel.Controls.Add(this.label5);
            this.aboutPanel.Location = new System.Drawing.Point(8, 55);
            this.aboutPanel.Name = "aboutPanel";
            this.aboutPanel.Size = new System.Drawing.Size(342, 132);
            this.aboutPanel.TabIndex = 9;
            this.aboutPanel.Visible = false;
            // 
            // PopupHilfe
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 233);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.aboutPanel);
            this.Controls.Add(this.manualPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PopupHilfe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PopupHilfe";
            this.manualPanel.ResumeLayout(false);
            this.manualPanel.PerformLayout();
            this.aboutPanel.ResumeLayout(false);
            this.aboutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel manualPanel;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label manualLabel;
        private System.Windows.Forms.Panel aboutPanel;
    }
}
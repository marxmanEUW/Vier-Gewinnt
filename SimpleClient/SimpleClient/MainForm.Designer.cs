﻿namespace SimpleClient
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
            this.textIP = new System.Windows.Forms.TextBox();
            this.textPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.textData = new System.Windows.Forms.TextBox();
            this.btnSendData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textIP
            // 
            this.textIP.Location = new System.Drawing.Point(12, 12);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(100, 20);
            this.textIP.TabIndex = 0;
            this.textIP.Text = "IP";
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(12, 46);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(100, 20);
            this.textPort.TabIndex = 1;
            this.textPort.Text = "1333";
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.LightGreen;
            this.btnConnect.Location = new System.Drawing.Point(12, 81);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 45);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // textData
            // 
            this.textData.Location = new System.Drawing.Point(130, 12);
            this.textData.Multiline = true;
            this.textData.Name = "textData";
            this.textData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textData.Size = new System.Drawing.Size(127, 54);
            this.textData.TabIndex = 3;
            this.textData.Text = "Text to send.";
            // 
            // btnSendData
            // 
            this.btnSendData.Enabled = false;
            this.btnSendData.Location = new System.Drawing.Point(130, 81);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(127, 45);
            this.btnSendData.TabIndex = 4;
            this.btnSendData.Text = "Send";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 134);
            this.Controls.Add(this.btnSendData);
            this.Controls.Add(this.textData);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.textIP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox textData;
        private System.Windows.Forms.Button btnSendData;
    }
}


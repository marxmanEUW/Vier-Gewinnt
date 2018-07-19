namespace VierGewinntServer
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
            this.btnStartStop = new System.Windows.Forms.Button();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.lblServerPort = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(12, 12);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(139, 44);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "Server starten";
            this.btnStartStop.UseVisualStyleBackColor = true;
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblServerStatus.Location = new System.Drawing.Point(157, 12);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(139, 44);
            this.lblServerStatus.TabIndex = 1;
            this.lblServerStatus.Text = "Status: OFFLINE";
            this.lblServerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServerIP
            // 
            this.lblServerIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblServerIP.Location = new System.Drawing.Point(302, 12);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(185, 44);
            this.lblServerIP.TabIndex = 2;
            this.lblServerIP.Text = "Server IP: XXX.XXX.XXX.XXX";
            this.lblServerIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServerPort
            // 
            this.lblServerPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblServerPort.Location = new System.Drawing.Point(493, 12);
            this.lblServerPort.Name = "lblServerPort";
            this.lblServerPort.Size = new System.Drawing.Size(139, 44);
            this.lblServerPort.TabIndex = 3;
            this.lblServerPort.Text = "Server Port: XXXXX";
            this.lblServerPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblServerPort);
            this.Controls.Add(this.lblServerIP);
            this.Controls.Add(this.lblServerStatus);
            this.Controls.Add(this.btnStartStop);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vier Gewinnt Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label lblServerStatus;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.Label lblServerPort;
    }
}


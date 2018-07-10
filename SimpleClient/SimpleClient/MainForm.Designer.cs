namespace SimpleClient
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
            this.textPort.Text = "Port";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(130, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(117, 54);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 81);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.textIP);
            this.Name = "MainForm";
            this.Text = "TCP Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Button btnConnect;
    }
}


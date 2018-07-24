namespace SecureClient
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
            this.KlartextLabel1 = new System.Windows.Forms.Label();
            this.KlartextTextbox1 = new System.Windows.Forms.TextBox();
            this.ChiffreTextbox = new System.Windows.Forms.TextBox();
            this.ChiffreLabel = new System.Windows.Forms.Label();
            this.KlartextTextbox2 = new System.Windows.Forms.TextBox();
            this.KlartextLabel2 = new System.Windows.Forms.Label();
            this.VerschluesselnButton = new System.Windows.Forms.Button();
            this.EntschluesselnButton = new System.Windows.Forms.Button();
            this.SchluesselTextbox = new System.Windows.Forms.TextBox();
            this.SchluesselLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // KlartextLabel1
            // 
            this.KlartextLabel1.AutoSize = true;
            this.KlartextLabel1.Location = new System.Drawing.Point(60, 177);
            this.KlartextLabel1.Name = "KlartextLabel1";
            this.KlartextLabel1.Size = new System.Drawing.Size(66, 20);
            this.KlartextLabel1.TabIndex = 0;
            this.KlartextLabel1.Text = "Klartext:";
            // 
            // KlartextTextbox1
            // 
            this.KlartextTextbox1.Location = new System.Drawing.Point(171, 174);
            this.KlartextTextbox1.Name = "KlartextTextbox1";
            this.KlartextTextbox1.Size = new System.Drawing.Size(373, 26);
            this.KlartextTextbox1.TabIndex = 1;
            // 
            // ChiffreTextbox
            // 
            this.ChiffreTextbox.Location = new System.Drawing.Point(171, 225);
            this.ChiffreTextbox.Name = "ChiffreTextbox";
            this.ChiffreTextbox.Size = new System.Drawing.Size(373, 26);
            this.ChiffreTextbox.TabIndex = 3;
            // 
            // ChiffreLabel
            // 
            this.ChiffreLabel.AutoSize = true;
            this.ChiffreLabel.Location = new System.Drawing.Point(60, 228);
            this.ChiffreLabel.Name = "ChiffreLabel";
            this.ChiffreLabel.Size = new System.Drawing.Size(60, 20);
            this.ChiffreLabel.TabIndex = 2;
            this.ChiffreLabel.Text = "Chiffre:";
            // 
            // KlartextTextbox2
            // 
            this.KlartextTextbox2.Location = new System.Drawing.Point(171, 282);
            this.KlartextTextbox2.Name = "KlartextTextbox2";
            this.KlartextTextbox2.Size = new System.Drawing.Size(373, 26);
            this.KlartextTextbox2.TabIndex = 5;
            // 
            // KlartextLabel2
            // 
            this.KlartextLabel2.AutoSize = true;
            this.KlartextLabel2.Location = new System.Drawing.Point(60, 285);
            this.KlartextLabel2.Name = "KlartextLabel2";
            this.KlartextLabel2.Size = new System.Drawing.Size(66, 20);
            this.KlartextLabel2.TabIndex = 4;
            this.KlartextLabel2.Text = "Klartext:";
            // 
            // VerschluesselnButton
            // 
            this.VerschluesselnButton.Location = new System.Drawing.Point(609, 170);
            this.VerschluesselnButton.Name = "VerschluesselnButton";
            this.VerschluesselnButton.Size = new System.Drawing.Size(122, 35);
            this.VerschluesselnButton.TabIndex = 6;
            this.VerschluesselnButton.Text = "Verschlüsseln";
            this.VerschluesselnButton.UseVisualStyleBackColor = true;
            this.VerschluesselnButton.Click += new System.EventHandler(this.VerschluesselnButton_Click);
            // 
            // EntschluesselnButton
            // 
            this.EntschluesselnButton.Location = new System.Drawing.Point(609, 221);
            this.EntschluesselnButton.Name = "EntschluesselnButton";
            this.EntschluesselnButton.Size = new System.Drawing.Size(122, 35);
            this.EntschluesselnButton.TabIndex = 7;
            this.EntschluesselnButton.Text = "Entschlüsseln";
            this.EntschluesselnButton.UseVisualStyleBackColor = true;
            this.EntschluesselnButton.Click += new System.EventHandler(this.EntschluesselnButton_Click);
            // 
            // SchluesselTextbox
            // 
            this.SchluesselTextbox.Location = new System.Drawing.Point(171, 84);
            this.SchluesselTextbox.Name = "SchluesselTextbox";
            this.SchluesselTextbox.Size = new System.Drawing.Size(373, 26);
            this.SchluesselTextbox.TabIndex = 9;
            // 
            // SchluesselLabel
            // 
            this.SchluesselLabel.AutoSize = true;
            this.SchluesselLabel.Location = new System.Drawing.Point(60, 87);
            this.SchluesselLabel.Name = "SchluesselLabel";
            this.SchluesselLabel.Size = new System.Drawing.Size(81, 20);
            this.SchluesselLabel.TabIndex = 8;
            this.SchluesselLabel.Text = "Schlüssel:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 615);
            this.Controls.Add(this.SchluesselTextbox);
            this.Controls.Add(this.SchluesselLabel);
            this.Controls.Add(this.EntschluesselnButton);
            this.Controls.Add(this.VerschluesselnButton);
            this.Controls.Add(this.KlartextTextbox2);
            this.Controls.Add(this.KlartextLabel2);
            this.Controls.Add(this.ChiffreTextbox);
            this.Controls.Add(this.ChiffreLabel);
            this.Controls.Add(this.KlartextTextbox1);
            this.Controls.Add(this.KlartextLabel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label KlartextLabel1;
        private System.Windows.Forms.TextBox KlartextTextbox1;
        private System.Windows.Forms.TextBox ChiffreTextbox;
        private System.Windows.Forms.Label ChiffreLabel;
        private System.Windows.Forms.TextBox KlartextTextbox2;
        private System.Windows.Forms.Label KlartextLabel2;
        private System.Windows.Forms.Button VerschluesselnButton;
        private System.Windows.Forms.Button EntschluesselnButton;
        private System.Windows.Forms.TextBox SchluesselTextbox;
        private System.Windows.Forms.Label SchluesselLabel;
    }
}


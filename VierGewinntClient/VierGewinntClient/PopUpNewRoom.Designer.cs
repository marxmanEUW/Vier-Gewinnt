namespace VierGewinntClient
{
    partial class popupNewRoom
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
            this.labelRaumName = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelRaumName
            // 
            this.labelRaumName.AutoSize = true;
            this.labelRaumName.Location = new System.Drawing.Point(9, 27);
            this.labelRaumName.Name = "labelRaumName";
            this.labelRaumName.Size = new System.Drawing.Size(91, 13);
            this.labelRaumName.TabIndex = 0;
            this.labelRaumName.Text = "Name des Raums";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(106, 24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(166, 24);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(42, 98);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(195, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Abbrechen";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Location = new System.Drawing.Point(42, 69);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(195, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Raum erstellen";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // popupNewRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 134);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.labelRaumName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "popupNewRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Neuen Raum erstellen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRaumName;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
    }
}
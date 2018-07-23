using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalClient
{
    public partial class MainForm : Form
    {
        private Graphics panelGraphics;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.panelGraphics = this.panel1.CreateGraphics();

            this.panelGraphics.DrawRectangle(new Pen(this.panel1.BackColor), new Rectangle(new Point(), this.panel1.Size));
            this.panelGraphics.DrawRectangle(new Pen(Color.Green), new Rectangle(10, 10, 20, 20));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panelGraphics.DrawRectangle(new Pen(Color.Red), new Rectangle(40, 40, 50, 50));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.panelGraphics.DrawRectangle(new Pen(Color.Green), new Rectangle(10, 10, 20, 20));
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginForm Login = new LoginForm();

            DialogResult Result = Login.ShowDialog();

            if(Result == DialogResult.OK)
            {
                MessageBox.Show(String.Format("Success\nServer IP: {0}\nServer Port: {1}\nPlayer Name: {2}", Login.ServerIP, Login.ServerPort, Login.PlayerName),"Info");
            }
            else
            {
                MessageBox.Show("Canceled", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTestGuid_Click(object sender, EventArgs e)
        {
            long ticks = DateTime.Now.Ticks;
            string ClientID = String.Format("{0}-{1}", ticks, Guid.NewGuid().ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VierGewinntServer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region Control Events

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (Connections.StartStopServer())
            {
                UpdateServerStatus();
            }

        }

        #endregion

        #region Private/Helper Methods

        private void UpdateServerStatus()
        {
            if(Connections.CurrentServerStatus == Connections.ServerStatus.Online)
            {
                lblServerStatus.Text = "Status: ONLINE";
                lblServerIP.Text = String.Format("Server IP: {0}", Connections.ServerIP.ToString());
                tBoxIP.Text = Connections.ServerIP.ToString();
                lblServerPort.Text = String.Format("Server Port: {0}", Connections.SERVER_PORT);
                btnStartStop.Text = "Server stoppen";
            }
            else
            {
                lblServerStatus.Text = "Status: OFFLINE";
                lblServerIP.Text = "Server IP: XXX.XXX.XXX.XXX";
                tBoxIP.Text = String.Empty;
                lblServerPort.Text = "Server Port: XXXXX";
                btnStartStop.Text = "Server starten";
            }
        }

        #endregion
    }
}

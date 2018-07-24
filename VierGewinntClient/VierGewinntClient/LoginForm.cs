using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalClient
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        #region Variables

        private const int PORT = 53335;

        public IPAddress ServerIP;
        public int ServerPort = PORT;
        public string PlayerName = String.Empty;

        #endregion

        /*
         * Zum ausrufen:
         * LoginForm x = new LoginForm();
         * DialogResult y = x.ShowDialog(); oder so in der Art
         * if(y.DialogResult == DialogResult.OK) ...
        */

        #region Form Methods

        /// <summary>
        /// Checks if IP is valid. If so, then sets the DialogResult to OK and closes window, else highlights Server IP text field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (textServerIP.Text != String.Empty && textPort.Text != String.Empty && textPlayerName.Text != String.Empty)
            {
                IPAddress IP = CheckIpFormat(textServerIP.Text);

                if (IP != null)
                {
                    ServerIP = IP;
                    ServerPort = int.Parse(textPort.Text);
                    PlayerName = GetUTF8String(textPlayerName.Text);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    textServerIP.BackColor = Color.Crimson;
                }
            }
        }       

        /// <summary>
        /// When text is changed, change color back to normal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textServerIP_TextChanged(object sender, EventArgs e)
        {
            textServerIP.BackColor = SystemColors.Window;
        }

        /// <summary>
        /// When the text field is clicked, select the input for easier change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textServerIP_MouseClick(object sender, MouseEventArgs e)
        {
            textServerIP.SelectAll();
        }

        /// <summary>
        /// Resets variables, sets Dialogresult to Cancel and closes window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //ServerIP = null;
            //ServerPort = PORT;
            //PlayerName = String.Empty;
            //this.DialogResult = DialogResult.Cancel;
            //this.Close();
            Environment.Exit(0);
        }

        #endregion

        #region Private Methods

        private string GetUTF8String(string aString)
        {
            byte[] stringBytes = Encoding.Default.GetBytes(aString);
            return Encoding.UTF8.GetString(stringBytes);
        }

        private IPAddress CheckIpFormat(string aIP)
        {
            IPAddress IP;
            if (IPAddress.TryParse(aIP, out IP))
            {
                return IP;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}

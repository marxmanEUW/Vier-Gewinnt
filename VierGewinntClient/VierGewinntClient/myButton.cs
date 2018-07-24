using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _VierGewinntClient
{
    class myButton : System.Windows.Forms.Button
    {
        public myButton()
        {

            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.MaximumSize = new System.Drawing.Size(40,80);


        }
        public void setColor(System.Drawing.Color color)
        {
            this.BackColor = color;
        }

        public void setTextColor()
        {
            this.ForeColor = this.BackColor;
        }



    }

}

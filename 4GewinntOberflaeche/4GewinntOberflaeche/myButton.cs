using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4GewinntOberflaeche
{
    class myButton : System.Windows.Forms.Button
    {
        public myButton()
        {

            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Click += new EventHandler(Form1.buttonClick);



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

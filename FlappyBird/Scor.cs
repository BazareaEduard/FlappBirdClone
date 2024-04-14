using System;
using System.Windows.Forms;
using System.Drawing;

namespace flappy_bird
{
    class Scor : Label
    {
        public Scor(string text)
        {
            this.Text = text;
            this.Height = 100;
            this.Width = 300;
            this.BackColor = Color.Transparent;
            if (Convert.ToInt32(this.Text) < 10)
                this.Left = 170;
            else
                this.Left = 100;
            this.ForeColor = Color.White;
            this.Font = new Font("Arial", 60);
        }
    }
}

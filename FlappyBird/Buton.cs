using System;
using System.Windows.Forms;
using System.Drawing;

namespace flappy_bird
{
    class Buton : Button
    {
        public Buton(string text, int sus, int stanga, int latime, int inaltime, Color culoare, int border, Bitmap img)
        {
            this.Text = text;
            this.Top = sus;
            this.Left = stanga;
            this.Width = latime;
            this.Height = inaltime;
            this.BackColor = culoare;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize=0;
            if (border == 1)
                this.BackgroundImage = img;

        }
    }
}

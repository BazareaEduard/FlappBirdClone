using System;
using System.Windows.Forms;
using System.Drawing;

namespace flappy_bird
{
    class Panou : Panel
    {
        public Panou(int sus, int stanga, int latime, int inaltime, Bitmap img)
        {
            this.Top = sus;
            this.Left = stanga;
            this.Width = latime;
            this.Height = inaltime;
            this.BackgroundImage = img;
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
        }
    }
}

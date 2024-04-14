using System;
using System.Windows.Forms;
using System.Drawing;

namespace flappy_bird
{
    class LeaderLabel : Label
    {
        public LeaderLabel(int sus, int stanga, int latime, int inaltime)
        {
            this.Top = sus;
            this.Left = stanga;
            this.Width = latime;
            this.Height = inaltime;
        }
    }
}

using System;
using System.Windows.Forms;

namespace flappy_bird
{
    class Cronometru :Timer
    {
        public Cronometru(bool stare, int timp)
        {
            this.Enabled = stare;
            this.Interval = timp;
        }
    }
}

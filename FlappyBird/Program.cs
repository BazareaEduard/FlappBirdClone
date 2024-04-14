using System;
using System.Windows.Forms;
using System.Drawing;
namespace flappy_bird
{
    public class Aplicatie
    {
        public static void Main()
        {
            Fereastra F = new Fereastra("FlappyBird", 10, 10, 400, 639);
            Application.Run(F);
        }
    }
}

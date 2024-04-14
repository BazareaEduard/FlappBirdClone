using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace flappy_bird
{
    class LeaderBoard : Form
    {
        Buton Bt;
        Bitmap Leader1 = new Bitmap(@"poze\leaderboard.png");
        CasetaText insert;
        LeaderLabel Lb;
        string text;
        string[] linie;
        int scor;
        public LeaderBoard (int ok, int score)
        {
            this.Text = "LeaderBoard";
            this.Top = 10;
            this.Left = 10;
            this.Height = 400;
            this.Width = 300;
            this.BackColor = Color.Orange;
            StreamReader input = new StreamReader("LeaderBoard.txt");
            Bt = new Buton("Introdu", 10, 250, 20, 20, Color.Green, 0, Leader1);
            insert = new CasetaText(10, 10, 230, 10);
            Lb = new LeaderLabel(40, 10, 370, 280);
            while(text!=null)
            {
                text = input.ReadLine();
                Lb.Text = text + '\n';
            }
            this.Controls.Add(Lb);
            if (ok == 1)
            {
                this.Controls.Add(Bt);
                this.Controls.Add(insert);
            }
            Bt.Click += new EventHandler(this.Mouse);
            scor = score / 32;
        }
        void Mouse(object sender, EventArgs e)
        {
            Control C = (Control)sender;
            if(C.Text=="Introdu")
            {
                StreamWriter output = File.AppendText("LeaderBoard.txt");
                output.WriteLine(insert.Text.ToString() + "          "  + scor.ToString());
                output.Close();
            }
        }
    }
}

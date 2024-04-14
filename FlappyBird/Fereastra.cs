//THE SCORE MUST ALWAYS BE DIVEDED BY 36!!!
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.IO;
using System.Collections.Generic;
using System.Windows.Media;
namespace flappy_bird
{
    class Fereastra : Form
    {
        Rectangle flappy, pd1, pu1, pd2, pu2, pm1, pm2, bg1, bg2;
        Buton Restart, Leaderboard, Start, LeaderboardOpen;
        bool punct, MoveNow = false;
        int posx = 172, posy = 350, velocity = 4, res = 0, score = 0, dead = 0, index=0;
        Scor scor;
        LeaderBoard Lider;
        Panou GetReady, Medal, FlappyBird;
        Cronometru Tm;
        Cronometru Wings;
        Random Rm;
        string Input_File = @"input.txt";
        string Output_File = @"input.txt";

        //sounds

        MediaPlayer point;
        SoundPlayer fly;
        MediaPlayer hit;
        MediaPlayer die;

        //images

        Bitmap bird = new Bitmap(@"poze\yellowbird-midflap.png");
        Bitmap bg = new Bitmap(@"poze\background.png");
        Bitmap pipu = new Bitmap(@"poze\pipeup.png");
        Bitmap pipd = new Bitmap(@"poze\pipedown.png");
        Bitmap StartImg = new Bitmap(@"poze\start.png");
        Bitmap Resstart = new Bitmap(@"poze\start.png");
        Bitmap Leader1 = new Bitmap(@"poze\leaderboard.png");
        Bitmap Leader2 = new Bitmap(@"poze\leaderboard.png");
        Bitmap Getready = new Bitmap(@"poze\startpanel.png");
        Bitmap Medalie = new Bitmap(@"poze\lost.png");
        Bitmap Flappybird = new Bitmap(@"poze\FlappyBird.png");
        Bitmap GameOver = new Bitmap(@"poze\GameOver.png");
        List<Bitmap> flapps = new List<Bitmap>();
        Bitmap FlappyDown = new Bitmap(@"poze\yellowbird-downflap.png");
        Bitmap FlappyMiddle = new Bitmap(@"poze\yellowbird-midflap.png");
        Bitmap FlappyUp = new Bitmap(@"poze\yellowbird-upflap.png");
        Bitmap bg_tile = new Bitmap(@"poze\background_tile.png");
        Bitmap BronzeM = new Bitmap(@"poze\bronze.png");
        Bitmap SilverM = new Bitmap(@"poze\silver.png");
        Bitmap GoldM = new Bitmap(@"poze\gold.png");
        Bitmap PlatinumM = new Bitmap(@"poze\platinum.png");

        //picturebox

        PictureBox GO = new PictureBox();
        PictureBox Award = new PictureBox();

        //gravity stuff

        const float Gravity = 9.8f;
        static float Speed = 0f;
        static float Mass = 15f;
        static float Acceleration = (Mass / Gravity);
        static float vertical = 350;
        bool MoveDown = true, MoveUp = false;
        float angle = 0f;

        public Fereastra(string text, int sus, int stanga, int latime, int inaltime)
        {

            //form declaration

            this.Top = sus;
            this.Left = stanga;
            this.Width = latime;
            this.Height = inaltime;
            this.BackgroundImage = bg;
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.Paint += new PaintEventHandler(this.Deseneaza);
            this.Paint += new PaintEventHandler(this.Rotate);
            Rm = new Random();

            //bird

            flappy = new Rectangle(posx, posy, 36, 28);

            //game over

            GO.Image = GameOver;
            GO.BackColor = System.Drawing.Color.Transparent;
            GO.Location = new Point(-30, 90);
            GO.Width = 400;
            GO.Height = 100;

            //Medals

            Award.Visible = true;
            Award.BackColor = System.Drawing.Color.Green;
            Award.Location = new Point(100, 300);
            Award.BringToFront();
            Award.Width = 22;
            Award.Height = 22;

            //pipes

            pd1 = new Rectangle(600, 0, 38, 163);
            pu1 = new Rectangle(600, 294, 38, 216);
            pm1 = new Rectangle(600, 163, 30, 131);
            pd2 = new Rectangle(850, 0, 38, 163);
            pu2 = new Rectangle(850, 294, 38, 216);
            pm2 = new Rectangle(850, 163, 30, 131);

            //background

            bg1 = new Rectangle(0, 508, 385, 28);
            bg2 = new Rectangle(386, 508, 385, 28);

            //Buttons

            Restart = new Buton("", 438, 20, 129, 73, System.Drawing.Color.Transparent, 1, Resstart);
            Start = new Buton("", 438, 20, 129, 73, System.Drawing.Color.Transparent, 1, StartImg);
            LeaderboardOpen = new Buton("", 439, 230, 129, 73, System.Drawing.Color.Transparent, 1, Leader2);

            //EventHandlers

            Tm = new Cronometru(true, 1);
            Wings = new Cronometru(true, 50);
            Tm.Tick += new EventHandler(this.Misca);
            Wings.Tick += new EventHandler(this.Flaps);
            Leaderboard = new Buton("", 439, 230, 129, 73, System.Drawing.Color.Transparent, 1, Leader1);
            Leaderboard.Click += new EventHandler(this.Mouse);
            this.KeyPress += new KeyPressEventHandler(this.Apas);
            LeaderboardOpen.Click += new EventHandler(this.Mouse);

            //panels

            GetReady = new Panou(10, 10, 650, 800, Getready);
            Medal = new Panou(250, 47, 300, 160, Medalie);
            Medal.SendToBack();

            //sounds

            point = new MediaPlayer();
            point.Open(new Uri(@"sunete\sfx_point.wav", UriKind.RelativeOrAbsolute));
            fly = new SoundPlayer(@"sunete\sfx_wing.wav");
            hit = new MediaPlayer();
            die = new MediaPlayer();
            scor = new Scor("0");
            FlappyBird = new Panou(10, 10, 500, 300, Flappybird);
            fly.Load();
            hit.Open(new Uri(@"sunete\sfx_hit.wav", UriKind.Relative));
            die.Open(new Uri(@"sunete\sfx_die.wav", UriKind.Relative));

            //function to start the game

            StartGame();

            flapps.Add(FlappyDown);
            flapps.Add(FlappyMiddle);
            flapps.Add(FlappyUp);
            flapps.Add(FlappyMiddle);

            
        }
        public void Deseneaza(object sender, PaintEventArgs e) //adds the pipes on the screen
        {
            e.Graphics.DrawImage(pipd, pd1);
            e.Graphics.DrawImage(pipu, pu1);
            e.Graphics.DrawImage(pipd, pd2);
            e.Graphics.DrawImage(pipu, pu2);
            //e.Graphics.FillRectangle(System.Drawing.Brushes.Orange, pm1);
            //e.Graphics.FillRectangle(System.Drawing.Brushes.Orange, pm2);    //theese are for debugging, will delete later if I don't forget
            //e.Graphics.FillEllipse(System.Drawing.Brushes.Yellow, flappy);
            e.Graphics.DrawImage(bg_tile, bg1);
            e.Graphics.DrawImage(bg_tile, bg2);
        }
        void StartGame() //maniuplation of the first screen buttons
        {
            Controls.Add(Start);
            Controls.Add(LeaderboardOpen);
            Controls.Add(FlappyBird);
            Start.Click += new EventHandler(this.Mouse);
        }
        void Game_over() //controls what happens when flappy hits the pipes or ground
        {

            //sounds to play when dead

            hit.Play();
            die.Play();
            velocity = 0;

            CheckScore();

            //stop flappy from falling once it touches the ground

            if (flappy.Y >= 510)
            {
                MoveNow = false;
                flappy.Y = 510;
            }

            //add the buttons and manipulate them

            Controls.Add(GO);
            Controls.Add(Restart);
            Controls.Add(Leaderboard);
            Restart.Click += new EventHandler(this.Mouse);

            //tells the game that flappy is dead

            dead = 1;

            //the last panel is shown

            this.Controls.Add(Medal);

            //the big score is invisible

            this.Controls.Remove(scor);

            //flappy stops moving his wings

            Wings.Enabled = false;

            //make the medal appear
            if (Convert.ToInt32(scor.Text) >= 0)
                this.Controls.Add(Award);
        }
        public void Misca(object sender, EventArgs e)
        {
            if (MoveNow == true) //checks if flappy is moving
            {
                if (MoveDown == true) //checks if flappy is falling
                {
                    Speed += Acceleration * 0.1f;
                    vertical += Speed;
                    angle += 3;
                }
                if (MoveUp == true) //check if flappy is flying upwards
                {
                    Speed = (Speed - Acceleration * 0.1f) - (1 / Gravity);
                    vertical -= Speed;
                    angle -= 6;
                }
                if (Speed <= 0) //checks if the upwards speed reached 0 in order to fall again
                {
                    MoveDown = true;
                    MoveUp = false;
                }
                flappy.Y = Convert.ToInt32(vertical); //updates the position of flappy

                //move the pipes

                pd1.X -= velocity;
                pu1.X -= velocity;
                pd2.X -= velocity;
                pu2.X -= velocity;
                pm1.X -= velocity;
                pm2.X -= velocity;
                bg1.X -= velocity;
                bg2.X -= velocity;
                if (pd1.X < -84) //checks if the first pipe is out of the screen and moves it forward
                {
                    pd1.Height = Rm.Next(40, 339);
                    pm1.Y = pd1.Height;
                    pu1.Y = pd1.Height + 135;
                    pu1.Height = 510 - pu1.Y;
                    pd1.X = pu1.X = pm1.X = this.ClientSize.Width;

                }
                if (pd2.X < -84) //checks if the secound pipe is out of the screen and moves it forward
                {
                    pd2.Height = Rm.Next(40, 339);
                    pm2.Y = pd2.Height;
                    pu2.Y = pd2.Height + 131;
                    pu2.Height = 510 - pu2.Y;
                    pd2.X = pu2.X = pm2.X = this.ClientSize.Width;
                }
                if(bg1.X<-385)
                {
                    bg1.X = this.ClientSize.Width;
                }
                if (bg2.X < -385)
                {
                    bg2.X = this.ClientSize.Width;
                }
            }

            //collision detection

            if (flappy.IntersectsWith(pu1) || flappy.IntersectsWith(pd1) || flappy.IntersectsWith(pu2) || flappy.IntersectsWith(pd2) || flappy.Y >= 510)
                Game_over();
            if ((flappy.IntersectsWith(pm1) || flappy.IntersectsWith(pm2)) && dead != 1)
            {
                score++;
                int scorsecund = score / 21;
                Console.WriteLine(score);
                scor.Text = scorsecund.ToString();
                if (punct == false) //during the collison with the rectangle that is between pipes, the .IntersectsWith() function is not activated once, but multiple times until flappy is not interacting with the middle rectangle, so punct checks if the collision already happend once and does not let the sound to play more than once
                {
                    point.Play();
                    punct = true;
                    point.Position = TimeSpan.Zero;
                }
            }
            else
                punct = false; //once the collision with the middle rectangle is not happening anymore, punct becomes false in order to become true again once the next interaction is happening

            Invalidate();//necessary for the app to make the images move
        }
        void Apas(object sender, KeyPressEventArgs e) //checks if space is pressed
        {
            if (e.KeyChar == (char)32 && dead!=1)
            {
                fly.Play();
                if (GetReady.Visible == true)
                {
                    GetReady.Visible = false;
                    velocity = 3;
                    Controls.Add(scor);
                    MoveNow = true;
                }
                MoveUp = true;
                MoveDown = false;
                Speed = 5.5f;
            }
        }
        void Mouse(object sender, EventArgs e) //checks the mouse clicks on the buttons
        {
            Control C = (Control)sender;
            if (C.BackgroundImage == Resstart && res == 0)
            {
                res = 1;
                Application.Restart();
            }
            if (C.BackgroundImage == Leader2)
            {
                LeaderBoard lider = new LeaderBoard(1, score);
                lider.Show();
            }
            if (C.BackgroundImage == Leader1)
            {
                LeaderBoard lider = new LeaderBoard(0, score);
                lider.Show();
            }
            if (C.BackgroundImage == StartImg)
            {
                Controls.Remove(Start);
                Controls.Remove(LeaderboardOpen);
                this.Controls.Add(GetReady);
                this.Controls.Remove(FlappyBird);
                flappy.X = 120;
            }
        }
        void Flaps(object sender, EventArgs e) //changes the flappy picture so flappys wings are animated
        {
            if (index == 0)
                index = 1;
            else
                if (index == 1)
                    index = 2;
                else
                    if (index == 2)
                        index = 0;
        }
        void Rotate(object sender, PaintEventArgs e) //makes flappy to rotate
        {
            if (angle >= 90)
                angle = 90;
            if (angle <= -40)
                angle = -40;
            e.Graphics.TranslateTransform((flappy.Left + flappy.Left + flappy.Width) / 2, (flappy.Top + flappy.Top + flappy.Height) / 2);
            e.Graphics.RotateTransform(angle);
            e.Graphics.TranslateTransform(-(flappy.Left + flappy.Left + flappy.Width) / 2, -(flappy.Top + flappy.Top + flappy.Height) / 2);
            e.Graphics.DrawImage(flapps[index], flappy);
        }
        void WriteScore()
        {
            StreamReader input = new StreamReader(Input_File);
            string temp = input.ReadLine();
            input.Close();
            StreamWriter output = new StreamWriter(Output_File);
            if (Convert.ToInt32(scor.Text) > Convert.ToInt32(temp));
                output.WriteLine(scor.Text);
            output.Close();
        }
        void CheckScore()
        {
            if(Convert.ToInt32(scor.Text) >= 0 && Convert.ToInt32(scor.Text) < 20)
            {
                Award.Image = BronzeM;
            }
            if (Convert.ToInt32(scor.Text) >= 20 && Convert.ToInt32(scor.Text) < 30)
            {
                Award.Image = SilverM;
            }
            if (Convert.ToInt32(scor.Text) >= 30 && Convert.ToInt32(scor.Text) < 30)
            {
                Award.Image = GoldM;
            }
            if(Convert.ToInt32(scor.Text) >= 30)
            {
                Award.Image = PlatinumM;
            }
        }
    }
}

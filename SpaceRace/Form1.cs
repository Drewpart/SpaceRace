using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace SpaceRace
{
    public partial class Run : Form
    {
        Rectangle player1 = new Rectangle(120, 300, 20, 20);
        Random randGen = new Random();

        List<Carss> cars = new List<Carss>();

        public static int player1Speed = 2;
        public static int leftLines = 20;
        public static int rightLines = 20;
        public static int ply1Score = 0;
        public static int ply2Score = 0;


        public static string gameState = "waiting";


        public static bool wDown = false;
        public static bool sDown = false;
        public static bool upArrowDown = false;
        public static bool downArrowDown = false;
        public static bool rightArrowDown = false;
        public static bool leftArrowDown = false;

        public static List<Rectangle> leftLine = new List<Rectangle>();
        public static List<Rectangle> rightLine = new List<Rectangle>();
        public static List<int> lineSpeeds = new List<int>();

        public static SolidBrush whiteBrush = new SolidBrush(Color.White);
        public static SolidBrush greenBrush = new SolidBrush(Color.Green);
        public static SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        public static Pen borderPen = new Pen(Color.White, 3);


        public Run()
        {
            InitializeComponent();
        }
        public void GameInitialize()
        {
            titleLabel.Text = "";
            SubTitle.Text = "";


            gameTimer.Enabled = true;
            gameState = "running";
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;


                case Keys.Space:
                    if (gameState == "waiting" || gameState == "over" || gameState == "Win" || gameState == "lost")

                    {

                        GameInitialize();

                    }

                    break;

                case Keys.Escape:

                    if (gameState == "waiting" || gameState == "over" || gameState == "Win" || gameState == "lost")

                    {

                        Application.Exit();

                    }

                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move player 1 
            if (downArrowDown == true && player1.Y > 0)
            {
                player1.Y += player1Speed;
            }

            if (upArrowDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y -= player1Speed;
            }
            if (rightArrowDown == true && player1.Y > 0)
            {
                player1.X += player1Speed;
            }
            if (leftArrowDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X -= player1Speed;
            }

            rightLines--;

            if (rightLines == 0)
            {
                cars.Add(new Carss(0, randGen.Next(2, 250)));
                rightLines = 10;
                //lineSpeeds.Add(randGen.Next(2, 10));
            }

            foreach (Carss car in cars)
            {
                car.Move();
            }

            if (player1.Y == 0)
            {
                gameState = "Win";
                titleLabel.Text = $"Youve Won!!!";


                player1.X = 75;
                player1.Y = 300;
            }

            //for (int i = 0; i < leftLine.Count(); i++)
            //{
            //    int x = leftLine[i].X + lineSpeeds[i];
            //    int Y = 400;
            //    leftLine[i] = new Rectangle(x, leftLine[i].Y, 5, 5);
            //}



            //for (int i = 0; i < rightLine.Count(); i++)
            //{
            //    int x = rightLine[i].X - lineSpeeds[i];
            //    int Y = 300;
            //    rightLine[i] = new Rectangle(x, rightLine[i].Y, 5, 5);
            //}

            //rightLines--;
            //if (rightLines == 0)
            //{
            //    rightLine.Add(new Rectangle(600, randGen.Next(2, 250), 5, 5));
            //    rightLines = 20;
            //    lineSpeeds.Add(randGen.Next(2, 10));
            //}


            for (int i = 0; i < cars.Count(); i++)
            {
                Rectangle carRec = new Rectangle(cars[i].x, cars[i].y, cars[i].size, cars[i].size);
                
                if (player1.IntersectsWith(carRec))
                {
                    player1.X = 120;
                    player1.Y = 300;
                    SoundPlayer ding = new SoundPlayer(Properties.Resources.pong);
                    ding.Play();
                    gameState = "lost";
                    titleLabel.Text = $"YOU SUCK!";



                }
            }


            //for (int i = 0; i < rightLine.Count(); i++)
            //{
            //    if (player1.IntersectsWith(rightLine[i]))
            //    {
            //        player1.X = 120;
            //        player1.Y = 300;
            //        SoundPlayer ding = new SoundPlayer(Properties.Resources.pong);
            //        ding.Play();
            //    }
            //}

            //if(player1.Y == 0)
            //{

            //    player1.X = 120;
            //    player1.Y = 300;



            //}

            //if (ply2Score == 3)
            //{
            //    winLabel.Text = "Player Wins!!";
            //    gameTimer.Enabled = false;
            //    gameState = "Win"; 
            //}
            if (gameState == "Win")
            {
                SubTitle.Text = "Press Esc To Exit";
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            foreach (Carss car in cars)
            {
                e.Graphics.FillRectangle(whiteBrush, car.x, car.y, car.size, car.size);
            }

            e.Graphics.FillRectangle(greenBrush, player1);


            if (gameState == "waiting")

            {

                titleLabel.Text = "Space Race";

                SubTitle.Text = "Press Space Bar to Start";

            }
            else if (gameState == "over")

            {





                titleLabel.Text = "GAME OVER";




                SubTitle.Text += "\nPress Space Bar to Start";

            }
            else if (gameState == "lost")
            {
                titleLabel.Text = "You Have Been HIT!";

                SubTitle.Text += "\nPress Space Bar to Start";
            }
            else if (gameState == "Win")
            {
                titleLabel.Text = "YOUVE WONN!!!";
                SubTitle.Text += "\nPress Space Bar to Start";
            }



        }

        private void Space_Load(object sender, EventArgs e)
        {

        }

        private void SubTitle_Click(object sender, EventArgs e)
        {

        }
    }
}

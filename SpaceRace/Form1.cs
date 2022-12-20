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
        Rectangle player2 = new Rectangle(400, 300, 20, 20);
        Random randGen = new Random();

        List<Carss> cars = new List<Carss>();
        List<rightCars> rCars = new List<rightCars>();

        public static int player1Speed = 2;
        public static int player2Speed = 2;
        public static int leftLines = 20;
        public static int rightLines = 20;
          int ply1Score = 0;
          int ply2Score = 0;


        public static string gameState = "waiting";


        public static bool wDown = false;
        public static bool sDown = false;
        public static bool upArrowDown = false;
        public static bool downArrowDown = false;
        public static bool rightArrowDown = false;
        public static bool leftArrowDown = false;
        public static bool dDown = false;
        public static bool aDown = false;

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
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.A:
                    aDown = true;
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
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.A:
                    aDown = false;
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
            if (downArrowDown == true && player2.Y > 0)
            {
                player2.Y += player2Speed;
            }

            if (upArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y -= player2Speed;
            }
            if (rightArrowDown == true && player2.Y > 0)
            {
                player2.X += player2Speed;
            }
            if (leftArrowDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X -= player2Speed;
            }
            //move player 2
            if (sDown == true && player1.Y > 0)
            {
                player1.Y += player1Speed;
            }

            if (wDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y -= player1Speed;
            }
            if (dDown == true && player1.Y > 0)
            {
                player1.X += player1Speed;
            }
            if (aDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X -= player1Speed;
            }

            int randValue = randGen.Next(1, 100);

            if (randValue < 10)
            {
                cars.Add(new Carss(0, randGen.Next(1, this.Height - 120), 6));
            }
            else if (randValue < 20)
            {
                cars.Add(new Carss(this.Width, randGen.Next(1, this.Height - 120), -6));

            }



            foreach (Carss car in cars)
            {
                car.Move();
            }
            //foreach (rightCars right in rCars)
            //{
            //    right.MoveRight();
            //}

            if (player1.Y == 0)
            {
                ply1Score++;

                p1Score.Text = $"{ply1Score}";

                player1.X = 120;
                player1.Y = 300;
            }
            if (player2.Y == 0)
            {
                ply2Score++;

                p2Score.Text = $"{ply2Score}";

                player2.X = 400;
                player2.Y = 300;
            }



           

            //player 1 intercecting
           
            for (int i = 0; i < cars.Count(); i++)
            {
                Rectangle carRec = new Rectangle(cars[i].x, cars[i].y, cars[i].size, cars[i].size);

                if (player1.IntersectsWith(carRec))
                {
                    player1.X = 120;
                    player1.Y = 300;
                    SoundPlayer ding = new SoundPlayer(Properties.Resources.pong);
                    ding.Play();
                  


                }
            }
            //player 2 intercecting

            for (int i = 0; i < cars.Count(); i++)
            {
                Rectangle carRec = new Rectangle(cars[i].x, cars[i].y, cars[i].size, cars[i].size);

                if (player2.IntersectsWith(carRec))
                {
                    player2.X = 400;
                    player2.Y = 300;
                    SoundPlayer ding = new SoundPlayer(Properties.Resources.pong);
                    ding.Play();
                    



                }
            }



            if (ply1Score == 3)
            {
                winLabel.Text = "Player 1 Wins!!";
                gameTimer.Enabled = false;
                gameState = "Win";
            }
            if (ply2Score == 3)
            {
                winLabel.Text = "Player 2 Wins!!";
                gameTimer.Enabled = false;
                gameState = "Win";
            }
            if (gameState == "Win")
            {
                SubTitle.Text = "Press Esc To Exit";
            }

            Refresh();
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].x < 0 && cars[i].xSpeed > this.Width)
                {
                    cars.Remove(cars[i]);
                }
                else if (cars[i].x > this.Width && cars[i].xSpeed < 0)
                {
                    cars.Remove(cars[i]);
                }
            }
            foreach (Carss car in cars)
            {
                e.Graphics.FillRectangle(whiteBrush, car.x, car.y, car.size, car.size);
                
            }
            foreach (rightCars right in rCars)
            {
                e.Graphics.FillRectangle(whiteBrush, right.x ,right.y, right.size, right.size);
            }

            e.Graphics.FillRectangle(greenBrush, player1);
            e.Graphics.FillRectangle(greenBrush, player2);

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
                titleLabel.Text = "";

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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

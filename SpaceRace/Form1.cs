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
    public partial class Space : Form
    {
        Rectangle player1 = new Rectangle(120, 300, 20, 20);
        Rectangle player2 = new Rectangle(420, 300, 20, 20);
        Rectangle divider = new Rectangle(280, 0, 10, 400);
        Random randGen = new Random();


        int player1Speed = 4;
        int player2Speed = 4;
        int leftLines = 20;
        int rightLines = 20;
       int  ply1Score = 0;
        int ply2Score = 0;

        string gameState = "waiting";


        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;

        List<Rectangle> leftLine = new List<Rectangle>();
        List<Rectangle> rightLine = new List<Rectangle>();
        List<int> lineSpeeds = new List<int>();

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.Gray);
        SolidBrush orangeBrush = new SolidBrush(Color.Orange);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        Pen borderPen = new Pen(Color.White, 3);


        public Space()
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
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move player 1 
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= player1Speed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += player1Speed;
            }

            //move  player 2 
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= player2Speed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += player2Speed;
            }

            for (int i = 0; i < leftLine.Count(); i++)
            {
                int x = leftLine[i].X + lineSpeeds[i];
                int Y = randGen.Next(0, this.Height - leftLines * 2);
                leftLine[i] = new Rectangle(x, leftLine[i].Y, 5, 5);
            }

            leftLines--;
            if (leftLines == 0)
            {
                leftLine.Add(new Rectangle(0, randGen.Next(2, 250), 5, 5));
                leftLines = 20;
                lineSpeeds.Add(randGen.Next(2, 10));
            }

            for (int i = 0; i < rightLine.Count(); i++)
            {
                int x = rightLine[i].X - lineSpeeds[i];
                int Y = randGen.Next(0, this.Height - rightLines * 2);
                rightLine[i] = new Rectangle(x, rightLine[i].Y, 5, 5);
            }

            rightLines--;
            if (rightLines == 0)
            {
                rightLine.Add(new Rectangle(600, randGen.Next(2, 250), 5, 5));
                rightLines = 20;
                lineSpeeds.Add(randGen.Next(2, 10));
            }
            for (int i = 0; i < leftLine.Count(); i++)
            {
                if (player1.IntersectsWith(leftLine[i]))
                {
                    player1.X = 120;
                    player1.Y =300;
                    SoundPlayer ding = new SoundPlayer(Properties.Resources.pong);
                    ding.Play();
                }
            }
            for (int i = 0; i < leftLine.Count(); i++)
            {
                if (player2.IntersectsWith(leftLine[i]))
                {
                    player2.X = 420;
                    player2.Y = 300;
                    SoundPlayer ding = new SoundPlayer(Properties.Resources.pong);
                    ding.Play();
                }
            }
                for (int i = 0; i < rightLine.Count(); i++)
            {
                if (player2.IntersectsWith(rightLine[i]))
                {
                    player2.X = 420;
                    player2.Y = 300;
                    SoundPlayer ding = new SoundPlayer(Properties.Resources.pong);
                    ding.Play();
                }
            }
            for (int i = 0; i < rightLine.Count(); i++)
            {
                if (player1.IntersectsWith(rightLine[i]))
                {
                    player1.X = 120;
                    player1.Y = 300;
                    SoundPlayer ding = new SoundPlayer(Properties.Resources.pong);
                    ding.Play();
                }
            }
            if(player2.Y == 0)
            {
                ply1Score++;
                p1Score.Text = $"{ply1Score}";
                player2.X = 420;
                player2.Y = 300;
                
            }
            if(player1.Y == 0)
            {
                ply2Score++;
                p2Score.Text = $"{ply2Score}";
                player1.X = 120;
                player1.Y = 300;
               


            }

            if (ply2Score == 3)
            {
                winLabel.Text = "Player 1 Wins!";
                gameTimer.Enabled = false;
                gameState = "Win";
            }
            if (ply1Score ==3)
            {
                winLabel.Text = "Player 2 Wins!!";
                gameTimer.Enabled = false;
                gameState = "Win"; 
            }
            if(gameState == "Win")
            {
               SubTitle.Text = "Press Esc To Exit";
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(orangeBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, divider);
            
            for (int i = 0; i < leftLine.Count; i++)
            {
                e.Graphics.FillRectangle(whiteBrush, leftLine[i]);
            }

            for (int i = 0; i < leftLine.Count; i++)
            {
                e.Graphics.FillRectangle(whiteBrush, rightLine[i]);
            }
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

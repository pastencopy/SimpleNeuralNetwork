/*
 *  Snake Game
 * 
 * 
 * 
 *  2020-02-15 Coded by GwangSu Lee
 * 
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NeuralNetworkLibrary;

namespace Snake
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();

        Snake snake;
        Food food;

        int nScore = 0;

        int world_width;
        int world_height;

        Bitmap drawImage;

        Population pop;
        public Form1()
        {
            InitializeComponent();

            drawImage = new Bitmap(picCanvas.Width, picCanvas.Height);
            Graphics.FromImage(drawImage).Clear(Color.White);
            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);
        }

        private void NewGame()
        {
            world_width = picCanvas.Width / Snake.SIZE - 1;
            world_height = picCanvas.Height / Snake.SIZE - 1;

            snake = new Snake(world_width, world_height);
            food = null;
            nScore = 0;
            this.Text = string.Format("Score : {0}", nScore);
            MakeNewFood();
        }

        private void NewGameGenetic()
        {
            world_width = picCanvas.Width / Snake.SIZE - 1;
            world_height = picCanvas.Height / Snake.SIZE - 1;
            food = null;
            nScore = 0;
            MakeNewFood();
        }

        private void MakeNewFood()
        {
            if (food != null && food.eat == false)
                return;

            int x = rnd.Next(0, world_width );
            int y = rnd.Next(0, world_height );

            if (snake != null)
            {
                while (snake.CheckCollide(x, y))
                {
                    x = rnd.Next(0, world_width);
                    y = rnd.Next(0, world_height);
                }
            }
            food = new Food(x, y);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tmrGenetic.Enabled = false;
            NewGame();
            tmrGame.Enabled = true;
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(drawImage);
            g.Clear(Color.White);

            snake.Forward();

            if (snake.Eat(food) == true)
            {
                nScore++;
                this.Text = string.Format("Score : {0}", nScore);
            }

            snake.Draw(g);

            if (food != null)
                food.Draw(g);

            MakeNewFood();

            if (snake.IsDead() == true)
            {
                //GameOver
                tmrGame.Enabled = false;
                this.Text = string.Format("Score : {0} [GAME OVER]", nScore);
            }

            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (tmrGame.Enabled == true)
            {
                //화살표 이동키를 사용
                if (keyData == Keys.NumPad4 || keyData == Keys.Left)
                {
                    snake.Go(Snake.DIRECTION.LEFT);
                }
                else if (keyData == Keys.NumPad6 || keyData == Keys.Right)
                {
                    snake.Go(Snake.DIRECTION.RIGHT);
                }
                else if (keyData == Keys.NumPad8 || keyData == Keys.Up)
                {
                    snake.Go(Snake.DIRECTION.UP);
                }
                else if (keyData == Keys.NumPad5 || keyData == Keys.Down)
                {
                    snake.Go(Snake.DIRECTION.DOWN);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);
        }

        private void btnStartGenetic_Click(object sender, EventArgs e)
        {
            tmrGame.Enabled = false;

            //Genetic Snake
            world_width = picCanvas.Width / Snake.SIZE - 1;
            world_height = picCanvas.Height / Snake.SIZE - 1;
            food = null;
            nScore = 0;

            NewGameGenetic();

            pop = new Population(100, world_width, world_height);
            snake = pop.GetNextSnake();
            tmrGenetic.Enabled = true;
        }


        private void tmrGenetic_Tick(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(drawImage);
            g.Clear(Color.White);

            this.Text = string.Format("Best Score : {0},  Fit:{1:0.000000000}    Gen : {2}  ", pop.bestScore, pop.latestFitness, pop.generation);

            for (int i = 0; i < trackSkip.Value; i++)
            {
                snake.Forward();
                snake.Think(food);
                snake.Eat(food);

                snake.Draw(g);

                if (food != null)
                    food.Draw(g);

                MakeNewFood();

                if (snake.IsDead() == true)
                {
                    if (chkBestSnake.Checked == true && pop.bestSnake != null)
                    {
                        snake = pop.bestSnake.Copy();
                        NewGameGenetic();
                    }
                    else
                    {
                        if (pop.OutOfSnakes() == true)
                        {
                            //Evolution!
                            pop.NextGeneration();
                            NewGameGenetic();
                        }
                        snake = pop.GetNextSnake();
                    }
                }

            }

            picCanvas.CreateGraphics().DrawImageUnscaled(drawImage, 0, 0);
        }

        private void trackSpeed_Scroll(object sender, EventArgs e)
        {
            tmrGenetic.Interval = trackSpeed.Value;
        }

        private void btnApplyBestSnake_Click(object sender, EventArgs e)
        {
            if (pop.bestSnake == null)
            {
                MessageBox.Show("아직 없습니다.");
                return;
            }

            trackSkip.Value = 1;
            tmrGenetic.Enabled = false;
            snake = pop.bestSnake.Copy();
            tmrGenetic.Enabled = true;
        }
    }
}

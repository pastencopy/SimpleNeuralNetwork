using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkLibrary;

namespace Snake
{
    class Snake
    {
        public const int SIZE = 20;
        public const int LIFE = 200;
        public enum DIRECTION
        {
            NONE = 0,
            UP = 1,
            DOWN = 2,
            LEFT = 3,
            RIGHT = 4,
        }

        int x;
        int y;

        int screen_width;
        int screen_height;

        bool dead;
        DIRECTION dir;
        List<Point> tails = new List<Point>();

        public int nScore = 0;

        //Life Fitness
        public int life;
        int alive;

        //VISION
        double[] vision = new double[7];
        public double fitness;
        public NeuralNetwork brain;
        public enum VISION
        {
            FOOD = 0,
            ROAD = 1,
            TAIL = 2,
            WALL = 3,
            
        }
        public Snake(NeuralNetwork target_brain, int screen_width, int screen_height)
        {
            this.brain = target_brain.Copy();
            Init(screen_width, screen_height);
        }

        public Snake Copy()
        {
            return new Snake(this.brain, screen_width, screen_height);
        }


        private void Init( int screen_width, int screen_height)
        {
            this.x = RandomGaussian.Next(3, screen_width - 3);
            this.y = RandomGaussian.Next(3, screen_height - 3);
            this.screen_width = screen_width;
            this.screen_height = screen_height;

            this.dead = false;
            this.dir = DIRECTION.UP;
            this.nScore = 0;


            //Neural초기값
            this.life = LIFE;
            this.alive = 0;
            this.fitness = 0.0;
        }
        public Snake(int screen_width, int screen_height)
        {
            brain = new NeuralNetwork(vision.Length, 10, 3);
            Init(screen_width, screen_height);
        }

        public void Forward()
        {
            if (dead == true) return;

            int oldX = x;
            int oldY = y;

            life--;
            alive++;

            Move(this.dir);
            MoveTail(oldX, oldY);

            if (CheckBound(x, y) || CheckCollideTail(x, y) || life <= 0)
            {
                life = 0;
                dead = true;
                return;
            }

        }
        public void Go(DIRECTION dir)
        {
            if (dead == true) return;

            //잘못된 움직임 예방
            if (((dir == DIRECTION.LEFT && this.dir == DIRECTION.RIGHT)
                 || (dir == DIRECTION.RIGHT && this.dir == DIRECTION.LEFT)
                 )
                 ||
                 ((dir == DIRECTION.UP && this.dir == DIRECTION.DOWN)
                 || (dir == DIRECTION.DOWN && this.dir == DIRECTION.UP)
                 || (dir == DIRECTION.NONE)
                 )
            )
            {

                return;
            }

            this.dir = dir;
        }

        private void MoveTail(int x, int y)
        {
            if (tails.Count == 0) return;

            int oldX = x;
            int oldY = y;

            for (int i = 0; i < tails.Count; i++)
            {
                Point p = tails[i];
                int tmpX = p.X;
                int tmpY = p.Y;
                p.X = oldX;
                p.Y = oldY;
                oldX = tmpX;
                oldY = tmpY;
                tails[i] = p;
            }
        }

        private bool CheckCollideTail(int x, int y)
        {
            foreach (Point p in tails)
            {
                if (p.X == x && p.Y == y)
                {
                    return true;
                }
            }
            return false;
        }


        private void Move(DIRECTION dir)
        {
            this.dir = dir;

            if (dir == DIRECTION.LEFT)
            {
                x--;
            }
            else if (dir == DIRECTION.RIGHT)
            {
                x++;
            }
            else if (dir == DIRECTION.UP)
            {
                y--;
            }
            else if (dir == DIRECTION.DOWN)
            {
                y++;
            }
        }

        private bool CheckBound(int x, int y)
        {
            if (x < 0 || y < 0 || x > screen_width || y > screen_height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsDead()
        {
            return this.dead;
        }
        private void Grow()
        {
            Point p = new Point(this.x, this.y);
            tails.Add(p);
        }

        public bool Eat(Food food)
        {
            if (food == null) return false;
            if (food.eat == true) return false;

            if (this.x == food.x && this.y == food.y)
            {
                nScore++;
                life += LIFE;
                
                food.eat = true;
                Grow();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CheckCollide(int x, int y)
        {
            if (this.x == x && this.y == y)
            {
                return true;
            }

            return CheckCollideTail(x, y);
        }

        public void Draw(Graphics g, bool showVision = false)
        {
            Brush brush = Brushes.Black;
            if (dead == true)
            {
                brush = Brushes.Gray;
            }
            g.FillRectangle(brush, x * SIZE, y * SIZE, SIZE, SIZE);

            foreach (Point child in tails)
            {
                g.FillRectangle(brush, child.X * SIZE, child.Y * SIZE, SIZE, SIZE);
            }

            if (showVision == true)
            {
                DrawVision(g);
            }

        }

        private void DrawVision(Graphics g)
        {
            Brush eyeColor = new SolidBrush(Color.FromArgb(50, Color.Red));

            if (this.dir == DIRECTION.LEFT)
            {
                //     x
                //    x<---
                //     x
                g.FillRectangle(eyeColor, this.x * SIZE, (this.y + 1) * SIZE, SIZE, SIZE); //left
                g.FillRectangle(eyeColor, (this.x - 1) * SIZE, this.y * SIZE, SIZE, SIZE); //up
                g.FillRectangle(eyeColor, this.x * SIZE, (this.y - 1) * SIZE, SIZE, SIZE); //right
            }
            else if (this.dir == DIRECTION.RIGHT)
            {
                //     x
                //    ->x
                //     x
                g.FillRectangle(eyeColor, this.x * SIZE, (this.y - 1) * SIZE, SIZE, SIZE); //left
                g.FillRectangle(eyeColor, (this.x + 1) * SIZE, this.y * SIZE, SIZE, SIZE); //up
                g.FillRectangle(eyeColor, this.x * SIZE, (this.y + 1) * SIZE, SIZE, SIZE); //right
            }
            else if (this.dir == DIRECTION.DOWN)
            {
                //     |
                //    xVx
                //     x
                g.FillRectangle(eyeColor, (this.x + 1) * SIZE, this.y * SIZE, SIZE, SIZE); //left
                g.FillRectangle(eyeColor, this.x * SIZE, (this.y + 1) * SIZE, SIZE, SIZE); //up
                g.FillRectangle(eyeColor, (this.x - 1) * SIZE, this.y * SIZE, SIZE, SIZE); //right
            }
            else
            {
                //     x
                //    x^x
                //     |
                g.FillRectangle(eyeColor, (this.x - 1) * SIZE, this.y * SIZE, SIZE, SIZE); //left
                g.FillRectangle(eyeColor, this.x * SIZE, (this.y - 1) * SIZE, SIZE, SIZE); //up
                g.FillRectangle(eyeColor, (this.x + 1) * SIZE, this.y * SIZE, SIZE, SIZE); //right
            }
        }

        //NeuralNetwork Moving
        public void Think(Food f)
        {
            //Step1 : 현재 진행중인 방향의 상좌우 대각선의 목표물을 확인하여 결정
            if (this.dir == DIRECTION.LEFT)
            {
                //     x
                //    x<---
                //     x

                vision[0] = (double)LookAt(this.x, this.y + 1, f); //left
                vision[1] = (double)LookAt(this.x - 1, this.y, f); //up
                vision[2] = (double)LookAt(this.x, this.y - 1, f); //right
            }
            else if (this.dir == DIRECTION.RIGHT)
            {
                //     x
                //    ->x
                //     x

                vision[0] = (double)LookAt(this.x, this.y - 1, f); //left
                vision[1] = (double)LookAt(this.x + 1, this.y, f); //up
                vision[2] = (double)LookAt(this.x, this.y + 1, f); //right
            }
            else if (this.dir == DIRECTION.DOWN)
            {
                //     |
                //    xVx
                //     x

                vision[0] = (double)LookAt(this.x + 1, this.y, f); //left
                vision[1] = (double)LookAt(this.x, this.y + 1, f); //up
                vision[2] = (double)LookAt(this.x - 1, this.y, f); //right

            }
            else
            {
                //     x
                //    x^x
                //     |

                vision[0] = (double)LookAt(this.x - 1, this.y, f); //left
                vision[1] = (double)LookAt(this.x, this.y - 1, f); //up
                vision[2] = (double)LookAt(this.x + 1, this.y, f); //right
            }
            
            vision[3] = Math.Atan2(y - f.y, x - f.x); // 음식 위치와 이루는 각
            vision[4] = (1 / (Vector2.Distance(new Vector2(f.x, f.y), new Vector2(x, y)) + 1)); //음식과의 거리

            if (tails.Count > 0)
            {
                vision[5] = Math.Atan2(y - tails.Last().Y, x - tails.Last().X); // 마지막 꼬리와 이루는 각
                vision[6] = (1 / (Vector2.Distance(new Vector2(tails.Last().X, tails.Last().Y), new Vector2(x, y)) + 1)); //마지막꼬리와의 거리
            }
            else
            {
                vision[5] = 0;
                vision[6] = 0;
            }

            double[] direction = brain.Predict(vision);
            int k = Array.IndexOf(direction, direction.Max());
            ChangeDirectionFromPredict(k);
        }

        private void ChangeDirectionFromPredict(int k)
        {
            if (this.dir == DIRECTION.UP)
            {
                if (k == 1)
                {
                    Go(DIRECTION.LEFT);
                }
                else if (k == 2)
                {
                    Go(DIRECTION.RIGHT);
                }
            }
            else if (this.dir == DIRECTION.DOWN)
            {
                if (k == 1)
                {
                    Go(DIRECTION.RIGHT);
                }
                else if (k == 2)
                {
                    Go(DIRECTION.LEFT);
                }
            }
            else if (this.dir == DIRECTION.LEFT)
            {
                if (k == 1)
                {
                    Go(DIRECTION.DOWN);
                }
                else if (k == 2)
                {
                    Go(DIRECTION.UP);
                }
            }
            else if (this.dir == DIRECTION.RIGHT)
            {
                if (k == 1)
                {
                    Go(DIRECTION.UP);
                }
                else if (k == 2)
                {
                    Go(DIRECTION.DOWN);
                }
            }
        }


        private double LookAt(int x, int y, Food f)
        {
            if (CheckCollideTail(x, y) == true)
            {
                return -1.0;
            }

            if (CheckBound(x, y) == true)
            {
                return -1.0;
            }

            if (f.x == x && f.y == y)
            {
                return 1.0;
            }

            return 0;
        }

        public void CalcFitness()
        {
            //꼬리 가중치 + 생명력 가중치
            fitness = Math.Pow(this.tails.Count + nScore + 1, 2.0) + life;

            double[] output = brain.Predict(vision);
            
        }

        private double GaussianValue(double v)
        {
            double result = v;
            if (RandomGaussian.NextDouble() <= 0.1)
            {
                result = v + (RandomGaussian.NextGaussian() * 0.5);
            }
            return result;
        }

        public Snake Crossover(Snake partner)
        {
            return new Snake(brain.Crossover(partner.brain),screen_width,screen_height);
        }
        public void Mutate()
        {
            brain.Mutate(GaussianValue);
        }
    }
}
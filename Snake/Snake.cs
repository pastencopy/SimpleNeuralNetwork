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
        public const int MAX_LIFE = 200;
        public enum DIRECTION
        {
            NONE = 0,
            UP = 1,
            DOWN = 2,
            LEFT = 3,
            RIGHT = 4,
        }

        int startX;
        int startY;

        int x;
        int y;
        int screen_width;
        int screen_height;

        bool dead;
        DIRECTION dir;
        List<Point> tails = new List<Point>();


        Snake bestSnake;
        //Life Fitness
        public int life;
        int alive;

        //VISION
        double[] vision = new double[8];
        public double fitness;
        public NeuralNetwork brain;
        public enum VISION
        {
            ROAD,
            FOOD,
            WALL,
            TAIL
        }
        public Snake(NeuralNetwork brain, int x, int y, int screen_width, int screen_height)
        {
            this.brain = brain.Copy();
            Init(x, y, screen_width, screen_height);
        }

        public Snake Copy()
        {
            return new Snake(this.brain, this.startX, this.startY, screen_width, screen_height);
        }


        private void Init(int x, int y, int screen_width, int screen_height)
        {
            this.startX = x;
            this.startY = y;
            this.x = x;
            this.y = y;
            this.screen_width = screen_width;
            this.screen_height = screen_height;

            this.dead = false;
            this.dir = DIRECTION.UP;
            //Neural초기값
            this.life = MAX_LIFE;
            this.alive = 0;
            this.fitness = 0.0;
        }
        public Snake(int x, int y, int screen_width, int screen_height)
        {
            Init(x, y, screen_width, screen_height);

            brain = new NeuralNetwork(8, 10, 3);
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

            //Protect from wrong moving
            if ( ((dir == DIRECTION.LEFT && this.dir == DIRECTION.RIGHT)
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
            int oldX = x;
            int oldY = y;

            for (int i=0;i<tails.Count;i++)
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
            foreach(Point p in tails)
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
            if (x < 1 || y < 1 || x >= screen_width || y >= screen_height)
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
                life += 300;
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

        public void Draw(Graphics g)
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
        }
        
        //NeuralNetwork Moving
        public DIRECTION Think(Food f)
        {
            //Step1 : 현재 진행중인 방향의 상좌우 대각선의 목표물을 확인하여 결정
            life--;

            
            if (this.dir == DIRECTION.LEFT)
            {
                //    xx
                //    x<---
                //    xx

                vision[0] = (int)LookAt(this.x    , this.y + 1, f); //left
                vision[1] = (int)LookAt(this.x - 1, this.y + 1, f); //leftup
                vision[2] = (int)LookAt(this.x - 1, this.y    , f); //up
                vision[3] = (int)LookAt(this.x - 1, this.y - 1, f);  //rightup
                vision[4] = (int)LookAt(this.x    , this.y - 1, f); //right
                vision[5] = (int)DIRECTION.LEFT;
                vision[6] = 1 / Vector2.Distance(new Vector2(f.x,f.y), new Vector2(x, y));
                vision[7] = (this.x - 1) / screen_width;
            }
            else if (this.dir == DIRECTION.RIGHT)
            {
                //     xx
                //    ->x
                //     xx

                vision[0] = (int)LookAt(this.x    , this.y - 1, f); //left
                vision[1] = (int)LookAt(this.x + 1, this.y - 1, f); //leftup
                vision[2] = (int)LookAt(this.x + 1, this.y    , f); //up
                vision[3] = (int)LookAt(this.x + 1, this.y + 1, f);  //rightup
                vision[4] = (int)LookAt(this.x    , this.y + 1, f); //right
                vision[5] = (int)DIRECTION.RIGHT;
                vision[6] = 1 / Vector2.Distance(new Vector2(f.x, f.y), new Vector2(x, y));
                vision[7] = (screen_width - (this.x + 1) ) / screen_width;
            }
            else if (this.dir == DIRECTION.DOWN)
            {
                //     |
                //    xVx
                //    xxx

                vision[0] = (int)LookAt(this.x + 1, this.y    , f); //left
                vision[1] = (int)LookAt(this.x + 1, this.y + 1, f); //leftup
                vision[2] = (int)LookAt(this.x    , this.y + 1, f); //up
                vision[3] = (int)LookAt(this.x - 1, this.y + 1, f); //rightup
                vision[4] = (int)LookAt(this.x - 1, this.y    , f); //right
                vision[5] = (int)DIRECTION.DOWN;
                vision[6] = 1 / Vector2.Distance(new Vector2(f.x, f.y), new Vector2(x, y));
                vision[7] = (screen_height - (this.y + 1)) / screen_height;
            }
            else
            {
                //    xxx
                //    x^x
                //     |

                vision[0] = (int)LookAt(this.x - 1, this.y    , f); //left
                vision[1] = (int)LookAt(this.x - 1, this.y - 1, f); //leftup
                vision[2] = (int)LookAt(this.x    , this.y - 1, f); //up
                vision[3] = (int)LookAt(this.x + 1, this.y - 1, f);  //rightup
                vision[4] = (int)LookAt(this.x + 1, this.y    , f); //right
                vision[5] = (int)DIRECTION.UP;
                vision[6] = 1 / Vector2.Distance(new Vector2(f.x, f.y), new Vector2(x, y));
                vision[7] = (screen_height - (this.y - 1)) / screen_height;
            }

            double[] direction = brain.Predict(vision);

            int k = Array.IndexOf(direction, direction.Max());

            if (this.dir == DIRECTION.UP)
            {
                if (k == 1)
                {
                    return DIRECTION.LEFT;
                }
                else if (k == 2)
                {
                    return DIRECTION.RIGHT;
                }
            }
            else if (this.dir == DIRECTION.DOWN)
            {
                if (k == 1)
                {
                    return DIRECTION.RIGHT;
                }
                else if (k == 2)
                {
                    return DIRECTION.LEFT;
                }
            }
            else if (this.dir == DIRECTION.LEFT)
            {
                if (k == 1)
                {
                    return DIRECTION.DOWN;
                }
                else if (k == 2)
                {
                    return DIRECTION.UP;
                }
            }
            else if (this.dir == DIRECTION.RIGHT)
            {
                if (k == 1)
                {
                    return DIRECTION.UP;
                }
                else if (k == 2)
                {
                    return DIRECTION.DOWN;
                }
            }

            return DIRECTION.NONE;
        }

        private VISION LookAt(int x, int y, Food f)
        {
            if (CheckCollideTail(x,y) == true)
            {
                return VISION.TAIL;
            }

            if (CheckBound(x,y) == true)
            {
                return VISION.WALL;
            }

            if (f.x == x && f.y == y)
            {
                return VISION.FOOD;
            }

            return VISION.ROAD;
        }

        public void CalcFitness()
        {
            //꼬리 가중치 + 생명력 가중치
            fitness = 1 + Math.Pow((this.tails.Count + 1), 3.0) + life + Math.Pow(alive + 1, 2.0);
            
            double[] output = brain.Predict(vision);
            if (this.dead == true)
            {
                //죽었을때 가중치 -
                fitness *= 0.1;
            }
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
        public void Mutate()
        {
            brain.Mutate(GaussianValue);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake
    {
        public const int SIZE = 20;
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

        public Snake(int x, int y, int screen_width, int screen_height)
        {
            this.dead = false;
            this.x = x;
            this.y = y;
            this.screen_width = screen_width;
            this.screen_height = screen_height;

            this.dir = DIRECTION.NONE;
        }

        public void Forward()
        {
            if (dead == true) return;

            int oldX = x;
            int oldY = y;
            
            Move(this.dir);
            MoveTail(oldX, oldY);

            if (CheckBound() || CheckCollideTail(x, y))
            {
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
                 || (dir == DIRECTION.DOWN && this.dir == DIRECTION.UP))
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

        private bool CheckBound()
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
    }
}

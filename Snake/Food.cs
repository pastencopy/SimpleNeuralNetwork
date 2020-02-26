using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Food
    {
        public const int SIZE = 10;

        public int x;
        public int y;

        public bool eat = false;

        public Food(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw(Graphics g)
        {
            if (eat == true) return;

            Brush brush = Brushes.Green;
            g.FillEllipse(brush, x * SIZE, y * SIZE, SIZE, SIZE);

        }
    }
}

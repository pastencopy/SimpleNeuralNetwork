using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRocket
{
    class Block
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public Block(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public bool Collide(Rocket r)
        {
            if ((r.pos.X >= this.x && r.pos.X + r.size < this.x + this.width)
                && (r.pos.Y >= this.y && r.pos.Y + r.size < this.y + this.height)
                )
            {
                return true;
            }
            return false;
        }
        public bool Collide(int x, int y, int width, int height)
        {
            if ((x <= this.x && x + width > this.x + this.width)
                && (y <= this.y && y + height > this.y + this.height)
                )
            {
                return true;
            }
            return false;
        }

        public void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.Black, (float)this.x, (float)this.y, (float)this.width, (float)this.height);
        }

    }
}

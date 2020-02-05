using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRocket
{
    public class Vector2D
    {
        private static Random rnd = new Random();
        public static Vector2 RandomizeVector2(int maxForce)
        {
            double digree = rnd.Next(0, 360);
            float fRadius = rnd.Next(1, maxForce);
            float x = (float)(fRadius * System.Math.Sin(digree * Math.PI / 180));
            float y = (float)(fRadius * System.Math.Cos(digree * Math.PI / 180));

            return new Vector2((float)x, (float)y);
        }
    }
}

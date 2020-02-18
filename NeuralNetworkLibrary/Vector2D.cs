using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class Vector2D
    {
        private static Random rnd = new Random();
        public static Vector2 RandomizeVector2(double maxForce)
        {
            double digree = rnd.Next(0, 360);
            float fRadius = (float)(rnd.NextDouble() * maxForce);
            float x = (float)(fRadius * System.Math.Sin(digree * Math.PI / 180));
            float y = (float)(fRadius * System.Math.Cos(digree * Math.PI / 180));

            return new Vector2((float)x, (float)y);
        }

        public static Vector2 Rotate(Vector2 v, int angle)
        {
            Vector2 result = new Vector2();

            double theta = Math.PI * (angle / 180.0);

            /**
             *  [ cos  -sin ] [x]
             *  [ sin   cos ] [y]
             * */

            result.X = (float)(Math.Cos(theta) * v.X - Math.Sin(theta) * v.Y);
            result.Y = (float)(Math.Sin(theta) * v.X + Math.Cos(theta) * v.Y);

            return result;

        }
    }
}

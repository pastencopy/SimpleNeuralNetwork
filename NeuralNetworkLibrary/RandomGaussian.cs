/*
 *  Random Gaussian
 * 
 *  2020 Coded by Gwangsu Lee
 * 
 * Box-Muller Transformation
 * http://mathworld.wolfram.com/Box-MullerTransformation.html
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class RandomGaussian
    {
        static Random rnd = new Random();
        public static double NextGaussian(double mean = 0.0, double stdDev = 1.0)
        {
            //Box-Muller Transformation

            double x1 = rnd.NextDouble();
            double x2 = rnd.NextDouble();
            double z1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2 * Math.PI * x2);
            double z2 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Sin(2 * Math.PI * x2);
            return mean + (stdDev * (z1 * z2));
        }

        public static double NextDouble()
        {
            return rnd.NextDouble();
        }

        public static int Next()
        {
            return rnd.Next();
        }

        public static int Next(int min, int count)
        {
            return rnd.Next(min, count);
        }
    }
}

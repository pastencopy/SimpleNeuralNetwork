/*
 * Matrix.cs
 * 
 * N by N 행렬 구현
 *  
 * 2020-02-03 Coded by GwangSu Lee 
 * 
 * 
 * Original Reference : https://github.com/CodingTrain/Toy-Neural-Network-JS/blob/master/lib/matrix.js

 * */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace NeuralNetworkLibrary
{
    public class Matrix
    {

        //행렬 데이터
        public int rows;
        public int cols;
        public double[,] data;

        public Matrix(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            this.data = new double[this.rows, this.cols];
        }

        public Matrix Copy()
        {
            Matrix result = new Matrix(this.rows, this.cols);

            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    result.data[i, j] = this.data[i, j];
                }
            }
            return result;
        }

        public void Print()
        {
            for (int i = 0; i < this.rows; i++)
            {
                Console.Write("[");
                for (int j = 0; j < this.cols; j++)
                {
                    Console.Write(string.Format("{0},", this.data[i, j]));
                }
                Console.Write("]\n");
            }
        }

        public static Matrix ConvertFromArray(double[] arr)
        {
            return new Matrix(arr.Length, 1).map((x, i) => arr[i]);
        }

        public static Matrix Substract(Matrix a, Matrix b)
        {
            if (a.rows != b.rows || a.cols != b.cols)
            {
                Console.Error.WriteLine("Columns ans Rows Not Match");
                return null;
            }

            return new Matrix(a.rows, a.cols).map((x, i, j) => a.data[i, j] - b.data[i, j]);
        }

        public static Matrix Transpose(Matrix m)
        {
            return new Matrix(m.cols, m.rows).map((x, i, j) => m.data[j, i]);
        }

        //행렬 곱셈
        public static Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.cols != b.rows)
            {
                Console.Error.WriteLine("Cols 와 Rows가 같아야 곱할 수 있습니다. a x b ");
                return null;
            }

            return new Matrix(a.rows, b.cols).map((x, i, j) =>
            {
                double sum = 0;
                for (int k = 0; k < a.cols; k++)
                {
                    sum += a.data[i, k] * b.data[k, j];
                }
                return sum;
            });
        }

        //행렬에 전부 매핑하는 함수
        public static Matrix map(Matrix m, Func<double, int, int, double> func)
        {
            return new Matrix(m.rows, m.cols).map((x, i, j) => func(m.data[i, j], i, j));
        }

        //행렬 구성분 상수배
        public Matrix Multiply(Matrix m)
        {
            if (this.rows != m.rows || this.cols != m.cols)
            {
                Console.Error.WriteLine("Columns 와 Rows 는 같아야 상수배 곱할 수 있습니다.");
                return null;
            }

            return this.map((x, i, j) => x * m.data[i, j]);
        }
        public Matrix Multiply(double n)
        {
            return this.map(x => x * n);
        }

        public double[] ToArray()
        {
            double[] arr = new double[rows * cols];
            int index = 0;
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    arr[index++] = this.data[i, j];
                }
            }
            return arr;
        }

        public Matrix Add(Matrix m)
        {
            if (this.rows != m.rows || this.cols != m.cols)
            {
                Console.Error.WriteLine("Columns Rows 는 같아야 합니다.");
                return null;
            }
            return this.map((x, i, j) => x + m.data[i, j]);
        }

        public Matrix Add(double n)
        {
            return this.map(x => x + n);
        }

        public Matrix Randomize()
        {            
            return this.map(x => (RandomGaussian.NextDouble() * 2 - 1));
        }

        /**  
         *  Usage : map([Callback function])
         *  Return : Matrix
         * 
         * 
         *  Javascript Array.prototype.map() 을 필요에 맞게 별도로 구현      2020-02-03 Coded by GwangSu Lee 
         *  
         * */
        public Matrix map(Func<double, double> func)
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    this.data[i, j] = func(this.data[i, j]);
                }
            }
            return this;
        }

        public Matrix map(Func<double, int, double> func)
        {
            int index = 0;
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    this.data[i, j] = func(this.data[i, j], index++);
                }
            }
            return this;
        }

        public Matrix map(Func<double, int, int, double> func)
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    double val = this.data[i, j];
                    this.data[i, j] = func(val, i, j);
                }
            }
            return this;
        }

    }
}

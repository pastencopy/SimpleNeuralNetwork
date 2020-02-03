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

namespace SimpleNeuralNetwork
{
    public partial class frmXORexample : Form
    {

        NeuralNetwork nn = new NeuralNetwork(2, 4, 1);

        int train_count = 0;

        public frmXORexample()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // training_data
            //
            // 0 xor 0 = 0
            // 1 xor 0 = 1
            // 0 xor 1 = 1
            // 1 xor 1 = 0
            double[,] training_data = { 
                { 0, 0, 0 }, 
                { 0, 1, 1 },
                { 1, 0, 1 },
                { 1, 1, 0 } 
            };

            Random r = new Random();

            for (int i = 0; i < 1000; i++)
            {
                int index = r.Next() % 4;
                double[] input = { training_data[index, 0], training_data[index, 1] };
                double[] output = { training_data[index, 2] };

                nn.Train(input, output);
            }

            Graphics g = this.CreateGraphics();

            int k = 10;
            int cols = this.Width / 2 / k;
            int rows = this.Height / k;

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    double in1 = (double)(i) / cols;
                    double in2 = (double)(j) / rows;
                    double[] inputs = { in1, in2 };
                    double[] y = nn.Predict(inputs);

                    int val = (int)(y[0] * 255);
                    SolidBrush brush = new SolidBrush(Color.FromArgb(255, val, val, val));

                    g.FillRectangle(brush, i * k, j * k, k, k);
                }
            }

            train_count++;

            this.Text = string.Format("XOR Train Phase {0}", train_count);
        }
    }
}

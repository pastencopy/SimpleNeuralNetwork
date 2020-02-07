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

    public partial class frmGuessWhat : Form
    {

        //Nerual Network
        NeuralNetwork nn;

        //Drawing Mouse Move 
        int pX;
        int pY;

        bool bDrawing = false;
        Bitmap drawing;

        const int IMG_SIZE = 28;

        public frmGuessWhat()
        {
            InitializeComponent();

            // 28x28 byte -> hiddenLayer -> 0 to 9
            nn = new NeuralNetwork(28 * 28, 80, 10);

            drawing = new Bitmap(picCanvas.Width, picCanvas.Height, picCanvas.CreateGraphics());
            Graphics.FromImage(drawing).Clear(Color.White);
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (bDrawing)
            {
                Graphics g = Graphics.FromImage(drawing);
                Pen pen = new Pen(Brushes.Black, 25);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

                g.DrawLine(pen, pX, pY, e.X, e.Y);
                picCanvas.CreateGraphics().DrawImageUnscaled(drawing, new Point(0, 0));
            }

            pX = e.X;
            pY = e.Y;
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            bDrawing = false;
        }
        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                bDrawing = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                //Clear Drawing Canvas!
                Graphics g = Graphics.FromImage(drawing);
                g.Clear(Color.White);
                picCanvas.CreateGraphics().DrawImageUnscaled(drawing, new Point(0, 0));
            }
        }


        private double ColorNormalize(double data)
        {
            double result;

            if (data >= (255 / 2))
            {
                result = 0;
            }
            else
            {
                result = 1;
            }

            return result;
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            int currentX = 0;
            int currentY = 0;

            foreach (MnistImage img in MnistReader.ReadTrainingData(Int32.Parse(txtTraingDataCount.Text)))
            {
                double [] input = new double[img.Data.Length];
                int index = 0;
                for (int i = 0; i < IMG_SIZE; i++)
                {
                    for (int j = 0; j < IMG_SIZE; j++)
                    {
                        double data = img.Data[j, i] ^ 0xFF;
                        data = ColorNormalize(data);
                        input[index++] = data; // row x col
                    }
                }

                double[] output = new double[10];
                output[img.Label] = 1;

                nn.Train(input, output);
                
                //28x28 Image TEST Drawing
                //
                /*
                index = 0;
                for (int i = 0; i < IMG_SIZE; i++)
                {
                    for (int j = 0; j < IMG_SIZE; j++)
                    {
                        byte data = (byte) input[index++];
                        drawing.SetPixel(currentX + i, currentY + j, Color.FromArgb(data, data, data));
                    }
                }

                currentX += IMG_SIZE;

                if (currentX >= picCanvas.Width - IMG_SIZE)
                {
                    currentX = 0;
                    currentY += IMG_SIZE;
                }

                Console.WriteLine(img.Label);

                if (currentY >= picCanvas.Height - IMG_SIZE)
                {
                   break;
                }
                */
            }

            picCanvas.CreateGraphics().DrawImageUnscaled(drawing, new Point(0, 0));

            MessageBox.Show("Train Done.");
            btnTrain.Enabled = false;
        }

        private void btnPredict_Click(object sender, EventArgs e)
        {
            //Resize Bitmap
            Bitmap test = new Bitmap(drawing, IMG_SIZE, IMG_SIZE);

            double[] input = new double[test.Width * test.Height];
            int index = 0;

            for(int i = 0; i< test.Width; i++)
            {
                for (int j = 0; j < test.Height; j++)
                {
                    Color c = test.GetPixel(i, j); //row x col

                    double data = c.R;
                    data = ColorNormalize(data);

                    input[index++] = data;
                }
            }

            double[] output = nn.Predict(input);

            MessageBox.Show(string.Format("예측 : {0}", Array.IndexOf(output, output.Max())));

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            int count = 0;

            foreach (MnistImage img in MnistReader.ReadTestData(3000))
            {
                double[] input = new double[img.Data.Length];
                int index = 0;
                for (int i = 0; i < IMG_SIZE; i++)
                {
                    for (int j = 0; j < IMG_SIZE; j++)
                    {
                        double data = img.Data[j, i] ^ 0xFF;

                        data = ColorNormalize(data);

                        input[index++] = data; // row x col
                    }
                }

                double[] output = nn.Predict(input);

                if (img.Label == Array.IndexOf(output, output.Max()))
                {
                    count++;
                }
            }

            MessageBox.Show(string.Format("정확도 : {0}", (count / 30.0)));
        }

        private void btnManualTrain_Click(object sender, EventArgs e)
        {
            //Resize Bitmap
            Bitmap test = new Bitmap(drawing, IMG_SIZE, IMG_SIZE);

            double[] input = new double[test.Width * test.Height];
            int index = 0;

            for (int i = 0; i < test.Width; i++)
            {
                for (int j = 0; j < test.Height; j++)
                {
                    Color c = test.GetPixel(i, j); //row x col

                    double data = c.R;
                    data = ColorNormalize(data);

                    input[index++] = data;
                }
            }

            index = 0;
            for (int i = 0; i < IMG_SIZE; i++)
            {
                for (int j = 0; j < IMG_SIZE; j++)
                {
                    byte data = (byte)input[index++];
                    drawing.SetPixel(i, j, Color.FromArgb(data, data, data));
                }
            }

            double[] output = new double[10];

            if (txtNumber.Text.Length > 0)
            {
                output[Int32.Parse(txtNumber.Text)] = 1;
                nn.Train(input, output);
                txtNumber.Clear();
                MessageBox.Show("학습완료");
            }
        }
    }
}

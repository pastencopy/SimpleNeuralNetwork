using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace SimpleRocket
{
    public partial class Form1 : Form
    {
        Population m_popul;
        Vector2 m_target;

        Random rnd = new Random();

        System.Drawing.Point m_MouseCapturePoint;
        bool bStart = false;

        Bitmap drawing;

        public Form1()
        {
            InitializeComponent();

            m_target.X = picCanvas.Width/2;
            m_target.Y = 50;

            m_popul = new Population(m_target.X, m_target.Y, //target position
                Int32.Parse(txtRockets.Text),Int32.Parse(txtDNA.Text), //Rockets , DNA
                picCanvas.Width / 2 , picCanvas.Height - 50 //start position
                , picCanvas.Width, picCanvas.Height //Boundary
                );


            drawing = new Bitmap(picCanvas.Width, picCanvas.Height, picCanvas.CreateGraphics());
            Graphics.FromImage(drawing).Clear(Color.White);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bStart = true;
        }


        private void tmrAnimated_Tick(object sender, EventArgs e)
        {
            if (bStart == true)
            {
                for (int i = 0; i < trackBarTimer.Value; i++)
                {
                    m_popul.Run();
                }
            }

            Draw();
        }

        private void Draw()
        {
            Graphics g = Graphics.FromImage(drawing);
            Graphics.FromImage(drawing).Clear(Color.White);

            //Drawing Rockets
            m_popul.Draw(g);

            //Drawing Target
            for (int j = 0; j < 100; j++)
            {
                int i = rnd.Next(0, 360);
                int d = rnd.Next(0, 10);
                g.DrawLine(Pens.Red, m_target.X, m_target.Y,
                    m_target.X + (float)(d * Math.Sin(i * Math.PI / 180)),
                    m_target.Y - (float)(d * Math.Cos(i * Math.PI / 180)));
            }

            picCanvas.CreateGraphics().DrawImageUnscaled(drawing, new System.Drawing.Point(0, 0));

            this.Text = string.Format("{0}세대, Max Fit : {1}", m_popul.generations, m_popul.maxFit);
        }

        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button  == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                m_MouseCapturePoint.X = e.X;
                m_MouseCapturePoint.Y = e.Y;
            }
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_popul.AddBlock(
                    Math.Min(e.X, m_MouseCapturePoint.X),
                    Math.Min(e.Y, m_MouseCapturePoint.Y),
                    Math.Abs(e.X - m_MouseCapturePoint.X),
                    Math.Abs(e.Y - m_MouseCapturePoint.Y)
                    );
            }
            else if (e.Button == MouseButtons.Right)
            {
                m_popul.DeleteBlock(
                    Math.Min(e.X, m_MouseCapturePoint.X),
                    Math.Min(e.Y, m_MouseCapturePoint.Y),
                    Math.Abs(e.X - m_MouseCapturePoint.X),
                    Math.Abs(e.Y - m_MouseCapturePoint.Y)
                    );
            }

            m_MouseCapturePoint.X = 0;
            m_MouseCapturePoint.Y = 0;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            bStart = false;
            m_popul = new Population(m_target.X, m_target.Y, //target position
                Int32.Parse(txtRockets.Text), Int32.Parse(txtDNA.Text), //Rockets , DNA
                picCanvas.Width / 2, picCanvas.Height - 50 //start position
                , picCanvas.Width, picCanvas.Height //Boundary
                );

            picCanvas.CreateGraphics().Clear(Color.White);

            this.Text = "시작을 눌러주세요.";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            bStart = false;
        }
        
        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}

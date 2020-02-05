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

        public Form1()
        {
            InitializeComponent();

            m_target.X = 100;
            m_target.Y = 100;

            m_popul = new Population(m_target.X, m_target.Y, //target position
                100, 100, //Rockets , DNA
                this.picCanvas.Width / 2 , this.picCanvas.Height - 100 //start position
                , this.picCanvas.Width, this.picCanvas.Height //Boundary
                ); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tmrAnimated_Tick(object sender, EventArgs e)
        {
            //Run
            m_popul.Run();


            //Drawing Rockets
            Graphics g = picCanvas.CreateGraphics();
            g.Clear(Color.White);
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
        }
    }
}

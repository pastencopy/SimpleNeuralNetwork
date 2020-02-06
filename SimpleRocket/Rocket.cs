using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Numerics;

namespace SimpleRocket
{   class Rocket
    {
        public string name;
        public int genCounter = 0;

        public DNA dna;

        public double size = 10;
        public float fitness = 0.0F; //피트니스 수치

        double maxSpeed = 10.0; //움직이는 속도 최대값

        public Vector2 pos; //위치
        public Vector2 vel; //속도
        public Vector2 accel; //가속도

        public bool crashed = false;
        public bool success = false;

        private void init(string name, float x, float y)
        {
            this.name = name;
            this.pos.X = x;
            this.pos.Y = y;
        }
        public Rocket(string name, float x, float y, int dnaCount)
        {
            this.init(name, x, y);
            this.dna = new DNA(dnaCount);
        }
        public Rocket(string name, float x, float y, DNA dna)
        {
            this.init(name, x, y);
            this.dna = dna;
        }

        public void ApplyForce(Vector2 f)
        {
            this.accel += f;
        }

        private void Update(Vector2 target)
        {
            //성공/실패한경우 움직이지 않음
            if (this.crashed == true || this.success == true) return;

            float dist = Vector2.Distance(this.pos, target);
            //목표도착
            if (dist <= this.size)
            {
                this.success = true;
                return;
            }

            this.vel += this.accel;
            //최대 속도 제한
            if (this.vel.Length() > this.maxSpeed)
            {
                this.vel -= this.accel;
            }

            this.pos += this.vel;

            this.accel *= 0;
        }


        public void Run(Vector2 target)
        {
            //DNA정보만큼 이동
            if (genCounter < dna.genes.Length)
            {
                this.ApplyForce(dna.genes[genCounter++]);
            }

            this.Update(target);
        }
        public void CheckBoundary(int width, int height)
        {
            if (this.pos.X < 0 || this.pos.X >= width - this.size)
            {
                this.crashed = true;
            }

            if (this.pos.Y < 0 || this.pos.Y >= height - this.size)
            {
                this.crashed = true;
            }

        }

        public void Draw(Graphics g)
        {
            //g.DrawString(this.name, System.Drawing.SystemFonts.DefaultFont, Brushes.Blue, this.pos.X, this.pos.Y);
            g.DrawEllipse(Pens.Black, (float)this.pos.X, (float)this.pos.Y, (float)this.size, (float)this.size);
        }

    }

}

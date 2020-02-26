using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Numerics;

using NeuralNetworkLibrary;

namespace SimpleRocket
{   class Rocket
    {
        public string name;
        public int genCounter = 0;

        public DNA dna;

        public float size = 10;
        public float fitness = 0.0F; //피트니스 수치

        double maxSpeed = 20.0; //움직이는 속도 최대값

        public Vector2 pos; //위치
        public Vector2 vel; //속도
        public Vector2 accel; //가속도

        public bool crashed = false;
        public bool success = false;

        private void init(string name, float x, float y)
        {
            this.name = name;
            this.pos = new Vector2(x, y);
            this.vel = new Vector2(0, 0);
            this.accel = new Vector2(0, 0);
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
            //   기준 위치
            //
            //   Left
            //
            //   ▷ ---> dir
            //
            //   right
            //
            
            Vector2 center = new Vector2(this.pos.X + (size / 2), this.pos.Y + (size / 2));
            Vector2 dir = Vector2.Normalize(vel);
            Vector2 left = new Vector2(-size, size/2);
            Vector2 right = new Vector2(-size, -size/2);

            left = center + Vector2D.RotateFromDirection(left, dir);
            right = center + Vector2D.RotateFromDirection(right, dir);

            Pen pen = new Pen(Color.Black, 1.6F);
       
            g.DrawLine(pen, center.X, center.Y, left.X,left.Y);
            g.DrawLine(pen, center.X, center.Y, right.X, right.Y);
            g.DrawLine(pen, left.X, left.Y, right.X, right.Y);
        }

    }

}

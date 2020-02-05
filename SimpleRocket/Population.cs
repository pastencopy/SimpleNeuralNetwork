using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Numerics;

namespace SimpleRocket
{
    class Population
    {
        public List<Rocket> population = new List<Rocket>();
        public List<Rocket> matingPool = new List<Rocket>();

        public Vector2 start;
        public Vector2 target;

        int generations;
        int dnaCount;
        int rocketCount;
        int lifeCycleCount;

        int worldWidth;
        int worldHeight;


        public void Evaluate()
        {
            float maxFit = 0;
        }
        public void Selection()
        {
            int count = this.rocketCount;

            this.matingPool.Clear();

            while (count >= 0)
            {
                foreach (Rocket r in this.population)
                {
                    Console.WriteLine("{0}", r.fitness * this.rocketCount);
                    for (int i = 0; i < (r.fitness * this.rocketCount); i++)
                    {
                        this.matingPool.Add(r);
                        count--;
                    }
                }
            }

        }

        public void Reprodution()
        {
            //Selection();

            generations++;
            this.lifeCycleCount = this.dnaCount;
            this.population.Clear();

            for (int i = 0; i < this.rocketCount; i++)
            {
                this.population.Add(new Rocket(string.Format("{0}",i), this.start.X, this.start.Y, this.dnaCount));
            }
        }

        public Population(float targetX, float targetY,
            int rocket_count, int dna_count,
            float startX, float startY,
            int world_width, int world_height)
        {
            this.target.X = targetX;
            this.target.Y = targetY;
            this.rocketCount = rocket_count;
            this.start.X = startX;
            this.start.Y = startY;

            this.worldWidth = world_width;
            this.worldHeight = world_height;

            this.dnaCount = dna_count;
            this.lifeCycleCount = this.dnaCount;

            for (int i = 0; i < this.rocketCount; i++)
            {
                this.population.Add(new Rocket(string.Format("{0}",i),this.start.X, this.start.Y, this.dnaCount));
            }
        }

        public void Run()
        {
            foreach (Rocket r in population)
            {
                r.Run(this.target);
                r.CheckBoundary(this.worldWidth, this.worldHeight);
            }

            if (lifeCycleCount-- == 0)
            {

                Reprodution();
                //다음세대 생성!!
            }
        }

        public void Draw(Graphics g)
        {
            foreach (Rocket r in population)
            {
                r.Draw(g);
            }
        }
    }
}

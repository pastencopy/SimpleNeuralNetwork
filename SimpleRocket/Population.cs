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
        private static Random rnd = new Random();

        public List<Block> blocks = new List<Block>();
        public List<Rocket> rockets = new List<Rocket>();
        public List<Rocket> matingPool = new List<Rocket>();

        public Vector2 start;
        public Vector2 target;

        public int generations;
        public float maxFit;

        int dnaCount;
        int rocketCount;
        int lifeCycleCount;

        int worldWidth;
        int worldHeight;



        private float CalculateFitness(Rocket r)
        {
            float dist = Vector2.Distance(r.pos, target);

            //normalize dist
            float fitness = 1 / dist;

            if (r.crashed == true)
            {
                fitness *= 0.01F;
            }
            if (r.success == true)
            {
                fitness *= 10.0F;
            }
            return fitness;
        }

        private void Evaluate()
        {
            float maxOfFitness = 0;
            float sumOffitness = 0;

            for (int i = 0; i < this.rocketCount; i++)
            {
                this.rockets[i].fitness = CalculateFitness(this.rockets[i]);
                sumOffitness += this.rockets[i].fitness;
                if (this.rockets[i].fitness > maxOfFitness)
                    maxOfFitness = this.rockets[i].fitness;
            }
            this.maxFit = maxOfFitness;

            //Normalize Fitness
            for (int i = 0; i < this.rocketCount; i++)
            {
                this.rockets[i].fitness /= sumOffitness;
            }

            this.matingPool.Clear();
            for (int i = 0; i < this.rocketCount; i++)
            {
                int n = (int)(this.rockets[i].fitness * 100);
                for (int j = 0; j < n; j++)
                {
                    this.matingPool.Add(this.rockets[i]);
                }
            }
        }
   
        private void Selection()
        {
            List<Rocket> newRockets = new List<Rocket>();
            int index = 0;
            foreach (Rocket r in this.rockets)
            {
                DNA parentA = this.matingPool[Population.rnd.Next(0, this.matingPool.Count)].dna;
                DNA parentB = this.matingPool[Population.rnd.Next(0, this.matingPool.Count)].dna;
                DNA child = parentA.Crossover(parentB);
                child.Mutate();
                newRockets.Add(new Rocket(string.Format("{0}", index++), start.X, start.Y,child));
            }
            this.rockets = newRockets;
        }

        public void Reprodution()
        {
            generations++;
            this.lifeCycleCount = this.dnaCount;
            Evaluate();
            Selection();
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
                this.rockets.Add(new Rocket(string.Format("{0}",i),this.start.X, this.start.Y, this.dnaCount));
            }
        }

        public void AddBlock(int x, int y, int width ,int height)
        {
            this.blocks.Add(new Block(x, y, width, height));
        }
        public void DeleteBlock(int x, int y, int width, int height)
        {
            if (this.blocks.Count == 0) return;

            for (int i = this.blocks.Count -1 ; i >= 0; i--)
            {
                if (this.blocks[i].Collide(x,y,width,height) == true)
                {
                    this.blocks.RemoveAt(i);
                }
            }
        }

        public void Run()
        {
            foreach (Rocket r in rockets)
            {
                foreach (Block b in blocks)
                {
                    if (b.Collide(r) == true)
                    {
                        r.crashed = true;
                        break;
                    }
                }

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
            
            foreach (Block b in blocks)
            {
                b.Draw(g);
            }

            foreach (Rocket r in rockets)
            {
                r.Draw(g);
            }
        }
    }
}

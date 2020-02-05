using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace SimpleRocket
{
    class DNA
    {
        private Random rnd = new Random();

        private const int MAX_SPEED = 5;

        public Vector2 [] genes;
        public DNA(int num)
        {
            this.genes = new Vector2[num];

            for (int i = 0; i<this.genes.Length; i++)
            {
                this.genes[i] = Vector2D.RandomizeVector2(MAX_SPEED);
            }
        }

        public DNA(Vector2 [] src)
        {
            genes = new Vector2[src.Length];
            src.CopyTo(genes, 0);
        }

        public DNA Crossover(DNA partner)
        {
            Vector2[] newgenes = new Vector2[this.genes.Length];

            //렌덤의 중간값을 기준으로 파트너와 교차
            int mid = rnd.Next(0, this.genes.Length);
            for(int i = 0; i < this.genes.Length; i++)
            {
                newgenes[i] = (i > mid) ? this.genes[i] : partner.genes[i];
            }
            return new DNA(newgenes);
        }

        public void Mutate()
        {
            for(int i = 0; i < this.genes.Length; i++)
            {
                // 1% 확률로 돌연변이
                if (rnd.NextDouble() < 0.01)
                {
                    this.genes[i] = Vector2D.RandomizeVector2(MAX_SPEED);
                }

            }
        }
    }


}

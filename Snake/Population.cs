using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkLibrary;


namespace Snake
{
    class Population
    {
        public int generation = 0;
        public double latestFitness = 0;
                
        public Snake[] snakes;
        public Snake bestSnake;
        public int bestScore = 0;
        
        int index = 0;
        double maxFitness = 0;

        public Population(int numOfSnakes, int screen_width, int screen_height)
        {
            snakes = new Snake[numOfSnakes];

            for (int i = 0; i < numOfSnakes; i++)
            {
                snakes[i] = new Snake(screen_width, screen_height);
            }
        }

        public Snake GetNextSnake()
        {
            return snakes[index++];
        }

        public void UpdateSnake(Snake snake)
        {
            snakes[index - 1] = snake;
        }

        public bool OutOfSnakes()
        {
            if (index >= snakes.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Snake SelectOne()
        {
            int index = 0;

            double rate = RandomGaussian.NextDouble(); // 0 to 1

            while(rate > 0)
            {
                rate -= snakes[index++].fitness;
            }
            
            return snakes[--index].Copy();
        }


        private void PoolGenerate()
        {
            for (int i = 0; i < snakes.Length; i++)
            {
                snakes[i].CalcFitness();
            }

            //Normalize
            double sumOfFitness = 0.0;
            for (int i = 0; i < snakes.Length; i++)
            {
                sumOfFitness += snakes[i].fitness;

                //최고Snake 기록
                if (bestSnake == null) {
                    bestSnake = snakes[i].Copy();
                    bestScore = snakes[i].nScore;
                } 
                else if (bestScore < snakes[i].nScore)
                {
                    bestSnake = snakes[i].Copy();
                    bestScore = snakes[i].nScore;
                }
            }

            //Find Best Parent
            int parentIndex = 0;
            for (int i = 0; i < snakes.Length; i++)
            {
                snakes[i].fitness /= sumOfFitness;

                if (maxFitness <= snakes[i].fitness)
                {
                    maxFitness = snakes[i].fitness;
                    parentIndex = i;
                }
            }

            Snake[] newSnakes = new Snake[snakes.Length];
            Snake parent = snakes[parentIndex].Copy();

            for (int i = 0; i < newSnakes.Length; i++)
            {
                Snake partner = SelectOne();
                newSnakes[i] = parent.Crossover(partner);
                newSnakes[i].Mutate();
            }
            snakes = newSnakes;

        }

        public void NextGeneration()
        {
            PoolGenerate();
            generation++;
            index = 0;
            latestFitness = maxFitness;
            maxFitness = 0;
        }
    }
}

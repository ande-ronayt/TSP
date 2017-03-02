using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_TSP
{
    public class Program
    {
        public static Random r { get; private set; }

        public static int CurId { get; set; } 
        
        static Program(){
            CurId = 1;
        }

        public static void Main()
        {
            r = new Random();

            Tour dest = Tour.random(Env.numCities);
            Population p = Population.randomized(dest, Env.popSize);

            int gen = 0;
            bool better = true;

            while (true /*gen < 100 */ )
            {
                if (better)
                    display(p, gen);

                better = false;
                double oldFit = p.maxFit;

                p = p.evolve();
                if (p.maxFit > oldFit)
                    better = true;

                gen++;
            }
        }

        public static void display(Population p, int gen)
        {
            Tour best = p.findBest();
            System.Console.WriteLine("Generation {0}\n" +
                "Best fitness:  {1}\n" +
                "Best distance: {2}\n" +
            "Tour: {3}", gen, best.fitness, best.distance, best.GetPath());
        }
    }

    public static class Env
    {
        public const double mutRate = 0.03;
        public const int elitism = 6;
        public const int popSize = 60;
        public const int numCities = 20;

        public const int travelers = 3;
    }
}

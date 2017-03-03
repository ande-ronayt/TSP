using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGA
{
    class Program
    {
        public static Random r { get; private set; }
        static void Main(string[] args)
        {
        }
    }

    public static class Env
    {
        public const double mutRate = 0.03;
        public const int elitism = 6;
        public const int popSize = 60;
        public const int numCities = 40;
    }
}

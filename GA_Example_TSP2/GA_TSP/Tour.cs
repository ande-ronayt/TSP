using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_TSP
{
    public class Tour
    {

        // Member variables
        public List<City> t { get; private set; }
        public double distance { get; private set; }
        public double fitness { get; private set; }

        public double balancedFitness{ get; set; }

        public double GetFit(bool balanced)
        {
            if (balanced)
                return balancedFitness;

            return fitness;
        }
        public List<double> distances { get; set; }
        // ctor
        public Tour(List<City> l)
        {
            this.t = l;
            this.distance = this.calcDist();
            this.calcFit();
        }

        // Functionality
        public static Tour random(int n)
        {
            List<City> t = new List<City>();

            for (int i = 0; i < n; ++i)
                t.Add(City.random());

            for (int i = 0; i < Env.travelers; i++)
            {
                t.Add(new City(t[0].x, t[0].y){IsDepo = true});
            }

            t[0].IsDepo = true;
            return new Tour(t);
        }

        public Tour shuffle()
        {
            List<City> tmp = new List<City>(this.t);
            int n = tmp.Count;

            while (n > 1)
            {
                n--;
                int k = Program.r.Next(n)+1;
                City v = tmp[k];
                tmp[k] = tmp[n];
                tmp[n] = v;
            }

            return new Tour(tmp);
        }

        public Tour crossover(Tour m)
        {
            int i = Program.r.Next(1, m.t.Count);
            int j = Program.r.Next(i, m.t.Count);
            List<City> s = this.t.GetRange(i, j - i + 1);
            List<City> ms = m.t.Except(s).ToList();
            List<City> c = ms.Take(i)
                             .Concat(s)
                             .Concat(ms.Skip(i))
                             .ToList();
            return new Tour(c);
        }

        public Tour mutate()
        {
            List<City> tmp = new List<City>(this.t);

            if (Program.r.NextDouble() < Env.mutRate)
            {
                int i = Program.r.Next(1, this.t.Count);
                int j = Program.r.Next(1, this.t.Count);
                City v = tmp[i];
                tmp[i] = tmp[j];
                tmp[j] = v;
            }

            return new Tour(tmp);
        }

        private double calcDist()
        {
            var distances = new List<double>();
            int travelerN = -1;
            double total = 0;
            double temp;
            for (int i = 0; i < this.t.Count; ++i)
            {
                temp = this.t[i].distanceTo(this.t[(i + 1)%this.t.Count]);

                if (t[i].IsDepo)
                {
                    distances.Add(temp);
                    travelerN++;
                }
                else
                {
                    distances[travelerN] += temp;
                }

                total += temp;
            }

            this.distances = distances;

            return total;

            // Execution time is doubled by using linq in this case
            //return this.t.Sum( c => c.distanceTo(this.t[ (this.t.IndexOf(c) + 1) % this.t.Count] ) );
        }

        private void calcFit()
        {

            this.fitness = 1 / this.distance;

            //----Find balanced tour:
            //find dispersion
            //1

            var sredn = this.distance/distances.Count;

            //2
            var tmpSum = 0d;
            for (int i = 0; i < this.distances.Count; i++)
            {
                tmpSum += distances[i]*distances[i];
            }
            tmpSum /= distances.Count;
            //3
            var dispersion2 = tmpSum - sredn*sredn;
            var dispersion = Math.Sqrt((dispersion2));
            double fit = 0;

            this.balancedFitness = 1/dispersion;


        }

        public string GetPath()
        {
            var path = "";
            foreach (var item in t)
            {
                path += item.id + "-";
            }

            return path;
        }

        public string GetDistances()
        {
            var path = "";
            foreach (var item in distances)
            {
                path += item + "-";
            }

            return path;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_TSP
{
    public class City
    {

        // Member variables
        public double x { get; private set; }
        public double y { get; private set; }

        public double id { get; private set; }
        // ctor
        public City(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.id = Program.CurId++; 
        }

        // Functionality
        public double distanceTo(City c)
        {
            if (this.x == c.x && this.y == c.y) return 999;
            return Math.Sqrt(Math.Pow((c.x - this.x), 2)
                            + Math.Pow((c.y - this.y), 2));
        }

        public static City random()
        {
            return new City(Program.r.NextDouble(), Program.r.NextDouble());
        }
    }
}

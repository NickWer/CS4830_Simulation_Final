using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation.Entities
{
    public class MiningSite
    {
        public int oreCount;
        public int distance;
        public Robot activeMiner;
        public bool available = true;

        public MiningSite(int distance, int ore = int.MaxValue)
        {
            oreCount = ore;
            this.distance = distance;
        }
    }
}

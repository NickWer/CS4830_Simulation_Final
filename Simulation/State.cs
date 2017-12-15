using CS4830Final.Simulation.Entities;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation
{
    public class State
    {
        public IEnumerable<Robot> robots;
        public IEnumerable<MiningSite> sites;
        public BaseStation baseStation;
        public SimplePriorityQueue<WorldEvent> eventQueue = new SimplePriorityQueue<WorldEvent>();
        public float time;
    }
}

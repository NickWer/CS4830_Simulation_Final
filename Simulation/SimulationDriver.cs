using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation
{
    public class SimulationDriver
    {
        public Func<State, Boolean> stopCondition;
        public State world;

        public void Run() {
            while(world.eventQueue.Count > 0 && !stopCondition.Invoke(world))
            {
                world.time = world.eventQueue.GetPriority(world.eventQueue.First);
                WorldEvent e = world.eventQueue.Dequeue();
                e.Run(world);
            }
        }
    }
}

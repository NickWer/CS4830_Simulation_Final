using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation
{
    /// <summary>
    /// This class is responsible for actually running the simulation.
    /// The stopCondition function is called after each event, allowing the consumer
    /// to run until they meet the desired conditions to end the simulation - e.g. some time
    /// or some amount of ore mined, etc.
    /// </summary>
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

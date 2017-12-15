using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation
{
    /// <summary>
    /// These are events that the simulation driver will consume. Probably should be an 
    /// interface, but again, I misjudged the utility of an abstract class.
    /// </summary>
    public abstract class WorldEvent
    {
        public abstract void Run(State world);
        public abstract float GetTime();
    }
}

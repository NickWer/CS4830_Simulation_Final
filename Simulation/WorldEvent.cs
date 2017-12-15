using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation
{
    public abstract class WorldEvent
    {
        public abstract void Run(State world);
        public abstract float GetTime();
    }
}

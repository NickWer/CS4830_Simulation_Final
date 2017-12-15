using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation.RandomGenerators
{
    class ConstantGenerator : IRandomGenerator
    {
        public int GetNext()
        {
            return 5;
        }
    }
}

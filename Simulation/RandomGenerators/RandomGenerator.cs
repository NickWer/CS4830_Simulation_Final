using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation.RandomGenerators
{
    /// <summary>
    /// An interface for random number generators that I thought would 
    /// be used far more often than it was.
    /// </summary>
    public interface IRandomGenerator
    {
        int GetNext();
    }
}

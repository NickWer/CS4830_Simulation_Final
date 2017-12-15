using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation.RandomGenerators
{
    class BatteryQualityGenerator : IRandomGenerator
    {
        IEnumerable<int> Frequencies;
        List<float> PMF;
        int offset = 90;
        float max;

        public BatteryQualityGenerator()
        {
            Frequencies = File.ReadAllLines("batteryQualityFreqency.txt").Select(s => int.Parse(s));

            int total = Frequencies.Sum();
            PMF = Frequencies.Select(v => (float)v / total).ToList();
            max = PMF.Max();
        }

        public int GetNext()
        {
            while (true)
            {
                int index = ThreadSafeRandom.getRandomInstance().Next(0, PMF.Count);
                float guess = (float)ThreadSafeRandom.getRandomInstance().NextDouble() * max;
                if (PMF[index] > guess)
                    return index + 91;
            }
        }
    }
}

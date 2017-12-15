using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation.RandomGenerators
{
    /// <summary>
    /// Uses rejection sampling to generate pseudo-random battery qualities based on
    /// a dataset provided in batteryQualityData.txt with the associated frequencies in batteryQualityFrequency.txt
    /// </summary>
    class BatteryQualityGenerator : IRandomGenerator
    {
        IEnumerable<int> Frequencies;
        List<float> PMF;
        int offset = 90;
        float max;

        /// <summary>
        /// Computes the PMF
        /// </summary>
        public BatteryQualityGenerator()
        {
            Frequencies = File.ReadAllLines("batteryQualityFreqency.txt").Select(s => int.Parse(s));

            int total = Frequencies.Sum();
            PMF = Frequencies.Select(v => (float)v / total).ToList();
            max = PMF.Max();
        }


        /// <summary>
        /// Performs the actual rejection sampling
        /// </summary>
        /// <returns>Random numbers with frequencies relative to the distribution of those found
        /// in the data file.</returns>
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final
{
    //Modified from https://blogs.msdn.microsoft.com/pfxteam/2009/02/19/getting-random-numbers-in-a-thread-safe-way/
    public class ThreadSafeRandom
    {
        private static Random _global = new Random();
        [ThreadStatic]
        private static Random _local;

        private static Random getRandomInstance()
        {
            Random inst = _local;
            if (inst == null)
            {
                int seed;
                lock (_global) seed = _global.Next();
                _local = inst = new Random(seed);
            }
            return inst;
        }

        public static int Next()
        {
            return getRandomInstance().Next();
        }

        public static double NextDouble()
        {
            return getRandomInstance().NextDouble();
        }

        //Box muller transform [barely] modified from https://stackoverflow.com/a/218600
        public static double NextNormal(double mean, double stdDev)
        {
            double u1 = 1.0 - NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            return mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
        }

        public static int NextNormalInt(double mean, double stdDev)
        {
            return Convert.ToInt32(NextNormal(mean, stdDev));
        }
    }
}

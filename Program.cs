using CS4830Final.Simulation;
using CS4830Final.Simulation.Entities;
using CS4830Final.Simulation.Events;
using CS4830Final.Simulation.RandomGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final
{
    class Program {
        const int numRuns = 10000;

        /// <summary>
        /// Standard scenario. 15ft/s robots, 500 ore cap, 4 chargers
        /// </summary>
        private static void scenario1()
        {
            Parallel.For(0, numRuns, (i) =>
            {
                BatteryQualityGenerator g = new BatteryQualityGenerator();
                SimulationDriver sim = new SimulationDriver()
                {
                    stopCondition = (state) =>
                    {
                        return state.time >= 3600 * 24 * 365; //One year
                    }
                };
                var robots = new List<Robot>
                 {
                    new Robot(ThreadSafeRandom.NextNormalInt(15, 1), g.GetNext()),
                    new Robot(ThreadSafeRandom.NextNormalInt(15, 1), g.GetNext()),
                    new Robot(ThreadSafeRandom.NextNormalInt(15, 1), g.GetNext()),
                    new Robot(ThreadSafeRandom.NextNormalInt(15, 1), g.GetNext())
                 };
                var sites = new List<MiningSite>
                 {
                    new MiningSite(100),
                    new MiningSite(110),
                    new MiningSite(80),
                    new MiningSite(100)
                 };
                State starting = new State()
                {
                    baseStation = new BaseStation(4),
                    robots = robots,
                    sites = sites
                };

                sim.world = starting;

                robots.ForEach(r =>
                {
                    sim.world.eventQueue.Enqueue(new AttemptChargingEnqueueEvent(r, starting.baseStation, 0), 0);
                });

                sim.Run();
                Console.WriteLine(starting.baseStation.oreMined);
            });
        }

        /// <summary>
        /// Scenario 2 is like scenario 1, but with very slow, fast drilling robots
        /// </summary>
        private static void scenario2()
        {
            Parallel.For(0, numRuns, (i) =>
            {
                BatteryQualityGenerator g = new BatteryQualityGenerator();
                SimulationDriver sim = new SimulationDriver()
                {
                    stopCondition = (state) =>
                    {
                        return state.time >= 3600 * 24 * 365; //One year
                    }
                };
            var robots = new List<Robot>
                 {
                    new Robot(ThreadSafeRandom.NextNormalInt(5, 1), g.GetNext()),
                    new Robot(ThreadSafeRandom.NextNormalInt(5, 1), g.GetNext()),
                    new Robot(ThreadSafeRandom.NextNormalInt(5, 1), g.GetNext()),
                    new Robot(ThreadSafeRandom.NextNormalInt(5, 1), g.GetNext())
                 };
                robots.ForEach(r => { r.AvgMiningTime = 4; r.StdDevMiningTime = 1; });
                var sites = new List<MiningSite>
                 {
                    new MiningSite(100),
                    new MiningSite(110),
                    new MiningSite(80),
                    new MiningSite(100)
                 };
                State starting = new State()
                {
                    baseStation = new BaseStation(4),
                    robots = robots,
                    sites = sites
                };

                sim.world = starting;

                robots.ForEach(r =>
                {
                    sim.world.eventQueue.Enqueue(new AttemptChargingEnqueueEvent(r, starting.baseStation, 0), 0);
                });

                sim.Run();
                Console.WriteLine(starting.baseStation.oreMined);
            });
        }


        /// <summary>
        /// Scenario 3 is like scenario 1, but with only 2 chargers
        /// </summary>
        private static void scenario3()
        {
            Parallel.For(0, numRuns, (i) =>
            {
                BatteryQualityGenerator g = new BatteryQualityGenerator();
                SimulationDriver sim = new SimulationDriver()
                {
                    stopCondition = (state) =>
                    {
                        return state.time >= 3600 * 24 * 365; //One year
                    }
                };
                var robots = new List<Robot>
                 {
                    new Robot(ThreadSafeRandom.NextNormalInt(15, 1), g.GetNext()),
                    new Robot(ThreadSafeRandom.NextNormalInt(15, 1), g.GetNext()),
                    new Robot(ThreadSafeRandom.NextNormalInt(15, 1), g.GetNext()),
                    new Robot(ThreadSafeRandom.NextNormalInt(15, 1), g.GetNext())
                 };
                var sites = new List<MiningSite>
                 {
                    new MiningSite(100),
                    new MiningSite(110),
                    new MiningSite(80),
                    new MiningSite(100)
                 };
                State starting = new State()
                {
                    baseStation = new BaseStation(2),
                    robots = robots,
                    sites = sites
                };

                sim.world = starting;

                robots.ForEach(r =>
                {
                    sim.world.eventQueue.Enqueue(new AttemptChargingEnqueueEvent(r, starting.baseStation, 0), 0);
                });

                sim.Run();
                Console.WriteLine(starting.baseStation.oreMined);
            });
        }

        private static void GenerateBatteryData()
        {
            List<int> batteries = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                batteries.Add(ThreadSafeRandom.getRandomInstance().Next(96, 99));
            }
            for (int i = 0; i < 500; i++)
            {
                batteries.Add(ThreadSafeRandom.NextNormalInt(94.5, 1));
                batteries.Add(ThreadSafeRandom.NextNormalInt(100, 1));
            }
            File.WriteAllLines("batteryQualityData.txt", batteries.Select(b => b.ToString()));
        }

        private static void GenerateNormals()
        {
            List<double> data = new List<double>();
            for (int i = 0; i < 20000; i++)
            {
                data.Add(ThreadSafeRandom.NextNormal(0, 1));
            }
            File.WriteAllLines("normalData.txt", data.Select(b => b.ToString()));
        }

        private static void GeneratePMFData()
        {
            List<int> data = new List<int>();
            BatteryQualityGenerator g = new BatteryQualityGenerator();
            for (int i = 0; i < 1000; i++)
            {
                data.Add(g.GetNext());
            }
            File.WriteAllLines("pmfData.txt", data.Select(b => b.ToString()));
        }
        static void Main(string[] args)
        {
            switch (int.Parse(args[0]))
            {
                case 1: scenario1(); break;
                case 2: scenario2(); break;
                case 3: scenario3(); break;
                case 4: GenerateBatteryData(); GeneratePMFData(); GenerateNormals(); break;
                default: Console.WriteLine("Ya goofed"); break;
            }
        }
    }
}

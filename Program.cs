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
        private static void scenario1()
        {
            Parallel.For(0, 1000, (i) =>
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

        private static void GenerateChargeData()
        {
            List<int> batteries = new List<int>();
        }
        static void Main(string[] args)
        {
            scenario1();
        }
    }
}

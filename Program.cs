using CS4830Final.Simulation;
using CS4830Final.Simulation.Entities;
using CS4830Final.Simulation.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 1000, (i) =>
             {
                 SimulationDriver sim = new SimulationDriver()
                 {
                     stopCondition = (state) =>
                     {
                         return state.time >= 3600 * 24 * 365; //One year
                     }
                 };
                 var robots = new List<Robot>
                 {
                    new Robot(ThreadSafeRandom.NextNormalInt(15, 1), ThreadSafeRandom.NextNormalInt(95, 5)),
                    new Robot(ThreadSafeRandom.NextNormalInt(15, 1), ThreadSafeRandom.NextNormalInt(95, 5)),
                    new Robot(ThreadSafeRandom.NextNormalInt(15, 1), ThreadSafeRandom.NextNormalInt(95, 5)),
                    new Robot(ThreadSafeRandom.NextNormalInt(15, 1), ThreadSafeRandom.NextNormalInt(95, 5))
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
    }
}

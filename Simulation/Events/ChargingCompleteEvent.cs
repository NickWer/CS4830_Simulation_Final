using CS4830Final.Simulation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation.Events
{
    public class ChargingCompleteEvent : WorldEvent
    {
        Robot robot;
        private float time;

        public ChargingCompleteEvent(Robot r, float time)
        {
            robot = r;
            this.time = time;
        }

        public override float GetTime()
        {
            return time;
        }

        public override void Run(State world)
        {
            world.baseStation.chargingStationsInUse -= 1;
            MiningSite site = world.sites.First(s => s.available);
            robot.state = RobotState.inTransit;
            robot.batteryPowerConsumed = 0;
            site.available = false;

            float arrivalTime = world.time + robot.getArrivalTime(site.distance);
            var e = new AtMiningSiteEvent(robot, site, arrivalTime);
            world.eventQueue.Enqueue(e, e.GetTime());
        }
    }
}

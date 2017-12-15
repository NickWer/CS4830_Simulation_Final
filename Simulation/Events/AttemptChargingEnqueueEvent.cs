using CS4830Final.Simulation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation.Events
{
    class AttemptChargingEnqueueEvent : WorldEvent
    {
        Robot robot;
        BaseStation station;
        private float time;

        public AttemptChargingEnqueueEvent(Robot r, BaseStation s, float time)
        {
            robot = r;
            station = s;
            this.time = time;
        }

        public override float GetTime()
        {
            return time;
        }

        public override void Run(State world)
        {
            if (station.chargingStationsInUse == station.maxChargingStations)
            {
                var nextChargingExitEvent = world.eventQueue.First(exitTime => exitTime.GetType().Equals(typeof(ChargingCompleteEvent)));
                var nextAttemptTime = world.eventQueue.GetPriority(nextChargingExitEvent);
                var e = new AttemptChargingEnqueueEvent(robot, station, nextAttemptTime + 1);
                world.eventQueue.Enqueue(e, e.GetTime());
            }
            else
            {
                station.chargingStationsInUse += 1;
                robot.state = RobotState.charging;
                WorldEvent e = new ChargingCompleteEvent(robot, world.time + robot.getChargingTime());
                world.eventQueue.Enqueue(e, e.GetTime());
            }
        }
    }
}

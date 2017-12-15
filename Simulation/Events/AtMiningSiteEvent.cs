using CS4830Final.Simulation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation.Events
{
    class AtMiningSiteEvent : WorldEvent
    {
        Robot robot;
        MiningSite site;
        private float time;

        public AtMiningSiteEvent(Robot r, MiningSite s, float time)
        {
            robot = r;
            site = s;
            this.time = time;
        }

        public override float GetTime()
        {
            return time;
        }

        public override void Run(State world)
        {
            if (site.activeMiner != null)
                throw new InvalidOperationException("There is already an active miner at that site");
            site.activeMiner = robot;
            robot.state = RobotState.mining;
            robot.distanceFromBase = site.distance;
            float freeAt = world.time + robot.getMiningTime();

            OreMiningCompleteEvent e = new OreMiningCompleteEvent(site, robot, freeAt);
            world.eventQueue.Enqueue(e, freeAt);
        }
    }
}

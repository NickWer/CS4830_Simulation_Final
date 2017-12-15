using CS4830Final.Simulation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation.Events
{
    class OreMiningCompleteEvent : WorldEvent
    {
        MiningSite site;
        Robot robot;
        private float time;

        public OreMiningCompleteEvent(MiningSite site, Robot robot, float time)
        {
            this.site = site;
            this.robot = robot;
            this.time = time;
        }

        public override float GetTime()
        {
            return time;
        }

        /// <summary>
        /// This event marks the robot as in transit, clears the active miner from the site and marks
        /// the site available, and then adds an event reflecting how long it will take the robot to
        /// get back to the base camp
        /// </summary>
        /// <param name="world">The current state of the world</param>
        public override void Run(State world)
        {
            robot.state = RobotState.inTransit;
            site.activeMiner = null;
            site.available = true;
            WorldEvent e = new AtBaseEvent(robot, world.baseStation, world.time + robot.getReturnTime());
            world.eventQueue.Enqueue(e, e.GetTime());
        }
    }
}

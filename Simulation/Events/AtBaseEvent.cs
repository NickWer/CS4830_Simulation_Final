﻿using CS4830Final.Simulation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation.Events
{
    class AtBaseEvent : WorldEvent
    {
        Robot robot;
        BaseStation station;
        private float time;

        public AtBaseEvent(Robot robot, BaseStation station, float time)
        {
            this.robot = robot;
            this.station = station;
            this.time = time;
        }

        public override float GetTime()
        {
            return time;
        }

        public override void Run(State world)
        {
            station.oreMined += robot.oreCapacity;
            robot.distanceFromBase = 0;
            AttemptChargingEnqueueEvent e = new AttemptChargingEnqueueEvent(robot, station, world.time + 1);
            world.eventQueue.Enqueue(e, world.time + 1);
        }
    }
}

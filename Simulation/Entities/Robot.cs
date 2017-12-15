using CS4830Final.Simulation.RandomGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation.Entities
{
    public enum RobotState
    {
        inTransit,
        charging,
        mining
    }

    public class Robot
    {
        public int batteryCurrent;
        public int batteryMax;
        public int oreCapacity;
        public int distanceFromBase; //distance from base station
        public RobotState state;
        public int speed;

        public IRandomGenerator miningTimeGenerator = new ConstantGenerator();
        public IRandomGenerator chargingTimeGenerator = new ConstantGenerator();

        public Robot(int speed, int batteryMax, int oreCap = 500)
        {
            this.speed = speed;
            this.batteryMax = batteryMax;
            this.oreCapacity = oreCap;
        }

        public int getMiningTime()
        {
            //TODO: May be able to look up what the sum of normally generated numbers looks like and only generate one number
            int time = 0;
            for (int i = 0; i < oreCapacity; i++)
            {
                time += miningTimeGenerator.GetNext();
            }

            return time;
        }

        public int getReturnTime()
        {
            return distanceFromBase / speed;
        }

        public int getArrivalTime(int distance)
        {
            return distance / speed;
        }

        public int getChargingTime()
        {
            return chargingTimeGenerator.GetNext();
        }
    }
}

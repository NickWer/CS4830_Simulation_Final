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
        public int batteryPowerConsumed;
        public int batteryQuality;
        public int oreCapacity;
        public int distanceFromBase; //distance from base station
        public RobotState state;
        public int speed;
        public int AvgUnloadTime = 10;
        public int StdDevUnloadTime = 3;
        public int AvgMiningTime = 7;
        public int StdDevMiningTime = 3;

        public float loadModifier = 1/1000; //Default to 1/1000, this means that at a full default load, power will be consumed at 1.5x the unloaded speed

        public Robot(int speed, int batteryMax, int oreCap = 500)
        {
            this.speed = speed;
            this.batteryQuality = batteryMax;
            this.oreCapacity = oreCap;
        }

        /// <summary>
        /// Black or white box model - black b/c it doesn't really explain why it takes so long, but
        /// white in that it explains that different mining bits can produce different speeds.
        /// 
        /// Returns the time it takes to mine enough ore to fill the oreCapacity, based on a single unit
        /// of ore requiring AvgMiningTime seconds with a standard deviation of StdDevMiningTime
        /// </summary>
        /// <returns>Time required to mine all ore</returns>
        public int getMiningTime()
        {
            int time = ThreadSafeRandom.NextNormalInt(AvgMiningTime * oreCapacity, StdDevMiningTime * oreCapacity);

            return time;
        }

        /// <summary>
        /// White box model.
        /// Gets the time required to return to the base station, using the equation time = distance / speed.
        /// </summary>
        /// <returns>Return time as a function of distance and speed.</returns>
        public int getReturnTime()
        {
            return distanceFromBase / speed;
        }

        /// <summary>
        /// White box model.
        /// Gets the time required to travel to a particular mining site, as a function of dist and speed.
        /// See also getReturnTime()
        /// </summary>
        /// <param name="distance">Distance of the target mining site</param>
        /// <returns>Time in seconds to get to the target site</returns>
        public int getArrivalTime(int distance)
        {
            return distance / speed;
        }

        /// <summary>
        /// White box model.
        /// Gets the time required to recharge the battery.
        /// Result is based on the battery quality rating and power consumed
        /// </summary>
        /// <returns>Time require to charge</returns>
        public int getChargingTime()
        {
            return 100 / batteryQuality * batteryPowerConsumed;
        }

        /// <summary>
        /// Black Box Model.
        /// Gets the time required to unload the ore, reflecting the fact that
        /// the time is not constant and may vary depending on the geometry of the rocks inside the
        /// bin the robot carries ore in
        /// </summary>
        /// <returns>Time required to unload the ore</returns>
        public int GetUnloadTime()
        {
            return ThreadSafeRandom.NextNormalInt(AvgUnloadTime, StdDevUnloadTime);
        }

        /// <summary>
        /// Black box model.
        /// The load factor is effecitvely a modifier for how much power is consumed by the robot
        /// when it is at various levels of load. In this regard, the power consumed is a white box
        /// model, but the load factor itself a black box model - just a figure ripped from the robot's 
        /// motor spec.
        /// </summary>
        /// <returns>A number that modifies the power consumed based on the capacity of this robot</returns>
        public float GetLoadFactor()
        {
            return loadModifier * this.oreCapacity + 1;
        }
    }
}

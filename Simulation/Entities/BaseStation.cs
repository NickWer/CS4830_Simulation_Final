using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS4830Final.Simulation.Entities
{
    public class BaseStation
    {
        public int chargingStationsInUse;
        public int maxChargingStations;
        public int oreMined;

        public BaseStation(int chargers)
        {
            maxChargingStations = chargers;
        }
    }
}

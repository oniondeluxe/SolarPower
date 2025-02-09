using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Services
{
    /// <summary>
    /// A very blunt calculation implementation of solar power based on weather code and visibility
    /// </summary>
    internal class PowerCalculator
    {
        readonly double _nominalPowerCapacity;
        readonly double _latitude;


        public double GetCurrentPower(string weatherCode, string visibility)
        {
            return 0.0;
        }


        public PowerCalculator(double nominalPowerCapacity, double latitude)
        {
            _nominalPowerCapacity = nominalPowerCapacity;
            _latitude = latitude;
        }
    }
}

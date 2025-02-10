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


        public double GetCurrentPower(int weatherCode, int visibility)
        {
            // Fake science is applied here. Elon Musk will know for sure

            if(weatherCode > 3)
            {
                // Cloudy - no sun
                return 0.0;
            }                       

            var currentVisibiltiy = 10000;
            if(visibility < currentVisibiltiy)
            {
                currentVisibiltiy = visibility;
            }

            return _nominalPowerCapacity * (currentVisibiltiy / 10000.0) * ((90.0 - _latitude) / 90.0);
        }


        public PowerCalculator(double nominalPowerCapacity, double latitude)
        {
            _nominalPowerCapacity = nominalPowerCapacity;
            _latitude = latitude;
        }
    }
}

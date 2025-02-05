using OnionDlx.SolPwr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.BusinessObjects
{
    public class PowerPlant : BusinessObject
    {
        public DateTime UtcInstallDate { get; set; }

        public GeoCoordinate Location { get; set; }
    }
}

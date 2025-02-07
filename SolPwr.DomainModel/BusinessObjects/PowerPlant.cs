using OnionDlx.SolPwr.Data;

namespace OnionDlx.SolPwr.BusinessObjects
{
    public class PowerPlant : BusinessObject
    {
        public string PlantName { get; set; }

        public DateTime UtcInstallDate { get; set; }

        public GeoCoordinate Location { get; set; }

        public double PowerCapacity { get; set; }

        public IList<PowerGenerationRecord> GenerationRecords { get; set; }

        public PowerPlant()
        {
        }
    }


}

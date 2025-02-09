using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Dto
{
    [Serializable]
    public class UnitDescriptor
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("weather_code")]
        public string WeatherCode { get; set; }

        [JsonPropertyName("visibility")]
        public string Visibility { get; set; }

        public UnitDescriptor()
        {
        }
    }


    [Serializable]
    public class ValueDescriptor
    {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; }

        [JsonPropertyName("weather_code")]
        public List<int> WeatherCode { get; set; }

        [JsonPropertyName("visibility")]
        public List<double> Visibility { get; set; }

        public ValueDescriptor()
        {
           // Time = new List<string>();
           // WeatherCode = new List<string>();
           // Visibility = new List<string>();
        }
    }


    [Serializable]
    public class MeteoData : IDataTransferObject
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("generationtime_ms")]
        public double GenerationTime { get; set; }

        [JsonPropertyName("utc_offset_seconds")]
        public int UtcOffset { get; set; }

        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("timezone_abbreviation")]
        public string TimeZoneAbbrev { get; set; }

        [JsonPropertyName("elevation")]
        public double Elevation { get; set; }

        [JsonPropertyName("hourly_units")]
        public UnitDescriptor HourlyUnits { get; set; }

        [JsonPropertyName("hourly")]
        public ValueDescriptor HourlyValues { get; set; }

        public MeteoData()
        {
         //   HourlyUnits = new UnitDescriptor();
          //  HourlyValues = new ValueDescriptor();
        }
    }
}



/*
{
    "latitude": 52.52,
    "longitude": 13.419998,
    "generationtime_ms": 0.07736682891845703,
    "utc_offset_seconds": 0,
    "timezone": "GMT",
    "timezone_abbreviation": "GMT",
    "elevation": 38.0,
    "hourly_units": {
        "time": "iso8601",
        "weather_code": "wmo code",
        "visibility": "m"
    },
    "hourly": {
        "time": [
            "2025-02-09T00:00",
            "2025-02-09T01:00",
            "2025-02-09T02:00",
            "2025-02-09T03:00",
            "2025-02-11T20:00",
            "2025-02-11T21:00",
            "2025-02-11T22:00",
            "2025-02-11T23:00"
        ],
        "weather_code": [
            3,
            2,
            3,
            0,
            0,
            0,
            3,
            3,
            3,
            3,
            2
        ],
        "visibility": [
            28000.00,
            26820.00,
            25860.00,
            66340.00,
            59140.00,
            54020.00,
            50220.00,
            47020.00,
            44380.00
        ]
    }
}
*/
using OnionDlx.SolPwr.Data;
using OnionDlx.SolPwr.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Services
{
    internal static class DataExtensions
    {
        public static IEnumerable<MeteoData> Convert(this IEnumerable<ProviderMeteoData> input)
        {
            foreach (var item in input)
            {
                if (item.Elevation == 0.0)
                {

                }
            }

            yield break;
        }



        //public MeteoData Convert()
        //{
        //    return new MeteoData
        //    {
        //        Location = new GeoCoordinate { Latitude = this.Latitude, Longitude = this.Longitude },
        //        UtcTime = DateTime.UtcNow,
        //        Visibility = 7000,//  this.HourlyValues.Visibility[0],
        //        WeatherCode = 2 // this.HourlyValues.WeatherCode[0]
        //    };
        //}
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnionDlx.SolPwr.Data;
using OnionDlx.SolPwr.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Services
{
    internal class IntegrationEndpoint : IIntegrationEndpoint, IMeteoLookupService
    {
        ILogger<IIntegrationProxy> Logger { get; set; }
        IConfigurationSection ConfigurationSection { get; set; }

        public void Dispose()
        {
        }

        ~IntegrationEndpoint()
        {
        }

        public IntegrationEndpoint()
        {
            // Instantiated by Reflection
        }


        public IMeteoLookupService GetLookupService()
        {
            return this;
        }


        public void Initialize(ILogger<IIntegrationProxy> logger, IConfigurationSection configurationSection)
        {
            Logger = logger;
            ConfigurationSection = configurationSection;
        }


        public async Task<MeteoData> ExecuteAsync(double latitude, double longitude, int dayLapse)
        {            
            var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=weather_code,visibility&forecast_days={dayLapse}";

            // Go to the meteo service
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            var instance = JsonSerializer.Deserialize<List<ProviderMeteoData>>(data);

            // Weather code

            // Visibility

            // Latitude
            return null;
        }


        public Task<MeteoData> GetMeteoDataAsync(GeoCoordinate geoCoordinate, DateTime time)
        {
            return Task.FromResult(new MeteoData { Location = geoCoordinate, Visibility = 9000, WeatherCode = 3 });
        }
    }
}

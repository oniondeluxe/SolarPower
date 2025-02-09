using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnionDlx.SolPwr.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Services
{
    internal class IntegrationEndpoint : IIntegrationEndpoint
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


        public void Initialize(ILogger<IIntegrationProxy> logger, IConfigurationSection configurationSection)
        {
            Logger = logger;
            ConfigurationSection = configurationSection;
        }


        public async Task ExecuteAsync()
        {
            var latitude = 52.52;
            var longitude = 13.41;
            var daylapse = 3;
            var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=weather_code,visibility&forecast_days={daylapse}";

            // Go to the meteo service
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            var instance = JsonSerializer.Deserialize<List<MeteoData>>(data);

            // Weather code

            // Visibility

            // Latitude
        }
    }
}

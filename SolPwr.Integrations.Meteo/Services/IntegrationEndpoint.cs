using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public Task ExecuteAsync()
        {

            return Task.CompletedTask;
        }
    }
}

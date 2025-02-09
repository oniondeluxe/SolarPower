using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnionDlx.SolPwr.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Services
{
    internal class IntegrationProxy : IIntegrationProxy
    {
        IIntegrationEndpoint _endpoint;
        readonly ILogger<IIntegrationProxy> _logger;
        readonly IConfigurationSection _configurationSection;

        public void Initialize(CancellationToken cancellationToken)
        {
            if (_endpoint == null)
            {
                // Find the proper plugin/extension and load it
                var chosenProvider = _configurationSection["Provider"];

                var loader = new PluginLoader(_logger);
                string message;
                if (loader.TryLoadProvider(chosenProvider, _logger, out _endpoint, out message))
                {

                }
                else
                {
                    _logger.LogError(message);
                }
            }
        }


        public void Cleanup()
        {
            _endpoint?.Dispose();
        }


        public IIntegrationEndpoint GetEndpoint()
        {
            // Callback to the meteo service of our choice can be done from here
            return _endpoint;
        }


        public IntegrationProxy(IConfigurationSection configurationSection, ILogger<IIntegrationProxy> logger)
        {
            _configurationSection = configurationSection;
            _logger = logger;
        }
    }
}

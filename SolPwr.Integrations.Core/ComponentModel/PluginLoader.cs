using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnionDlx.SolPwr.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.ComponentModel
{
    class PluginLoader
    {
        readonly ILogger<IIntegrationProxy> _logger;

        public bool TryLoadProvider(string provider, ILogger<IIntegrationProxy> logger, out IIntegrationEndpoint endpoint, out string message)
        {
            // Load the provider
            endpoint = null;
            message = string.Empty;

            return true;
        }


        public PluginLoader(ILogger<IIntegrationProxy> logger)
        {
            _logger = logger;
        }
    }
}

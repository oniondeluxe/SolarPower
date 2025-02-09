using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnionDlx.SolPwr.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.ComponentModel
{
    class PluginLoader
    {
        readonly ILogger<IIntegrationProxy> _logger;
        readonly IConfigurationSection _configurationSection;

        public bool TryLoadProvider(string provider, ILogger<IIntegrationProxy> logger, out IIntegrationEndpoint endpoint, out string message)
        {
            // Load the provider
            endpoint = null;
            message = string.Empty;

            var root = Assembly.GetEntryAssembly().Location;
            var directory = Path.GetDirectoryName(root);
            foreach (var item in Directory.GetFiles(directory, "SolPwr*.dll"))
            {
                var assy = Assembly.LoadFrom(item);
                foreach (var attrib in assy.CustomAttributes)
                {
                    var probe = IntegrationPluginAttribute.FromAssembly(attrib);
                    if (probe != null)
                    {
                        if (probe.PluginIdentifier == provider)
                        {
                            var instance = Activator.CreateInstance(probe.PluginType) as IIntegrationEndpoint;
                            if (instance != null)
                            {
                                // The logger here is different from the logger we used to bootstrap this very loader 
                                instance.Initialize(logger, _configurationSection);
                                endpoint = instance;
                                return true;
                            }
                        }
                    }
                }
            }

            message = $"Provider {provider} not found";
            return false;
        }


        public PluginLoader(ILogger<IIntegrationProxy> logger, IConfigurationSection configurationSection)
        {
            _logger = logger;
            _configurationSection = configurationSection;
        }
    }
}

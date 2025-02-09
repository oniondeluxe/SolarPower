using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.ComponentModel
{
    public class IntegrationPluginAttribute : Attribute
    {
        readonly string _pluginIdentifier;

        public string PluginIdentifier
        {
            get { return _pluginIdentifier; }
        }


        public IntegrationPluginAttribute(string pluginIdentifier, string description)
        {
            _pluginIdentifier = pluginIdentifier;
        }
    }
}

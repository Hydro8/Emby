using System;
using MediaBrowser.Model.Plugins;

namespace Constellation.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public ConstellationOptions[] Options { get; set; }

        public PluginConfiguration()
        {
            Options = new ConstellationOptions[] { };
        }
    }

    public class ConstellationOptions
    {
        public Boolean Enabled { get; set; }
        public String Url { get; set; }
        public int Port { get; set; }
        public String Sentinel { get; set; }
        public String Package { get; set; }
        public String Credential { get; set; }
        public String DeviceId { get; set; }
        public string MediaBrowserUserId { get; set; }
    }

}
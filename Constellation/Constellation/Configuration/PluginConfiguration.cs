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
        public String Port { get; set; }
        public String Token { get; set; }
        public String DeviceId { get; set; }
        public string MediaBrowserUserId { get; set; }
    }

}
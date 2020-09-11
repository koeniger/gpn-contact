using System.IO;
using Microsoft.Extensions.Configuration;

namespace DataBaseUpdater
{
    public class Helpers
    {
        public static IConfiguration ReadConfigFromAppconfig()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(StringResources.AppSettingsFileName);
            return configBuilder.Build();
        }
    }
}

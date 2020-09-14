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

        public static string AddDbRolesScript()
        {
            return
                "do $do$ begin IF NOT EXISTS( " +
                "SELECT FROM pg_catalog.pg_roles WHERE rolname = 'user_db') THEN " +
                "CREATE USER user_db WITH LOGIN PASSWORD 'gfer'; " +
                "ALTER USER user_db CREATEDB; " +
                "END IF; END $do$;";
        }
    }
}

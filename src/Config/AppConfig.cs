using Microsoft.Extensions.Configuration;

namespace API_DB_Tests.Config
{
    public static class AppConfig
    {
        public static IConfigurationRoot Configuration { get; private set; }

        static AppConfig()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }
    }
}

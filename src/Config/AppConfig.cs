using Microsoft.Extensions.Configuration;

namespace API_DB.Config
{
    public static class AppConfig
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public static ErrorMessages ErrorMessages { get; private set; } // ⚡️ This is an instance, not a type

        static AppConfig()
        {
            try
            {
                Configuration = new ConfigurationBuilder()

                     //  .SetBasePath(Directory.GetCurrentDirectory())
                    .SetBasePath(AppContext.BaseDirectory)

                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("errorMessages.json", optional: false, reloadOnChange: true) // ⚡️ Add errorMessages.json
                    .Build();
                ErrorMessages = Configuration.GetSection("ErrorMessages").Get<ErrorMessages>();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(Configuration)
                    .CreateLogger();

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to initialize AppConfig.");
                throw;
            }
            if (Configuration == null)
            {
                throw new Exception("⚠️ appsettings.json не загружен!");
            }

        }
    }
}

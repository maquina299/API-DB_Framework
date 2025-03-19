using Microsoft.Extensions.Configuration;

namespace API_DB.Config
{
    public static class AppConfig
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public static ErrorMessages ErrorMessages { get; private set; } 
        public static string ConnectionString { get; private set; }

        static AppConfig()
        {
            try
            {
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("errorMessages.json", optional: false, reloadOnChange: true)
                    .Build();

                Log.Information("AppSettings JSON: {@Config}", Configuration);

                ErrorMessages = Configuration.GetSection("ErrorMessages").Get<ErrorMessages>() ?? new ErrorMessages();
                Log.Information("Full Configuration: {@Config}", Configuration.AsEnumerable().ToDictionary(k => k.Key, v => v.Value));

                var dbSectionExists = Configuration.GetSection("Database").Exists();
                Log.Information("Database section exists: {Exists}", dbSectionExists);

                var dbConfig = Configuration.GetSection("Database").Get<DatabaseConfig>();
                if (dbConfig == null)
                {
                    Log.Error("Database section is missing or not parsed correctly!");
                    throw new Exception("Database configuration is missing in appsettings.json!");
                }

                Log.Information("Database Config: {@DbConfig}", dbConfig);

                ConnectionString = dbConfig.ConnectionString;

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
                throw new Exception("appsettings.json is not loaded!");
            }
        }


    }
}


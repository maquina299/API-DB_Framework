namespace API_DB.Utils
{
    public static class LoggerSetup
    {
        public static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()    // ✅ Logs to Console
                .WriteTo.File("logs/test_log.txt", rollingInterval: RollingInterval.Day) // ✅ Logs to File
                .CreateLogger();
        }
    }

public static class Logger
    {
        public static void Info(string message) => Log.Information(message);
        public static void Error(string message) => Log.Error(message);
        public static void Debug(string message) => Log.Debug(message);
        public static void Warning(string message) => Log.Warning(message);
    }
}

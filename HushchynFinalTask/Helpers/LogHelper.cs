using Serilog;
using Serilog.Core;
using Xunit.Abstractions;

namespace HushchynFinalTask.Helpers
{
    public static class LogHelper
    {
        private static readonly string LogFilePath = Path.Combine(AppContext.BaseDirectory, "logs", "SauceDemoTests-.log");

        public static ILogger CreateLogger(ITestOutputHelper output)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.TestOutput(output, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .WriteTo.File(LogFilePath,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
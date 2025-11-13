using System.Globalization;
using Serilog;
using Serilog.Events;
using Xunit.Abstractions;

namespace HushchynFinalTask.Helpers
{
    public static class LogHelper
    {
        private static readonly string LogFilePath = Path.Combine(AppContext.BaseDirectory, "logs", "HushchynFinalTask-.log");

        public static ILogger CreateLogger(ITestOutputHelper output)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.TestOutput(
                    output,
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    formatProvider: CultureInfo.InvariantCulture)
                .WriteTo.File(
                    path: LogFilePath,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    formatProvider: CultureInfo.InvariantCulture)
                .CreateLogger();
        }
    }
}

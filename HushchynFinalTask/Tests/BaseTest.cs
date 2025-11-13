using HushchynFinalTask.Drivers;
using HushchynFinalTask.Helpers;
using Serilog;
using Xunit.Abstractions;

namespace HushchynFinalTask.Tests
{
    public abstract class BaseTest : IDisposable
    {
        protected readonly ILogger _log;
        protected readonly DriverManager _driverManager;

        private bool _disposed = false;

        protected BaseTest(ITestOutputHelper output)
        {
            _log = LogHelper.CreateLogger(output);
            _driverManager = new DriverManager();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing && _driverManager != null)
                {
                        _driverManager.QuitDriver();
                        _log.Information("Browser closed.");
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseTest()
        {
            Dispose(false);
        }
    }
}

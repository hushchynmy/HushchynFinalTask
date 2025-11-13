using HushchynFinalTask.Drivers;
using HushchynFinalTask.Helpers;
using Serilog;
using Xunit.Abstractions;

namespace HushchynFinalTask.Tests
{
    public abstract class BaseTest(ITestOutputHelper output)
        : IDisposable
    {
        private bool disposed;

        protected ILogger Log { get; } = LogHelper.CreateLogger(output);

        protected DriverManager DriverManager { get; } = new DriverManager();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.DriverManager?.QuitDriver();
                this.Log.Information("Browser closed.");
            }

            this.disposed = true;
        }
    }
}

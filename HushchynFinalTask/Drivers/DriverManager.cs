using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace HushchynFinalTask.Drivers
{
    public class DriverManager
    {

        private IWebDriver? _driver;

        public IWebDriver GetDriver(BrowserType browserType)
        {
            if (_driver == null)
            {
                _driver = CreateDriver(browserType);
                _driver.Manage().Window.Maximize();
            }
            return _driver;
        }

        public void QuitDriver()
        {
            if (_driver != null)
            {
                try
                {
                    _driver.Quit();
                    _driver.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error quitting driver: {ex.Message}");
                }
                finally
                {
                    _driver = null;
                }
            }
        }

        private static IWebDriver CreateDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    var chromeOptions = new ChromeOptions();
                    return new ChromeDriver(chromeOptions);

                case BrowserType.Firefox:
                    var firefoxOptions = new FirefoxOptions();
                    return new FirefoxDriver(firefoxOptions);

                default:
                    throw new ArgumentOutOfRangeException(nameof(browserType), browserType, "Unsupported browser type.");
            }
        }
    }
}
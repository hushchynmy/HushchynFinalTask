using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace HushchynFinalTask.Drivers
{
    public class DriverManager
    {
        private IWebDriver? driver;

        public IWebDriver GetDriver(BrowserType browserType)
        {
            if (this.driver == null)
            {
                this.driver = CreateDriver(browserType);
                this.driver.Manage().Window.Maximize();
            }

            return this.driver;
        }

        public void QuitDriver()
        {
            if (this.driver != null)
            {
                try
                {
                    this.driver.Quit();
                    this.driver.Dispose();
                }
                catch (WebDriverException ex)
                {
                    Console.WriteLine($"Error quitting driver: {ex.Message}");
                }
                finally
                {
                    this.driver = null;
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

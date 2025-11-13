using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;

namespace HushchynFinalTask.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; }
        protected ILogger Log { get; }
        protected WebDriverWait Wait { get; }

        protected BasePage(IWebDriver driver, ILogger logger)
        {
            Driver = driver;
            Log = logger;
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        protected void ClickWhenReady(By locator)
        {
            try
            {
                Log.Debug($"Waiting for element to be clickable: {locator}");

                IWebElement element = Wait.Until(driver =>
                {
                    try
                    {
                        var elem = driver.FindElement(locator);
                        if (elem != null && elem.Displayed && elem.Enabled)
                        {
                            return elem;
                        }
                        return null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });

                element.Click();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to click element: {locator}");
                throw;
            }
        }

        private IWebElement WaitForElementVisible(By locator)
        {
            return Wait.Until(driver =>
            {
                try
                {
                    var elem = driver.FindElement(locator);

                    if (elem != null && elem.Displayed)
                    {
                        return elem;
                    }
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
        }

        protected void SendKeysWhenReady(By locator, string text)
        {
            try
            {
                Log.Debug($"Waiting for element to be visible: {locator}");
                var element = WaitForElementVisible(locator);
                element.Clear();
                element.SendKeys(text);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to enter text '{text}' into element: {locator}");
                throw;
            }
        }

        protected void ClearInput(By locator)
        {
            var element = WaitForElementVisible(locator);
            element.Click();
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
            Wait.Until(d => d.FindElement(locator).GetAttribute("value") == "");

            Log.Debug($"Field {locator} cleared using SendKeys.");
        }

        protected string GetTextWhenVisible(By locator)
        {
            try
            {
                Log.Debug($"Waiting for text from element: {locator}");
                var element = WaitForElementVisible(locator);
                return element.Text;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to get text from element: {locator}");
                throw;
            }
        }
    }
}

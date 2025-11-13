using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;

namespace HushchynFinalTask.Pages
{
    public abstract class BasePage
    {
        protected BasePage(IWebDriver driver, ILogger logger)
        {
            this.Driver = driver;
            this.Log = logger;
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(10));
        }

        protected IWebDriver Driver { get; }

        protected ILogger Log { get; }

        protected WebDriverWait Wait { get; }

        protected void ClickWhenReady(By locator)
        {
            try
            {
                this.Log.Debug($"Waiting for element to be clickable: {locator}");

                IWebElement element = this.Wait.Until(driver =>
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
                this.Log.Error(ex, $"Failed to click element: {locator}");
                throw;
            }
        }

        protected void SendKeysWhenReady(By locator, string text)
        {
            try
            {
                this.Log.Debug($"Waiting for element to be visible: {locator}");
                var element = this.WaitForElementVisible(locator);
                element.Clear();
                element.SendKeys(text);
            }
            catch (Exception ex)
            {
                this.Log.Error(ex, $"Failed to enter text '{text}' into element: {locator}");
                throw;
            }
        }

        protected void ClearInput(By locator)
        {
            var element = this.WaitForElementVisible(locator);
            element.Click();
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
            _ = this.Wait.Until(d => string.IsNullOrEmpty(d.FindElement(locator).GetAttribute("value")));

            this.Log.Debug($"Field {locator} cleared using SendKeys.");
        }

        protected string GetTextWhenVisible(By locator)
        {
            try
            {
                this.Log.Debug($"Waiting for text from element: {locator}");
                var element = this.WaitForElementVisible(locator);
                return element.Text;
            }
            catch (Exception ex)
            {
                this.Log.Error(ex, $"Failed to get text from element: {locator}");
                throw;
            }
        }

        private IWebElement WaitForElementVisible(By locator)
        {
            return this.Wait.Until(driver =>
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
    }
}

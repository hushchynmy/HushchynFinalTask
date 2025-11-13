using FluentAssertions;
using HushchynFinalTask.Pages;
using OpenQA.Selenium;
using Serilog;

namespace HushchynFinalTask.Pages
{
    public class InventoryPage : BasePage
    {
        private static By Title => By.XPath("//div[@class='app_logo']");
        private const string ExpectedTitle = "Swag Labs";

        public InventoryPage(IWebDriver driver, ILogger logger) : base(driver, logger)
        {
        }

        public void VerifyOnInventoryPage()
        {
            Log.Information("Verifying presence on the inventory page");
            try
            {
                var titleText = Driver.FindElement(Title).Text;
                titleText.Should().Be(ExpectedTitle);
                Log.Information("Successful login: Page title 'Products' confirmed.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to confirm navigation to the inventory page");
                throw;
            }
        }
    }
}
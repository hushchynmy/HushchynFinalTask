using FluentAssertions;
using OpenQA.Selenium;
using Serilog;

namespace HushchynFinalTask.Pages
{
    public class InventoryPage(IWebDriver driver, ILogger logger)
        : BasePage(driver, logger)
    {
        private const string ExpectedTitle = "Swag Labs";

        private static By Title => By.XPath("//div[@class='app_logo']");

        public void VerifyOnInventoryPage()
        {
            this.Log.Information("Verifying presence on the inventory page");
            try
            {
                var titleText = this.Driver.FindElement(Title).Text;
                _ = titleText.Should().Be(ExpectedTitle);
                this.Log.Information("Successful login: Page title 'Products' confirmed.");
            }
            catch (Exception ex)
            {
                this.Log.Error(ex, "Failed to confirm navigation to the inventory page");
                throw;
            }
        }
    }
}

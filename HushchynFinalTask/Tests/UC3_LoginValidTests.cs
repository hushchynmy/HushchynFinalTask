using HushchynFinalTask.Drivers;
using HushchynFinalTask.Pages;
using Xunit;
using Xunit.Abstractions;

namespace HushchynFinalTask.Tests
{
    public class UC3_LoginValidTests(ITestOutputHelper output)
        : BaseTest(output)
    {
        public static TheoryData<BrowserType, string, string> UC3Data => new()
        {
            { BrowserType.Chrome, "standard_user", "secret_sauce" },
            { BrowserType.Firefox, "standard_user", "secret_sauce" },
        };

        [Theory]
        [MemberData(nameof(UC3Data))]
        public void UC3_TestLoginWithValidCredentials(BrowserType browserType, string username, string password)
        {
            this.Log.Information($"--- Start UC-3 ({browserType}, User: {username}) ---");

            var driver = this.DriverManager.GetDriver(browserType);
            var loginPage = new LoginPage(driver, this.Log);
            var inventoryPage = new InventoryPage(driver, this.Log);

            loginPage.GoToPage();
            loginPage.TypeUsername(username);
            loginPage.TypePassword(password);
            loginPage.ClickLogin();
            inventoryPage.VerifyOnInventoryPage();

            this.Log.Information($"--- End UC-3 ({browserType}, User: {username}) ---");
        }
    }
}

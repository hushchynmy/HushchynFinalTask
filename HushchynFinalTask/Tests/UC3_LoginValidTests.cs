using HushchynFinalTask.Drivers;
using HushchynFinalTask.Pages;
using HushchynFinalTask.Tests;
using Xunit.Abstractions;

namespace HushchynFinalTask.Tests
{
    public class UC3_LoginValidTests : BaseTest
    {
        public UC3_LoginValidTests(ITestOutputHelper output) : base(output)
        {
        }
        public static IEnumerable<object[]> GetValidLoginData()
        {
            yield return new object[] { BrowserType.Chrome, "standard_user", "secret_sauce" };
            yield return new object[] { BrowserType.Firefox, "standard_user", "secret_sauce" };
        }

        [Theory]
        [MemberData(nameof(GetValidLoginData))]
        public void UC3_TestLoginWithValidCredentials(BrowserType browserType, string username, string password)
        {
            _log.Information($"--- Start UC-3 ({browserType}, User: {username}) ---");

            var driver = _driverManager.GetDriver(browserType);
            var loginPage = new LoginPage(driver, _log);
            var inventoryPage = new InventoryPage(driver, _log);

            loginPage.GoToPage();
            loginPage.TypeUsername(username);
            loginPage.TypePassword(password);
            loginPage.ClickLogin();
            inventoryPage.VerifyOnInventoryPage();

            _log.Information($"--- End UC-3 ({browserType}, User: {username}) ---");
        }
    }
}
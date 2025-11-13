using HushchynFinalTask.Drivers;
using HushchynFinalTask.Pages;
using Xunit.Abstractions;

namespace HushchynFinalTask.Tests
{
    public class UC1_LoginEmptyCredentialsTests : BaseTest
    {
        public UC1_LoginEmptyCredentialsTests(ITestOutputHelper output) : base(output)
        {
        }
        public static IEnumerable<object[]> GetUC1Data()
        {
            yield return new object[] { BrowserType.Chrome, "some_user", "some_pass" };
            yield return new object[] { BrowserType.Firefox, "some_user", "some_pass" };
        }

        [Theory]
        [MemberData(nameof(GetUC1Data))]
        public void UC1_TestLoginWithEmptyCredentials(BrowserType browserType, string userToType, string passToType)
        {
            _log.Information($"--- Start UC-1 ({browserType}) ---");

            var driver = _driverManager.GetDriver(browserType);

            var loginPage = new LoginPage(driver, _log);
            loginPage.GoToPage();
            loginPage.TypeUsername(userToType);
            loginPage.TypePassword(passToType);
            loginPage.ClearUsername();
            loginPage.ClearPassword();
            loginPage.ClickLogin();

            loginPage.VerifyErrorMessage("Epic sadface: Username is required");

            _log.Information($"--- End UC-1 ({browserType}) ---");
            
        }
    }
}

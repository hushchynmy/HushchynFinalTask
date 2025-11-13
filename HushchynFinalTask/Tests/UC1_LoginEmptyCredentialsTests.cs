using HushchynFinalTask.Drivers;
using HushchynFinalTask.Pages;
using Xunit;
using Xunit.Abstractions;

namespace HushchynFinalTask.Tests
{
    public class UC1_LoginEmptyCredentialsTests(ITestOutputHelper output)
        : BaseTest(output)
    {
        public static TheoryData<BrowserType, string, string, string> UC1Data => new()
        {
            { BrowserType.Chrome, "some_user", "some_pass", "Epic sadface: Username is required" },
            { BrowserType.Firefox, "some_user", "some_pass", "Epic sadface: Username is required" },
        };

        [Theory]
        [MemberData(nameof(UC1Data))]
        public void UC1_TestLoginWithEmptyCredentials(BrowserType browserType, string userToType, string passToType, string errorMessage)
        {
            this.Log.Information($"--- Start UC-1 ({browserType}) ---");

            var driver = this.DriverManager.GetDriver(browserType);

            var loginPage = new LoginPage(driver, this.Log);
            loginPage.GoToPage();
            loginPage.TypeUsername(userToType);
            loginPage.TypePassword(passToType);
            loginPage.ClearUsername();
            loginPage.ClearPassword();
            loginPage.ClickLogin();

            loginPage.VerifyErrorMessage(errorMessage);

            this.Log.Information($"--- End UC-1 ({browserType}) ---");
        }
    }
}

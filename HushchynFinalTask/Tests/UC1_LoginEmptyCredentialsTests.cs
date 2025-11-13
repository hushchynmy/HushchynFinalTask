using HushchynFinalTask.Drivers;
using HushchynFinalTask.Pages;
using Xunit;
using Xunit.Abstractions;

namespace HushchynFinalTask.Tests
{
    public class UC1_LoginEmptyCredentialsTests(ITestOutputHelper output)
        : BaseTest(output)
    {
        public static IEnumerable<object[]> UC1Data =>
        [
            [BrowserType.Chrome, "some_user", "some_pass"],
            [BrowserType.Firefox, "some_user", "some_pass"]
        ];

        [Theory]
        [MemberData(nameof(UC1Data))]
        public void UC1_TestLoginWithEmptyCredentials(BrowserType browserType, string userToType, string passToType)
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

            loginPage.VerifyErrorMessage("Epic sadface: Username is required");

            this.Log.Information($"--- End UC-1 ({browserType}) ---");
        }
    }
}

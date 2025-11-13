using HushchynFinalTask.Drivers;
using HushchynFinalTask.Pages;
using Xunit;
using Xunit.Abstractions;

namespace HushchynFinalTask.Tests
{
    public class UC2_LoginEmptyPasswordTests(ITestOutputHelper output)
        : BaseTest(output)
    {
        public static TheoryData<BrowserType, string, string, string> UC2Data => new()
        {
            { BrowserType.Chrome, "standard_user", "some_pass", "Epic sadface: Password is required" },
            { BrowserType.Firefox, "standard_user", "some_pass", "Epic sadface: Password is required" },
        };

        [Theory]
        [MemberData(nameof(UC2Data))]
        public void UC2_TestLoginWithEmptyPassword(BrowserType browserType, string userToType, string passToType, string errorMessage)
        {
            this.Log.Information($"--- Start UC-2 ({browserType}) ---");

            var driver = this.DriverManager.GetDriver(browserType);
            var loginPage = new LoginPage(driver, this.Log);

            loginPage.GoToPage();
            loginPage.TypeUsername(userToType);
            loginPage.TypePassword(passToType);
            loginPage.ClearPassword();
            loginPage.ClickLogin();

            loginPage.VerifyErrorMessage(errorMessage);

            this.Log.Information($"--- End UC-2 ({browserType}) ---");
        }
    }
}

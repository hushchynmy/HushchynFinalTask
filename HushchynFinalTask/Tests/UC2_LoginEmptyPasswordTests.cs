using HushchynFinalTask.Drivers;
using HushchynFinalTask.Pages;
using Xunit.Abstractions;

namespace HushchynFinalTask.Tests
{
    public class UC2_LoginEmptyPasswordTests : BaseTest
    {

        public UC2_LoginEmptyPasswordTests(ITestOutputHelper output) : base(output)
        {
        }

        public static IEnumerable<object[]> GetUC2Data()
        {
            yield return new object[] { BrowserType.Chrome, "standard_user", "some_pass" };
            yield return new object[] { BrowserType.Firefox, "standard_user", "some_pass" };
        }

        [Theory]
        [MemberData(nameof(GetUC2Data))]
        public void UC2_TestLoginWithEmptyPassword(BrowserType browserType, string userToType, string passToType)
        {
            _log.Information($"--- Start UC-2 ({browserType}) ---");

            var driver = _driverManager.GetDriver(browserType);
            var loginPage = new LoginPage(driver, _log);


            loginPage.GoToPage();
            loginPage.TypeUsername(userToType);
            loginPage.TypePassword(passToType);
            loginPage.ClearPassword();
            loginPage.ClickLogin();

            loginPage.VerifyErrorMessage("Epic sadface: Password is required");

            _log.Information($"--- End UC-2 ({browserType}) ---");
        }
    }
}


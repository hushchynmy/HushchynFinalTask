using FluentAssertions;
using HushchynFinalTask.Pages;
using OpenQA.Selenium;
using Serilog;

namespace HushchynFinalTask.Pages
{
    public class LoginPage : BasePage
    {
        private const string BaseUrl = "https://www.saucedemo.com/";

        private static By UsernameInput => By.XPath("//input[@data-test='username']");
        private static By PasswordInput => By.XPath("//input[@data-test='password']");
        private static By LoginButton => By.XPath("//input[@data-test='login-button']");
        private static By ErrorMessageContainer => By.XPath("//h3[@data-test='error']");

        public LoginPage(IWebDriver driver, ILogger logger) : base(driver, logger)
        {
        }

        public void GoToPage()
        {
            Log.Information($"Opening page: {BaseUrl}");
            Driver.Navigate().GoToUrl(BaseUrl);
        }

        public void TypeUsername(string username)
        {
            Log.Information($"Typing username: {username}");
            SendKeysWhenReady(UsernameInput, username);
        }

        public void TypePassword(string password)
        {
            Log.Information($"Typing password: {password}");
            SendKeysWhenReady(PasswordInput, password);
        }

        public void ClearUsername()
        {
            Log.Information("Clearing Username field");
            ClearInput(UsernameInput);
        }

        public void ClearPassword()
        {
            Log.Information("Clearing Password field");
            ClearInput(PasswordInput);
        }

        public void ClickLogin()
        {
            Log.Information("Clicking Login button");
            ClickWhenReady(LoginButton);
        }

        public void VerifyErrorMessage(string expectedMessage)
        {
            Log.Information($"Verifying error message: '{expectedMessage}'");
            var errorMessage = GetTextWhenVisible(ErrorMessageContainer);

            errorMessage.Should().Be(expectedMessage);
        }
    }
}

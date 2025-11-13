using FluentAssertions;
using OpenQA.Selenium;
using Serilog;

namespace HushchynFinalTask.Pages
{
    public class LoginPage(IWebDriver driver, ILogger logger)
        : BasePage(driver, logger)
    {
        private const string BaseUrl = "https://www.saucedemo.com/";

        private static By UsernameInput => By.XPath("//input[@data-test='username']");

        private static By PasswordInput => By.XPath("//input[@data-test='password']");

        private static By LoginButton => By.XPath("//input[@data-test='login-button']");

        private static By ErrorMessageContainer => By.XPath("//h3[@data-test='error']");

        public void GoToPage()
        {
            this.Log.Information($"Opening page: {BaseUrl}");
            this.Driver.Navigate().GoToUrl(new Uri(BaseUrl));
        }

        public void TypeUsername(string username)
        {
            this.Log.Information($"Typing username: {username}");
            this.SendKeysWhenReady(UsernameInput, username);
        }

        public void TypePassword(string password)
        {
            this.Log.Information($"Typing password: {password}");
            this.SendKeysWhenReady(PasswordInput, password);
        }

        public void ClearUsername()
        {
            this.Log.Information("Clearing Username field");
            this.ClearInput(UsernameInput);
        }

        public void ClearPassword()
        {
            this.Log.Information("Clearing Password field");
            this.ClearInput(PasswordInput);
        }

        public void ClickLogin()
        {
            this.Log.Information("Clicking Login button");
            this.ClickWhenReady(LoginButton);
        }

        public void VerifyErrorMessage(string expectedMessage)
        {
            this.Log.Information($"Verifying error message: '{expectedMessage}'");
            var errorMessage = this.GetTextWhenVisible(ErrorMessageContainer);

            _ = errorMessage.Should().Be(expectedMessage);
        }
    }
}

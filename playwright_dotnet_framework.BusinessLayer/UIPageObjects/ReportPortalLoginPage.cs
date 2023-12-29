using Microsoft.Playwright;

namespace playwright.dotnet.framework.BusinessLayer.UIPageObjects
{
    //Custom Element Demo
    public class ReportPortalLoginPage
    {
        private readonly IPage _page;
        private readonly UsernameField _usernameField;
        private readonly PasswordField _passwordField;
        private readonly LoginButton _logInButton;

        public ReportPortalLoginPage(IPage page)
        {
            _page = page;
            _usernameField = new UsernameField(page.GetByPlaceholder("login"));
            _passwordField = new PasswordField(page.GetByPlaceholder("Password"));
            _logInButton = new LoginButton(page.GetByRole(AriaRole.Button, new() { NameString = "Login" }));
        }

        public async Task EnterUsername(string username)
        {
            await _usernameField.WriteUsernameDown(username);
        }

        public async Task EnterPassword(string password)
        {
            await _passwordField.WritePasswordDown(password);
        }

        public async Task ClickLogin()
        {
            await _logInButton.ClickTheLoginButton();
        }
    }
}

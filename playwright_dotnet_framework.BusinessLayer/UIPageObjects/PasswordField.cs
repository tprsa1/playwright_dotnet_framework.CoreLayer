using Microsoft.Playwright;


namespace playwright.dotnet.framework.BusinessLayer.UIPageObjects
{
    public class PasswordField
    {
        private readonly ILocator locator;

        public PasswordField(ILocator locator)
        {
            this.locator = locator;
        }
        public async Task WritePasswordDown(string password)
        {
            await locator.FillAsync(password);
        }
    }
}

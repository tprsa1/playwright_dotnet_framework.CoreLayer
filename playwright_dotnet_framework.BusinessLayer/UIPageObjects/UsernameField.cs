

using Microsoft.Playwright;

namespace playwright.dotnet.framework.BusinessLayer.UIPageObjects
{
    public class UsernameField
    {
        private readonly ILocator locator;

        public UsernameField(ILocator locator)
        {
            this.locator = locator;
        }

        public async Task WriteUsernameDown(string username)
        {
            await locator.FillAsync(username);
        }
    }
}

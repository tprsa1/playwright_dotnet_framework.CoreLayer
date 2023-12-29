using Microsoft.Playwright;

namespace playwright.dotnet.framework.BusinessLayer.UIPageObjects
{
    public class LaunchPage
    {
        private readonly IPage page;
        private readonly ILocator _launchTitle;

        public LaunchPage(IPage page)
        {
            this.page = page;
            _launchTitle = page.GetByText("DEMO_FILTER").First;
        }


        public async Task<string> GetLaunchTitleAsync()
        {
            if (_launchTitle != null)
            {
                await _launchTitle.ScrollIntoViewIfNeededAsync();
                return await _launchTitle.TextContentAsync();
            }
            return String.Empty;
        }
    }

}

using Microsoft.Playwright;

namespace playwright.dotnet.framework.CoreLayer.APIUtils
{
    public class PlaywrightHttpWrapper
    {
        private readonly IPlaywright _playwright;

        public PlaywrightHttpWrapper(IPlaywright playwright)
        {
            _playwright= playwright;
        }

    }


}


using FluentAssertions;
using NUnit.Framework;
using playwright.dotnet.framework.BusinessLayer.UIPageObjects;

namespace playwright.dotnet.framework.TestRunner.NUnit.TestRunner.UITests
{
    public class NavigateToAppropriateLaunchUITest : WidgetsBaseUITest
    {
        [Test]
        public async Task NavigateToAppropriateLaunch()
        {
            await reportPortalDemoDashboardPage.ClickOnAppropriateLaunchAsync();
            var reportPortalLaunchPage = new LaunchPage(page);
            var launchTitle = await reportPortalLaunchPage.GetLaunchTitleAsync();
            launchTitle.Should().Be("DEMO_FILTER");
        }
    }
}

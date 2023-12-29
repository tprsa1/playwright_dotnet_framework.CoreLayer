using Microsoft.Playwright;
using NUnit.Framework;
using playwright.dotnet.framework.BusinessLayer.UIPageObjects;
using playwright.dotnet.framework.CoreLayer.Config;
using System.Text.RegularExpressions;

namespace playwright.dotnet.framework.TestRunner.NUnit.TestRunner.UITests
{
    public class WidgetsBaseUITest
    {
        protected IPage page;
        private IBrowser browser;
        protected DemoDashboardPage? reportPortalDemoDashboardPage;

        [SetUp]
        public async Task SetUp()
        {
            var playwrightInit = await Playwright.CreateAsync();
            browser = await playwrightInit.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });

            page = await browser.NewPageAsync();
            await page.GotoAsync(ConfigReader.GetValue("AppSettings.DemoEnvironment.BaseUrl"));
            var reportPortalLoginPage = new ReportPortalLoginPage(page);
            await reportPortalLoginPage.EnterUsername(ConfigReader.GetValue("AppSettings.DemoEnvironment.Username"));
            await reportPortalLoginPage.EnterPassword(ConfigReader.GetValue("AppSettings.DemoEnvironment.Password"));
            await reportPortalLoginPage.ClickLogin();
            await Assertions.Expect(page).ToHaveURLAsync(new Regex("https://demo.reportportal.io/ui/#default_personal/dashboard"));
            var reportPortalLandingPage = new ReportPortalLandingPage(page);
            await reportPortalLandingPage.SearchForDashboard("DEMO DASHBOARD");
            await reportPortalLandingPage.ClickOnDemoDashboardLink();
            reportPortalDemoDashboardPage = new DemoDashboardPage(page);
            await Assertions.Expect(reportPortalDemoDashboardPage.GetDemoDashBoardTitle()).ToContainTextAsync("DEMO DASHBOARD");
        }
        [TearDown]
        public async Task TearDown()
        {
            await browser.DisposeAsync();
        }
    }
}

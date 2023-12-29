using Microsoft.Playwright;
using System.Threading.Tasks;

namespace playwright.dotnet.framework.BusinessLayer.UIPageObjects
{
    public class ReportPortalLandingPage
    {
        private readonly IPage page;
        private readonly ILocator _demoDashboardLink;
        private readonly ILocator _demoSearch;


        public ReportPortalLandingPage(IPage page)
        {
            this.page = page;
            _demoDashboardLink = page.GetByRole(AriaRole.Link, new() { NameString = "DEMO DASHBOARD", Exact = true });
            _demoSearch = page.GetByPlaceholder("Search by name");
        }

        public async Task ClickOnDemoDashboardLink()
        {
            await _demoDashboardLink.ClickAsync();
        }

        public async Task SearchForDashboard(string dashboardName)
        {
            await _demoSearch.FillAsync(dashboardName);
            await _demoSearch.PressAsync("Enter");
        }
    }
}

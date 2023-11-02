using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace playwright.dotnet.framework.TestRunner.NUnit.TestRunner
{
    public class BaseTest : PlaywrightTest
    {
        protected IAPIRequestContext Request = null;

        [SetUp]
        public async Task SetUpAPITesting()
        {
                await CreateAPIRequestContext();
        }

        private async Task CreateAPIRequestContext()
        {
            Request = await this.Playwright.APIRequest.NewContextAsync();
        }

        [OneTimeTearDown]
        public async Task TearDownAPITesting()
        {
            await Request.DisposeAsync();
        }
    }
}

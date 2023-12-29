using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playwright.dotnet.framework.BusinessLayer.UIPageObjects
{
    public class LoginButton
    {
        private readonly ILocator locator;

        public LoginButton(ILocator locator)
        {
            this.locator = locator;
        }
        public async Task ClickTheLoginButton()
        {
            await locator.ClickAsync();
        }
    }
}

using FluentAssertions;
using NUnit.Framework;

namespace playwright.dotnet.framework.TestRunner.NUnit.TestRunner.UITests
{
    public class ResizeUITest : WidgetsBaseUITest
    {
        [Test]
        public async Task ResizeTest()
        {
            var resizeButton1 = reportPortalDemoDashboardPage.GetResizeButton().First;
            await resizeButton1.ScrollIntoViewIfNeededAsync();
            var resizeButton = await resizeButton1.BoundingBoxAsync();
            Console.WriteLine(resizeButton.X + " " + resizeButton.Y);
            await page.Mouse.MoveAsync(resizeButton.X + resizeButton.Width / 2, resizeButton.Y + resizeButton.Height! / 2);
            await page.Mouse.DownAsync();
            await page.Mouse.MoveAsync(resizeButton.X + 80, resizeButton.Y);
            await page.Mouse.UpAsync();
            var resizeButton2 = await resizeButton1.BoundingBoxAsync();
            Console.WriteLine(resizeButton2?.X + " " + resizeButton2?.Y);
            resizeButton.X.Should().NotBe(resizeButton2?.X);
        }
    }
}

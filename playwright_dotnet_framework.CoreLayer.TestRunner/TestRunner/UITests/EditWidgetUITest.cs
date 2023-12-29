using FluentAssertions;
using NUnit.Framework;

namespace playwright.dotnet.framework.TestRunner.NUnit.TestRunner.UITests
{
    public class EditWidgetUITest : WidgetsBaseUITest
    {
        [Test]
        public async Task EditWidgetTest()
        {
            var widgetHeaderLocator = reportPortalDemoDashboardPage.GetWidgetHeader();
            var widgetCount = await widgetHeaderLocator.CountAsync();
            widgetCount.Should().BeGreaterThan(1);

            var firstWidget = widgetHeaderLocator.First;

            await firstWidget.WaitForAsync();
            var firstWidgetText = await firstWidget.TextContentAsync();
            var originalText = firstWidgetText;
            var newText = "a new text";
            await firstWidget.HoverAsync();
            await reportPortalDemoDashboardPage.ClickOnEditButtonAsync();
            await reportPortalDemoDashboardPage.EditWIdgetNameAsync(newText);
            await reportPortalDemoDashboardPage.ClickOnSaveButtonAsync();
            await firstWidget.HoverAsync();
            await firstWidget.IsVisibleAsync();
            var firstWidgetTextAfter = await firstWidget.TextContentAsync();
            firstWidgetTextAfter.Should().Be(newText);
            await firstWidget.IsVisibleAsync();
            await firstWidget.HoverAsync();
            await reportPortalDemoDashboardPage.ClickOnEditButtonAsync();
            await reportPortalDemoDashboardPage.EditWIdgetNameAsync(originalText);
            await reportPortalDemoDashboardPage.ClickOnSaveButtonAsync();
            await firstWidget.HoverAsync();


        }
    }
}

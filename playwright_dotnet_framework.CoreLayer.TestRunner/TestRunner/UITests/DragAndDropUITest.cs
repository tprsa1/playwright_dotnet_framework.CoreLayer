using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playwright.dotnet.framework.TestRunner.NUnit.TestRunner.UITests
{
    public class DragAndDropUITest : WidgetsBaseUITest
    {
        [Test]
        public async Task DragAndDropTest()
        {
            var widgetHeaderLocator = reportPortalDemoDashboardPage.GetWidgetHeader();
            var widgetCount = await widgetHeaderLocator.CountAsync();
            widgetCount.Should().BeGreaterThan(1);

            var firstWidget = widgetHeaderLocator.First;

            await firstWidget.WaitForAsync();
            var firstWidgetText = await firstWidget.TextContentAsync();

            var originalWidget = reportPortalDemoDashboardPage.GetOriginalWidget(firstWidgetText);
            var originalWidgetStyleBeforeMove = await originalWidget.GetAttributeAsync("style");

            var lastWidget = widgetHeaderLocator.Nth(4);
            await lastWidget.WaitForAsync();
            await lastWidget.ScrollIntoViewIfNeededAsync();

            await firstWidget.DragToAsync(lastWidget);

            var originalWidgetStyleAfterMove = await originalWidget.GetAttributeAsync("style");
            originalWidgetStyleBeforeMove.Should().NotBeEquivalentTo(originalWidgetStyleAfterMove);
        }
    }
}

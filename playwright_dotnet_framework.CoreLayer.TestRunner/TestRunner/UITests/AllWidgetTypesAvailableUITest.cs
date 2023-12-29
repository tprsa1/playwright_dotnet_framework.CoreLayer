using FluentAssertions;
using NUnit.Framework;


namespace playwright.dotnet.framework.TestRunner.NUnit.TestRunner.UITests
{
    public class AllWidgetTypesAvailableUITest : WidgetsBaseUITest
    {
        [Test]
        public async Task AllWidgetTypesAvailable()
        {
            await reportPortalDemoDashboardPage.ClickOnAddNewWidgetButtonAsync();
            var widgetTypes = await reportPortalDemoDashboardPage.GetAllWidgetTypes();
            var csvData = CsvReader.ReadCsvFile("statistics");
            csvData.Should().BeEquivalentTo(widgetTypes);
        }
    }
}

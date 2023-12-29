using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using playwright.dotnet.framework.BusinessLayer.APIServiceObject;
using playwright.dotnet.framework.CoreLayer.CommonUtils;
using System.Text.Json.Nodes;
using playwright.dotnet.framework.BusinessLayer.Models.UpdateWidgetModels;
using playwright.dotnet.framework.BusinessLayer.Models.CreateWidgetModels;
using playwright.dotnet.framework.BusinessLayer.Models.GetWidgetByIdModels;
using playwright.dotnet.framework.CoreLayer.APIUtils;

namespace playwright.dotnet.framework.TestRunner.NUnit.TestRunner.APITests
{
    [Parallelizable(ParallelScope.Children)]
    public class WidgetTests
    {
        //TODO move to other folder
        [TestCaseSource(nameof(GetTestDataForWidget))]
        public async Task GetWidgetById(string testData)
        {
            JObject testObject = JObject.Parse(testData);
            var expectedResponse = testObject.ToObject<GetWidgetByIdContentResponse>();
            var num = expectedResponse?.Id;
            var widgetService = new WidgetsServiceObject(new PlaywrightHttpWrapper());
            var getAllWidgetNamesResponse = await widgetService.GetWidgetById(num.ToString());
            getAllWidgetNamesResponse.StatusCode.Should().Be(200);
            var getAllWidgetNamesActualResponse = JsonConvert.DeserializeObject<GetWidgetByIdContentResponse>(getAllWidgetNamesResponse.Content);
            getAllWidgetNamesActualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        public static IEnumerable<string> GetTestDataForWidget()
        {
            JArray array = TestDataLoader.GetTestData("GetWidgetById");
            foreach (var item in array)
            {
                yield return item.ToString();
            }
        }



        [TestCaseSource(nameof(GetTestDataForUpdateWidget))]
        public async Task UpdateWidgetTest(string testData)
        {
            var guuid = Guid.NewGuid().ToString();
            JObject testObject = JObject.Parse(testData);
            var num = testObject["id"];
            var widgetService = new WidgetsServiceObject(new PlaywrightHttpWrapper());
            // Initialize Playwright and APIRequestContextOptions
            var getAllWidgetNamesResponse = await widgetService.GetWidgetById(num.ToString());
            getAllWidgetNamesResponse.StatusCode.Should().Be(200);
            var getAllWidgetNamesStringJObject = JObject.Parse(getAllWidgetNamesResponse.Content);
            var getUpdateWidgetRequest = JsonConvert.DeserializeObject<UpdateWidgetRequest>(getAllWidgetNamesResponse.Content);
            getUpdateWidgetRequest.Description = guuid;
            getUpdateWidgetRequest.FilterIds = new List<string>();
            getUpdateWidgetRequest.FilterIds.Add(getAllWidgetNamesStringJObject?["appliedFilters"][0]["id"].ToString());
            getUpdateWidgetRequest.Filters = new List<Filter>();
            getUpdateWidgetRequest.Filters.Add(new Filter
            {
                Value = getAllWidgetNamesStringJObject["appliedFilters"]?[0]?["id"]?.ToString(),
                Name = getAllWidgetNamesStringJObject["appliedFilters"]?[0]?["name"]?.ToString()
            });
            var getUpdateWidgetResponse = await widgetService.UpdateWidget(num.ToString(), getUpdateWidgetRequest);
            getUpdateWidgetResponse.StatusCode.Should().Be(200);
            var ResponseObject = JsonNode.Parse(getUpdateWidgetResponse.Content);
            var expectedResponseMessage = testObject["message"].ToString();
            var actualResponseMessage = ResponseObject["message"].ToString();
            actualResponseMessage.Should().Be(expectedResponseMessage);

        }

        public static IEnumerable<string> GetTestDataForUpdateWidget()
        {
            JArray array = TestDataLoader.GetTestData("UpdateWidgetTest");
            foreach (var item in array)
            {
                yield return item.ToString();
            }
        }

        [TestCaseSource(nameof(GetTestDataForCreadteWidget))]
        public async Task CreateWidgetTest(string testData)
        {
            var guuid = Guid.NewGuid().ToString();
            JObject testObject = JObject.Parse(testData);
            var createWidgetRequest = testObject.ToObject<CreateWidgetRequestBody>();
            createWidgetRequest.Name = guuid;
            var widgetService = new WidgetsServiceObject(new PlaywrightHttpWrapper());
            var createWidgetResponse = await widgetService.CreateWidget(createWidgetRequest);
            createWidgetResponse.StatusCode.Should().Be(201);
            createWidgetResponse.Content.Should().NotBeEmpty();
        }
        public static IEnumerable<string> GetTestDataForCreadteWidget()
        {
            JArray array = TestDataLoader.GetTestData("CreateWidgetTest");
            foreach (var item in array)
            {
                yield return item.ToString();
            }
        }
    }
}
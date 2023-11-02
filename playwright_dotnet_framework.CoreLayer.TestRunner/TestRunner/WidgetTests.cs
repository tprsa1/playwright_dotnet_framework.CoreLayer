using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using playwright.dotnet.framework.BusinessLayer.APIServiceObject;
using playwright.dotnet.framework.TestRunner.NUnit.TestRunner;
using playwright.dotnet.framework.CoreLayer.CommonUtils;
using System.Text.Json.Nodes;
using playwright.dotnet.framework.BusinessLayer.Models.UpdateWidgetModels;
using playwright.dotnet.framework.BusinessLayer.Models.CreateWidgetModels;
using playwright.dotnet.framework.BusinessLayer.Models.GetWidgetByIdModels;
using playwright.dotnet.framework.BusinessLayer.Models.TokenModel;

namespace playwright.dotnet.framework.CoreLayer.TestRunner.TestRunner
{
    [Parallelizable(ParallelScope.Children)]
    public class WidgetTests : BaseTest
    {
        //TODO move to other folder
        [TestCaseSource(nameof(GetTestDataForWidget))]
        public async Task GetWidgetById(string testData)
        {
            JObject testObject = JObject.Parse(testData);
            var expectedResponse = testObject.ToObject<GetWidgetByIdContentResponse>();
            var num = expectedResponse?.Id;
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "GetWidgetById" + num);
            var widgetService = new WidgetsServiceObject(Request);
            var tokenResponse = await widgetService.GetTokenAsync();
            tokenResponse.Status.Should().Be(200);
            var stringResponse = await tokenResponse.TextAsync();
            //create wrapper for null checks
            var tokenObject = JsonConvert.DeserializeObject<TokenResponse>(stringResponse);
            // Initialize Playwright and APIRequestContextOptions
            var getAllWidgetNamesResponse = await widgetService.GetWidgetById(tokenObject.Access_Token, num.ToString());
            getAllWidgetNamesResponse.Status.Should().Be(200);
            var getAllWidgetNamesString = await getAllWidgetNamesResponse.TextAsync();
            var getAllWidgetNamesActualResponse = JsonConvert.DeserializeObject<GetWidgetByIdContentResponse>(getAllWidgetNamesString);
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
            var widgetService = new WidgetsServiceObject(Request);
            var tokenResponse = await widgetService.GetTokenAsync();
            tokenResponse.Status.Should().Be(200);
            var stringResponse = await tokenResponse.TextAsync();
            //create wrapper for null checks
            var tokenObject = JsonConvert.DeserializeObject<TokenResponse>(stringResponse);
            // Initialize Playwright and APIRequestContextOptions
            var getAllWidgetNamesResponse = await widgetService.GetWidgetById(tokenObject.Access_Token, num.ToString());
            getAllWidgetNamesResponse.Status.Should().Be(200);
            var getAllWidgetNamesString = await getAllWidgetNamesResponse.TextAsync();
            var getAllWidgetNamesStringJObject = JObject.Parse(getAllWidgetNamesString);
            var getUpdateWidgetRequest = JsonConvert.DeserializeObject<UpdateWidgetRequest>(getAllWidgetNamesString);
            getUpdateWidgetRequest.Description = guuid;
            getUpdateWidgetRequest.FilterIds = new List<string>();
            getUpdateWidgetRequest.FilterIds.Add(getAllWidgetNamesStringJObject["appliedFilters"][0]["id"].ToString());
            getUpdateWidgetRequest.Filters = new List<Filter>();
            getUpdateWidgetRequest.Filters.Add(new Filter
            {
                Value = getAllWidgetNamesStringJObject["appliedFilters"][0]["id"].ToString(),
                Name = getAllWidgetNamesStringJObject["appliedFilters"][0]["name"].ToString()
            });
            var getUpdateWidgetResponse = await widgetService.UpdateWidget(tokenObject.Access_Token, num.ToString(), getUpdateWidgetRequest);
            getUpdateWidgetResponse.Status.Should().Be(200);
            var UpdateResponse = await getUpdateWidgetResponse.TextAsync();
            var ResponseObject = JsonObject.Parse(UpdateResponse);
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
            var widgetService = new WidgetsServiceObject(Request);
            var tokenResponse = await widgetService.GetTokenAsync();
            tokenResponse.Status.Should().Be(200);
            var stringResponse = await tokenResponse.TextAsync();
            //create wrapper for null checks
            var tokenObject = JsonConvert.DeserializeObject<TokenResponse>(stringResponse);
            // Initialize Playwright and APIRequestContextOptions
            var createWidgetResponse = await widgetService.CreateWidget(tokenObject.Access_Token, createWidgetRequest);
            createWidgetResponse.Status.Should().Be(201);
            var createWidgetResponseString = await createWidgetResponse.TextAsync();
            createWidgetResponseString.Should().NotBeEmpty();
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
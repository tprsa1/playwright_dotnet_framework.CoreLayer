using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using playwright.dotnet.framework.BusinessLayer.APIServiceObject;
using playwright.dotnet.framework.BusinessLayer.Models.GetWidgetByIdModels;
using playwright.dotnet.framework.CoreLayer.CommonUtils;
using TechTalk.SpecFlow;
using FluentAssertions;
using playwright.dotnet.framework.BusinessLayer.Models.CreateWidgetModels;
using playwright.dotnet.framework.BusinessLayer.Models.UpdateWidgetModels;
using System.Text.Json.Nodes;
using playwright.dotnet.framework.CoreLayer.APIUtils;

[Binding]
public class WidgetStepDefinitions
{
    private readonly WidgetsServiceObject? _widgetService;
    private GetWidgetByIdContentResponse? _expectedResponseGetWidgetById;
    private CreateWidgetRequestBody? _createWidgetRequestBody;
    private string? _apiResponse;
    private string? _expectedUpdateMessage;

    public WidgetStepDefinitions(IHttpService service)
    {
        _widgetService = new WidgetsServiceObject(service);
    }

    [Given(@"I print ""([^""]*)"" before test execution")]
    public void GivenIPrintBeforeTestExecution(string print)
    {
        Console.WriteLine(print);
    }


    [Given(@"I have widget test data for ID (.*)")]
    public void GivenIHaveWidgetTestDataFor(int id)
    {
        JArray allTestData = TestDataLoader.GetTestData("GetWidgetById");

        var testObject = allTestData.FirstOrDefault(obj => obj?["id"]?.Value<int>() == id);
        if (testObject != null)
        {
            _expectedResponseGetWidgetById = testObject.ToObject<GetWidgetByIdContentResponse>();
        }
    }
    [Given(@"I have widget creation test data for widget named ""(.*)""")]
    public void GivenIHaveWidgetCreationTestData(string widgetName)
    {
        JArray allTestData = TestDataLoader.GetTestData("CreateWidgetTest");

        var testObject = allTestData.FirstOrDefault(obj => obj?["name"]?.Value<string>() == widgetName);
        if (testObject != null)
        {
            _createWidgetRequestBody = testObject.ToObject<CreateWidgetRequestBody>();
        }
    }

    [Given(@"I have widget update test data for widget id (.*)")]
    public void GivenIHaveWidgetUpdateTestDataForWidgetId(int id)
    {
        JArray allTestData = TestDataLoader.GetTestData("UpdateWidgetTest");

        var testObject = allTestData.FirstOrDefault(obj => obj?["id"]?.Value<int>() == id);
        if (testObject != null)
        {
            _expectedResponseGetWidgetById = new GetWidgetByIdContentResponse();
            _expectedResponseGetWidgetById.Id = testObject["id"].Value<int>();
            _expectedUpdateMessage = testObject?["message"]?.ToString();
        }
    }

    [When(@"I retrieve the widget by ID")]
    public async Task WhenIRetrieveTheWidgetById()
    {
        var response = await _widgetService.GetWidgetById(_expectedResponseGetWidgetById?.Id.ToString());
        response.StatusCode.Should().Be(200);
        _apiResponse = response.Content;
    }
    [When(@"I create a new widget")]
    public async Task WhenICreateANewWidget()
    {
        var response = await _widgetService.CreateWidget(_createWidgetRequestBody);
        response.StatusCode.Should().Be(201);
        _apiResponse = response.Content;
    }
    [When(@"something happens")]
    public void WhenSomethingHappens(Table table)
    {
        foreach (var row in table.Rows)
        {
            var firstThing = row["First thing"];
            var secondThing = row["Second thing"];
            var thirdThing = row["Third Thing"];

            // Example: Print the values
            Console.WriteLine($"First: {firstThing}, Second: {secondThing}, Third: {thirdThing}");
        }
    }
    [When(@"I want to know what is the shortest string between (.*)")]
    public void WhenIWantToKnowWhatIsTheShortestStringBetween(List<string> strings)
    {
        var shortestString = strings.OrderBy(s => s.Length).FirstOrDefault();
        Console.WriteLine($"The shortest string is: {shortestString}");
    }


    [StepArgumentTransformation]
    public List<string> TransformToListOfString(string commaSeparatedList)
    {
        return commaSeparatedList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim())
                                 .ToList();
    }


    [When(@"I update that widget by adding a unique description")]
    public async Task WhenIUpdateThatWidgetByAddingAUniqueDescription()
    {
        var guuid = Guid.NewGuid().ToString();
        var getAllWidgetNamesStringJObject = JObject.Parse(_apiResponse);
        var getUpdateWidgetRequest = JsonConvert.DeserializeObject<UpdateWidgetRequest>(_apiResponse);
        getUpdateWidgetRequest.Description = guuid;
        getUpdateWidgetRequest.FilterIds = new List<string>();
        getUpdateWidgetRequest.FilterIds.Add(getAllWidgetNamesStringJObject["appliedFilters"][0]["id"].ToString());
        getUpdateWidgetRequest.Filters = new List<Filter>();
        getUpdateWidgetRequest.Filters.Add(new Filter
        {
            Value = getAllWidgetNamesStringJObject?["appliedFilters"]?[0]?["id"]?.ToString(),
            Name = getAllWidgetNamesStringJObject?["appliedFilters"]?[0]?["name"]?.ToString()
        });
        var getUpdateWidgetResponse = await _widgetService.UpdateWidget(_expectedResponseGetWidgetById?.Id.ToString(), getUpdateWidgetRequest);
        getUpdateWidgetResponse.StatusCode.Should().Be(200);
        _apiResponse = getUpdateWidgetResponse.Content;
    }


    [Then(@"the widget response should match the expected result for GET Widget By Id")]
    public void ThenTheWidgetResponseShouldMatchTheExpectedResultForGETWidgetById()
    {
        var actualResponse = JsonConvert.DeserializeObject<GetWidgetByIdContentResponse>(_apiResponse);
        actualResponse.Should().BeEquivalentTo(_expectedResponseGetWidgetById);
    }

    [Then(@"the widget response should contain an Id number")]
    public void ThenTheWidgetShouldContainAnIdNumber()
    {
        var actualResponse = JsonConvert.DeserializeObject<GetWidgetByIdContentResponse>(_apiResponse);
        actualResponse.Should().NotBeNull();
    }

    [Then(@"the widget response should match the expected result for PUT Widget By Id")]
    public void ThenTheWidgetResponseShouldMatchTheExpectedResultForPUTWidgetById()
    {
        var responseObject = JsonObject.Parse(_apiResponse);
        var actualResponseMessage = responseObject?["message"]?.ToString();
        actualResponseMessage.Should().Be(_expectedUpdateMessage);
    }
}

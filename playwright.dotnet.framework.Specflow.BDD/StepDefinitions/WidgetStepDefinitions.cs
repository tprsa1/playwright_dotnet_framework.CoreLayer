using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using playwright.dotnet.framework.BusinessLayer.APIServiceObject;
using playwright.dotnet.framework.BusinessLayer.Models.GetWidgetByIdModels;
using playwright.dotnet.framework.CoreLayer.CommonUtils;
using TechTalk.SpecFlow;
using FluentAssertions;
using playwright.dotnet.framework.BusinessLayer.Models.TokenModel;
using playwright.dotnet.framework.BusinessLayer.Models.CreateWidgetModels;
using playwright.dotnet.framework.BusinessLayer.Models.UpdateWidgetModels;
using System.Text.Json.Nodes;
using System;

[Binding]
public class WidgetStepDefinitions
{
    private readonly WidgetsServiceObject _widgetService;
    private GetWidgetByIdContentResponse _expectedResponseGetWidgetById;
    private CreateWidgetRequestBody _createWidgetRequestBody;
    private CreateWidgetRequestBody _expectedResponseCreateWidget;
    private readonly TokenResponse _tokenResponse;
    private string _apiResponse;
    private string _expectedUpdateMessage;

    public WidgetStepDefinitions(IAPIRequestContext requestContext, TokenResponse tokenResponse)
    {
        _widgetService = new WidgetsServiceObject(requestContext);
        _tokenResponse = tokenResponse;
    }

    [Given(@"I have widget test data for ID (.*)")]
    public void GivenIHaveWidgetTestDataFor(int id)
    {
        JArray allTestData = TestDataLoader.GetTestData("GetWidgetById");

        var testObject = allTestData.FirstOrDefault(obj => obj["id"].Value<int>() == id);
        if (testObject != null)
        {
            _expectedResponseGetWidgetById = testObject.ToObject<GetWidgetByIdContentResponse>();
        }
    }
    [Given(@"I have widget creation test data for widget named ""(.*)""")]
    public void GivenIHaveWidgetCreationTestData(string widgetName)
    {
        JArray allTestData = TestDataLoader.GetTestData("CreateWidgetTest");

        var testObject = allTestData.FirstOrDefault(obj => obj["name"].Value<string>() == widgetName);
        if (testObject != null)
        {
            _createWidgetRequestBody = testObject.ToObject<CreateWidgetRequestBody>();
        }
    }

    [Given(@"I have widget update test data for widget id (.*)")]
    public void GivenIHaveWidgetUpdateTestDataForWidgetId(int id)
    {
        JArray allTestData = TestDataLoader.GetTestData("UpdateWidgetTest");

        var testObject = allTestData.FirstOrDefault(obj => obj["id"].Value<int>() == id);
        if (testObject != null)
        {
            _expectedResponseGetWidgetById = new GetWidgetByIdContentResponse();
            _expectedResponseGetWidgetById.Id = testObject["id"].Value<int>();
            _expectedUpdateMessage = testObject["message"].ToString();
        }
    }

    [When(@"I retrieve the widget by ID")]
    public async Task WhenIRetrieveTheWidgetById()
    {
        var response = await _widgetService.GetWidgetById(_expectedResponseGetWidgetById.Id.ToString());
        response.Status.Should().Be(200);
        _apiResponse = await response.TextAsync();
    }
    [When(@"I create a new widget")]
    public async Task WhenICreateANewWidget()
    {
        var token = _tokenResponse.Access_Token;
        var response = await _widgetService.CreateWidget(_createWidgetRequestBody);
        response.Status.Should().Be(201);
        _apiResponse = await response.TextAsync();
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
            Value = getAllWidgetNamesStringJObject["appliedFilters"][0]["id"].ToString(),
            Name = getAllWidgetNamesStringJObject["appliedFilters"][0]["name"].ToString()
        });
        var getUpdateWidgetResponse = await _widgetService.UpdateWidget(_expectedResponseGetWidgetById.Id.ToString(), getUpdateWidgetRequest);
        getUpdateWidgetResponse.Status.Should().Be(200);
        _apiResponse = await getUpdateWidgetResponse.TextAsync();
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
        var actualResponseMessage = responseObject["message"].ToString();
        actualResponseMessage.Should().Be(_expectedUpdateMessage);
    }
}

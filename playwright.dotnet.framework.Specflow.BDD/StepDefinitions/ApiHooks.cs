using BoDi;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

[Binding]
public class ApiHooks
{
    private readonly IObjectContainer _container;
    private IAPIRequestContext _requestContext;

    public ApiHooks(IObjectContainer container)
    {
        _container = container;
    }

    [BeforeScenario]
    public async Task BeforeScenarioAsync()
    {
        var playwright = await Playwright.CreateAsync();
        _requestContext = await playwright.APIRequest.NewContextAsync();

        // Register the IAPIRequestContext instance in the container for dependency injection
        _container.RegisterInstanceAs<IAPIRequestContext>(_requestContext);
    }

    [AfterScenario]
    public async Task AfterScenarioAsync()
    {
        if (_requestContext != null)
        {
            await _requestContext.DisposeAsync();
        }
    }
}

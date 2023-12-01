using BoDi;
using playwright.dotnet.framework.CoreLayer.APIUtils;
using TechTalk.SpecFlow;

[Binding]
public class ApiHooks
{
    private readonly IObjectContainer _container;
    private IHttpService? _requestContext;

    public ApiHooks(IObjectContainer container)
    {
        _container = container;
    }

    [BeforeScenario]
    public void BeforeScenarioAsync()
    {
        _requestContext = new PlaywrightHttpWrapper();
        _container.RegisterInstanceAs<IHttpService>(_requestContext);
    }

    [AfterScenario]
    public void AfterScenarioAsync()
    {
        Console.WriteLine("After Scenario");
    }
}

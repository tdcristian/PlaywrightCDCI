using EaFramework.Config;
using EaFramework.Driver;
using Microsoft.Playwright;

namespace EaTestAutomation.Fixture;

public interface ITestFixtureBase
{
    Task NavigateToUrl(string url);
    Task TakeScreenshotAsync();
}

public class TestFixtureBase : ITestFixtureBase
{
    private readonly IPlaywrightDriver _playwrightDriver;
    private readonly TestSettings _testSettings;
    private Task<IPage> _page;

    public TestFixtureBase(IPlaywrightDriver playwrightDriver, TestSettings testSettings)
    {
        _playwrightDriver = playwrightDriver;
        _testSettings = testSettings;
        _page = playwrightDriver.Page;
    }

    public async Task NavigateToUrl(string url)
    {
        await (await _page).GotoAsync(url);
    }

    public async Task TakeScreenshotAsync()
    {
        await (await _page).ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "screenshot.png",
            FullPage = true
        });
    }
}
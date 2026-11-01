using EaFramework.Config;
using Microsoft.Playwright;

namespace EaFramework.Driver;

public class PlaywrightDriver : IDisposable, IPlaywrightDriver
{
    private readonly AsyncTask<IBrowser> _browser;
    private readonly AsyncTask<IBrowserContext> _browserContext;
    private readonly AsyncTask<IPage> _page;
    private readonly TestSettings _testSettings;
    private readonly IPlaywrightDriverInitializer _playwrightDriverInitializer;
    private bool _isDisposed;

    public PlaywrightDriver(TestSettings testSettings, IPlaywrightDriverInitializer playwrightDriverInitializer)
    {
        _testSettings = testSettings;
        _playwrightDriverInitializer = playwrightDriverInitializer;

        _browser = new AsyncTask<IBrowser>(InitializePlaywrightAsync);
        _browserContext = new AsyncTask<IBrowserContext>(CreateBrowserContext);
        _page = new AsyncTask<IPage>(CreatePageAsync);
    }

    public Task<IPage> Page => _page.Value;
    public Task<IBrowserContext> BrowserContext => _browserContext.Value;
    public Task<IBrowser> Browser => _browser.Value;

    private async Task<IBrowser> InitializePlaywrightAsync()
    {
        return _testSettings.DriverType switch
        {
            DriverType.Chromium => await _playwrightDriverInitializer.GetChromeDriverAsync(_testSettings),
            DriverType.Edge => await _playwrightDriverInitializer.GetEdgeDriverAsync(_testSettings),
            DriverType.Firefox => await _playwrightDriverInitializer.GetFirefoxDriverAsync(_testSettings),
            DriverType.Webkit => await _playwrightDriverInitializer.GetWebKitDriverAsync(_testSettings),
            _ => await _playwrightDriverInitializer.GetChromeDriverAsync(_testSettings)
        };
    }

    private async Task<IBrowserContext> CreateBrowserContext()
    {
        var browser = await _browser;
        return await browser.NewContextAsync(new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true
        });
    }

    private async Task<IPage> CreatePageAsync()
    {
        var browserContext = await _browserContext;
        return await browserContext.NewPageAsync();
    }

    public void Dispose()
    {
        if (!_isDisposed)
            return;

        if (_browser.IsValueCreated)
        {
            Task.Run(async () =>
            {
                await (await Browser).CloseAsync();
                await (await BrowserContext).CloseAsync();
            });
        }

        _isDisposed = true;
    }
}
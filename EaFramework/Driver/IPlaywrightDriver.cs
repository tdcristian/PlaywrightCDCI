using Microsoft.Playwright;

namespace EaFramework.Driver;

public interface IPlaywrightDriver
{
    Task<IPage> Page { get; }
    Task<IBrowserContext> BrowserContext { get; }
    Task<IBrowser> Browser { get; }
}
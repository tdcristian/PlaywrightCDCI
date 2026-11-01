using System.ComponentModel;
using EaFramework.Config;
using EaFramework.Driver;
using EaTestAutomation.Fixture;
using EaTestAutomation.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace EaTestAutomation;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IPlaywrightDriverInitializer, PlaywrightDriverInitializer>()
            .AddScoped<IPlaywrightDriver, PlaywrightDriver>()
            .AddSingleton(ConfigReader.ReadConfig())
            .AddScoped<IProductPage, ProductPage>()
            .AddScoped<IProductListPage, ProductListPage>()
            .AddScoped<ITestFixtureBase, TestFixtureBase>();
    }
}
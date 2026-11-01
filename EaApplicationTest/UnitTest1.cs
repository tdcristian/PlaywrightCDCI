using AutoFixture.Xunit2;
using EaFramework.Config;
using EaFramework.Driver;
using EaTestAutomation.Pages;
using EaTestAutomation.Models;
using Microsoft.Playwright;


namespace EaTestAutomation;

public class Tests
{
    private readonly IPlaywrightDriver _playwrightDriver;
    private readonly TestSettings _testSettings;
    private readonly IProductListPage _productListPage;
    private IProductPage _productPage;

    public Tests(IPlaywrightDriver playwrightDriver, TestSettings testSettings, IProductListPage productListPage,
        IProductPage productPage)
    {
        _playwrightDriver = playwrightDriver;
        _testSettings = testSettings;
        _productListPage = productListPage;
        _productPage = productPage;
    }

    [Fact]
    public async Task TestLogin()
    {
        var page = await _playwrightDriver.Page;
        await page.GotoAsync(_testSettings.ApplicationUrl);

        await page.Locator("text=Login").ClickAsync();
        await page.Locator("#UserName").FillAsync("admin");
        await page.Locator("#Password").FillAsync("password");
        await page.Locator("button:has-text(\"Sign In\")").ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "👥 Employees" }).ClickAsync();
    }

}
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

    [Fact]
    public async Task Test2()
    {
        var page = await _playwrightDriver.Page;
        await page.GotoAsync("http://localhost:8000/");

        await _productListPage.CreateProductAsync();
        
        var product = new Product
        {
            Name = "UPS",
            Description = "UPS Description",
            Price = 2000,
            ProductType = ProductType.PERIPHARALS
        };
        await _productPage.CreateProductAsync(product);
        await _productListPage.ClickProductFromListAsync(product.Description);

        Assert.True(await _productListPage.IsProductCreatedDisplayedAsync(product.Name));
    }

    [Theory]
    [InlineData("UPPS", "UPS Description", "2000", "PERIPHARALS")]
    public async Task Test3(string productName, string productDescription, string productPrice, string productCategory)
    {
        var page = await _playwrightDriver.Page;
        await page.GotoAsync("http://localhost:8000/");


        await _productListPage.CreateProductAsync();

        var product = new Product
        {
            Name = productName,
            Description = productDescription,
            Price = decimal.Parse(productPrice),
            ProductType = Enum.Parse<ProductType>(productCategory)
        };
        await _productPage.CreateProductAsync(product);

        await _productListPage.ClickProductFromListAsync(product.Description);

        Assert.True(await _productListPage.IsProductCreatedDisplayedAsync(product.Name));
    }

    [Theory, AutoData]
    public async Task TestWithAutoFixtureData(Product product)
    {
        var page = await _playwrightDriver.Page;
        await page.GotoAsync("http://localhost:8000/");

        await _productListPage.CreateProductAsync();

        await _productPage.CreateProductAsync(product);
        await _productListPage.ClickProductFromListAsync(product.Description);

        Assert.True(await _productListPage.IsProductCreatedDisplayedAsync(product.Name));
    }
}
using EaFramework.Driver;
using Microsoft.Playwright;

namespace EaTestAutomation.Pages;

public interface IProductListPage
{
    Task CreateProductAsync();
    Task ClickProductFromListAsync(string productDescription);
    Task<bool> IsProductCreatedDisplayedAsync(string productName);
}

public class ProductListPage : IProductListPage
{
    private readonly IPage _page;

    public ProductListPage(IPlaywrightDriver playwrightDriver) => _page = playwrightDriver.Page.Result;

    private ILocator _lnkProductList => _page.GetByRole(AriaRole.Link, new() { Name = "Product" });
    private ILocator _lnkCreate => _page.GetByRole(AriaRole.Link, new() { Name = "Create" });


    public async Task CreateProductAsync()
    {
        await _lnkProductList.ClickAsync();
        await _lnkCreate.ClickAsync();
    }

    public async Task ClickProductFromListAsync(string productDescription)
    {
        var rowProduct = _page.GetByRole(AriaRole.Row, new() { Name = productDescription });
        var lnkDetails = rowProduct.GetByRole(AriaRole.Link, new() { Name = "Details" }).Last;
        await lnkDetails.ClickAsync();
    }

    public async Task<bool> IsProductCreatedDisplayedAsync(string productName)
    {
        return await _page.Locator("#Name").Filter(new() { HasText = productName }).IsVisibleAsync();
    }
}
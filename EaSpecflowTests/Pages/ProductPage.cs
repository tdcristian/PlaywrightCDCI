using EaFramework.Driver;
using EaTestAutomation.Models;
using Microsoft.Playwright;

namespace EaTestAutomation.Pages;

public interface IProductPage
{
    Task CreateProductAsync(Product product);
}

public class ProductPage : IProductPage
{
    private readonly IPage _page;

    public ProductPage(IPlaywrightDriver playwrightDriver) => _page = playwrightDriver.Page.Result;


    private ILocator _txtName => _page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
    private ILocator _txtDescription => _page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
    private ILocator _txtPrice => _page.Locator("#Price");
    private ILocator _selectProductType => _page.GetByLabel("ProductType");
    private ILocator _btnCreate => _page.GetByRole(AriaRole.Button, new() { Name = "Create" });

    public async Task CreateProductAsync(Product product)
    {
        await _txtName.FillAsync(product.Name);
        await _txtDescription.FillAsync(product.Description);
        await _txtPrice.FillAsync(product.Price.ToString());
        await _selectProductType.SelectOptionAsync(product.ProductType.ToString());
        await _btnCreate.ClickAsync();
        await Task.Delay(1000);
    }
}
using AutoFixture.Xunit2;
using EaTestAutomation.Fixture;
using EaTestAutomation.Pages;
using EaTestAutomation.Models;


namespace EaTestAutomation;

public class CreteProductTest
{
    private readonly ITestFixtureBase _testFixtureBase;
    private readonly IProductListPage _productListPage;
    private IProductPage _productPage;

    public CreteProductTest(ITestFixtureBase testFixtureBase, IProductListPage productListPage,
        IProductPage productPage)
    {
        _productListPage = productListPage;
        _productPage = productPage;
        _testFixtureBase = testFixtureBase;
    }


    [Theory, AutoData]
    public async Task TestWithAutoFixtureData(Product product)
    {
        // Arrange
        await _testFixtureBase.NavigateToUrl("http://localhost:8000/");
        await _productListPage.CreateProductAsync();
        await _productPage.CreateProductAsync(product);
        
        // Act
        await _productListPage.ClickProductFromListAsync(product.Description);

        // Assert
        Assert.True(await _productListPage.IsProductCreatedDisplayedAsync(product.Name));
    }
}
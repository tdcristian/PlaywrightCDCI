using EaTestAutomation.Models;
using EaTestAutomation.Pages;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace EaTestAutomation.Steps
{
    [Binding]
    public sealed class ProductSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IProductPage _productPage;
        private readonly IProductListPage _productListPage;

        public ProductSteps(ScenarioContext scenarioContext, IProductPage productPage, IProductListPage productListPage)
        {
            _scenarioContext = scenarioContext;
            _productPage = productPage;
            _productListPage = productListPage;
        }

        [Given("I click on Product menu")]
        public async Task GivenIClickOnProductMenu()
        {
            await _productListPage.CreateProductAsync();
        }

        [Given("I create project with following details")]
        public async Task GivenICreateProjectWithFollowingDetails(Table table)
        {
            var product = table.CreateInstance<Product>();
            _scenarioContext.Set(product);
            await _productPage.CreateProductAsync(product);
        }

        [When("I click the Details link of the newly created product")]
        public async Task WhenIClickTheDetailsLinkOfTheNewlyCreatedProduct()
        {
            var product = _scenarioContext.Get<Product>();
            await _productListPage.ClickProductFromListAsync(product.Description);
        }

        [Then("I see all the product details are created as expected")]
        public async Task ThenISeeAllTheProductDetailsAreCreatedAsExpected()
        {
            var product = _scenarioContext.Get<Product>();
            Assert.True(await _productListPage.IsProductCreatedDisplayedAsync(product.Name));
        }
    }
}
# UnitTest1.cs method reference

This README lists the methods declared and directly called in [UnitTest1.cs](EaApplicationTest/UnitTest1.cs). Repeated calls are listed once per method. Methods called internally by page objects are outside this list.

## Test methods

| Method | Purpose | Test data |
| --- | --- | --- |
| `TestLogin()` | Opens the configured application, logs in, and clicks the Employees link. | Username `admin`, password `password`. |
| `Test2()` | Creates a product, opens its details, and asserts that its name is displayed. | A product with name `UPS`, description `UPS Description`, price `2000`, and type `PERIPHARALS`. |
| `Test3(string productName, string productDescription, string productPrice, string productCategory)` | Creates and checks a product using parameterized data. | xUnit `[InlineData]`; converts price and category from strings. |
| `TestWithAutoFixtureData(Product product)` | Creates and checks a product supplied by AutoFixture. | `[Theory, AutoData]`. |

`TestLogin()` currently has no explicit assertion. All three product tests navigate to `http://localhost:8000/`.

## Methods called

| Method | Purpose in this file | Used by |
| --- | --- | --- |
| `ConfigReader.ReadConfig()` | Loads the test settings. | `Tests` constructor |
| `page.GotoAsync(url)` | Navigates to the application URL. | All four test methods |
| `page.Locator(selector)` | Finds the Login link, username and password inputs, or Sign In button using a selector. | `TestLogin` |
| `locator.ClickAsync()` | Clicks the Login link, Sign In button, or Employees link. | `TestLogin` |
| `locator.FillAsync(value)` | Enters the username or password. | `TestLogin` |
| `page.GetByRole(role, options)` | Finds the Employees link by its role and accessible name. | `TestLogin` |
| `productListPage.CreateProductAsync()` | Opens the Product list and then the Create page. | `Test2`, `Test3`, `TestWithAutoFixtureData` |
| `productPage.CreateProductAsync(product)` | Fills the product form and submits it. | `Test2`, `Test3`, `TestWithAutoFixtureData` |
| `productListPage.ClickProductFromListAsync(productDescription)` | Opens Details for the row matching the product description. | `Test2`, `Test3`, `TestWithAutoFixtureData` |
| `productListPage.IsProductCreatedDisplayedAsync(productName)` | Returns whether the product name element matching the supplied text is visible. | `Test2`, `Test3`, `TestWithAutoFixtureData` |
| `Assert.True(condition)` | Asserts that the product visibility check returns true. | `Test2`, `Test3`, `TestWithAutoFixtureData` |
| `decimal.Parse(productPrice)` | Converts the supplied price string to a decimal. | `Test3` |
| `Enum.Parse<ProductType>(productCategory)` | Converts the supplied category string to a `ProductType` enum value. | `Test3` |

## Constructors used

Constructors are listed separately from methods.

| Constructor | Purpose |
| --- | --- |
| `Tests(PlaywrightDriverInitializer playwrightDriverInitializer)` | Receives the xUnit fixture, reads configuration, and creates the driver wrapper. |
| `new PlaywrightDriver(_testSettings, _playwrightDriverInitializer)` | Creates the framework's Playwright driver wrapper. |
| `new ProductListPage(page)` | Creates the product list page object for the current page. |
| `new ProductPage(page)` | Creates the product form page object for the current page. |
| `new Product { ... }` | Creates product data in `Test2` and `Test3`. |

The target-typed `new() { Name = ... }` expression in `TestLogin` creates locator options. `_playwrightDriver.Page` is an awaited property, and `[Fact]`, `[Theory]`, `[InlineData]`, and `[AutoData]` are attributes rather than method calls.

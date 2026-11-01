Feature: Product
Create a new product

Link to step definitions: [ProductStepDefinitions]($projectname$/Steps/ProductStepDefinitions.cs)

Scenario: Add two numbers
    Given I click on Product menu
    And I create project with following details
        | Name       | Description       | Price | ProductType |
        | Headphones | Noise Cancelation |   300 | PERIPHARALS |
    When I click the Details link of the newly created product
    Then I see all the product details are created as expected
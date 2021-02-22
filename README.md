# Automation
Automation Test

Created the two Scenario
1. Creating an Order for the product T-shirt and verify the order history page has the reference number
2. Updating the Personal Information in My Account


Execution of Test Scenario

1.Download or Clone the resipotory
2. Restore the Nugets packages
3. Run the Regression test from the Test Explorer

Further Improvements
1. Creating Page objects in components level so it will be easily modified in future and easy for maintance
2. I have created all the step in one step file which is not a good practice, Will seeprate steps files based on the functionality
3. Need to add Web extensions for the common methods
4. Extent Reports Features are not combined and displayed as each test scenario in the left hand side (Need to update the Extent Reports to latest )
5. Extenet reports should have created with parallel execution options and made as singleton file at present if we run parallel execution the reports not update correctly. 
6. Seperating the Framework and the actual application test into different projects
7. Example of using Imperative steps, update with declarative steps
8. At present browser can be changed from the App.Config, should automatically taken from the Scenarios. 
9. Need to add RemoteWebDriver to run from Selenium Grid

Declarative type of Scenario
Scenario: Order a Tshirt product and verify the order updated in the order history 
Given I navigate to the application and logged in with username "dummy@gmail.com" and password "12345"
When I click T-Shirts amd add the product "Faded Short Sleeve T-Shirts" with Quantity "1" , Color "Blue" and Size "M"
And I open the Shopping cart I Should able to see the added product 
And I proceed to Address page to confrim the address
And I agree Terms and services for Shipping
And the payment should be "BANKWIRE"
Then I should see the order refernce number in the order History page
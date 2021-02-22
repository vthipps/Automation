Feature: Ordering
	Ordering a product T-Shirts to the cart and verify the order is in the Order History

@Regression @Imperative-examples
Scenario Outline: Order a T-Shirt product and verify the order updated in the order history
Given I navigate to application
Then I see Homepage displayed
And I click login link
When I enter username and password 
| UserName        | Password |
| TestUser@Automation.com | 12345    |
Then login should be successfully
When I navigate to products "T-SHIRTS" page
Then I should see to "T-Shirts" page
When I click on the product "Faded Short Sleeve T-shirts"
And I select product and Add to cart
|Quantity|Size|Color|
|<Quantity>| <Size>|<Color>|
Then Product cart layer should be displayed with user selected Quantity and color
|Quantity|Size|Color|
|<Quantity>| <Size>|<Color>|
When I click on Proceed to checkout button 
Then I verify total unit price of the product which selected "<Quantity>"
When I click on proceed to checkout from Shopping cart page
Then I should navigate to Address page
When I click on  proceed to checkout from Address page
Then I should navigate to Shipping page
When I click on procced to checkout button from Shipping page
Then I should navigate to Payment page
When user clicks to make payment from "<PaymentType>" 
Then order summary page should be displayed
When user confrims the order 
Then I should get order reference number for the Paymentype "<PaymentType>"
When I click on Back to order
Then I sholud navigate to order History page
And I should see the latest order in the order list	

Examples: 
| Quantity | Size | Color  | PaymentType |
| 2        | M    | Blue   | BANKWIRE    |
| 3        | L    | Orange | CHEQUE      |

@Declarative-examples @Regression
Scenario: Order a Tshirt product and verify the order updated in the order history 
Given I navigate to the application and logged in with username "TestUser@Automation.com" and password "12345"
When I click T-Shirts amd add the product "Faded Short Sleeve T-shirts" with Quantity "1" , Color "Blue" and Size "M"
And I open the Shopping cart I Should able to see the added product 
And I proceed to Address page to confrim the address
And I agree Terms and services for Shipping
And the payment should be "BANKWIRE"
Then I should see the order refernce number in the order History page

Feature: Update Personal Information
	I want to update Personal information

@Regression
Scenario: Update Firstname in my account
Given I navigate to application
Then I see Homepage displayed
And I click login link
When I enter username and password 
| UserName        | Password |
| TestUser@Automation.com | 12345   |
Then login should be successfully
When I click on Personal Information page
Then I should navigate to Personal Information page
When I update personal infromation Firstname as "TestUser" and Save
Then I should see success message as "Your personal information has been successfully updated."
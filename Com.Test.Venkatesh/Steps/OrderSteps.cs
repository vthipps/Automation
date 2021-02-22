using Com.Test.Venkatesh.Model;
using Com.Test.Venkatesh.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Com.Test.Venkatesh.Steps
{
    [Binding]
    public sealed class OrderSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private HomePage hpage;
        private ProductsPage ppage;
        private ProductDetailPage pdpage;
        private ShoppingCartSummaryPage scpage;
        private LoginPage lpage;
        private MyAccountPage myaccountpage;
        private PersonalInformationPage piPage;
        private DeliveryAddressPage daPage;
        private ShippingPage sPage;
        private PaymentMethod pmethod;
        private OrderSummaryPage osPage;
        private OrderConfirmationPage ocPage;
        private OrderHistoryPage ohPage;


        public OrderSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I navigate to application")]
        public void GivenINavigateToApplication()
        {
            hpage = new HomePage();
            hpage.LaunchURL();
        }

        [Then(@"I see Homepage displayed")]
        public void ThenISeeHomepageDisplayed()
        {
            Assert.IsTrue(hpage.isDisplayed(), "HomePage is not displayed");  
        }

        [Then(@"I click login link")]
        public void ThenIClickLoginLink()
        {
            lpage = hpage.ClickSignIn();
            Assert.IsTrue(lpage.IsDisplayed(), "Authentication page is not displayed");
        }

        [When(@"I enter username and password")]
        public void WhenIEnterUsernameAndPassword(Table table)
        {
            var data = table.CreateInstance<(string UserName, string Password)>();            
            myaccountpage = lpage.Login(data.UserName, data.Password);
            User.Username = data.UserName;
            User.Password = data.Password;
        }

        [Then(@"login should be successfully")]
        public void ThenLoginShouldBeSuccessfully()
        {
            Assert.IsTrue(myaccountpage.IsDisplayed(), "Login not successfully");
        }

        [When(@"I navigate to products ""(.*)"" page")]
        public void WhenINavigateToProductsPage(string productType)
        {
            if(productType.ToUpper() == "T-SHIRTS")
            {
                ppage = myaccountpage.ClickTShirts();
            }
            else if(productType.ToUpper() == "DRESSES")
            {
                ppage = myaccountpage.ClickDresses();
            }
            
        }

        [Then(@"I should see to ""(.*)"" page")]
        public void ThenIShoulseeToPage(string p0)
        {
            Assert.IsTrue(ppage.IsDisplayed(), "Product page is not displayed");
        }

        [When(@"I click on the product ""(.*)""")]
        public void WhenIClickOnTheProduct(string productName)
        {
            pdpage = ppage.ClickProductByName(productName);
        }

        [When(@"I select product and Add to cart")]
        public void WhenISelectProductAndAddToCart(Table table)
        {
            var data = table.CreateInstance<(string Quantity, string Size, string Color)>();
            pdpage.EnterQuantityColorSizeandClickAddtoCart(data.Quantity, data.Size,data.Color);
           
        }

        [Then(@"Product cart layer should be displayed with user selected Quantity and color")]
        public void ThenProductCartLayerShouldBeDisplayedWithUserSelectedQuantityAndColor(Table table)
        {
            var data = table.CreateInstance<(string Quantity, string Size, string Color)>();
            Assert.IsTrue(pdpage.CheckproductAttributesAndQuantity("Color", data.Color), "Selected color is not matching");
            Assert.IsTrue(pdpage.CheckproductAttributesAndQuantity("Quantity", data.Quantity), "Selected quantity is not matching");
        }


        [When(@"I click on Proceed to checkout button")]
        public void WhenIClickOnProceedToCheckoutButton()
        {
            scpage = pdpage.ClickProceedToCheckout();
        }

        [Then(@"I verify total unit price of the product which selected ""(.*)""")]
        public void ThenIVerifyTotalUnitPriceOfTheProductWhichSelected(string quantity)
        {
            Assert.IsTrue(scpage.IsDisplayed(), "Shopping cart Summary page is not displayed");
            scpage.VerifyUnitTotalPrice(quantity);
        }

        [When(@"I click on proceed to checkout from Address")]
        public void WhenIClickOnProceedToCheckoutFromAddress()
        {
           
           
        }
        [When(@"I click on proceed to checkout from Shopping cart page")]
        public void WhenIClickOnProceedToCheckoutFromShoppingCartPage()
        {
            daPage = scpage.ClickProccedToCheckout();
        }

        [Then(@"I should navigate to Address page")]
        public void ThenIShouldNavigateToAddressPage()
        {
            Assert.IsTrue(daPage.IsDisplayed(), "Address page is not displayed");
        }

        [When(@"I click on  proceed to checkout from Address page")]
        public void WhenIClickOnProceedToCheckoutFromAddressPage()
        {
            sPage = daPage.ClickProceedToCheckout();
        }


        [Then(@"I should navigate to Shipping page")]
        public void ThenIShouldNavigateToShippingPage()
        {
            Assert.IsTrue(sPage.IsDisplayed(), "Shipping page is not displayed");
        }

        [When(@"I click on procced to checkout button from Shipping page")]
        public void WhenIClickOnProccedToCheckoutButtonFromShippingPage()
        {
            pmethod = sPage.ClickProceedtoCheckout();
        }

        [Then(@"I should navigate to Payment page")]
        public void ThenIShouldNavigateToPAymentPage()
        {
            Assert.IsTrue(pmethod.IsDisplayed(), "Payment page is not displayed");
        }

        [When(@"user clicks to make payment from ""(.*)""")]
        public void WhenUserClicksToMakePaymentFrom(string paymentType)
        {
            osPage = pmethod.ClickPayBy(paymentType);
        }

        [Then(@"order summary page should be displayed")]
        public void ThenOrderSummaryPageShouldBeDisplayed()
        {
            Assert.IsTrue(osPage.IsDisplayed(), "Order Summary page should be displayed");
        }

        [When(@"user confrims the order")]
        public void WhenUserConfrimsTheOrder()
        {
            ocPage = osPage.ConfrimOrder();
        }

        [Then(@"I should get order reference number")]
        public void ThenIShouldGetOrderReferenceNumber()
        {
            
        }

        [Then(@"I should get order reference number for the Paymentype ""(.*)""")]
        public void ThenIShouldGetOrderReferenceNumberForThePaymentype(string PaymentType)
        {
            var ReferenceNumber = "";
            if (PaymentType == "BANKWIRE")
            {
                ReferenceNumber = ocPage.GetOrderReferenceNumberforBankWire();
            }
            else if(PaymentType == "CHEQUE")
            {
                ReferenceNumber = ocPage.GetOrderReferenceNumberforCheque();
            }
            _scenarioContext["OrderNumber"] = ReferenceNumber;
        }


        [When(@"I click on Back to order")]
        public void WhenIClickOnBackToOrder()
        {
            ohPage = ocPage.ClickBacktoOrders();
        }

        [Then(@"I sholud navigate to order History page")]
        public void ThenISholudNavigateToOrderHistoryPage()
        {            
            Assert.IsTrue(ohPage.IsDisplayed(), "Order History page is not dispalyed");
        }

        [Then(@"I should see the latest order in the order list")]
        public void ThenIShouldSeeTheLatestOrderInTheOrderList()
        {
            Assert.IsTrue(ohPage.CheckReferenceNumberExists(_scenarioContext["OrderNumber"].ToString()).Equals(_scenarioContext["OrderNumber"].ToString()), "Order is not matching");
        }

        [When(@"I click on Personal Information page")]
        public void WhenIClickOnPersonalInformationPage()
        {
            piPage = myaccountpage.ClickPersonalInformation();
        }

        [Then(@"I should navigate to Personal Information page")]
        public void ThenIShouldNavigateToPersonalInformationPage()
        {
            Assert.IsTrue(piPage.IsDisplayed(), "Personal Information page is not displayed");
        }

        [When(@"I update personal infromation Firstname as ""(.*)"" and Save")]
        public void WhenIUpdatePersonalInfromationFirstnameAsAndSave(string firstName)
        {
            piPage = piPage.UpdatePersonalInformation(firstName, User.Password);
        }



        [Then(@"I should see success message as ""(.*)""")]
        public void ThenIShouldSeeSuccessMessageAs(string successMessage)
        {
            Assert.IsTrue(piPage.VerifyPersonalInformationUpdatedMessage(successMessage), "Updated Personal information is not matching");
        }

        //// To be refactory 

        [Given(@"I navigate to the application and logged in with username ""(.*)"" and password ""(.*)""")]
        public void GivenINavigateToTheApplicationAndLoggedInWithUsernameAndPassword(string UserName, string Password)
        {

            hpage = new HomePage();
            hpage.LaunchURL();
            Assert.IsTrue(hpage.isDisplayed(), "HomePage is not displayed");
            lpage = hpage.ClickSignIn();
            Assert.IsTrue(lpage.IsDisplayed(), "Authentication page is not displayed");
            myaccountpage = lpage.Login(UserName, Password);
            User.Username = UserName;
            User.Password = Password; 
            Assert.IsTrue(myaccountpage.IsDisplayed(), "Login not successfully");
        }

        [When(@"I click T-Shirts amd add the product ""(.*)"" with Quantity ""(.*)"" , Color ""(.*)"" and Size ""(.*)""")]
        public void WhenIClickT_ShirtsAmdAddTheProductWithQuantityColorAndSize(string productName, string Quantity, string Color, string Size)
        {
            ppage = myaccountpage.ClickTShirts();
            Assert.IsTrue(ppage.IsDisplayed(), "Product page is not displayed");
            pdpage = ppage.ClickProductByName(productName);
            
            pdpage.EnterQuantityColorSizeandClickAddtoCart(Quantity, Size, Color);
            Assert.IsTrue(pdpage.CheckproductAttributesAndQuantity("Color", Color), "Selected color is not matching");
            Assert.IsTrue(pdpage.CheckproductAttributesAndQuantity("Quantity", Quantity), "Selected quantity is not matching");
            scpage = pdpage.ClickProceedToCheckout();
        }

        [When(@"I open the Shopping cart I Should able to see the added product")]
        public void WhenIOpenTheShoppingCartIShouldAbleToSeeTheAddedProduct()
        {
            Assert.IsTrue(scpage.IsDisplayed(), "Shopping cart Summary page is not displayed");            
        }

        [When(@"I proceed to Address page to confrim the address")]
        public void WhenIProceedToAddressPageToConfrimTheAddress()
        {
            daPage = scpage.ClickProccedToCheckout();
            Assert.IsTrue(daPage.IsDisplayed(), "Address page is not displayed");
        }

        [When(@"I agree Terms and services for Shipping")]
        public void WhenIAgreeTermsAndServicesForShipping()
        {
            sPage = daPage.ClickProceedToCheckout();
            Assert.IsTrue(sPage.IsDisplayed(), "Shipping page is not displayed");
            pmethod = sPage.ClickProceedtoCheckout();

        }

        [When(@"the payment should be ""(.*)""")]
        public void WhenThePaymentShouldBe(string paymentType)
        {
            Assert.IsTrue(pmethod.IsDisplayed(), "Payment page is not displayed");
            osPage = pmethod.ClickPayBy(paymentType);
            Assert.IsTrue(osPage.IsDisplayed(), "Order Summary page should be displayed");
            ocPage = osPage.ConfrimOrder();
            var ReferenceNumber = ocPage.GetOrderReferenceNumberforBankWire();
            _scenarioContext["OrderNumber"] = ReferenceNumber;

        }

        [Then(@"I should see the order refernce number in the order History page")]
        public void ThenIShouldSeeTheOrderRefernceNumberInTheOrderHistoryPage()
        {
            ohPage = ocPage.ClickBacktoOrders();
            Assert.IsTrue(ohPage.IsDisplayed(), "Order History page is not dispalyed");
            Assert.IsTrue(ohPage.CheckReferenceNumberExists(_scenarioContext["OrderNumber"].ToString()).Equals(_scenarioContext["OrderNumber"].ToString()), "Order is not matching");
        }

    }
}

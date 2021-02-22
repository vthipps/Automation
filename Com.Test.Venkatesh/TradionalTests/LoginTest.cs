using Com.Test.Venkatesh.Pages;
using Com.Test.Venkatesh.TestBase;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Test.Venkatesh.Tests
{
    [TestFixture]
    public class LoginTest 
    {
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

        [Test]
        [TestCase(Category ="NunitTestCase")]
        public void AddProductandCreateOrder()
        {
            hpage = new HomePage();
            
            hpage.LaunchURL();

            lpage = hpage.ClickSignIn();
            lpage.IsDisplayed();
            myaccountpage = lpage.Login(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]);
            myaccountpage.IsDisplayed();

            ppage = myaccountpage.ClickTShirts();
            Assert.IsTrue(ppage.IsDisplayed(), "PAge is not dispalyed");
             pdpage = ppage.ClickProductByName("Faded Short Sleeve T-shirts");
            pdpage.IsDisplayed();
            pdpage.EnterQuantityColorSizeandClickAddtoCart("2", "M", "Blue");
            pdpage.CheckproductAttributesAndQuantity("Color", "Blue");
            pdpage.CheckproductAttributesAndQuantity("Quantity", "2");
            scpage = pdpage.ClickProceedToCheckout();
            scpage.IsDisplayed();
            scpage.VerifyUnitTotalPrice("2");
            daPage = scpage.ClickProccedToCheckout();
            daPage.IsDisplayed();
            sPage = daPage.ClickProceedToCheckout();
            sPage.IsDisplayed();
            pmethod = sPage.ClickProceedtoCheckout();
            pmethod.IsDisplayed();
            osPage = pmethod.ClickPayBy("BANKWIRE");
            osPage.IsDisplayed();
            ocPage = osPage.ConfrimOrder();
            ocPage.IsDisplayed();
            var ReferenceNumber = ocPage.GetOrderReferenceNumberforBankWire();
            myaccountpage = ocPage.ClickUserAccount();
            myaccountpage.IsDisplayed();
            ohPage = myaccountpage.ClickOrderHistory();
            ohPage.IsDisplayed();
            Assert.IsTrue(ohPage.CheckReferenceNumberExists(ReferenceNumber).Equals(ReferenceNumber), "Order is not matching");
        }

        [Test]
        [TestCase(Category = "NunitTestCase")]
        public void UpdateAccount()
        {
            hpage = new HomePage();
            hpage.LaunchURL();
            lpage = hpage.ClickSignIn();
            lpage.IsDisplayed();
            myaccountpage = lpage.Login("dummy@gmail.com", "12345");
            myaccountpage.IsDisplayed();
            piPage = myaccountpage.ClickPersonalInformation();
            piPage.IsDisplayed();
            piPage = piPage.UpdatePersonalInformation("TestUser", "12345");
            Assert.IsTrue(piPage.VerifyPersonalInformationUpdatedMessage("Your personal information has been successfully updated.")
                                                                        , "Personal information is updated");
        }

        [SetUp]
        public void Setup()
        {
            BrowserFactory browserFactory = new BrowserFactory();
            string browser = ConfigurationManager.AppSettings["BrowserType"];
            DriverFactory.GetInstance().SetDriver(browserFactory.CreateBrowserInstance(browser));

            DriverFactory.GetInstance().GetDriver().Manage().Window.Maximize();
            DriverFactory.GetInstance().GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            DriverFactory.GetInstance().CloseBrowser();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;

namespace Com.Test.Venkatesh.Pages
{
    public class OrderConfirmationPage : BasePage
    {
        
        IWebElement pageHeading => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".page-heading"));
      //  IWebElement pageSubHeading => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector("page-subheading"));

        IWebElement orderReference => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".box"));
        IWebElement backToOrders => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".button-exclusive.btn.btn-default"));
        IWebElement userAccount => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".header_user_info a.account"));
        public OrderConfirmationPage(IWebDriver driver ) : base(driver)
        {
           
        }

        public override bool IsDisplayed()
        {
            return wait.Until((d) => pageHeading.Displayed &&
                                     pageHeading.Text.Equals("ORDER CONFIRMATION"));
        }

        public string GetOrderReferenceNumberforCheque()
        {
            var orderRef = orderReference.Text;

            var start = orderRef.IndexOf("reference ", 0) + 9;
            var end = orderRef.IndexOf(".\r\n-", start);
            var oref = orderRef.Substring(start, end - start);
            Console.WriteLine(oref);
            return oref.Trim();
        }
        public string GetOrderReferenceNumberforBankWire()
        {
            var orderRef = orderReference.Text;

            var start = orderRef.IndexOf("reference ", 0) + 9;
            var end = orderRef.IndexOf("in", start);
            var oref = orderRef.Substring(start, end - start);
            Console.WriteLine(oref);
            return oref.Trim();
        }


        public MyAccountPage ClickUserAccount()
        {
            userAccount.Click();
            return new MyAccountPage(DriverFactory.GetInstance().GetDriver());
        }
        
        public OrderHistoryPage ClickBacktoOrders()
        {
            backToOrders.Click();
            return new OrderHistoryPage(DriverFactory.GetInstance().GetDriver());
        }

    }
}

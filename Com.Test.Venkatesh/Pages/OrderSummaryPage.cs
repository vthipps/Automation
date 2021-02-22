using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;

namespace Com.Test.Venkatesh.Pages
{
    public class OrderSummaryPage : BasePage
    {
        private string paymentType;
        IWebElement pageHeading => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".page-heading"));

        IWebElement pageSubHeading => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector("page-subheading"));
        IWebElement orderConfrim => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector("button.btn.btn-default.button-medium"));
        public OrderSummaryPage(IWebDriver driver, string pType) : base(driver)
        {
            paymentType = pType;
        }

        public override bool IsDisplayed()
        {
            return wait.Until((d) => pageHeading.Displayed && pageHeading.Text.Equals("ORDER SUMMARY") );
        }

        public OrderConfirmationPage ConfrimOrder()
        {
            orderConfrim.Click();
            return new OrderConfirmationPage(DriverFactory.GetInstance().GetDriver());
            //switch (PaymentType)
            //{
            //    case "BANKWIRE":
                    
                   
            //    case "CHEQUE":
                    
            //        return new OrderConfirmationPage(DriverFactory.GetInstance().GetDriver());
            //    default:
            //        return null;
            //}
        }
            
    }
}

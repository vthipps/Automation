using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;

namespace Com.Test.Venkatesh.Pages
{
    public class PaymentMethod : BasePage
    {
        IWebElement pageHeading => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".page-heading"));
        IWebElement payByBankWire => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".bankwire"));
        IWebElement payByCheque => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".cheque"));

        public PaymentMethod(IWebDriver driver) : base(driver)
        {
        }

        public override bool IsDisplayed()
        {
            return wait.Until((d) => pageHeading.Displayed && pageHeading.Text.Equals("PLEASE CHOOSE YOUR PAYMENT METHOD"));
        }


        public OrderSummaryPage ClickPayBy(string PaymentType)
        {
            switch (PaymentType)
            {
                case "BANKWIRE":
                    payByBankWire.Click();
                    return new OrderSummaryPage(DriverFactory.GetInstance().GetDriver(), "BANK-WIRE PAYMENT.");
                case "CHEQUE":
                    payByCheque.Click();
                    return new OrderSummaryPage(DriverFactory.GetInstance().GetDriver(), "CHECK PAYMENT");
                default:
                    return null;
            }
        }

    }
}

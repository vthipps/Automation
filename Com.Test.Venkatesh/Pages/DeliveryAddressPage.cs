using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;

namespace Com.Test.Venkatesh.Pages
{
    public class DeliveryAddressPage : BasePage
    {
        IWebElement pageHeading => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".page-heading"));

        IWebElement proceedToCheckout => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector("button.btn.btn-default.button-medium"));
        public DeliveryAddressPage(IWebDriver driver) : base(driver)
        {
        }

        public override bool IsDisplayed()
        {
            return wait.Until((d) => pageHeading.Displayed && pageHeading.Text.Equals("ADDRESSES"));
        }

        public ShippingPage ClickProceedToCheckout()
        {
            proceedToCheckout.Click();
            return new ShippingPage(DriverFactory.GetInstance().GetDriver());
        }
    }
}

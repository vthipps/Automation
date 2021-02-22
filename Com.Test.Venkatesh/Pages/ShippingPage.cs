using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;

namespace Com.Test.Venkatesh.Pages
{
    public class ShippingPage : BasePage
    {
        IWebElement pageHeading => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".page-heading"));
        IWebElement termsofService => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector("#cgv"));
        IWebElement proceedToCheckout => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".button.btn.btn-default.standard-checkout.button-medium"));

        public ShippingPage(IWebDriver driver) : base(driver)
        {
        }

        public override bool IsDisplayed()
        {
            return wait.Until((d) => pageHeading.Displayed && pageHeading.Text.Equals("SHIPPING"));
        }

        public PaymentMethod ClickProceedtoCheckout()
        {
            termsofService.Click();
            proceedToCheckout.Click();
            return new PaymentMethod(DriverFactory.GetInstance().GetDriver());
        }

    }
}

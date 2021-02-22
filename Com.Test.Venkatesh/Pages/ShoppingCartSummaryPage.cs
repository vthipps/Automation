using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;

namespace Com.Test.Venkatesh.Pages
{
    public class ShoppingCartSummaryPage : BasePage
    {
        IWebElement cart_title => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("cart_title"));

        IWebElement productDescription => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".cart_drscription"));
        IWebElement unitprice => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".cart_unit span.price"));
        IWebElement unitTotal => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".cart_total"));

        IWebElement proceedToCheckout => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".button.btn.btn-default.standard-checkout.button-medium"));

        public ShoppingCartSummaryPage(IWebDriver driver) : base(driver)
        {
        }

        public override bool IsDisplayed()
        {
            return wait.Until((d) => cart_title.Displayed);
        }

        public bool VerifyUnitTotalPrice(string quantity)
        {
            //Get Unit price
            var uprice = Convert.ToDecimal(unitprice.Text.Replace("$", ""));
            var utotal = uprice * Convert.ToInt32(quantity);
            return unitTotal.Text.Replace("$", "").Equals(utotal); 
        }

        public DeliveryAddressPage ClickProccedToCheckout()
        {
            proceedToCheckout.Click();
            return new DeliveryAddressPage(DriverFactory.GetInstance().GetDriver());
        }
    }
}

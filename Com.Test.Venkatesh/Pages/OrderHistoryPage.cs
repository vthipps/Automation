using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;

namespace Com.Test.Venkatesh.Pages
{
    public class OrderHistoryPage : BasePage
    {

        IWebElement pageHeading => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".page-heading.bottom-indent"));
        IList<IWebElement> orderReference => DriverFactory.GetInstance().GetDriver().FindElements(By.CssSelector(".color-myaccount"));
        public OrderHistoryPage(IWebDriver driver) : base(driver)
        {
        }
        
        public override bool IsDisplayed()
        {
            return wait.Until((d) => pageHeading.Displayed && pageHeading.Text.Equals("ORDER HISTORY"));
        }

        public string CheckReferenceNumberExists(string orderNumber)
        {
            var ele = orderReference.First(d => d.Text.Equals(orderNumber));
            return ele.Text;
        }


    }
}

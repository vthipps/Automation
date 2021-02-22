using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;

namespace Com.Test.Venkatesh.Pages
{
    public class MyAccountPage : BasePage
    {
        IWebElement pageHeading => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".page-heading"));
        IWebElement tshirt_Menulink => DriverFactory.GetInstance().GetDriver()
                                        .FindElement(By.XPath("//div[@id='block_top_menu']/ul/li[3]/a[text()='T-shirts']"));

        IWebElement dresses_Menulink => DriverFactory.GetInstance().GetDriver()
                                        .FindElement(By.XPath("//*[@id='block_top_menu']/ul/li[2]/a[text()='Dresses']"));
        IWebElement orderHistoryandDetails => DriverFactory.GetInstance().GetDriver()
                                                .FindElement(By.XPath("//span[text()='Order history and details']"));
        IWebElement myPersonalInformation => DriverFactory.GetInstance().GetDriver()
                                                .FindElement(By.XPath("//span[text()='My personal information']"));
        public MyAccountPage(IWebDriver driver) : base(driver)
        {
        }

        public override bool IsDisplayed()
        {
            return wait.Until((d) => pageHeading.Displayed && pageHeading.Text.Equals("MY ACCOUNT"));
        }

        public OrderHistoryPage ClickOrderHistory()
        {
            orderHistoryandDetails.Click();
            return new OrderHistoryPage(DriverFactory.GetInstance().GetDriver());
        }

        public PersonalInformationPage ClickPersonalInformation()
        {
            myPersonalInformation.Click();
            return new PersonalInformationPage(DriverFactory.GetInstance().GetDriver());
        }
        public ProductsPage ClickTShirts()
        {
            tshirt_Menulink.Click();
            return new ProductsPage(DriverFactory.GetInstance().GetDriver());
        }

        public ProductsPage ClickDresses()
        {
            dresses_Menulink.Click();
            return new ProductsPage(DriverFactory.GetInstance().GetDriver());
        }
    }
}

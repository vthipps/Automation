using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Test.Venkatesh.Pages
{
    public class HomePage 
    {
        
        IWebElement loginlink => DriverFactory.GetInstance().GetDriver().FindElement(By.ClassName("login"));
        IWebElement tshirt_Menulink => 
            DriverFactory.GetInstance().GetDriver().FindElement(By.XPath("//div[@id='block_top_menu']/ul/li[3]/a[text()='T-shirts']"));

        public void LaunchURL()
        {
            DriverFactory.GetInstance().GetDriver().Navigate().GoToUrl(ConfigurationManager.AppSettings["url"]);
        }

        public bool CheckIfLoginExists()
        {
            return true;
        }

        public ProductsPage ClickTShirts()
        {
            tshirt_Menulink.Click();
            return new ProductsPage(DriverFactory.GetInstance().GetDriver());
        }

        public LoginPage ClickSignIn()
        {
            loginlink.Click();
            return new LoginPage(DriverFactory.GetInstance().GetDriver());
        }

        public bool isDisplayed()
        {
            return loginlink.Displayed;
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;

namespace Com.Test.Venkatesh.Pages
{
    public class LoginPage : BasePage
    {
        IWebElement pageHeading => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".page-heading"));
        IWebElement email => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("email"));
        IWebElement pwd => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("passwd"));
        IWebElement SignInBtn => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("SubmitLogin"));


        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

       

        public override bool IsDisplayed()
        {
            return wait.Until((d) => pageHeading.Displayed && pageHeading.Text.Equals("AUTHENTICATION"));
        }

        public MyAccountPage Login(string userName, string password)
        {
            email.Clear();
            email.SendKeys(userName);
            pwd.Clear();
            pwd.SendKeys(password);

            SignInBtn.Click();
            return new MyAccountPage(DriverFactory.GetInstance().GetDriver());

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;

namespace Com.Test.Venkatesh.Pages
{
    public class PersonalInformationPage : BasePage
    {

        IWebElement pageSubHeading => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".page-subheading"));
        IWebElement firstname => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("firstname"));
        IWebElement currentPassword => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("old_passwd"));

        IWebElement saveBtn => DriverFactory.GetInstance().GetDriver().FindElement(By.XPath("//button/span[text()='Save']"));

        IWebElement successMessage => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".alert.alert-success"));

        public PersonalInformationPage(IWebDriver driver) : base(driver)
        {
        }

      
        public override bool IsDisplayed()
        {
            return wait.Until((d) => pageSubHeading.Displayed);
        }

        // This method will be updating becuase atpresent we are mofifying only Firstname 
        public PersonalInformationPage UpdatePersonalInformation(string firstName,string currentPwd)
        {
            firstname.Clear();
            firstname.SendKeys(firstName);
            currentPassword.Clear();
            currentPassword.SendKeys(currentPwd);
            saveBtn.Click();
            return new PersonalInformationPage(DriverFactory.GetInstance().GetDriver());
        }

        public bool VerifyPersonalInformationUpdatedMessage(string successMsg)
        {
            return successMessage.Text.Equals(successMsg);
        }

    }
}

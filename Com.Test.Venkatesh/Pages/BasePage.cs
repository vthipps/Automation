using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Test.Venkatesh.Pages
{
    public abstract class BasePage
    {
        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public abstract Boolean IsDisplayed();
    }
}

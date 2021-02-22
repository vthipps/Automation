using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Test.Venkatesh.TestBase
{
    public class BrowserFactory
    {
        public IWebDriver CreateBrowserInstance(string browser)
        {
            RemoteWebDriver driver = null;


            switch (browser.ToUpper())
            {
                case "CHROME":
                    ChromeOptions options = new ChromeOptions();
                    driver = new ChromeDriver(options);
                    return driver;
                case "FIREFOX":
                    driver = new FirefoxDriver();
                    return driver;

                case "IE":
                    driver = new InternetExplorerDriver();
                    return driver;
                default:
                    throw new ArgumentException($"Browser not yet implemented : {browser}");
            }
            
        }

    }
}

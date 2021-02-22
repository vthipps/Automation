using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Test.Venkatesh.TestBase
{
    public class DriverFactory
    {
        private DriverFactory()
        {

        }

        private static DriverFactory instance = new DriverFactory();

        public static DriverFactory GetInstance()
        {
            return instance;
        }

        ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        
        public IWebDriver GetDriver()
        {
            return driver.Value;
        }

        public void SetDriver(IWebDriver driverParm)
        {
            driver.Value = driverParm;
        }

        public void CloseBrowser()
        {
            driver.Value.Quit();
            //driver.Dispose();
        }
    }
}

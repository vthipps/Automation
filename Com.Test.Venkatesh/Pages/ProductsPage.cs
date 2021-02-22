using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Test.Venkatesh.Pages
{
    public class ProductsPage : BasePage
    {
        public ProductsPage(IWebDriver driver):base(driver)
        {
            
        }
        IWebElement sortOrder => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("selectProductSort"));

        IList<IWebElement> product => DriverFactory.GetInstance().GetDriver().FindElements(By.CssSelector(".product_img_link img"));

        IWebElement quickView => DriverFactory.GetInstance().GetDriver().FindElement(By.ClassName("quick-view"));

        IWebElement pageHeader => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector(".page-heading.product-listing span.cat-name"));

        private IWebElement ProductElement(string productName)
        {
            return product.SingleOrDefault(iw => iw.GetAttribute("title").Equals(productName));
        }
        public ProductDetailPage ClickProductByName(string productName)
        {
            var ele = ProductElement(productName);
            Actions action = new Actions(DriverFactory.GetInstance().GetDriver());
            action.MoveToElement(ele, 0, 10).Click().Build().Perform();
            return new ProductDetailPage(DriverFactory.GetInstance().GetDriver());
        }

        public void ClickQuickView(string productName)
        {
            var ele = ProductElement(productName);
            Actions action = new Actions(DriverFactory.GetInstance().GetDriver());
            action.MoveToElement(ele).Build().Perform();
            quickView.Click();
        }

        
        public override bool IsDisplayed()
        {
            return wait.Until((d) => this.pageHeader.Displayed);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Test.Venkatesh.TestBase;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Com.Test.Venkatesh.Pages
{
    public class ProductDetailPage : BasePage
    {
        IWebElement productTitle => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector("h1"));

        IWebElement quantity => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("quantity_wanted"));
        IWebElement size => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("group_1"));

        IList<IWebElement> colour => DriverFactory.GetInstance().GetDriver().FindElements(By.CssSelector("#color_to_pick_list li a"));

        IWebElement submitButton => DriverFactory.GetInstance().GetDriver().FindElement(By.CssSelector("#add_to_cart > button"));

        IWebElement layerCart => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("layer_cart"));

        IWebElement layer_cart_product_title => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("layer_cart_product_title"));
        IWebElement layer_cart_product_attributes => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("layer_cart_product_attributes"));
        IWebElement layer_cart_product_quantity => DriverFactory.GetInstance().GetDriver().FindElement(By.Id("layer_cart_product_quantity"));
        IWebElement proceedToCheckout => DriverFactory.GetInstance().GetDriver().FindElement(By.XPath("//a[@title='Proceed to checkout']"));

        public ProductDetailPage(IWebDriver driver) : base(driver)
        {
        }

        public override bool IsDisplayed()
        {
            return wait.Until((d) => this.productTitle.Displayed);
        }

        private void SelectColor(string color)
        {
            colour.SingleOrDefault(iw => iw.GetAttribute("name").Equals(color)).Click();
        }

        private void EnterQuantity(string numnber)
        {
            quantity.Clear();
            quantity.SendKeys(numnber);
        }

        private void SelectSize(string sizevalue)
        {
            SelectElement ddl = new SelectElement(size);
            ddl.SelectByText(sizevalue);
        }

        public void EnterQuantityColorSizeandClickAddtoCart(string quantity, string size, string color)
        {
            EnterQuantity(quantity);
            SelectSize(size);
            SelectColor(color);
            ClickAddtoCart();
            wait.Until((d) => layerCart.Displayed);
        }

        private void ClickAddtoCart()
        {
            submitButton.Click();
        }

        public bool CheckproductAttributesAndQuantity(string attributeType, string attributeValue)
        {
            switch (attributeType.ToUpper())
            {
                case "COLOR":
                    return layer_cart_product_attributes.Text.Split(',')[0].Equals(attributeValue);
                case "QUANTITY":
                    return layer_cart_product_quantity.Text.Equals(attributeValue);
                default:
                    return false;
            }
        } 

        public ShoppingCartSummaryPage ClickProceedToCheckout()
        {
            proceedToCheckout.Click();
            return new ShoppingCartSummaryPage(DriverFactory.GetInstance().GetDriver());
        }
    }
}

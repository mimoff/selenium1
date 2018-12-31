using System;
using OpenQA.Selenium;

namespace Task19
{
    internal class ProductPage : Page
    {
        public ProductPage(IWebDriver driver) : base(driver) { }

        internal int GetCartQuantity()
        {
            return Convert.ToInt32(driver.FindElement(By.XPath("//span[@class='quantity']")).Text);
        }

        internal void SelectSize(string size)
        {
            var optionSize = driver.FindElements(By.XPath("//select[@name='options[Size]']"));
            if (optionSize.Count > 0)
                optionSize[0].SendKeys(size);
        }

        internal void AddProductToCart()
        {
            int quantity = GetCartQuantity();
            driver.FindElement(By.XPath("//button[@name='add_cart_product']")).Click();

            wait.Until(d => GetCartQuantity() != quantity);
        }

    }
}
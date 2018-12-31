using System;
using OpenQA.Selenium;

namespace Task19
{
    internal class CartPage : Page
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        internal CartPage Open()
        {
            driver.Url = "http://localhost/litecart/en/checkout";
            return this;
        }

        internal int GetCartRowsCount()
        {
            return driver.FindElements(By.XPath("//td[@class='item']")).Count;
        }

        internal void RemoveProductFromCart()
        {
            // click на первый shortcut, чтобы остановить ротацию товаров
            var shortCuts = driver.FindElements(By.XPath("//li[@class='shortcut']"));
            if (shortCuts.Count > 0)
                shortCuts[0].Click();

            int rows = GetCartRowsCount();
            // кнопка Remove
            driver.FindElement(By.XPath("//button[@name='remove_cart_item']")).Click();

            // ожидаем изменения таблицы
            wait.Until(d => GetCartRowsCount() != rows);
        }
    }
}
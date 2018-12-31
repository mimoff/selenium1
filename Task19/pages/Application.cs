using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Task19
{
    public class Application
    {
        private IWebDriver driver;

        private MainPage mainPage;
        private ProductPage productPage;
        private CartPage cartPage;

        public Application()
        {
            driver = new ChromeDriver();
            mainPage = new MainPage(driver);
            productPage = new ProductPage(driver);
            cartPage = new CartPage(driver);
        }

        public void Quit()
        {
            driver.Quit();
        }

        public void OpenMainPage()
        {
            mainPage.Open();
        }

        public int GetCartQuantity()
        {
            return productPage.GetCartQuantity();
        }

        public void OpenProduct(int n)
        {
            mainPage.GetProductList()[n].Click();
        }
        public void AddProductToCart()
        {
            // если у товара есть Size, выбираем Small
            productPage.SelectSize("Small");

            // кнопка Add to Cart
            productPage.AddProductToCart();
        }
        public void OpenCart()
        {
            cartPage.Open();
        }
        public bool IsEmptyCart()
        {
            return cartPage.GetCartRowsCount() == 0;
        }
        public void RemoveProductFromCart()
        {
            cartPage.RemoveProductFromCart();
        }
    }
}
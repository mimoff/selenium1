using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Task19
{
    internal class MainPage : Page
    {
        public MainPage(IWebDriver driver) : base(driver) { }

        internal MainPage Open()
        {
            driver.Url = "http://localhost/litecart/";
            return this;
        }

        internal IList<IWebElement> GetProductList()
        {
            return driver.FindElements(By.XPath("//li[contains(@class,'product')]"));
        }
    }
}
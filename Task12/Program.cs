using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace Task12
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                driver.Url = "http://localhost/litecart/admin/";
                driver.FindElement(By.Name("username")).SendKeys("admin");
                driver.FindElement(By.Name("password")).SendKeys("admin");
                driver.FindElement(By.Name("login")).Click();
                // проверка, что страница загрузилась
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#body-wrapper")));

                string product = "New Duck " + Guid.NewGuid().ToString();
                // создание товара
                driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog";
                driver.FindElement(By.XPath("//a[@class='button'][2]")).Click();

                // tab General
                driver.FindElement(By.XPath("//input[@name='status'][@value='1']")).Click();
                driver.FindElement(By.XPath("//input[@name='name[en]']")).SendKeys(product);
                driver.FindElement(By.XPath("//input[@name='code']")).SendKeys("12345");
                driver.FindElement(By.XPath("//input[@name='product_groups[]'][@value='1-3']")).Click();
                driver.FindElement(By.XPath("//input[@name='quantity']")).Clear();
                driver.FindElement(By.XPath("//input[@name='quantity']")).SendKeys("100");

                FileInfo fi = new FileInfo("duck-image.jpg");
                driver.FindElement(By.XPath("//input[@name='new_images[]']")).SendKeys(fi.FullName);

                driver.FindElement(By.XPath("//input[@name='date_valid_from']")).SendKeys("01122018");
                driver.FindElement(By.XPath("//input[@name='date_valid_to']")).SendKeys("31122018");

                // tab Information
                driver.FindElement(By.XPath("//a[@href='#tab-information']")).Click();
                Thread.Sleep(500);

                driver.FindElement(By.XPath("//select[@name='manufacturer_id']")).SendKeys("ACME Corp.");
                driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("aaaaa");
                driver.FindElement(By.XPath("//input[@name='short_description[en]']")).SendKeys("aaaaa");
                driver.FindElement(By.XPath("//textarea[@name='description[en]']")).SendKeys("aaaaa");
                driver.FindElement(By.XPath("//input[@name='head_title[en]']")).SendKeys("aaaaa");
                driver.FindElement(By.XPath("//input[@name='meta_description[en]']")).SendKeys("aaaaa");

                // tab Prices
                driver.FindElement(By.XPath("//a[@href='#tab-prices']")).Click();
                Thread.Sleep(500);

                driver.FindElement(By.XPath("//input[@name='purchase_price']")).SendKeys("10");
                driver.FindElement(By.XPath("//select[@name='purchase_price_currency_code']")).SendKeys("Euros");
                driver.FindElement(By.XPath("//input[@name='prices[USD]']")).SendKeys("100");
                driver.FindElement(By.XPath("//input[@name='prices[EUR]']")).SendKeys("100");

                // создание аккаунта
                Console.WriteLine("Press any key to create");
                Console.ReadKey(true);
                driver.FindElement(By.XPath("//button[@name='save']")).Click();

                // поиск товара
                bool found = false;
                foreach (var el in driver.FindElements(By.XPath("//table[@class='dataTable']/tbody/tr/td[3]")))
                    if (el.Text == product)
                        found = true;
                Console.WriteLine("Product {0} is found : {1}", product, found);
            }
            finally
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey(true);
                driver.Quit();
                driver = null;
            }
        }
    }
}

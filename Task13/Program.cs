using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task13
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                while(true)
                {
                    driver.Url = "http://localhost/litecart/";
                    driver.FindElement(By.XPath("//li[contains(@class,'product')]")).Click();

                    // если у товара есть Size, выбираем Small
                    var optionSize = driver.FindElements(By.XPath("//select[@name='options[Size]']"));
                    if(optionSize.Count() > 0)
                        optionSize[0].SendKeys("Small");

                    // кнопка Add to Cart
                    driver.FindElement(By.XPath("//button[@name='add_cart_product']")).Click();

                    // проверяем изменение кол-ва товаров в корзине
                    var quantityEl = driver.FindElement(By.XPath("//span[@class='quantity']"));
                    string quantity = quantityEl.Text;
                    wait.Until(d => quantityEl.Text != quantity);
                    if (Convert.ToInt32(quantityEl.Text) >= 3)
                        break;
                } 

                driver.Url = "http://localhost/litecart/en/checkout";

                while (true)
                {
                    var orderTable = driver.FindElements(By.XPath("//div[@id='order_confirmation-wrapper']"));
                    if (orderTable.Count() == 0)
                        break; // таблицы нет на странице, все удалили

                    // click на первый shortcut, чтобы остановить ротацию товаров
                    var shortCuts = driver.FindElements(By.XPath("//li[@class='shortcut']"));
                    if (shortCuts.Count() > 0)
                        shortCuts[0].Click();

                    // кнопка Remove
                    driver.FindElement(By.XPath("//button[@name='remove_cart_item']")).Click();

                    // ожидаем исчезновения таблицы
                    wait.Until(ExpectedConditions.StalenessOf(orderTable[0]));
                }
            }
            finally
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}

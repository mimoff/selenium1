using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task7
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

                int cnt = driver.FindElements(By.XPath("//li[@id='app-']")).Count;
                for (int i=1; i <= cnt; i++)
                {
                    driver.FindElement(By.XPath("//li[@id='app-']["+i.ToString()+"]")).Click();
                    CheckPresent(driver, "h1");

                    int cnt2 = driver.FindElements(By.XPath("//li[@id='app-']//li")).Count;
                    for (int j = 1; j <= cnt2; j++)
                    {
                        driver.FindElement(By.XPath("//li[@id='app-']//li[" + j.ToString() + "]")).Click();
                        CheckPresent(driver, "h1");
                        // задержка для визуализации
                        Thread.Sleep(300);
                    }
                }
            }
            finally
            {
                driver.Quit();
                driver = null;
            }
        }

        static void CheckPresent(IWebDriver driver, string tag)
        {
            if (driver.FindElements(By.TagName(tag)).Count > 0)
                Console.WriteLine(tag + " is present on page " + driver.Url);
            else
                Console.WriteLine(tag + " is NOT present on page " + driver.Url);
        }
    }
}

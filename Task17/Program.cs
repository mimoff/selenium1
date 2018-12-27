using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task17
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

                driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";

                var urls = new List<string>();
                foreach (var el in driver.FindElements(By.XPath("//td[img]/a")))
                    urls.Add(el.GetAttribute("href"));

                // очищаем лог
                driver.Manage().Logs.GetLog("browser");

                foreach (string url in urls)
                {
                    driver.Url = url;
                    Console.WriteLine("url: " + url);

                    var logs = driver.Manage().Logs.GetLog("browser");
                    Console.WriteLine(logs.Count > 0 ? "ARTICLE HAS LOG" : "no log");

                    foreach (LogEntry l in logs)
                        Console.WriteLine(l);
                }
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

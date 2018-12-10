using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task9
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

                // 1.a
                driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";

                string prev = "";
                var urls = new List<string>();
                foreach (var row in driver.FindElements(By.XPath("//tr[@class='row']")))
                {
                    var el = row.FindElement(By.XPath("./td[5]"));
                    string s = el.Text;
                    Console.WriteLine("country: " + s);
                    // проверка алфавитного порядка
                    if (s.CompareTo(prev) < 0)
                        Console.WriteLine("wrong country order: " + s);
                    prev = s;

                    // сбор url, где есть зоны
                    if ( row.FindElement(By.XPath("./td[6]")).Text != "0")
                        urls.Add(row.FindElement(By.XPath("./td[5]/a")).GetAttribute("href"));

                }

                // 1 b
                foreach (string url in urls)
                {
                    driver.Url = url;
                    Console.WriteLine("url: " + url);
                    prev = "";
                    var e = driver.FindElements(By.XPath("//table[@class='dataTable']/tbody/tr/td[3][input[@type='hidden']]"));
                    foreach (var el in e)
                    {
                        string s = el.Text;
                        Console.WriteLine("zone: " + s);
                        // проверка алфавитного порядка
                        if (s.CompareTo(prev) < 0)
                            Console.WriteLine("wrong zone order: " + s);
                        prev = s;
                    }
                }

                // 2
                driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

                urls.Clear();

                foreach (var row in driver.FindElements(By.XPath("//table[@class='dataTable']/tbody/tr/td[5]/a")))
                    urls.Add(row.GetAttribute("href"));

                foreach (string url in urls)
                {
                    driver.Url = url;
                    Console.WriteLine("url: " + url);
                    prev = "";
                    var e = driver.FindElements(By.XPath("//select[contains(@name,'zone_code')]/option[@selected]"));
                    foreach (var el in e)
                    {
                        string s = el.Text;
                        Console.WriteLine("zone: " + s);
                        // проверка алфавитного порядка
                        if (s.CompareTo(prev) < 0)
                            Console.WriteLine("wrong zone order: " + s);
                        prev = s;
                    }
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

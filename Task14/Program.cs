using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace Task14
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

                driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";

                // редактирование страны
                driver.FindElement(By.XPath("//tr[@class='row'][1]/td[7]")).Click();

                string mainWindow = driver.CurrentWindowHandle;
                ICollection<string> oldWindows = driver.WindowHandles;
                foreach (var link in driver.FindElements(By.XPath("//a[i[@class='fa fa-external-link']]")))
                {
                    Thread.Sleep(1000); // для наглядности
                    link.Click();
                    string newWindow = wait.Until(ThereIsWindowOtherThan(oldWindows));
                    driver.SwitchTo().Window(newWindow);
                    Thread.Sleep(1000); // для наглядности
                    driver.Close();
                    driver.SwitchTo().Window(mainWindow);
                }
            }
            finally
            {
                driver.Quit();
                driver = null;
            }
        }
        static Func<IWebDriver, string> ThereIsWindowOtherThan(ICollection<string> oldWindows)
        {
            return driver =>
            {
                var newWindows = driver.WindowHandles.Except(oldWindows);
                return newWindows.Count() > 0 ? newWindows.First() : null;
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            //IWebDriver chromeDriver = new ChromeDriver();
            IWebDriver ieDriver = new InternetExplorerDriver();
            //IWebDriver firefoxDriver = new FirefoxDriver();

            //runDriver(chromeDriver);
            //Console.ReadKey(true);
            runDriver(ieDriver);
            //Console.ReadKey(true);
            //runDriver(firefoxDriver);
        }
        static void runDriver(IWebDriver driver)
        {
            //IWebDriver driver = new ChromeDriver();
            try
            {
                driver.Url = "http://localhost/litecart/admin/";
                driver.FindElement(By.Name("username")).SendKeys("admin");
                driver.FindElement(By.Name("password")).SendKeys("admin");
                driver.FindElement(By.Name("login")).Click();
            }
            finally
            {
                Console.ReadKey(true);
                driver.Quit();
                driver = null;
            }
        }
    }
}

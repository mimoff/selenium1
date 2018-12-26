using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;


namespace Task15
{
    class Program
    {
        static void Main(string[] args)
        {
            //IWebDriver driver = new ChromeDriver();
            var caps = new DesiredCapabilities();
            caps.SetCapability(CapabilityType.BrowserName, "chrome");
            //caps.SetCapability(CapabilityType.Version, "53.0");
            //caps.SetCapability(CapabilityType.Platform, "Windows 10");
            IWebDriver driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), caps);

            string url = "https://news.yandex.ru/index.html";
            string template = "//h2[@class='story__title']";
            //url = "https://www.kolesa.ru/news";
            //template = "//h6";
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                Console.WriteLine("start load " + url);
                driver.Url = url;
                Console.WriteLine("loaded");

                foreach (IWebElement el in driver.FindElements(By.XPath(template)))
                    Console.WriteLine(el.Text);
            }
            finally
            {
                Console.WriteLine("press any key");
                Console.ReadKey(true);
                driver.Quit();
                driver = null;
            }
        }
    }
}

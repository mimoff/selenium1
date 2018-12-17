using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Headless
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = null;// = new ChromeDriver();
            string url = "https://news.yandex.ru/index.html";
            string template = "//h2[@class='story__title']";
            url = "https://www.kolesa.ru/news";
            template = "//h6";
            try
            {
                ChromeOptions options = new ChromeOptions();
                //options.setHeadless(true);
                options.AddArgument("--headless");
                //options.AddArgument("--disable-gpu");
                //options.AddArgument("--disable-software-rasterizer");
                //options.AddArgument("--window-size=1920x1080");
                //options.AddArgument("--no-sandbox");
                //options.AddArgument("--disable-dev-shm-usage");
                driver = new ChromeDriver(options);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                Console.WriteLine("start load " + url);
                driver.Url = url;
                Console.WriteLine("loaded");

                Screenshot s1 = ((ITakesScreenshot)driver).GetScreenshot();
                s1.SaveAsFile("test.png", ScreenshotImageFormat.Png);

                foreach( IWebElement el in driver.FindElements(By.XPath(template)) )
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

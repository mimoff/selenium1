using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Task18
{
    class Program
    {
        static void Main(string[] args)
        {
            Proxy proxy = new Proxy();
            proxy.Kind = ProxyKind.Manual;
            proxy.HttpProxy = "localhost:8080";
            ChromeOptions options = new ChromeOptions();
            options.Proxy = proxy;
            IWebDriver driver = new ChromeDriver(options);

            driver.Url = "http://hmn.ru/";
            Console.WriteLine("press any key");
            Console.ReadKey(true);
            driver.Quit();
            driver = null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Task8
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                driver.Url = "http://localhost/litecart/";

                foreach (var el in driver.FindElements(By.XPath("//li[@class='product column shadow hover-light']")))
                    Console.WriteLine((el.FindElements(By.XPath(".//div[contains(@class,'sticker')]")).Count==1).ToString());
            }
            finally
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}

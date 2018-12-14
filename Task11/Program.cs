using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Task11
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            //IWebDriver driver = new InternetExplorerDriver();
            //IWebDriver driver = new FirefoxDriver();

            string email = Guid.NewGuid().ToString() + "@a.com";
            string password = "12345";
            try
            {
                driver.Url = "http://localhost/litecart/en/create_account";

                // заполнение полей
                driver.FindElement(By.XPath("//input[@name='firstname']")).SendKeys("aaaaa");
                driver.FindElement(By.XPath("//input[@name='lastname']")).SendKeys(email);
                driver.FindElement(By.XPath("//input[@name='address1']")).SendKeys("aaaaa");
                driver.FindElement(By.XPath("//input[@name='postcode']")).SendKeys("12345");
                driver.FindElement(By.XPath("//input[@name='city']")).SendKeys("aaaaa");
                driver.FindElement(By.XPath("//select[@name='country_code']")).SendKeys("United States");
                driver.FindElement(By.XPath("//input[@name='email']")).SendKeys(email);
                driver.FindElement(By.XPath("//input[@name='phone']")).SendKeys("+1234567890");
                driver.FindElement(By.XPath("//input[@name='password']")).SendKeys(password);
                driver.FindElement(By.XPath("//input[@name='confirmed_password']")).SendKeys(password);
                Console.WriteLine("Press any key to create account");
                Console.ReadKey(true);

                // создание аккаунта
                driver.FindElement(By.XPath("//button[@name='create_account']")).Click();
                Console.WriteLine("Press any key to logout");
                Console.ReadKey(true);

                // logout
                //driver.Url = "http://localhost/litecart/en/logout";
                driver.FindElement(By.XPath("//div[@id='box-account']//li[4]/a")).Click();
                driver.FindElement(By.XPath("//input[@name='email']")).SendKeys(email);
                driver.FindElement(By.XPath("//input[@name='password']")).SendKeys(password);
                Console.WriteLine("Press any key to login");
                Console.ReadKey(true);

                // вход
                driver.FindElement(By.XPath("//button[@name='login']")).Click();
                Console.WriteLine("Press any key to logout");
                Console.ReadKey(true);

                // logout
                //driver.Url = "http://localhost/litecart/en/logout";
                driver.FindElement(By.XPath("//div[@id='box-account']//li[4]/a")).Click();
                Console.WriteLine("Press any key to exit");
                Console.ReadKey(true);
            }
            finally
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}

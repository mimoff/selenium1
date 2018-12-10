using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Task10
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            //IWebDriver driver = new InternetExplorerDriver();
            //IWebDriver driver = new FirefoxDriver();

            try
            {
                driver.Url = "http://localhost/litecart/";

                var el = driver.FindElement(By.XPath("//div[@id='box-campaigns']//a[@class='link']"));
                string title1 = el.FindElement(By.XPath(".//div[@class='name']")).Text;
                var elPR1 = el.FindElement(By.XPath(".//s[@class='regular-price']"));
                int priceRegular1 = Convert.ToInt32(elPR1.Text.Replace("$", ""));
                string colorRegular1 = elPR1.GetCssValue("color");
                string textdecoRegular1 = elPR1.GetCssValue("text-decoration-line");

                var elPC1 = el.FindElement(By.XPath(".//strong[@class='campaign-price']"));
                int priceCampaign1 = Convert.ToInt32(elPC1.Text.Replace("$", ""));
                string colorCampaign1 = elPC1.GetCssValue("color");
                string fontweightCampaign1 = elPC1.GetCssValue("font-weight");

                // переход на страницу товара
                el.Click();
                el = driver.FindElement(By.XPath("//div[@id='box-product']"));

                string title2 = el.FindElement(By.XPath(".//h1[@class='title']")).Text;
                var elPR2 = el.FindElement(By.XPath(".//s[@class='regular-price']"));
                int priceRegular2 = Convert.ToInt32(elPR2.Text.Replace("$", ""));
                string colorRegular2 = elPR2.GetCssValue("color");
                string textdecoRegular2 = elPR2.GetCssValue("text-decoration-line");

                var elPC2 = el.FindElement(By.XPath(".//strong[@class='campaign-price']"));
                int priceCampaign2 = Convert.ToInt32(elPC2.Text.Replace("$", ""));
                string colorCampaign2 = elPC2.GetCssValue("color");
                string fontweightCampaign2 = elPC2.GetCssValue("font-weight");

                // проверки
                //а) на главной странице и на странице товара совпадает текст названия товара
                Console.WriteLine("\ntitle1: {0} title2: {1} \nequal: {2}\n", title1, title2, title1 == title2);

                //б) на главной странице и на странице товара совпадают цены(обычная и акционная)
                Console.WriteLine("priceRegular1: {0} priceRegular2: {1} \nequal: {2}\n", priceRegular1, priceRegular2, priceRegular1 == priceRegular2);
                Console.WriteLine("priceCampaign1: {0} priceCampaign2: {1} \nequal: {2}\n", priceCampaign1, priceCampaign2, priceCampaign1 == priceCampaign2);

                //в) обычная цена зачёркнутая и серая(можно считать, что "серый" цвет это такой, у которого в RGBa представлении одинаковые значения для каналов R, G и B)
                Console.WriteLine("priceRegular1 text-decoration-line: {0} \nstriked: {1}\n", textdecoRegular1, textdecoRegular1 == "line-through");
                Console.WriteLine("priceRegular1 color: {0} \nis gray: {1}\n", colorRegular1, colorIsGray(colorRegular1));
                Console.WriteLine("priceRegular2 text-decoration-line: {0} \nstriked: {1}\n", textdecoRegular2, textdecoRegular2 == "line-through");
                Console.WriteLine("priceRegular2 color: {0} \nis gray: {1}\n", colorRegular1, colorIsGray(colorRegular2));
                
                //г) акционная жирная и красная (можно считать, что "красный" цвет это такой, у которого в RGBa представлении каналы G и B имеют нулевые значения)
                //(цвета надо проверить на каждой странице независимо, при этом цвета на разных страницах могут не совпадать)
                Console.WriteLine("priceCampaign1 font-weigth: {0} \nbold: {1}\n", fontweightCampaign1, Convert.ToInt32(fontweightCampaign1) >= 700);
                Console.WriteLine("priceCampaign1 color: {0} \nis red: {1}\n", colorCampaign1, colorIsRed(colorCampaign1));
                Console.WriteLine("priceCampaign2 font-weigth: {0} \nbold: {1}\n", fontweightCampaign2, Convert.ToInt32(fontweightCampaign2) >= 700);
                Console.WriteLine("priceCampaign2 color: {0} \nis red: {1}\n", colorCampaign2, colorIsRed(colorCampaign2));

                //д) акционная цена <крупнее> МЕНЬШЕ, чем обычная (это тоже надо проверить на каждой странице независимо)
                Console.WriteLine("priceCampaign1: {0} priceRegular1: {1} \nlesser: {2}\n", priceCampaign1, priceRegular1, priceCampaign1 < priceRegular1);
                Console.WriteLine("priceCampaign2: {0} priceRegular2: {1} \nlesser: {2}\n", priceCampaign2, priceRegular2, priceCampaign2 < priceRegular2);
            }
            finally
            {
                driver.Quit();
                driver = null;
            }
        }

        static char[] sep = new char[] { ',',' ','(',')' };
        static bool colorIsGray(string color)
        {
            string[] s = color.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            return s[1]==s[2] && s[2]==s[3];
        }

        static bool colorIsRed(string color)
        {
            string[] s = color.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            return s[2] == "0" && s[3] == "0";
        }
    }
}

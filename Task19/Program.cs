using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task19
{
    class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();

            do
            {
                app.OpenMainPage();
                app.OpenProduct(0);
                app.AddProductToCart();
            }
            while (app.GetCartQuantity() < 3);

            app.OpenCart();
            while (!app.IsEmptyCart())
                app.RemoveProductFromCart();

            app.Quit();
            app = null;
        }
    }
}

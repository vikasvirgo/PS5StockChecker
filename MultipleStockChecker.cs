using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PS5StockChecker
{
    public class MultipleStockChecker
    {
        public async Task CheckStock()
        {
            var isAvailable = false;
            var unavailablectr = 0;
            var ps5AmazonURL = "https://www.amazon.co.uk/PlayStation-9395003-5-Console/dp/B08H95Y452/ref=sr_1_1?dchild=1&keywords=ps5&qid=1615755401&sr=8-1";
            var ps5ArgosURL = "https://www.argos.co.uk/vp/oos/ps5.html?clickSR=slp:term:ps5%20console:1:2:1";

            var httpClient = new HttpClient();
            var urls = new string[] { ps5AmazonURL, ps5ArgosURL };
            var tasks = new List<Task<string>>();

            foreach (var url in urls)
            {
                tasks.Add(httpClient.GetStringAsync(url));
            }

            Task.WaitAll(tasks.ToArray());

            var data = new List<string> { await tasks[0], await tasks[1] };

            foreach (var content in data)
            {
                if (content.ToString().IndexOf("amazon") > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Checking at Amazon : {unavailablectr}");
                    var isAmazonBasket = content.ToString().IndexOf("Add to Basket");
                    var availableAmazonctr = 0;

                    if (isAmazonBasket != -1)
                    {
                        while (availableAmazonctr <= 10000)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("PS5 Available at Amazon");
                            Console.Beep();
                            availableAmazonctr++;
                        }
                        isAvailable = true;
                        Console.ReadLine();
                    }
                }

                if (content.ToString().IndexOf("argos") > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Checking at Argos : {unavailablectr}");
                    var isArgosBasket = content.ToString().IndexOf("trolley");
                    var availableatArgosctr = 0;

                    if (isArgosBasket != -1)
                    {
                        while (availableatArgosctr <= 10000)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("PS5 Available at Argos");
                            Console.Beep();
                            availableatArgosctr++;
                        }
                        isAvailable = true;
                        Console.ReadLine();
                    }                                                           
                }

                ++unavailablectr;
            }
        }
    }
}

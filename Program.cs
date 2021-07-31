//using PS5StockChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientEx
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var isAvailable = false;
            var unavailablectr = 0;
            //PS5 URLs
            //var ps5AmazonURL = "https://www.amazon.co.uk/PlayStation-9395003-5-Console/dp/B08H95Y452/ref=sr_1_1?dchild=1&keywords=ps5&qid=1615755401&sr=8-1";
            var ps5ArgosURL = "https://www.smythstoys.com/uk/en-gb/video-games-and-tablets/playstation-5/playstation-5-consoles/playstation-5-console/p/191259";

            //Something which is available
            //var ps5AmazonURL = "https://www.amazon.co.uk/RAVPower-Charger-Charging-Stations-6-Port-Black/dp/B07MFPN87Y?ref_=ast_sto_dp&th=1&psc=1";
            //var ps5ArgosURL = "https://www.smythstoys.com/uk/en-gb/video-games-and-tablets/nintendo-switch/nintendo-switch-consoles/nintendo-switch-lite-turquoise-%2b-animal-crossing-%2b-nintendo-switch-online-3-month-membership-bundle/p/195861";


            var httpClient = new HttpClient();
            var urls = new string[] { ps5ArgosURL };            

            while (!isAvailable)
            {
                var tasks = new List<Task<string>>();

                foreach (var url in urls)
                {
                    tasks.Add(httpClient.GetStringAsync(url));
                }

                Task.WaitAll(tasks.ToArray());

                var data = new List<string> { await tasks[0] }; //, await tasks[1]

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

                    if (content.ToString().IndexOf("smyths") > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Checking at Smyths : {unavailablectr}");
                        var isArgosBasket = content.ToString().IndexOf("Add to Basket");
                        var availableatArgosctr = 0;

                        if (isArgosBasket != -1)
                        {
                            while (availableatArgosctr <= 10000)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("PS5 Available at Smyths");
                                Console.Beep();
                                availableatArgosctr++;
                            }
                            isAvailable = true;
                            Console.ReadLine();
                        }
                    }                                 
                }
                ++unavailablectr;
            }
        }
    }
}

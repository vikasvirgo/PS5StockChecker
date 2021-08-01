using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientEx
{
    class Program
    {
        // first change
        //Second Change
        //Third change
        static async Task Main(string[] args)
        {
            var isAvailable = false;
            var unavailablectr = 0;

            while (!isAvailable)
            {     
                
                Thread.Sleep(4000);
                var client = new HttpClient();
                //PS5
                var content = await client.GetStringAsync("https://www.amazon.co.uk/PlayStation-9395003-5-Console/dp/B08H95Y452/ref=sr_1_1?dchild=1&keywords=ps5&qid=1615755401&sr=8-1");
                //var content = await client.GetStringAsync("https://www.smythstoys.com/uk/en-gb/video-games-and-tablets/playstation-5/playstation-5-consoles/playstation-5-console/p/191259");

                // Something available....
                //var content = await client.GetStringAsync("https://www.amazon.co.uk/dp/B08S6LX7T2/ref=sspa_dk_detail_3?psc=1&pd_rd_i=B08S6LX7T2&pd_rd_w=Wm50c&pf_rd_p=871455a8-2081-4ae3-be47-0e6ec29adb28&pd_rd_wg=yDnMr&pf_rd_r=G3RGT73YFE20H59SRZ5Z&pd_rd_r=061cb155-f8f2-4402-bcfd-b50d7ed218be&spLa=ZW5jcnlwdGVkUXVhbGlmaWVyPUEzUzBISEhKMlE1RkRMJmVuY3J5cHRlZElkPUEwNjQ5Mjg0MzFTSURRRUlROTg3VSZlbmNyeXB0ZWRBZElkPUEwMjg3NjAxM0dDNFNHT0lLSFlETCZ3aWRnZXROYW1lPXNwX2RldGFpbCZhY3Rpb249Y2xpY2tSZWRpcmVjdCZkb05vdExvZ0NsaWNrPXRydWU=");
                //var content = await client.GetStringAsync("https://www.argos.co.uk/product/2077921?clickSR=slp:term:switch:1:498:1");

                var askingforCaptcha = content.ToString().IndexOf("not a robot");

                //var isBasket = content.ToString().IndexOf("trolley");
                var isBasket = content.ToString().IndexOf("Add to Basket");
                //var isBasket = content.ToString().IndexOf("Currently unavailable");
                var availablectr = 0;
                var robotctr = 0;
                if (askingforCaptcha > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;                    
                    while (robotctr <= 10000)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Asking for Captcha Visit Amazon and Accept cookies");
                        //System.Media.SystemSounds.Question.Play();
                        Console.Beep();
                        robotctr++;
                    }

                    Console.ReadLine();
                    break;
                }

                if (isBasket != -1)
                {
                    while (availablectr <= 10000)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"PS5 Available at Amazon {DateTime.Now}");
                        Console.Beep();                        
                        availablectr++;
                    }
                    isAvailable = true;
                    Console.ReadLine();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Checking at Amazon : {unavailablectr}");
                    ++unavailablectr;
                }
            }
        }
    }
}

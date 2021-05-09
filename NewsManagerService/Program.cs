using BaseClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewsManager;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //TestNewsManager(args).ConfigureAwait(false).GetAwaiter().GetResult();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseModularStartup<Startup>();
                });

        static async Task TestNewsManager(string[] args)
        {
            Console.WriteLine("--- NewsManager testing ---");
            var manager = new Manager();
            Console.WriteLine("Let's get some news!");
            var answer = new List<NewsArticle>();
            foreach (var site in manager.GetAvailableSites())
            {
                Console.Write($"From {site} ... ");
                answer.AddRange(await manager.GetNewsAsync(site).ConfigureAwait(false));
                Console.WriteLine("Done.");
            }

            Console.Write("Print it? ");
            Console.ReadLine();
            Console.WriteLine();
            foreach (var news in answer)
            {
                Console.WriteLine("---");
                Console.WriteLine("URL: " + news.URL);
                Console.WriteLine("Name: " + news.Name);
                Console.WriteLine("Date: " + news.Date);
                Console.WriteLine("Text: " + news.Text);
                Console.Write("Entities: " + string.Join(" // ", news.Entities));
                Console.WriteLine();
                Console.WriteLine("---");
                Console.WriteLine();
            }
        }
    }
}

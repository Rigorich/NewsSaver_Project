using System;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Pullenti.Sdk.InitializeAll();
            var manager = new ServerManager();
            Console.WriteLine("Let's get some news!");
            var answer = await manager.GetNewsAsync();
            foreach (var news in answer)
            {
                Console.WriteLine("---");
                Console.WriteLine("URL: " + news.URL);
                Console.WriteLine("Name: " + news.Name);
                Console.WriteLine("Date: " + news.Date);
                Console.WriteLine("Text: " + news.Text);
                Console.WriteLine("Entities:");
                foreach (var entity in news.Entities)
                {
                    Console.WriteLine(entity);
                }
                Console.WriteLine("---");
            }
        }
    }
}

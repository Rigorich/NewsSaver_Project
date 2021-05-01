using GeneralClasses;
using NewsManager.Instructions;
using NewsManager.Instructions.Available;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NewsManager
{
    class Program
    {   
        /*
        class TestNM : NewsMaker
        {
            public TestNM() : base(new SpecificInstruction_()) { }
            public NewsArticle EntityThis(InternetPage page)
            {
                var news = ParsePage(page);
                news.Entities = AnalyzeText(news.Text);
                return news;
            }
        }
        */
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var manager = new Manager();
            Console.WriteLine("Let's get some news!");
            var answer = new List<NewsArticle>(); 
            foreach (var instr in AllInstructions.List)
            {
                Console.Write($"From {instr.MainPageUrl} ... ");
                answer.AddRange(await manager.GetNewsAsync(instr).ConfigureAwait(false));
                Console.WriteLine("Done.");
            }

            //string url = "TEST";
            //string html;
            //string path = @"article.html";
            //using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            //{
            //    var sr = new StreamReader(fs);
            //    html = sr.ReadToEnd();
            //    sr.Close();
            //}
            //var page = new InternetPage(url, html);
            //var news = new TestNM().EntityThis(page);

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
                Console.Write("Entities: ");
                foreach (var entity in news.Entities)
                {
                    Console.Write(entity + ", ");
                }
                Console.WriteLine();
                Console.WriteLine("XmlEntities: " + news.XmlEntities);
                Console.WriteLine("---");
                Console.WriteLine();
            }
        }
    }
}

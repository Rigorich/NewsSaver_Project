using GeneralClasses;
using NewsManager.Instructions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NewsManager
{
    class Program
    {
        class TestNM : NewsMaker
        {
            public TestNM() : base(new SpecificInstruction_1()) { }
            public NewsArticle EntityThis(InternetPage page)
            {
                var news = ParsePage(page);
                news.Entities = AnalyzeText(news.Text);
                return news;
            }
        }
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var manager = new Manager();
            var instruction = new SpecificInstruction_1();
            Console.WriteLine("Let's get some news!");
            var answer = await manager.GetNewsAsync(instruction, 10);

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
                Console.WriteLine("XmlEntities: " + news.XmlEntities);
                Console.WriteLine("---");
            }
        }
    }
}

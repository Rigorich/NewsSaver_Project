using System;
using System.Linq;
using ServiceStack;
using BaseClasses;
using ServerService;

namespace TestClient
{
    public static class NewsExtension
    {
        public static string Formatted(this NewsArticle article)
        {
            return article.Name + " (" + article.Date + ") " + article.URL;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Tester! (Press Enter)");
            Console.ReadLine();
            var server = new JsonServiceClient("http://localhost:5000");

            try
            {
                Console.WriteLine("Let's get list of available sites!");
                Console.WriteLine("Please stand by...");
                string[] sites = server.Get(new AvailableSitesRequest()).Result;
                Console.WriteLine(sites.Join(", "));
            }
            catch (WebServiceException ex)
            {
                Console.WriteLine("ERROR - " + ex.StatusCode + " " + ex.ErrorCode + " : " + ex.Message);
                return;
            }


            NewsArticle[] articles;
            
            Console.WriteLine("Let's get some news!");
            Console.WriteLine("Enter site name as part of url or nothing for random news article:");
            string site = Console.ReadLine();
            int count;
            if (string.IsNullOrWhiteSpace(site))
            {
                Console.WriteLine("Random news!");
                site = null;
                count = 1;
            }
            else
            {
                Console.WriteLine("Enter articles count or nothing for 3 news articles:");
                count = Console.ReadLine().ToInt(3);
            }
            Console.WriteLine("Please stand by...");
            
            try
            {
                var response = server.Get(new NewsRequest() { Url = site, Count = count });
                articles = response.ResultNews;
                bool[] saved = response.ResultIsSaved;
                for (int i = 0; i < articles.Length; i++)
                {
                    Console.WriteLine((saved[i] ? "    Saved" : "Not saved") + " <- " + articles[i].URL);
                }
            }
            catch (WebServiceException ex)
            {
                Console.WriteLine("ERROR - " + ex.StatusCode + " " + ex.ErrorCode + " : " + ex.Message);
            }
            
            // FULL LIST
            {
                Console.WriteLine("Continue? (Press Enter)"); Console.ReadLine();
                Console.WriteLine("GET FULL LIST");
                articles = server.Get(new ListRequest()).Result;
                foreach (var article in articles)
                {
                    Console.WriteLine(article.Formatted());
                }
                Console.WriteLine("END FULL LIST");
            }

            // FILTER LIST
            {
                Console.WriteLine("Continue? (Press Enter)"); Console.ReadLine();
                Console.WriteLine("GET FILTER LIST");
                string date, url;
                string[] keywords, entities;
                Console.WriteLine("Enter date:"); date = Console.ReadLine();
                Console.WriteLine("Enter url:"); url = Console.ReadLine();

                static string[] ParseLine(string s)
                {
                    return s.Split(",").Select(w => w.ToLowerInvariant().Trim()).Where(w => !w.IsNullOrEmpty()).ToArray();
                }
                Console.WriteLine("Enter keywords separeted by comma:"); keywords = ParseLine(Console.ReadLine());
                Console.WriteLine("Enter entities separeted by comma:"); entities = ParseLine(Console.ReadLine());

                Console.WriteLine($"URL = {url}");
                Console.WriteLine($"Date = {date}");
                Console.WriteLine($"Keywords = {keywords.Join(", ")}");
                Console.WriteLine($"Entities = {entities.Join(", ")}");

                Console.WriteLine("OK? (Press Enter)");
                Console.ReadLine();

                ListRequest request = new ListRequest() { Url = url, Date = date, Keywords = keywords, Entitities = entities };
                articles = server.Get(request).Result;
                foreach (var article in articles)
                {
                    Console.WriteLine(article.Formatted());
                }
                Console.WriteLine("END FILTER LIST");
            }

            // ONE ARTICLE
            {
                Console.WriteLine("Continue? (Press Enter)"); Console.ReadLine();
                Console.WriteLine("GET ARTICLE");
                Console.WriteLine("Enter url:");
                string url = Console.ReadLine();
                NewsArticle article = server.Get(new ArticleRequest() { Url = url }).Result;
                if (article is null)
                {
                    Console.WriteLine("There is no such article");
                }
                else
                {
                    Console.WriteLine(article.Formatted());
                    Console.WriteLine(article.Text);
                }
                Console.WriteLine("END ARTICLE");
            }

            Console.WriteLine("End of Test program. (Press Enter)"); Console.ReadLine();
        }
    }
}

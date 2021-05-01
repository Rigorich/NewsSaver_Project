using System;
using System.Collections.Generic;
using ServiceStack;
using GeneralClasses;
using ServerService;
using DatabaseService;

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
            Console.WriteLine("Hello Tester!");
            Console.ReadLine();
            var news = new JsonServiceClient("http://localhost:5000");
            var db = new JsonServiceClient("http://localhost:5001");

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
                articles = news.Get(new NewsRequest() { Url = site, Count = count }).Result;
            }
            catch (WebServiceException ex)
            {
                Console.WriteLine("ERROR - " + ex.StatusCode + " " + ex.ErrorCode + " : " + ex.Message);
                return;
            }

            Console.WriteLine("POST ARTICLES");
            foreach (var article in articles)
            {
                Console.WriteLine();
                bool success = false;
                try
                {
                    success = db.Post(new SaveRequest() { Article = article }).Result;
                }
                catch (WebServiceException ex)
                {
                    Console.WriteLine("ERROR - " + ex.StatusCode + " " + ex.ErrorCode + " : " + ex.Message);
                }
                Console.WriteLine(success + " <- " + article.URL);
            }
            Console.WriteLine("\nEND POST ARTICLES");
            Console.ReadLine();


            Console.WriteLine("GET LIST");
            string date, url;
            
            // FULL LIST
            {
                Console.WriteLine("GET FULL LIST");
                Console.ReadLine();
                articles = db.Get(new ListRequest()).Result;
                foreach (var article in articles)
                {
                    Console.WriteLine(article.Formatted());
                }
                Console.WriteLine("END FULL LIST");
                Console.ReadLine();
            }

            // DATE FILTER
            {
                Console.WriteLine("GET DATE LIST");
                Console.WriteLine("Enter date:");
                date = Console.ReadLine();
                articles = db.Get(new ListRequest() { Date = date }).Result;
                foreach (var article in articles)
                {
                    Console.WriteLine(article.Formatted());
                }
                Console.WriteLine("END DATE LIST");
                Console.ReadLine();
            }

            // URL FILTER
            {
                Console.WriteLine("GET URL LIST");
                Console.WriteLine("Enter url:");
                url = Console.ReadLine();
                articles = db.Get(new ListRequest() { Url = url }).Result;
                foreach (var article in articles)
                {
                    Console.WriteLine(article.Formatted());
                }
                Console.WriteLine("END URL LIST");
                Console.ReadLine();
            }

            // URL&DATE FILTER
            {
                Console.WriteLine("GET URL&DATE LIST");
                Console.ReadLine();
                Console.WriteLine($"URL = {url}");
                Console.WriteLine($"Date = {date}");
                articles = db.Get(new ListRequest() { Url = url, Date = date }).Result;
                foreach (var article in articles)
                {
                    Console.WriteLine(article.Formatted());
                }
                Console.WriteLine("END URL&DATE LIST");
                Console.ReadLine();
            }


            Console.WriteLine("\nEND GET LIST");
            Console.ReadLine();

            // ONE ARTICLE
            {
                Console.WriteLine("GET ARTICLE");
                Console.WriteLine("Enter url:");
                url = Console.ReadLine();
                NewsArticle article = db.Get(new ArticleRequest() { Url = url }).Result;
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
                Console.ReadLine();
            }
        }
    }
}

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
            articles = news.Get(new NewsRequest()).Result;

            Console.WriteLine("POST ARTICLES");
            foreach (var article in articles)
            {
                Console.ReadLine();
                var answer = db.Post(new SaveRequest() { Article = article }).Result;
                Console.WriteLine(answer + " <- " + article.Name);
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

            // URL FILTER
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

            // URL&DATE FILTER
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


            Console.WriteLine("\nEND GET LIST");
            Console.ReadLine();
        }
    }
}

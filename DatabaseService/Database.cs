using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ServiceStack;
using ServiceStack.Text;
using static ServiceStack.Text.JsonSerializer;
using BaseClasses;

namespace DatabaseService
{
    public static class Database
    {
        private static readonly string FileName = "TestDatabase.txt";

        static Database()
        {
        }

        public static List<NewsArticle> GetAll()
        {
            List<NewsArticle> News;
            string json = "";
            using (var fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                var sr = new StreamReader(fs);
                json = sr.ReadToEnd();
                sr.Close();
            }
            News = DeserializeFromString< List<NewsArticle> >(json);
            News ??= new List<NewsArticle>();
            return News;
        }

        public static NewsArticle GetOne(string url)
        {
            List<NewsArticle> News = GetAll();
            NewsArticle article;
            try 
            { 
                article = News.Where(art => art.URL == url).LastOrDefault();
            }
            catch
            {
                return null;
            }
            return article;
        }

        static void SaveAll(List<NewsArticle> news)
        {
            using (var fs = new FileStream(FileName, FileMode.Create))
            {
                string json = SerializeToString(news).IndentJson();
                var sw = new StreamWriter(fs);
                sw.Write(json);
                sw.Flush();
                sw.Close();
            }
        }
        
        public static void Add(NewsArticle article)
        {
            List<NewsArticle> News = GetAll();
            if (News.Where(art => art.URL == article.URL && art.Text == article.Text).Count() > 0)
            {
                return;
            }
            News.Add(article);
            SaveAll(News);
        }

        public delegate bool Filter(NewsArticle article);
        public static List<NewsArticle> GetFilteredList(Filter filter)
        {
            List<NewsArticle> AllNews = GetAll();
            List<NewsArticle> FilteredNews = new List<NewsArticle>();
            foreach (NewsArticle article in AllNews)
            {
                if (filter(article))
                {
                    FilteredNews.Add(new NewsArticle(article.URL, null, article.Name, null, article.Date));
                }
            }
            return FilteredNews;
        }
    }
}

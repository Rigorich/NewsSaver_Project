using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeneralClasses;
using ServiceStack;
using ServiceStack.Text;
using static ServiceStack.Text.JsonSerializer;

namespace DatabaseService
{
    public static class Database
    {
        private static readonly string FileName = "TestDatabase.txt";

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
                article = News.Where(art => art.URL == url).Last();
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
        
        public static bool Add(NewsArticle article)
        {
            List<NewsArticle> News = GetAll();
            foreach (var news in News)
            {
                if (news.URL == article.URL && news.Text == article.Text)
                {
                    return false;
                }
            }
            News.Add(article);
            SaveAll(News);
            return true;
        }
        
        public static List<NewsArticle> GetShortList()
        {
            List<NewsArticle> News = GetAll();
            for (int i = 0; i < News.Count; i++)
            {
                News[i] = new NewsArticle(News[i].URL, null, News[i].Name, null, News[i].Date);
            }
            return News;
        }

        public delegate bool Filter(NewsArticle article);
        public static List<NewsArticle> GetFilteredList(Filter filter)
        {
            List<NewsArticle> list = GetShortList();
            List<NewsArticle> FilteredNews = new List<NewsArticle>();
            foreach (var heap in list)
            {
                if (filter(heap))
                {
                    FilteredNews.Add(heap);
                }
            }
            return FilteredNews;
        }
    }
}

using BaseClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ServiceStack;
using ServiceStack.Text;
using static ServiceStack.Text.JsonSerializer;
using ServiceStack.OrmLite;

namespace DatabaseService
{
    public static class Database
    {
        private static readonly string FileName = "ArchiveDatabase.backup";
        private static readonly string ConnectionString = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=test";
        private static readonly OrmLiteConnectionFactory dbFactory;

        static Database()
        {
            dbFactory = new OrmLiteConnectionFactory(ConnectionString, PostgreSqlDialect.Provider);
            using (var db = dbFactory.Open())
            {
                if (db.CreateTableIfNotExists<NewsArticle>())
                {
                    List<NewsArticle> News = LoadFromFile();
                    News = News.OrderBy(art => art.Date).ToList();
                    foreach (NewsArticle article in News)
                    {
                        db.Insert(article);
                    }
                }
                SaveToFile();
                db.ExecuteSql(@"
DROP TABLE IF EXISTS text_analysis
;");
                db.ExecuteSql(@"
CREATE TABLE text_analysis(
id SERIAL PRIMARY KEY,
article_id SERIAL REFERENCES news_article(id),
tsvec tsvector
);");
                db.ExecuteSql(@"
INSERT INTO text_analysis (article_id, tsvec)
SELECT id, to_tsvector(text)
FROM news_article
;");
            }
        }
        static List<NewsArticle> LoadFromFile()
        {
            List<NewsArticle> News;
            {
                string json = "";
                using (var fs = new FileStream(FileName, FileMode.OpenOrCreate))
                {
                    var sr = new StreamReader(fs);
                    json = sr.ReadToEnd();
                    sr.Close();
                }
                News = DeserializeFromString<List<NewsArticle>>(json);
                News ??= new List<NewsArticle>();
            }
            return News;
        }
        static void SaveToFile()
        {
            List<NewsArticle> News;
            using (var db = dbFactory.Open())
            {
                News = db
                    .Select<NewsArticle>()
                    .OrderBy(art => art.Date)
                    .ToList();
            }
            using (var fs = new FileStream(FileName, FileMode.Create))
            {
                string json = SerializeToString(News).IndentJson();
                var sw = new StreamWriter(fs);
                sw.Write(json);
                sw.Flush();
                sw.Close();
            }
        }

        public static void Add(NewsArticle article)
        {
            using (var db = dbFactory.Open())
            {
                if (!db.Exists<NewsArticle>(m => m.URL == article.URL && m.Text == article.Text))
                {
                    db.Insert(article);
                    long articleId = db.LastInsertId();
                    db.ExecuteSql(@"
INSERT INTO text_analysis (article_id, tsvec)
SELECT id, to_tsvector(text)
FROM news_article
WHERE id=" + articleId + @"
;");
                }
            }
        }

        public static NewsArticle GetOne(string url)
        {
            NewsArticle article;
            using (var db = dbFactory.Open())
            {
                article = db
                    .Select<NewsArticle>(art => art.URL == url)
                    .OrderBy(art => art.Date)
                    .LastOrDefault();
            }
            return article;
        }

        public static List<NewsArticle> GetFilteredList(ListRequest request)
        {
            List<NewsArticle> FilteredNews;
            using (var db = dbFactory.Open())
            {
                var q = db.From<NewsArticle>();
                q = q.Where(art => request.LeftBoundDate <= art.Date && art.Date <= request.RightBoundDate);
                q = q.Where(art => art.URL.Contains(request.Url));
                //q = q.Where(request.Keywords);
                //q = q.Where(request.Entitities);
                q = request.OldestFirst ? q.OrderBy(art => art.Date) : q.OrderByDescending(art => art.Date);
                q = q.Limit(request.Skip, request.Count);
                q = q.Select(art => new { art.URL, art.Name, art.Date });
                FilteredNews = db.Select(q).ToList();
            }
            return FilteredNews;
        }
    }
}

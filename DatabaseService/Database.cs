using BaseClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ServiceStack;
using ServiceStack.Text;
using static ServiceStack.Text.JsonSerializer;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.PostgreSQL;

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
            bool empty;
            using (var db = dbFactory.Open())
            {
                empty = db.CreateTableIfNotExists<NewsArticle>();
                if (empty)
                {
                    db.ExecuteSql(@"
DROP TABLE IF EXISTS text_analysis
;");
                    db.ExecuteSql(@"
CREATE TABLE text_analysis(
id SERIAL PRIMARY KEY,
article_id SERIAL REFERENCES news_article(id) ON DELETE CASCADE,
tsvec tsvector
);");
                }
                db.ExecuteSql(@"
CREATE INDEX IF NOT EXISTS url_index ON news_article (url)
;");
                //db.CreateIndex<NewsArticle>(art => art.URL);
                NewsArticle egg = new NewsArticle(
                    "tg: @Rigorich",
                    "-",
                    "Author",
                    "Гришаев Никита Григорьевич",
                    System.Data.SqlTypes.SqlDateTime.MinValue.Value
                );
                if (!db.Exists<NewsArticle>(art => art.URL == egg.URL))
                {
                    db.Insert(egg);
                }
            }
            if (empty)
            {
                List<NewsArticle> News = LoadFromFile();
                News = News.OrderBy(art => art.Date).ToList();
                foreach (NewsArticle article in News)
                {
                    Add(article);
                }
            }
            SaveToFile();
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
                    db.ExecuteSql(@"
INSERT INTO text_analysis (article_id, tsvec)
SELECT id, setweight(to_tsvector(name), 'A') || setweight(to_tsvector(text), 'D')
FROM news_article
WHERE url='" + article.URL + @"'
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
                q = q.Where(art => art.URL.StartsWith(request.Url));

                string keywords = request.Keywords;
                if (!keywords.IsNullOrEmpty())
                {
                    keywords = keywords.Replace("\'", "\'\'");
                    var results = db.Select<(int ArtId, float Rank)>(@"
WITH query_rank AS (
SELECT article_id AS art_id, ts_rank(tsvec, websearch_to_tsquery('" + keywords + @"')) AS ts_rank_value
FROM text_analysis
)

SELECT id, ts_rank_value
FROM query_rank

INNER JOIN news_article
    ON query_rank.ts_rank_value > 0
    AND query_rank.art_id = news_article.id

ORDER BY ts_rank_value DESC
;")
                        .Map(tup =>
                        new SearchRankResult()
                        {
                            ArtId = tup.ArtId,
                            Rank = tup.Rank,
                            Query = keywords
                        });
                    db.CreateTableIfNotExists<SearchRankResult>();
                    foreach (SearchRankResult result in results)
                    {
                        if (!db.Exists<SearchRankResult>(r => r.ArtId == result.ArtId && r.Query == result.Query))
                        {
                            db.Insert(result);
                        }
                    }
                    q = q.Join<NewsArticle, SearchRankResult>((art, rnk) => art.Id == rnk.ArtId && rnk.Query == keywords);
                    q = q.OrderByDescending<NewsArticle, SearchRankResult>((art, rnk) => rnk.Rank);
                }
                else
                {
                    q = request.OldestFirst ? q.OrderBy(art => art.Date) : q.OrderByDescending(art => art.Date);
                }

                if (!(request.Entitities is null || request.Entitities.IsEmpty()))
                {
                    q = q.UnsafeWhere(PgSql.Array(request.Entitities) + " <@ entities");
                }
                q = q.Limit(request.Skip, request.Count);
                q = q.Select(art => new { art.Id, art.URL, art.Name, art.Date });
                FilteredNews = db.Select(q).ToList();
                if (!keywords.IsNullOrEmpty())
                {
                    db.Delete<SearchRankResult>(rnk => rnk.Query == keywords);
                }
            }
            return FilteredNews;
        }
    }
}

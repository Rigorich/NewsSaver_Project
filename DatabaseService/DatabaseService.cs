using System;
using System.Linq;
using BaseClasses;
using ServiceStack;

namespace DatabaseService
{
    public class DatabaseService : Service
    {
        [AddHeader(ContentType = MimeTypes.Json)]
        public object Post(SaveRequest request)
        {
            NewsArticle article = request.Article;

            if (article is null)
            {
                return HttpError.BadRequest($"Sended article is null");
            }

            foreach (var prop in typeof(NewsArticle).GetProperties())
            {
                var value = prop.GetValue(article);
                if (value is null ||
                    (prop.PropertyType == typeof(string) &&
                    string.IsNullOrWhiteSpace(value as string)))
                {
                    return HttpError.BadRequest($"Sended article is not full! {prop.Name} is null or empty");
                }
            }

            try
            {
                Database.Add(article);
            }
            catch
            {
                return HttpError.BadRequest($"For some reason we can't save article {article.URL}");
            }

            var response = new SaveResponse();
            return response;
        }

        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(ListRequest request)
        {
            /*
            bool FilterFunction(NewsArticle article)
            {
                if (!string.IsNullOrWhiteSpace(requestUrl) && !article.URL.Contains(requestUrl))
                {
                    return false;
                }

                if (!(keywords is null || keywords.IsEmpty()) &&
                    (article.Name is null || 
                    article.Text is null || 
                    !keywords.All(word =>
                    article.Name.ToLowerInvariant().Contains(word) ||
                    article.Text.ToLowerInvariant().Contains(word))))
                {
                    return false;
                }

                if (!(entities is null || entities.IsEmpty()) &&
                    (article.Entities is null ||
                    !entities.All(ent => 
                    article.Entities.Any(artent => 
                    artent.ToLowerInvariant().Contains(ent)))))
                {
                    return false;
                }

                DateTime articleDate = new DateTime(
                    article.Date.Year, article.Date.Month, article.Date.Day);
                if (requestDate.HasValue &&
                    requestDate.Value != articleDate)
                {
                    return false;
                }

                return true;
            }
            */
            var response = new ListResponse();
            response.Result = Database.GetFilteredList(request).ToArray();
            return response;
        }

        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(ArticleRequest request)
        {
            string requestUrl = request.Url;
            NewsArticle article = Database.GetOne(requestUrl);
            var response = new ArticleResponse();
            response.Result = article;
            return response;
        }
    }
}

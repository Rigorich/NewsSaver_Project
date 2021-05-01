using System;
using System.Linq;
using GeneralClasses;
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

            bool saved = false;
            try
            {
                saved = Database.Add(article);
            }
            catch
            {
                return HttpError.Conflict($"We can't save your article");
            }

            var response = new SaveResponse();
            response.Result = saved;
            return response;
        }

        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(ListRequest request)
        {
            string requestUrl = request.Url;

            DateTime? requestDate;
            try
            {
                requestDate = DateTime.Parse(request.Date);
            }
            catch
            {
                requestDate = null;
            }

            bool FilterFunction(NewsArticle article)
            {
                if (!string.IsNullOrWhiteSpace(requestUrl) &&
                    !article.URL.Contains(requestUrl))
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

            var list = Database.GetFilteredList(FilterFunction);

            var response = new ListResponse();
            response.Result = list.ToArray();
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

using BaseClasses;
using DatabaseService;
using ManagerService;
using ServiceStack;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ServerService
{
    public partial class NewsService : Service
    {
        JsonServiceClient manager = new JsonServiceClient("http://localhost:5003");
        JsonServiceClient database = new JsonServiceClient("http://localhost:5001");

        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(NewsRequest request)
        {
            Logger.Log("Get NewsRequest");

            string url = request.Url;
            int count = request.Count;
            string date = request.Date;
            Logger.Log($"Url = {url}, Count = {count}, Date = {date}");

            Logger.Log("Connecting to NewsManagerService...");
            string[] sites;
            try
            {
                sites = manager.Get(new AvailableSitesRequest()).Result;
            }
            catch
            {
                Logger.Log("Connecting Error");
                return HttpError.ServiceUnavailable($"No connection with NewsManagerService");
            }
            Logger.Log("AvailableSites: " + sites.Join(", "));

            if (string.IsNullOrWhiteSpace(url))
            {
                int index = new System.Random().Next(sites.Length);
                url = sites[index];
                Logger.Log($"There is no url specified. Let's get random news from {url}");
            }

            NewsArticle[] news;
            try
            {
                Logger.Log("GetNewsAsync");
                news = manager.Get(new NewsAsyncRequest() { Url = url, MaxNewsCount = count, CrawlMinimumDelayMilliSeconds = 200 }).Result;
                Logger.Log("GetNewsAsync Success");
            }
            catch
            {
                Logger.Log("GetNewsAsync Error");
                return HttpError.ServiceUnavailable($"There is some error during parsing news from url '{url}'");
            }

            if (news is null)
            {
                Logger.Log($"There is no proper instruction for url: {url}");
                return HttpError.BadRequest($"Unknown site: {url}");
            }

            bool[] saved = new bool[news.Length];
            for (int i = 0; i < news.Length; i++)
            {
                NewsArticle article = news[i];
                try
                {
                    database.Post(new SaveRequest() { Article = article });
                    saved[i] = true;
                }
                catch (WebServiceException ex)
                {
                    Logger.Log("Save article ERROR - " + ex.StatusCode + " " + ex.ErrorCode + " : " + ex.Message);
                    saved[i] = false;
                }
            }

            Logger.Log("Get NewsRequest Responsing");
            var response = new NewsResponse();
            response.ResultNews = news;
            response.ResultIsSaved = saved;
            return response;
        }

        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(ArticlePasswordedRequest request)
        {
            string[] Passwords = new string[] {
                "1834e58a07a1bd761ded407b46f4d330",
                "6542f875eaa09a5c550e5f3986400ad9",
                "d3a7614779546ac33f7afb2f5e4872aa"
            };
            MD5 md5 = MD5.Create();
            string hash = md5.ComputeHash(Encoding.UTF8.GetBytes(request.Password)).ToHex(false);
            if (Passwords.Contains(hash))
            {
                var response = new ArticlePasswordedResponse();
                response.Result = database.Get(new ArticleRequest() { Url = request.Url }).Result;
                return response;
            }
            else
            {
                return HttpError.Forbidden("Wrong password");
            }
        }
    }
}

using BaseClasses;
using DatabaseService;
using ManagerService;
using ServiceStack;

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
    }
}

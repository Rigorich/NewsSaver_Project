using BaseClasses;
using ServiceStack;
using System.Collections.Generic;

namespace ManagerService
{
    [Route("/sites")]
    public class AvailableSitesRequest : IReturn<AvailableSitesResponse>
    {
    }
    public class AvailableSitesResponse
    {
        public string[] Result { get; set; }
    }

    [Route("/news")]
    public class NewsAsyncRequest : IReturn<NewsAsyncResponse>
    {
        public string Url { get; set; } = null;
        public int MaxNewsCount { get; set; } = 1;
        public int CrawlMinimumDelayMilliSeconds { get; set; } = 1000;
    }
    public class NewsAsyncResponse
    {
        public NewsArticle[] Result { get; set; }
    }
}

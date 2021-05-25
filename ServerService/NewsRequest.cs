using BaseClasses;
using ServiceStack;
using System.Collections.Generic;
using ManagerService;
using DatabaseService;

namespace ServerService
{
    [Route("/news")]
    public class NewsRequest : IReturn<NewsResponse>
    {
        public string Url { get; set; } = null;
        public string Date { get; set; } = null;
        public int Count { get; set; } = 1;
    }
    public class NewsResponse
    {
        public NewsArticle[] ResultNews { get; set; }
        public bool[] ResultIsSaved { get; set; }
    }

    [Route("/article")]
    public class ArticlePasswordedRequest : IReturn<ArticlePasswordedResponse>
    {
        public string Url { get; set; }
        public string Password { get; set; }
    }
    public class ArticlePasswordedResponse : DatabaseService.ArticleResponse { }
}

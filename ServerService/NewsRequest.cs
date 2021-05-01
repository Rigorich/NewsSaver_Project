using GeneralClasses;
using ServiceStack;
using System.Collections.Generic;

namespace ServerService
{
    [Route("/News")]
    public class NewsRequest : IReturn<NewsResponse>
    {
        public string Url { get; set; } = null;
        public string Date { get; set; } = null;
        public int Count { get; set; } = 1;
    }
    public class NewsResponse
    {
        public NewsArticle[] Result { get; set; }
    }
    /*
    [Route("/Article")]
    public class ArticleRequest : IReturn<ArticleResponse>
    {
        public string Url { get; set; }
    }
    public class ArticleResponse
    {
        public NewsArticle Result { get; set; }
    }
    */
}

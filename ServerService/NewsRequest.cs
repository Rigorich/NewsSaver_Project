using GeneralClasses;
using ServiceStack;
using System.Collections.Generic;

namespace ServerService
{
    [Route("/News")]
    public class NewsRequest : IReturn<NewsResponse>
    {
        //public string Url { get; set; }
        //public string Date { get; set; } = null;
    }
    public class NewsResponse
    {
        public NewsArticle[] Result { get; set; }
    }
}

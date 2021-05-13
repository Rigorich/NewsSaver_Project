using System;
using ServiceStack;
using BaseClasses;

namespace DatabaseService
{
    #region PostArticleRequest
    public class SaveRequest : IReturn<SaveResponse>
    {
        public NewsArticle Article { get; set; }
    }
    public class SaveResponse
    {
    }
    #endregion

    #region GetListRequest
    [Route("/list")]
    public class ListRequest : IReturn<ListResponse>
    {
        public string Url { get; set; } = null;
        public DateTime LeftBoundDate { get; set; } = DateTime.MinValue;
        public DateTime RightBoundDate { get; set; } = DateTime.MaxValue;
        public string[] Keywords { get; set; } = null;
        public string[] Entitities { get; set; } = null;

        public int Skip { get; set; } = 0;
        public int Count { get; set; } = 10;
        public bool OldestFirst { get; set; } = false;
    }
    public class ListResponse
    {
        public NewsArticle[] Result { get; set; }
    }
    #endregion

    #region GetArticleRequest
    [Route("/article")]
    public class ArticleRequest : IReturn<ArticleResponse>
    {
        public string Url { get; set; }
    }
    public class ArticleResponse
    {
        public NewsArticle Result { get; set; }
    }
    #endregion
}

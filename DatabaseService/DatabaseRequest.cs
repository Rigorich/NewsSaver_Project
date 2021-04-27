using System;
using ServiceStack;
using GeneralClasses;

namespace DatabaseService
{
    #region PostArticleRequest
    [Route("/Save")]
    public class SaveRequest : IReturn<SaveResponse>
    {
        public NewsArticle Article { get; set; }
    }
    public class SaveResponse
    {
        public bool Result { get; set; }
    }
    #endregion

    #region GetListRequest
    [Route("/GetList")]
    public class ListRequest : IReturn<ListResponse>
    {
        public string Url { get; set; } = null;
        public string Date { get; set; } = null;
    }
    public class ListResponse
    {
        public NewsArticle[] Result { get; set; }
    }
    #endregion

    #region GetArticleRequest
    [Route("/GetArticle")]
    public class ArticleRequest : IReturn<ArticleResponse>
    {
        public string Url { get; set; }
    }
    public class ArticleResponse
    {
        public NewsArticle Result { get; set; }
    }
    #endregion

    #region GetListByKeyword
    [Route("/GetListByKeyword")]
    public class KeywordRequest : IReturn<KeywordResponse>
    {
        public string[] Keywords { get; set; }
    }
    public class KeywordResponse
    {
        public NewsArticle[] Result { get; set; }
    }
    #endregion

    #region GetListByEntity
    [Route("/GetListByEntity")]
    public class EntityRequest : IReturn<EntityResponse>
    {
        public string[] Entitities { get; set; }
    }
    public class EntityResponse
    {
        public NewsArticle[] Result { get; set; }
    }
    #endregion
}

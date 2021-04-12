using ServiceStack;

namespace ServerService
{
    public class NewsService : Service
    {
        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(NewsRequest request)
        {
            var manager = new NewsManager.Manager();
            var news = manager.GetNewsAsync(new NewsManager.Instructions.SpecificInstruction_1()).GetAwaiter().GetResult();

            foreach (var article in news)
            {
                article.Entities = null;
            }

            return news.ToJson();
        }
    }
}

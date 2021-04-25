using ServiceStack;

namespace ServerService
{
    public class NewsService : Service
    {
        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(NewsRequest request)
        {
            var manager = new NewsManager.Manager();
            var instruction = new NewsManager.Instructions.SpecificInstruction_1();
            GeneralClasses.NewsArticle[] news;
            try
            {
                news = manager.GetNewsAsync(instruction).GetAwaiter().GetResult();
            }
            catch
            {
                throw HttpError.ServiceUnavailable($"There is some error during parsing news from {instruction.MainPageUrl}");
            }
            return news;
        }
    }
}

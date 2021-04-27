using ServiceStack;

namespace ServerService
{
    public class NewsService : Service
    {
        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(NewsRequest request)
        {
            Logger.Log("Get NewsRequest");
            Logger.Log("Creating NewsManager...");
            var manager = new NewsManager.Manager();
            Logger.Log("Creating NewsManager Success");
            Logger.Log("Creating Instruction...");
            var instruction = new NewsManager.Instructions.SpecificInstruction_1();
            Logger.Log($"Instruction MainPageUrl: {instruction.MainPageUrl}");
            Logger.Log("Creating Instruction Success");
            GeneralClasses.NewsArticle[] news;
            try
            {
                Logger.Log("GetNewsAsync");
                news = manager.GetNewsAsync(instruction, 10, 100).GetAwaiter().GetResult();
                Logger.Log("GetNewsAsync Success");
            }
            catch
            {
                Logger.Log("GetNewsAsync Error");
                throw HttpError.ServiceUnavailable($"There is some error during parsing news from {instruction.MainPageUrl}");
            }

            Logger.Log("Get NewsRequest Responsing");
            var response = new NewsResponse();
            response.Result = news;
            return response;
        }
    }
}

using ServiceStack;

namespace ServerService
{
    public class NewsService : Service
    {
        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(NewsRequest request)
        {
            Logger.Log("Get NewsRequest");

            string url = request.Url;
            int count = request.Count;
            string date = request.Date;
            Logger.Log($"Url = {url}");
            Logger.Log($"Count = {count}");
            Logger.Log($"Date = {date}");

            Logger.Log("Creating NewsManager...");
            var manager = new NewsManager.Manager();
            Logger.Log("Creating NewsManager Success");
            Logger.Log("Creating Instruction...");
            NewsManager.Instructions.SpecificInstruction instruction = null;
            var instrs = NewsManager.Instructions.AllInstructions.List;
            if (string.IsNullOrWhiteSpace(url))
            {
                Logger.Log($"There is no url specified. Let's get random news!");
                int index = new System.Random().Next(instrs.Length);
                instruction = NewsManager.Instructions.AllInstructions.List[index];
            }
            else
            {
                foreach (var instr in instrs)
                {
                    if (instr.MainPageUrl.Contains(url))
                    {
                        if (instruction is null)
                        {
                            instruction = instr;
                            Logger.Log($"Found proper instruction: {instr.GetType()}");
                        }
                        else
                        {
                            Logger.Log($"Instruction conflict: {instr.GetType()}");
                        }
                    }
                }
            }

            if (instruction is null)
            {
                Logger.Log($"There is no proper instruction for url: {url}");
                return HttpError.BadRequest($"Unknown site: {url}");
            }

            Logger.Log($"Instruction MainPageUrl: {instruction.MainPageUrl}");
            Logger.Log("Creating Instruction Success");
            GeneralClasses.NewsArticle[] news;
            try
            {
                Logger.Log("GetNewsAsync");
                news = manager.GetNewsAsync(instruction, count, 200).GetAwaiter().GetResult();
                Logger.Log("GetNewsAsync Success");
            }
            catch
            {
                Logger.Log("GetNewsAsync Error");
                return HttpError.ServiceUnavailable($"There is some error during parsing news from {instruction.MainPageUrl}");
            }

            Logger.Log("Get NewsRequest Responsing");
            var response = new NewsResponse();
            response.Result = news;
            return response;
        }
        /*
        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(ArticleRequest request)
        {
            Logger.Log("Get ArticleRequest");

            return null;
        }
        */
    }
}

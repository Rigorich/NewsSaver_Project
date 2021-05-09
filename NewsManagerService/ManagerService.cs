using BaseClasses;
using ServiceStack;

namespace ManagerService
{
    public class ManagerService : Service
    {
        NewsManager.Manager Manager = new NewsManager.Manager();

        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(AvailableSitesRequest request)
        {
            var response = new AvailableSitesResponse();
            response.Result = Manager.GetAvailableSites();
            return response;
        }

        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(NewsAsyncRequest request)
        {
            var response = new NewsAsyncResponse();
            response.Result = Manager.GetNewsAsync(
                request.Url, request.MaxNewsCount, request.CrawlMinimumDelayMilliSeconds)
                .ConfigureAwait(false).GetAwaiter().GetResult();
            return response;
        }
    }
}

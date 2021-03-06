using BaseClasses;
using ServiceStack;
using ManagerService;
using DatabaseService;

namespace ServerService
{
    #region ManagerService

    [Route("/sites")]
    public class AvailableSitesRequest : ManagerService.AvailableSitesRequest { }
    public class AvailableSitesResponse : ManagerService.AvailableSitesResponse { }

    #endregion


    #region DatabaseService

    [Route("/list")]
    public class ListRequest : DatabaseService.ListRequest { }
    public class ListResponse : DatabaseService.ListResponse { }

    #endregion

    public partial class NewsService : Service
    {
        #region ManagerService

        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(AvailableSitesRequest request) => manager.Get(request);

        #endregion


        #region DatabaseService

        [AddHeader(ContentType = MimeTypes.Json)]
        public object Get(ListRequest request) => database.Get(request);

        #endregion
    }
}

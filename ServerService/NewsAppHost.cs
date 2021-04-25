using Funq;
using ServiceStack;

namespace ServerService
{
    public class NewsAppHost : AppHostBase
    {
        public NewsAppHost() : base("News Service", typeof(NewsService).Assembly) { }
        public override void Configure(Container container) { }
    }
}

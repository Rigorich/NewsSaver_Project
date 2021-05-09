using Funq;
using ServiceStack;

namespace ManagerService
{
    public class ManagerAppHost : AppHostBase
    {
        public ManagerAppHost() : base("Manager Service", typeof(ManagerService).Assembly) { }
        public override void Configure(Container container) { }
    }
}

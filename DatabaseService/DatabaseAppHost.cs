using Funq;
using ServiceStack;

namespace DatabaseService
{
    public class DatabaseAppHost : AppHostBase
    {
        public DatabaseAppHost() : base("Database Service", typeof(DatabaseService).Assembly) { }
        public override void Configure(Container container) { }
    }
}

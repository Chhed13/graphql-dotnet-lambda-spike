using Microsoft.AspNetCore.Hosting;

namespace Graphql.Tests.Integration.Api
{
    public class QueryGraphTypeTests : QueryTestsBase
    {
        public QueryGraphTypeTests() : base(new WebHostBuilder()
            .UseEnvironment("Test")
            .UseStartup<Graphql.Api.Aspnet.GraphType.Startup>())
        {
        }
    }
}
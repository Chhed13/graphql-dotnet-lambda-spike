using Microsoft.AspNetCore.Hosting;

namespace Graphql.Tests.Integration.Api
{
    public class QuerySchemaFirstTests : QueryTestsBase
    {
        public QuerySchemaFirstTests() : base(new WebHostBuilder()
            .UseEnvironment("Test")
            .UseStartup<Graphql.Api.Aspnet.SchemaFirst.Startup>())
        {
        }
    }
}
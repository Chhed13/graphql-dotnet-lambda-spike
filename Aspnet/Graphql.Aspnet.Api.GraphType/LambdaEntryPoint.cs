using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Graphql.Aspnet.Api.GraphType
{
    public class ProgramLambda: Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseApiGateway();
        }
    }
}
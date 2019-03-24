using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Graphql.Api.Aspnet.GraphType
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseUrls("http://*:5051")
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}
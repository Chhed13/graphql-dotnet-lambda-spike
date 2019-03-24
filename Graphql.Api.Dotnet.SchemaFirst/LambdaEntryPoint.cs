using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Graphql.Api.Dotnet.SchemaFirst.Models;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Graphql.Api.Dotnet.SchemaFirst
{
    public class ProgramLambda
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;

        public ProgramLambda()
        {
            IServiceCollection services = new ServiceCollection();
            new Startup().ConfigureServices(services);

            var sp = services.BuildServiceProvider();

            _schema = sp.GetService<ISchema>();
            _executer = sp.GetService<IDocumentExecuter>();
            _schema.Initialize();

//            db.EnsureSeedData();
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var query = JsonConvert.DeserializeObject<GraphqlQuery>(request.Body);
            if (query == null) throw new ArgumentNullException(nameof(query));

            try
            {
                var execOptions = new ExecutionOptions
                {
                    Schema = _schema,
                    Query = query.Query,
                };

                var result = await _executer.ExecuteAsync(execOptions).ConfigureAwait(false);
                if (result.Errors?.Count > 0)
                {
                    context.Logger.Log("GraphQL error: " + result.Errors.First().Message);
                    return new APIGatewayProxyResponse {StatusCode = 500};
                }

                var output = JsonConvert.SerializeObject(result);
                context.Logger.Log("GraphQL execution result: " + output);
                return new APIGatewayProxyResponse {StatusCode = 200, Body = output};
            }

            catch (Exception ex)
            {
                context.Logger.Log("Document exexuter exception " + ex);
                return new APIGatewayProxyResponse {StatusCode = 500};
            }
        }
    }
}
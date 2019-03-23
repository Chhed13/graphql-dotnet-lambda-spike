using Graphql.Aspnet.Core.Models;
using GraphQL;

namespace Graphql.Aspnet.Api.SchemaFirst.Resolvers
{
    [GraphQLMetadata("Droid", IsTypeOf = typeof(Droid))]
    public class DroidResolver : CharacterResolver<Droid>
    {
        public string PrimaryFunction(Droid _) => _.PrimaryFunction;
    }
}
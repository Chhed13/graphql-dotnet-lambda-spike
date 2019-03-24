using Graphql.Core.Models;
using GraphQL;

namespace Graphql.Api.Aspnet.SchemaFirst.Resolvers
{
    [GraphQLMetadata("Droid", IsTypeOf = typeof(Droid))]
    public class DroidResolver : CharacterResolver<Droid>
    {
        public string PrimaryFunction(Droid _) => _.PrimaryFunction;
    }
}
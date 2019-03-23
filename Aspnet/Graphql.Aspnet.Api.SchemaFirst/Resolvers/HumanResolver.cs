using Graphql.Aspnet.Core.Models;
using GraphQL;

namespace Graphql.Aspnet.Api.SchemaFirst.Resolvers
{
    [GraphQLMetadata("Human", IsTypeOf = typeof(Human))]
    public class HumanResolver : CharacterResolver<Human>
    {
        public string HomePlanet(Human _) => _.HomePlanet.Name;
    }
}
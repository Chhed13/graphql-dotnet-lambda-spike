using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Logic;
using Graphql.Aspnet.Core.Models;
using GraphQL;

namespace Graphql.Aspnet.Api.SchemaFirst.Resolvers
{
    public class Mutation
    {
        private readonly ITrilogyHeroes _trilogyHeroes;
        private readonly IDroidRepository _droidRepository;
        private readonly IHumanRepository _humanRepository;

        public Mutation(ITrilogyHeroes trilogyHeroes, IDroidRepository droidRepository,
            IHumanRepository humanRepository)
        {
            _trilogyHeroes = trilogyHeroes;
            _droidRepository = droidRepository;
            _humanRepository = humanRepository;
        }

        [GraphQLMetadata("addDroid")]
        public Droid AddDroid(Droid droid)
        {
            return _droidRepository.Add(droid);
        }
    }
}
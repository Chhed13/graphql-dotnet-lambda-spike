using Graphql.Aspnet.GraphType.Api.Models;
using Graphql.Aspnet.GraphType.Core.Data;
using Graphql.Aspnet.GraphType.Core.Logic;
using Graphql.Aspnet.GraphType.Core.Models;
using GraphQL.Types;

namespace Graphql.Aspnet.GraphType.Api.Resolvers
{
    public class Mutation: ObjectGraphType
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

            Name = "Mutation";

            Field<DroidType>(
                Name = "addDroid",
                arguments: new QueryArguments(
                        new QueryArgument<DroidTypeInput> { Name = "droid", DefaultValue =  null}),
                resolve: AddDroid
            );
        }

        private object AddDroid(ResolveFieldContext<object> context)
        {

            var droid = context.GetArgument<Droid>("droid");


            var droidOut = _droidRepository.Add( _mapper.Map<Droid>(droid));
            var mapped = _mapper.Map<Droid>(droidOut);
            return mapped;
        }
    }
}
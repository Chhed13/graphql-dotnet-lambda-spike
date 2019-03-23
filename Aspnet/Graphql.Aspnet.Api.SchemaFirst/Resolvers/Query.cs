using Graphql.Aspnet.Api.GraphType.Models;
using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Logic;
using GraphQL.Types;

namespace Graphql.Aspnet.Api.GraphType.Resolvers
{
    public class Query : ObjectGraphType
    {
        private readonly ITrilogyHeroes _trilogyHeroes;
        private readonly IDroidRepository _droidRepository;
        private readonly IHumanRepository _humanRepository;

        public Query(ITrilogyHeroes trilogyHeroes, IDroidRepository droidRepository,
            IHumanRepository humanRepository)
        {
            _trilogyHeroes = trilogyHeroes;
            _droidRepository = droidRepository;
            _humanRepository = humanRepository;

            Name = "Query";

            Field<CharacterInterface>(
                "hero",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<EpisodeEnum>>
                    {
                        Name = "episode",
                        Description =
                            "If omitted, returns the hero of the whole saga. If provided, returns the hero of that particular episode."
                    }
                ),
                resolve: GetHero
            );
            Field<HumanType>(
                "human",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> {Name = "id", Description = "id of the human"}
                ),
                resolve: GetHuman
            );

            Field<DroidType>(
                "droid",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> {Name = "id", Description = "id of the droid"}
                ), resolve: GetDroid);


            Field<ListGraphType<CharacterInterface>>("heroes", resolve: GetHeroes);
            Field<ListGraphType<HumanType>>("humans", resolve: GetHumans);
            Field<ListGraphType<DroidType>>("droids", resolve: GetDroids);
        }

        private object GetHero(ResolveFieldContext<object> context)
        {
            var episode = context.GetArgument<Episodes>("episode");
            var character = _trilogyHeroes.GetHero((int) episode).Result;
            return character;
        }

        private object GetHuman(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int>("id");
            var human = _humanRepository.Get(id, include: "HomePlanet").Result;
            return human;
        }

        private object GetDroid(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int>("id");
            var droid = _droidRepository.Get(id).Result;
            return droid;
        }

        private object GetHeroes(ResolveFieldContext<object> arg)
        {
            var heroes = _trilogyHeroes.GetHeroes().Result;
            return heroes;
        }

        private object GetHumans(ResolveFieldContext<object> arg)
        {
            var humans = _humanRepository.GetAll().Result;
            return humans;
        }

        private object GetDroids(ResolveFieldContext<object> context)
        {
            var droids = _droidRepository.GetAll().Result;
            return droids;
        }
    }
}
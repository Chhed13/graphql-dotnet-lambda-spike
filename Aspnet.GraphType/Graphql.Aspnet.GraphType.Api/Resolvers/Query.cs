using System.Collections.Generic;
using AutoMapper;
using Graphql.Aspnet.GraphType.Api.Models;
using Graphql.Aspnet.GraphType.Core.Data;
using Graphql.Aspnet.GraphType.Core.Logic;
using GraphQL.Types;

namespace Graphql.Aspnet.GraphType.Api.Resolvers
{
    public class StarWarsQuery : ObjectGraphType
    {
        private readonly ITrilogyHeroes _trilogyHeroes;
        private readonly IDroidRepository _droidRepository;
        private readonly IHumanRepository _humanRepository;
        private readonly IMapper _mapper;

        public StarWarsQuery(ITrilogyHeroes trilogyHeroes, IDroidRepository droidRepository,
            IHumanRepository humanRepository, IMapper mapper)
        {
            _trilogyHeroes = trilogyHeroes;
            _droidRepository = droidRepository;
            _humanRepository = humanRepository;
            _mapper = mapper;

            Name = "Query";

            Field<CharacterInterface>(
                "hero",
                arguments: new QueryArguments(
                    new QueryArgument<EpisodeEnum>
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
            var episode = context.GetArgument<Episodes?>("episode");
            var character = _trilogyHeroes.GetHero((int?) episode).Result;
            return _mapper.Map<Character>(character);
        }

        private object GetHuman(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int>("id");
            var human = _humanRepository.Get(id, include: "HomePlanet").Result;
            return _mapper.Map<Human>(human);
        }

        private object GetDroid(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int>("id");
            var droid = _droidRepository.Get(id).Result;
            return _mapper.Map<Droid>(droid);
        }

        private object GetHeroes(ResolveFieldContext<object> arg)
        {
            var heroes = _trilogyHeroes.GetHeroes().Result;
            return _mapper.Map<IList<Character>>(heroes);
        }

        private object GetHumans(ResolveFieldContext<object> arg)
        {
            var humans = _humanRepository.GetAll().Result;
            return _mapper.Map<IList<Human>>(humans);
        }

        private object GetDroids(ResolveFieldContext<object> context)
        {
            var droids = _droidRepository.GetAll().Result;
            return _mapper.Map<IList<Droid>>(droids);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Graphql.Aspnet.GraphType.Api.Models;
using Graphql.Aspnet.GraphType.Core.Data;
using Graphql.Aspnet.GraphType.Core.Logic;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace Graphql.Aspnet.GraphType.Api
{
    public class NewStarWarsQuery : ObjectGraphType
    {
        private readonly ITrilogyHeroes _trilogyHeroes;
        private readonly IDroidRepository _droidRepository;
        private readonly IHumanRepository _humanRepository;
        private readonly IMapper _mapper;

        public NewStarWarsQuery(ITrilogyHeroes trilogyHeroes, IDroidRepository droidRepository,
            IHumanRepository humanRepository, IMapper mapper)
        {
            _trilogyHeroes = trilogyHeroes;
            _droidRepository = droidRepository;
            _humanRepository = humanRepository;
            _mapper = mapper;
            new HeroQuery(this, trilogyHeroes, mapper);

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

    internal class HeroQuery
    {
        private readonly IMapper _mapper;
        private readonly ITrilogyHeroes _trilogyHeroes;

        public HeroQuery(ObjectGraphType parent, ITrilogyHeroes trilogyHeroes, IMapper mapper)
        {
            _mapper = mapper;
            _trilogyHeroes = trilogyHeroes;
        }

        public FieldType GetField()
        {
            return new FieldType
            {
                Name = "hero",
                Type = typeof(CharacterInterface),
                Arguments = new QueryArguments(
                    new QueryArgument<EpisodeEnum>
                    {
                        Name = "episode",
                        Description =
                            "If omitted, returns the hero of the whole saga. If provided, returns the hero of that particular episode."
                    }
                ),
                Resolver = (IFieldResolver) new FuncFieldResolver<object, object>(GetHero)
            };
        }

        private object GetHero(ResolveFieldContext<object> context)
        {
            var episode = context.GetArgument<Episodes?>("episode");
            var character = _trilogyHeroes.GetHero((int?) episode).Result;
            return _mapper.Map<Character>(character);
        }
    }
}
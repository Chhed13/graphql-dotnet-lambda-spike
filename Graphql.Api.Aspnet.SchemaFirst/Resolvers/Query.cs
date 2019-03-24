using System.Collections.Generic;
using Graphql.Core.Data;
using Graphql.Core.Logic;
using Graphql.Core.Models;
using GraphQL;

namespace Graphql.Api.Aspnet.SchemaFirst.Resolvers
{
    public class Query
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
        }

        [GraphQLMetadata("hero")]
        public Character GetHero(Episodes episode)
        {
            return _trilogyHeroes.GetHero((int)episode).Result;
        }

        [GraphQLMetadata("human")]
        public Human GetHuman(int id)
        {
            return _humanRepository.Get(id, include: "HomePlanet").Result;;
        }

        [GraphQLMetadata("droid")]
        public Droid GetDroid(int id)
        {
            return _droidRepository.Get(id).Result;
        }

        [GraphQLMetadata("heroes")]
        public IList<Character> GetHeroes()
        {
            return _trilogyHeroes.GetHeroes().Result;;
        }

        [GraphQLMetadata("humans")]
        public IList<Human> GetHumans()
        {
            return _humanRepository.GetAll().Result;
        }

        [GraphQLMetadata("droids")]
        public IList<Droid> GetDroids()
        {
            return _droidRepository.GetAll().Result;
        }
    }
}
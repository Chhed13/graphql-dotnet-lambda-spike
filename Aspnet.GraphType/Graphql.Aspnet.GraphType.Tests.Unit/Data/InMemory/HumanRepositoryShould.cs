using System.Linq;
using Graphql.Aspnet.GraphType.Data.InMemory;
using Graphql.Aspnet.GraphType.Data.InMemory.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace graphql.Tests.Unit.Data.InMemory
{
    public class HumanRepositoryShould
    {
        private readonly HumanRepository _humanRepository;

        public HumanRepositoryShould()
        {
            var dbLogger = new Mock<ILogger<StarWarsInMemoryContext>>();
            var db = new StarWarsInMemoryContext(dbLogger.Object);

            var logger = new Mock<ILogger<HumanRepository>>();

            _humanRepository = new HumanRepository(db, logger.Object);
        }

        [Fact, Trait("test", "unit")]
        public async void ReturnLukeHumanGivenId()
        {
            var human = await _humanRepository.Get(1000);

            Assert.NotNull(human);
            Assert.Equal("Luke Skywalker", human.Name);
            Assert.Equal(3, human.CharacterEpisodes.Count);
            Assert.Equal("Tatooine", human.HomePlanet.Name);
            Assert.Equal(4, human.CharacterFriends.Count);
            Assert.Contains(human.CharacterFriends, f => f.Friend.Name == "Han Solo");
        }
    }
}
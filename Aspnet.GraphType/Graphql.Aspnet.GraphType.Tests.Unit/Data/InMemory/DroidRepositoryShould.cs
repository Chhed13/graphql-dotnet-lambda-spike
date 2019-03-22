using System.Linq;
using Graphql.Aspnet.GraphType.Data.InMemory;
using Graphql.Aspnet.GraphType.Data.InMemory.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace graphql.Tests.Unit.Data.InMemory
{
    public class DroidRepositoryShould
    {
        private readonly DroidRepository _droidRepository;

        public DroidRepositoryShould()
        {
            var dbLogger = new Mock<ILogger<StarWarsInMemoryContext>>();
            var db = new StarWarsInMemoryContext(dbLogger.Object);

            var logger = new Mock<ILogger<DroidRepository>>();

            _droidRepository = new DroidRepository(db, logger.Object);
        }

        [Fact, Trait("test", "unit")]
        public async void ReturnR2D2DroidGivenId()
        {
            var droid = await _droidRepository.Get(2001);

            Assert.NotNull(droid);
            Assert.Equal("R2-D2", droid.Name);
            Assert.Equal(3, droid.CharacterEpisodes.Count);
            Assert.Equal("Astromech", droid.PrimaryFunction);
            Assert.Equal(1000, droid.CharacterFriends.First().Friend.Id);
        }
    }
}
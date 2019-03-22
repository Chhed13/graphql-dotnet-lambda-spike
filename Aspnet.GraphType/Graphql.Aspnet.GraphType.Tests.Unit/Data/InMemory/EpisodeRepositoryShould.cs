using Graphql.Aspnet.GraphType.Data.InMemory;
using Graphql.Aspnet.GraphType.Data.InMemory.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace graphql.Tests.Unit.Data.InMemory
{
    public class EpisodeRepositoryShould
    {
        private readonly EpisodeRepository _episodeRepository;

        public EpisodeRepositoryShould()
        {
            var dbLogger = new Mock<ILogger<StarWarsInMemoryContext>>();
            var db = new StarWarsInMemoryContext(dbLogger.Object);

            var logger = new Mock<ILogger<EpisodeRepository>>();

            _episodeRepository = new EpisodeRepository(db, logger.Object);
        }

        [Fact, Trait("test", "unit")]
        public async void ReturnEpisodeGivenId()
        {
            var episode = await _episodeRepository.Get(6);

            Assert.NotNull(episode);
            Assert.Equal("JEDI", episode.Title);
        }
    }
}
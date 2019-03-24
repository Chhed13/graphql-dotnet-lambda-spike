using Graphql.Data.InMemory;
using Graphql.Data.InMemory.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Graphql.Tests.Unit.Data.InMemory
{
    public class EpisodeRepositoryShould
    {
        private readonly EpisodeRepository _episodeRepository;

        public EpisodeRepositoryShould()
        {
            var dbLogger = new Mock<ILogger<InMemoryContext>>();
            var db = new InMemoryContext(dbLogger.Object);

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
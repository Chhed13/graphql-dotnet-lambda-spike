using Graphql.Aspnet.GraphType.Data.InMemory;
using Graphql.Aspnet.GraphType.Data.InMemory.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace graphql.Tests.Unit.Data.InMemory
{
    public class PlanetRepositoryShould
    {
        private readonly PlanetRepository _planetRepository;

        public PlanetRepositoryShould()
        {
            var dbLogger = new Mock<ILogger<StarWarsInMemoryContext>>();
            var db = new StarWarsInMemoryContext(dbLogger.Object);

            var logger = new Mock<ILogger<PlanetRepository>>();

            _planetRepository = new PlanetRepository(db, logger.Object);
        }

        [Fact, Trait("test", "unit")]
        public async void ReturnTatooineGivenId()
        {
            var model = await _planetRepository.Get(1);

            Assert.NotNull(model);
            Assert.Equal("Tatooine", model.Name);
        }
    }
}
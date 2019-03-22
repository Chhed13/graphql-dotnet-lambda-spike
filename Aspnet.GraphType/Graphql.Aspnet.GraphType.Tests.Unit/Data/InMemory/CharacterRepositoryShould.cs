using Graphql.Aspnet.GraphType.Data.InMemory;
using Graphql.Aspnet.GraphType.Data.InMemory.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace graphql.Tests.Unit.Data.InMemory
{
    public class CharacterRepositoryShould
    {
        private readonly CharacterRepository _characterRepository;

        public CharacterRepositoryShould()
        {
            var dbLogger = new Mock<ILogger<StarWarsInMemoryContext>>();
            var db = new StarWarsInMemoryContext(dbLogger.Object);

            var logger = new Mock<ILogger<CharacterRepository>>();

            _characterRepository = new CharacterRepository(db, logger.Object);
        }

        [Fact, Trait("test", "unit")]
        public async void ReturnCharacterGivenId()
        {
            var character = await _characterRepository.Get(2001);

            Assert.NotNull(character);
            Assert.Equal("R2-D2", character.Name);
        }
    }
}
using Graphql.Aspnet.Data.InMemory;
using Graphql.Aspnet.Data.InMemory.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Graphql.Aspnet.Tests.Unit.Data.InMemory
{
    public class CharacterRepositoryShould
    {
        private readonly CharacterRepository _characterRepository;

        public CharacterRepositoryShould()
        {
            var dbLogger = new Mock<ILogger<InMemoryContext>>();
            var db = new InMemoryContext(dbLogger.Object);

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
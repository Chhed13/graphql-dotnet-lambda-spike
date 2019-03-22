using graphql.core.Models;
using graphql.data.EntityFramework;
using graphql.data.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace graphql.Tests.Unit.Data.EntityFramework.Repositories
{
    public class DroidRepositoryShould
    {
        private readonly DroidRepository _droidRepository;

        public DroidRepositoryShould()
        {
            var dbLogger = new Mock<ILogger<StarWarsContext>>();

            var options = new DbContextOptionsBuilder<StarWarsContext>()
                .UseInMemoryDatabase(databaseName: "StarWars")
                .Options;
            using (var context = new StarWarsContext(options, dbLogger.Object))
            {
                context.Droids.Add(new Droid {Id = 1, Name = "R2-D2"});
                context.SaveChanges();
            }

            var starWarsContext = new StarWarsContext(options, dbLogger.Object);
            var repoLogger = new Mock<ILogger<DroidRepository>>();
            _droidRepository = new DroidRepository(starWarsContext, repoLogger.Object);
        }

        [Fact]
        public async void ReturnR2D2DroidGivenIdOf1()
        {
            var droid = await _droidRepository.Get(1);

            Assert.NotNull(droid);
            Assert.Equal("R2-D2", droid.Name);
        }
    }
}
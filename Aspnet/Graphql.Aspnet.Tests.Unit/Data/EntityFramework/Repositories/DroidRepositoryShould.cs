using Graphql.Aspnet.Core.Models;
using Graphql.Aspnet.Data.EntityFramework.Repositories;
using Graphql.Aspnet.GraphType.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Graphql.Aspnet.Tests.Unit.Data.EntityFramework.Repositories
{
    public class DroidRepositoryShould
    {
        private readonly DroidRepository _droidRepository;

        public DroidRepositoryShould()
        {
            var dbLogger = new Mock<ILogger<Context>>();

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "StarWars")
                .Options;
            using (var context = new Context(options, dbLogger.Object))
            {
                context.Droids.Add(new Droid {Id = 1, Name = "R2-D2"});
                context.SaveChanges();
            }

            var starWarsContext = new Context(options, dbLogger.Object);
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
using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.Data.InMemory.Repositories
{
    public class PlanetRepository : BaseRepository<Planet, int>, IPlanetRepository
    {
        public PlanetRepository(InMemoryContext db, ILogger<PlanetRepository> logger) : base(db, logger)
        {
        }
    }
}
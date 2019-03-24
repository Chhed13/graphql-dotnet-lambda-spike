using Graphql.Core.Data;
using Graphql.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Data.InMemory.Repositories
{
    public class PlanetRepository : BaseRepository<Planet, int>, IPlanetRepository
    {
        public PlanetRepository(InMemoryContext db, ILogger<PlanetRepository> logger) : base(db, logger)
        {
        }
    }
}
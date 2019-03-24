using Graphql.Core.Data;
using Graphql.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Data.EntityFramework.Repositories
{
    public class PlanetRepository : BaseRepository<Planet, int>, IPlanetRepository
    {
        public PlanetRepository(Context db, ILogger<PlanetRepository> logger)
            : base(db, logger)
        {
        }
    }
}
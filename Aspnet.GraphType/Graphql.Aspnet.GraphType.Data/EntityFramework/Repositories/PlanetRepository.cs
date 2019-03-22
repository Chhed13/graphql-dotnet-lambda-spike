using Graphql.Aspnet.GraphType.Core.Models;
using Graphql.Aspnet.GraphType.Core.Data;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.GraphType.Data.EntityFramework.Repositories
{
    public class PlanetRepository : BaseRepository<Planet, int>, IPlanetRepository
    {
        public PlanetRepository()
        {
        }

        public PlanetRepository(StarWarsContext db, ILogger<PlanetRepository> logger)
            : base(db, logger)
        {
        }
    }
}
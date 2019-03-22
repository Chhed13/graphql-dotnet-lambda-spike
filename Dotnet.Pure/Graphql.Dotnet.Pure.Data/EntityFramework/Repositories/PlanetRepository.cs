using graphql.core.Models;
using graphql.core.Data;
using Microsoft.Extensions.Logging;

namespace graphql.data.EntityFramework.Repositories
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
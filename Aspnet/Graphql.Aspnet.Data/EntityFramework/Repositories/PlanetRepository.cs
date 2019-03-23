using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;
using Graphql.Aspnet.GraphType.Data.EntityFramework;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.Data.EntityFramework.Repositories
{
    public class PlanetRepository : BaseRepository<Planet, int>, IPlanetRepository
    {
        public PlanetRepository(Context db, ILogger<PlanetRepository> logger)
            : base(db, logger)
        {
        }
    }
}
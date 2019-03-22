using Graphql.Aspnet.GraphType.Core.Data;
using Graphql.Aspnet.GraphType.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.GraphType.Data.EntityFramework.Repositories
{
    public class EpisodeRepository : BaseRepository<Episode, int>, IEpisodeRepository
    {
        public EpisodeRepository()
        {
        }

        public EpisodeRepository(StarWarsContext db, ILogger<PlanetRepository> logger) : base(db, logger)
        {
        }
    }
}
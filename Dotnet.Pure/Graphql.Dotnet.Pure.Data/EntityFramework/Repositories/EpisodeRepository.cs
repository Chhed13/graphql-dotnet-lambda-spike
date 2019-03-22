using graphql.core.Data;
using graphql.core.Models;
using Microsoft.Extensions.Logging;

namespace graphql.data.EntityFramework.Repositories
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
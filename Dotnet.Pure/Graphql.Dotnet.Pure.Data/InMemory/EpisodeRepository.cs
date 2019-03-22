using graphql.core.Data;
using graphql.core.Models;
using Microsoft.Extensions.Logging;

namespace graphql.data.InMemory
{
    public class EpisodeRepository : BaseRepository<Episode, int>, IEpisodeRepository
    {
        public EpisodeRepository(ILogger<PlanetRepository> logger) : base(logger)
        {
        }
    }
}
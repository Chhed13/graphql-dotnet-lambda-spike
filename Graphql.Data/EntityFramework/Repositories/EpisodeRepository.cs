using Graphql.Core.Data;
using Graphql.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Data.EntityFramework.Repositories
{
    public class EpisodeRepository : BaseRepository<Episode, int>, IEpisodeRepository
    {
        public EpisodeRepository(Context db, ILogger<PlanetRepository> logger) : base(db, logger)
        {
        }
    }
}
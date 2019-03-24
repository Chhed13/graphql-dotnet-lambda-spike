using Graphql.Core.Data;
using Graphql.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Data.InMemory.Repositories
{
    public class EpisodeRepository : BaseRepository<Episode, int>, IEpisodeRepository
    {
        public EpisodeRepository(InMemoryContext db, ILogger<EpisodeRepository> logger) : base(db, logger)
        {
        }
    }
}
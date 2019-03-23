using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.Data.InMemory.Repositories
{
    public class EpisodeRepository : BaseRepository<Episode, int>, IEpisodeRepository
    {
        public EpisodeRepository(InMemoryContext db, ILogger<EpisodeRepository> logger) : base(db, logger)
        {
        }
    }
}
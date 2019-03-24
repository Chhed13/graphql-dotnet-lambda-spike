using Graphql.Core.Data;
using Graphql.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Data.EntityFramework.Repositories
{
    public class DroidRepository : BaseRepository<Droid, int>, IDroidRepository
    {
        public DroidRepository(Context db, ILogger<DroidRepository> logger)
            : base(db, logger)
        {
        }
    }
}
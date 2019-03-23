using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;
using Graphql.Aspnet.GraphType.Data.EntityFramework;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.Data.EntityFramework.Repositories
{
    public class DroidRepository : BaseRepository<Droid, int>, IDroidRepository
    {
        public DroidRepository(Context db, ILogger<DroidRepository> logger)
            : base(db, logger)
        {
        }
    }
}
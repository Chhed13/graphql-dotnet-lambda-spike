using Graphql.Core.Data;
using Graphql.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Data.InMemory.Repositories
{
    public class DroidRepository: BaseRepository<Droid, int>, IDroidRepository
    {
         public DroidRepository(InMemoryContext db,  ILogger<DroidRepository> logger): base(db, logger)
        {
        }
    }
}
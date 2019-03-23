using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.Data.InMemory.Repositories
{
    public class DroidRepository: BaseRepository<Droid, int>, IDroidRepository
    {
         public DroidRepository(InMemoryContext db,  ILogger<DroidRepository> logger): base(db, logger)
        {
        }
    }
}
using Graphql.Aspnet.GraphType.Core.Data;
using Graphql.Aspnet.GraphType.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.GraphType.Data.InMemory.Repositories
{
    public class DroidRepository: BaseRepository<Droid, int>, IDroidRepository
    {
         public DroidRepository(StarWarsInMemoryContext db,  ILogger<DroidRepository> logger): base(db, logger)
        {
        }
    }
}
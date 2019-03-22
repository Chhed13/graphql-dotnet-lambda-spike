using Graphql.Aspnet.GraphType.Core.Data;
using Graphql.Aspnet.GraphType.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.GraphType.Data.InMemory.Repositories
{
    public class HumanRepository : BaseRepository<Human, int>, IHumanRepository
    {
        public HumanRepository(StarWarsInMemoryContext db, ILogger<HumanRepository> logger) : base(db, logger)
        {
        }
    }
}
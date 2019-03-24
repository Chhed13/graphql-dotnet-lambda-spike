using Graphql.Core.Data;
using Graphql.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Data.InMemory.Repositories
{
    public class HumanRepository : BaseRepository<Human, int>, IHumanRepository
    {
        public HumanRepository(InMemoryContext db, ILogger<HumanRepository> logger) : base(db, logger)
        {
        }
    }
}
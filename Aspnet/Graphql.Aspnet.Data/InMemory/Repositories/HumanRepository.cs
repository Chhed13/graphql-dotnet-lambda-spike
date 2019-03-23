using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.Data.InMemory.Repositories
{
    public class HumanRepository : BaseRepository<Human, int>, IHumanRepository
    {
        public HumanRepository(InMemoryContext db, ILogger<HumanRepository> logger) : base(db, logger)
        {
        }
    }
}
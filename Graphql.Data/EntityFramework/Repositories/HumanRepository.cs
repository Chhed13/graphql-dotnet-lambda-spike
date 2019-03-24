using Graphql.Core.Data;
using Graphql.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Data.EntityFramework.Repositories
{
    public class HumanRepository: BaseRepository<Human, int>, IHumanRepository
    {
        public HumanRepository(Context db, ILogger<HumanRepository> logger)
            : base(db, logger)
        {
        }
    }
}
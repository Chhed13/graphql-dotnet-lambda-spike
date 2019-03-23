using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;
using Graphql.Aspnet.GraphType.Data.EntityFramework;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.Data.EntityFramework.Repositories
{
    public class HumanRepository: BaseRepository<Human, int>, IHumanRepository
    {
        public HumanRepository(Context db, ILogger<HumanRepository> logger)
            : base(db, logger)
        {
        }
    }
}
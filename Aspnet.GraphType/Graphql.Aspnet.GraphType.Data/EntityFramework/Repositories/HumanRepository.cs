using Graphql.Aspnet.GraphType.Core.Data;
using Graphql.Aspnet.GraphType.Core.Models;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.GraphType.Data.EntityFramework.Repositories
{
    public class HumanRepository: BaseRepository<Human, int>, IHumanRepository
    {
        public HumanRepository() { }

        public HumanRepository(StarWarsContext db, ILogger<HumanRepository> logger)
            : base(db, logger)
        {
        }
    }
}
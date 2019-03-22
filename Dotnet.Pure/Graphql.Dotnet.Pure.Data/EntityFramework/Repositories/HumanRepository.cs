using graphql.core.Data;
using graphql.core.Models;
using Microsoft.Extensions.Logging;

namespace graphql.data.EntityFramework.Repositories
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
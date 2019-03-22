using graphql.core.Data;
using graphql.core.Models;
using Microsoft.Extensions.Logging;

namespace graphql.data.InMemory
{
    public class HumanRepository: BaseRepository<Human, int>, IHumanRepository
    {

        public HumanRepository(ILogger<HumanRepository> logger)
            : base(logger)
        {
        }
    }
}
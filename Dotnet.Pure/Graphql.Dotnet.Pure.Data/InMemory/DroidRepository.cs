using graphql.core.Data;
using graphql.core.Models;
using Microsoft.Extensions.Logging;

namespace graphql.data.InMemory
{
    public class DroidRepository: BaseRepository<Droid, int>, IDroidRepository
    {
         public DroidRepository(ILogger<DroidRepository> logger): base(logger)
        {
            Entities.Add(new Droid {Id = 2001, Name = "R2-D2"});
        }

//      public Task<Droid> Get(int id)
//        {
//            return Task.FromResult(_entities.FirstOrDefault(droid => droid.Id == id));
//        }
    }
}
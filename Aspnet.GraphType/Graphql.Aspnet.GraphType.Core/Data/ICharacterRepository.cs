using System.Collections.Generic;
using System.Threading.Tasks;
using Graphql.Aspnet.GraphType.Core.Models;

namespace Graphql.Aspnet.GraphType.Core.Data
{
    public interface ICharacterRepository : IBaseRepository<Character, int>
    {
        Task<ICollection<Character>> GetFriends(int id);
        Task<ICollection<Episode>> GetEpisodes(int id);
    }
}
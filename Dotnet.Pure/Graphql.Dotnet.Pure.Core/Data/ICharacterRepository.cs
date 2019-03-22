using System.Collections.Generic;
using System.Threading.Tasks;
using graphql.core.Models;

namespace graphql.core.Data
{
    public interface ICharacterRepository : IBaseRepository<Character, int>
    {
        Task<ICollection<Character>> GetFriends(int id);
        Task<ICollection<Episode>> GetEpisodes(int id);
    }
}
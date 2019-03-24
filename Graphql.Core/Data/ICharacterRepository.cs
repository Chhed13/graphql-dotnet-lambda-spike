using System.Collections.Generic;
using System.Threading.Tasks;
using Graphql.Core.Models;

namespace Graphql.Core.Data
{
    public interface ICharacterRepository : IBaseRepository<Character, int>
    {
        Task<ICollection<Character>> GetFriends(int id);
        Task<ICollection<Episode>> GetEpisodes(int id);
    }
}
using System.Threading.Tasks;
using graphql.core.Models;

namespace graphql.core.Logic
{
    public interface ITrilogyHeroes
    {
        Task<Character> GetHero(int? episodeId);
    }
}
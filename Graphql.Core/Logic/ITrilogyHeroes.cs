using System.Collections.Generic;
using System.Threading.Tasks;
using Graphql.Core.Models;

namespace Graphql.Core.Logic
{
    public interface ITrilogyHeroes
    {
        Task<Character> GetHero(int episodeId);
        Task<List<Character>> GetHeroes();
    }
}
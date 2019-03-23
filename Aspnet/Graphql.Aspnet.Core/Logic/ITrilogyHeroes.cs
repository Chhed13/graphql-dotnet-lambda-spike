using System.Collections.Generic;
using System.Threading.Tasks;
using Graphql.Aspnet.Core.Models;

namespace Graphql.Aspnet.Core.Logic
{
    public interface ITrilogyHeroes
    {
        Task<Character> GetHero(int episodeId);
        Task<List<Character>> GetHeroes();
    }
}
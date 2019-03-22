using System.Collections.Generic;
using System.Threading.Tasks;
using Graphql.Aspnet.GraphType.Core.Models;

namespace Graphql.Aspnet.GraphType.Core.Logic
{
    public interface ITrilogyHeroes
    {
        Task<Character> GetHero(int? episodeId);
        Task<List<Character>> GetHeroes();
    }
}
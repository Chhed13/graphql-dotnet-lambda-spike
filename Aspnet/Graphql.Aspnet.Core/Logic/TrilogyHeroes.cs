using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;

namespace Graphql.Aspnet.Core.Logic
{
    public class TrilogyHeroes : ITrilogyHeroes
    {
        private readonly IEpisodeRepository _episodeRepository;

        public TrilogyHeroes(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        public async Task<Character> GetHero(int episodeId)
        {
            var episode = await _episodeRepository.Get(episodeId, include: "Hero");
            return episode.Hero;
        }

        public async Task<List<Character>> GetHeroes()
        {
            var episodes = await _episodeRepository.GetAll();
            return episodes.Select(e => e.Hero).ToList();
        }
    }
}
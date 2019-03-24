using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graphql.Core.Data;
using Graphql.Core.Models;

namespace Graphql.Core.Logic
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
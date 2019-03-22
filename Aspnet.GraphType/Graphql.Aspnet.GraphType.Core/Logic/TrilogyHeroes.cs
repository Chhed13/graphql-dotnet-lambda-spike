using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graphql.Aspnet.GraphType.Core.Data;
using Graphql.Aspnet.GraphType.Core.Models;

namespace Graphql.Aspnet.GraphType.Core.Logic
{
    public class TrilogyHeroes: ITrilogyHeroes
    {
        private readonly IEpisodeRepository _episodeRepository;
        private readonly ICharacterRepository _characterRepository;

        public TrilogyHeroes(IEpisodeRepository episodeRepository, ICharacterRepository characterRepository)
        {
            _episodeRepository = episodeRepository;
            _characterRepository = characterRepository;
        }

        public async Task<Character> GetHero(int? episodeId)
        {
            if (episodeId.HasValue)
            {
                var episode = await _episodeRepository.Get(episodeId.Value, include: "Hero");
                return episode.Hero;
            }

            return await _characterRepository.Get(2001);
        }

        public async Task<List<Character>> GetHeroes()
        {
            var episodes = await _episodeRepository.GetAll();
            return episodes.Select(e => e.Hero).ToList();
        }
    }
}
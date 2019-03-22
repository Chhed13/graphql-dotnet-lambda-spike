using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using graphql.core.Data;
using graphql.core.Models;
using Microsoft.Extensions.Logging;

namespace graphql.data.InMemory
{
    public class CharacterRepository : BaseRepository<Character, int>, ICharacterRepository
    {
        public CharacterRepository(ILogger<CharacterRepository> logger) : base(logger)
        {
        }

        public async Task<ICollection<Character>> GetFriends(int id)
        {
            // TODO: find better way to do this?
            var character = await Get(id, "CharacterFriends.Friend");
            return character.CharacterFriends.Select(c => c.Friend).ToList();
        }

        public async Task<ICollection<Episode>> GetEpisodes(int id)
        {
            // TODO: find better way to do this?
            var character = await Get(id, "CharacterEpisodes.Episode");
            return character.CharacterEpisodes.Select(c => c.Episode).ToList();
        }

    }
}
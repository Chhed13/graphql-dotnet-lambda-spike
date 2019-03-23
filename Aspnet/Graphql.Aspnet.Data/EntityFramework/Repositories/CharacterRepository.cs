using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;
using Graphql.Aspnet.GraphType.Data.EntityFramework;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.Data.EntityFramework.Repositories
{
    public class CharacterRepository : BaseRepository<Character, int>, ICharacterRepository
    {
        public CharacterRepository(Context db, ILogger<CharacterRepository> logger) : base(db, logger)
        {
        }

        public async Task<ICollection<Character>> GetFriends(int id)
        {
            var character = await Get(id, "CharacterFriends.Friend");
            return character.CharacterFriends.Select(c => c.Friend).ToList();
        }

        public async Task<ICollection<Episode>> GetEpisodes(int id)
        {
            var character = await Get(id, "CharacterEpisodes.Episode");
            return character.CharacterEpisodes.Select(c => c.Episode).ToList();
        }

    }
}
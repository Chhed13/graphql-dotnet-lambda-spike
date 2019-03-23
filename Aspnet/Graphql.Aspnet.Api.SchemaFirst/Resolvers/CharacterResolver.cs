using System.Collections.Generic;
using System.Linq;
using Graphql.Aspnet.Core.Models;

namespace Graphql.Aspnet.Api.SchemaFirst.Resolvers
{
    public class CharacterResolver<T> where T : Character
    {
        public int Id(T _) => _.Id;

        public string Name(T _) => _.Name;

        public IEnumerable<Episode> AppearsIn(T _) => _.CharacterEpisodes.Select(x => x.Episode);

        public IEnumerable<Character> Friends(T _) => _.CharacterFriends.Select(x => x.Friend);
    }
}
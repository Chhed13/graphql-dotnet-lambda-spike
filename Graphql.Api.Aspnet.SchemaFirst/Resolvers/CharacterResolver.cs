using System.Collections.Generic;
using System.Linq;
using Graphql.Core.Models;

namespace Graphql.Api.Aspnet.SchemaFirst.Resolvers
{
    public class CharacterResolver<T> where T : Character
    {
        public int Id(T _) => _.Id;

        public string Name(T _) => _.Name;

        public IEnumerable<string> AppearsIn(T _) => _.CharacterEpisodes.Select(x => x.Episode.Title);

        public IEnumerable<Character> Friends(T _) => _.CharacterFriends.Select(x => x.Friend);
    }
}
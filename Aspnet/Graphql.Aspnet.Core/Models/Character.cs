using System.Collections.Generic;
using Graphql.Aspnet.Core.Data;

namespace Graphql.Aspnet.Core.Models
{
    public class Character : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CharacterEpisode> CharacterEpisodes { get; set; }
        public ICollection<CharacterFriend> CharacterFriends { get; set; }
        public ICollection<CharacterFriend> FriendCharacters { get; set; }
    }
}
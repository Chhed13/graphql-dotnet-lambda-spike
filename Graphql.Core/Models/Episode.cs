using System.Collections.Generic;
using Graphql.Core.Data;

namespace Graphql.Core.Models
{
    public class Episode : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Character Hero { get; set; }
        public virtual ICollection<CharacterEpisode> CharacterEpisodes { get; set; }
    }
}
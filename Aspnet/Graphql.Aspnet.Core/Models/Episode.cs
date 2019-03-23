using System.Collections.Generic;
using Graphql.Aspnet.Core.Data;

namespace Graphql.Aspnet.Core.Models
{
    public class Episode : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Character Hero { get; set; }
        public virtual ICollection<CharacterEpisode> CharacterEpisodes { get; set; }
    }
}
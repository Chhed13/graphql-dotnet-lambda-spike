using System.Collections.Generic;
using Graphql.Core.Data;

namespace Graphql.Core.Models
{
    public class Planet : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Human> Humans { get; set; }
    }
}
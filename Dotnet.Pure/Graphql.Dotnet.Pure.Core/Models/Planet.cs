using System.Collections.Generic;
using graphql.core.Data;

namespace graphql.core.Models
{
    public class Planet : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Human> Humans { get; set; }
    }
}
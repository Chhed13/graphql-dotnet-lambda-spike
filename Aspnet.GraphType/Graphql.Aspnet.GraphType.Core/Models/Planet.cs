using System.Collections.Generic;
using Graphql.Aspnet.GraphType.Core.Data;

namespace Graphql.Aspnet.GraphType.Core.Models
{
    public class Planet : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Human> Humans { get; set; }
    }
}
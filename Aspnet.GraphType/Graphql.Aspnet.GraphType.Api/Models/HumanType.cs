using Graphql.Aspnet.GraphType.Core.Data;
using Graphql.Aspnet.GraphType.Core.Models;

namespace Graphql.Aspnet.GraphType.Api.Models
{
    public class HumanType: CharacterType<Human>
    {
        public HumanType(ICharacterRepository characterRepository) : base(characterRepository)
        {
            Name = "Human";

            Field(h => h.HomePlanet, nullable: true).Description("The home planet of the human.");
        }
    }
}
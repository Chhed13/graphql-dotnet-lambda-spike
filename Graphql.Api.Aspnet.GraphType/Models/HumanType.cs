using Graphql.Core.Data;
using Graphql.Core.Models;

namespace Graphql.Api.Aspnet.GraphType.Models
{
    public class HumanType : CharacterType<Human>
    {
        public HumanType(ICharacterRepository characterRepository) : base(characterRepository)
        {
            Name = "Human";

            Field("HomePlanet", h => h.HomePlanet != null ? h.HomePlanet.Name : "", nullable: true)
                .Description("The home planet of the human.");
        }
    }
}
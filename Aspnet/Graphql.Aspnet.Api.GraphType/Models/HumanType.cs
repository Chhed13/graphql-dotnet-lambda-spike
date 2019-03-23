using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;

namespace Graphql.Aspnet.Api.GraphType.Models
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
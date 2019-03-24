using Graphql.Core.Data;
using Graphql.Core.Models;

namespace Graphql.Api.Aspnet.GraphType.Models
{
    public class DroidType : CharacterType<Droid>
    {
        public DroidType(ICharacterRepository characterRepository) : base(characterRepository)
        {
            Name = "Droid";
            Description = "A mechanical creature in the Star Wars universe.";

            Field(d => d.PrimaryFunction, nullable: true).Description("The primary function of the droid.");
        }
    }
}
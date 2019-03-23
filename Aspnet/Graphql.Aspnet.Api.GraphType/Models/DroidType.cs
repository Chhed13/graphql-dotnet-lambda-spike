using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;

namespace Graphql.Aspnet.Api.GraphType.Models
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
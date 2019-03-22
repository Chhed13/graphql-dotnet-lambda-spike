using Graphql.Aspnet.GraphType.Core.Models;

namespace Graphql.Aspnet.GraphType.Api.Models
{
    public class DroidType : CharacterType<Droid>
    {
        public DroidType(Graphql.Aspnet.GraphType.Core.Data.ICharacterRepository characterRepository) : base(characterRepository)
        {
            Name = "Droid";
            Description = "A mechanical creature in the Star Wars universe.";

            Field(d => d.PrimaryFunction, nullable: true).Description("The primary function of the droid.");
        }
    }
}
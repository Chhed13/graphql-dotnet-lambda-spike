using Graphql.Aspnet.GraphType.Core.Models;
using GraphQL.Types;

namespace Graphql.Aspnet.GraphType.Api.Models
{
    public class DroidTypeInput : InputObjectGraphType<Droid>
    {
        public DroidTypeInput(Graphql.Aspnet.GraphType.Core.Data.ICharacterRepository characterRepository)
        {
            Name = "DroidInput";

            Field(x => x.Id).Description("The Id of the Droid.");
            Field(x => x.Name, nullable: true).Description("The name of the Droid.");
        }
    }
}
using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;
using GraphQL.Types;

namespace Graphql.Aspnet.Api.GraphType.Models
{
    public class DroidTypeInput : InputObjectGraphType<Droid>
    {
        public DroidTypeInput(ICharacterRepository characterRepository)
        {
            Name = "DroidInput";

            Field(x => x.Id).Description("The Id of the Droid.");
            Field(x => x.Name, nullable: true).Description("The name of the Droid.");
        }
    }
}
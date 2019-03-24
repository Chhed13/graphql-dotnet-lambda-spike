using Graphql.Core.Data;
using Graphql.Core.Models;
using GraphQL.Types;

namespace Graphql.Api.Aspnet.GraphType.Models
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
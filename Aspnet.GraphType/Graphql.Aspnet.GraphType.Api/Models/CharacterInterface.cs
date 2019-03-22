using System.Linq;
using GraphQL.Types;

namespace Graphql.Aspnet.GraphType.Api.Models
{
    public class CharacterInterface : InterfaceGraphType<Graphql.Aspnet.GraphType.Core.Models.Character>
    {
        public CharacterInterface()
        {
            Name = "Character";

            Field(d => d.Id).Description("The id of the character.");
            Field(d => d.Name, nullable: true).Description("The name of the character.");

            Field<ListGraphType<CharacterInterface>>(name: "Friends", resolve: d => d.Source.CharacterFriends.Select(c => c.Friend));
//            Field<ListGraphType<CharacterInterface>>("Friends");
//            Field<ListGraphType<EpisodeEnum>>("AppearsIn", "Which movie they appear in.");
        }
    }
}
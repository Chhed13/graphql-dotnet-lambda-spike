using System.Linq;
using Graphql.Aspnet.Core.Models;
using GraphQL.Types;

namespace Graphql.Aspnet.Api.GraphType.Models
{
    public class CharacterInterface : InterfaceGraphType<Character>
    {
        public CharacterInterface()
        {
            Name = "Character";

            Field(d => d.Id).Description("The id of the character.");
            Field(d => d.Name, nullable: true).Description("The name of the character.");

            Field<ListGraphType<CharacterInterface>>(name: "Friends",
                resolve: d => d.Source.CharacterFriends.Select(c => c.Friend));

            Field<ListGraphType<EpisodeEnum>>("AppearsIn", "Which movie they appear in.", resolve: d =>
                d.Source.CharacterEpisodes.Select(c => c.EpisodeId));
        }
    }
}
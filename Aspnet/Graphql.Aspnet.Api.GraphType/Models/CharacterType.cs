using System.Linq;
using Graphql.Aspnet.Core.Data;
using Graphql.Aspnet.Core.Models;
using GraphQL.Types;

namespace Graphql.Aspnet.Api.GraphType.Models
{
    public class CharacterType<TCharacter>: ObjectGraphType<TCharacter> where TCharacter : Character
    {
        public CharacterType(ICharacterRepository characterRepository)
        {
            Field(h => h.Id).Description("The id of the character.");
            Field(h => h.Name, nullable: true).Description("The name of the character.");

            Field<ListGraphType<CharacterInterface>>(
                "Friends",
                resolve: context =>
                {
                    var friends = characterRepository.GetFriends(context.Source.Id).Result;
                    return friends;
                }
            );

            Field<ListGraphType<EpisodeEnum>>(
                "AppearsIn",
                "Which movie they appear in.",
                resolve: context =>
                {
                    return context.Source.CharacterEpisodes.Select(y => (Episodes)y.Episode.Id).ToArray();
                }
            );

            Interface<CharacterInterface>();
        }
    }
}
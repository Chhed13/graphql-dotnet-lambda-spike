using System.Collections.Generic;
using System.Linq;
using Graphql.Aspnet.GraphType.Core.Data;
using Graphql.Aspnet.GraphType.Core.Models;
using GraphQL.Types;

namespace Graphql.Aspnet.GraphType.Api.Models
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
                    var mapped = mapper.Map<IEnumerable<Character>>(friends);
                    return mapped;
                }
            );

            Field<ListGraphType<EpisodeEnum>>(
                "AppearsIn",
                "Which movie they appear in.",
                resolve: context =>
                {
                    var episodes = characterRepository.GetEpisodes(context.Source.Id).Result;
                    var episodeEnums = episodes.Select(y => (Episodes)y.Id).ToArray();
                    return episodeEnums;
                }
            );

            Interface<CharacterInterface>();
        }
    }
}
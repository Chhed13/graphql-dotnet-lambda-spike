using System.Linq;
using graphql.data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace graphql.Tests.Integration.Data.EntityFramework
{
    public class StarWarsContextShould
    {
        [Fact]
        public async void ReturnR2D2Droid()
        {
            // Given
            using (var db = new StarWarsContext())
            {
                // When
                var r2D2 = await db.Droids
                    .Include("CharacterEpisodes.Episode")
                    .Include("CharacterFriends.Friend")
                    .FirstOrDefaultAsync(d => d.Id == 2001);

                // Then
                Assert.NotNull(r2D2);
                Assert.Equal("R2-D2", r2D2.Name);
                Assert.Equal("Astromech", r2D2.PrimaryFunction);
                var episodes = r2D2.CharacterEpisodes.Select(e => e.Episode.Title);
                Assert.Equal(new[] { "NEWHOPE", "EMPIRE", "JEDI" }, episodes);
                var friends = r2D2.CharacterFriends.Select(e => e.Friend.Name);
                Assert.Equal(new[] { "Luke Skywalker", "Han Solo", "Leia Organa" }, friends);
            }
        }
    }
}
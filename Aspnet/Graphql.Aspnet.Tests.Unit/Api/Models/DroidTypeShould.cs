using Graphql.Aspnet.Api.GraphType.Models;
using Graphql.Aspnet.Core.Data;
using GraphQL.Types;
using Moq;
using Xunit;

namespace Graphql.Aspnet.Tests.Unit.Api.Models
{
    public class DroidTypeShould
    {
        [Fact]
        public void HaveIdAndNameFields()
        {
            //Given
            var characterRepository = new Mock<ICharacterRepository>();

            // When
            var droidType = new DroidType(characterRepository.Object);

            // Then
            Assert.NotNull(droidType);
            Assert.True(droidType.HasField("Id"));
            Assert.Equal(typeof(NonNullGraphType<IntGraphType>), droidType.GetField("Id").Type);
            Assert.True(droidType.HasField("Name"));
            Assert.True(droidType. HasField("Friends"));
            Assert.True(droidType. HasField("AppearsIn"));
            Assert.True(droidType.HasField("PrimaryFunction"));
        }
    }
}
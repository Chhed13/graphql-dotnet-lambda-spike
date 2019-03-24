using Graphql.Api.Aspnet.GraphType.Models;
using Graphql.Core.Data;
using GraphQL.Types;
using Moq;
using Xunit;

namespace Graphql.Tests.Unit.Api.GraphType.Models
{
    public class HumanTypeShould
    {
        [Fact]
        public void HaveIdAndNameFields()
        {
            //Given
            var characterRepository = new Mock<ICharacterRepository>();

            // When
            var humanType = new HumanType(characterRepository.Object);

            // Then
            Assert.NotNull(humanType);
            Assert.True(humanType.HasField("Id"));
            Assert.Equal(typeof(NonNullGraphType<IntGraphType>), humanType.GetField("Id").Type);
            Assert.True(humanType.HasField("Name"));
            Assert.True(humanType. HasField("Friends"));
            Assert.True(humanType. HasField("AppearsIn"));
            Assert.True(humanType.HasField("HomePlanet"));
        }
    }
}
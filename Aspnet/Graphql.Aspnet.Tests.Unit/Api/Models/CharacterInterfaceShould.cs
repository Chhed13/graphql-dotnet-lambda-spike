using Graphql.Aspnet.Api.GraphType.Models;
using Graphql.Aspnet.Core.Data;
using GraphQL.Types;
using Moq;
using Xunit;

namespace Graphql.Aspnet.Tests.Unit.Api.Models
{
    public class CharacterInterfaceShould
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
        }
    }
}
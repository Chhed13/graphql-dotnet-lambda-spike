using AutoMapper;
using Graphql.Aspnet.GraphType.Api;
using Graphql.Aspnet.GraphType.Api.Models;
using Graphql.Aspnet.GraphType.Core.Data;
using GraphQL.Types;
using Moq;
using Xunit;

namespace graphql.Tests.Unit.Api.Models
{
    public class HumanTypeShould
    {
        [Fact]
        public void HaveIdAndNameFields()
        {
            //Given
            var characterRepository = new Mock<ICharacterRepository>();

            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();

            // When
            var humanType = new HumanType(characterRepository.Object, mapper);

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
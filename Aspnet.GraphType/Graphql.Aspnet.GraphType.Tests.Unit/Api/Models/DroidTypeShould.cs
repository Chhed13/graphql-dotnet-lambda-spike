using AutoMapper;
using Graphql.Aspnet.GraphType.Api;
using Graphql.Aspnet.GraphType.Api.Models;
using Graphql.Aspnet.GraphType.Core.Data;
using GraphQL.Types;
using Moq;
using Xunit;

namespace graphql.Tests.Unit.Api.Models
{
    public class DroidTypeShould
    {
        [Fact]
        public void HaveIdAndNameFields()
        {
            //Given
            var characterRepository = new Mock<ICharacterRepository>();

            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();

            // When
            var droidType = new DroidType(characterRepository.Object, mapper);

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
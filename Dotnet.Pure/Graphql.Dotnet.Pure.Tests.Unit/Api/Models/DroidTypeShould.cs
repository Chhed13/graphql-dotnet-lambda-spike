using AutoMapper;
using graphql.api.Models;
using graphql.core.Data;
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
            var mapper = new Mock<Mapper>();

            // When
            var droidType = new DroidType(characterRepository.Object, mapper.Object);

            // Then
            Assert.NotNull(droidType);
            Assert.True(droidType.HasField("Id"));
            Assert.True(droidType.HasField("Name"));
        }
    }
}
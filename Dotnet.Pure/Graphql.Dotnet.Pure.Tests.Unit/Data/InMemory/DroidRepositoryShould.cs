using graphql.data.InMemory;
using Xunit;

namespace graphql.Tests.Unit.Data.InMemory
{
    public class DroidRepositoryShould
    {
        private readonly DroidRepository _droidRepository;

        public DroidRepositoryShould()
        {
            _droidRepository = new DroidRepository(null);
        }

        [Fact, Trait("test", "unit")]
        public async void ReturnR2D2DroidGivenIdOf1()
        {
            var droid = await _droidRepository.Get(1);

            Assert.NotNull(droid);
            Assert.Equal("R2-D2", droid.Name);
        }
    }
}
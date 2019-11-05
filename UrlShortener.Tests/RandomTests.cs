using Xunit;

namespace UrlShortener.Tests
{

    public class RandomTests
    {
        private Random.Random _randomGenerator;

        public RandomTests()
        {
            _randomGenerator = new Random.Random();
        }

        [Theory]
        [InlineData(1)]
        public void StringsShouldBeRandom(int length)
        {
            var newValue = _randomGenerator.Generate(length);
            Assert.Equal(length, newValue.Length);
        }
    }
}

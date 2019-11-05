using UrlShortener.Common.Helpers;
using Xunit;

namespace UrlShortener.Tests
{
    
    public class UrlHelperTests
    {
        [Theory]
        [InlineData("http://google.com")]
        [InlineData("https://google.com")]
        [InlineData("https://google.com/page")]
        [InlineData("https://google.com/page&key=value")]
        public void UrlShouldBeValid(string url)
        {
            Assert.True(UrlHelper.Validate(url));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("some string")]
        [InlineData("ftp://1.1.1.2")]
        [InlineData("google.com")]
        [InlineData("http:bla")]
        [InlineData("http://")]
        public void UrlShouldNotBeValid(string url)
        {
            Assert.False(UrlHelper.Validate(url));
        }
    }
}

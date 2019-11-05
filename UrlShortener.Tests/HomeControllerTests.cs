using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UrlShortener.Controllers;
using UrlShortener.Random;
using UrlShortener.Redis;
using Xunit;

namespace UrlShortener.Tests
{
    public class HomeControllerTests
    {
        private Mock<IRedisClient> _mockedRedis;
        private Mock<IRandom> _mockedRandom;
        private Mock<IHttpContextAccessor> _mockedContext;

        public HomeControllerTests()
        {
            _mockedRedis = new Mock<IRedisClient>();
            _mockedRedis.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            _mockedRandom = new Mock<IRandom>();
            _mockedRandom.Setup(x => x.Generate(It.IsAny<int>())).Returns("abc");

            _mockedContext = new Mock<IHttpContextAccessor>();
            _mockedContext.SetupGet(x => x.HttpContext.Request.Host).Returns(new HostString("localhost"));
        }

        [Theory]
        [InlineData("http://google.com")]
        public void ControllerShouldStore(string url)
        {
            var controller = new HomeController(_mockedRedis.Object, _mockedRandom.Object, _mockedContext.Object);

            var result = controller.AddRedirect(new Models.AddRedirectModel() { Url = url });

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Models.AddRedirectModel>(viewResult.ViewData.Model);
            Assert.Equal("localhost/abc", model.ShortenedUrl);
        }
    }
}

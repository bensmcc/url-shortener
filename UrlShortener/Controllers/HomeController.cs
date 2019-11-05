using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Common.Helpers;
using UrlShortener.Models;
using UrlShortener.Random;
using UrlShortener.Redis;

namespace UrlShortener.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private IRedisClient _redisClient;
        private IRandom _randomGenerator;
        private IHttpContextAccessor _contextAccessor;

        public HomeController(IRedisClient redisClient, IRandom random, IHttpContextAccessor contextAccessor)
        {
            _redisClient = redisClient;
            _randomGenerator = random;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View(new AddRedirectModel());
        }

        [HttpPost]
        public IActionResult AddRedirect(AddRedirectModel redirect)
        {
            if (UrlHelper.Validate(redirect.Url))
            {
                var randomToken = RandomTokenThatDoesntExist();

                if (_redisClient.Add(randomToken, redirect.Url))
                {
                    var shortUrl = $"{_contextAccessor.HttpContext.Request.Host}/{randomToken}";
                    redirect.ShortenedUrl = shortUrl;
                    redirect.Url = null;
                }

                return View("Index", redirect);
            }
            else
            {
                redirect.Error = "It doesn't seem like that URL is valid. Try Again?";
                return View("Index", redirect);
            }
        }
        
        [HttpGet("/{token}")]
        public new IActionResult Redirect(string token)
        {
            if (_redisClient.Exists(token))
            {
                var url = _redisClient.Get(token);
                return RedirectPermanent(url);
            }
            else
            {
                Response.StatusCode = 404;
                var model = new AddRedirectModel() { Error = "Sorry, couldn't find that URL. Create a new one?" };
                return View("Index", model);
            }
        }


        private string RandomTokenThatDoesntExist()
        {
            var randomToken = _randomGenerator.Generate(8);

            if (_redisClient.Exists(randomToken))
            {
                return RandomTokenThatDoesntExist();
            }

            return randomToken;
        }
    }
}

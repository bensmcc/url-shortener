using System;

namespace UrlShortener.Common.Helpers
{
    public static class UrlHelper
    {
        /// <summary>
        /// Given a url, determines if it's a valid http(s) uri.
        /// 
        /// https://stackoverflow.com/questions/7578857/how-to-check-whether-a-string-is-a-valid-http-url
        /// </summary>
        /// <param name="url">Uri to validate.</param>
        /// <returns>True if valid, false if invalid</returns>
        public static bool Validate(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }
    }
}

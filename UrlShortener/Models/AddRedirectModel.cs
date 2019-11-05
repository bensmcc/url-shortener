namespace UrlShortener.Models
{
    public class AddRedirectModel
    {
        public string Error { get; set; }
        public string Url { get; set; }
        public string ShortenedUrl { get; set; }
    }
}

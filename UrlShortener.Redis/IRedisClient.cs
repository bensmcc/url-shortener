namespace UrlShortener.Redis
{
    public interface IRedisClient
    {
        string Get(string key);
        bool Add(string key, string value);
        bool Exists(string key);
    }
}

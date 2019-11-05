using StackExchange.Redis;

namespace UrlShortener.Redis
{
    public class RedisClient : IRedisClient
    {
        private static ConnectionMultiplexer _client;
        private static IDatabase _db;

        static RedisClient()
        {
            _client = ConnectionMultiplexer.Connect("redis");
            _db = _client.GetDatabase();
        }

        public bool Add(string key, string value)
        {
            return _db.StringSet(key, value);            
        }

        public string Get(string key)
        {
            var redisValue = _db.StringGet(key);

            if (redisValue.HasValue)
            {
                return redisValue;
            }

            return null;
        }

        public bool Exists(string key)
        {
            return _db.StringGet(key).HasValue;
        }
    }
}

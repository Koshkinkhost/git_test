using StackExchange.Redis;
namespace api_for_kursach.Services
{
    public interface IRedisService
    {
        Task<string> GetValueASync(string key);
        Task SetValueAsync(string key, string value,TimeSpan? expiry);
        Task<bool> IsKeyExist(string key);
    }
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;
        public RedisService( IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }
        public async Task<string> GetValueASync(string key)
        {
            var value=await _database.StringGetAsync(key);
            return value.HasValue?value.ToString():null;
        }

        public async Task<bool> IsKeyExist(string key)
        {
            return await _database.KeyExistsAsync(key);  
        }

        public async Task SetValueAsync(string key, string value, TimeSpan? expiry)
        {
            await _database.StringSetAsync(key, value, expiry);
            
        }
    }
}

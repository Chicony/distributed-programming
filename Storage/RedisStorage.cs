using StackExchange.Redis;
using System.Collections.Generic;

namespace Storage
{
    public class RedisStorage : IStorage
    {
        private readonly string _host = "localhost";
        private IConnectionMultiplexer _connection;
        
        public RedisStorage()
        {
            _connection = ConnectionMultiplexer.Connect(_host);
        }
        public void SetValue (string key, string value)
        {
            var db = _connection.GetDatabase();
            db.StringSet(key, value);
        }
        public string GetValue (string key)
        {
            var db = _connection.GetDatabase();
            return db.StringGet(key);
        }
        public List<string> GetValues(string prefix)
        {
            var server = _connection.GetServer(_host, 6379);
            List<string> keys = new List<string>();
            foreach (var key in server.Keys(pattern: prefix + "*"))
            {
                keys.Add(key);
            }

            return keys;
        }
    }
}
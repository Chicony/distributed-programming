using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Collections.Generic;

namespace Valuator
{
    public class RedisStorage : IStorage
    {
        private readonly IConnectionMultiplexer connection = ConnectionMultiplexer.Connect("localhost, allowAdmin=true");
        private readonly IDatabase db;
        public RedisStorage() 
        {
            db = connection.GetDatabase();
        }

        public void SetValue (string key, string value)
        {
            db.StringSet(key, value);
        }

        public string GetValue (string key)
        {
            return db.StringGet(key);
        }

        public List<string> GetValues (string pref)
        {
            var address = connection.GetServer("localhost", 6379);
            List<string> key = new List<string>();
            foreach (var val in address.Keys(pattern: "*" + pref + "*"))
            {
                key.Add(val);
            }
            
            return key;
        }
    }
}
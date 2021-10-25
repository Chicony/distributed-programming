using System.Collections.Generic;
namespace Storage
{
    public interface IStorage
    {
        void SetValue  (string key, string value);
        string GetValue  (string key);
        List<string> GetValues(string prefix);
    }
}
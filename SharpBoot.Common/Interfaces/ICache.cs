using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Common.Interfaces
{
    public interface ICache
    {
        bool Exists(string key);
        bool SetExpire(string key, int expSecond);
        long GetExpire(string key);
        T GetCache<T>(string key);
        object GetCache(string key, Type type);
        string GetCacheString(string key);
        void SetCache(string key, object value, int expSecond = 0);
        void RemoveCache(string key);

    }
}

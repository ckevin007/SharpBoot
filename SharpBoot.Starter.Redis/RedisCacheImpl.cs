using Newtonsoft.Json;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Redis
{
    [Component]
    public class RedisCacheImpl : ICache
    {

        public bool Exists(string key)
        {
            return RedisHelper.Exists(key);
        }

        public T GetCache<T>(string key)
        {
            return RedisHelper.Get<T>(key);
        }

        public object GetCache(string key, Type type)
        {
            var str = GetCacheString(key);
            if (string.IsNullOrEmpty(str)) return null;
            try
            {
                return JsonConvert.DeserializeObject(str, type);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetCacheString(string key)
        {
            return RedisHelper.Get(key);
        }

        public long GetExpire(string key)
        {
            return RedisHelper.Ttl(key);
        }

        public void RemoveCache(string key)
        {
            RedisHelper.Del(key);
        }

        public void SetCache(string key, object value, int expSecond = 0)
        {
            RedisHelper.Set(key, value, expSecond);
        }

        public bool SetExpire(string key, int expSecond)
        {
            return RedisHelper.Expire(key, expSecond);
        }
    }
}
